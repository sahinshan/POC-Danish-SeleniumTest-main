using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonAlertAndHazardReview : BaseClass
    {

        public string TableName = "PersonAlertAndHazardReview";
        public string PrimaryKeyName = "PersonAlertAndHazardReviewId";


        public PersonAlertAndHazardReview()
        {
            AuthenticateUser();
        }

        public PersonAlertAndHazardReview(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetPersonAlertAndHazardReviewByAlertAndHazardID(Guid PersonAlertAndHazardId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonAlertAndHazardId", ConditionOperatorType.Equal, PersonAlertAndHazardId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonAlertAndHazardReviewByID(Guid PersonAlertAndHazardReviewId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonAlertAndHazardReviewId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateAssociatedPerson(Guid PersonAlertAndHazardReviewId, Guid personid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonAlertAndHazardReviewId", PersonAlertAndHazardReviewId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeletePersonAlertAndHazardReview(Guid PersonAlertAndHazardReviewId)
        {
            this.DeleteRecord(TableName, PersonAlertAndHazardReviewId);
        }



        public Guid CreatePersonAlertAndHazardReview(Guid personalertandhazardid, Guid ownerid, DateTime plannedreviewdate, Guid alertandhazardreviewoutcomeid, DateTime reviewdate, Guid reviewcompletedbyid)

        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            this.AddFieldToBusinessDataObject(buisinessDataObject, "personalertandhazardid", personalertandhazardid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "alertandhazardreviewoutcomeid", alertandhazardreviewoutcomeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "reviewcompletedbyid", reviewcompletedbyid);


            this.AddFieldToBusinessDataObject(buisinessDataObject, "plannedreviewdate", plannedreviewdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "reviewdate", reviewdate);


            return this.CreateRecord(buisinessDataObject);
        }


    }
}
