using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ProviderComplaintFeedBackType : BaseClass
    {

        private string tableName = "ProviderComplaintFeedBackType";
        private string primaryKeyName = "ProviderComplaintFeedBackTypeId";

        public ProviderComplaintFeedBackType()
        {
            AuthenticateUser();
        }

        public ProviderComplaintFeedBackType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateProviderComplaintFeedBackType(Guid OwnerId, string Name, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetProviderComplaintFeedBackTypeByName(string Name)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Contains, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetProviderComplaintFeedBackTypeByID(Guid ProviderComplaintFeedBackTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, ProviderComplaintFeedBackTypeId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteProviderComplaintFeedBackType(Guid ProviderComplaintFeedBackTypeID)
        {
            this.DeleteRecord(tableName, ProviderComplaintFeedBackTypeID);
        }




    }
}
