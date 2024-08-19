using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CaseForm : BaseClass
    {

        public string TableName = "CaseForm";
        public string PrimaryKeyName = "CaseFormId";


        public CaseForm()
        {
            AuthenticateUser();
        }

        public CaseForm(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCaseFormRecord(Guid OwnerId, Guid DocumentId, Guid PersonId, Guid CaseId, DateTime StartDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "DocumentId", DocumentId);
            AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CaseId", CaseId);

            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(buisinessDataObject, "AssessmentStatusId", 1);

            AddFieldToBusinessDataObject(buisinessDataObject, "SDEExecuted", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "AnswersInitialized", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "JointCarerAssessment", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "SeparateAssessment", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "CarerDeclinedJointAssessment", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "NewPerson", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateCaseFormRecord(Guid OwnerId, Guid DocumentId, Guid PersonId, Guid CaseId, DateTime StartDate, int AssessmentStatusId,
            Guid JointCarerId, bool SeparateAssessment = false, bool JointCarerAssessment = false, bool CarerDeclinedJointAssessment = false,
            bool SDEStatusExecuted = false, bool AnswersInitialized = false, bool NewPerson = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "DocumentId", DocumentId);
            AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CaseId", CaseId);

            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(buisinessDataObject, "AssessmentStatusId", AssessmentStatusId);

            AddFieldToBusinessDataObject(buisinessDataObject, "SDEExecuted", SDEStatusExecuted);
            AddFieldToBusinessDataObject(buisinessDataObject, "AnswersInitialized", AnswersInitialized);
            AddFieldToBusinessDataObject(buisinessDataObject, "JointCarerAssessment", JointCarerAssessment);
            AddFieldToBusinessDataObject(buisinessDataObject, "SeparateAssessment", SeparateAssessment);
            AddFieldToBusinessDataObject(buisinessDataObject, "CarerDeclinedJointAssessment", CarerDeclinedJointAssessment);
            AddFieldToBusinessDataObject(buisinessDataObject, "NewPerson", NewPerson);
            AddFieldToBusinessDataObject(buisinessDataObject, "JointCarerId", JointCarerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateCaseForm(Guid ownerid, Guid personid, string PersonName, Guid responsibleuserid, Guid caseid, string CaseName,
            Guid documentid, string documentidName, int assessmentstatusid, DateTime startdate, DateTime? duedate, DateTime? reviewdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid_cwname", PersonName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "caseid", caseid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caseid_cwname", CaseName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "documentid", documentid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "documentid_cwname", documentidName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "assessmentstatusid", assessmentstatusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);

            if (duedate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "duedate", duedate.Value);

            if (reviewdate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "reviewdate", reviewdate.Value);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "sdeexecuted", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "answersinitialized", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "jointcarerassessment", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "separateassessment", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "carerdeclinedjointassessment", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "newperson", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateCaseFormRecord(Guid OwnerId, Guid DocumentId, Guid PersonId, Guid CaseId, DateTime StartDate, bool JointCarerAssessment)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "DocumentId", DocumentId);
            AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CaseId", CaseId);

            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(buisinessDataObject, "AssessmentStatusId", 1);

            AddFieldToBusinessDataObject(buisinessDataObject, "SDEExecuted", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "AnswersInitialized", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "JointCarerAssessment", JointCarerAssessment);
            AddFieldToBusinessDataObject(buisinessDataObject, "SeparateAssessment", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "CarerDeclinedJointAssessment", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "NewPerson", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetCaseFormByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCaseFormByCaseID(Guid CaseID, DateTime startdate)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.GreaterEqual, startdate.Date);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.LessEqual, startdate.Date.AddHours(23).AddMinutes(59));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCaseFormByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCaseFormsByCaseAndFormType(Guid CaseID, Guid FormTypeID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);
            this.BaseClassAddTableCondition(query, "DocumentId", ConditionOperatorType.Equal, FormTypeID);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);


        }

        public List<Guid> GetCaseFormsByCaseAndFormType(Guid CaseID, Guid FormTypeID, DateTime StartDate)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);
            this.BaseClassAddTableCondition(query, "DocumentId", ConditionOperatorType.Equal, FormTypeID);
            this.BaseClassAddTableCondition(query, "StartDate", ConditionOperatorType.Equal, StartDate);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Dictionary<string, object> GetCaseFormByID(Guid CaseFormId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseFormId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void RemoveCaseFormRestrictionFromDB(Guid CaseFormID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                var _record = entity.CaseForms.Where(c => c.CaseFormId == CaseFormID).FirstOrDefault();
                _record.DataRestrictionId = null;
                entity.SaveChanges();
            }
        }

        public Guid? GetDataRestrictionForCaseForm(Guid CaseFormID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.CaseForms.Where(c => c.CaseFormId == CaseFormID).Select(c => c.DataRestrictionId).FirstOrDefault();
            }
        }

        public void UpdateStatus(Guid CaseFormId, int assessmentstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseFormId);

            AddFieldToBusinessDataObject(buisinessDataObject, "assessmentstatusid", assessmentstatusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCaseFormRecord(Guid CaseFormID, DateTime StartDate, bool SeparateAssessment)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseFormID);

            AddFieldToBusinessDataObject(buisinessDataObject, "CaseFormId", CaseFormID);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "SeparateAssessment", SeparateAssessment);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCaseFormRecord(Guid CaseFormID, DateTime StartDate, DateTime duedate, DateTime reviewdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "CaseFormId", CaseFormID);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "duedate", duedate);
            AddFieldToBusinessDataObject(buisinessDataObject, "reviewdate", reviewdate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteCaseForm(Guid CaseFormId)
        {
            this.DeleteRecord(TableName, CaseFormId);
        }
    }
}
