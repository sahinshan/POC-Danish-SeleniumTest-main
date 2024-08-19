using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ProviderForm : BaseClass
    {

        public string TableName = "ProviderForm";
        public string PrimaryKeyName = "ProviderFormId";


        public ProviderForm()
        {
            AuthenticateUser();
        }

        public ProviderForm(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateProviderFormRecord(Guid ownerid, Guid documentid, Guid providerid, DateTime startdate, int assessmentstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "documentid", documentid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "assessmentstatusid", assessmentstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "sdeexecuted", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "answersinitialized", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", 0);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetProviderFormByProviderID(Guid providerid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerid", ConditionOperatorType.Equal, providerid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetProviderFormByProviderID(Guid providerid, DateTime startdate)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerid", ConditionOperatorType.Equal, providerid);

            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.GreaterEqual, startdate.Date);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.LessEqual, startdate.Date.AddHours(23).AddMinutes(59));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetProviderFormsByProviderAndFormType(Guid providerid, Guid FormTypeID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerid", ConditionOperatorType.Equal, providerid);
            this.BaseClassAddTableCondition(query, "DocumentId", ConditionOperatorType.Equal, FormTypeID);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);


        }

        public List<Guid> GetProviderFormsByProviderAndFormType(Guid providerid, Guid FormTypeID, DateTime StartDate)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerid", ConditionOperatorType.Equal, providerid);
            this.BaseClassAddTableCondition(query, "DocumentId", ConditionOperatorType.Equal, FormTypeID);
            this.BaseClassAddTableCondition(query, "StartDate", ConditionOperatorType.Equal, StartDate);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Dictionary<string, object> GetProviderFormByID(Guid ProviderFormId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ProviderFormId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateStatus(Guid ProviderFormId, int assessmentstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ProviderFormId);

            AddFieldToBusinessDataObject(buisinessDataObject, "assessmentstatusid", assessmentstatusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteProviderForm(Guid ProviderFormId)
        {
            this.DeleteRecord(TableName, ProviderFormId);
        }
    }
}
