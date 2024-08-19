using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Phoenix.DBHelper.Models
{
    public class CareproviderReportableEventType : BaseClass
    {

        public string TableName = "CareproviderReportableEventType";
        public string PrimaryKeyName = "CareproviderReportableEventTypeId";


        public CareproviderReportableEventType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);

        }

        public Guid CreateCareproviderReportableEventTypeRecord(string name, DateTime startdate, Guid ownerid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteCareproviderReportableEventType(Guid CareproviderReportableEventTypeId)
        {
            this.DeleteRecord(TableName, CareproviderReportableEventTypeId);
        }

        public void DeleteCareproviderReportableEventType_AdoNetDirectConnection(Guid CareproviderReportableEventTypeId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from CareproviderReportableEventType where CareproviderReportableEventTypeId = @UserAddressID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserAddressID", CareproviderReportableEventTypeId);

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
