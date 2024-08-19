using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageOfferCommunication : BaseClass
    {

        public string TableName = "BrokerageOfferCommunication";
        public string PrimaryKeyName = "BrokerageOfferCommunicationId";


        public BrokerageOfferCommunication()
        {
            AuthenticateUser();
        }

        public BrokerageOfferCommunication(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByBrokerageOfferId(Guid BrokerageOfferId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "BrokerageOfferId", ConditionOperatorType.Equal, BrokerageOfferId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateBrokerageOfferCommunication(Guid ownerid, Guid brokerageofferid, Guid caseid, Guid personid, DateTime communicationdatetime, String subject, Guid brokeragecommunicationwithid, String communicationdetails,
                                                        Guid contactmethodid, Guid outcomeid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "brokerageofferid", brokerageofferid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "subject", subject);
            AddFieldToBusinessDataObject(dataObject, "communicationdatetime", communicationdatetime);
            AddFieldToBusinessDataObject(dataObject, "brokeragecommunicationwithid", brokeragecommunicationwithid);
            AddFieldToBusinessDataObject(dataObject, "communicationdetails", communicationdetails);
            AddFieldToBusinessDataObject(dataObject, "contactmethodid", contactmethodid);
            AddFieldToBusinessDataObject(dataObject, "outcomeid", outcomeid);



            return this.CreateRecord(dataObject);
        }

        public Dictionary<string, object> UpdateBrokerageOfferCommunication(Guid BrokerageOfferCommunicationId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageOfferCommunicationId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public Dictionary<string, object> GetByID(Guid BrokerageOfferCommunicationId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageOfferCommunicationId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageOfferCommunication(Guid BrokerageOfferCommunicationId)
        {
            this.DeleteRecord(TableName, BrokerageOfferCommunicationId);
        }
    }
}
