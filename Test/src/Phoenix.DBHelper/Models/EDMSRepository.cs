using System;

namespace Phoenix.DBHelper.Models
{
    public class EDMSRepository : BaseClass
    {

        private string tableName = "EDMSRepository";
        private string primaryKeyName = "EDMSRepositoryId";

        public EDMSRepository()
        {
            AuthenticateUser();
        }

        public EDMSRepository(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public void UpdateIsDefault(Guid EDMSRepositoryID, bool isdefault)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, EDMSRepositoryID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "isdefault", isdefault);

            this.UpdateRecord(buisinessDataObject);
        }

    }
}
