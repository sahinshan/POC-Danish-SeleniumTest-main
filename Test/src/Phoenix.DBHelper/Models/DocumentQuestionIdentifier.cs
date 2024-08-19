using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentQuestionIdentifier : BaseClass
    {

        private string tableName = "DocumentQuestionIdentifier";
        private string primaryKeyName = "DocumentQuestionIdentifierId";

        public DocumentQuestionIdentifier()
        {
            AuthenticateUser();
        }

        public DocumentQuestionIdentifier(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByIdentifier(string Identifier)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Identifier", ConditionOperatorType.Equal, Identifier);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByDocumentSectionQuestionIdAndQuestionCatalogueId(Guid DocumentSectionQuestionId, Guid QuestionCatalogueId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DocumentSectionQuestionId", ConditionOperatorType.Equal, DocumentSectionQuestionId);
            this.BaseClassAddTableCondition(query, "QuestionCatalogueId", ConditionOperatorType.Equal, QuestionCatalogueId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetDocumentQuestionIdentifierByID(Guid DocumentQuestionIdentifierId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentQuestionIdentifierId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByDocumentSectionQuestionId(Guid DocumentSectionQuestionId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DocumentSectionQuestionId", ConditionOperatorType.Equal, DocumentSectionQuestionId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


    }
}
