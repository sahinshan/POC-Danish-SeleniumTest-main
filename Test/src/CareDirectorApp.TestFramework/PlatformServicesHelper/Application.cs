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
    public class Application : BaseClass
    {

        public Application(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetApplicationByDisplayName(string DisplayName)
        {
            var query = this.GetDataQueryObject("Application", false, "ApplicationId");
            
            this.AddReturnField(query, "Application", "ApplicationId");

            this.AddTableCondition(query, "DisplayName", ConditionOperatorType.Equal, DisplayName);

            return ExecuteDataQueryAndExtractGuidFields(query, "ApplicationId");
        }
    }
}
