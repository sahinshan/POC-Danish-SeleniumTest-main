using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonConversations : BaseClass
    {
        public string TableName = "PersonConversations";
        public string PrimaryKeyName = "PersonConversationsId";


        public PersonConversations()
        {
            AuthenticateUser();
        }

        public PersonConversations(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonConversations(Guid ownerid,
            Guid personid, string preferences, DateTime occurred,
            string notes,
            Dictionary<Guid, string> RequiredCarePhysicalLocations, string locationifother,
            Guid carewellbeingid, string actiontaken,
            Guid? careassistanceneededid, int? careassistancelevelid,
            Dictionary<Guid, string> systemuserinfo, int timespentwithclient,
            string additionalnotes = "",
            bool isincludeinnexthandover = false, bool flagrecordforhandover = false,
            Guid? linkedadlcategoriesid = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "preferences", preferences);
            AddFieldToBusinessDataObject(dataObject, "occurred", occurred);

            AddFieldToBusinessDataObject(dataObject, "notes", notes);
            
            dataObject.MultiSelectBusinessObjectFields["carephysicallocations"] = new MultiSelectBusinessObjectDataCollection();

            if (RequiredCarePhysicalLocations != null && RequiredCarePhysicalLocations.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> locationId in RequiredCarePhysicalLocations)
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

            AddFieldToBusinessDataObject(dataObject, "careassistanceneededid", careassistanceneededid);
            if(careassistancelevelid.HasValue)
                AddFieldToBusinessDataObject(dataObject, "careassistancelevelid", careassistancelevelid);
            AddFieldToBusinessDataObject(dataObject, "carewellbeingid", carewellbeingid);
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

            AddFieldToBusinessDataObject(dataObject, "timespentwithclient", timespentwithclient);
            AddFieldToBusinessDataObject(dataObject, "additionalnotes", additionalnotes);

            AddFieldToBusinessDataObject(dataObject, "isincludeinnexthandover", isincludeinnexthandover);
            AddFieldToBusinessDataObject(dataObject, "flagrecordforhandover", flagrecordforhandover);

            AddFieldToBusinessDataObject(dataObject, "linkedadlcategoriesid", linkedadlcategoriesid);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByPersonId(Guid PersonId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, PersonId);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByPersonConversationsId(Guid PersonConversationsId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonConversationsId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetByID(Guid PersonConversationsId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonConversationsId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonConversations(Guid PersonConversationsId)
        {
            this.DeleteRecord(TableName, PersonConversationsId);
        }


    }
}
