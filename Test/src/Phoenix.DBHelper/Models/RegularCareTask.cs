using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RegularCareTask : BaseClass
    {

        public string TableName = "RegularCareTask";
        public string PrimaryKeyName = "RegularCareTaskId";


        public RegularCareTask()
        {
            AuthenticateUser();
        }

        public RegularCareTask(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateRegularCareTask(Guid ownerid, Guid owningbusinessunitid, bool inactive, Guid personid, Guid? caretaskid, Guid? personcareplaninterventionid, string name, Guid careplanid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "caretaskid", caretaskid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personcareplaninterventionid", personcareplaninterventionid);
            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "careplanid", careplanid);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonId(Guid personid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByCarePlanID(Guid careplanid, bool inactive)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careplanid", ConditionOperatorType.Equal, careplanid);
            this.BaseClassAddTableCondition(query, "inactive", ConditionOperatorType.Equal, inactive);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void DeleteRegularCareTask(Guid RegularCareTaskId)
        {
            this.DeleteRecord(TableName, RegularCareTaskId);
        }
    }
}
