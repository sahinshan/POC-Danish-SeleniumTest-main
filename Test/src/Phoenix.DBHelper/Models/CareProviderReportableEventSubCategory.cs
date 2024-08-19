using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{

    public class CareproviderReportableEventSubCategory : BaseClass
    {

        public string TableName = "CareproviderReportableEventSubCategory";
        public string PrimaryKeyName = "CareproviderReportableEventSubCategoryId";


        public CareproviderReportableEventSubCategory(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)

        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);

        }


        public Guid CreateCareProviderReportableEventSubCategoryRecord(string name, DateTime startdate, Guid careproviderreportableeventcategoryid, Guid ownerid)

        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderreportableeventcategoryid", careproviderreportableeventcategoryid);


            return this.CreateRecord(buisinessDataObject);
        }


        public Guid CreateCareProviderReportableSubEventCategoryRecord(string name, DateTime startdate, Guid careproviderreportableeventcategoryid, Guid ownerid, DateTime enddate)

        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderreportableeventcategoryid", careproviderreportableeventcategoryid);
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


        public void DeleteCareproviderReportableEventSubCategory(Guid CareproviderReportableEventSubCategoryId)
        {
            this.DeleteRecord(TableName, CareproviderReportableEventSubCategoryId);
        }







    }
}
