using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CareEquipment : BaseClass
    {
        public string TableName
        {
            get
            {
                return "CareEquipment";
            }
        }
        public string PrimaryKeyName
        {
            get
            {
                return "CareEquipmentid";
            }
        }


        public CareEquipment()
        {
            AuthenticateUser();
        }

        public CareEquipment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareEquipment(string name, DateTime startdate, Guid ownerid, List<int> values)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            dataObject.MultiSelectOptionSetFields = new MultiSelectOptionSetDataDictionary();
            dataObject.MultiSelectOptionSetFields["validfor"] = new MultiSelectOptionSetDataCollection();

            foreach (var numericCode in values)
            {
                dataObject.AddMultiSelectOptionSetValue("validfor", numericCode, "");
            }

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

        public Dictionary<string, object> GetById(Guid Careequipmentid, params string[] fields)
        {
            System.Threading.Thread.Sleep(1000);
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, fields);
            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, Careequipmentid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
