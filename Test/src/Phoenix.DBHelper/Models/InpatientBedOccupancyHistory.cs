using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientBedOccupancyHistory : BaseClass
    {

        public string TableName = "InpatientBedOccupancyHistory";
        public string PrimaryKeyName = "InpatientBedOccupancyHistoryId";


        public InpatientBedOccupancyHistory()
        {
            AuthenticateUser();
        }

        public InpatientBedOccupancyHistory(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetInpatientBedOccupancyHistoryById(Guid Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByCaseId(Guid caseid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "caseid", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, caseid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetInpatientBedOccupancyHistoryByInpatientBedId(Guid InpatientBedId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "InpatientBedId", ConditionOperatorType.Equal, InpatientBedId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetInpatientBedOccupancyHistoryByID(Guid InpatientBedOccupancyHistoryId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, InpatientBedOccupancyHistoryId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateInpatientBedOccupancyHistory(Guid ownerid, Guid inpatientwardid, string name, string order, string row, string maxbedsinleftrow, string maxbedsinrightrow)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "inpatientWardid", inpatientwardid);
            AddFieldToBusinessDataObject(dataObject, "order", order);
            AddFieldToBusinessDataObject(dataObject, "row", row);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "maxbedsinleftrow", maxbedsinleftrow);
            AddFieldToBusinessDataObject(dataObject, "maxbedsinrightrow", maxbedsinrightrow);
            AddFieldToBusinessDataObject(dataObject, "applicablegendertypeid", 1);
            AddFieldToBusinessDataObject(dataObject, "hasagerestriction", 0);
            AddFieldToBusinessDataObject(dataObject, "roomtypeid", 1);
            AddFieldToBusinessDataObject(dataObject, "entrypointid", 2);

            return this.CreateRecord(dataObject);
        }

        public void DeleteInpatientBedOccupancyHistory(Guid InpatientBedOccupancyHistoryId)
        {
            this.DeleteRecord(TableName, InpatientBedOccupancyHistoryId);
        }
    }
}
