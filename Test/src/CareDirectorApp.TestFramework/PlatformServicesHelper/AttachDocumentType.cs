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
    public class AttachDocumentType: BaseClass
    {
        public AttachDocumentType()
        {
            AuthenticateUser();
        }

        public AttachDocumentType(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetAttachDocumentTypeIdByName(string AttachDocumentTypeName)
        {
            DataQuery query = this.GetDataQueryObject("AttachDocumentType", false, "AttachDocumentTypeId");

            this.AddTableCondition(query, "name", ConditionOperatorType.Equal, AttachDocumentTypeName);

            this.AddReturnField(query, "AttachDocumentType", "AttachDocumentTypeId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "AttachDocumentTypeId");

        }

    }
}
