using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public class UserFavouriteRecord : BaseClass
    {

        public string TableName = "UserFavouriteRecord";
        public string PrimaryKeyName = "UserFavouriteRecordId";
        

        public UserFavouriteRecord(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public void CreateUserFavouriteRecord(Guid UserID, Guid RecordId, string RecordIdTableName)
        {
            this.PinRecord(RecordId, RecordIdTableName, UserID);
        }

        
    }
}
