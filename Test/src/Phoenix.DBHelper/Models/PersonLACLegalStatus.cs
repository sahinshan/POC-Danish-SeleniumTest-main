using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonLACLegalStatus : BaseClass
    {

        private string tableName = "PersonLACLegalStatus";
        private string primaryKeyName = "PersonLACLegalStatusId";

        public PersonLACLegalStatus()
        {
            AuthenticateUser();
        }

        public PersonLACLegalStatus(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonLACLegalStatus(Guid ownerid, Guid caseid, string casetitle, Guid personid, Guid lacepisodeid, Guid laclegalstatusreasonid, Guid laclegalstatusid, Guid lacplacementid, Guid laclocationcodeid, Guid lacplacementproviderid, DateTime startdate)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "caseid_cwname", casetitle);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "lacepisodeid", lacepisodeid);
            AddFieldToBusinessDataObject(dataObject, "laclegalstatusreasonid", laclegalstatusreasonid);
            AddFieldToBusinessDataObject(dataObject, "laclegalstatusid", laclegalstatusid);
            AddFieldToBusinessDataObject(dataObject, "lacplacementid", lacplacementid);
            AddFieldToBusinessDataObject(dataObject, "laclocationcodeid", laclocationcodeid);
            AddFieldToBusinessDataObject(dataObject, "lacplacementproviderid", lacplacementproviderid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);


            return this.CreateRecord(dataObject);
        }

        public Guid CreatePersonLACLegalStatus(Guid ownerid, Guid caseid, string casetitle, Guid personid, Guid lacepisodeid, Guid laclegalstatusreasonid, Guid laclegalstatusid, Guid lacplacementid, Guid laclocationcodeid, Guid lacplacementproviderid, DateTime startdate, string placementpostcode)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "caseid_cwname", casetitle);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "lacepisodeid", lacepisodeid);
            AddFieldToBusinessDataObject(dataObject, "laclegalstatusreasonid", laclegalstatusreasonid);
            AddFieldToBusinessDataObject(dataObject, "laclegalstatusid", laclegalstatusid);
            AddFieldToBusinessDataObject(dataObject, "lacplacementid", lacplacementid);
            AddFieldToBusinessDataObject(dataObject, "laclocationcodeid", laclocationcodeid);
            AddFieldToBusinessDataObject(dataObject, "lacplacementproviderid", lacplacementproviderid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "placementpostcode", placementpostcode);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);


            return this.CreateRecord(dataObject);
        }

        public void UpdateReasonEnded(Guid PhoneCallId, DateTime enddate, Guid laclegalstatusendreasonid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PhoneCallId);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "laclegalstatusendreasonid", laclegalstatusendreasonid);

            UpdateRecord(buisinessDataObject);
        }


        public List<Guid> GetPersonLACLegalStatusByCaseID(Guid CaseId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetPersonLACLegalStatusByID(Guid PersonLACLegalStatusId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonLACLegalStatusId", ConditionOperatorType.Equal, PersonLACLegalStatusId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonLACLegalStatus(Guid PersonLACLegalStatusID)
        {
            this.DeleteRecord(tableName, PersonLACLegalStatusID);
        }



    }
}
