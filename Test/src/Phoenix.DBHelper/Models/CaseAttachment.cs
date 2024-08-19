using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CaseAttachment : BaseClass
    {

        public string TableName = "CaseAttachment";
        public string PrimaryKeyName = "CaseAttachmentId";


        public CaseAttachment()
        {
            AuthenticateUser();
        }

        public CaseAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetCaseAttachmentByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCaseAttachmentByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCaseAttachmentByID(Guid CaseAttachmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseAttachmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void RemoveCaseAttachmentRestrictionFromDB(Guid CaseAttachmentID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                var _record = entity.CaseAttachments.Where(c => c.CaseAttachmentId == CaseAttachmentID).FirstOrDefault();
                _record.DataRestrictionId = null;
                entity.SaveChanges();
            }
        }

        public Guid? GetDataRestrictionForCaseAttachment(Guid CaseAttachmentID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.CaseAttachments.Where(c => c.CaseAttachmentId == CaseAttachmentID).Select(x => x.DataRestrictionId).FirstOrDefault();

            }
        }

        public void DeleteCaseAttachment(Guid CaseAttachmentId)
        {
            this.DeleteRecord(TableName, CaseAttachmentId);
        }
    }
}
