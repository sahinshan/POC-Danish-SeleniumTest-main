using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PrivateFosteringArrangement : BaseClass
    {

        private string tableName = "PrivateFosteringArrangement";
        private string primaryKeyName = "PrivateFosteringArrangementId";

        public PrivateFosteringArrangement()
        {
            AuthenticateUser();
        }

        public PrivateFosteringArrangement(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePrivateFosteringArrangement(Guid ownerid, Guid responsibleuserid, Guid caseid, Guid personid, DateTime notificationreceiveddate,
            DateTime? initialvisitdate = null, int? householdsuitableforprivatefosteringid = null, DateTime? notificationceasingdate = null,
            DateTime? privatefosteringstartdate = null, DateTime? privatfosteringenddate = null, string arrangementreason = "",
            int? hasreceivedactionforregulation4id = null, Guid? fostercareprohibitionnoticeid = null,
            DateTime? prohibitionnoticedate = null, DateTime? prohibitionnoticewithdrawaldate = null, Guid? fostercarerefusalnoticeid = null,
            DateTime? refusalnoticedate = null, DateTime? refusalnoticewithdrawaldate = null, DateTime? appeallodgeddate = null,
            DateTime? appealdetermineddate = null, Guid? fostercareappealoutcomeid = null)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "notificationreceiveddate", notificationreceiveddate);

            AddFieldToBusinessDataObject(dataObject, "initialvisitdate", initialvisitdate);
            AddFieldToBusinessDataObject(dataObject, "householdsuitableforprivatefosteringid", householdsuitableforprivatefosteringid);
            AddFieldToBusinessDataObject(dataObject, "notificationceasingdate", notificationceasingdate);
            AddFieldToBusinessDataObject(dataObject, "privatefosteringstartdate", privatefosteringstartdate);
            AddFieldToBusinessDataObject(dataObject, "privatfosteringenddate", privatfosteringenddate);
            AddFieldToBusinessDataObject(dataObject, "arrangementreason", arrangementreason);
            AddFieldToBusinessDataObject(dataObject, "hasreceivedactionforregulation4id", hasreceivedactionforregulation4id);
            AddFieldToBusinessDataObject(dataObject, "fostercareprohibitionnoticeid", fostercareprohibitionnoticeid);
            AddFieldToBusinessDataObject(dataObject, "prohibitionnoticedate", prohibitionnoticedate);
            AddFieldToBusinessDataObject(dataObject, "prohibitionnoticewithdrawaldate", prohibitionnoticewithdrawaldate);
            AddFieldToBusinessDataObject(dataObject, "fostercarerefusalnoticeid", fostercarerefusalnoticeid);
            AddFieldToBusinessDataObject(dataObject, "refusalnoticedate", refusalnoticedate);
            AddFieldToBusinessDataObject(dataObject, "refusalnoticewithdrawaldate", refusalnoticewithdrawaldate);
            AddFieldToBusinessDataObject(dataObject, "appeallodgeddate", appeallodgeddate);
            AddFieldToBusinessDataObject(dataObject, "appealdetermineddate", appealdetermineddate);
            AddFieldToBusinessDataObject(dataObject, "fostercareappealoutcomeid", fostercareappealoutcomeid);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetPrivateFosteringArrangementByCaseID(Guid caseid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "caseid", ConditionOperatorType.Equal, caseid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid PrivateFosteringArrangementId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PrivateFosteringArrangementId", ConditionOperatorType.Equal, PrivateFosteringArrangementId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePrivateFosteringArrangementId(Guid PrivateFosteringArrangementId)
        {
            this.DeleteRecord(tableName, PrivateFosteringArrangementId);
        }



    }
}
