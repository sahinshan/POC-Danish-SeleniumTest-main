using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceProvisionStartReason : BaseClass
    {
        public string TableName { get { return "ServiceProvisionStartReason"; } }
        public string PrimaryKeyName { get { return "ServiceProvisionStartReasonid"; } }

        public ServiceProvisionStartReason()
        {
            AuthenticateUser();
        }

        public ServiceProvisionStartReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateServiceProvisionStartReason(Guid OwnerId, Guid OwningBusinessUnitId, string Name, DateTime StartDate, bool DefaultBrokerageStartReason = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwningBusinessUnitId", OwningBusinessUnitId);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "DefaultBrokerageStartReason", DefaultBrokerageStartReason);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateServiceProvisionStartReason(Guid PrimaryKeyId, Guid OwnerId, Guid OwningBusinessUnitId, string Name, DateTime StartDate, bool DefaultBrokerageStartReason = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, PrimaryKeyId);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwningBusinessUnitId", OwningBusinessUnitId);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "DefaultBrokerageStartReason", DefaultBrokerageStartReason);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetDefaultBrokerageStartReason()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "defaultbrokeragestartreason", ConditionOperatorType.Equal, true);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByRateUnitNameAndRateTypeId(string rateunitname, int ratetypeid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "rateunitname", ConditionOperatorType.Equal, rateunitname);
            this.BaseClassAddTableCondition(query, "ratetypeid", ConditionOperatorType.Equal, ratetypeid);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ServiceProvisionStartReasonid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceProvisionStartReasonid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public void UpdateServiceProvisionDefaultBrokerageStartReason(Guid ServiceProvisionStartReasonID, bool defaultbrokeragestartreason)
        {

            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ServiceProvisionStartReasonID);
            AddFieldToBusinessDataObject(buisinessDataObject, "defaultbrokeragestartreason", defaultbrokeragestartreason);

            this.UpdateRecord(buisinessDataObject);
        }


        public void DeleteServiceProvisionStartReason(Guid ServiceProvisionStartReasonID)
        {
            this.DeleteRecord(TableName, ServiceProvisionStartReasonID);
        }

        public List<Guid> GetAll()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }
    }
}
