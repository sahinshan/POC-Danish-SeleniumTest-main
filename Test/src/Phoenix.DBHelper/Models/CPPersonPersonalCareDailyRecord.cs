using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Security.Cryptography.Xml;

namespace Phoenix.DBHelper.Models
{
    public class CPPersonPersonalCareDailyRecord : BaseClass
    {

        public string TableName = "cppersonpersonalcaredailyrecord";
        public string PrimaryKeyName = "cppersonpersonalcaredailyrecordid";

        public CPPersonPersonalCareDailyRecord()
        {
            AuthenticateUser();
        }

        public CPPersonPersonalCareDailyRecord(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        /// <summary>
        /// Use this method to creates a Person Daily Personal Care record when the "Consent Given?" field is set to "No" and "Non-consent Detail" is set to "Reason for Absence?"
        /// </summary>
        /// <returns></returns>
        public Guid CreateCPPersonPersonalCareDailyRecord(Guid personid, Guid ownerid, string preferences, DateTime occurred, int careconsentgivenid, int carenonconsentid, string reasonforabsence)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "preferences", preferences);
            AddFieldToBusinessDataObject(buisinessDataObject, "occurred", occurred);
            AddFieldToBusinessDataObject(buisinessDataObject, "careconsentgivenid", careconsentgivenid);
            AddFieldToBusinessDataObject(buisinessDataObject, "carenonconsentid", carenonconsentid);
            AddFieldToBusinessDataObject(buisinessDataObject, "reasonforabsence", reasonforabsence);

            return CreateRecord(buisinessDataObject);
        }

        /// <summary>
        /// Use this method to creates a Person Daily Personal Care record when the "Consent Given?" field is set to "Yes" 
        /// </summary>
        /// <returns></returns>
        public Guid CreateCPPersonPersonalCareDailyRecord(Guid personid, Guid ownerid, string preferences, DateTime occurred, int careconsentgivenid, 
            Dictionary<Guid, string> Locations, Dictionary<Guid, string> Equipment, Guid carewellbeingid, Guid careassistanceneededid, string actiontaken, Dictionary<Guid, string> StaffRequired, int timespentwithclient,
            string carenote)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "preferences", preferences);
            AddFieldToBusinessDataObject(buisinessDataObject, "occurred", occurred);
            AddFieldToBusinessDataObject(buisinessDataObject, "careconsentgivenid", careconsentgivenid);

            AddFieldToBusinessDataObject(buisinessDataObject, "skinconcernisany", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "reviewrequiredbyseniorcolleague", false);

            AddMultiSelectBusinessObjectData(buisinessDataObject, "locationid", Locations, "carephysicallocation");
            AddMultiSelectBusinessObjectData(buisinessDataObject, "equipmentid", Equipment, "careequipment");
            AddFieldToBusinessDataObject(buisinessDataObject, "carewellbeingid", carewellbeingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careassistanceneededid", careassistanceneededid);
            AddFieldToBusinessDataObject(buisinessDataObject, "actiontaken", actiontaken);
            AddMultiSelectBusinessObjectData(buisinessDataObject, "staffrequired", StaffRequired, "systemuser");
            AddFieldToBusinessDataObject(buisinessDataObject, "timespentwithclient", timespentwithclient);

            AddFieldToBusinessDataObject(buisinessDataObject, "carenote", carenote);

            AddFieldToBusinessDataObject(buisinessDataObject, "isincludeinnexthandover", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "flagrecordforhandover", false);

            return CreateRecord(buisinessDataObject);
        }

        

        public List<Guid> GetByPersonId(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CPPersonPersonalCareDailyRecordId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CPPersonPersonalCareDailyRecordId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
