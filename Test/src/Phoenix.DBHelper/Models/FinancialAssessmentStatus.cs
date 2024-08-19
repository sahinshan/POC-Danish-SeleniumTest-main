using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinancialAssessmentStatus : BaseClass
    {
        public string TableName = "FinancialAssessmentStatus";
        public string PrimaryKeyName = "FinancialAssessmentStatusId";

        public FinancialAssessmentStatus()
        {
            AuthenticateUser();
        }

        public FinancialAssessmentStatus(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetFinancialAssessmentStatusByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetFinancialAssessmentStatusByID(Guid FinancialAssessmentStatusId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, FinancialAssessmentStatusId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteFinancialAssessmentStatus(Guid FinancialAssessmentStatusId)
        {
            this.DeleteRecord(TableName, FinancialAssessmentStatusId);
        }

    }
}
