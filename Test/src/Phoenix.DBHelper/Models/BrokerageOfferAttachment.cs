using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageOfferAttachment : BaseClass
    {

        public string TableName = "BrokerageOfferAttachment";
        public string PrimaryKeyName = "BrokerageOfferAttachmentId";


        public BrokerageOfferAttachment()
        {
            AuthenticateUser();
        }

        public BrokerageOfferAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBrokerageOfferAttachment(Guid ownerid, string title, DateTime Date, Guid DocumentTypeId, Guid DocumentSubTypeId, Guid FileId, Guid PersonId, Guid CaseId, Guid BrokerageOfferId)
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
            this.AddFieldToBusinessDataObject(buisinessDataObject, "BrokerageOfferId", BrokerageOfferId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsCloned", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Declared", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByBrokerageOfferId(Guid BrokerageOfferId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "BrokerageOfferId", ConditionOperatorType.Equal, BrokerageOfferId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetBrokerageOfferAttachmentByID(Guid BrokerageOfferAttachmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageOfferAttachmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageOfferAttachment(Guid BrokerageOfferAttachmentId)
        {
            this.DeleteRecord(TableName, BrokerageOfferAttachmentId);
        }
    }
}
