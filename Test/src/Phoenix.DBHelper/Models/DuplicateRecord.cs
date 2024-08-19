using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DuplicateRecord : BaseClass
    {

        public string TableName = "DuplicateRecord";
        public string PrimaryKeyName = "DuplicateRecordId";


        public DuplicateRecord()
        {
            AuthenticateUser();
        }

        public DuplicateRecord(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDuplicateRecord(Guid ownerid, string title, Guid duplicatedetectionruleid, int numberofduplicates, Guid recordid, string recordidtablename, string recordidname)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "duplicatedetectionruleid", duplicatedetectionruleid);
            AddFieldToBusinessDataObject(buisinessDataObject, "numberofduplicates", numberofduplicates);
            AddFieldToBusinessDataObject(buisinessDataObject, "recordid", recordid);
            AddFieldToBusinessDataObject(buisinessDataObject, "recordidtablename", recordidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "recordidname", recordidname);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByRecordID(Guid recordid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "recordid", ConditionOperatorType.Equal, recordid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid DuplicateRecordId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, DuplicateRecordId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteDuplicateRecord(Guid DuplicateRecordId)
        {
            this.DeleteRecord(TableName, DuplicateRecordId);
        }
    }
}
