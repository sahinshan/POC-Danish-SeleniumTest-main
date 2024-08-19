using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MHARightsAndRequestsCaseNote : BaseClass
    {
        public string TableName = "MHARightsAndRequestsCaseNote";
        public string PrimaryKeyName = "MHARightsAndRequestsCaseNoteId";


        public MHARightsAndRequestsCaseNote()
        {
            AuthenticateUser();
        }

        public MHARightsAndRequestsCaseNote(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateMHARightsAndRequestsCaseNote(Guid ownerid, Guid? responsibleuserid, Guid personid,
            string subject, string notes, Guid mharightsandrequestsid,
            Guid? activitycategoryid, Guid? activitysubcategoryid, Guid? activityoutcomeid, Guid? activityreasonid, Guid? activitypriorityid,
            bool issignificantevent, Guid? significanteventcategoryid, Guid? significanteventsubcategoryid,
            DateTime? significanteventdate, DateTime casenotedate, bool informationbythirdparty = false, int statusid = 1, bool iscloned = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "subject", subject);
            AddFieldToBusinessDataObject(dataObject, "notes", notes);
            AddFieldToBusinessDataObject(dataObject, "mharightsandrequestsid", mharightsandrequestsid);
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

        public List<Guid> GetByMHARightsAndRequestsId(Guid MHARightsAndRequestsId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "MHARightsAndRequestsId", ConditionOperatorType.Equal, MHARightsAndRequestsId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid MHARightsAndRequestsCaseNoteId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, MHARightsAndRequestsCaseNoteId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteMHARightsAndRequestsCaseNote(Guid MHARightsAndRequestsCaseNoteId)
        {
            this.DeleteRecord(TableName, MHARightsAndRequestsCaseNoteId);
        }
    }
}
