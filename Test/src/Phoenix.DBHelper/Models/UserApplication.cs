using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class UserApplication : BaseClass
    {

        public string TableName = "UserApplication";
        public string PrimaryKeyName = "UserApplicationId";


        public UserApplication(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetBySystemUserAndHomeScreenId(Guid SystemUserId, Guid homescreenid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);
            this.BaseClassAddTableCondition(query, "homescreenid", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, homescreenid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetBySystemUserAndApplicationId(Guid SystemUserId, Guid applicationid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);
            this.BaseClassAddTableCondition(query, "applicationid", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, applicationid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public List<Guid> GetUserApplicationBySystemUserId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetUserApplicationByUserNamePrefix(string UserName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "UserName", CareWorks.Foundation.Enums.ConditionOperatorType.Like, UserName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void UpdateUserApplication(Guid UserApplicationdId, Guid homescreenid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, UserApplicationdId);
            AddFieldToBusinessDataObject(buisinessDataObject, "homescreenid", homescreenid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteUserApplication(Guid UserApplicationId)
        {
            this.DeleteRecord(TableName, UserApplicationId);
        }

        public void DeleteUserApplication_AdoNetDirectConnection(Guid UserApplicationId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from UserApplication where UserApplicationId = @UserApplicationID";


            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserApplicationID", UserApplicationId);

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
