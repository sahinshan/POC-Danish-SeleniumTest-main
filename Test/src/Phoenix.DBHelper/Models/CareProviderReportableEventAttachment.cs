using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderReportableEventAttachment : BaseClass
    {
        public string TableName { get { return "CareProviderReportableEventAttachment"; } }
        public string PrimaryKeyName { get { return "CareProviderReportableEventAttachmentid"; } }

        public CareProviderReportableEventAttachment()
        {
            AuthenticateUser();
        }

        public CareProviderReportableEventAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareProviderReportableEventAttachment(Guid careproviderreportableeventid, Guid ownerid, string title, DateTime date, Guid documenttypeid, Guid documentsubtypeid, string FilePath)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderreportableeventid", careproviderreportableeventid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "date", date);
            AddFieldToBusinessDataObject(buisinessDataObject, "documenttypeid", documenttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "documentsubtypeid", documentsubtypeid);

            var CareProviderReportableEventAttachmentId = CreateRecord(buisinessDataObject);


            if (!string.IsNullOrEmpty(FilePath))
            {
                var fileId = UploadFile(FilePath, TableName, "fileid", CareProviderReportableEventAttachmentId);
                UpdateFileId(CareProviderReportableEventAttachmentId, fileId);
            }

            return CareProviderReportableEventAttachmentId;
        }

        public void UpdateFileId(Guid PersonAttachmentId, Guid? FileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonAttachmentId);

            AddFieldToBusinessDataObject(buisinessDataObject, "fileid", FileId);

            this.UpdateRecord(buisinessDataObject);
        }
        public List<Guid> GetByReportableEvent(Guid careproviderreportableeventid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careproviderreportableeventid", ConditionOperatorType.Equal, careproviderreportableeventid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public Dictionary<string, object> GetByTitle(Guid title, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, title);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public void DeleteCareProviderReportableEventAttachment(Guid CareProviderReportableEventAttachmentID)
        {
            this.DeleteRecord(TableName, CareProviderReportableEventAttachmentID);
        }

    }
}
