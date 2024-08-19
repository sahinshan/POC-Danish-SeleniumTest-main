using CareDirector.Sdk.Query;
using CareDirector.Sdk.ServiceRequest;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public abstract class BaseClass
    {

        #region Field

        internal string appKey;
        internal string appSecret;
        internal Guid environmentID;

        internal CareDirector.Sdk.Client.Interfaces.ISecurityDataProvider _SecurityDataProvider;
        internal CareDirector.Sdk.Client.Interfaces.IBusinessDataProvider _DataProvider;
        internal CareDirector.Sdk.Client.Interfaces.IMetadataProvider _MetaDataProvider;


        #endregion

        #region Property

        private string _accessToken;
        public string AccessToken { get { return _accessToken; } set { _accessToken = value; } }

        internal CareDirector.Sdk.Client.Interfaces.IMetadataProvider MetadataProvider
        {
            get
            {
                if (_MetaDataProvider == null)
                    _MetaDataProvider = new CareDirector.Sdk.Services.BusinessMetadataService(AccessToken, 10);

                return _MetaDataProvider;
            }
            set
            {
                _MetaDataProvider = value;
            }
        }

        internal CareDirector.Sdk.Client.Interfaces.IBusinessDataProvider DataProvider
        {
            get
            {
                if (_DataProvider == null)
                    _DataProvider = new CareDirector.Sdk.Services.BusinessDataService(AccessToken, 10);

                return _DataProvider;
            }
            set
            {
                _DataProvider = value;
            }
        }
        internal CareDirector.Sdk.Client.Interfaces.ISecurityDataProvider SecurityDataProvider
        {
            get
            {
                if (_SecurityDataProvider == null)
                    if (string.IsNullOrEmpty(AccessToken))
                        _SecurityDataProvider = new CareDirector.Sdk.Services.SecurityService(10);
                    else
                        _SecurityDataProvider = new CareDirector.Sdk.Services.SecurityService(AccessToken, 10);

                return _SecurityDataProvider;
            }
            set
            {
                _SecurityDataProvider = value;
            }
        }

        #endregion


        public BaseClass()
        {
            appKey = ConfigurationManager.AppSettings["appKey"];
            appSecret = ConfigurationManager.AppSettings["appSecret"];
            environmentID = Guid.Parse(ConfigurationManager.AppSettings["EnvironmentID"]);
        }

        public void AuthenticateUser()
        {
            if (System.Net.ServicePointManager.SecurityProtocol == (System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls))
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

            string UserName = ConfigurationManager.AppSettings["DefaultUserName"];
            string Password = ConfigurationManager.AppSettings["DefaultUserPassword"];

            var authenticationRequest = GetAuthenticationRequest(UserName, Password);
            var authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);
        }

        public void AuthenticateUser(string UserName, string Password)
        {
            if (System.Net.ServicePointManager.SecurityProtocol == (System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls))
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

            var authenticationRequest = GetAuthenticationRequest(UserName, Password);
            var authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

        }

       
        private CareDirector.Sdk.ServiceRequest.AuthenticateRequest GetAuthenticationRequest(string UserName, string Password)
        {
            AccessToken = null;
            DataProvider = null;
            SecurityDataProvider = null;

            var authenticateRequest = new CareDirector.Sdk.ServiceRequest.AuthenticateRequest
            {
                ApplicationKey = appKey,
                ApplicationSecret = appSecret,
                EnvironmentId = environmentID,
                BrowserType = "InternetExplorer",
                BrowserVersion = "11.0",
                MobileOS = CareDirector.Sdk.Enums.MobileOS.Unknown,
                Password = Password,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko",
                UserIPAddress = "192.168.9.43",
                UserName = UserName,
            };
            return authenticateRequest;
        }

        private void SetServiceConnectionDataFromAuthenticationResponse(CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse)
        {
            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            AccessToken = authResponse.AccessToken;

            this.SecurityDataProvider = null; /*this will reset the security data provider service. the next time the object is called the new service connection will be used (with the security token and all remain information)*/
            this.DataProvider = null; /*this will reset the data provider service. the next time the object is called the new service connection will be used (with the security token and all remain information)*/
        }


        public void RefreshObjectCache(string BusinessObjectName)
        {
            var sc = new CareWorks.Foundation.SystemEntities.ServiceConnection();
            sc.AccessToken = CareWorks.Foundation.Encryption.HMACEncryptor.EncryptAccessToken("E6FF366DC7F9D5B6A7E1F07C6066C039", AccessToken, DateTime.UtcNow);

            Type t = typeof(CareDirector.Sdk.Services.BusinessDataService).Assembly.GetType("CareDirector.Sdk.Services.CacheService");
            object cacheManager = Activator.CreateInstance(t, new object[] { sc });

            var syncContext = new CareWorks.Foundation.SystemEntities.CacheSyncContext();
            syncContext.SyncType = CacheSyncType.SyncByType;
            syncContext.EntityLogicalName = BusinessObjectName;

            var methodSyncCache = cacheManager.GetType().GetMethod("SyncCache", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            methodSyncCache.Invoke(cacheManager, new object[1] { syncContext });
        }


        #region Create / Update records

        internal BusinessData GetBusinessDataBaseObject(string BusinessObjectName, string PrimaryKeyName)
        {
            BusinessData businessData = new BusinessData()
            {
                BusinessObjectName = BusinessObjectName,
                MultiSelectBusinessObjectFields = new MultiSelectBusinessObjectDataDictionary(),
                MultiSelectOptionSetFields = new MultiSelectOptionSetDataDictionary(),
                PrimaryKeyName = PrimaryKeyName,
            };

            return businessData;
        }

        internal void AddFieldToBusinessDataObject(BusinessData BussinessDataObject, string FieldName, object FieldValue)
        {
            BussinessDataObject.FieldCollection.Add(FieldName, FieldValue);
        }

        internal void UpdateRecord(BusinessData BussinessDataObject)
        {
            var response = DataProvider.Update(BussinessDataObject);

            if (response != null && response.HasErrors)
                throw new Exception(response.Error);
        }

        internal Guid CreateRecord(BusinessData record)
        {
            var response = DataProvider.Create(record);

            if (response.HasErrors)
                throw new Exception(response.Error);

            return response.Id.Value;
        }

        internal void PinRecord(Guid RecordID, string BusinessObjectName, Guid UserId)
        {
            var response = DataProvider.PinRecord(new List<Guid> { RecordID }, BusinessObjectName, UserId);

            if (response.HasErrors)
                throw new Exception(response.Error);
        }

        #endregion

        #region Delete Record

        internal void DeleteRecord(string BusinessObjectName, Guid BusinessObjectID)
        {
            var response = DataProvider.Delete(BusinessObjectName, BusinessObjectID);

            if (response.HasErrors)
                throw new Exception(response.Error);
        }

        #endregion

        #region Perform Queries

        internal DataQuery GetDataQueryObject(string TableName, bool Allfields, string PrimaryKeyName)
        {
            DataQuery query = new DataQuery(TableName, Allfields);
            query.PrimaryKeyName = PrimaryKeyName;

            return query;
        }

        internal void AddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, string ConditionValue)
        {
            query.AddThisTableCondition(FieldName, OperatorType, ConditionValue);
        }

        internal void AddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, Guid ConditionValue)
        {
            query.AddThisTableCondition(FieldName, OperatorType, ConditionValue);
        }

        internal void AddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, int ConditionValue)
        {
            query.AddThisTableCondition(FieldName, OperatorType, ConditionValue);
        }

        internal void AddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, DateTime ConditionValue)
        {
            query.AddThisTableCondition(FieldName, OperatorType, ConditionValue);
        }

        internal void AddTableCondition(DataQuery query, string FieldName, ConditionOperatorType OperatorType, Boolean ConditionValue)
        {
            query.AddThisTableCondition(FieldName, OperatorType, ConditionValue);
        }

        internal void AddReturnField(DataQuery query, string TableName, string FieldName)
        {
            query.AddField(TableName, FieldName, FieldName);
        }

        internal void AddReturnFields(DataQuery query, string TableName, string[] FieldNames)
        {
            foreach (var fieldName in FieldNames)
            {
                query.AddField(TableName, fieldName, fieldName);
            }

        }

        internal Dictionary<string, object> ExecuteDataQueryAndExtractFirstResultFields(DataQuery query)
        {
            var response = DataProvider.ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Error);

            if (response.BusinessDataCollection == null || response.BusinessDataCollection.Count == 0)
                return new Dictionary<string, object>();

            Dictionary<string, object> fields = new Dictionary<string, object>();

            foreach (var field in response.BusinessDataCollection.FirstOrDefault().FieldCollection)
                fields.Add(field.Key, field.Value);

            return fields;

        }

        internal List<Guid> ExecuteDataQueryAndExtractGuidFields(DataQuery query, string FieldName)
        {
            var response = DataProvider.ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Error);

            if (response.BusinessDataCollection == null || response.BusinessDataCollection.Count == 0)
                return new List<Guid>();

            List<Guid> data = new List<Guid>();

            foreach (var businessData in response.BusinessDataCollection)
                if (businessData.FieldCollection[FieldName.ToLower()] != null)
                    data.Add((Guid)businessData.FieldCollection[FieldName.ToLower()]);

            return data;
        }

        #endregion


        #region Revoke record level access

        internal void RevokeRecordLevelAccess(Guid RecordID)
        {
            var ids = new List<Guid>() { RecordID };
            var response = SecurityDataProvider.RevokeRecordLevelAccess(ids);

            if (response.HasErrors)
                throw new Exception(response.Error);
        }

        #endregion


        #region Grant record level access

        internal void GrantRecordLevelAccess(CareDirector.Sdk.SystemEntities.RecordLevelAccess Access)
        {
            var response = SecurityDataProvider.GrantRecordLevelAccess(Access);

            if (response.HasErrors)
                throw new Exception(response.Error);
        }

        #endregion

        public CareDirector.Sdk.Interface.IUserSetting GetMetadataUserSettings()
        {
            return MetadataProvider.GetUserSetting();
        }

    }
}


