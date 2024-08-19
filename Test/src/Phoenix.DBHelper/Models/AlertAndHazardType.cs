using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AlertAndHazardType : BaseClass
    {

        private string TableName = "AlertAndHazardType";
        private string PrimaryKeyName = "AlertAndHazardTypeId";

        public AlertAndHazardType()
        {
            AuthenticateUser();
        }

        public AlertAndHazardType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAlertAndHazardType(Guid OwnerId, string Name, DateTime StartDate, bool validforswallowassessment = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            AddFieldToBusinessDataObject(dataObject, "validforswallowassessment", validforswallowassessment);
            AddFieldToBusinessDataObject(dataObject, "RequiresReview", false);
            AddFieldToBusinessDataObject(dataObject, "ShowOnSummary", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByAlertAndHazardTypeId(Guid AlertAndHazardTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AlertAndHazardTypeId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteAlertAndHazardTypeRecord(Guid AlertAndHazardTypeId)
        {
            this.DeleteRecord(TableName, AlertAndHazardTypeId);
        }

    }
}
