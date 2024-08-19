using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonForm : BaseClass
    {

        private string tableName = "PersonForm";
        private string primaryKeyName = "PersonFormId";

        public PersonForm()
        {
            AuthenticateUser();
        }

        public PersonForm(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonForm(Guid OwnerId, Guid PersonId, Guid DocumentId, DateTime StartDate, int AssessmentStatusId = 1)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(dataObject, "DocumentId", DocumentId);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "AssessmentStatusId", 1);
            AddFieldToBusinessDataObject(dataObject, "SDEExecuted", false);
            AddFieldToBusinessDataObject(dataObject, "AnswersInitialized", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);


            return this.CreateRecord(dataObject);
        }


        public Guid CreatePersonForm(Guid OwnerId, Guid PersonId, Guid DocumentId, DateTime StartDate, bool createdonproviderportal, int AssessmentStatusId = 1)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(dataObject, "DocumentId", DocumentId);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "AssessmentStatusId", 1);
            AddFieldToBusinessDataObject(dataObject, "SDEExecuted", false);
            AddFieldToBusinessDataObject(dataObject, "AnswersInitialized", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "createdonproviderportal", createdonproviderportal);


            return this.CreateRecord(dataObject);
        }


        public void UpdatePersonForm(Guid PersonFormId, Guid? precedingformid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonFormId);

            AddFieldToBusinessDataObject(buisinessDataObject, "precedingformid", precedingformid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePersonFormWithCreatedOnPortal(Guid PersonFormId, bool createdonproviderportal)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonFormId);

            AddFieldToBusinessDataObject(buisinessDataObject, "createdonproviderportal", createdonproviderportal);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePersonFormStatus(Guid PersonFormID, int assessmentstatusid, Guid? FormCancellationReasonId = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonFormID);

            AddFieldToBusinessDataObject(buisinessDataObject, "assessmentstatusid", assessmentstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "FormCancellationReasonId", FormCancellationReasonId);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonFormByPersonID(Guid PersonId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetPersonFormByPersonIDAndFormType(Guid PersonId, Guid DocumentId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);
            this.BaseClassAddTableCondition(query, "DocumentId", ConditionOperatorType.Equal, DocumentId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetPersonFormByID(Guid PersonFormId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, PersonFormId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonForm(Guid PersonFormID)
        {
            this.DeleteRecord(tableName, PersonFormID);
        }



    }
}
