using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageEpisodeRejectionReason : BaseClass
    {

        private string TableName = "BrokerageEpisodeRejectionReason";
        private string PrimaryKeyName = "BrokerageEpisodeRejectionReasonId";

        public BrokerageEpisodeRejectionReason()
        {
            AuthenticateUser();
        }

        public BrokerageEpisodeRejectionReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBrokerageEpisodeRejectionReason(string Name, DateTime StartDate, Guid OwnerId, bool IsOther = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "IsOther", IsOther);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateBrokerageEpisodeRejectionReason(Guid BrokerageEpisodeRejectionReasonId, string Name, DateTime StartDate, Guid OwnerId, bool IsOther = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, BrokerageEpisodeRejectionReasonId);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "IsOther", IsOther);
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

        public Dictionary<string, object> GetByBrokerageEpisodeRejectionReasonId(Guid BrokerageEpisodeRejectionReasonId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageEpisodeRejectionReasonId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageEpisodeRejectionReasonId(Guid BrokerageEpisodeRejectionReasonId)
        {
            this.DeleteRecord(TableName, BrokerageEpisodeRejectionReasonId);
        }

    }
}
