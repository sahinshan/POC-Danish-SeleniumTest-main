using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Phoenix.DBHelper.Models
{
    public class SystemUserAlias : BaseClass
    {

        public string TableName = "SystemUserAlias";
        public string PrimaryKeyName = "SystemUserAliasId";


        public SystemUserAlias(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateSystemUserAlias(Guid ownerId, Guid SystemUserId, Guid systemuseraliastypeid, string firstname, string middlename, string lastname)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SystemUserId", SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuseraliastypeid", systemuseraliastypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "firstname", firstname);
            AddFieldToBusinessDataObject(buisinessDataObject, "middlename", middlename);
            AddFieldToBusinessDataObject(buisinessDataObject, "lastname", lastname);


            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetBySystemUserAliasId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetAliasBySystemUserAliasID(Guid SystemUserAliasId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemUserAliasId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteSystemUserAlias(Guid SystemUserAliasId)
        {
            this.DeleteRecord(TableName, SystemUserAliasId);
        }

        public void DeleteSystemUserAlias_AdoNetDirectConnection(Guid SystemUserAliasId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from SystemUserAlias where SystemUserAliasId = @UserAddressID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserAddressID", SystemUserAliasId);

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
