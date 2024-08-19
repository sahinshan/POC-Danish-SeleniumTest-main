using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceProvisionEndReason : BaseClass
    {
        public string TableName { get { return "ServiceProvisionEndReason"; } }
        public string PrimaryKeyName { get { return "ServiceProvisionEndReasonid"; } }

        public ServiceProvisionEndReason()
        {
            AuthenticateUser();
        }

        public ServiceProvisionEndReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateServiceProvisionEndReason(Guid OwnerId, string Name, DateTime StartDate, bool defaultbrokerageendreason = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "defaultbrokerageendreason", defaultbrokerageendreason);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetServiceProvisionEndReasonByNumber(string ServiceProvisionEndReasonNumber)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "ServiceProvisionEndReasonNumber", ConditionOperatorType.Equal, ServiceProvisionEndReasonNumber);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetDefaultBrokerageEndReason()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "defaultbrokerageendreason", ConditionOperatorType.Equal, true);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ServiceProvisionEndReasonid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceProvisionEndReasonid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetServiceProvisionEndReasonByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateServiceProvisionDefaultBrokerageEndReason(Guid ServiceProvisionEndReasonID, bool defaultbrokerageendreason)
        {

            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ServiceProvisionEndReasonID);
            AddFieldToBusinessDataObject(buisinessDataObject, "defaultbrokerageendreason", defaultbrokerageendreason);

            this.UpdateRecord(buisinessDataObject);
        }


        public void DeleteServiceProvisionEndReason(Guid ServiceProvisionEndReasonID)
        {
            this.DeleteRecord(TableName, ServiceProvisionEndReasonID);
        }

        public List<Guid> GetAll()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

    }
}
