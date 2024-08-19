using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageEpisode : BaseClass
    {

        public string TableName = "BrokerageEpisode";
        public string PrimaryKeyName = "BrokerageEpisodeId";


        public BrokerageEpisode()
        {
            AuthenticateUser();
        }

        public BrokerageEpisode(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBrokerageEpisode(Guid ownerid, Guid caseid, Guid personid, Guid brokeragerequestsourceid, Guid brokerageepisodepriorityid,
            DateTime requestreceiveddatetime, DateTime? targetdatetime,
            int statusid, int numberofoffersreceived, int numberofproviderscontacted, int contracttypeid,
            bool section117eligibleneeds, bool temporarycare, bool schedulerequired, bool deferredtocommissioning,
            bool brokerageresponsibleforcontact, bool contactregisteredincaredirector, bool offeraccepted)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "brokeragerequestsourceid", brokeragerequestsourceid);
            AddFieldToBusinessDataObject(dataObject, "brokerageepisodepriorityid", brokerageepisodepriorityid);
            AddFieldToBusinessDataObject(dataObject, "requestreceiveddatetime", requestreceiveddatetime);
            AddFieldToBusinessDataObject(dataObject, "targetdatetime", targetdatetime);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "numberofoffersreceived", numberofoffersreceived);
            AddFieldToBusinessDataObject(dataObject, "section117eligibleneeds", section117eligibleneeds);
            AddFieldToBusinessDataObject(dataObject, "temporarycare", temporarycare);
            AddFieldToBusinessDataObject(dataObject, "contracttypeid", contracttypeid);
            AddFieldToBusinessDataObject(dataObject, "schedulerequired", schedulerequired);
            AddFieldToBusinessDataObject(dataObject, "numberofproviderscontacted", numberofproviderscontacted);
            AddFieldToBusinessDataObject(dataObject, "deferredtocommissioning", deferredtocommissioning);
            AddFieldToBusinessDataObject(dataObject, "brokerageresponsibleforcontact", brokerageresponsibleforcontact);
            AddFieldToBusinessDataObject(dataObject, "contactregisteredincaredirector", contactregisteredincaredirector);
            AddFieldToBusinessDataObject(dataObject, "offeraccepted", offeraccepted);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);


            return this.CreateRecord(dataObject);
        }

        public Guid UpdateResponsibeUser(Guid BrokerageEpisodeId, Guid responsibleuserid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "BrokerageEpisodeId", BrokerageEpisodeId);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);

            return this.CreateRecord(dataObject);
        }

        public void UpdateBrokerageEpisode(Guid BrokerageEpisodeId, int statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageEpisodeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);

            this.UpdateRecord(buisinessDataObject);
        }



        public void UpdateBrokerageEpisodeForApprovedStatus(Guid BrokerageEpisodeId, int statusid, Guid serviceelement1id, Guid serviceelement2id, DateTime plannedstartdate, DateTime? plannedenddate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageEpisodeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdate", plannedstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedenddate", plannedenddate);


            this.UpdateRecord(buisinessDataObject);
        }


        public void UpdateBrokerageEpisodeStatusApproved(Guid BrokerageEpisodeId, int statusid, Guid serviceelement1id, Guid serviceelement2id, DateTime plannedstartdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageEpisodeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdate", plannedstartdate);

            this.UpdateRecord(buisinessDataObject);
        }


        public void UpdateTrackingStatus(Guid BrokerageEpisodeId, Guid? brokerageepisodetrackingstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageEpisodeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "brokerageepisodetrackingstatusid", brokerageepisodetrackingstatusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateBrokerageEpisodeForApprovedStatus(Guid BrokerageEpisodeId, int statusid, Guid serviceelement1id, Guid serviceelement2id)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageEpisodeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateBrokerageEpisodeForApprovedStatus(Guid BrokerageEpisodeId, int statusid, Guid serviceelement1id, Guid serviceelement2id, DateTime plannedstartdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageEpisodeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdate", plannedstartdate);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetBrokerageEpisodeByCaseID(Guid CaseId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseId);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid BrokerageEpisodeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageEpisodeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public List<Guid> GetBrokerageEpisodeByPersonID(Guid PersonId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetBrokerageEpisodeByID(Guid BrokerageEpisodeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageEpisodeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageEpisode(Guid BrokerageEpisodeId)
        {
            this.DeleteRecord(TableName, BrokerageEpisodeId);
        }

        public List<Guid> GetInActiveEpisodesByCaseID(Guid caseid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "caseid", ConditionOperatorType.Equal, caseid);
            this.BaseClassAddTableCondition(query, "Inactive", ConditionOperatorType.Equal, 1);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void SourceBrokerageEpisode(Guid BrokerageEpisodeId, int statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageEpisodeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);


            this.UpdateRecord(buisinessDataObject);
        }

        public Guid CreateBrokerageEpisodeWithResposibleUser(Guid ownerid, Guid caseid, Guid personid, Guid brokeragerequestsourceid, Guid brokerageepisodepriorityid,
          DateTime requestreceiveddatetime, DateTime? targetdatetime,
          int statusid, int numberofoffersreceived, int numberofproviderscontacted, int contracttypeid,
          bool section117eligibleneeds, bool temporarycare, bool schedulerequired, bool deferredtocommissioning,
          bool brokerageresponsibleforcontact, bool contactregisteredincaredirector, bool offeraccepted, Guid responsibleuserid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "brokeragerequestsourceid", brokeragerequestsourceid);
            AddFieldToBusinessDataObject(dataObject, "brokerageepisodepriorityid", brokerageepisodepriorityid);
            AddFieldToBusinessDataObject(dataObject, "requestreceiveddatetime", requestreceiveddatetime);
            AddFieldToBusinessDataObject(dataObject, "targetdatetime", targetdatetime);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "numberofoffersreceived", numberofoffersreceived);
            AddFieldToBusinessDataObject(dataObject, "section117eligibleneeds", section117eligibleneeds);
            AddFieldToBusinessDataObject(dataObject, "temporarycare", temporarycare);
            AddFieldToBusinessDataObject(dataObject, "contracttypeid", contracttypeid);
            AddFieldToBusinessDataObject(dataObject, "schedulerequired", schedulerequired);
            AddFieldToBusinessDataObject(dataObject, "numberofproviderscontacted", numberofproviderscontacted);
            AddFieldToBusinessDataObject(dataObject, "deferredtocommissioning", deferredtocommissioning);
            AddFieldToBusinessDataObject(dataObject, "brokerageresponsibleforcontact", brokerageresponsibleforcontact);
            AddFieldToBusinessDataObject(dataObject, "contactregisteredincaredirector", contactregisteredincaredirector);
            AddFieldToBusinessDataObject(dataObject, "offeraccepted", offeraccepted);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);

            return this.CreateRecord(dataObject);
        }

    }
}
