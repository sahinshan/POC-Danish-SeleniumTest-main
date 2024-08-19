using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Phoenix.DBHelper.Models
{
    public class CareproviderReportableEventAction : BaseClass
    {

        public string TableName = "CareproviderReportableEventAction";
        public string PrimaryKeyName = "CareproviderReportableEventActionId";

        public CareproviderReportableEventAction(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareproviderReportableEventActionRecord(Guid responsibleuserid, Guid careproviderreportableeventid, string action, int careproviderreportableeventactionstatusid, DateTime startdate, Guid ownerId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderreportableeventid", careproviderreportableeventid);
            AddFieldToBusinessDataObject(buisinessDataObject, "action", action);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderreportableeventactionstatusid", careproviderreportableeventactionstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByActionName(string action)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "action", CareWorks.Foundation.Enums.ConditionOperatorType.Contains, action);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public List<Guid> GetByReportableEventID(Guid careproviderreportableeventid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careproviderreportableeventid", ConditionOperatorType.Equal, careproviderreportableeventid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public Dictionary<string, object> GetByCareproviderReportableEventID(Guid careproviderreportableeventid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, careproviderreportableeventid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCareproviderReportableEventAction(Guid CareproviderReportableEventActionId)
        {
            this.DeleteRecord(TableName, CareproviderReportableEventActionId);
        }

        public void DeleteCareproviderReportableEventAction_AdoNetDirectConnection(Guid CareproviderReportableEventActionId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from CareproviderReportableEventAction where CareproviderReportableEventActionId = @UserAddressID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserAddressID", CareproviderReportableEventActionId);

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
