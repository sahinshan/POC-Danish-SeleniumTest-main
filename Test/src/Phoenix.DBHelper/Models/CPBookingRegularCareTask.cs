using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPBookingRegularCareTask : BaseClass
    {

        private string tableName = "CPBookingRegularCareTask";
        private string primaryKeyName = "CPBookingRegularCareTaskId";

        public CPBookingRegularCareTask()
        {
            AuthenticateUser();
        }

        public CPBookingRegularCareTask(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByCPBookingRegularCareTaskId(Guid cpbookingregularcaretaskid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cpbookingtypeid", ConditionOperatorType.Equal, cpbookingregularcaretaskid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }



        public Guid CreateCPBookingRegularCareTask(Guid ownerid, Guid owningbusinessunitid, string title, Guid regularcaretaskid, int caretaskstatusid, Guid bookingid, Guid personid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);

            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "regularcaretaskid", regularcaretaskid);
            AddFieldToBusinessDataObject(buisinessDataObject, "caretaskstatusid", caretaskstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "bookingid", bookingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);




            return this.CreateRecord(buisinessDataObject);
        }


    }
}
