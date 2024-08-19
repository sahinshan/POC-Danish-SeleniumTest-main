using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class UserDataView : BaseClass
    {
        public string TableName { get { return "UserDataView"; } }
        public string PrimaryKeyName { get { return "UserDataViewid"; } }


        public UserDataView()
        {
            AuthenticateUser();
        }

        public UserDataView(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateUserDataView(Guid userdataviewid, Guid businessobjectid, Guid owninguserid, string name, string dataqueryxml, string layoutxml, string conditionbuilderxml, Guid? firstsortfieldid, Guid? secondsortfieldid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "userdataviewid", userdataviewid);
            AddFieldToBusinessDataObject(buisinessDataObject, "businessobjectid", businessobjectid);
            AddFieldToBusinessDataObject(buisinessDataObject, "owninguserid", owninguserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "dataqueryxml", dataqueryxml);
            AddFieldToBusinessDataObject(buisinessDataObject, "layoutxml", layoutxml);
            AddFieldToBusinessDataObject(buisinessDataObject, "conditionbuilderxml", conditionbuilderxml);
            AddFieldToBusinessDataObject(buisinessDataObject, "firstsortfieldid", firstsortfieldid);
            AddFieldToBusinessDataObject(buisinessDataObject, "secondsortfieldid", secondsortfieldid);
            AddFieldToBusinessDataObject(buisinessDataObject, "firstsortascending", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "secondsortascending", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", 0);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetUserDataViewByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetUserDataViewByID(Guid UserDataViewId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("UserDataView", false, "UserDataViewId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "UserDataViewId", ConditionOperatorType.Equal, UserDataViewId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateUserDataView(Guid UserDataViewId, bool AutoRefresh, int? RefreshTime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, UserDataViewId);
            AddFieldToBusinessDataObject(buisinessDataObject, "AutoRefresh", AutoRefresh);
            AddFieldToBusinessDataObject(buisinessDataObject, "RefreshTime", RefreshTime);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
