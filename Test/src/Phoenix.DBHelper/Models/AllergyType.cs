using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class AllergyType : BaseClass
    {

        private string TableName = "AllergyType";
        private string PrimaryKeyName = "AllergyTypeId";

        public AllergyType()
        {
            AuthenticateUser();
        }

        public AllergyType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAllergyType(Guid OwnerId, string Name, DateTime StartDate, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            AddFieldToBusinessDataObject(dataObject, "isnoneknown", false);

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

        public Dictionary<string, object> GetByAllergyTypeId(Guid AllergyTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AllergyTypeId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteAllergyTypeRecord(Guid AllergyTypeId)
        {
            this.DeleteRecord(TableName, AllergyTypeId);
        }

    }
}
