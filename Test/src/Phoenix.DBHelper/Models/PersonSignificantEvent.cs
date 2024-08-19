using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class PersonSignificantEvent : BaseClass
    {

        public string TableName = "PersonSignificantEvent";
        public string PrimaryKeyName = "PersonSignificantEventId";


        public PersonSignificantEvent()
        {
            AuthenticateUser();
        }

        public PersonSignificantEvent(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetPersonSignificantEventByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonSignificantEventByID(Guid PersonSignificantEventId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonSignificantEventId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid? GetDataRestrictionForPersonSignificantEvent(Guid PersonSignificantEventID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.PersonSignificantEvents.Where(c => c.PersonSignificantEventId == PersonSignificantEventID).Select(x => x.DataRestrictionId).FirstOrDefault();

            }
        }

        public void RemovePersonSignificantEventRestrictionFromDB(Guid PersonSignificantEventID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                var _record = entity.PersonSignificantEvents.Where(c => c.PersonSignificantEventId == PersonSignificantEventID).FirstOrDefault();
                _record.DataRestrictionId = null;
                entity.SaveChanges();
            }
        }

        public Phoenix.DBHelper.PersonSignificantEvent GetPersonSignificantEventsForTask(Guid TaskID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.PersonSignificantEvents.Where(c => c.SourceActivityId == TaskID && c.SourceActivityIdTableName == "task").FirstOrDefault();
            }
        }

        public Guid CreatePersonSignificantEvent(Guid personID, Guid significanteventcategoryid, DateTime eventdate, Guid ownerid, Guid significanteventsubcategoryid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "personID", personID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "significanteventcategoryid", significanteventcategoryid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "significanteventsubcategoryid", significanteventsubcategoryid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "eventdate", eventdate);

            return this.CreateRecord(buisinessDataObject);

        }

        public void DeletePersonSignificantEvent(Guid PersonSignificantEventId)
        {
            this.DeleteRecord(TableName, PersonSignificantEventId);
        }

        public List<Guid> GetByPersonIdAndSourceActivityIdTableName(Guid PersonID, string SourceActivityIdTableName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);
            this.BaseClassAddTableCondition(query, "SourceActivityIdTableName", ConditionOperatorType.Equal, SourceActivityIdTableName);


            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
    }
}
