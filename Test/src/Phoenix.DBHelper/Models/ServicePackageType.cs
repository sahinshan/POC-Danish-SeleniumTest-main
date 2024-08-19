using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;


namespace Phoenix.DBHelper.Models
{
    public class ServicePackageType : BaseClass
    {

        public string TableName = "ServicePackageType".ToLower();
        public string PrimaryKeyName = "ServicePackageTypeId".ToLower();

        public ServicePackageType()
        {
            AuthenticateUser();
        }

        public ServicePackageType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServicePackageType(Guid OwnerId, string Name, DateTime StartDate, DateTime? EndDate = null, bool inactive = false, bool validforexport = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "name", Name);
            AddFieldToBusinessDataObject(dataObject, "startdate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "enddate", EndDate);

            AddFieldToBusinessDataObject(dataObject, "inactive", inactive);
            AddFieldToBusinessDataObject(dataObject, "validforexport", validforexport);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ServicePackageId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServicePackageId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
