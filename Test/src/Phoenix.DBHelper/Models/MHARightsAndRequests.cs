using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MHARightsAndRequests : BaseClass
    {

        private string tableName = "MHARightsAndRequests";
        private string primaryKeyName = "MHARightsAndRequestsId";

        public MHARightsAndRequests()
        {
            AuthenticateUser();
        }

        public MHARightsAndRequests(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateMHARightsAndRequests(Guid ownerid, Guid personid, Guid caseid,
            DateTime createddateandtime,
            Guid? personmhalegalstatusid, Guid? wardmanagerid)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "createddateandtime", createddateandtime);
            AddFieldToBusinessDataObject(dataObject, "personmhalegalstatusid", personmhalegalstatusid);
            AddFieldToBusinessDataObject(dataObject, "wardmanagerid", wardmanagerid);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetMHARightsAndRequestsByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid MHARightsAndRequestsId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "MHARightsAndRequestsId", ConditionOperatorType.Equal, MHARightsAndRequestsId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteMHARightsAndRequests(Guid MHARightsAndRequestsId)
        {
            this.DeleteRecord(tableName, MHARightsAndRequestsId);
        }



    }
}
