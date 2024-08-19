using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using CareWorks.Foundation.SystemEntities;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AdultSafeguardingStatus : BaseClass
    {

        private string TableName = "AdultSafeguardingStatus";
        private string PrimaryKeyName = "AdultSafeguardingStatusId";

        public AdultSafeguardingStatus()
        {
            AuthenticateUser();
        }

        public AdultSafeguardingStatus(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAdultSafeguardingStatus(Guid OwnerId, string Name, DateTime StartDate, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByAdultSafeguardingStatusId(Guid AdultSafeguardingStatusId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AdultSafeguardingStatusId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteAdultSafeguardingStatusRecord(Guid AdultSafeguardingStatusId)
        {
            this.DeleteRecord(TableName, AdultSafeguardingStatusId);
        }

    }
}
