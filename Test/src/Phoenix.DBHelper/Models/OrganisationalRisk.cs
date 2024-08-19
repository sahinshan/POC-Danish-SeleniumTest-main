using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Phoenix.DBHelper.Models
{
    public class OrganisationalRisk : BaseClass
    {

        public string TableName = "OrganisationalRisk";
        public string PrimaryKeyName = "OrganisationalRiskId";


        public OrganisationalRisk(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateOrganisationalRisk(Guid ownerId, Guid organisationalrisktypeid,
            int Consequences, int Likelihood, DateTime riskidentificationdate, DateTime? NextReviewDate, int ResidualConsequences,
            int ResidualLikelihood, string RiskDescription, Guid? ResponsibleUserId, int CorporateRiskRegisterId,
            int riskstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "organisationalrisktypeid", organisationalrisktypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "Consequences", Consequences);
            AddFieldToBusinessDataObject(buisinessDataObject, "Likelihood", Likelihood);
            AddFieldToBusinessDataObject(buisinessDataObject, "riskidentificationdate", riskidentificationdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "NextReviewDate", NextReviewDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResidualConsequences", ResidualConsequences);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResidualLikelihood", ResidualLikelihood);
            AddFieldToBusinessDataObject(buisinessDataObject, "RiskDescription", RiskDescription);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CorporateRiskRegisterId", CorporateRiskRegisterId);
            AddFieldToBusinessDataObject(buisinessDataObject, "riskstatusid", riskstatusid);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }


        public Guid CreateOrganisationalRiskRecord(Guid ownerId, Guid OwningBusinessUnitId, Guid organisationalrisktypeid,
           int Consequences, int Likelihood, DateTime riskidentificationdate, DateTime? NextReviewDate, int ResidualConsequences,
           int ResidualLikelihood, string RiskDescription, Guid? ResponsibleUserId, int CorporateRiskRegisterId,
           int riskstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "organisationalrisktypeid", organisationalrisktypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "Consequences", Consequences);
            AddFieldToBusinessDataObject(buisinessDataObject, "Likelihood", Likelihood);
            AddFieldToBusinessDataObject(buisinessDataObject, "riskidentificationdate", riskidentificationdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "NextReviewDate", NextReviewDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResidualConsequences", ResidualConsequences);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResidualLikelihood", ResidualLikelihood);
            AddFieldToBusinessDataObject(buisinessDataObject, "RiskDescription", RiskDescription);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CorporateRiskRegisterId", CorporateRiskRegisterId);
            AddFieldToBusinessDataObject(buisinessDataObject, "riskstatusid", riskstatusid);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }


        public Guid CreateOrgRecord(Guid ownerId, Guid OwningBusinessUnitId, int Consequences, int Likelihood, DateTime? NextReviewDate, int ResidualConsequences,
    int ResidualLikelihood, DateTime riskIdentificationdate, string RiskDescription, Guid ResponsibleUserId, int CorporateRiskRegisterId, Guid OrganisationalRiskTypeId, int riskstatusid)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", ownerId);
            AddFieldToBusinessDataObject(businessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(businessDataObject, "Consequences", Consequences);
            AddFieldToBusinessDataObject(businessDataObject, "Likelihood", Likelihood);
            AddFieldToBusinessDataObject(businessDataObject, "NextReviewDate", NextReviewDate);
            AddFieldToBusinessDataObject(businessDataObject, "RiskIdentificationDate", DateTime.Now.ToUniversalTime());
            AddFieldToBusinessDataObject(businessDataObject, "ResidualConsequences", ResidualConsequences);
            AddFieldToBusinessDataObject(businessDataObject, "ResidualLikelihood", ResidualLikelihood);
            AddFieldToBusinessDataObject(businessDataObject, "RiskDescription", RiskDescription);
            AddFieldToBusinessDataObject(businessDataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(businessDataObject, "CorporateRiskRegisterId", CorporateRiskRegisterId);
            AddFieldToBusinessDataObject(businessDataObject, "OrganisationalRiskTypeId", OrganisationalRiskTypeId);
            AddFieldToBusinessDataObject(businessDataObject, "riskIdentificationdate", riskIdentificationdate);
            AddFieldToBusinessDataObject(businessDataObject, "riskstatusid", riskstatusid);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);

            return this.CreateRecord(businessDataObject);
        }

        public List<Guid> GetOrganisationalRiskIdByRiskDescription(string RiskDescription)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RiskDescription", CareWorks.Foundation.Enums.ConditionOperatorType.Contains, RiskDescription);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public List<Guid> GetRiskManagementRecordByRiskDescription(string riskDescription)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "riskDescription", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, riskDescription);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public List<Guid> GetRiskManagementRecordByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", CareWorks.Foundation.Enums.ConditionOperatorType.Contains, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByOrganisationalRiskID(Guid OrganisationalRiskId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, OrganisationalRiskId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public Dictionary<string, object> GetOrganisationRiskByResponsibleUserID(Guid responsibleuserid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, responsibleuserid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
        public void DeleteOrganisationalRisk(Guid OrganisationalRiskId)
        {
            this.DeleteRecord(TableName, OrganisationalRiskId);
        }

        public Guid CreateOrganisationalRisk(Guid ownerId, Guid OwningBusinessUnitId, int Consequences, int Likelihood, DateTime? NextReviewDate, int ResidualConsequences,
    int ResidualLikelihood, string RiskDescription, Guid ResponsibleUserId, int CorporateRiskRegisterId)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", ownerId);
            AddFieldToBusinessDataObject(businessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(businessDataObject, "Consequences", Consequences);
            AddFieldToBusinessDataObject(businessDataObject, "Likelihood", Likelihood);
            AddFieldToBusinessDataObject(businessDataObject, "NextReviewDate", NextReviewDate);
            AddFieldToBusinessDataObject(businessDataObject, "RiskIdentificationDate", DateTime.Now.ToUniversalTime());
            AddFieldToBusinessDataObject(businessDataObject, "ResidualConsequences", ResidualConsequences);
            AddFieldToBusinessDataObject(businessDataObject, "ResidualLikelihood", ResidualLikelihood);
            AddFieldToBusinessDataObject(businessDataObject, "RiskDescription", RiskDescription);
            AddFieldToBusinessDataObject(businessDataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(businessDataObject, "CorporateRiskRegisterId", CorporateRiskRegisterId);
            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false); return this.CreateRecord(businessDataObject);
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
