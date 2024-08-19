using System;


namespace Phoenix.DBHelper.Models
{
    public class UserFavouriteRecord : BaseClass
    {

        public string TableName = "UserFavouriteRecord";
        public string PrimaryKeyName = "UserFavouriteRecordId";

        public UserFavouriteRecord(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public void CreateUserFavouriteRecord(Guid UserID, Guid RecordId, string RecordIdTableName)
        {
            this.PinRecord(RecordId, RecordIdTableName, UserID);
        }


    }
}
