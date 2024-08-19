using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{


    public class SpecialistMattress : BaseClass
    {
        private string TableName = "specialistmattress";
        private string PrimaryKeyName = "specialistmattressid";

        public SpecialistMattress()
        {
            AuthenticateUser();
        }

        public SpecialistMattress(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateSpecialistMattress(string name, DateTime startdate, Guid ownerid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "iselectricdevice", false);


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

        public Dictionary<string, object> GetById(Guid carewellbeingid, params string[] fields)
        {
            System.Threading.Thread.Sleep(1000);
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, fields);
            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, carewellbeingid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
