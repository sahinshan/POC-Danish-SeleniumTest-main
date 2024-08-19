using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentFile : BaseClass
    {

        private string tableName = "DocumentFile";
        private string primaryKeyName = "DocumentFileId";

        public DocumentFile()
        {
            AuthenticateUser();
        }

        public DocumentFile(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public void UpdateRelatedBusinessObject(Guid DocumentFileId, Guid? BusinessObjectRecordId, string BusinessObjectTableName, string BusinessObjectFieldName)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentFileId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "BusinessObjectRecordId", BusinessObjectRecordId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "BusinessObjectTableName", BusinessObjectTableName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "BusinessObjectFieldName", BusinessObjectFieldName);

            this.UpdateRecord(buisinessDataObject);
        }



        public List<Guid> GetByDocumentFileId(Guid DocumentFileId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DocumentFileId", ConditionOperatorType.Equal, DocumentFileId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByBusinessObjectRecordId(Guid BusinessObjectRecordId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "BusinessObjectRecordId", ConditionOperatorType.Equal, BusinessObjectRecordId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid DocumentFileId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentFileId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
