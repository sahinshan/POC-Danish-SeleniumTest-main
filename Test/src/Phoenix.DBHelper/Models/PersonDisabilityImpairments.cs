using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonDisabilityImpairments : BaseClass
    {

        public string TableName = "PersonDisabilityImpairments";
        public string PrimaryKeyName = "PersonDisabilityImpairmentsId";

        public PersonDisabilityImpairments()
        {
            AuthenticateUser();
        }

        public PersonDisabilityImpairments(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonDisabilityImpairments(Guid ownerid, Guid disabilityimpairmenttypeid, Guid disabilitytypeid, Guid personid,
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

        public List<Guid> GetPersonDisabilityImpairmentsByPersonID(Guid personid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public Dictionary<string, object> GetPersonDisabilityImpairmentsByID(Guid PersonDisabilityImpairmentsId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonDisabilityImpairmentsId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonDisabilityImpairments(Guid PersonDisabilityImpairmentsId)
        {
            this.DeleteRecord(TableName, PersonDisabilityImpairmentsId);
        }
    }
}
