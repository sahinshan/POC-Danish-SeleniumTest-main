using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientWard : BaseClass
    {

        public string TableName = "InpatientWard";
        public string PrimaryKeyName = "InpatientWardId";


        public InpatientWard()
        {
            AuthenticateUser();
        }

        public InpatientWard(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetInpatientWardByTitle(string Title)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Title", ConditionOperatorType.Equal, Title);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetInpatientWardByTitle(string Title, Guid providerid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Title", ConditionOperatorType.Equal, Title);
            this.BaseClassAddTableCondition(query, "providerid", ConditionOperatorType.Equal, providerid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetInpatientWardByID(Guid InpatientWardId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, InpatientWardId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateInpatientWard(Guid ownerid, Guid providerid, Guid wardmanagerid, Guid wardspecialtyid, string title, DateTime startdate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);



            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(dataObject, "wardmangerid", wardmanagerid);
            AddFieldToBusinessDataObject(dataObject, "wardspecialtyid", wardspecialtyid);
            AddFieldToBusinessDataObject(dataObject, "title", title);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "seclusionward", 0);
            AddFieldToBusinessDataObject(dataObject, "enhancedobservationward", 0);



            return this.CreateRecord(dataObject);
        }

        public Guid UpdateInpatientWardWithSeclusionasYes(Guid ownerid, Guid providerid, Guid wardmanagerid, Guid wardspecialtyid, string title, DateTime startdate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);



            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(dataObject, "wardmangerid", wardmanagerid);
            AddFieldToBusinessDataObject(dataObject, "wardspecialtyid", wardspecialtyid);
            AddFieldToBusinessDataObject(dataObject, "title", title);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "seclusionward", 1);
            AddFieldToBusinessDataObject(dataObject, "enhancedobservationward", 0);



            return this.CreateRecord(dataObject);
        }

        public void UpdateInpatientWardSeclusions(Guid inpatientwardid, bool seclusionward)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, inpatientwardid);

            AddFieldToBusinessDataObject(dataObject, "seclusionward", seclusionward);

            this.UpdateRecord(dataObject);
        }


        public void DeleteInpatientWard(Guid InpatientWardId)
        {
            this.DeleteRecord(TableName, InpatientWardId);
        }
    }
}
