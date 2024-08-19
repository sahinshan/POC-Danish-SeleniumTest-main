using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPBookingRegularCareTaskStaff : BaseClass
    {

        private string tableName = "CPBookingRegularCareTaskStaff";
        private string primaryKeyName = "CPBookingRegularCareTaskStaffId";

        public CPBookingRegularCareTaskStaff()
        {
            AuthenticateUser();
        }

        public CPBookingRegularCareTaskStaff(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByCPBookingRegularCareTaskId(Guid cpbookingregularcaretaskid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cpbookingregularcaretaskid", ConditionOperatorType.Equal, cpbookingregularcaretaskid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }



        public Guid CreateCPBookingRegularCareTaskStaff(Guid cpbookingregularcaretaskid, Guid cpbookingdiarystaffid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);


            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingregularcaretaskid", cpbookingregularcaretaskid);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingdiarystaffid", cpbookingdiarystaffid);

            return this.CreateRecord(buisinessDataObject);
        }


    }
}
