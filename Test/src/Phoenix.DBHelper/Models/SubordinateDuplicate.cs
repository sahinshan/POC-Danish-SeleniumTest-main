using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SubordinateDuplicate : BaseClass
    {

        public string TableName = "SubordinateDuplicate";
        public string PrimaryKeyName = "SubordinateDuplicateId";


        public SubordinateDuplicate()
        {
            AuthenticateUser();
        }

        public SubordinateDuplicate(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateProfessionalSubordinateDuplicate(Guid ownerid, string title, Guid duplicaterecordid, Guid recordid, string recordidtablename, string recordidname, string recordnumber, bool submitted)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "duplicaterecordid", duplicaterecordid);
            AddFieldToBusinessDataObject(buisinessDataObject, "recordid", recordid);
            AddFieldToBusinessDataObject(buisinessDataObject, "recordidtablename", recordidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "recordidname", recordidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "recordnumber", recordnumber);
            AddFieldToBusinessDataObject(buisinessDataObject, "submitted", submitted);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByDuplicateRecordID(Guid duplicaterecordid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "duplicaterecordid", ConditionOperatorType.Equal, duplicaterecordid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid SubordinateDuplicateId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SubordinateDuplicateId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteSubordinateDuplicate(Guid SubordinateDuplicateId)
        {
            this.DeleteRecord(TableName, SubordinateDuplicateId);
        }
    }
}
