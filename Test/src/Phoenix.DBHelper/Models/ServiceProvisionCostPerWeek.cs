using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceProvisionCostPerWeek : BaseClass
    {

        private string tableName = "ServiceProvisionCostPerWeek";
        private string primaryKeyName = "ServiceProvisionCostPerWeekId";

        public ServiceProvisionCostPerWeek()
        {
            AuthenticateUser();
        }

        public ServiceProvisionCostPerWeek(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceProvisionCostPerWeek(Guid ownerid, Guid serviceprovisionid, Guid personid, DateTime startdate, DateTime enddate, decimal costperweek)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "serviceprovisionid", serviceprovisionid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(dataObject, "costperweek", costperweek);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetServiceProvisionCostPerWeekByServiceProvisionID(Guid ServiceProvisionID)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "serviceprovisionid", ConditionOperatorType.Equal, ServiceProvisionID);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetServiceProvisionCostPerWeekByID(Guid ServiceProvisionCostPerWeekId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "ServiceProvisionCostPerWeekId", ConditionOperatorType.Equal, ServiceProvisionCostPerWeekId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteServiceProvisionCostPerWeek(Guid ServiceProvisionCostPerWeekID)
        {
            this.DeleteRecord(tableName, ServiceProvisionCostPerWeekID);
        }



    }
}
