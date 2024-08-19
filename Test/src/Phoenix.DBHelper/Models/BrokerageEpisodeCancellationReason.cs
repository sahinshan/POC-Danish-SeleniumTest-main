using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageEpisodeCancellationReason : BaseClass
    {

        private string TableName = "BrokerageEpisodeCancellationReason";
        private string PrimaryKeyName = "BrokerageEpisodeCancellationReasonId";

        public BrokerageEpisodeCancellationReason()
        {
            AuthenticateUser();
        }

        public BrokerageEpisodeCancellationReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBrokerageEpisodeCancellationReason(Guid BrokerageEpisodeCancellationReasonId, string Name, DateTime StartDate, DateTime? EndDate, Guid OwnerId, bool IsOther = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, BrokerageEpisodeCancellationReasonId);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "EndDate", EndDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "IsOther", IsOther);

            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            return this.CreateRecord(dataObject);
        }


        public Guid CreateBrokerageEpisodeCancellationReason(Guid BrokerageEpisodePriorityId, string Name, DateTime StartDate, Guid OwnerId, bool ValidForExport = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, BrokerageEpisodePriorityId);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", ValidForExport);

            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            return this.CreateRecord(dataObject);
        }



        public List<Guid> GetByBrokerageEpisodeCancellationReasonName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByBrokerageEpisodeCancellationReasonId(Guid BrokerageEpisodeCancellationReasonId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageEpisodeCancellationReasonId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageEpisodeCancellationReasonRecord(Guid BrokerageEpisodeCancellationReasonId)
        {
            this.DeleteRecord(TableName, BrokerageEpisodeCancellationReasonId);
        }

    }
}