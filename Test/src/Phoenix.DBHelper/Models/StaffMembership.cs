using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class StaffMembership : BaseClass
    {

        public string TableName = "StaffMembership";
        public string PrimaryKeyName = "staffmembershipid";


        public StaffMembership()
        {
            AuthenticateUser();
        }

        public StaffMembership(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateStaffMembership(Guid ownerid, string title, Guid providerid, Guid professionalid, Guid professionaltypeid, DateTime datejoined)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            //AddFieldToBusinessDataObject(dataObject, "name", Name);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "title", title);
            AddFieldToBusinessDataObject(dataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(dataObject, "professionalid", professionalid);
            AddFieldToBusinessDataObject(dataObject, "professionaltypeid", professionaltypeid);

            AddFieldToBusinessDataObject(dataObject, "datejoined", datejoined);


            return this.CreateRecord(dataObject);
        }



        public List<Guid> GetByproviderid(Guid providerid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerid", ConditionOperatorType.Equal, providerid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


    }
}
