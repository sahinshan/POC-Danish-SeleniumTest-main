using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageOfferCommunicationOutcome : BaseClass
    {

        private string TableName = "BrokerageOfferCommunicationOutcome";
        private string PrimaryKeyName = "BrokerageOfferCommunicationOutcomeId";

        public BrokerageOfferCommunicationOutcome()
        {
            AuthenticateUser();
        }

        public BrokerageOfferCommunicationOutcome(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBrokerageOfferCommunicationOutcome(string Name, DateTime StartDate, Guid OwnerId, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateBrokerageOfferCommunicationOutcome(Guid BrokerageOfferCommunicationOutcomeId, string Name, DateTime StartDate, Guid OwnerId, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, BrokerageOfferCommunicationOutcomeId);

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

        public Dictionary<string, object> GetByBrokerageOfferCommunicationOutcomeId(Guid BrokerageOfferCommunicationOutcomeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageOfferCommunicationOutcomeId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageOfferCommunicationOutcomeRecord(Guid BrokerageOfferCommunicationOutcomeId)
        {
            this.DeleteRecord(TableName, BrokerageOfferCommunicationOutcomeId);
        }

    }
}
