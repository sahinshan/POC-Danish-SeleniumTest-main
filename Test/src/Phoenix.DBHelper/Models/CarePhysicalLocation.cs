using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CarePhysicalLocation : BaseClass
    {

        private string TableName = "carephysicallocation";
        private string PrimaryKeyName = "carephysicallocationid";


        public CarePhysicalLocation()
        {
            AuthenticateUser();
        }

        public CarePhysicalLocation(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCarePhysicalLocation(string name, DateTime startdate, Guid ownerid, List<int> values)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            dataObject.MultiSelectOptionSetFields = new MultiSelectOptionSetDataDictionary();
            dataObject.MultiSelectOptionSetFields["isvalidfor"] = new MultiSelectOptionSetDataCollection();

            foreach (var numericCode in values)
            {
                dataObject.AddMultiSelectOptionSetValue("isvalidfor", numericCode, "");
            }

            return this.CreateRecord(dataObject);
        }

        public Guid CreateCarePhysicalLocation(string name, DateTime startdate, Guid ownerid, Dictionary<int, string> values)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", true);

            dataObject.MultiSelectOptionSetFields = new MultiSelectOptionSetDataDictionary();
            dataObject.MultiSelectOptionSetFields["isvalidfor"] = new MultiSelectOptionSetDataCollection();

            if(values != null && values.Count > 0)
            {
                foreach (KeyValuePair<int, string> value in values)
                {
                    var dataRecord = new MultiSelectOptionSetData()
                    {
                        OptionSetValue = value.Key,
                        OptionSetText = value.Value,

                    };
                    dataObject.MultiSelectOptionSetFields["isvalidfor"].Add(dataRecord);
                }
            }


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
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

        public Dictionary<string, object> GetById(Guid carephysicallocationid, params string[] fields)
        {
            System.Threading.Thread.Sleep(1000);
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, fields);
            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, carephysicallocationid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
