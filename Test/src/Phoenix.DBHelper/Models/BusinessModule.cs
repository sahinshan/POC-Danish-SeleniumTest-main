using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BusinessModule : BaseClass
    {
        public string TableName { get { return "BusinessModule"; } }
        public string PrimaryKeyName { get { return "BusinessModuleid"; } }


        public BusinessModule()
        {
            AuthenticateUser();
        }

        public BusinessModule(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetBusinessModuleByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetBusinessModuleByID(Guid BusinessModuleId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("BusinessModule", false, "BusinessModuleId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "BusinessModuleId", ConditionOperatorType.Equal, BusinessModuleId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        //public void DeactivateModule(Guid BusinessModuleId)
        //{
        //    var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

        //    AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BusinessModuleId);
        //    AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", true);

        //    this.UpdateBusinessModule(buisinessDataObject);
        //}

        //public void ActivateModule(Guid BusinessModuleId)
        //{
        //    this.ActivateBusinessModule(BusinessModuleId);
        //}
    }
}
