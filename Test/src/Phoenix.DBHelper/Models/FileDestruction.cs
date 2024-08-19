using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FileDestruction : BaseClass
    {

        private string tableName = "FileDestruction";
        private string primaryKeyName = "FileDestructionId";

        public FileDestruction()
        {
            AuthenticateUser();
        }


        public FileDestruction(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthInformation)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthInformation);

        }

        public List<Guid> GetFileDestructionByDefaultRecordId(Guid DefaultRecordId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DefaultRecordId", ConditionOperatorType.Equal, DefaultRecordId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetFileDestructionByID(Guid FileDestructionId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "FileDestructionId", ConditionOperatorType.Equal, FileDestructionId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteFileDestruction(Guid FileDestructionID)
        {
            this.DeleteRecord(tableName, FileDestructionID);
        }



    }
}
