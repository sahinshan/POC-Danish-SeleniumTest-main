using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SystemUserTraining : BaseClass
    {

        public string TableName = "systemusertraining";
        public string PrimaryKeyName = "systemusertrainingid";

        public SystemUserTraining()
        {
            AuthenticateUser();
        }

        public SystemUserTraining(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateSystemUserTraining(Guid regardingid, Guid trainingitemid, Guid? trainingrequirementid,
            DateTime startdate, DateTime? finishdate, int? outcomeid, DateTime? expirydate,
            string referencenumber, string notes, Guid ownerid, string regardingidname = "", string regardingidtablename = "systemuser")
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "trainingitemid", trainingitemid);
            AddFieldToBusinessDataObject(buisinessDataObject, "trainingrequirementid", trainingrequirementid);

            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "finishdate", finishdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "outcomeid", outcomeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "expirydate", expirydate);

            AddFieldToBusinessDataObject(buisinessDataObject, "referencenumber", referencenumber);
            AddFieldToBusinessDataObject(buisinessDataObject, "notes", notes);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidname", regardingidname);

            AddFieldToBusinessDataObject(buisinessDataObject, "updateRequired", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }


        public Guid CreateSystemUserTraining(Guid regardingid, Guid trainingitemid, Guid? trainingrequirementid, Guid bookingid,
           string regardingidname = "rd", string regardingidtablename = "systemuser")
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "trainingitemid", trainingitemid);
            AddFieldToBusinessDataObject(buisinessDataObject, "trainingrequirementid", trainingrequirementid);
            AddFieldToBusinessDataObject(buisinessDataObject, "bookingid", bookingid);



            AddFieldToBusinessDataObject(buisinessDataObject, "stafftrainingstatusid", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }


        public List<Guid> GetSystemUserTrainingById(Guid systemusertrainingid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "systemusertrainingid", ConditionOperatorType.Equal, systemusertrainingid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetSystemUserTrainingByRegardingUserName(string username)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "regardingidname", ConditionOperatorType.Equal, username);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetSystemUserTrainingByRegardingUserId(Guid systemuserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "regardingid", ConditionOperatorType.Equal, systemuserId);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetSystemUserTrainingByTrainingNameAndRegardingUserId(string trainingName, Guid systemuserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, trainingName);
            this.BaseClassAddTableCondition(query, "regardingid", ConditionOperatorType.Equal, systemuserId);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(Guid trainingItemId, Guid systemuserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "trainingItemId", ConditionOperatorType.Equal, trainingItemId);
            this.BaseClassAddTableCondition(query, "regardingid", ConditionOperatorType.Equal, systemuserId);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetBySystemUserTrainingID(Guid systemusertrainingid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, systemusertrainingid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetAll()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteSystemUserTraining(Guid systemusertrainingid)
        {
            this.DeleteRecord(TableName, systemusertrainingid);
        }

        public void UpdateSystemUserTraining(Guid SystemUserTrainingId, Guid trainingrequirementid, DateTime startDate, DateTime finishdate, int outcome)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserTrainingId);
            //this.AddFieldToBusinessDataObject(buisinessDataObject, "trainingitemid", trainingitemid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "trainingrequirementid", trainingrequirementid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "finishdate", finishdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "outcomeId", outcome);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateSystemUserTraining(Guid SystemUserTrainingId, int outcome)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserTrainingId);
            //this.AddFieldToBusinessDataObject(buisinessDataObject, "trainingitemid", trainingitemid);
            //this.AddFieldToBusinessDataObject(buisinessDataObject, "trainingrequirementid", trainingrequirementid);
            //this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startDate);
            //this.AddFieldToBusinessDataObject(buisinessDataObject, "finishdate", finishdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "outcome", outcome);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateInactiveStatus(Guid ComplianceID, bool? InactiveStatus)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ComplianceID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", InactiveStatus);
        }


    }
}
