using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class Audit : BaseClass
    {

        public string tableName = "Audit";
        public string primaryKeyName = "AuditId";

        public Audit()
        {
            AuthenticateUser();
        }

        public Audit(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAudit(string TableName, int Operation, Guid RecordId, string RecordTitle, Guid ApplicationId, DateTime CreatedOn)
        {
            Guid userId = new Guid("2d7c765e-23d3-ea11-a2cd-005056926fe4"); //Security Test User 
            Guid AuditId = Guid.NewGuid();
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                DBHelper.Audit audit = new DBHelper.Audit()
                {
                    AuditId = AuditId,
                    TableName = TableName,
                    Operation = Operation,
                    RecordId = RecordId,
                    RecordTitle = RecordTitle,
                    ApplicationId = ApplicationId,
                    CreatedOn = CreatedOn,
                    CreatedBy = userId
                };

                entity.Audits.Add(audit);
                entity.SaveChanges();

            }
            return AuditId;
        }

        public List<Guid> GetAuditByRecordID(Guid RecordId)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return (from a in entity.Audits
                        where a.RecordId == RecordId
                        select a.AuditId).ToList();
            }
        }

        public List<Guid> GetByCreatedByID(Guid SystemUserID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return (from a in entity.Audits
                        where
                        a.CreatedBy == SystemUserID
                        select a.AuditId).ToList();
            }
        }

        public Dictionary<string, object> GetByID(Guid AuditID, params string[] Fields)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();


            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareDirectorQA_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "select * from [CareDirectorQA_CD].[DBO].[Audit] a where a.AuditID = @AuditID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@AuditID", AuditID);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    reader.Read();

                    foreach (var fieldToAdd in Fields)
                    {
                        dataToReturn.Add(fieldToAdd, reader[fieldToAdd]);
                    }

                }
                finally
                {
                    reader.Close();
                }
            }

            return dataToReturn;
        }

        public void DeleteAudit(Guid AuditID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                var recordToDelete = (from a in entity.Audits where a.AuditId == AuditID select a).FirstOrDefault();

                entity.Audits.Remove(recordToDelete);

                entity.SaveChanges();
            }
        }

        public List<Guid> GetByCreatedByID(string creadterid)
        {
            throw new NotImplementedException();
        }

        public object GetByCreatedByID(object createrid)
        {
            throw new NotImplementedException();
        }
    }
}
