using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Phoenix.DBHelper.Models
{
    public class SystemUserAddress : BaseClass
    {

        public string TableName = "SystemUserAddress";
        public string PrimaryKeyName = "SystemUserAddressId";


        public SystemUserAddress(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateSystemUserAddress(Guid ownerId, Guid SystemUserId, string propertyname, string addressline1, string addressline2, string addressline3,
                                            string addressline4, string addressline5, string postcode, string country, int addresstypeid, DateTime StartDate, DateTime? enddate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SystemUserId", SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "propertyname", propertyname);
            AddFieldToBusinessDataObject(buisinessDataObject, "addressline1", addressline1);
            AddFieldToBusinessDataObject(buisinessDataObject, "addressline2", addressline2);
            AddFieldToBusinessDataObject(buisinessDataObject, "addressline3", addressline3);
            AddFieldToBusinessDataObject(buisinessDataObject, "addressline4", addressline4);
            AddFieldToBusinessDataObject(buisinessDataObject, "addressline5", addressline5);
            AddFieldToBusinessDataObject(buisinessDataObject, "postcode", postcode);
            AddFieldToBusinessDataObject(buisinessDataObject, "country", country);
            AddFieldToBusinessDataObject(buisinessDataObject, "addresstypeid", addresstypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetBySystemUserAddressId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);
            query.AddThisTableOrder("startdate", CareWorks.Foundation.Enums.SortOrder.Ascending);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetBySystemUser(string SystemUser)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUser);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteSystemUserAddress(Guid SystemUserAddressId)
        {
            this.DeleteRecord(TableName, SystemUserAddressId);
        }

        public void DeleteSystemUserAddress_AdoNetDirectConnection(Guid SystemUserAddressId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from SystemUserAddress where SystemUserAddressId = @UserAddressID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserAddressID", SystemUserAddressId);

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

        public Dictionary<string, object> GetSystemUserAddressBySystemUserID(Guid LinkedAddressId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, LinkedAddressId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



    }
}
