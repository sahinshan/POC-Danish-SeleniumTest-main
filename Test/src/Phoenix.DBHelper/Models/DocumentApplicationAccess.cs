using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentApplicationAccess : BaseClass
    {

        private string tableName = "DocumentApplicationAccess";
        private string primaryKeyName = "DocumentApplicationAccessId";

        public DocumentApplicationAccess()
        {
            AuthenticateUser();
        }

        public DocumentApplicationAccess(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDocumentApplicationAccess(Guid ApplicationId, Guid DocumentId, bool CanEdit)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ApplicationId", ApplicationId);
            AddFieldToBusinessDataObject(dataObject, "DocumentId", DocumentId);
            AddFieldToBusinessDataObject(dataObject, "CanEdit", CanEdit);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByDocumentId(Guid DocumentId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DocumentId", ConditionOperatorType.Equal, DocumentId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid DocumentApplicationAccessId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentApplicationAccessId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteDocumentApplicationAccess(Guid DocumentApplicationAccessID)
        {
            this.DeleteRecord(tableName, DocumentApplicationAccessID);
        }



    }
}
