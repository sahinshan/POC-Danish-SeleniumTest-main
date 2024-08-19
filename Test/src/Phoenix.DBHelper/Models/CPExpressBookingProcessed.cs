using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{


    public class CPExpressBookingProcessed : BaseClass
    {

        public string TableName = "cpexpressbookingprocessed";
        public string PrimaryKeyName = " cpexpressbookingprocessedid";

        public CPExpressBookingProcessed(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCPExpressBookingProcessed(Guid ownerid, Guid owningbusinessunitid, Guid regardingid, string regardingidtablename, string regardingidname, Guid cpbookingscheduleid, Guid cpexpressbookingcriteriaid, DateTime bookingrunstartdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);


            AddFieldToBusinessDataObject(buisinessDataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingscheduleid", cpbookingscheduleid);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpexpressbookingcriteriaid", cpexpressbookingcriteriaid);
            AddFieldToBusinessDataObject(buisinessDataObject, "bookingrunstartdate", bookingrunstartdate);



            return this.CreateRecord(buisinessDataObject);
        }


        public List<Guid> GetByCPExpressBookCriteriaId(Guid cpexpressbookingcriteriaId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "cpexpressbookingcriteriaId", ConditionOperatorType.Equal, cpexpressbookingcriteriaId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
