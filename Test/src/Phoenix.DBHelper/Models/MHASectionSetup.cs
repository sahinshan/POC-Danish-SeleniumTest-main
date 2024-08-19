using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MHASectionSetup : BaseClass
    {
        private string tableName = "MHASectionSetup";
        private string primaryKeyName = "MHASectionSetupId";

        public MHASectionSetup()
        {
            AuthenticateUser();
        }

        public MHASectionSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateMHASectionSetup(string sectionName, string code, Guid ownerid, string description,
            bool informal = false, bool validforexport = false, int courtorderid = 1, int ministryofjusticerestrictionid = 1,
            bool section117entitlement = true, int medicalApproverCountId = 2, int? mhasectionlengthtypeid = null,
            bool allowplaceofextensions = false, bool allowrenewalbeforesectionend = false)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "sectionname", sectionName);
            AddFieldToBusinessDataObject(dataObject, "code", code);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "description", description);
            AddFieldToBusinessDataObject(dataObject, "informal", informal);
            AddFieldToBusinessDataObject(dataObject, "validforexport", validforexport);
            AddFieldToBusinessDataObject(dataObject, "courtorderid", courtorderid);
            AddFieldToBusinessDataObject(dataObject, "ministryofjusticerestrictionid", ministryofjusticerestrictionid);
            AddFieldToBusinessDataObject(dataObject, "section117entitlement", section117entitlement);
            AddFieldToBusinessDataObject(dataObject, "medicalApproverCountId", medicalApproverCountId); // value 2 corresponds to option 1
            AddFieldToBusinessDataObject(dataObject, "mhasectionlengthtypeid", mhasectionlengthtypeid);
            AddFieldToBusinessDataObject(dataObject, "allowplaceofextensions", allowplaceofextensions);
            AddFieldToBusinessDataObject(dataObject, "allowrenewalbeforesectionend", allowrenewalbeforesectionend);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);



            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetMHASectionSetupBySectionName(String SectionName)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "SectionName", ConditionOperatorType.Equal, SectionName);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetMHASectionSetupByCode(String Code)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Code", ConditionOperatorType.Equal, Code);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetMHASectionSetupBySectionNameAndCode(String SectionName, string Code)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "SectionName", ConditionOperatorType.Equal, SectionName);

            this.BaseClassAddTableCondition(query, "Code", ConditionOperatorType.Equal, Code);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetMHASectionSetupByID(Guid MHASectionSetupId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, MHASectionSetupId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteMHASectionSetup(Guid MHASectionSetupID)
        {
            this.DeleteRecord(tableName, MHASectionSetupID);
        }
    }
}
