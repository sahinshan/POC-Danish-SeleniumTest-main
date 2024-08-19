using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CaseFormAssessmentFactor : BaseClass
    {
        public string TableName { get { return "CaseFormAssessmentFactor"; } }
        public string PrimaryKeyName { get { return "CaseFormAssessmentFactorid"; } }


        public CaseFormAssessmentFactor()
        {
            AuthenticateUser();
        }

        public CaseFormAssessmentFactor(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByCaseID(Guid CaseId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByCaseFormID(Guid caseformid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "caseformid", ConditionOperatorType.Equal, caseformid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByCasePersonId(Guid PersonId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public Dictionary<string, object> GetByID(Guid CaseFormAssessmentFactorId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseFormAssessmentFactorId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCaseFormAssessmentFactor(Guid CaseFormAssessmentFactorid)
        {
            this.DeleteRecord(TableName, CaseFormAssessmentFactorid);
        }
    }
}
