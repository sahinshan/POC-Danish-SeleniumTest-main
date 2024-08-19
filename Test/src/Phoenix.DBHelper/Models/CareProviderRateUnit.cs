using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderRateUnit : BaseClass
    {

        public string TableName = "CareProviderRateUnit";
        public string PrimaryKeyName = "CareProviderRateUnitId";

        public CareProviderRateUnit()
        {
            AuthenticateUser();
        }

        public CareProviderRateUnit(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string CareProviderRateUnitName)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, CareProviderRateUnitName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetByCode(int Code)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Code", ConditionOperatorType.Equal, Code);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetAll()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateCareProviderRateUnit(Guid ownerid, string Name, DateTime startdate, int code, bool timeanddays = false, bool bandedrates = false, bool oneoff = false, bool decimalplaces = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "timeanddays", timeanddays);
            AddFieldToBusinessDataObject(buisinessDataObject, "bandedrates", bandedrates);
            AddFieldToBusinessDataObject(buisinessDataObject, "oneoff", oneoff);
            AddFieldToBusinessDataObject(buisinessDataObject, "decimalplaces", decimalplaces);

            AddFieldToBusinessDataObject(buisinessDataObject, "minimumallowed", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "maximumallowed", 999999);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitdivisor", 0);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public void UpdateOneOff(Guid CareProviderContractSchemeId, bool oneoff)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractSchemeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "oneoff", oneoff);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateTimeAndDays(Guid CareProviderContractSchemeId, bool timeanddays)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractSchemeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "timeanddays", timeanddays);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteCareProviderRateUnitRecord(Guid CareProviderRateUnitId)
        {
            this.DeleteRecord(TableName, CareProviderRateUnitId);
        }
    }
}
