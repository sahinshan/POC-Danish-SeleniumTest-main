using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class AuditChangeSet : BaseClass
    {

        private string tableName = "AuditChangeSet";
        private string primaryKeyName = "AuditChangeSetId";

        public AuditChangeSet()
        {
            AuthenticateUser();
        }

        public AuditChangeSet(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAuditChangeSet(Guid AuditId, string FieldName, string NewValue, string PreviousValue, bool? PreviousBooleanValue, bool? NewBooleanValue, DateTime? PreviousDateValue, DateTime? NewDateValue,
            decimal? PreviousDecimalValue, decimal? NewDecimalValue, int? PreviousIntValue, int? NewIntValue, Guid? PreviousGuidValue, Guid? NewGuidValue, DateTime? PreviousDateTimeValue, DateTime? NewDateTimeValue,
            TimeSpan? PreviousTimeValue, TimeSpan? NewTimeValue, int FieldType, string PreviousChecklistValue, string NewChecklistValue)
        {
            Guid AuditChangeSetId = Guid.NewGuid();
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                DBHelper.AuditChangeSet acs = new DBHelper.AuditChangeSet()
                {
                    AuditChangeSetId = AuditChangeSetId,
                    AuditId = AuditId,
                    FieldName = FieldName,
                    NewValue = NewValue,
                    PreviousValue = PreviousValue,
                    PreviousBooleanValue = PreviousBooleanValue,
                    NewBooleanValue = NewBooleanValue,
                    PreviousDateValue = PreviousDateValue,
                    NewDateValue = NewDateValue,
                    PreviousDecimalValue = PreviousDecimalValue,
                    NewDecimalValue = NewDecimalValue,
                    PreviousIntValue = PreviousIntValue,
                    NewIntValue = NewIntValue,
                    PreviousGuidValue = PreviousGuidValue,
                    NewGuidValue = NewGuidValue,
                    PreviousDateTimeValue = PreviousDateTimeValue,
                    NewDateTimeValue = NewDateTimeValue,
                    PreviousTimeValue = PreviousTimeValue,
                    NewTimeValue = NewTimeValue,
                    FieldType = FieldType,
                    PreviousChecklistValue = PreviousChecklistValue,
                    NewChecklistValue = NewChecklistValue
                };


                entity.AuditChangeSets.Add(acs);


                entity.SaveChanges();

            }
            return AuditChangeSetId;

        }

        public List<Guid> GetAuditChangeSetByAuditID(Guid AuditId)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return (from acs in entity.AuditChangeSets
                        where acs.AuditId == AuditId
                        select acs.AuditChangeSetId).ToList();
            }
        }



        public void DeleteAuditChangeSet(Guid AuditChangeSetID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                var recordToDelete = (from a in entity.AuditChangeSets where a.AuditChangeSetId == AuditChangeSetID select a).FirstOrDefault();

                entity.AuditChangeSets.Remove(recordToDelete);

                entity.SaveChanges();
            }
        }



    }
}
