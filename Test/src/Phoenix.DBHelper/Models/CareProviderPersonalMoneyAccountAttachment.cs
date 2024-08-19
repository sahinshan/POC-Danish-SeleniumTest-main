using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderPersonalMoneyAccountAttachment : BaseClass
    {

        private string tableName = "CareProviderPersonalMoneyAccountAttachment";
        private string primaryKeyName = "CareProviderPersonalMoneyAccountAttachmentId";

        public CareProviderPersonalMoneyAccountAttachment()
        {
            AuthenticateUser();
        }

        public CareProviderPersonalMoneyAccountAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public void CreateMultipleAttachmentRecords(int TotalRecordsToCreate, Guid ownerid, Guid personalmoneyaccountid, string title, string FilePath)
        {
            var allRecordsToCreate = new List<BusinessData>();

            //we cannot insert more than 1000 records at once
            if (TotalRecordsToCreate > 100)
                TotalRecordsToCreate = 100;

            for (int i = 0; i < TotalRecordsToCreate; i++)
            {
                var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

                this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "personalmoneyaccountid", personalmoneyaccountid);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "title", title);

                this.AddFieldToBusinessDataObject(buisinessDataObject, "iscloned", 0);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "declared", 0);

                allRecordsToCreate.Add(buisinessDataObject);
            }

            var CareProviderPersonalMoneyAccountAttachmentIds = this.CreateMultipleRecords(allRecordsToCreate);

            if (!string.IsNullOrEmpty(FilePath))
            {
                foreach (var CareProviderPersonalMoneyAccountAttachmentId in CareProviderPersonalMoneyAccountAttachmentIds)
                {
                    var fileId = UploadFile(FilePath, tableName, primaryKeyName, CareProviderPersonalMoneyAccountAttachmentId);
                    UpdateFileId(CareProviderPersonalMoneyAccountAttachmentId, fileId);
                }
            }
        }

        public void UpdateFileId(Guid CareProviderPersonalMoneyAccountAttachmentId, Guid? FileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, CareProviderPersonalMoneyAccountAttachmentId);

            AddFieldToBusinessDataObject(buisinessDataObject, "fileid", FileId);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonalMoneyAccountId(Guid personalmoneyaccountid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personalmoneyaccountid", ConditionOperatorType.Equal, personalmoneyaccountid);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CareProviderPersonalMoneyAccountAttachmentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CareProviderPersonalMoneyAccountAttachmentId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCareProviderPersonalMoneyAccountAttachment(Guid CareProviderPersonalMoneyAccountAttachmentID)
        {
            this.DeleteRecord(tableName, CareProviderPersonalMoneyAccountAttachmentID);
        }

    }
}
