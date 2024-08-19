using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RecurrencePattern : BaseClass
    {

        public string TableName = "RecurrencePattern";
        public string PrimaryKeyName = "RecurrencePatternId";


        public RecurrencePattern()
        {
            AuthenticateUser();
        }

        public RecurrencePattern(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateRecurrencePattern(int RecurrencePatternTypeId, int RecurrenceDay)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "RecurrencePatternTypeId", RecurrencePatternTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "RecurrenceDay", RecurrenceDay);
            AddFieldToBusinessDataObject(buisinessDataObject, "Monday", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "Tuesday", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "Wednesday", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "Thursday", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "Friday", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "Saturday", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "Sunday", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "January", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "February", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "March", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "April", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "May", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "June", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "July", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "August", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "September", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "October", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "November", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "December", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", 0);


            return this.CreateRecord(buisinessDataObject);
        }


        public List<Guid> GetByTitle(string title)

        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);


            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, title);


            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetRecurrencePatternByID(Guid RecurrencePatternId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, RecurrencePatternId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetRecurrencePatternIdByName(string RecurrencePatternName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Title", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, RecurrencePatternName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetRecurrencePatternByName(string name, Guid regardinguserid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);
            this.BaseClassAddTableCondition(query, "regardinguserid", ConditionOperatorType.Equal, regardinguserid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public void DeleteRecurrencePattern(Guid RecurrencePatternId)
        {
            this.DeleteRecord(TableName, RecurrencePatternId);
        }
    }
}
