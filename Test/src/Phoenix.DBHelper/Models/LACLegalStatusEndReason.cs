using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class LACLegalStatusEndReason : BaseClass
    {

        private string tableName = "LACLegalStatusEndReason";
        private string primaryKeyName = "LACLegalStatusEndReasonId";

        public LACLegalStatusEndReason()
        {
            AuthenticateUser();
        }

        public LACLegalStatusEndReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateLACLegalStatusEndReason(Guid ownerid, string Name, string Code, string GovCode, DateTime StartDate, bool ApplicableToCeasedLookedAfter, bool POCContinuesSubsequentLAC)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);

            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "GovCode", GovCode);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "ApplicableToCeasedLookedAfter", ApplicableToCeasedLookedAfter);
            AddFieldToBusinessDataObject(dataObject, "POCContinuesSubsequentLAC", POCContinuesSubsequentLAC);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetLACLegalStatusEndReasonByID(Guid LACLegalStatusEndReasonId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "LACLegalStatusEndReasonId", ConditionOperatorType.Equal, LACLegalStatusEndReasonId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteLACLegalStatusEndReason(Guid LACLegalStatusEndReasonID)
        {
            this.DeleteRecord(tableName, LACLegalStatusEndReasonID);
        }



    }
}
