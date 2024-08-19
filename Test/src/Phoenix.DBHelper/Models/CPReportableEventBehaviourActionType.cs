using CareDirector.Sdk.Query;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{


    public class CPReportableEventBehaviourActionType : BaseClass
    {

        public string TableName = "CPReportableEventBehaviourActionType";
        public string PrimaryKeyName = " cpreportableeventbehaviouractiontypeid";

        public CPReportableEventBehaviourActionType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCPReportableEventBehaviourActionType(Guid responsibleuserid, Guid ownerid, string name, int code, string govcode, DateTime startdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "govcode", govcode);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "valid for export", true);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByName(string name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", CareWorks.Foundation.Enums.ConditionOperatorType.Contains, name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
