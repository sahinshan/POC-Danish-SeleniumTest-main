using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Helpers;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.ServiceRequest;
using CareDirector.Sdk.ServiceResponse;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using CareWorks.Foundation.Extensions;
using CareWorks.Foundation.SystemEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Phoenix.DBHelper
{
    public abstract class BaseClass
    {
        #region Properties

        string appKey = "9B31281E8FB34A992876086FA57C9F695D6A5D5F81E2817A94487E691394878B";
        string appSecret = "E6FF366DC7F9D5B6A7E1F07C6066C039";

        private string AccessToken { get; set; }

        private static HttpRestClient _restClient = new HttpRestClient();

        private HttpRestClient GetRestClient()
        {
            _restClient.BaseUrl = GetAutomationServiceUri();
            _restClient.AccessToken = AccessToken;
            return _restClient;
        }

        #endregion

        #region Authentication

        internal AuthenticateRequest GetAuthenticationRequest(string UserName, string Password)
        {
            var tenantName = ConfigurationManager.AppSettings["TenantName"];
            return GetAuthenticationRequest(UserName, Password, tenantName);
        }

        internal AuthenticateRequest GetAuthenticationRequest(string UserName, string Password, string TenantName)
        {
            AuthenticateRequest authenticateRequest = new AuthenticateRequest
            {
                ApplicationKey = appKey,
                ApplicationSecret = appSecret,
                TenantName = TenantName,
                BrowserType = "InternetExplorer",
                BrowserVersion = "11.0",
                MobileOS = CareDirector.Sdk.Enums.MobileOS.Unknown,
                Password = Password,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko",
                UserIPAddress = "192.168.9.43",
                UserName = UserName,
                ScreenSize = 100M,
                OSVersion = "1",
                RetrieveSecurityData = false,
                ApplicationVersion = "6.2.9"

            };
            return authenticateRequest;
        }

        internal void SetServiceConnectionDataFromAuthenticationResponse(AuthenticateResponse authResponse)
        {
            this.AccessToken = authResponse.AccessToken;
        }

        internal AuthenticateResponse AuthenticateUser(string TenantName = null)
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string DataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

            if (DataEncoded.Equals("true"))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(username);
                username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                base64EncodedBytes = System.Convert.FromBase64String(password);
                password = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

            return AuthenticateUser(username, password, TenantName);
        }

        internal AuthenticateResponse AuthenticateUser(string UserName, string Password, string TenantName)
        {
            if (System.Net.ServicePointManager.SecurityProtocol == (System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls))
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

            AuthenticateRequest authenticationRequest;

            if (!string.IsNullOrEmpty(TenantName))
                authenticationRequest = GetAuthenticationRequest(UserName, Password, TenantName);
            else
                authenticationRequest = GetAuthenticationRequest(UserName, Password);

            _restClient.BaseUrl = GetServiceUri();
            _restClient.AccessToken = "";
            var authResponse = _restClient.Authenticate(authenticationRequest);

            if (authResponse.HasErrors && !string.IsNullOrEmpty(authResponse.Error))
            {
                if (authResponse.Exception != null)
                    throw new Exception(authResponse.Exception.Category + "\r\n" + authResponse.Exception.Category + "\r\n" + authResponse.Exception.ErrorCode + "\r\n" + authResponse.Exception.Message);
                else
                    throw new Exception(authResponse.Error);
            }

            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            return authResponse;
        }

        #endregion

        #region Product Languages

        internal Dictionary<string, Guid> RetrieveProductLanguages()
        {
            var response = GetRestClient().Get<List<ProductLanguage>>("product-languages");
            return response.ToDictionary(c => c.Name, c => c.Id.Value);
        }

        #endregion

        #region Updating Records

        internal void UpdateRecord(BusinessData record)
        {
            ConvertDateFields(record, false);
            GetRestClient().Put<Guid?, BusinessData>("", record);
            System.Threading.Thread.Sleep(50);
        }

        internal void Assign(AssignRequest request)
        {
            GetRestClient().Put<AssignRequest>("assign", request);
            System.Threading.Thread.Sleep(50);
        }

        private void ConvertDateFields(BusinessData record, bool removeNullValue)
        {
            var collection = new NameValueDictionary<object>();
            foreach (var item in record.FieldCollection)
            {
                if (removeNullValue && item.Value == null)
                    continue;

                if (item.Value != null && item.Value.GetType().ToString() == "System.DateTime")
                {
                    var date = ((DateTime)item.Value);
                    if (date.Hour == 0 && date.Minute == 0 && date.Second == 0 && date.Kind != DateTimeKind.Utc)
                    {
                        collection.Add(item.Key, date.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        if (date.Kind != DateTimeKind.Utc)
                            collection.Add(item.Key, date.ToString("yyyy-MM-ddTHH:mm:ss"));
                        else
                            collection.Add(item.Key, date.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                    }
                }
                else
                {
                    collection.Add(item.Key, item.Value);
                }
                record.FieldCollection = collection;
            }
        }

        #endregion

        #region Creating Records

        internal BusinessData GetBusinessDataBaseObject(string BusinessObjectName, string PrimaryKeyName)
        {
            var _businessData = new BusinessData(BusinessObjectName.ToLower(), PrimaryKeyName.ToLower());
            _businessData.MultiSelectBusinessObjectFields = new MultiSelectBusinessObjectDataDictionary();

            return _businessData;
        }

        internal void AddFieldToBusinessDataObject(BusinessData BussinessDataObject, string FieldName, DataType FieldDataType, BusinessObjectFieldType ObjectFieldType, bool IsUtcDate, object FieldValue)
        {
            BussinessDataObject.FieldCollection.Add(FieldName, FieldValue);
        }

        internal void AddFieldToBusinessDataObject(BusinessData BussinessDataObject, string FieldName, object FieldValue)
        {
            BussinessDataObject.FieldCollection.Add(FieldName.ToLower(), FieldValue);
        }

        internal void AddMultiSelectBusinessObjectData(BusinessData buisinessDataObject, string BusinessObjectField, Dictionary<Guid, string> MultiSelectData, string ReferenceIdTableName)
        {
            buisinessDataObject.MultiSelectBusinessObjectFields[BusinessObjectField] = new MultiSelectBusinessObjectDataCollection();

            if (MultiSelectData != null && MultiSelectData.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> data in MultiSelectData)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = data.Key,
                        ReferenceIdTableName = ReferenceIdTableName,
                        ReferenceName = data.Value

                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields[BusinessObjectField].Add(dataRecord);
                }
            }
        }

        internal Guid CreateRecord(BusinessData record)
        {
            ConvertDateFields(record, true);
            var id = GetRestClient().Post<Guid?, BusinessData>("", record);
            System.Threading.Thread.Sleep(200);
            return id.Value;
        }

        internal List<Guid> CreateMultipleRecords(List<BusinessData> records)
        {
            foreach (var record in records)
            {
                ConvertDateFields(record, true);
            }

            var response = GetRestClient().Post<List<Guid>, List<BusinessData>>("create-multiple", records);
            return response;
        }

        #endregion

        #region Executing Queries

        internal DataQuery GetDataQueryObject(string tableName, bool allfields, string primaryKeyName)
        {
            DataQuery query = new DataQuery(tableName.ToLower(), allfields);
            query.PrimaryKeyName = primaryKeyName.ToLower();
            if (!allfields)
                query.AddThisTableField(primaryKeyName);

            return query;
        }

        internal void BaseClassAddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType)
        {
            query.AddThisTableCondition(FieldName.ToLower(), OperatorType);
        }

        internal void BaseClassAddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, string ConditionValue)
        {
            query.AddThisTableCondition(FieldName.ToLower(), OperatorType, ConditionValue);
        }

        internal void BaseClassAddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, Guid ConditionValue)
        {
            query.AddThisTableCondition(FieldName.ToLower(), OperatorType, ConditionValue);
        }

        internal void BaseClassAddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, Guid? ConditionValue)
        {
            query.AddThisTableCondition(FieldName.ToLower(), OperatorType, ConditionValue);
        }

        internal void BaseClassAddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, int ConditionValue)
        {
            query.AddThisTableCondition(FieldName.ToLower(), OperatorType, ConditionValue);
        }

        internal void BaseClassAddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, int? ConditionValue)
        {
            query.AddThisTableCondition(FieldName.ToLower(), OperatorType, ConditionValue);
        }

        internal void BaseClassAddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, TimeSpan ConditionValue)
        {
            query.AddThisTableCondition(FieldName.ToLower(), OperatorType, ConditionValue);
        }

        internal void BaseClassAddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, DateTime ConditionValue)
        {
            query.AddThisTableCondition(FieldName.ToLower(), OperatorType, ConditionValue);
        }

        internal void BaseClassAddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, bool ConditionValue)
        {
            query.AddThisTableCondition(FieldName.ToLower(), OperatorType, ConditionValue);
        }

        internal void AddRelatedTableRelationship(DataQuery query, string rightTableName, string rightTableFieldName, string leftTableAlias, string leftTableFieldName, JoinOperator operation, string rightTableAlias)
        {
            query.AddRelatedTableRelationship(rightTableName, rightTableFieldName, leftTableAlias, leftTableFieldName, operation, rightTableAlias);
        }

        internal void AddRelatedTableCondition(DataQuery query, string tableAlias, string FieldName, ConditionOperatorType OperatorType, string ConditionValue)
        {
            query.AddRelatedTableCondition(tableAlias, FieldName, OperatorType, ConditionValue);
        }

        internal void AddRelatedTableCondition(DataQuery query, string tableAlias, string FieldName, ConditionOperatorType OperatorType, Guid ConditionValue)
        {
            query.AddRelatedTableCondition(tableAlias, FieldName, OperatorType, ConditionValue);
        }

        internal void AddReturnField(DataQuery query, string TableName, string FieldName)
        {
            query.AddField(TableName.ToLower(), FieldName.ToLower(), FieldName.ToLower());
        }

        internal void AddReturnFields(DataQuery query, string TableName, string[] FieldNames)
        {
            foreach (var field in FieldNames)
                query.AddField(TableName, field, field);

        }

        internal List<Guid> ExecuteDataQueryAndExtractGuidFields(DataQuery query, string FieldName)
        {
            BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.BusinessDataCollection == null || response.BusinessDataCollection.Count == 0)
                return new List<Guid>();

            List<Guid> data = new List<Guid>();

            foreach (var businessData in response.BusinessDataCollection)
                if (businessData.FieldCollection[FieldName.ToLower()] != null)
                {
                    string fieldData = businessData.FieldCollection[FieldName.ToLower()].ToString();
                    data.Add(Guid.Parse(fieldData));
                }

            return data;
        }

        internal List<int> ExecuteDataQueryAndExtractIntFields(DataQuery query, string FieldName)
        {
            BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.BusinessDataCollection == null || response.BusinessDataCollection.Count == 0)
                return new List<int>();

            return response.BusinessDataCollection
                .Where(c => c.FieldCollection.ContainsKey(FieldName.ToLower()))
                .Select(c => int.Parse(c.FieldCollection[FieldName.ToLower()].ToString()))
                .ToList();

        }

        internal Dictionary<string, object> ExecuteDataQueryAndExtractFirstResultFields(DataQuery query)
        {
            BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.BusinessDataCollection == null || response.BusinessDataCollection.Count == 0)
                return new Dictionary<string, object>();

            var dictionary = new Dictionary<string, object>();
            foreach (var item in response.BusinessDataCollection[0].FieldCollection)
            {
                var valueType = item.Value.GetType().ToString();
                if (valueType == "System.Int64")
                {
                    dictionary.Add(item.Key, int.Parse(item.Value.ToString()));
                    continue;
                }

                if (item.Value.GetType().ToString() == "System.String")
                {
                    var stringValue = item.Value.ToString();
                    if (stringValue.Length == 36 && Guid.TryParse(stringValue, out Guid id))
                    {
                        dictionary.Add(item.Key, id);
                        continue;
                    }

                    if (stringValue.IndexOf(":") == 2 && TimeSpan.TryParse(stringValue, out TimeSpan timeSpan))
                    {
                        dictionary.Add(item.Key, timeSpan);
                        continue;
                    }

                    if (stringValue.IndexOf("-") > 0 && DateTime.TryParse(stringValue, out DateTime date))
                    {
                        dictionary.Add(item.Key, date);
                        continue;
                    }
                }

                dictionary.Add(item.Key, item.Value);
            }

            return dictionary;
        }

        protected BusinessDataCollectionResponse ExecuteDataQuery(DataQuery query)
        {
            var response = GetRestClient().Post<BusinessDataCollectionResponse, DataQuery>("query", query);
            if (response.HasErrors)
                throw response.Exception.ToCareDirectorException();

            return response;
        }

        #endregion

        #region Delete Records

        internal void DeleteRecord(string businessObjectName, Guid id)
        {
            GetRestClient().Delete($"{businessObjectName}/{id}");
        }

        #endregion

        #region Uploading Files

        internal Guid UploadFile(string filePath, string businessObjectName, string businessObjectFieldName, Guid recordId)
        {
            using (var stream = System.IO.File.OpenRead(filePath))
            {
                var fileName = Path.GetFileName(filePath);
                var httpRequest = GetRestClient().GetRequest(HttpMethod.Post, GetAutomationServiceUri().CombineUrl("upload-file"));

                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
                StreamContent fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                multipartFormDataContent.Add(fileContent, "files", fileName);

                fileContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("id", recordId.ToString()));
                fileContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("BusinessObjectTableName", businessObjectName));
                fileContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("BusinessObjectFieldName", businessObjectFieldName));
                fileContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("FileType", "1"));

                httpRequest.Content = multipartFormDataContent;

                var response = GetRestClient()._httpClient.SendAsync(httpRequest).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Guid>(data);
                }
                else
                {
                    var exception = GetRestClient().ProcessException(response, GetAutomationServiceUri().CombineUrl("upload-file"));
                    throw exception;
                }
            }
        }

        #endregion

        #region Import Documents

        internal void ImportDocument(byte[] document, string FileName)
        {
            var packageFile = new PackageFile()
            {
                Name = FileName,
                Data = document,
                Extension = ".zip",
                FileSize = document.Length
            };

            var id = GetRestClient().Post<Guid?, PackageFile>("upload-datapackage-file", packageFile);

            var importPackageRequestObject = new ImportPackageRequest()
            {
                FileId = id.Value,
                ProcessId = Guid.NewGuid(),
                Data = document,
                UpgradeMode = false,
            };

            GetRestClient().Post<ImportPackageRequest>("import-data-package", importPackageRequestObject);

            var response = GetRestClient().Get<ExportImportNotification>($"import-datapackage-progress/{importPackageRequestObject.ProcessId}");

            int count = 0;
            while (response.Status == ExportImportProcessStatus.InProgress || response.Status == ExportImportProcessStatus.NotStarted)
            {
                count++;

                if (count > 90)
                    return; //if we wait for 90 seconds and the import is not completed we will simply return

                System.Threading.Thread.Sleep(1000);
                response = GetRestClient().Get<ExportImportNotification>($"import-datapackage-progress/{importPackageRequestObject.ProcessId}");
            }

            if (response.Status != ExportImportProcessStatus.Completed)
            {
                throw new Exception(response.LogText + response.Error + response.Message + response.Exception);
            }

        }

        #endregion

        #region Pin Records

        internal void PinRecord(Guid recordId, string businessObjectName, Guid userId)
        {
            GetRestClient().Post<bool>($"pin-record/{recordId}/{businessObjectName}/{userId}");
        }

        #endregion

        #region restrictions

        public void RestrictRecord(Guid recordId, string businessObjectName, Guid restrictionId)
        {
            GetRestClient().Post<bool>($"restrict-record/{recordId}/{businessObjectName}/{restrictionId}");
        }

        public void RemoveRecordRestriction(Guid recordId, string businessObjectName)
        {
            GetRestClient().Post<bool>($"remove-restriction/{recordId}/{businessObjectName}");
        }

        #endregion

        private string GetServiceUri()
        {
            return ConfigurationManager.AppSettings["appURL"];
        }

        private string GetAutomationServiceUri()
        {
            return ConfigurationManager.AppSettings["appURL"].CombineUrl("api/automation-test");
        }
    }
}
