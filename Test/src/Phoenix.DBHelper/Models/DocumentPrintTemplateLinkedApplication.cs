using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentPrintTemplateLinkedApplication : BaseClass
    {

        private string tableName = "DocumentPrintTemplateLinkedApplication";
        private string primaryKeyName = "DocumentPrintTemplateLinkedApplicationId";

        public DocumentPrintTemplateLinkedApplication()
        {
            AuthenticateUser();
        }

        public DocumentPrintTemplateLinkedApplication(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetBDocumentPrintTemplateId(Guid DocumentPrintTemplateId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DocumentPrintTemplateId", ConditionOperatorType.Equal, DocumentPrintTemplateId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid DocumentPrintTemplateLinkedApplicationId, params string[] fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentPrintTemplateLinkedApplicationId);

            this.AddReturnFields(query, tableName, fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);

        }

        public void DeleteDocumentPrintTemplateLinkedApplication(Guid documentprinttemplatelinkedapplicationid)
        {
            this.DeleteRecord(tableName, documentprinttemplatelinkedapplicationid);
        }
    }
}
