using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceElement1 : BaseClass
    {
        public string TableName { get { return "ServiceElement1"; } }
        public string PrimaryKeyName { get { return "ServiceElement1id"; } }

        public ServiceElement1()
        {
            AuthenticateUser();
        }

        public ServiceElement1(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceElement1(Guid ownerid, string name, DateTime startdate, int code, int whotopayid, int paymentscommenceid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "whotopayid", whotopayid);
            AddFieldToBusinessDataObject(buisinessDataObject, "paymentscommenceid", paymentscommenceid);

            AddFieldToBusinessDataObject(buisinessDataObject, "checkrules", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "checkmaximumcapacity", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "legalstatusapplies", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "usedinfinance", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "usedinbatchsetup", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddaterequired", false);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceElement1(Guid ownerid, string name, DateTime startdate, int code, int whotopayid, int paymentscommenceid, List<Guid> ValidRateUnits, bool usedinfinance = true)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "whotopayid", whotopayid);
            AddFieldToBusinessDataObject(buisinessDataObject, "paymentscommenceid", paymentscommenceid);

            AddFieldToBusinessDataObject(buisinessDataObject, "checkrules", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "checkmaximumcapacity", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "legalstatusapplies", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "usedinfinance", usedinfinance);
            AddFieldToBusinessDataObject(buisinessDataObject, "usedinbatchsetup", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddaterequired", false);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);


            buisinessDataObject.MultiSelectBusinessObjectFields["validrateunits"] = new MultiSelectBusinessObjectDataCollection();

            if (ValidRateUnits != null && ValidRateUnits.Count > 0)
            {
                foreach (Guid rateUnitId in ValidRateUnits)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = rateUnitId,
                        ReferenceIdTableName = "RateUnit"
                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields["validrateunits"].Add(dataRecord);
                }
            }

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceElement1(Guid ownerid, string name, DateTime startdate, int code, int whotopayid, int paymentscommenceid, List<Guid> ValidRateUnits, Guid DefaultRateUnitId, Guid? glcodeid = null, bool usedinfinance = true)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "whotopayid", whotopayid);
            AddFieldToBusinessDataObject(buisinessDataObject, "paymentscommenceid", paymentscommenceid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", DefaultRateUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "glcodeid", glcodeid);

            AddFieldToBusinessDataObject(buisinessDataObject, "checkrules", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "checkmaximumcapacity", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "legalstatusapplies", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "usedinfinance", usedinfinance);
            AddFieldToBusinessDataObject(buisinessDataObject, "usedinbatchsetup", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddaterequired", false);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);


            buisinessDataObject.MultiSelectBusinessObjectFields["validrateunits"] = new MultiSelectBusinessObjectDataCollection();

            if (ValidRateUnits != null && ValidRateUnits.Count > 0)
            {
                foreach (Guid rateUnitId in ValidRateUnits)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = rateUnitId,
                        ReferenceIdTableName = "RateUnit"
                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields["validrateunits"].Add(dataRecord);
                }
            }

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceElement1(Guid ownerid, string name, DateTime startdate, int code, int whotopayid, int paymentscommenceid, List<Guid> ValidRateUnits, Guid DefaultRateUnitId, Guid paymenttypecodeid, Guid providerbatchgroupingid, int adjusteddays, Guid vatcodeid, bool usedinfinance = false, Guid? glcodeid = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "whotopayid", whotopayid);
            AddFieldToBusinessDataObject(buisinessDataObject, "paymentscommenceid", paymentscommenceid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", DefaultRateUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "glcodeid", glcodeid);

            AddFieldToBusinessDataObject(buisinessDataObject, "checkrules", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "checkmaximumcapacity", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "legalstatusapplies", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "usedinfinance", usedinfinance);
            AddFieldToBusinessDataObject(buisinessDataObject, "usedinbatchsetup", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddaterequired", false);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);


            buisinessDataObject.MultiSelectBusinessObjectFields["validrateunits"] = new MultiSelectBusinessObjectDataCollection();

            if (ValidRateUnits != null && ValidRateUnits.Count > 0)
            {
                foreach (Guid rateUnitId in ValidRateUnits)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = rateUnitId,
                        ReferenceIdTableName = "RateUnit"
                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields["validrateunits"].Add(dataRecord);
                }
            }

            AddFieldToBusinessDataObject(buisinessDataObject, "paymenttypecodeid", paymenttypecodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerbatchgroupingid", providerbatchgroupingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "adjusteddays", adjusteddays);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatcodeid", vatcodeid);

            return CreateRecord(buisinessDataObject);
        }

        public void UpdateGLCode(Guid ServiceElement1Id, Guid glcodeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ServiceElement1Id);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "glcodeid", glcodeid);

            this.UpdateRecord(buisinessDataObject);
        }
        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByCode(int Code)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "code", ConditionOperatorType.Equal, Code);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ServiceElement1id, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceElement1id);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteServiceElement1(Guid ServiceElement1ID)
        {
            this.DeleteRecord(TableName, ServiceElement1ID);
        }

        public List<Guid> GetByCode(string Code)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "code", ConditionOperatorType.Equal, Code);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
