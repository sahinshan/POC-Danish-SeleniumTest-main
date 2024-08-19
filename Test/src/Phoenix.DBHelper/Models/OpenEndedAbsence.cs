using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class OpenEndedAbsence : BaseClass
    {

        public string TableName = "openendedabsence";
        public string PrimaryKeyName = "openendedabsenceid";


        public OpenEndedAbsence(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateOpenEndedAbsence(Guid ownerid, Guid owningbusinessunitid, string title, Guid locationid, Guid cpbookingtypeid, Guid systemuserid, Guid contractid, DateTime startdateandtime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);

            AddFieldToBusinessDataObject(buisinessDataObject, "locationid", locationid);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingtypeid", cpbookingtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "contractid", contractid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdateandtime", startdateandtime);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }



        public List<Guid> GetByContractId(Guid contractid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "contractid", ConditionOperatorType.Equal, contractid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }






    }
}
