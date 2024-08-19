using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RecruitmentRoleApplicant : BaseClass
    {

        public string TableName = "RecruitmentRoleApplicant";
        public string PrimaryKeyName = "RecruitmentRoleApplicantId";


        public RecruitmentRoleApplicant()
        {
            AuthenticateUser();
        }

        public RecruitmentRoleApplicant(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateRecruitmentRoleApplicant(Guid applicantid, Guid careproviderstaffroletypeid, Guid responsibleuserid, Guid ownerid, DateTime applicationdate, Guid targetteamid, int recruitmentstatusid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "applicantid", applicantid);
            AddFieldToBusinessDataObject(dataObject, "careproviderstaffroletypeid", careproviderstaffroletypeid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "applicationdate", applicationdate.Date);
            AddFieldToBusinessDataObject(dataObject, "targetteamid", targetteamid);
            AddFieldToBusinessDataObject(dataObject, "recruitmentstatusid", recruitmentstatusid);
            return this.CreateRecord(dataObject);
        }

        public Guid CreateRecruitmentRoleApplicant(Guid applicantid, Guid careproviderstaffroletypeid, Guid responsibleuserid, Guid ownerid, DateTime applicationdate, Guid targetteamid, int recruitmentstatusid, string applicantidtablename, string applicantidname, Guid? contracttypeid = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "applicantid", applicantid);
            AddFieldToBusinessDataObject(dataObject, "careproviderstaffroletypeid", careproviderstaffroletypeid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "applicationdate", applicationdate);
            AddFieldToBusinessDataObject(dataObject, "targetteamid", targetteamid);
            AddFieldToBusinessDataObject(dataObject, "recruitmentstatusid", recruitmentstatusid);

            AddFieldToBusinessDataObject(dataObject, "applicantidtablename", applicantidtablename);
            AddFieldToBusinessDataObject(dataObject, "applicantidname", applicantidname);

            AddFieldToBusinessDataObject(dataObject, "contracttypeid", contracttypeid);
            //AddFieldToBusinessDataObject(dataObject, "inductionprogress", 50);
            //AddFieldToBusinessDataObject(dataObject, "daystohire", 65);
            //AddFieldToBusinessDataObject(dataObject, "fullyacceptedprogress", 45);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByApplicantId(Guid applicantid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "applicantid", ConditionOperatorType.Equal, applicantid);
            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetRecruitmentRoleApplicantByID(Guid RecruitmentRoleApplicantId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, RecruitmentRoleApplicantId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteRecruitmentRoleApplicant(Guid RecruitmentRoleApplicantId)
        {
            this.DeleteRecord(TableName, RecruitmentRoleApplicantId);
        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Contains, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateRecruitmentStatus(Guid RecruitmentRoleApplicantId, int recruitmentstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, RecruitmentRoleApplicantId);

            AddFieldToBusinessDataObject(buisinessDataObject, "recruitmentstatusid", recruitmentstatusid);

            UpdateRecord(buisinessDataObject);
        }
    }
}
