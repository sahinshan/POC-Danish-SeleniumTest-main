using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class StaffReviewForm : BaseClass
    {

        public string TableName = "StaffReviewForm";
        public string PrimaryKeyName = "StaffReviewFormId";


        public StaffReviewForm()
        {
            AuthenticateUser();
        }

        public StaffReviewForm(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByStaffReviewId(Guid staffreviewid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "staffreviewid", ConditionOperatorType.Equal, staffreviewid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetBySystemUserId(Guid regardinguserid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "regardinguserid", ConditionOperatorType.Equal, regardinguserid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateStaffReviewForms(Guid staffreviewid, Guid documentid, int assessmentstatusid, DateTime startdate, Guid ownerid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "staffreviewid", staffreviewid);
            AddFieldToBusinessDataObject(dataObject, "documentid", documentid);
            AddFieldToBusinessDataObject(dataObject, "assessmentstatusid", assessmentstatusid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);


            return this.CreateRecord(dataObject);
        }

        public void DeleteStaffReviewForm(Guid StaffReviewFormId)
        {
            this.DeleteRecord(TableName, StaffReviewFormId);
        }
    }
}
