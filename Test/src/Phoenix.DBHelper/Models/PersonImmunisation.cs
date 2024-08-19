using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonImmunisation : BaseClass
    {

        public string TableName = "PersonImmunisation";
        public string PrimaryKeyName = "PersonImmunisationId";

        public PersonImmunisation()
        {
            AuthenticateUser();
        }

        public PersonImmunisation(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonImmunisation(Guid ownerid, Guid disabilityimpairmenttypeid, Guid disabilitytypeid, Guid personid,
            DateTime startdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "disabilityimpairmenttypeid", disabilityimpairmenttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "disabilitytypeid", disabilitytypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);




            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonImmunisationByPersonID(Guid personid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public Dictionary<string, object> GetPersonImmunisationByID(Guid PersonImmunisationId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonImmunisationId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonImmunisation(Guid PersonImmunisationId)
        {
            this.DeleteRecord(TableName, PersonImmunisationId);
        }
    }
}
