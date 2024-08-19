using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonSignificantEventChronology : BaseClass
    {

        public string TableName = "PersonSignificantEventChronology";
        public string PrimaryKeyName = "PersonSignificantEventChronologyId";


        public PersonSignificantEventChronology()
        {
            AuthenticateUser();
        }

        public PersonSignificantEventChronology(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Dictionary<string, object> GetPersonSignificantEventChronologyByID(Guid PersonSignificantEventChronologyId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonSignificantEventChronologyId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreatePersonSignificantEventChronology(Guid PersonChronologyId, Guid PersonSignificantEventId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonChronologyId", PersonChronologyId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonSignificantEventId", PersonSignificantEventId);

            return this.CreateRecord(buisinessDataObject);

        }

        public void DeletePersonSignificantEventChronology(Guid PersonSignificantEventChronologyId)
        {
            this.DeleteRecord(TableName, PersonSignificantEventChronologyId);
        }
    }
}
