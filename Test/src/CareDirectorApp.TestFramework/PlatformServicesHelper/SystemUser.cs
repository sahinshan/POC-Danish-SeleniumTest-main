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
    public class SystemUser : BaseClass
    {

        public string TableName = "SystemUser";
        public string PrimaryKeyName = "SystemUserId";
        

        public SystemUser(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetSystemUserByUserName(string UserName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);
                
            this.AddTableCondition(query, "UserName", ConditionOperatorType.Equal, UserName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
