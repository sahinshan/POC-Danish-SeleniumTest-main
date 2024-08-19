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
    public class BusinessModule : BaseClass
    {

        public BusinessModule(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetBusinessModuleByName(string Name)
        {
            var query = this.GetDataQueryObject("BusinessModule", false, "BusinessModuleId");
            
            this.AddReturnField(query, "BusinessModule", "BusinessModuleId");

            this.AddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, "BusinessModuleId");
        }
    }
}
