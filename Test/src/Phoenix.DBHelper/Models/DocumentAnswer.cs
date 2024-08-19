using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class DocumentAnswer : BaseClass
    {

        private string tableName = "DocumentAnswer";
        private string primaryKeyName = "DocumentAnswerId";

        public DocumentAnswer()
        {
            AuthenticateUser();
        }

        public DocumentAnswer(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDateTimeDocumentAnswer(Guid assessmentid, Guid documentquestionidentifierid,
            DateTime dateanswer, string assessmentidtablename, int gridrowid)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "assessmentid", assessmentid);
            AddFieldToBusinessDataObject(dataObject, "documentquestionidentifierid", documentquestionidentifierid);
            AddFieldToBusinessDataObject(dataObject, "dateanswer", dateanswer);
            AddFieldToBusinessDataObject(dataObject, "assessmentidtablename", assessmentidtablename);
            AddFieldToBusinessDataObject(dataObject, "truefalseanswer", false);
            AddFieldToBusinessDataObject(dataObject, "usersaved", true);
            AddFieldToBusinessDataObject(dataObject, "gridrowid", gridrowid);
            AddFieldToBusinessDataObject(dataObject, "questiontypeid", 7);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreatePicklistDocumentAnswer(Guid assessmentid, Guid documentquestionidentifierid, Guid picklistvalueid, string assessmentidtablename, int gridrowid)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "assessmentid", assessmentid);
            AddFieldToBusinessDataObject(dataObject, "documentquestionidentifierid", documentquestionidentifierid);
            AddFieldToBusinessDataObject(dataObject, "picklistvalueid", picklistvalueid);
            AddFieldToBusinessDataObject(dataObject, "assessmentidtablename", assessmentidtablename);
            AddFieldToBusinessDataObject(dataObject, "gridrowid", gridrowid);
            AddFieldToBusinessDataObject(dataObject, "questiontypeid", 14);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "truefalseanswer", false);
            AddFieldToBusinessDataObject(dataObject, "usersaved", true);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetDocumentAnswer(Guid AssessmentId, Guid DocumentQuestionIdentifierId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "AssessmentId", ConditionOperatorType.Equal, AssessmentId);
            this.BaseClassAddTableCondition(query, "DocumentQuestionIdentifierId", ConditionOperatorType.Equal, DocumentQuestionIdentifierId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetDocumentAnswer(String AssessmentIdTableName, Guid DocumentQuestionIdentifierId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "AssessmentIdTableName", ConditionOperatorType.Equal, AssessmentIdTableName);
            this.BaseClassAddTableCondition(query, "DocumentQuestionIdentifierId", ConditionOperatorType.Equal, DocumentQuestionIdentifierId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetDocumentAnswerByID(Guid DocumentAnswerId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DocumentAnswerId", ConditionOperatorType.Equal, DocumentAnswerId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid? GetDocumentAnswerImageID(Guid AssessmentID, string DocumentQuestionIdentifier)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return (from da in entity.DocumentAnswers
                        where da.AssessmentId == AssessmentID
                        && da.DocumentQuestionIdentifier.Identifier == DocumentQuestionIdentifier
                        select da.ImageId).FirstOrDefault();
            }
        }

        public void UpdateShortAnswer(Guid DocumentAnswerId, string ShortAnswer)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentAnswerId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ShortAnswer", ShortAnswer);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDateAnswer(Guid DocumentAnswerId, DateTime DateAnswer)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentAnswerId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "DateAnswer", DateAnswer);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDateAnswer(Guid DocumentAnswerId, DateTime DateAnswer, int GridRowId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentAnswerId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "DateAnswer", DateAnswer);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "GridRowId", GridRowId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDecimalAnswer(Guid DocumentAnswerId, decimal DecimalAnswer)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentAnswerId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "DecimalAnswer", DecimalAnswer);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateMultichoiceAnswer(Guid DocumentAnswerId, Guid MultichoiceAnswerId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentAnswerId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "MultichoiceAnswerId", MultichoiceAnswerId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateNumericAnswer(Guid DocumentAnswerId, int NumericAnswer)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentAnswerId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "NumericAnswer", NumericAnswer);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateParagraphAnswer(Guid DocumentAnswerId, string ParagraphAnswer)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentAnswerId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ParagraphAnswer", ParagraphAnswer);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePicklistValueAnswer(Guid DocumentAnswerId, Guid PicklistValueId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentAnswerId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PicklistValueId", PicklistValueId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePicklistValueAnswer(Guid DocumentAnswerId, Guid PicklistValueId, int GridRowId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentAnswerId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PicklistValueId", PicklistValueId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "GridRowId", GridRowId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateTrueFalseAnswer(Guid DocumentAnswerId, bool? TrueFalseAnswer)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentAnswerId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "TrueFalseAnswer", TrueFalseAnswer);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateLookupAnswer(Guid DocumentAnswerId, Guid LookupObjectId, string LookupObjectTypeName, string LookupObjectTitle)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentAnswerId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "LookupObjectId", LookupObjectId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "LookupObjectTypeName", LookupObjectTypeName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "LookupObjectTitle", LookupObjectTitle);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDateTimeAnswer(Guid DocumentAnswerId, DateTime DateAndTimeAnswer)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentAnswerId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "DateAndTimeAnswer", DateAndTimeAnswer);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
