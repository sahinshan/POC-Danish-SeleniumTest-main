using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderVATCodeSetup : BaseClass
    {

        public string TableName = "careprovidervatcodesetup";
        public string PrimaryKeyName = "careprovidervatcodesetupId";

        public CareProviderVATCodeSetup()
        {
            AuthenticateUser();
        }

        public CareProviderVATCodeSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetByVATCodeAndVATReference(Guid vatcodeid, string vatreference)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "vatcodeid", ConditionOperatorType.Equal, vatcodeid);
            this.BaseClassAddTableCondition(query, "vatreference", ConditionOperatorType.Equal, vatreference);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Guid CreateCareProviderVATCodeSetup(Guid ownerid, Guid vatcodeid, decimal vatpercentage, string vatreference, DateTime startdate, DateTime? enddate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatcodeid", vatcodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatpercentage", vatpercentage);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatreference", vatreference);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);

            if (enddate.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate.Value);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }


    }
}
