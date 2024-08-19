using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Phoenix.DBHelper.Models
{
    public class Automated_UI_Test_Document_1 : BaseClass
    {

        public Automated_UI_Test_Document_1()
        {
            AuthenticateUser();
        }

        public Automated_UI_Test_Document_1(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public int CountReportDataByAssessmentId(Guid AssessmentID)
        {
            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareDirectorQA_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "select count (*) as total from [CareDirectorQA_CD].[CDR].[Automated_UI_Test_Document_1] a where a.AssessmentId = @AssessmentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@AssessmentId", AssessmentID);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        return (int)reader["total"];
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return -1;
        }


        public Dictionary<string, object> GetReportDataByAssessmentId(Guid AssessmentID)
        {
            var dataToReturn = new Dictionary<string, object>();


            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareDirectorQA_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = @"select top 1 [AssessmentId]
           ,[LastUpdated]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[ModifiedBy]
           ,[ModifiedOn]
           ,[OwnerId]
           ,[OwningBusinessUnitId]
           ,[QA-DQ-163]
           ,[QA-DQ-164]
           ,[QA-DQ-168]
           ,[QA-DQ-168Name]
           ,[QA-DQ-169]
           ,[QA-DQ-169Name]
           ,[QA-DQ-170]
           ,[QA-DQ-170Name]
           ,[QA-DQ-171]
           ,[QA-DQ-172]
           ,[QA-DQ-173]
           ,[QA-DQ-173Name]
           ,[QA-DQ-174]
           ,[QA-DQ-185]
           ,[QA-DQ-186]
           ,[QA-DQ-188]
           ,[QA-DQ-253]
           ,[QA-DQ-254]
           ,[QA-DQ-255]
           ,[QA-DQ-256]
           ,[QA-DQ-258]
           ,[QA-DQ-260]
           ,[QA-DQ-262]
           ,[SpecialPurposeColumns] 
from [CareDirectorQA_CD].[CDR].[Automated_UI_Test_Document_1] a 
where a.AssessmentId = @AssessmentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@AssessmentId", AssessmentID);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        dataToReturn.Add("QA-DQ-163", reader["QA-DQ-163"]);
                        dataToReturn.Add("QA-DQ-164", reader["QA-DQ-164"]);
                        dataToReturn.Add("QA-DQ-168", reader["QA-DQ-168"]);
                        dataToReturn.Add("QA-DQ-168Name", reader["QA-DQ-168Name"]);
                        dataToReturn.Add("QA-DQ-169", reader["QA-DQ-169"]);
                        dataToReturn.Add("QA-DQ-169Name", reader["QA-DQ-169Name"]);
                        dataToReturn.Add("QA-DQ-170", reader["QA-DQ-170"]);
                        dataToReturn.Add("QA-DQ-170Name", reader["QA-DQ-170Name"]);
                        dataToReturn.Add("QA-DQ-171", reader["QA-DQ-171"]);
                        dataToReturn.Add("QA-DQ-172", reader["QA-DQ-172"]);
                        dataToReturn.Add("QA-DQ-173", reader["QA-DQ-173"]);
                        dataToReturn.Add("QA-DQ-173Name", reader["QA-DQ-173Name"]);
                        dataToReturn.Add("QA-DQ-174", reader["QA-DQ-174"]);
                        dataToReturn.Add("QA-DQ-186", reader["QA-DQ-186"]);
                        dataToReturn.Add("QA-DQ-188", reader["QA-DQ-188"]);

                        return dataToReturn;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return null;
        }


        public int CountWFTableWithUnlimitedRowsByAssessmentId(Guid AssessmentID)
        {
            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareDirectorQA_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "select count (*) as total from [CareDirectorQA_CD].[CDR].[Automated_UI_Test_Document_1QA-DQ-244] a where a.AssessmentId = @AssessmentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@AssessmentId", AssessmentID);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        return (int)reader["total"];
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return -1;
        }

        public Dictionary<string, object> GetWFTableWithUnlimitedRowsByAssessmentAndRowId(Guid AssessmentID, int RowID)
        {
            var dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareDirectorQA_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = @"select top 1 [AssessmentId]
           ,[RowId]
           ,[QA-DQ-245]
           ,[QA-DQ-246]
           ,[QA-DQ-246Name]
           ,[SpecialPurposeColumns] 
from [CareDirectorQA_CD].[CDR].[Automated_UI_Test_Document_1QA-DQ-244] a 
where a.AssessmentId = @AssessmentId and a.RowID = @RowID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@AssessmentId", AssessmentID);
                command.Parameters.AddWithValue("@RowID", RowID);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        dataToReturn.Add("QA-DQ-245", reader["QA-DQ-245"]);
                        dataToReturn.Add("QA-DQ-246", reader["QA-DQ-246"]);
                        dataToReturn.Add("QA-DQ-246Name", reader["QA-DQ-246Name"]);

                        return dataToReturn;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return null;
        }


    }
}
