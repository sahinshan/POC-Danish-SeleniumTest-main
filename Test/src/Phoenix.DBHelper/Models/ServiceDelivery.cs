using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceDelivery : BaseClass
    {
        public string TableName { get { return "ServiceDelivery"; } }
        public string PrimaryKeyName { get { return "ServiceDeliveryid"; } }

        public ServiceDelivery()
        {
            AuthenticateUser();
        }

        public ServiceDelivery(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceDelivery(Guid ownerid, Guid personid, Guid serviceprovisionid, Guid rateunitid, int units, int numberofcarers,
                                            bool selectall, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday, string notetext)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionid", serviceprovisionid);
            AddFieldToBusinessDataObject(buisinessDataObject, "units", units);
            AddFieldToBusinessDataObject(buisinessDataObject, "numberofcarers", numberofcarers);
            AddFieldToBusinessDataObject(buisinessDataObject, "selectall", selectall);
            AddFieldToBusinessDataObject(buisinessDataObject, "monday", monday);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "tuesday", tuesday);
            AddFieldToBusinessDataObject(buisinessDataObject, "wednesday", wednesday);
            AddFieldToBusinessDataObject(buisinessDataObject, "thursday", thursday);
            AddFieldToBusinessDataObject(buisinessDataObject, "friday", friday);
            AddFieldToBusinessDataObject(buisinessDataObject, "saturday", saturday);
            AddFieldToBusinessDataObject(buisinessDataObject, "sunday", sunday);
            AddFieldToBusinessDataObject(buisinessDataObject, "notetext", notetext);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstarttime", "09:00");

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceDelivery(Guid ownerid, Guid personid, Guid serviceprovisionid, Guid rateunitid, int units, int numberofcarers,
                                            bool selectall, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday, string notetext, TimeSpan? plannedstarttime = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            if (plannedstarttime == null)
            {
                plannedstarttime = new TimeSpan(9, 0, 0);
            }
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionid", serviceprovisionid);
            AddFieldToBusinessDataObject(buisinessDataObject, "units", units);
            AddFieldToBusinessDataObject(buisinessDataObject, "numberofcarers", numberofcarers);
            AddFieldToBusinessDataObject(buisinessDataObject, "selectall", selectall);
            AddFieldToBusinessDataObject(buisinessDataObject, "monday", monday);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "tuesday", tuesday);
            AddFieldToBusinessDataObject(buisinessDataObject, "wednesday", wednesday);
            AddFieldToBusinessDataObject(buisinessDataObject, "thursday", thursday);
            AddFieldToBusinessDataObject(buisinessDataObject, "friday", friday);
            AddFieldToBusinessDataObject(buisinessDataObject, "saturday", saturday);
            AddFieldToBusinessDataObject(buisinessDataObject, "sunday", sunday);
            AddFieldToBusinessDataObject(buisinessDataObject, "notetext", notetext);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstarttime", plannedstarttime);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetServiceDeliveryByNumber(string ServiceDeliveryNumber)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "ServiceDeliveryNumber", ConditionOperatorType.Equal, ServiceDeliveryNumber);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ServiceDeliveryid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceDeliveryid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByServiceProvisionID(Guid serviceprovisionid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "serviceprovisionid", ConditionOperatorType.Equal, serviceprovisionid);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateServiceDeliveryEndDates(Guid ServiceDeliveryID, DateTime? PlannedEndDate, DateTime? ActualEndDate, Guid? ServiceDeliveryEndReasonId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ServiceDeliveryID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PlannedEndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, PlannedEndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ActualEndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, ActualEndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ServiceDeliveryEndReasonId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ServiceDeliveryEndReasonId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateServiceDeliveryStatus(Guid ServiceDeliveryID, Guid StatusID)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ServiceDeliveryID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ServiceDeliverystatusid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, StatusID);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteServiceDelivery(Guid ServiceDeliveryID)
        {
            this.DeleteRecord(TableName, ServiceDeliveryID);
        }

    }
}
