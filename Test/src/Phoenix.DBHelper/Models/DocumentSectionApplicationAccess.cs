using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentSectionApplicationAccess : BaseClass
    {

        private string tableName = "DocumentSectionApplicationAccess";
        private string primaryKeyName = "DocumentSectionApplicationAccessId";

        public DocumentSectionApplicationAccess()
        {
            AuthenticateUser();
        }

        public DocumentSectionApplicationAccess(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDocumentSectionApplicationAccess(Guid ApplicationId, Guid DocumentSectionId, bool CanEdit)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ApplicationId", ApplicationId);
            AddFieldToBusinessDataObject(dataObject, "DocumentSectionId", DocumentSectionId);
            AddFieldToBusinessDataObject(dataObject, "CanEdit", CanEdit);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByDocumentSectionId(Guid DocumentSectionId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DocumentSectionId", ConditionOperatorType.Equal, DocumentSectionId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid DocumentSectionApplicationAccessId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentSectionApplicationAccessId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteDocumentSectionApplicationAccess(Guid DocumentSectionApplicationAccessID)
        {
            this.DeleteRecord(tableName, DocumentSectionApplicationAccessID);
        }



    }
}
