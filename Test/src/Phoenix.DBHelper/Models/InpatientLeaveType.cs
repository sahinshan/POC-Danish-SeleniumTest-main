using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientLeaveType : BaseClass
    {
        public string TableName { get { return "InpatientLeaveType"; } }
        public string PrimaryKeyName { get { return "InpatientLeaveTypeid"; } }


        public InpatientLeaveType()
        {
            AuthenticateUser();
        }

        public InpatientLeaveType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonId(Guid Personid)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, Personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid InpatientLeaveTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, InpatientLeaveTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetInpatientLeaveTypeByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateInpatientLeaveType(string name, Guid ownerid, Guid OwningBusinessUnitId, DateTime startDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startDate", startDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "applicabletoleave", true);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "applicabletoawol", true);

            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteInpatientLeaveType(Guid InpatientLeaveTypeid)
        {
            this.DeleteRecord(TableName, InpatientLeaveTypeid);
        }
    }
}
