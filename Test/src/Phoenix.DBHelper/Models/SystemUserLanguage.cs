using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SystemUserLanguage : BaseClass
    {

        public string TableName = "SystemUserLanguage";
        public string PrimaryKeyName = "SystemUserLanguageId";


        public SystemUserLanguage(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateSystemUserLanguage(Guid OwnerId, Guid SystemUserId, Guid LanguageId, Guid? FluencyId, DateTime StartDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(buisinessDataObject, "SystemUserId", SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "LanguageId", LanguageId);
            AddFieldToBusinessDataObject(buisinessDataObject, "FluencyId", FluencyId);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetBySystemUserId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteSystemUserLanguage(Guid SystemUserLanguageId)
        {
            this.DeleteRecord(TableName, SystemUserLanguageId);
        }

        public void DeleteSystemUserLanguage_AdoNetDirectConnection(Guid SystemUserLanguageId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from SystemUserLanguage where SystemUserLanguageId = @UserLanguageID";


            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserID", SystemUserLanguageId);

                try
                {
                    connection.Open();
                    var count = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
