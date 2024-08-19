using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPPersonKeyworkerNote : BaseClass
    {

        public string TableName = "CPPersonKeyworkerNote";
        public string PrimaryKeyName = "CPPersonKeyworkerNoteid";

        public CPPersonKeyworkerNote()
        {
            AuthenticateUser();
        }

        public CPPersonKeyworkerNote(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        /// <summary>
        /// Use this method to creates a Person Keyworker Note record
        /// </summary>
        /// <returns></returns>
        public Guid CreateCPPersonKeyworkerNote(Guid personid, Guid ownerid, int totaltimespentwithclient, DateTime ocurred, string keyworkernote, bool isincludeinnexthandover = false, bool flagrecordforhandover = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "totaltimespentwithclient", totaltimespentwithclient);            
            AddFieldToBusinessDataObject(buisinessDataObject, "ocurred", ocurred);
            AddFieldToBusinessDataObject(buisinessDataObject, "keyworkernote", keyworkernote);
            AddFieldToBusinessDataObject(buisinessDataObject, "isincludeinnexthandover", isincludeinnexthandover);
            AddFieldToBusinessDataObject(buisinessDataObject, "flagrecordforhandover", flagrecordforhandover);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonId(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CPPersonKeyworkerNoteId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CPPersonKeyworkerNoteId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
