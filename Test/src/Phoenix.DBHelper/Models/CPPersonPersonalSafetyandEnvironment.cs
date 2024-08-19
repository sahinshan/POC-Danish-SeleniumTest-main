using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPPersonPersonalSafetyandEnvironment : BaseClass
    {
        public string TableName = "cppersonpersonalsafetyandenvironment";
        public string PrimaryKeyName = "cppersonpersonalsafetyandenvironmentid";


        public CPPersonPersonalSafetyandEnvironment()
        {
            AuthenticateUser();
        }

        public CPPersonPersonalSafetyandEnvironment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        //When Care Consent = No, Non-consent Detail = Absent
        public Guid CreateCPPersonPersonalSafetyandEnvironment(Guid personid, Guid ownerid, Guid owningbusinessunitid, DateTime occurred, int careconsentgivenid, int carenonconsentid, string reasonforabsence)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "occurred", occurred);
            AddFieldToBusinessDataObject(dataObject, "consentgivenid", careconsentgivenid);
            AddFieldToBusinessDataObject(dataObject, "nonconsentdetailid", carenonconsentid);
            AddFieldToBusinessDataObject(dataObject, "reasonforabsence", reasonforabsence);
            return this.CreateRecord(dataObject);
        }

        //When Care Consent = No, Non-consent Detail = Deferred
        public Guid CreateCPPersonPersonalSafetyandEnvironment(Guid personid, Guid ownerid, Guid owningbusinessunitid, DateTime occurred, int careconsentgivenid, int carenonconsentid, DateTime deferredtodate, int timeorshiftid, TimeSpan? deferredtotime, Guid? deferredtoshiftid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "occurred", occurred);
            AddFieldToBusinessDataObject(dataObject, "consentgivenid", careconsentgivenid);
            AddFieldToBusinessDataObject(dataObject, "nonconsentdetailid", carenonconsentid);
            AddFieldToBusinessDataObject(dataObject, "deferredtodate", deferredtodate);
            AddFieldToBusinessDataObject(dataObject, "timeorshiftid", timeorshiftid);
            AddFieldToBusinessDataObject(dataObject, "deferredtotime", deferredtotime);
            AddFieldToBusinessDataObject(dataObject, "deferredtoshiftid", deferredtoshiftid);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByPersonId(Guid PersonId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByCPPersonPersonalSafetyandEnvironmentId(Guid CPPersonPersonalSafetyandEnvironmentId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CPPersonPersonalSafetyandEnvironmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetByID(Guid CPPersonPersonalSafetyandEnvironmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CPPersonPersonalSafetyandEnvironmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCPPersonPersonalSafetyandEnvironment(Guid CPPersonPersonalSafetyandEnvironmentId)
        {
            this.DeleteRecord(TableName, CPPersonPersonalSafetyandEnvironmentId);
        }


    }
}
