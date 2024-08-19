using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonAddress : BaseClass
    {
        public PersonAddress()
        {
            AuthenticateUser();
        }

        public PersonAddress(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }



        public List<Guid> GetByPersonId(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject("PersonAddress", false, "PersonAddressId");

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, "PersonAddress", "PersonAddressId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "PersonAddressId");
        }

        public List<Guid> GetPersonAddress(Guid personid, int addresstypeid, DateTime startdate)
        {
            DataQuery query = this.GetDataQueryObject("PersonAddress", false, "PersonAddressId");

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);
            this.BaseClassAddTableCondition(query, "addresstypeid", ConditionOperatorType.Equal, addresstypeid);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.Equal, startdate);

            this.AddReturnField(query, "PersonAddress", "PersonAddressId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "PersonAddressId");
        }

        public Dictionary<string, object> GetPersonAddressById(Guid PersonAddressID, params string[] fields)
        {
            DataQuery query = this.GetDataQueryObject("PersonAddress", false, "PersonAddressId");

            this.AddReturnFields(query, "PersonAddress", fields);
            this.BaseClassAddTableCondition(query, "PersonAddressId", ConditionOperatorType.Equal, PersonAddressID);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonAddress(Guid PersonAddressID)
        {
            this.DeleteRecord("PersonAddress", PersonAddressID);
        }





    }
}
