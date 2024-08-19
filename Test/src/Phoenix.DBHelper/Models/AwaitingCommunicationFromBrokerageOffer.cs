using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AwaitingCommunicationFromBrokerageOffer : BaseClass
    {

        public string TableName = "AwaitingCommunicationFromBrokerageOffer";
        public string PrimaryKeyName = "AwaitingCommunicationFromBrokerageOfferId";


        public AwaitingCommunicationFromBrokerageOffer()
        {
            AuthenticateUser();
        }

        public AwaitingCommunicationFromBrokerageOffer(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetByBrokerageEpisodeId(Guid BrokerageEpisodeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "BrokerageEpisodeId", ConditionOperatorType.Equal, BrokerageEpisodeId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid AwaitingCommunicationFromBrokerageOfferId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AwaitingCommunicationFromBrokerageOfferId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteAwaitingCommunicationFromBrokerageOffer(Guid AwaitingCommunicationFromBrokerageOfferId)
        {
            this.DeleteRecord(TableName, AwaitingCommunicationFromBrokerageOfferId);
        }
    }
}
