using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentOutcomeType : BaseClass
    {

        private string tableName = "DocumentOutcomeType";
        private string primaryKeyName = "DocumentOutcomeTypeId";

        public DocumentOutcomeType()
        {
            AuthenticateUser();
        }

        public DocumentOutcomeType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDocumentOutcomeType(Guid outcomeid, Guid documentid)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "outcomeid", outcomeid);
            AddFieldToBusinessDataObject(dataObject, "documentid", documentid);

            return this.CreateRecord(dataObject);
        }


        public List<Guid> GetByDocumentOutcomeTypeId(Guid DocumentOutcomeTypeId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentOutcomeTypeId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByOutcomeId(Guid OutcomeId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "outcomeid", ConditionOperatorType.Equal, OutcomeId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByOutcomeIdAndDocumentId(Guid OutcomeId, Guid DocumentId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "outcomeid", ConditionOperatorType.Equal, OutcomeId);

            this.BaseClassAddTableCondition(query, "documentid", ConditionOperatorType.Equal, DocumentId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetDocumentOutcomeTypeByID(Guid DocumentOutcomeTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentOutcomeTypeId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
