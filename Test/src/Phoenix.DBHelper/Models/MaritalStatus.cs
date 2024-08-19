using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class MaritalStatus : BaseClass
    {

        public string TableName { get { return "MaritalStatus"; } }
        public string PrimaryKeyName { get { return "MaritalStatusid"; } }

        public MaritalStatus()
        {
            AuthenticateUser();
        }

        public MaritalStatus(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetMaritalStatusIdByName(string MaritalStatusName)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("MaritalStatus", true, "MaritalStatusId");
            query.PrimaryKeyName = "MaritalStatusId";

            query.Filter.AddCondition("MaritalStatus", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, MaritalStatusName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => Guid.Parse(c.FieldCollection["MaritalStatusId"].ToString())).ToList();
            else
                return new List<Guid>();
        }

        public Guid CreateMaritalStatus(string Name, DateTime startdate, Guid ownerid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }
    }
}
