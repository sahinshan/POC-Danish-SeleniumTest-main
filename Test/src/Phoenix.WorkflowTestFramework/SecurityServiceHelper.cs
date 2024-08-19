//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Phoenix.WorkflowTestFramework
//{
//    public partial class PhoenixPlatformServiceHelper
//    {
//        string appKey = "9B31281E8FB34A992876086FA57C9F695D6A5D5F81E2817A94487E691394878B";
//        string appSecret = "E6FF366DC7F9D5B6A7E1F07C6066C039";


//        private string _accessToken;
//        public string AccessToken { get { return _accessToken; } set { _accessToken = value; } }


//        private CareDirector.Sdk.Client.Interfaces.IBusinessDataProvider _DataProvider;
//        private CareDirector.Sdk.Client.Interfaces.IBusinessDataProvider DataProvider
//        {
//            get
//            {
//                if (_DataProvider == null)
//                    _DataProvider = new CareDirector.Sdk.Services.BusinessDataService(AccessToken, 10);

//                return _DataProvider;
//            }
//            set
//            {
//                _DataProvider = value;
//            }
//        }

        

//        private CareDirector.Sdk.Client.Interfaces.ISecurityDataProvider _SecurityDataProvider;
//        private CareDirector.Sdk.Client.Interfaces.ISecurityDataProvider SecurityDataProvider
//        {
//            get
//            {
//                if (_SecurityDataProvider == null)
//                    _SecurityDataProvider = new CareDirector.Sdk.Services.SecurityService(AccessToken, 10);

//                return _SecurityDataProvider;
//            }
//            set
//            {
//                _SecurityDataProvider = value;
//            }
//        }



//        private CareDirector.Sdk.Client.Interfaces.IMetadataProvider _MetadataProvider;
//        private CareDirector.Sdk.Client.Interfaces.IMetadataProvider MetadataProvider
//        {
//            get
//            {
//                if (_MetadataProvider == null)
//                    _MetadataProvider = new CareDirector.Sdk.Services.BusinessMetadataService(AccessToken, 10);

//                return _MetadataProvider;
//            }
//            set
//            {
//                _MetadataProvider = value;
//            }
//        }



//        public CareDirector.Sdk.ServiceRequest.AuthenticateRequest GetAuthenticationRequest(string UserName, string Password)
//        {
//            var envID = Guid.Parse(ConfigurationManager.AppSettings["EnvironmentID"]);
//            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = new CareDirector.Sdk.ServiceRequest.AuthenticateRequest
//            {
//                ApplicationKey = appKey,
//                ApplicationSecret = appSecret,
//                EnvironmentId = envID,
//                BrowserType = "InternetExplorer",
//                BrowserVersion = "11.0",
//                MobileOS = CareDirector.Sdk.Enums.MobileOS.Unknown,
//                Password = Password,
//                UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko",
//                UserIPAddress = "192.168.9.43",
//                UserName = UserName,
//            };
//            return authenticateRequest;
//        }

//        public CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticateUser(CareDirector.Sdk.ServiceRequest.AuthenticateRequest AuthRequest)
//        {
//            return SecurityDataProvider.Authenticate(AuthRequest);
//        }

//        public void SetServiceConnectionDataFromAuthenticationResponse(CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse)
//        {
//            this.AccessToken = authResponse.AccessToken;

//            this.SecurityDataProvider = null; /*this will reset the security data provider service. the next time the object is called the new service connection will be used (with the security token and all remain information)*/
//            this.DataProvider = null; /*this will reset the data provider service. the next time the object is called the new service connection will be used (with the security token and all remain information)*/
//        }

//        public CareDirector.Sdk.Interface.IUserSetting GetMetadataUserSettings()
//        {
//            return MetadataProvider.GetUserSetting();
//        }
        
//    }
//}
