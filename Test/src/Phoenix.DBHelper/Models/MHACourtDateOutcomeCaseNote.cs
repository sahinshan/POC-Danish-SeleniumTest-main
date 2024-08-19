using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MHACourtDateOutcomeCaseNote : BaseClass
    {
        public string TableName = "MHACourtDateOutcomeCaseNote";
        public string PrimaryKeyName = "MHACourtDateOutcomeCaseNoteId";


        public MHACourtDateOutcomeCaseNote()
        {
            AuthenticateUser();
        }

        public MHACourtDateOutcomeCaseNote(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateMHACourtDateOutcomeCaseNote(Guid ownerid, Guid? responsibleuserid,
            string subject, string notes, Guid MHACourtDateOutcomeId,
            Guid? activitycategoryid, Guid? activitysubcategoryid, Guid? activityoutcomeid, Guid? activityreasonid, Guid? activitypriorityid,
            bool issignificantevent, Guid? significanteventcategoryid, Guid? significanteventsubcategoryid,
            DateTime? significanteventdate, DateTime casenotedate, bool informationbythirdparty = false, int statusid = 1, bool iscloned = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "subject", subject);
            AddFieldToBusinessDataObject(dataObject, "notes", notes);
            AddFieldToBusinessDataObject(dataObject, "mhacourtdateoutcomeid", MHACourtDateOutcomeId);
            AddFieldToBusinessDataObject(dataObject, "activitycategoryid", activitycategoryid);
            AddFieldToBusinessDataObject(dataObject, "activitysubcategoryid", activitysubcategoryid);
            AddFieldToBusinessDataObject(dataObject, "activityoutcomeid", activityoutcomeid);
            AddFieldToBusinessDataObject(dataObject, "activityreasonid", activityreasonid);
            AddFieldToBusinessDataObject(dataObject, "activitypriorityid", activitypriorityid);
            AddFieldToBusinessDataObject(dataObject, "issignificantevent", issignificantevent);
            AddFieldToBusinessDataObject(dataObject, "significanteventcategoryid", significanteventcategoryid);
            AddFieldToBusinessDataObject(dataObject, "significanteventsubcategoryid", significanteventsubcategoryid);
            AddFieldToBusinessDataObject(dataObject, "significanteventdate", significanteventdate);
            AddFieldToBusinessDataObject(dataObject, "casenotedate", casenotedate);
            AddFieldToBusinessDataObject(dataObject, "informationbythirdparty", informationbythirdparty);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "iscloned", iscloned);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByMHACourtDateOutcomeId(Guid MHACourtDateOutcomeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "MHACourtDateOutcomeId", ConditionOperatorType.Equal, MHACourtDateOutcomeId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByMHACourtDateOutcomeCaseNoteId(Guid MHACourtDateOutcomeCaseNoteId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, MHACourtDateOutcomeCaseNoteId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetByID(Guid MHACourtDateOutcomeCaseNoteId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, MHACourtDateOutcomeCaseNoteId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteMHACourtDateOutcomeCaseNote(Guid MHACourtDateOutcomeCaseNoteId)
        {
            this.DeleteRecord(TableName, MHACourtDateOutcomeCaseNoteId);
        }
    }
}
