using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class AuthenticationProvider : BaseClass
    {

        public string TableName = "AuthenticationProvider";
        public string PrimaryKeyName = "AuthenticationProviderId";


        public AuthenticationProvider(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAuthenticationProvider(Guid ownerId, Guid SystemUserId, Guid AuthenticationProvidertypeid, string firstname, string middlename, string lastname)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SystemUserId", SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "AuthenticationProvidertypeid", AuthenticationProvidertypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "firstname", firstname);
            AddFieldToBusinessDataObject(buisinessDataObject, "middlename", middlename);
            AddFieldToBusinessDataObject(buisinessDataObject, "lastname", lastname);


            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByAuthenticationProviderId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByAuthenticationProviderID(Guid AuthenticationProviderId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AuthenticationProviderId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteAuthenticationProvider(Guid AuthenticationProviderId)
        {
            this.DeleteRecord(TableName, AuthenticationProviderId);
        }

        public void DeleteAuthenticationProvider_AdoNetDirectConnection(Guid AuthenticationProviderId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from AuthenticationProvider where AuthenticationProviderId = @UserAddressID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserAddressID", AuthenticationProviderId);

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

        public List<Guid> GetAuthenticationProviderIdByName(string AuthenticationProviderName)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("AuthenticationProvider", true, "AuthenticationProviderId");
            query.PrimaryKeyName = "AuthenticationProviderId";

            query.Filter.AddCondition("AuthenticationProvider", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, AuthenticationProviderName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => Guid.Parse(c.FieldCollection["AuthenticationProviderId"].ToString())).ToList();
            else
                return new List<Guid>();
        }

        public Dictionary<string, object> GetFieldsByAuthenticationProviderID(Guid AuthenticationProvider, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);



            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AuthenticationProvider);



            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
