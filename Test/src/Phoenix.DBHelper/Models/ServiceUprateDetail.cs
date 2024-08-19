using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceUprateDetail : BaseClass
    {
        public string TableName { get { return "ServiceUprateDetail"; } }
        public string PrimaryKeyName { get { return "ServiceUprateDetailId"; } }

        public ServiceUprateDetail()
        {
            AuthenticateUser();
        }

        public ServiceUprateDetail(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceUprateDetail(Guid ownerid, Guid serviceuprateid, Guid serviceelement1id,
            Guid serviceelement2id, DateTime upratestartdate, decimal currentrate, decimal currentratebankholiday,
            decimal? proposedrate, decimal? proposedratebankholiday, TimeSpan? timebandstart, TimeSpan? timebandend,
            TimeSpan? StartTime, TimeSpan? EndTime, bool? monday, bool? tuesday, bool? wednesday, bool? thursday,
            bool? friday, bool? saturday, bool? sunday, int? approvalstatusid, Guid serviceprovisionrateperiodid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceuprateid", serviceuprateid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "upratestartdate", upratestartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "currentrate", currentrate);
            AddFieldToBusinessDataObject(buisinessDataObject, "currentratebankholiday", currentratebankholiday);
            AddFieldToBusinessDataObject(buisinessDataObject, "proposedrate", proposedrate);
            AddFieldToBusinessDataObject(buisinessDataObject, "proposedratebankholiday", proposedratebankholiday);
            AddFieldToBusinessDataObject(buisinessDataObject, "timebandstart", timebandstart);
            AddFieldToBusinessDataObject(buisinessDataObject, "timebandend", timebandend);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", StartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", EndTime);

            AddFieldToBusinessDataObject(buisinessDataObject, "monday", monday);
            AddFieldToBusinessDataObject(buisinessDataObject, "tuesday", tuesday);
            AddFieldToBusinessDataObject(buisinessDataObject, "wednesday", wednesday);
            AddFieldToBusinessDataObject(buisinessDataObject, "thursday", thursday);
            AddFieldToBusinessDataObject(buisinessDataObject, "friday", friday);
            AddFieldToBusinessDataObject(buisinessDataObject, "saturday", saturday);
            AddFieldToBusinessDataObject(buisinessDataObject, "sunday", sunday);
            AddFieldToBusinessDataObject(buisinessDataObject, "approvalstatusid", approvalstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionrateperiodid", serviceprovisionrateperiodid);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByServiceUprateID(Guid serviceuprateid)
        {
            DataQuery query = GetDataQueryObject(TableName, false, PrimaryKeyName);

            BaseClassAddTableCondition(query, "serviceuprateid", ConditionOperatorType.Equal, serviceuprateid);

            AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid serviceupratedetailid, params string[] Fields)
        {
            DataQuery query = GetDataQueryObject(TableName, false, PrimaryKeyName);

            BaseClassAddTableCondition(query, "serviceupratedetailid", ConditionOperatorType.Equal, serviceupratedetailid);

            AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
