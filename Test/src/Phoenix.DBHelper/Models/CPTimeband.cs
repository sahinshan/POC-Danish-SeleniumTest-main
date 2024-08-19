using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPTimeband : BaseClass
    {

        public string TableName = "CPTimeband";
        public string PrimaryKeyName = "CPTimebandId";

        public CPTimeband()
        {
            AuthenticateUser();
        }

        public CPTimeband(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByTimebandSetid(Guid cptimebandsetid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "cptimebandsetid", ConditionOperatorType.Equal, cptimebandsetid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }


        public Guid CreateCPTimeband(Guid ownerid, Guid cptimebandsetid, int startdayid, TimeSpan starttime, int enddayid, TimeSpan endtime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "cptimebandsetid", cptimebandsetid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdayid", startdayid);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddayid", enddayid);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return CreateRecord(buisinessDataObject);
        }

        public void DeleteCPTimebandRecord(Guid CPTimebandId)
        {
            this.DeleteRecord(TableName, CPTimebandId);
        }
    }
}
