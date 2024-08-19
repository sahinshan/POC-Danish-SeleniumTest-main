using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonChronology : BaseClass
    {

        public string TableName = "PersonChronology";
        public string PrimaryKeyName = "PersonChronologyId";


        public PersonChronology()
        {
            AuthenticateUser();
        }

        public PersonChronology(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetPersonChronologyByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonChronologyByID(Guid PersonChronologyId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonChronologyId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }




        public Guid CreatePersonChronology(string Title, DateTime StartDate, DateTime EndDate, Guid ownerid, Guid personid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "Title", Title);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", EndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            return this.CreateRecord(buisinessDataObject);

        }

        public void DeletePersonChronology(Guid PersonChronologyId)
        {
            this.DeleteRecord(TableName, PersonChronologyId);
        }
    }
}
