using CareDirector.Sdk.Query;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareproviderReportableEventBehaviour : BaseClass
    {

        public string TableName = "CareproviderReportableEventBehaviour";
        public string PrimaryKeyName = "CareproviderReportableEventBehaviourId";

        public CareproviderReportableEventBehaviour(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareproviderReportableEventBehaviourRecord(Guid ownerId, DateTime startdate, string name, Guid owningbusinessunitid, Guid cpreportableeventbehaviouractiontypeid)
        {

            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpreportableeventbehaviouractiontypeid", cpreportableeventbehaviouractiontypeid);


            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", true);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByeventId(Guid careproviderreportableeventid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            // query.Orders.Add(new OrderBy("impacttypeid", sortOrder.Descending, TableName));
            this.BaseClassAddTableCondition(query, "careproviderreportableeventid", CareWorks.Foundation.Enums.ConditionOperatorType.Contains, careproviderreportableeventid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }




    }
}

