using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{

    public class CareproviderReportableEventCategory : BaseClass
    {

        public string TableName = "CareproviderReportableEventCategory";
        public string PrimaryKeyName = "CareproviderReportableEventCategoryId";


        public CareproviderReportableEventCategory(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)

        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);

        }


        public Guid CreateCareProviderReportableEventCategoryRecord(string name, DateTime startdate, Guid ownerid)

        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);



            return this.CreateRecord(buisinessDataObject);
        }


        public Guid CreateCareProviderReportableEventCategoryRecord(string name, DateTime startdate, Guid ownerid, DateTime enddate)

        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);


            return this.CreateRecord(buisinessDataObject);
        }
        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void DeleteCareproviderReportableEventCategory(Guid CareproviderReportableEventCategoryId)
        {
            this.DeleteRecord(TableName, CareproviderReportableEventCategoryId);
        }







    }
}
