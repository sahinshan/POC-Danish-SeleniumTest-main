using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Phoenix.DBHelper.Models
{
    public class CareproviderReportableEventImpact : BaseClass
    {

        public string TableName = "CareProviderReportableEventImpact";
        public string PrimaryKeyName = "CareproviderReportableEventImpactId";

        public CareproviderReportableEventImpact(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareproviderReportableEventImpactRecord(Guid reportableeventid, int impacttypeid, Guid internalpersonorganisationid, Guid careproviderreportableeventseverityid, DateTime createdon, Guid ownerId, string internalpersonorganisationidtablename, string internalpersonorganisationidname, Guid roleineventid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "reportableeventid", reportableeventid);
            AddFieldToBusinessDataObject(buisinessDataObject, "impacttypeid", impacttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "internalpersonorganisationid", internalpersonorganisationid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", createdon);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "internalpersonorganisationidtablename", internalpersonorganisationidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "internalpersonorganisationidname", internalpersonorganisationidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderreportableeventseverityid", careproviderreportableeventseverityid);
            AddFieldToBusinessDataObject(buisinessDataObject, "roleineventid", roleineventid);

            AddFieldToBusinessDataObject(buisinessDataObject, "isexternalpersonorganisation", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByImpactName(int impacttypeid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            // query.Orders.Add(new OrderBy("impacttypeid", sortOrder.Descending, TableName));
            this.BaseClassAddTableCondition(query, "impacttypeid", CareWorks.Foundation.Enums.ConditionOperatorType.Contains, impacttypeid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public Dictionary<string, object> GetByReportableEventID(Guid reportableeventid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, reportableeventid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



        public void DeleteCareproviderReportableEventImpact(Guid CareproviderReportableEventImpactId)
        {
            this.DeleteRecord(TableName, CareproviderReportableEventImpactId);
        }



        public void DeleteCareproviderReportableEventImpact_AdoNetDirectConnection(Guid CareproviderReportableEventImpactId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from CareproviderReportableEventAction where CareproviderReportableEventActionId = @UserAddressID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserAddressID", CareproviderReportableEventImpactId);

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
