﻿using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace CareDirectorApp.TestFramework
{
    public class PersonAlertAndHazard : BaseClass
    {

        public string TableName = "PersonAlertAndHazard";
        public string PrimaryKeyName = "PersonAlertAndHazardId";
        

        public PersonAlertAndHazard(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreatePersonAlertAndHazard(int roleid, string details, Guid ownerid, string ownerName, Guid personid, string personName, Guid alertandhazardtypeid, string alertandhazardtypeName, Guid alertandhazardendreasonid, string alertandhazardendreasonName, DateTime startdate, DateTime? enddate, int reviewfrequencytypeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "roleid", roleid);
            
            if (roleid == 1)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "roleid_cwname", "Represents an Alert/Hazard");
            else if (roleid == 2)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "roleid_cwname", "Exposed to Alert/Hazard");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "details", details);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid_cwname", ownerName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid_cwname", personName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "alertandhazardtypeid", alertandhazardtypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "alertandhazardtypeid_cwname", alertandhazardtypeName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "alertandhazardendreasonid", alertandhazardendreasonid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "alertandhazardendreasonid_cwname", alertandhazardendreasonName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            if(enddate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate.Value);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "reviewfrequencytypeid", reviewfrequencytypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonAlertAndHazardByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonAlertAndHazardByID(Guid PersonAlertAndHazardId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonAlertAndHazardId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonAlertAndHazard(Guid PersonAlertAndHazardId)
        {
            this.DeleteRecord(TableName, PersonAlertAndHazardId);
        }
    }
}
