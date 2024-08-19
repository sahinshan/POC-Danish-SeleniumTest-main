using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BusinessUnit : BaseClass
    {
        public string TableName { get { return "BusinessUnit"; } }
        public string PrimaryKeyName { get { return "BusinessUnitid"; } }


        public BusinessUnit()
        {
            AuthenticateUser();
        }

        public BusinessUnit(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBusinessUnit(string Name)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateBusinessUnit(string Name, Guid? parentbusinessunitid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "parentbusinessunitid", parentbusinessunitid);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetBusinessUnitByID(Guid BusinessUnitId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("BusinessUnit", false, "BusinessUnitId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "BusinessUnitId", ConditionOperatorType.Equal, BusinessUnitId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
