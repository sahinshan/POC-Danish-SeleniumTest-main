using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPAllowanceType : BaseClass
    {

        private string tableName = "CPAllowanceType";
        private string primaryKeyName = "CPAllowanceTypeId";

        public CPAllowanceType()
        {
            AuthenticateUser();
        }

        public CPAllowanceType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetCPAllowanceTypeByID(Guid CPAllowanceTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPAllowanceTypeId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateCPAllowanceType(string name, int code, int govcode, DateTime startdate, Guid ownerid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "govcode", govcode);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            return this.CreateRecord(buisinessDataObject);
        }

    }
}
