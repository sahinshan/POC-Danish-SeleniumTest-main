using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentAnswerChecklist : BaseClass
    {

        private string tableName = "DocumentAnswerChecklist";
        private string primaryKeyName = "DocumentAnswerChecklistId";

        public DocumentAnswerChecklist()
        {
            AuthenticateUser();
        }

        public DocumentAnswerChecklist(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDocumentAnswerChecklist(Guid DocumentAnswerId, Guid MultiOptionAnswerId, bool Checked)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "DocumentAnswerId", DocumentAnswerId);
            AddFieldToBusinessDataObject(dataObject, "MultiOptionAnswerId", MultiOptionAnswerId);
            AddFieldToBusinessDataObject(dataObject, "Checked", Checked);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByDocumentAnswerAndMultiOption(Guid DocumentAnswerId, Guid MultiOptionAnswerId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DocumentAnswerId", ConditionOperatorType.Equal, DocumentAnswerId);
            this.BaseClassAddTableCondition(query, "MultiOptionAnswerId", ConditionOperatorType.Equal, MultiOptionAnswerId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid DocumentAnswerChecklistId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentAnswerChecklistId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteDocumentAnswerChecklist(Guid DocumentAnswerChecklistID)
        {
            this.DeleteRecord(tableName, DocumentAnswerChecklistID);
        }



    }
}
