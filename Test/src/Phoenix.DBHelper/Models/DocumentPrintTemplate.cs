using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentPrintTemplate : BaseClass
    {

        private string tableName = "DocumentPrintTemplate";
        private string primaryKeyName = "DocumentPrintTemplateId";

        public DocumentPrintTemplate()
        {
            AuthenticateUser();
        }

        public DocumentPrintTemplate(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByDocumentIDAndName(Guid DocumentId, string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DocumentId", ConditionOperatorType.Equal, DocumentId);
            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid DocumentPrintTemplateId, params string[] fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentPrintTemplateId);

            this.AddReturnFields(query, tableName, fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);

        }


        public void DeleteDocumentPrintTemplate(Guid DocumentPrintTemplateId)
        {
            this.DeleteRecord(tableName, DocumentPrintTemplateId);
        }
    }
}
