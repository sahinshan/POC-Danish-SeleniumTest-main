using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ProtectiveMarkingScheme : BaseClass
    {
        private string TableName = "ProtectiveMarkingScheme";
        private string PrimaryKeyName = "ProtectiveMarkingSchemeId";

        public ProtectiveMarkingScheme()
        {
            AuthenticateUser();
        }

        public ProtectiveMarkingScheme(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateProtectiveMarkingScheme(string name, Guid ownerid, Guid owningbusinessunitid, bool? bold,
            bool? displaytext, bool? italic,
            int? textalignmentid, int? textlocationid, bool? underline, int? fontsizeid, int? fontstyleid, int? fontcolourid, bool? includesecurityheader, string pmsText = " ")
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "name", name);
            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(businessDataObject, "bold", bold);
            AddFieldToBusinessDataObject(businessDataObject, "displaytext", displaytext);
            AddFieldToBusinessDataObject(businessDataObject, "italic", italic);
            AddFieldToBusinessDataObject(businessDataObject, "pmstext", pmsText);
            AddFieldToBusinessDataObject(businessDataObject, "textalignmentid", textalignmentid);
            AddFieldToBusinessDataObject(businessDataObject, "textlocationid", textlocationid);
            AddFieldToBusinessDataObject(businessDataObject, "underline", underline);
            AddFieldToBusinessDataObject(businessDataObject, "fontsizeid", fontsizeid);
            AddFieldToBusinessDataObject(businessDataObject, "fontstyleid", fontstyleid);
            AddFieldToBusinessDataObject(businessDataObject, "fontcolourid", fontcolourid);
            AddFieldToBusinessDataObject(businessDataObject, "includesecurityheader", includesecurityheader);

            AddFieldToBusinessDataObject(businessDataObject, "validforexport", false);
            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);

            return this.CreateRecord(businessDataObject);
        }

        public Guid CreateProtectiveMarkingScheme(Guid protectivemarkingschemeid, string name, Guid ownerid, Guid owningbusinessunitid,
            bool? displaytext, bool? bold, bool? italic, bool? underline,
            int? textalignmentid, int? textlocationid, int? fontsizeid, int? fontstyleid, int? fontcolourid, bool? includesecurityheader, string pmsText = " ", bool inactive = false)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, PrimaryKeyName, protectivemarkingschemeid);
            AddFieldToBusinessDataObject(businessDataObject, "name", name);
            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(businessDataObject, "displaytext", displaytext);
            AddFieldToBusinessDataObject(businessDataObject, "bold", bold);
            AddFieldToBusinessDataObject(businessDataObject, "italic", italic);
            AddFieldToBusinessDataObject(businessDataObject, "underline", underline);
            AddFieldToBusinessDataObject(businessDataObject, "textalignmentid", textalignmentid);
            AddFieldToBusinessDataObject(businessDataObject, "textlocationid", textlocationid);
            AddFieldToBusinessDataObject(businessDataObject, "fontsizeid", fontsizeid);
            AddFieldToBusinessDataObject(businessDataObject, "fontstyleid", fontstyleid);
            AddFieldToBusinessDataObject(businessDataObject, "fontcolourid", fontcolourid);
            AddFieldToBusinessDataObject(businessDataObject, "includesecurityheader", includesecurityheader);
            AddFieldToBusinessDataObject(businessDataObject, "pmstext", pmsText);

            AddFieldToBusinessDataObject(businessDataObject, "validforexport", false);
            AddFieldToBusinessDataObject(businessDataObject, "inactive", inactive);

            return this.CreateRecord(businessDataObject);
        }

        public List<Guid> GetByName(string name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Dictionary<string, object> GetProtectiveMarkingSchemeById(Guid ProtectiveMarkingSchemeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ProtectiveMarkingSchemeId);

            foreach (string field in Fields)
                this.AddReturnField(query, TableName, field);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
