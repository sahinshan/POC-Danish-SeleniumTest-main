using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientBedStatusHistory : BaseClass
    {

        public string TableName = "InpatientBedStatusHistory";
        public string PrimaryKeyName = "InpatientBedStatusHistoryId";


        public InpatientBedStatusHistory()
        {
            AuthenticateUser();
        }

        public InpatientBedStatusHistory(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetInpatientBedStatusHistoryById(Guid Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetInpatientBedStatusHistoryByInpatientBedId(Guid InpatientBedId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "InpatientBedId", ConditionOperatorType.Equal, InpatientBedId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetInpatientBedStatusHistoryByID(Guid InpatientBedStatusHistoryId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, InpatientBedStatusHistoryId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateInpatientBedStatusHistory(Guid ownerid, Guid inpatientwardid, string name, string order, string row, string maxbedsinleftrow, string maxbedsinrightrow)
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

        public void DeleteInpatientBedStatusHistory(Guid InpatientBedStatusHistoryId)
        {
            this.DeleteRecord(TableName, InpatientBedStatusHistoryId);
        }
    }
}
