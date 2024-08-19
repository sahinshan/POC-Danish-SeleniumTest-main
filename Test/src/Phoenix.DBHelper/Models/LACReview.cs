using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class LACReview : BaseClass
    {

        private string tableName = "LACReview";
        private string primaryKeyName = "LACReviewId";

        public LACReview()
        {
            AuthenticateUser();
        }

        public LACReview(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateLACReview(Guid ownerid, Guid lacepisodeid, Guid lacreviewtypeid, DateTime planneddate)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "lacepisodeid", lacepisodeid);
            AddFieldToBusinessDataObject(dataObject, "lacreviewtypeid", lacreviewtypeid);
            AddFieldToBusinessDataObject(dataObject, "planneddate", planneddate);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByLACEpisode(Guid lacepisodeid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "lacepisodeid", ConditionOperatorType.Equal, lacepisodeid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetLACReviewByID(Guid LACReviewId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, LACReviewId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteLACReview(Guid LACReviewID)
        {
            this.DeleteRecord(tableName, LACReviewID);
        }



    }
}
