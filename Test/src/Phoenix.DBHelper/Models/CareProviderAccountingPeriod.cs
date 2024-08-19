using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderAccountingPeriod : BaseClass
    {
        private string TableName = "CareProviderAccountingPeriod";
        private string PrimaryKeyName = "CareProviderAccountingPeriodId";

        public CareProviderAccountingPeriod()
        {
            AuthenticateUser();
        }

        public CareProviderAccountingPeriod(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateCareProviderAccountingPeriod(Guid ownerid, DateTime? periodfrom, DateTime? periodto,
            int? ClientChargesByPeriodCriteriaId, bool? currentperiod, bool? ispreviousperiod,
            bool? clientchargesincludevat)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            if (periodfrom.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "periodfrom", periodfrom);
            if (periodto.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "periodto", periodto);
            if (ClientChargesByPeriodCriteriaId.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "clientchargesbyperiodcriteriaid", ClientChargesByPeriodCriteriaId);
            if (currentperiod.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "currentperiod", currentperiod);
            if (ispreviousperiod.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "ispreviousperiod", ispreviousperiod);
            if (clientchargesincludevat.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "clientchargesincludevat", clientchargesincludevat);

            return this.CreateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetById(Guid CareProviderAccountingPeriodId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderAccountingPeriodId);

            foreach (string field in Fields)
                this.AddReturnField(query, TableName, field);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByCurrentPeriod(bool CurrentPeriod)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "currentperiod", ConditionOperatorType.Equal, CurrentPeriod);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByIsPreviousPeriod(bool IsPreviousPeriod)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ispreviousperiod", ConditionOperatorType.Equal, IsPreviousPeriod);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        //Get By Period From and Period To Date
        public List<Guid> GetByPeriodFromAndPeriodToDate(DateTime PeriodFrom, DateTime PeriodTo)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "periodfrom", ConditionOperatorType.Equal, PeriodFrom);
            this.BaseClassAddTableCondition(query, "periodto", ConditionOperatorType.Equal, PeriodTo);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public List<Guid> GetByPeriodFromAndPeriodToDateAndCurrentPeriod(DateTime PeriodFrom, DateTime PeriodTo, bool CurrentPeriod, bool IsPreviousPeriod)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "periodfrom", ConditionOperatorType.Equal, PeriodFrom);
            this.BaseClassAddTableCondition(query, "periodto", ConditionOperatorType.Equal, PeriodTo);
            this.BaseClassAddTableCondition(query, "currentperiod", ConditionOperatorType.Equal, CurrentPeriod);
            this.BaseClassAddTableCondition(query, "ispreviousperiod", ConditionOperatorType.Equal, IsPreviousPeriod);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void DeleteCareProviderAccountingPeriod(Guid CareProviderAccountingPeriodID)
        {
            this.DeleteRecord("CareProviderAccountingPeriod", CareProviderAccountingPeriodID);
        }

        public List<Guid> GetAll()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("careprovideraccountingperiodnumber", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
    }
}
