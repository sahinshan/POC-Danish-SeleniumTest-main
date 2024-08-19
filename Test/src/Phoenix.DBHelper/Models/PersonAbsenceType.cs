using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonAbsenceType : BaseClass
    {

        public string TableName = "PersonAbsenceType";
        public string PrimaryKeyName = "PersonAbsenceTypeId";
        public PersonAbsenceType()
        {
            AuthenticateUser();
        }

        public PersonAbsenceType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }



        public List<Guid> GetByPersonId(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject("PersonAbsenceType", false, "PersonAbsenceTypeId");

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, "PersonAbsenceType", "PersonAbsenceTypeId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "PersonAbsenceTypeId");
        }

        public List<Guid> GetPersonAbsenceTypeByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreatePersonAbsenceType(string name, Guid ownerid, Guid OwningBusinessUnitId, DateTime startDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startDate", startDate);

            return this.CreateRecord(buisinessDataObject);

        }

        public void DeletePersonAbsenceType(Guid PersonAbsenceTypeID)
        {
            this.DeleteRecord("PersonAbsenceType", PersonAbsenceTypeID);
        }





    }
}
