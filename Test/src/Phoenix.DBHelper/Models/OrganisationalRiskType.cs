using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class OrganisationalRiskType : BaseClass
    {

        private string tableName = "OrganisationalRiskType";
        private string primaryKeyName = "OrganisationalRiskTypeId";

        public OrganisationalRiskType()
        {
            AuthenticateUser();
        }

        public OrganisationalRiskType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetOrganisationalRiskTypeByID(Guid OrganisationalRiskTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, OrganisationalRiskTypeId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetRiskTypeIdByName(string RiskTypeName)
        {
            DataQuery query = this.GetDataQueryObject("OrganisationalRiskType", false, "OrganisationalRiskTypeId");

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, RiskTypeName);

            this.AddReturnField(query, "OrganisationalRiskType", "OrganisationalRiskTypeId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "OrganisationalRiskTypeId");

        }

        //public List<Guid> GetRiskTypeIdByName(string RiskTypeName)
        //{
        //    var query = new CareDirector.Sdk.Query.DataQuery(tableName, true, primaryKeyName);
        //    query.PrimaryKeyName = primaryKeyName;

        //    query.Filter.AddCondition(tableName, "name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, RiskTypeName);

        //    CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

        //    if (response.HasErrors)
        //        throw new Exception(response.Exception.Message);

        //    if (response.BusinessDataCollection.Count > 0)
        //        return response.BusinessDataCollection.Select(c => (Guid.Parse(c.FieldCollection[primaryKeyName].ToString()))).ToList();
        //    else
        //        return new List<Guid>();
        //}

        public Guid CreateRiskType(Guid OwnerId, string Name, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }


    }
}
