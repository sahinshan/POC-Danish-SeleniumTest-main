using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class Audit_2019 : BaseClass
    {

        private string tableName = "Audit_2019";
        private string primaryKeyName = "Auditd";

        public Audit_2019()
        {
            AuthenticateUser();
        }

        public Audit_2019(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAudit_2019(string TableName, int Operation, Guid RecordId, string RecordTitle, Guid ApplicationId, DateTime CreatedOn)
        {
            Guid AuditId = Guid.NewGuid();
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                DBHelper.Audit_2019 audit = new DBHelper.Audit_2019()
                {
                    AuditId = AuditId,
                    TableName = TableName,
                    Operation = Operation,
                    RecordId = RecordId,
                    RecordTitle = RecordTitle,
                    ApplicationId = ApplicationId,
                    CreatedOn = CreatedOn
                };

                entity.Audit_2019.Add(audit);
                entity.SaveChanges();

            }
            return AuditId;
        }

        public List<Guid> GetAudit_2019ByRecordID(Guid RecordId)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return (from a in entity.Audit_2019
                        where a.RecordId == RecordId
                        select a.AuditId).ToList();
            }
        }

        public void DeleteAudit_2019(Guid AuditId)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                var recordToDelete = (from a in entity.Audit_2019 where a.AuditId == AuditId select a).FirstOrDefault();

                entity.Audit_2019.Remove(recordToDelete);

                entity.SaveChanges();
            }
        }



    }
}
