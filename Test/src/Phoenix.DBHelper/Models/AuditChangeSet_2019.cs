using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class AuditChangeSet_2019 : BaseClass
    {

        private string tableName = "AuditChangeSet_2019";
        private string primaryKeyName = "AuditChangeSetId";

        public AuditChangeSet_2019()
        {
            AuthenticateUser();
        }

        public AuditChangeSet_2019(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAuditChangeSet_2019(Guid AuditId, string FieldName, string NewValue, string PreviousValue, bool? PreviousBooleanValue, bool? NewBooleanValue, DateTime? PreviousDateValue, DateTime? NewDateValue,
            decimal? PreviousDecimalValue, decimal? NewDecimalValue, int? PreviousIntValue, int? NewIntValue, Guid? PreviousGuidValue, Guid? NewGuidValue, DateTime? PreviousDateTimeValue, DateTime? NewDateTimeValue,
            TimeSpan? PreviousTimeValue, TimeSpan? NewTimeValue, int FieldType, string PreviousChecklistValue, string NewChecklistValue)
        {
            Guid AuditChangeSetId = Guid.NewGuid();
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                DBHelper.AuditChangeSet_2019 acs = new DBHelper.AuditChangeSet_2019()
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


                entity.AuditChangeSet_2019.Add(acs);


                entity.SaveChanges();

            }
            return AuditChangeSetId;
        }

        public List<Guid> GetAuditChangeSet_2019ByAuditID(Guid AuditId)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return (from acs in entity.AuditChangeSet_2019
                        where acs.AuditId == AuditId
                        select acs.AuditChangeSetId).ToList();
            }
        }


        public void DeleteAuditChangeSet_2019(Guid AuditChangeSetID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                var recordToDelete = (from a in entity.AuditChangeSet_2019 where a.AuditChangeSetId == AuditChangeSetID select a).FirstOrDefault();

                entity.AuditChangeSet_2019.Remove(recordToDelete);

                entity.SaveChanges();
            }
        }



    }
}
