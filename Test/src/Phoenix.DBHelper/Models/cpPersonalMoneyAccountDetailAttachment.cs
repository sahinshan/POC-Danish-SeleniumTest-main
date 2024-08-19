using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class cpPersonalMoneyAccountDetailAttachment : BaseClass
    {

        private string tableName = "cpPersonalMoneyAccountDetailAttachment";
        private string primaryKeyName = "cpPersonalMoneyAccountDetailAttachmentId";

        public cpPersonalMoneyAccountDetailAttachment()
        {
            AuthenticateUser();
        }

        public cpPersonalMoneyAccountDetailAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public void CreateMultipleAttachmentRecords(int TotalRecordsToCreate, Guid ownerid, Guid personalmoneyaccountdetailid, string title, string FilePath)
        {
            var allRecordsToCreate = new List<BusinessData>();

            //we cannot insert more than 1000 records at once
            if (TotalRecordsToCreate > 100)
                TotalRecordsToCreate = 100;

            for (int i = 0; i < TotalRecordsToCreate; i++)
            {
                var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

                this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "personalmoneyaccountdetailid", personalmoneyaccountdetailid);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "title", title);

                this.AddFieldToBusinessDataObject(buisinessDataObject, "iscloned", 0);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "declared", 0);

                allRecordsToCreate.Add(buisinessDataObject);
            }

            var cpPersonalMoneyAccountDetailAttachmentIds = this.CreateMultipleRecords(allRecordsToCreate);

            if (!string.IsNullOrEmpty(FilePath))
            {
                foreach (var cpPersonalMoneyAccountDetailAttachmentId in cpPersonalMoneyAccountDetailAttachmentIds)
                {
                    var fileId = UploadFile(FilePath, tableName, primaryKeyName, cpPersonalMoneyAccountDetailAttachmentId);

                    UpdateFileId(cpPersonalMoneyAccountDetailAttachmentId, fileId);
                }
            }
        }

        public void UpdateFileId(Guid cpPersonalMoneyAccountDetailAttachmentId, Guid? FileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, cpPersonalMoneyAccountDetailAttachmentId);

            AddFieldToBusinessDataObject(buisinessDataObject, "fileid", FileId);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonalMoneyAccountDetailId(Guid personalmoneyaccountdetailid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personalmoneyaccountdetailid", ConditionOperatorType.Equal, personalmoneyaccountdetailid);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid cpPersonalMoneyAccountDetailAttachmentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, cpPersonalMoneyAccountDetailAttachmentId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletecpPersonalMoneyAccountDetailAttachment(Guid cpPersonalMoneyAccountDetailAttachmentID)
        {
            this.DeleteRecord(tableName, cpPersonalMoneyAccountDetailAttachmentID);
        }

    }
}
