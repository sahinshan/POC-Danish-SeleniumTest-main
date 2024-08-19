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
    public class RecordLevelAccess : BaseClass
    {

        public string TableName = "RecordLevelAccess";
        public string PrimaryKeyName = "RecordLevelAccessId";


        public RecordLevelAccess(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public void CreateRecordLevelAccess(Guid RecordId, string RecordIdTableName, string RecordIdName, Guid ShareToId, String ShareToIdTableName, string ShareToIdName, bool CanView, bool CanEdit, bool CanDelete, bool CanShare)
        {

            var recordLevelAccess = new CareDirector.Sdk.SystemEntities.RecordLevelAccess()
            {
                RecordId = RecordId,
                RecordIdTableName = RecordIdTableName,
                RecordIdName = RecordIdName,
                
                ShareToId = ShareToId,
                ShareToIdTableName = ShareToIdTableName,
                ShareToIdName = ShareToIdName,
                
                CanView = CanView,
                CanEdit = CanEdit,
                CanDelete = CanDelete,
                CanShare = CanShare
            };


            this.GrantRecordLevelAccess(recordLevelAccess);
        }

        public List<Guid> GetRecordLevelAccessByRecordID(Guid RecordId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "RecordId", ConditionOperatorType.Equal, RecordId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteRecordLevelAccess(Guid RecordLevelAccessId)
        {
            this.RevokeRecordLevelAccess(RecordLevelAccessId);
        }
    }
}
