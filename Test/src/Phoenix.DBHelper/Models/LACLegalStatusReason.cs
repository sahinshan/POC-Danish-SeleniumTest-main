using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class LACLegalStatusReason : BaseClass
    {

        private string tableName = "LACLegalStatusReason";
        private string primaryKeyName = "LACLegalStatusReasonId";

        public LACLegalStatusReason()
        {
            AuthenticateUser();
        }

        public LACLegalStatusReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateLACLegalStatusReason(Guid ownerid, string Name, string Code, string GovCode, DateTime StartDate, bool ApplicableToStartedLookedAfter, bool ValidForContinuingEpisodeOfCare)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);

            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "GovCode", GovCode);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "ApplicableToStartedLookedAfter", ApplicableToStartedLookedAfter);
            AddFieldToBusinessDataObject(dataObject, "ValidForContinuingEpisodeOfCare", ValidForContinuingEpisodeOfCare);

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

        public Dictionary<string, object> GetLACLegalStatusReasonByID(Guid LACLegalStatusReasonId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "LACLegalStatusReasonId", ConditionOperatorType.Equal, LACLegalStatusReasonId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteLACLegalStatusReason(Guid LACLegalStatusReasonID)
        {
            this.DeleteRecord(tableName, LACLegalStatusReasonID);
        }



    }
}
