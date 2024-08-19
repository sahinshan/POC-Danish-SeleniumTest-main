using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageOffer : BaseClass
    {

        public string TableName = "BrokerageOffer";
        public string PrimaryKeyName = "BrokerageOfferId";


        public BrokerageOffer()
        {
            AuthenticateUser();
        }

        public BrokerageOffer(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBrokerageOffer(Guid ownerid, Guid brokerageepisodeid, Guid caseid, Guid personid, Guid providerid, DateTime receiveddatetime, int statusid, bool providerregisteredincaredirector)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "brokerageepisodeid", brokerageepisodeid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(dataObject, "receiveddatetime", receiveddatetime);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "providerregisteredincaredirector", providerregisteredincaredirector);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateBrokerageOffer(Guid ownerid, Guid brokerageepisodeid, Guid caseid, Guid personid, string ExternalProvider, DateTime receiveddatetime, int statusid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "brokerageepisodeid", brokerageepisodeid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "externalprovider", ExternalProvider);
            AddFieldToBusinessDataObject(dataObject, "receiveddatetime", receiveddatetime);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "providerregisteredincaredirector", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }



        public List<Guid> GetByBrokerageEpisodeId(Guid BrokerageEpisodeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "BrokerageEpisodeId", ConditionOperatorType.Equal, BrokerageEpisodeId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByBrokerageEpisodeIdAndStatus(Guid BrokerageEpisodeId, int StatusId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "BrokerageEpisodeId", ConditionOperatorType.Equal, BrokerageEpisodeId);
            this.BaseClassAddTableCondition(query, "statusid", ConditionOperatorType.Equal, StatusId);


            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid BrokerageOfferId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageOfferId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageOffer(Guid BrokerageOfferId)
        {
            this.DeleteRecord(TableName, BrokerageOfferId);
        }

        public List<Guid> GetBrokerageOfferByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);


            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateBrokerageOfferStatus(Guid BrokerageOfferId, int statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageOfferId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);


            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateBrokerageOfferStatus(Guid BrokerageOfferId, int statusid, Guid brokerageofferrejectionreasonid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageOfferId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "brokerageofferrejectionreasonid", brokerageofferrejectionreasonid);


            this.UpdateRecord(buisinessDataObject);
        }


        public void UpdateBrokerageOfferCancelledStatus(Guid BrokerageOfferId, bool inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageOfferId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);


            this.UpdateRecord(buisinessDataObject);
        }






        public void UpdateBrokerageOfferSourcedStatus(Guid BrokerageOfferId, int statusid, DateTime sourceddatetime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageOfferId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "sourceddatetime", sourceddatetime);

            this.UpdateRecord(buisinessDataObject);
        }

        public void RejectBrokerageOffer(Guid BrokerageOfferId, int statusid, Guid brokerageofferrejectionreasonid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageOfferId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "brokerageofferrejectionreasonid", brokerageofferrejectionreasonid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void CancelBrokerageOffer(Guid BrokerageOfferId, int statusid, Guid brokerageoffercancellationreasonid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageOfferId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "brokerageoffercancellationreasonid", brokerageoffercancellationreasonid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateBrokerageOfferToAcceptedStatus(Guid BrokerageOfferId, int statusid, Guid rateunitid, Guid serviceprovidedid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageOfferId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void BrokerageOfferActivateRecord(Guid BrokerageOfferId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);



            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageOfferId);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", 5);



            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateBrokerageOfferApprovedStatus(Guid BrokerageOfferId, int statusid, Guid providerid, Guid rateunitid, Guid serviceprovidedid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageOfferId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);

            this.UpdateRecord(buisinessDataObject);
        }


    }
}
