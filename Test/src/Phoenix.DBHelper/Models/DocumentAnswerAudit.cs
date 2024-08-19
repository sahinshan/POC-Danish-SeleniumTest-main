using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentAnswerAudit : BaseClass
    {

        private string tableName = "DocumentAnswerAudit";
        private string primaryKeyName = "DocumentAnswerAuditId";

        public DocumentAnswerAudit()
        {
            AuthenticateUser();
        }

        public DocumentAnswerAudit(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetDocumentAnswerAuditForAssessmentQuestion(Guid AssessmentID, string DocumentSectionQuestionIdentifier)
        {
            DataQuery query = new DataQuery("DocumentAnswerAudit", false);
            query.PrimaryKeyName = "DocumentAnswerAuditId";

            query.AddRelatedTableRelationship("documentanswer", "documentanswerid", "DocumentAnswerAudit", "documentanswerid", CareDirector.Sdk.Enums.JoinOperator.InnerJoin, "documentanswer");
            query.AddRelatedTableRelationship("DocumentQuestionIdentifier", "DocumentQuestionIdentifierId", "documentanswer", "DocumentQuestionIdentifierId", CareDirector.Sdk.Enums.JoinOperator.InnerJoin, "DocumentQuestionIdentifier");

            query.AddRelatedTableCondition("documentanswer", "AssessmentId", ConditionOperatorType.Equal, AssessmentID);
            query.AddRelatedTableCondition("DocumentQuestionIdentifier", "Identifier", ConditionOperatorType.Equal, DocumentSectionQuestionIdentifier);

            query.AddField("DocumentAnswerAudit", "DocumentAnswerAuditId", "DocumentAnswerAuditId");

            query.AddThisTableOrder("createdon", SortOrder.Ascending);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            List<Guid> data = new List<Guid>();

            foreach (var businessData in response.BusinessDataCollection)
            {
                if (businessData.FieldCollection["DocumentAnswerAuditId".ToLower()] != null)
                {
                    string fieldData = businessData.FieldCollection["DocumentAnswerAuditId".ToLower()].ToString();
                    data.Add(Guid.Parse(fieldData));
                }
            }
            if (response.HasErrors) throw new Exception(response.Error);

            return data;

        }


        public Dictionary<string, object> GetDocumentAnswerAuditByID(Guid DocumentAnswerAuditId, params string[] fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentAnswerAuditId);

            this.AddReturnFields(query, tableName, fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);

        }


    }
}
