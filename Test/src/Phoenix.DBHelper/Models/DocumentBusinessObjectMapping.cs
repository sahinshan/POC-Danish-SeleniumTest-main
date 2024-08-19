using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentBusinessObjectMapping : BaseClass
    {
        private string TableName = "DocumentBusinessObjectMapping";
        private string PrimaryKeyName = "DocumentBusinessObjectMappingId";

        public DocumentBusinessObjectMapping()
        {
            AuthenticateUser();
        }

        public DocumentBusinessObjectMapping(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDocumentBusinessObjectMapping(Guid DocumentId, Guid BusinessObjectId, Guid SectionQuestionId, bool AllowMultipleInstances, string InstanceName)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "DocumentId", DocumentId);
            AddFieldToBusinessDataObject(buisinessDataObject, "BusinessObjectId", BusinessObjectId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SectionQuestionId", SectionQuestionId);
            AddFieldToBusinessDataObject(buisinessDataObject, "AllowMultipleInstances", AllowMultipleInstances);
            AddFieldToBusinessDataObject(buisinessDataObject, "InstanceName", InstanceName);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);
            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetDocumentBusinessObjectMappingByTitle(string Title)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, Title);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByDocumentBusinessObjectMappingId(Guid DocumentBusinessObjectMappingId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, DocumentBusinessObjectMappingId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid DocumentBusinessObjectMappingId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, DocumentBusinessObjectMappingId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteServiceProvisionRateScheduleRecord(Guid DocumentBusinessObjectMappingId)
        {
            this.DeleteRecord(TableName, DocumentBusinessObjectMappingId);
        }
    }
}
