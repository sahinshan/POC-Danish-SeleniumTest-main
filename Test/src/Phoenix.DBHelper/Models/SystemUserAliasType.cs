using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Phoenix.DBHelper.Models
{
    public class SystemUserAliasType : BaseClass
    {

        public string TableName = "SystemUserAliasType";
        public string PrimaryKeyName = "SystemUserAliasTypeId";


        public SystemUserAliasType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateSystemUserAliasType(Guid ownerId, Guid OwningBusinessUnitId, string name, DateTime startdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);




            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetBySystemUserAliasTypeId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetAliasBySystemUserAliasTypeID(Guid SystemUserAliasTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemUserAliasTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetSystemUserAliasesTypeByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void DeleteSystemUserAliasType(Guid SystemUserAliasTypeId)
        {
            this.DeleteRecord(TableName, SystemUserAliasTypeId);
        }

        public void DeleteSystemUserAliasType_AdoNetDirectConnection(Guid SystemUserAliasTypeId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from SystemUserAliasType where SystemUserAliasTypeId = @UserAddressID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserAddressID", SystemUserAliasTypeId);

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
