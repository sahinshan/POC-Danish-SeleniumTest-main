using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageEpisodeAttachment : BaseClass
    {

        public string TableName = "BrokerageEpisodeAttachment";
        public string PrimaryKeyName = "BrokerageEpisodeAttachmentId";


        public BrokerageEpisodeAttachment()
        {
            AuthenticateUser();
        }

        public BrokerageEpisodeAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBrokerageEpisodeAttachment(Guid ownerid, string title, DateTime Date, Guid DocumentTypeId, Guid DocumentSubTypeId, Guid FileId, Guid PersonId, Guid CaseId, Guid BrokerageEpisodeId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "date", Date);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DocumentTypeId", DocumentTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DocumentSubTypeId", DocumentSubTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "FileId", FileId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CaseId", CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "BrokerageEpisodeId", BrokerageEpisodeId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsCloned", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Declared", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByBrokerageEpisodeId(Guid BrokerageEpisodeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "BrokerageEpisodeId", ConditionOperatorType.Equal, BrokerageEpisodeId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetBrokerageEpisodeAttachmentByID(Guid BrokerageEpisodeAttachmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageEpisodeAttachmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageEpisodeAttachment(Guid BrokerageEpisodeAttachmentId)
        {
            this.DeleteRecord(TableName, BrokerageEpisodeAttachmentId);
        }
    }
}
