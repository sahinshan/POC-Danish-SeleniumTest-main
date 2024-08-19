using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CarePlanNeedDomain : BaseClass
    {

        public string TableName = "CarePlanNeedDomain";
        public string PrimaryKeyName = "CarePlanNeedDomainId";


        public CarePlanNeedDomain()
        {
            AuthenticateUser();
        }

        public CarePlanNeedDomain(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCarePlanNeedDomain(Guid CarePlanNeedDomainId, Guid ownerid, string name, DateTime startdate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "CarePlanNeedDomainId", CarePlanNeedDomainId);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", true);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateCarePlanNeedDomain(Guid ownerid, string name, DateTime startdate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", true);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetCarePlanNeedDomainIdByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByName(string name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetCarePlanNeedByID(Guid CarePlanNeedDomainId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CarePlanNeedDomainId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
