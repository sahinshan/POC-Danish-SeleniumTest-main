﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CarePlanInterventionType : BaseClass
    {

        public string TableName = "CarePlanInterventionType";
        public string PrimaryKeyName = "CarePlanInterventionTypeId";


        public CarePlanInterventionType()
        {
            AuthenticateUser();
        }

        public CarePlanInterventionType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCarePlanInterventionType(Guid ownerid, string name, DateTime startdate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetCarePlanTypeIdByName(string Name)
        {
            var query = new CareDirector.Sdk.Query.DataQuery(TableName, true, PrimaryKeyName);
            query.PrimaryKeyName = PrimaryKeyName;

            query.Filter.AddCondition(TableName, "name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => (Guid.Parse(c.FieldCollection[PrimaryKeyName].ToString()))).ToList();
            else
                return new List<Guid>();
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




    }
}
