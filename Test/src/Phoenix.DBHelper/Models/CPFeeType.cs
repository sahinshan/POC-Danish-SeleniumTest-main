using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPFeeType : BaseClass
    {
        public string TableName = "CPFeeType";
        public string PrimaryKeyName = "CPFeeTypeId";

        public CPFeeType()
        {
            AuthenticateUser();
        }

        public CPFeeType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCPFeeType(string name, DateTime startdate, int code, Guid ownerid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);


            return this.CreateRecord(buisinessDataObject);

        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CPFeeTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CPFeeTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCPFeeType(Guid CPFeeTypeId)
        {
            this.DeleteRecord(TableName, CPFeeTypeId);
        }

    }
}
