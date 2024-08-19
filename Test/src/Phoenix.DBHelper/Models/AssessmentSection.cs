using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AssessmentSection : BaseClass
    {

        private string tableName = "AssessmentSection";
        private string primaryKeyName = "AssessmentSectionId";

        public AssessmentSection()
        {
            AuthenticateUser();
        }

        public AssessmentSection(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetAssessmentSection(Guid AssessmentId, Guid DocumentSectionId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "AssessmentId", ConditionOperatorType.Equal, AssessmentId);
            this.BaseClassAddTableCondition(query, "DocumentSectionId", ConditionOperatorType.Equal, DocumentSectionId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetAssessmentSectionByID(Guid AssessmentSectionId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, AssessmentSectionId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }





    }
}
