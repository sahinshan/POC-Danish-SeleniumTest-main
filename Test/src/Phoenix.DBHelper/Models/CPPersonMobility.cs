using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPPersonMobility : BaseClass
    {

        private string tableName = "cppersonmobility";
        private string primaryKeyName = "cppersonmobilityid";

        public CPPersonMobility()
        {
            AuthenticateUser();
        }

        public CPPersonMobility(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonId(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetById(Guid CPPersonMobilityId, params string[] fields)
        {
            System.Threading.Thread.Sleep(1000);
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.AddReturnFields(query, tableName, fields);
            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPPersonMobilityId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public Guid CreatePersonMobility(Guid personid, Guid ownerid, Guid owningbusinessunitid, DateTime occurred, int careconsentgivenid, int carenonconsentid, string reasonforabsence)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "occurred", occurred);
            AddFieldToBusinessDataObject(dataObject, "careconsentgivenid", careconsentgivenid);
            AddFieldToBusinessDataObject(dataObject, "carenonconsentid", carenonconsentid);
            AddFieldToBusinessDataObject(dataObject, "reasonforabsence", reasonforabsence);




            return this.CreateRecord(dataObject);
        }

        public Guid CreatePersonMobility(Guid personid, Guid ownerid, Guid owningbusinessunitid, DateTime occurred, int careconsentgivenid, Guid mobilisedfromlocationid, Guid mobilisedtolocationid, int approximatedistance, Guid approximatedistanceunitid, Dictionary<Guid, string> Requiredcareequipmentid, Guid careassistanceneededid, Guid carewellbeingid, Dictionary<Guid, string> systemuserinfo, int timespentwithclient, string carenote)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "occurred", occurred);
            AddFieldToBusinessDataObject(dataObject, "careconsentgivenid", careconsentgivenid);
            AddFieldToBusinessDataObject(dataObject, "mobilisedfromlocationid", mobilisedfromlocationid);
            AddFieldToBusinessDataObject(dataObject, "mobilisedtolocationid", mobilisedtolocationid);
            AddFieldToBusinessDataObject(dataObject, "approximatedistance", approximatedistance);
            AddFieldToBusinessDataObject(dataObject, "approximatedistanceunitid", approximatedistanceunitid);

            dataObject.MultiSelectBusinessObjectFields["careequipmentid"] = new MultiSelectBusinessObjectDataCollection();

            if (Requiredcareequipmentid != null && Requiredcareequipmentid.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> careequipmentid in Requiredcareequipmentid)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = careequipmentid.Key,
                        ReferenceIdTableName = "careequipment",
                        ReferenceName = careequipmentid.Value,
                    };
                    dataObject.MultiSelectBusinessObjectFields["careequipmentid"].Add(dataRecord);
                }
            }
            AddFieldToBusinessDataObject(dataObject, "careassistanceneededid", careassistanceneededid);
            AddFieldToBusinessDataObject(dataObject, "carewellbeingid", carewellbeingid);

            dataObject.MultiSelectBusinessObjectFields["systemuserid"] = new MultiSelectBusinessObjectDataCollection();

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
                    dataObject.MultiSelectBusinessObjectFields["systemuserid"].Add(dataRecord);
                }
            }
            AddFieldToBusinessDataObject(dataObject, "timespentwithclient", timespentwithclient);
            AddFieldToBusinessDataObject(dataObject, "carenote", carenote);

            AddFieldToBusinessDataObject(dataObject, "isincludeinnexthandover", false);
            AddFieldToBusinessDataObject(dataObject, "flagrecordforhandover", false);


            return this.CreateRecord(dataObject);
        }


        public Guid CreatePersonMobility(Guid personid, Guid ownerid, Guid owningbusinessunitid, DateTime occurred, int careconsentgivenid, Guid mobilisedfromlocationid, Guid mobilisedtolocationid, int approximatedistance, Guid approximatedistanceunitid, Dictionary<Guid, string> Requiredcareequipmentid, Guid careassistanceneededid, Guid carewellbeingid, Dictionary<Guid, string> systemuserinfo, int timespentwithclient, string carenote,Boolean flagrecordforhandover)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "occurred", occurred);
            AddFieldToBusinessDataObject(dataObject, "careconsentgivenid", careconsentgivenid);
            AddFieldToBusinessDataObject(dataObject, "mobilisedfromlocationid", mobilisedfromlocationid);
            AddFieldToBusinessDataObject(dataObject, "mobilisedtolocationid", mobilisedtolocationid);
            AddFieldToBusinessDataObject(dataObject, "approximatedistance", approximatedistance);
            AddFieldToBusinessDataObject(dataObject, "approximatedistanceunitid", approximatedistanceunitid);

            dataObject.MultiSelectBusinessObjectFields["careequipmentid"] = new MultiSelectBusinessObjectDataCollection();

            if (Requiredcareequipmentid != null && Requiredcareequipmentid.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> careequipmentid in Requiredcareequipmentid)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = careequipmentid.Key,
                        ReferenceIdTableName = "careequipment",
                        ReferenceName = careequipmentid.Value,
                    };
                    dataObject.MultiSelectBusinessObjectFields["careequipmentid"].Add(dataRecord);
                }
            }
            AddFieldToBusinessDataObject(dataObject, "careassistanceneededid", careassistanceneededid);
            AddFieldToBusinessDataObject(dataObject, "carewellbeingid", carewellbeingid);

            dataObject.MultiSelectBusinessObjectFields["systemuserid"] = new MultiSelectBusinessObjectDataCollection();

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
                    dataObject.MultiSelectBusinessObjectFields["systemuserid"].Add(dataRecord);
                }
            }
            AddFieldToBusinessDataObject(dataObject, "timespentwithclient", timespentwithclient);
            AddFieldToBusinessDataObject(dataObject, "carenote", carenote);

            AddFieldToBusinessDataObject(dataObject, "isincludeinnexthandover", false);
            AddFieldToBusinessDataObject(dataObject, "flagrecordforhandover", flagrecordforhandover);


            return this.CreateRecord(dataObject);
        }

        //When Care Consent = No, Non-consent Detail = Deferred
        public Guid CreatePersonMobility(Guid personid, Guid ownerid, Guid owningbusinessunitid, DateTime occurred, int careconsentgivenid, int carenonconsentid, DateTime deferredtodate, int timeorshiftid, TimeSpan? deferredtotime, Guid? deferredtoshiftid)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "occurred", occurred);            
            AddFieldToBusinessDataObject(dataObject, "careconsentgivenid", careconsentgivenid);
            AddFieldToBusinessDataObject(dataObject, "carenonconsentid", carenonconsentid);
            AddFieldToBusinessDataObject(dataObject, "deferredtodate", deferredtodate);
            AddFieldToBusinessDataObject(dataObject, "timeorshiftid", timeorshiftid);
            AddFieldToBusinessDataObject(dataObject, "deferredtotime", deferredtotime);
            AddFieldToBusinessDataObject(dataObject, "deferredtoshiftid", deferredtoshiftid);

            return this.CreateRecord(dataObject);
        }

    }
}
