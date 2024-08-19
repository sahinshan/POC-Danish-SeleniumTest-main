using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CarePlanType : BaseClass
    {

        public string TableName = "CarePlanType";
        public string PrimaryKeyName = "CarePlanTypeId";


        public CarePlanType()
        {
            AuthenticateUser();
        }

        public CarePlanType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCarePlanTypeId(string name, DateTime startdate, Guid ownerid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "iscpa", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", true);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string name)
        {
            var query = new CareDirector.Sdk.Query.DataQuery(TableName, true, PrimaryKeyName);
            query.PrimaryKeyName = PrimaryKeyName;

            query.Filter.AddCondition(TableName, "name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, name);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => (Guid.Parse(c.FieldCollection[PrimaryKeyName].ToString()))).ToList();
            else
                return new List<Guid>();
        }

        public Dictionary<string, object> GetCarePlanTypeByID(Guid CarePlanTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CarePlanTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCarePlanType(Guid CarePlanTypeId)
        {
            this.DeleteRecord(TableName, CarePlanTypeId);
        }
    }
}
