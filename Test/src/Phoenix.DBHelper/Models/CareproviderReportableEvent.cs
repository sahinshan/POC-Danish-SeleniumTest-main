using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Phoenix.DBHelper.Models
{
    public class CareproviderReportableEvent : BaseClass
    {

        public string TableName = "CareproviderReportableEvent";
        public string PrimaryKeyName = "CareproviderReportableEventId";


        public CareproviderReportableEvent(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareproviderReportableEventRecord(Guid responsibleuserid, Guid CareproviderReportableEventtypeid, Guid CareproviderReportableEventseverityid, Guid CareproviderReportableEventstatusid, DateTime statuschangedon, Guid statuschangedbyid, Guid ownerId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareproviderReportableEventtypeid", CareproviderReportableEventtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareproviderReportableEventseverityid", CareproviderReportableEventseverityid);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareproviderReportableEventstatusid", CareproviderReportableEventstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "statuschangedon", statuschangedon);
            AddFieldToBusinessDataObject(buisinessDataObject, "statuschangedbyid", statuschangedbyid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateCareproviderReportableEventInactiveRecord(Guid responsibleuserid, Guid CareproviderReportableEventtypeid, Guid CareproviderReportableEventseverityid, Guid CareproviderReportableEventstatusid, DateTime statuschangedon, Guid statuschangedbyid, Guid ownerId, DateTime Enddate, DateTime Startdate)

        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareproviderReportableEventtypeid", CareproviderReportableEventtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareproviderReportableEventseverityid", CareproviderReportableEventseverityid);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareproviderReportableEventstatusid", CareproviderReportableEventstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "statuschangedon", statuschangedon);
            AddFieldToBusinessDataObject(buisinessDataObject, "statuschangedbyid", statuschangedbyid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Enddate", Enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "Startdate", Startdate);

            return this.CreateRecord(buisinessDataObject);
        }
        public List<Guid> GetByResponsibleUserId(Guid responsibleuserid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "responsibleuserid", CareWorks.Foundation.Enums.ConditionOperatorType.Contains, responsibleuserid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetInactiveRecordsByResponsibleUserId(Guid responsibleuserid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "responsibleuserid", CareWorks.Foundation.Enums.ConditionOperatorType.Contains, responsibleuserid);
            this.BaseClassAddTableCondition(query, "inactive", ConditionOperatorType.Equal, true);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public Dictionary<string, object> GetByCareproviderReportableEventID(Guid CareproviderReportableEventId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareproviderReportableEventId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCareproviderReportableEvent(Guid CareproviderReportableEventId)
        {
            this.DeleteRecord(TableName, CareproviderReportableEventId);
        }

        public void DeleteCareproviderReportableEvent_AdoNetDirectConnection(Guid CareproviderReportableEventId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from CareproviderReportableEvent where CareproviderReportableEventId = @UserAddressID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserAddressID", CareproviderReportableEventId);

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

        public List<Guid> GetInactiveRecords()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            //want all the records where inactive records set to true
            this.BaseClassAddTableCondition(query, "Inactive", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, true);
            //results to be ordered by identifier column in ascending order.
            query.Orders.Add(new OrderBy("Identifier", CareWorks.Foundation.Enums.SortOrder.Descending, TableName));
            //returns primary key for the tabke specified.
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public List<Guid> GetAllReportableEventsOrderDescending()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);



            query.Orders.Add(new OrderBy("identifier", CareWorks.Foundation.Enums.SortOrder.Descending, TableName));



            this.AddReturnField(query, TableName, PrimaryKeyName);



            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
    }
}
