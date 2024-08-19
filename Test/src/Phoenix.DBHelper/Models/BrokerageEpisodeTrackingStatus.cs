using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageEpisodeTrackingStatus : BaseClass
    {

        private string TableName = "BrokerageEpisodeTrackingStatus";
        private string PrimaryKeyName = "BrokerageEpisodeTrackingStatusId";

        public BrokerageEpisodeTrackingStatus()
        {
            AuthenticateUser();
        }

        public BrokerageEpisodeTrackingStatus(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBrokerageEpisodeTrackingStatus(string Name, DateTime StartDate, Guid OwnerId, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateBrokerageEpisodeTrackingStatus(Guid BrokerageEpisodeTrackingStatusId, string Name, DateTime StartDate, Guid OwnerId, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, BrokerageEpisodeTrackingStatusId);

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

        public Dictionary<string, object> GetByBrokerageEpisodeTrackingStatusId(Guid BrokerageEpisodeTrackingStatusId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageEpisodeTrackingStatusId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageEpisodeTrackingStatusRecord(Guid BrokerageEpisodeTrackingStatusId)
        {
            this.DeleteRecord(TableName, BrokerageEpisodeTrackingStatusId);
        }

    }
}
