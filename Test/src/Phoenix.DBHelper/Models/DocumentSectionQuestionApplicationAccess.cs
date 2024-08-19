using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentSectionQuestionApplicationAccess : BaseClass
    {

        private string tableName = "DocumentSectionQuestionApplicationAccess";
        private string primaryKeyName = "DocumentSectionQuestionApplicationAccessId";

        public DocumentSectionQuestionApplicationAccess()
        {
            AuthenticateUser();
        }

        public DocumentSectionQuestionApplicationAccess(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDocumentSectionQuestionApplicationAccess(Guid ApplicationId, Guid DocumentSectionQuestionId, bool CanEdit)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ApplicationId", ApplicationId);
            AddFieldToBusinessDataObject(dataObject, "DocumentSectionQuestionId", DocumentSectionQuestionId);
            AddFieldToBusinessDataObject(dataObject, "CanEdit", CanEdit);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByDocumentSectionQuestionId(Guid DocumentSectionQuestionId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DocumentSectionQuestionId", ConditionOperatorType.Equal, DocumentSectionQuestionId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid DocumentSectionQuestionApplicationAccessId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentSectionQuestionApplicationAccessId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteDocumentSectionQuestionApplicationAccess(Guid DocumentSectionQuestionApplicationAccessID)
        {
            this.DeleteRecord(tableName, DocumentSectionQuestionApplicationAccessID);
        }



    }
}
