using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Phoenix.DBHelper.Models
{
    public class OrganisationalRiskActionPlan : BaseClass
    {

        private string tableName = "OrganisationalRiskActionPlan";
        private string primaryKeyName = "OrganisationalRiskActionPlanId";

        public OrganisationalRiskActionPlan()
        {
            AuthenticateUser();
        }

        public OrganisationalRiskActionPlan(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByTitle(string title)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, title);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }
        public List<Guid> GetByOrganisationalRiskId(Guid organisationalriskid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "organisationalriskid", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, organisationalriskid);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetOrganisationalRiskIdByTitle(string Title)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Title", CareWorks.Foundation.Enums.ConditionOperatorType.Like, Title);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetOrganisationalRiskActionPlanByID(Guid OrganisationalRiskActionPlanId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, OrganisationalRiskActionPlanId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }
        public Guid CreateOrganisationalRiskActionPlan(Guid OwnerId, Guid ResponsibleUserId, Guid OwningBusinessUnitId, string Title, Guid OrganisationalRiskId, int StatusForActionId, string DescriptionOfActionPlan,
            DateTime? NextReviewDate)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Title", Title);
            AddFieldToBusinessDataObject(dataObject, "StatusForActionId", StatusForActionId);
            AddFieldToBusinessDataObject(dataObject, "DescriptionOfActionPlan", DescriptionOfActionPlan);
            AddFieldToBusinessDataObject(dataObject, "OrganisationalRiskId", OrganisationalRiskId);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(dataObject, "NextReviewDate", NextReviewDate);
            AddFieldToBusinessDataObject(dataObject, "OwningBusinessUnitId", OwningBusinessUnitId);

            return this.CreateRecord(dataObject);
        }
        public void DeleteOrganisationalRiskActionPlan(Guid organisationalriskid)
        {
            this.DeleteRecord(tableName, organisationalriskid);
        }
        public void DeleteOrganisationalRisk_AdoNetDirectConnection(Guid OrganisationalRiskId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from OrganisationalRisk where OrganisationalRiskId = @UserAddressID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserAddressID", OrganisationalRiskId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
        }


    }
}
