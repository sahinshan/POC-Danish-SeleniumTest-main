using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderBandRateSchedule : BaseClass
    {

        public string TableName = "CareProviderBandRateSchedule";
        public string PrimaryKeyName = "CareProviderBandRateScheduleId";

        public CareProviderBandRateSchedule()
        {
            AuthenticateUser();
        }

        public CareProviderBandRateSchedule(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByBandRateTypeId(Guid careproviderbandratetypeid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careproviderbandratetypeid", ConditionOperatorType.Equal, careproviderbandratetypeid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetByBandRateTypeIdAndName(Guid careproviderbandratetypeid, string name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careproviderbandratetypeid", ConditionOperatorType.Equal, careproviderbandratetypeid);
            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Guid CreateCareProviderBandRateSchedule(Guid ownerid, string Name, Guid careproviderbandratetypeid, TimeSpan timefrom, TimeSpan timeto)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderbandratetypeid", careproviderbandratetypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "timefrom", timefrom);
            AddFieldToBusinessDataObject(buisinessDataObject, "timeto", timeto);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return CreateRecord(buisinessDataObject);
        }

        public void DeleteCareProviderBandRateScheduleRecord(Guid CareProviderBandRateScheduleId)
        {
            this.DeleteRecord(TableName, CareProviderBandRateScheduleId);
        }
    }
}
