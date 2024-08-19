using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CaseAction : BaseClass
    {

        public string TableName = "CaseAction";
        public string PrimaryKeyName = "CaseActionId";


        public CaseAction()
        {
            AuthenticateUser();
        }

        public CaseAction(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCaseAction(Guid OwnerId, Guid PersonId, Guid CaseId, Guid CaseActionTypeId, DateTime DueDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(dataObject, "CaseId", CaseId);
            AddFieldToBusinessDataObject(dataObject, "CaseActionTypeId", CaseActionTypeId);
            AddFieldToBusinessDataObject(dataObject, "DueDate", DueDate);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByHealthAppointmentId(Guid HealthAppointmentId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "HealthAppointmentId", ConditionOperatorType.Equal, HealthAppointmentId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCaseActionByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCaseActionByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCaseActionByID(Guid CaseActionId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseActionId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void RemoveCaseActionRestrictionFromDB(Guid CaseActionID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                var _record = entity.CaseActions.Where(c => c.CaseActionId == CaseActionID).FirstOrDefault();
                _record.DataRestrictionId = null;
                entity.SaveChanges();
            }
        }

        public Guid? GetDataRestrictionForCaseAction(Guid CaseActionID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.CaseActions.Where(c => c.CaseActionId == CaseActionID).Select(x => x.DataRestrictionId).FirstOrDefault();

            }
        }

        public void DeleteCaseAction(Guid CaseActionId)
        {
            this.DeleteRecord(TableName, CaseActionId);
        }
    }
}
