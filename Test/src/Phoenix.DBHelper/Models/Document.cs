using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class Document : BaseClass
    {

        private string tableName = "Document";
        private string primaryKeyName = "DocumentId";

        public Document()
        {
            AuthenticateUser();
        }

        public Document(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDocument(string Name, Guid DocumentCategoryId, Guid DocumentTypeId, Guid OwnerId, int StatusId)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "name", Name);
            AddFieldToBusinessDataObject(dataObject, "documentcategoryid", DocumentCategoryId);
            AddFieldToBusinessDataObject(dataObject, "documenttypeid", DocumentTypeId);
            AddFieldToBusinessDataObject(dataObject, "ownerid", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "statusid", StatusId);
            AddFieldToBusinessDataObject(dataObject, "commentbold", 0);
            AddFieldToBusinessDataObject(dataObject, "commentitalic", 0);
            AddFieldToBusinessDataObject(dataObject, "commentunderline", 0);
            AddFieldToBusinessDataObject(dataObject, "doesnotrequiremanagersignoff", 0);
            AddFieldToBusinessDataObject(dataObject, "tableheaderbold", 1);
            AddFieldToBusinessDataObject(dataObject, "tableheaderitalic", 0);
            AddFieldToBusinessDataObject(dataObject, "tableheaderunderline", 0);
            AddFieldToBusinessDataObject(dataObject, "instructionbold", 0);
            AddFieldToBusinessDataObject(dataObject, "instructionitalic", 0);
            AddFieldToBusinessDataObject(dataObject, "instructionunderline", 0);
            AddFieldToBusinessDataObject(dataObject, "primaryquestionbold", 0);
            AddFieldToBusinessDataObject(dataObject, "primaryquestionitalic", 0);
            AddFieldToBusinessDataObject(dataObject, "primaryquestionunderline", 0);
            AddFieldToBusinessDataObject(dataObject, "questionbold", 0);
            AddFieldToBusinessDataObject(dataObject, "questionitalic", 0);
            AddFieldToBusinessDataObject(dataObject, "questionunderline", 0);
            AddFieldToBusinessDataObject(dataObject, "sectionbold", 1);
            AddFieldToBusinessDataObject(dataObject, "sectionitalic", 0);
            AddFieldToBusinessDataObject(dataObject, "sectionunderline", 0);
            AddFieldToBusinessDataObject(dataObject, "subheadingbold", 0);
            AddFieldToBusinessDataObject(dataObject, "subheadingitalic", 0);
            AddFieldToBusinessDataObject(dataObject, "subheadingunderline", 0);
            AddFieldToBusinessDataObject(dataObject, "syncquestionids", 0);
            AddFieldToBusinessDataObject(dataObject, "version", "1.0");
            AddFieldToBusinessDataObject(dataObject, "answerfontsizeid", 12);
            AddFieldToBusinessDataObject(dataObject, "commentfontsizeid", 10);
            AddFieldToBusinessDataObject(dataObject, "fontstyleid", 1);
            AddFieldToBusinessDataObject(dataObject, "instructionfontsizeid", 12);
            AddFieldToBusinessDataObject(dataObject, "primaryquestionfontsizeid", 12);
            AddFieldToBusinessDataObject(dataObject, "questionfontsizeid", 12);
            AddFieldToBusinessDataObject(dataObject, "questionnumberingid", 1);
            AddFieldToBusinessDataObject(dataObject, "questionsubheadingfontsizeid", 12);
            AddFieldToBusinessDataObject(dataObject, "sectionfontcolorid", 12);
            AddFieldToBusinessDataObject(dataObject, "sectionfontsizeid", 14);
            AddFieldToBusinessDataObject(dataObject, "sectionheadingbackgroundcolorid", 11);
            AddFieldToBusinessDataObject(dataObject, "tablebordercolorid", 12);
            AddFieldToBusinessDataObject(dataObject, "tableheaderbackgroundcolorid", 13);
            AddFieldToBusinessDataObject(dataObject, "tableheaderfontsizeid", 12);
            AddFieldToBusinessDataObject(dataObject, "sdemappingmodeid", 1);
            AddFieldToBusinessDataObject(dataObject, "availableinmobile", 0);
            AddFieldToBusinessDataObject(dataObject, "displaypagenumber", 0);
            AddFieldToBusinessDataObject(dataObject, "disableautosave", 0);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserselfsignoffallowed", 1);
            AddFieldToBusinessDataObject(dataObject, "automaticallycloseoncompletion", 0);
            AddFieldToBusinessDataObject(dataObject, "multipleopenformsallowed", 0);
            AddFieldToBusinessDataObject(dataObject, "allowformstatuscloning", 0);
            AddFieldToBusinessDataObject(dataObject, "completiondateallowedbeforestartdate", 1);
            AddFieldToBusinessDataObject(dataObject, "signoffdateallowedbeforecompletion", 1);
            AddFieldToBusinessDataObject(dataObject, "signoffdateallowedbeforestartdate", 1);
            AddFieldToBusinessDataObject(dataObject, "reviewdateallowedbeforestartdate", 1);
            AddFieldToBusinessDataObject(dataObject, "signoffdateallowedbeforecasedate", 1);
            AddFieldToBusinessDataObject(dataObject, "startdateallowedbeforecasestartdate", 1);
            AddFieldToBusinessDataObject(dataObject, "caseclosurebeforeformcompletedcloseddate", 1);
            AddFieldToBusinessDataObject(dataObject, "recordassessmentfactor", 0);
            AddFieldToBusinessDataObject(dataObject, "enablereporting", 0);
            AddFieldToBusinessDataObject(dataObject, "isreportingtableinsync", 0);
            AddFieldToBusinessDataObject(dataObject, "availabilityid", 2);
            AddFieldToBusinessDataObject(dataObject, "groupedassessment", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetDocumentByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetDocumentByID(Guid DocumentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateAvailability(Guid DocumentID, int availabilityid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "availabilityid", availabilityid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateStatus(Guid DocumentID, int statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateStatus(Guid DocumentID, bool activeStatus)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", activeStatus);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAllowMultipleForms(Guid DocumentID, bool multipleopenformsallowed)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "multipleopenformsallowed", multipleopenformsallowed);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateSDEMappingModeId(Guid DocumentID, int sdemappingmodeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "sdemappingmodeid", sdemappingmodeid);

            this.UpdateRecord(buisinessDataObject);
        }

        public Guid? GetDataRestrictionForDocumentForm(Guid DocumentFormID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.Documents.Where(c => c.DocumentId == DocumentFormID).Select(c => c.DataRestrictionId).FirstOrDefault();
            }
        }

        public void ImportDocumentUsingPlatformAPI(byte[] Document, string FileName)
        {
            base.ImportDocument(Document, FileName);
        }

    }
}
