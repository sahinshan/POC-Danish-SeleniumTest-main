using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceUprate : BaseClass
    {
        public string TableName { get { return "ServiceUprate"; } }
        public string PrimaryKeyName { get { return "ServiceUprateId"; } }

        public ServiceUprate()
        {
            AuthenticateUser();
        }

        public ServiceUprate(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceUprate(Guid ownerid, Guid? serviceprovisionid,
            DateTime StartDate, int uprateratetypeid, int upratebankholidaytypeid, int roundingoptionid, decimal uprateratevalue, decimal upratebankholidayvalue, int statusid,
            int serviceratetypeid, Guid rateunitid, int contracttypeid, Guid serviceelement1id, List<Guid> serviceelement2ids,
            bool historicratechange = false, bool suspendrate = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionid", serviceprovisionid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", StartDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);

            buisinessDataObject.MultiSelectBusinessObjectFields["serviceelement2id"] = new MultiSelectBusinessObjectDataCollection();

            if (serviceelement2ids != null && serviceelement2ids.Count > 0)
            {
                foreach (Guid serviceelement2id in serviceelement2ids)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = serviceelement2id,
                        ReferenceIdTableName = "serviceelement2"
                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields["serviceelement2id"].Add(dataRecord);
                }
            }
            AddFieldToBusinessDataObject(buisinessDataObject, "uprateratetypeid", uprateratetypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "upratebankholidaytypeid", upratebankholidaytypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "roundingoptionid", roundingoptionid);
            AddFieldToBusinessDataObject(buisinessDataObject, "uprateratevalue", uprateratevalue);
            AddFieldToBusinessDataObject(buisinessDataObject, "upratebankholidayvalue", upratebankholidayvalue);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceratetypeid", serviceratetypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "contracttypeid", contracttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "historicratechange", historicratechange);
            AddFieldToBusinessDataObject(buisinessDataObject, "suspendrate", suspendrate);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return CreateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetByID(Guid serviceuprateid, params string[] Fields)
        {
            DataQuery query = GetDataQueryObject(TableName, false, PrimaryKeyName);

            BaseClassAddTableCondition(query, "serviceuprateid", ConditionOperatorType.Equal, serviceuprateid);

            AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateServiceUprateStatus(Guid ServiceUprateID, int StatusID)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ServiceUprate", "serviceuprateid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "serviceuprateid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ServiceUprateID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, StatusID);

            this.UpdateRecord(buisinessDataObject);
        }


    }
}
