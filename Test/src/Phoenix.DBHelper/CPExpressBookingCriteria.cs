﻿using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CareDirector.Sdk.Query;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DBHelper.Models
{
   

    public class CPExpressBookingCriteria : BaseClass
    {

        public string TableName = "cpexpressbookingcriteria";
        public string PrimaryKeyName = " cpexpressbookingcriteriaid";

        public CPExpressBookingCriteria(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCPExpressBookingCriteria(Guid ownerid, Guid owningbusinessunitid,string title,int statusid,Guid? providerid,DateTime bookingrunstartdate,DateTime bookingrunenddate,DateTime scheduledjobstart,Guid regardingid,string regardingtablename,string regardingidname)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "bookingrunstartdate", bookingrunstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "bookingrunenddate", bookingrunenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "scheduledjobstart", scheduledjobstart);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingtablename", regardingtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidname", regardingidname);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
           

            return this.CreateRecord(buisinessDataObject);
        }

        public void UpdateStatus(Guid cpexpressbookingcriteriaid, int statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, cpexpressbookingcriteriaid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByRegardingID(Guid regardingid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "regardingid", ConditionOperatorType.Equal, regardingid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
