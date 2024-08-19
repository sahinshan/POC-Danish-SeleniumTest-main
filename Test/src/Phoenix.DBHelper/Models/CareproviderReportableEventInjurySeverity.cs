using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CareproviderReportableEventInjurySeverity : BaseClass
    {

        public string TableName = "CareproviderReportableEventInjurySeverity";
        public string PrimaryKeyName = "CareproviderReportableEventInjurySeverityId";


        public CareproviderReportableEventInjurySeverity(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);

        }

        public Guid CreateCareproviderReportableEventInjurySeverityRecord(string name, DateTime startdate, Guid TeamId, bool Inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", TeamId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateCareproviderReportableEventInjurySeverityRecord(string name, DateTime startdate, Guid TeamId, bool Inactive, DateTime enddate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", TeamId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetIdByName(string name)
        {

            var query = new DataQuery(TableName, true, PrimaryKeyName);
            query.PrimaryKeyName = PrimaryKeyName;

            query.Filter.AddCondition(TableName, "name", ConditionOperatorType.Equal, name);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
            {

                return response.BusinessDataCollection.Select(c => Guid.Parse(c.FieldCollection[PrimaryKeyName].ToString())).ToList();
            }
            else
            {
                return new List<Guid>();
            }

        }

        public void DeleteCareproviderReportableEventInjurySeverity(Guid careproviderreportableeventinjuryseverityid)
        {
            this.DeleteRecord(TableName, careproviderreportableeventinjuryseverityid);
        }

        public void DeleteCareproviderReportableEventInjurySeverity_AdoNetDirectConnection(Guid careproviderreportableeventinjuryseverityid)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from CareproviderReportableEventSeverity where CareproviderReportableEventSeverityId = @UserAddressID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserAddressID", careproviderreportableeventinjuryseverityid);

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
