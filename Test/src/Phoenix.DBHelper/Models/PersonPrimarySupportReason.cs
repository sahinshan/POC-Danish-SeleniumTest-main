using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonPrimarySupportReason : BaseClass
    {

        public string TableName = "PersonPrimarySupportReason";
        public string PrimaryKeyName = "PersonPrimarySupportReasonId";


        public PersonPrimarySupportReason()
        {
            AuthenticateUser();
        }

        public PersonPrimarySupportReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetPersonPrimarySupportReasonByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonPrimarySupportReasonByID(Guid PersonPrimarySupportReasonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonPrimarySupportReasonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



        public void DeletePersonPrimarySupportReason(Guid PersonPrimarySupportReasonId)
        {
            this.DeleteRecord(TableName, PersonPrimarySupportReasonId);
        }
        public Guid CreatePersonPrimarySupportReason(Guid personID, Guid OwnerId, Guid primarysupportreasontypeid, DateTime startdate, DateTime enddate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "personID", personID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "primarysupportreasontypeid", primarysupportreasontypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            return this.CreateRecord(buisinessDataObject);

        }

    }

}
