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
    public class AttachDocumentSubType: BaseClass
    {
        public AttachDocumentSubType()
        {
            AuthenticateUser();
        }

        public AttachDocumentSubType(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetAttachDocumentSubTypeIdByName(string AttachDocumentSubTypeName)
        {
            DataQuery query = this.GetDataQueryObject("AttachDocumentSubType", false, "AttachDocumentSubTypeId");

            this.AddTableCondition(query, "name", ConditionOperatorType.Equal, AttachDocumentSubTypeName);

            this.AddReturnField(query, "AttachDocumentSubType", "AttachDocumentSubTypeId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "AttachDocumentSubTypeId");

        }

    }
}
