using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonClinicalRiskFactorHistory : BaseClass
    {

        public string TableName = "PersonClinicalRiskFactorHistory";
        public string PrimaryKeyName = "PersonClinicalRiskFactorHistoryId";


        public PersonClinicalRiskFactorHistory()
        {
            AuthenticateUser();
        }

        public PersonClinicalRiskFactorHistory(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonClinicalRiskFactorId(Guid PersonClinicalRiskFactorId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonClinicalRiskFactorId", ConditionOperatorType.Equal, PersonClinicalRiskFactorId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonClinicalRiskFactorHistoryByID(Guid PersonClinicalRiskFactorHistoryId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonClinicalRiskFactorHistoryId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteClinicalRiskFactorId(Guid PersonClinicalRiskFactorHistoryId)
        {
            this.DeleteRecord(TableName, PersonClinicalRiskFactorHistoryId);
        }




    }
}
