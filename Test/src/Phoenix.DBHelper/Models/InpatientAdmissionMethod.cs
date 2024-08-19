using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientAdmissionMethod : BaseClass
    {
        public string TableName { get { return "InpatientAdmissionMethod"; } }
        public string PrimaryKeyName { get { return "InpatientAdmissionMethodid"; } }


        public InpatientAdmissionMethod()
        {
            AuthenticateUser();
        }

        public InpatientAdmissionMethod(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public Dictionary<string, object> GetByID(Guid InpatientAdmissionMethodId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, InpatientAdmissionMethodId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetAdmissionMethodByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateAdmissionMethod(string name, Guid ownerid, Guid OwningBusinessUnitId, DateTime startDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startDate", startDate);

            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteInpatientAdmissionMethod(Guid InpatientAdmissionMethodid)
        {
            this.DeleteRecord(TableName, InpatientAdmissionMethodid);
        }
    }
}
