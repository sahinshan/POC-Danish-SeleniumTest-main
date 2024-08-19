using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CpPersonBehaviourIncident : BaseClass
    {
        public string TableName = "CpPersonBehaviourIncident";
        public string PrimaryKeyName = "CpPersonBehaviourIncidentId";


        public CpPersonBehaviourIncident()
        {
            AuthenticateUser();
        }

        public CpPersonBehaviourIncident(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCpPersonBehaviourIncident(Guid ownerid,
            Guid personid, string preferences, DateTime occurred,
            int consentgivenid,
            string antecedent, bool werethereanytriggers, Dictionary<Guid, string> whatwerethetriggersList, string triggersifother, 
            string consequence,
            Dictionary<Guid, string> RequiredlocationId, string locationifother,
            Guid wellbeingid, string actiontaken,
            Guid? assistanceneededid, int? assistanceamountid,
            Dictionary<Guid, string> systemuserinfo, int totaltimespentwithclientminutes,
            string additionalnotes = "",
            bool isincludeinnexthandover = false, bool flagrecordforhandover = false,
            Guid? linkedadlcategoriesid = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "preferences", preferences);
            AddFieldToBusinessDataObject(dataObject, "occurred", occurred);
            AddFieldToBusinessDataObject(dataObject, "consentgivenid", consentgivenid);

            AddFieldToBusinessDataObject(dataObject, "antecedent", antecedent);
            AddFieldToBusinessDataObject(dataObject, "werethereanytriggers", werethereanytriggers);

            dataObject.MultiSelectBusinessObjectFields["whatwerethetriggers"] = new MultiSelectBusinessObjectDataCollection();

            if (whatwerethetriggersList != null && whatwerethetriggersList.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> whatwerethetriggers in whatwerethetriggersList)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = whatwerethetriggers.Key,
                        ReferenceIdTableName = "cppersonbehaviourincidenttrigger",
                        ReferenceName = whatwerethetriggers.Value,
                    };
                    dataObject.MultiSelectBusinessObjectFields["whatwerethetriggers"].Add(dataRecord);
                }
            }
            
            AddFieldToBusinessDataObject(dataObject, "triggersifother", triggersifother);
            AddFieldToBusinessDataObject(dataObject, "consequence", consequence);

            
            dataObject.MultiSelectBusinessObjectFields["location"] = new MultiSelectBusinessObjectDataCollection();

            if (RequiredlocationId != null && RequiredlocationId.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> locationId in RequiredlocationId)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = locationId.Key,
                        ReferenceIdTableName = "carephysicallocation",
                        ReferenceName = locationId.Value,
                    };
                    dataObject.MultiSelectBusinessObjectFields["carephysicallocationid"].Add(dataRecord);
                }
            }
            
            AddFieldToBusinessDataObject(dataObject, "locationifother", locationifother);

            AddFieldToBusinessDataObject(dataObject, "assistanceneededid", assistanceneededid);
            if(assistanceamountid.HasValue)
                AddFieldToBusinessDataObject(dataObject, "assistanceamountid", assistanceamountid);
            AddFieldToBusinessDataObject(dataObject, "wellbeingid", wellbeingid);
            AddFieldToBusinessDataObject(dataObject, "actiontaken", actiontaken);

            dataObject.MultiSelectBusinessObjectFields["staffrequired"] = new MultiSelectBusinessObjectDataCollection();

            if (systemuserinfo != null && systemuserinfo.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> systemuserid in systemuserinfo)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = systemuserid.Key,
                        ReferenceIdTableName = "systemuser",
                        ReferenceName = systemuserid.Value,
                    };
                    dataObject.MultiSelectBusinessObjectFields["staffrequired"].Add(dataRecord);
                }
            }

            AddFieldToBusinessDataObject(dataObject, "totaltimespentwithclientminutes", totaltimespentwithclientminutes);
            AddFieldToBusinessDataObject(dataObject, "additionalnotes", additionalnotes);

            AddFieldToBusinessDataObject(dataObject, "isincludeinnexthandover", isincludeinnexthandover);
            AddFieldToBusinessDataObject(dataObject, "flagrecordforhandover", flagrecordforhandover);

            AddFieldToBusinessDataObject(dataObject, "linkedadlcategoriesid", linkedadlcategoriesid);

            return this.CreateRecord(dataObject);
        }

        //When Care Consent = No, Non-consent Detail = Absent
        public Guid CreateCpPersonBehaviourIncident(Guid personid, Guid ownerid, Guid owningbusinessunitid, DateTime occurred, int careconsentgivenid, int carenonconsentid, string reasonforabsence)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "occurred", occurred);
            AddFieldToBusinessDataObject(dataObject, "consentgivenid", careconsentgivenid);
            AddFieldToBusinessDataObject(dataObject, "nonconsentdetailid", carenonconsentid);
            AddFieldToBusinessDataObject(dataObject, "reasonforabsence", reasonforabsence);
            return this.CreateRecord(dataObject);
        }

        //When Care Consent = No, Non-consent Detail = Deferred
        public Guid CreateCpPersonBehaviourIncident(Guid personid, Guid ownerid, Guid owningbusinessunitid, DateTime occurred, int careconsentgivenid, int carenonconsentid, DateTime deferredtodate, int timeorshiftid, TimeSpan? deferredtotime, Guid? deferredtoshiftid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "occurred", occurred);
            AddFieldToBusinessDataObject(dataObject, "consentgivenid", careconsentgivenid);
            AddFieldToBusinessDataObject(dataObject, "nonconsentdetailid", carenonconsentid);
            AddFieldToBusinessDataObject(dataObject, "deferredtodate", deferredtodate);
            AddFieldToBusinessDataObject(dataObject, "timeorshiftid", timeorshiftid);
            AddFieldToBusinessDataObject(dataObject, "deferredtotime", deferredtotime);
            AddFieldToBusinessDataObject(dataObject, "deferredtoshiftid", deferredtoshiftid);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByPersonId(Guid PersonId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByCpPersonBehaviourIncidentId(Guid CpPersonBehaviourIncidentId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CpPersonBehaviourIncidentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetByID(Guid CpPersonBehaviourIncidentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CpPersonBehaviourIncidentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCpPersonBehaviourIncident(Guid CpPersonBehaviourIncidentId)
        {
            this.DeleteRecord(TableName, CpPersonBehaviourIncidentId);
        }


    }
}
