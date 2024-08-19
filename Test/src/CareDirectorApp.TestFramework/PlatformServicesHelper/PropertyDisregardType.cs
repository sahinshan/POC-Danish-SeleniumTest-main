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
    public class PropertyDisregardType : BaseClass
    {

        public string TableName = "PropertyDisregardType";
        public string PrimaryKeyName = "PropertyDisregardTypeId";
        

        public PropertyDisregardType(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetPropertyDisregardTypeByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPropertyDisregardTypeByID(Guid PropertyDisregardTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PropertyDisregardTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePropertyDisregardType(Guid PropertyDisregardTypeId)
        {
            this.DeleteRecord(TableName, PropertyDisregardTypeId);
        }
    }
}
