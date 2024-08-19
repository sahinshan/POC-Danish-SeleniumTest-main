using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class TeamMember : BaseClass
    {

        private string tableName = "TeamMember";
        private string primaryKeyName = "TeamMemberId";

        public TeamMember()
        {
            AuthenticateUser();
        }

        public TeamMember(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateTeamMember(Guid TeamId, Guid SystemUserId, DateTime StartDate, DateTime? EndDate)
        {

            BusinessData record = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(record, "TeamId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, TeamId);
            this.AddFieldToBusinessDataObject(record, "SystemUserId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, SystemUserId);

            this.AddFieldToBusinessDataObject(record, "StartDate", DataType.Date, BusinessObjectFieldType.Unknown, false, StartDate);
            if (EndDate.HasValue)
            {
                this.AddFieldToBusinessDataObject(record, "EndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, EndDate);
                this.AddFieldToBusinessDataObject(record, "inactive", DataType.Boolean, BusinessObjectFieldType.Unknown, false, true);
            }


            return this.CreateRecord(record);
        }

        public List<Guid> GetTeamMemberByUserAndTeamID(Guid SystemUserId, Guid TeamId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", ConditionOperatorType.Equal, SystemUserId);
            this.BaseClassAddTableCondition(query, "TeamId", ConditionOperatorType.Equal, TeamId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }



        public Dictionary<string, object> GetTeamMemberByID(Guid TeamMemberId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "TeamMemberId", ConditionOperatorType.Equal, TeamMemberId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteTeamMember(Guid TeamMemberID)
        {
            this.DeleteRecord(tableName, TeamMemberID);
        }

        public void DeleteTeamMember_AdoNetDirectConnection(Guid TeamMemberId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from TeamMember where TeamMemberId = @TeamMemberID";


            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@TeamMemberID", TeamMemberId);

                try
                {
                    connection.Open();
                    var count = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }



    }
}
