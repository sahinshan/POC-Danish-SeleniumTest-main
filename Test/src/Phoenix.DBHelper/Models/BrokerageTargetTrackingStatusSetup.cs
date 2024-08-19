using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageTargetTrackingStatusSetup : BaseClass
    {

        private string TableName = "BrokerageTargetTrackingStatusSetup";
        private string PrimaryKeyName = "BrokerageTargetTrackingStatusSetupId";

        public BrokerageTargetTrackingStatusSetup()
        {
            AuthenticateUser();
        }

        public BrokerageTargetTrackingStatusSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBrokerageTargetTrackingStatusSetup(Guid BrokerageTargetSetupId, Guid BrokerageEpisodeTrackingStatusId, int Lapse, Guid OwnerId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "BrokerageTargetSetupId", BrokerageTargetSetupId);
            AddFieldToBusinessDataObject(dataObject, "BrokerageEpisodeTrackingStatusId", BrokerageEpisodeTrackingStatusId);
            AddFieldToBusinessDataObject(dataObject, "Lapse", Lapse);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateBrokerageTargetTrackingStatusSetup(Guid BrokerageTargetTrackingStatusSetupId, Guid BrokerageTargetSetupId, Guid BrokerageEpisodeTrackingStatusId, int Lapse, Guid OwnerId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, BrokerageTargetTrackingStatusSetupId);

            AddFieldToBusinessDataObject(dataObject, "BrokerageTargetSetupId", BrokerageTargetSetupId);
            AddFieldToBusinessDataObject(dataObject, "BrokerageEpisodeTrackingStatusId", BrokerageEpisodeTrackingStatusId);
            AddFieldToBusinessDataObject(dataObject, "Lapse", Lapse);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetById(Guid BrokerageTargetTrackingStatusSetupId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageTargetTrackingStatusSetupId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByBrokerageTargetTrackingStatusSetupId(Guid BrokerageTargetTrackingStatusSetupId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageTargetTrackingStatusSetupId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetByBrokerageTargetSetupId(Guid BrokerageTargetSetupId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "BrokerageTargetSetupId", ConditionOperatorType.Equal, BrokerageTargetSetupId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageEpisodeTargetSetupRecord(Guid BrokerageEpisodeTargetSetupId)
        {
            this.DeleteRecord(TableName, BrokerageEpisodeTargetSetupId);
        }

    }
}
