using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SignificantEventSubCategory : BaseClass
    {

        public string TableName = "SignificantEventSubCategory";
        public string PrimaryKeyName = "SignificantEventSubCategoryId";

        public SignificantEventSubCategory()
        {
            AuthenticateUser();
        }

        public SignificantEventSubCategory(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateSignificantEventSubCategory(string Name, DateTime StartDate, Guid SignificantEventCategoryId, Guid OwnerId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "SignificantEventCategoryId", SignificantEventCategoryId);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetSignificantEventSubCategoryByID(Guid SignificantEventSubCategoryId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SignificantEventSubCategoryId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> SignificantEventSubCategoryByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> SignificantEventSubCategoryByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateSignificantEventSubCategory(Guid ownerId, string name,
            Guid SignificantEventCategoryId, DateTime startdate, string code, string govcode, bool? enddate = null, bool validforexport = false, bool inactive = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "significanteventcategoryid", SignificantEventCategoryId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "govcode", govcode);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", validforexport);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);

            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteSignificantEventSubCategory(Guid SignificantEventSubCategoryId)
        {
            this.DeleteRecord(TableName, SignificantEventSubCategoryId);
        }

    }
}
