using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AssessmentSectionQuestionComment : BaseClass
    {

        private string tableName = "AssessmentSectionQuestionComment";
        private string primaryKeyName = "AssessmentSectionQuestionCommentId";

        public AssessmentSectionQuestionComment()
        {
            AuthenticateUser();
        }

        public AssessmentSectionQuestionComment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetAssessmentSectionQuestionCommentsForAssessmentQuestion(Guid AssessmentID, string DocumentSectionQuestionIdentifier)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            AddRelatedTableRelationship(query, "AssessmentSectionQuestion", "AssessmentSectionQuestionId", "AssessmentSectionQuestionComment", "AssessmentSectionQuestionId", JoinOperator.InnerJoin, "AssessmentSectionQuestion");
            AddRelatedTableRelationship(query, "DocumentSectionQuestion", "DocumentSectionQuestionId", "AssessmentSectionQuestion", "DocumentSectionQuestionId", JoinOperator.InnerJoin, "DocumentSectionQuestion");

            //BaseClassAddTableCondition(query, "AssessmentId", ConditionOperatorType.Equal, AssessmentID);
            AddRelatedTableCondition(query, "AssessmentSectionQuestion", "AssessmentId", ConditionOperatorType.Equal, AssessmentID);

            AddRelatedTableCondition(query, "DocumentSectionQuestion", "SectionQuestionIdentifier", ConditionOperatorType.Equal, DocumentSectionQuestionIdentifier);

            AddReturnField(query, tableName, primaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetAssessmentSectionQuestionCommentByID(Guid AssessmentSectionQuestionCommentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, AssessmentSectionQuestionCommentId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }





    }
}
