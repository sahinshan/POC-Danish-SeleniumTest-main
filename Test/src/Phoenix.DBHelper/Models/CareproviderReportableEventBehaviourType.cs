using CareDirector.Sdk.Query;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareproviderReportableEventBehaviourType : BaseClass
    {

        public string TableName = "CareProviderReportableEventBehaviourType";
        public string PrimaryKeyName = "CareProviderReportableEventBehaviourTypeId";

        public CareproviderReportableEventBehaviourType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareproviderReportableEventBehaviourTypeRecord(Guid ownerid, DateTime startdate, string name, Guid owningbusinessunitid, Guid cpreportableeventbehaviouractiontypeid)
        {

            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpreportableeventbehaviouractiontypeid", cpreportableeventbehaviouractiontypeid);


            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", true);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByName(string name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            // query.Orders.Add(new OrderBy("impacttypeid", sortOrder.Descending, TableName));
            this.BaseClassAddTableCondition(query, "name", CareWorks.Foundation.Enums.ConditionOperatorType.Contains, name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }




    }
}

