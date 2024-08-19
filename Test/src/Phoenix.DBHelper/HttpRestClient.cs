using CareDirector.Sdk.Configuration;
using CareDirector.Sdk.Helpers;
using CareDirector.Sdk.ServiceRequest;
using CareDirector.Sdk.ServiceResponse;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Exceptions;
using CareWorks.Foundation.Extensions;
using CareWorks.Foundation.Helpers;
using CareWorks.Foundation.Interface;
using CareWorks.Foundation.SystemEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CareWorks.Foundation.Logging;

namespace Phoenix.DBHelper
{
    public class HttpRestClient
    {
        #region fields

        public readonly HttpClient _httpClient;
        private readonly ProductInfoHeaderValue _userAgent;

        #endregion

        #region constructor

        public HttpRestClient(string baseUrl, string accessToken)
        {
            BaseUrl = baseUrl;
            AccessToken = accessToken;
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromMinutes(GetTimeOut())
            };
            _userAgent = new ProductInfoHeaderValue(AppSettings.ApplicationName.GetValueOrDefault("CareCloud"), VersionHelper.GetRunningVersion());
        }

        public HttpRestClient(string baseUrl) : this(baseUrl, null) { }

        public HttpRestClient() : this(null, null) { }

        #endregion

        #region properties

        public string ApiKey { get; set; }

        public IFileLog FileLogger { get; set; }

        public string AccessToken { get; set; }

        public string BaseUrl { get; set; }

        #endregion

        public bool UseTenantBasedUrl()
        {
            var response = Get<RetrieveResponse<bool>>("/api/auth/use-tenant-base-url");
            if (response.HasErrors)
                throw response.Exception.ToCareDirectorException();

            return response.Data;
        }

        public string GetServiceUrl(string serviceName)
        {
            return Get<string>($"/api/service-discovery/{serviceName}");
        }

        #region authentication methods

        public string GetAccessToken(string username, string oneTimePassword, Guid tenantId)
        {
            var request = new AuthenticateRequest
            {
                UseOneTimePassword = true,
                UserName = username,
                Password = oneTimePassword,
                EnvironmentId = tenantId,
                ApplicationKey = AppSettings.ApplicationKey,
                ApplicationSecret = AppSettings.ApplicationSecret
            };
            var response = Post<AccessTokenResponse, AuthenticateRequest>("api/auth/token-by-code", request);
            return response.AccessToken;
        }

        public string GetAccessToken(string username, string oneTimePassword, string tenantName)
        {
            var request = new AuthenticateRequest
            {
                UseOneTimePassword = true,
                UserName = username,
                Password = oneTimePassword,
                TenantName = tenantName,
                ApplicationKey = AppSettings.ApplicationKey,
                ApplicationSecret = AppSettings.ApplicationSecret
            };
            var response = Post<AccessTokenResponse, AuthenticateRequest>("api/auth/token-by-code", request);
            return response.AccessToken;
        }

        public string GetAccessToken()
        {
            var request = new Dictionary<string, string>
                {
                    { "client_id", $"{AppSettings.ApplicationKey}|{AppSettings.EnvironmentId}" },
                    { "client_secret",  AppSettings.ApplicationSecret }
                };

            var url = BaseUrl.CombineUrl("api/auth/token");
            HttpResponseMessage response = _httpClient.PostAsync(url, new FormUrlEncodedContent(request)).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<AccessTokenResponse>(data).AccessToken;
            }
            else
            {
                var exception = ProcessException(response, url);
                throw exception;
            }
        }

        public AuthenticateResponse ValidatePin(EnterPinServiceRequest request)
        {
            return Post<AuthenticateResponse, EnterPinServiceRequest>("api/auth/validate-pin", request);
        }

        public RetrieveResponse<bool> ResendPin(RequestNewPinServiceRequest request)
        {
            return Post<RetrieveResponse<bool>, RequestNewPinServiceRequest>("api/auth/resend-pin", request);
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            return Post<AuthenticateResponse, AuthenticateRequest>("api/auth/auth-internal", request);
        }

        public AuthenticateResponse Authenticate(AuthenticateExternalIdetityProviderRequest request)
        {
            return Post<AuthenticateResponse, AuthenticateExternalIdetityProviderRequest>("api/auth/auth-external", request);
        }

        #endregion

        #region basic methods

        public RetrieveMultipleResponse<T> GetMultiple<T>(string route)
        {
            return Get<RetrieveMultipleResponse<T>>(route);
        }

        public T Get<T>(string route)
        {
            return Get<T>(route, null);
        }

        public T Get<T>(string route, Dictionary<string, string> requestHeaders)
        {
            var httpRequest = GetRequest(HttpMethod.Get, BaseUrl.CombineUrl(route));

            if (requestHeaders != null)
            {
                foreach (var item in requestHeaders)
                {
                    httpRequest.Headers.Add(item.Key, item.Value);
                }
            }

            HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                var exception = ProcessException(response, BaseUrl.CombineUrl(route));
                throw exception;
            }
        }

        public T Post<T, U>(string route, U request)
        {
            var content = JsonConvert.SerializeObject(request);
            var httpRequest = GetRequest(HttpMethod.Post, BaseUrl.CombineUrl(route));

            httpRequest.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            var response = _httpClient.SendAsync(httpRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                var exception = ProcessException(response, BaseUrl.CombineUrl(route));
                throw exception;
            }
        }

        public T Post<T>(string route)
        {
            var httpRequest = GetRequest(HttpMethod.Post, BaseUrl.CombineUrl(route));

            var response = _httpClient.SendAsync(httpRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                var exception = ProcessException(response, BaseUrl.CombineUrl(route));
                throw exception;
            }
        }

        public void Post<U>(string route, U request)
        {
            var content = JsonConvert.SerializeObject(request);
            var httpRequest = GetRequest(HttpMethod.Post, BaseUrl.CombineUrl(route));

            httpRequest.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            var response = _httpClient.SendAsync(httpRequest).Result;
            if (!response.IsSuccessStatusCode)
            {
                var exception = ProcessException(response, BaseUrl.CombineUrl(route));
                throw exception;
            }
        }

        public T Put<T, U>(string route, U request)
        {
            var content = JsonConvert.SerializeObject(request);
            var httpRequest = GetRequest(HttpMethod.Put, BaseUrl.CombineUrl(route));

            httpRequest.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            var response = _httpClient.SendAsync(httpRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                var exception = ProcessException(response, BaseUrl.CombineUrl(route));
                throw exception;
            }
        }

        public void Put<U>(string route, U request)
        {
            var content = JsonConvert.SerializeObject(request);
            var httpRequest = GetRequest(HttpMethod.Put, BaseUrl.CombineUrl(route));

            httpRequest.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            var response = _httpClient.SendAsync(httpRequest).Result;
            if (!response.IsSuccessStatusCode)
            {
                var exception = ProcessException(response, BaseUrl.CombineUrl(route));
                throw exception;
            }
        }

        public void Delete(string route)
        {
            var httpRequest = GetRequest(HttpMethod.Delete, BaseUrl.CombineUrl(route));
            var response = _httpClient.SendAsync(httpRequest).Result;
            if (!response.IsSuccessStatusCode)
            {
                var exception = ProcessException(response, BaseUrl.CombineUrl(route));
                throw exception;
            }
        }

        public CreateResult PutFile(string route, System.IO.Stream file, string fileName)
        {
            var httpRequest = GetRequest(HttpMethod.Put, BaseUrl.CombineUrl(route));
            httpRequest.Content = GetMultipartFormDataContent(file, fileName);

            var response = _httpClient.SendAsync(httpRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<CreateResult>(data);
            }
            else
            {
                var exception = ProcessException(response, BaseUrl.CombineUrl(route));
                throw exception;
            }
        }

        public Tuple<string, byte[]> GetFile(string route)
        {
            var httpRequest = GetRequest(HttpMethod.Get, BaseUrl.CombineUrl(route));
            HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                var fileName = response.Content.Headers.ContentDisposition.FileName;
                return new Tuple<string, byte[]>(fileName, response.Content.ReadAsByteArrayAsync().Result);
            }
            else
            {
                var exception = ProcessException(response, BaseUrl.CombineUrl(route));
                throw exception;
            }
        }

        #endregion

        #region private methods

        private int GetTimeOut()
        {
            var timeout = AppSettingsHelper.GetIntValue("HttpClientTimeoutInMinutes", 10);
            if (timeout < 10)
                return 10;

            return timeout;
        }

        public MultipartFormDataContent GetMultipartFormDataContent(System.IO.Stream file, string fileName)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            content.Add(fileContent, "files", fileName);

            return content;
        }

        public HttpRequestMessage GetRequest(HttpMethod httpMethod, string route)
        {
            var httpRequestMessage = new HttpRequestMessage(httpMethod, route);
            if (AccessToken.IsNotEmpty())
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            httpRequestMessage.Headers.UserAgent.Add(_userAgent);

            if (ApiKey.IsNotEmpty())
                httpRequestMessage.Headers.Add(SystemConstants.ApiKeyHeader, ApiKey);

            return httpRequestMessage;
        }

        public ApiException ProcessException(HttpResponseMessage response, string route)
        {
            try
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Logger.TraceError(data);
                try
                {
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(data);
                    string message = string.Empty;
                    string code = string.Empty;

                    if (errorResponse != null)
                    {
                        message = errorResponse.ErrorMessage;
                        code = errorResponse.ErrorCode;
                    }

                    if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(message))
                        return new ApiException($"Error calling route: {route}. Error: {response.ReasonPhrase}", "UnknownError");

                    return new ApiException(message, code);
                }
                catch (Exception ex)
                {
                    FileLogger?.WriteLog(ex);
                    FileLogger?.WriteLog(data);
                    Logger.TraceError(ex);
                    return new ApiException($"Error calling route: {route}. Error: {response.ReasonPhrase}. No ErrorResponse.", "UnknownError");
                }
            }
            catch (Exception)
            {
                return new ApiException($"Error calling route: {route}. Error: {response.ReasonPhrase}. No Contents.", response.ReasonPhrase);
            }
        }

        #endregion
    }
}
