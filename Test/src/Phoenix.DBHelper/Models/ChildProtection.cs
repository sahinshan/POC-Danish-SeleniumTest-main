using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ChildProtection : BaseClass
    {

        private string tableName = "ChildProtection";
        private string primaryKeyName = "ChildProtectionId";

        public ChildProtection()
        {
            AuthenticateUser();
        }

        public ChildProtection(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateChildProtection(Guid ownerid, Guid caseid, string casetitle, Guid personid, Guid childprotectioncategoryofabuseid, Guid childprotectionstatustypeid, DateTime startdate, DateTime datestatuschanged)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "caseid_cwname", casetitle);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "childprotectioncategoryofabuseid", childprotectioncategoryofabuseid);
            AddFieldToBusinessDataObject(dataObject, "childprotectionstatustypeid", childprotectionstatustypeid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "datestatuschanged", datestatuschanged);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateChildProtection(Guid ownerid, Guid caseid, Guid personid, Guid childprotectioncategoryofabuseid, Guid childprotectionstatustypeid, DateTime startdate, DateTime enddate, DateTime datestatuschanged, Guid childprotectionendreasontypeid)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "childprotectioncategoryofabuseid", childprotectioncategoryofabuseid);
            AddFieldToBusinessDataObject(dataObject, "childprotectionstatustypeid", childprotectionstatustypeid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(dataObject, "datestatuschanged", datestatuschanged);
            AddFieldToBusinessDataObject(dataObject, "childprotectionendreasontypeid", childprotectionendreasontypeid);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public void UpdateStartDate(Guid PersonID, DateTime startdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetChildProtectionByCaseID(Guid CaseId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetChildProtectionByID(Guid ChildProtectionId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "ChildProtectionId", ConditionOperatorType.Equal, ChildProtectionId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteChildProtection(Guid ChildProtectionID)
        {
            this.DeleteRecord(tableName, ChildProtectionID);
        }



    }
}
