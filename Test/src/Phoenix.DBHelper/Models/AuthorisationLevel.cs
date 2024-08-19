using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AuthorisationLevel : BaseClass
    {

        public string TableName = "AuthorisationLevel";
        public string PrimaryKeyName = "AuthorisationLevelId";


        public AuthorisationLevel(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAuthorisationLevel(Guid ownerid, Guid systemuserid, DateTime startdate,
            int financerecordid, decimal? amount, bool authoriseownrecords, bool? forallservices, DateTime? enddate = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "financerecordid", financerecordid);
            AddFieldToBusinessDataObject(buisinessDataObject, "amount", amount);
            AddFieldToBusinessDataObject(buisinessDataObject, "authoriseownrecords", authoriseownrecords);
            AddFieldToBusinessDataObject(buisinessDataObject, "forallservices", forallservices);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetBySystemUserId(Guid systemuserid, int financerecordid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "systemuserid", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, systemuserid);
            this.BaseClassAddTableCondition(query, "financerecordid", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, financerecordid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetBySystemUserId(Guid systemuserid, int financerecordid, DateTime startdate)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "systemuserid", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, systemuserid);
            this.BaseClassAddTableCondition(query, "financerecordid", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, financerecordid);
            this.BaseClassAddTableCondition(query, "startdate", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, startdate);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
