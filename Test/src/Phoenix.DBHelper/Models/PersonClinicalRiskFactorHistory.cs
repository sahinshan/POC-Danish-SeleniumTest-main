using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonClinicalRiskFactor : BaseClass
    {

        public string TableName = "PersonClinicalRiskFactor";
        public string PrimaryKeyName = "PersonClinicalRiskFactorId";


        public PersonClinicalRiskFactor()
        {
            AuthenticateUser();
        }

        public PersonClinicalRiskFactor(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetPersonClinicalRiskFactorByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonClinicalRiskFactorByID(Guid PersonClinicalRiskFactorId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonClinicalRiskFactorId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteClinicalRiskFactorId(Guid PersonClinicalRiskFactorId)
        {
            this.DeleteRecord(TableName, PersonClinicalRiskFactorId);
        }




    }
}
