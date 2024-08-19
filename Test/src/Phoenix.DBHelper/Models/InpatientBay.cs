using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientBay : BaseClass
    {

        public string TableName = "InpatientBay";
        public string PrimaryKeyName = "InpatientBayId";


        public InpatientBay()
        {
            AuthenticateUser();
        }

        public InpatientBay(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetInpatientBayById(Guid Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public List<Guid> GetInpatientBayByInpatientWardId(Guid InpatientWardId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "InpatientWardId", ConditionOperatorType.Equal, InpatientWardId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetInpatientBayByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetInpatientBayByName(string Name, Guid inpatientWardid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);
            this.BaseClassAddTableCondition(query, "inpatientWardid", ConditionOperatorType.Equal, inpatientWardid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public Dictionary<string, object> GetInpatientBayByID(Guid InpatientBayId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, InpatientBayId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateInpatientBay(Guid ownerid, Guid inpatientwardid, string name, string order, string row, string maxbedsinleftrow, string maxbedsinrightrow)
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

        public Guid CreateInpatientBayNoBed(Guid ownerid, Guid inpatientwardid, string name, string order, string row)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "inpatientWardid", inpatientwardid);
            AddFieldToBusinessDataObject(dataObject, "order", order);
            AddFieldToBusinessDataObject(dataObject, "row", row);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "roomtypeid", 3);
            AddFieldToBusinessDataObject(dataObject, "entrypointid", 2);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateInpatientCaseBay(Guid ownerid, Guid inpatientwardid, string name, int order, string row, string maxbedsinleftrow, string maxbedsinrightrow, int applicablegendertypeid)
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
            AddFieldToBusinessDataObject(dataObject, "applicablegendertypeid", applicablegendertypeid);



            return this.CreateRecord(dataObject);
        }

        public void DeleteInpatientBay(Guid InpatientBayId)
        {
            this.DeleteRecord(TableName, InpatientBayId);
        }
    }
}
