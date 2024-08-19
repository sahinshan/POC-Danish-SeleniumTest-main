using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageEpisodePauseReason : BaseClass
    {

        private string TableName = "BrokerageEpisodePauseReason";
        private string PrimaryKeyName = "BrokerageEpisodePauseReasonId";

        public BrokerageEpisodePauseReason()
        {
            AuthenticateUser();
        }

        public BrokerageEpisodePauseReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBrokerageEpisodePauseReason(string Name, DateTime StartDate, Guid OwnerId, bool Inactive = false, bool IsOther = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);
            AddFieldToBusinessDataObject(dataObject, "IsOther", IsOther);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateBrokerageEpisodePauseReason(Guid BrokerageEpisodePauseReasonId, string Name, DateTime StartDate, Guid OwnerId, bool Inactive = false, bool IsOther = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, BrokerageEpisodePauseReasonId);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);
            AddFieldToBusinessDataObject(dataObject, "IsOther", IsOther);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByBrokerageEpisodePauseReasonId(Guid BrokerageEpisodePauseReasonId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageEpisodePauseReasonId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageEpisodePauseReasonRecord(Guid BrokerageEpisodePauseReasonId)
        {
            this.DeleteRecord(TableName, BrokerageEpisodePauseReasonId);
        }

    }
}
