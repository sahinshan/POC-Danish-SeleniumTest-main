using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonCarePlanForm : BaseClass
    {

        public string TableName = "PersonCarePlanForm";
        public string PrimaryKeyName = "PersonCarePlanFormId";


        public PersonCarePlanForm()
        {
            AuthenticateUser();
        }

        public PersonCarePlanForm(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonCarePlanForm(Guid ownerid, Guid documentid, DateTime startdate, Guid personid, Guid personcareplanid)

        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "documentid", documentid);
            AddFieldToBusinessDataObject(dataObject, "assessmentstatusid", 1);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "sdeexecuted", 1);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "personcareplanid", personcareplanid);


            return this.CreateRecord(dataObject);
        }


        public void UpdateAssessmentStatusid(Guid PersonCarePlanFormId, int assessmentstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonCarePlanFormId);
            AddFieldToBusinessDataObject(buisinessDataObject, "assessmentstatusid", assessmentstatusid);

            this.UpdateRecord(buisinessDataObject);
        }


        public List<Guid> GetByPersonId(Guid personid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPersonIdandAssessmentStatusId(Guid personid, int assessmentstatusid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);
            this.BaseClassAddTableCondition(query, "assessmentstatusid", ConditionOperatorType.Equal, assessmentstatusid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



    }
}
