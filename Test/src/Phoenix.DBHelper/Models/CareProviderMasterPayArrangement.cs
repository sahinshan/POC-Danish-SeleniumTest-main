using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderMasterPayArrangement : BaseClass
    {
        private string TableName = "careprovidermasterpayarrangement";
        private string PrimaryKeyName = "careprovidermasterpayarrangementid";

        public CareProviderMasterPayArrangement()
        {
            AuthenticateUser();
        }

        public Guid CreateRecord(Guid TeamId, string Name, int CpPayrollUnitTypeId, decimal DefaultRate, bool AllowForHybridRates, 
            bool IsPayScheduledCareOnActuals, DateTime StartDate, DateTime? EndDate, bool IsDraft, 
            bool IsProviderAll, Dictionary<Guid,String> Providers, bool IsEmploymentContractTypeAll, Dictionary<Guid, String> EmploymentContractTypes, bool IsCpBookingTypeAll, Dictionary<Guid, String> BookingTypes,
            bool IsCareProviderStaffRoleTypeAll, Dictionary<Guid, String> CareProviderStaffRoleTypes,
            bool IsContractSchemeAll, Dictionary<Guid, String> ContractSchemes, bool IsPersonContractAll, bool IsSystemUserEmploymentContractAll, Dictionary<Guid, String> SystemUserEmploymentContracts,
            bool IsCpTimebandSetAll, Dictionary<Guid, String> CpTimebandSets, bool applyminbookinglength = false, bool applymaxbookinglength = false)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(businessDataObject, "teamid", TeamId);
            this.AddFieldToBusinessDataObject(businessDataObject, "name", Name);
            this.AddFieldToBusinessDataObject(businessDataObject, "cppayrollunittypeid", CpPayrollUnitTypeId);
            this.AddFieldToBusinessDataObject(businessDataObject, "defaultrate", DefaultRate);
            this.AddFieldToBusinessDataObject(businessDataObject, "allowforhybridrates", AllowForHybridRates);
            this.AddFieldToBusinessDataObject(businessDataObject, "ispayscheduledcareonactuals", IsPayScheduledCareOnActuals);
            this.AddFieldToBusinessDataObject(businessDataObject, "startdate", StartDate);
            if(EndDate.HasValue)
                this.AddFieldToBusinessDataObject(businessDataObject, "enddate", EndDate);
            this.AddFieldToBusinessDataObject(businessDataObject, "isdraft", IsDraft);
            
            this.AddFieldToBusinessDataObject(businessDataObject, "isproviderall", IsProviderAll);
            businessDataObject.MultiSelectBusinessObjectFields["providerid"] = new MultiSelectBusinessObjectDataCollection();
            if (Providers != null && Providers.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> kvp in Providers)
                {

                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = kvp.Key,
                        ReferenceIdTableName = "Provider",
                        ReferenceName = kvp.Value
                    };
                    businessDataObject.MultiSelectBusinessObjectFields["providerid"].Add(dataRecord);
                }
            }

            this.AddFieldToBusinessDataObject(businessDataObject, "isemploymentcontracttypeall", IsEmploymentContractTypeAll);
            businessDataObject.MultiSelectBusinessObjectFields["employmentcontracttypeid"] = new MultiSelectBusinessObjectDataCollection();
            if (EmploymentContractTypes != null && EmploymentContractTypes.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> kvp in EmploymentContractTypes)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = kvp.Key,
                        ReferenceIdTableName = "EmploymentContractType",
                        ReferenceName = kvp.Value
                };
                    businessDataObject.MultiSelectBusinessObjectFields["employmentcontracttypeid"].Add(dataRecord);
                }
            }

            this.AddFieldToBusinessDataObject(businessDataObject, "iscpbookingtypeall", IsCpBookingTypeAll);
            businessDataObject.MultiSelectBusinessObjectFields["cpbookingtypeid"] = new MultiSelectBusinessObjectDataCollection();
            if (BookingTypes != null && BookingTypes.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> kvp in BookingTypes)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = kvp.Key,
                        ReferenceIdTableName = "CpBookingType",
                        ReferenceName = kvp.Value
                };
                    businessDataObject.MultiSelectBusinessObjectFields["cpbookingtypeid"].Add(dataRecord);
                }
            }

            this.AddFieldToBusinessDataObject(businessDataObject, "iscareproviderstaffroletypeall", IsCareProviderStaffRoleTypeAll);
            businessDataObject.MultiSelectBusinessObjectFields["careproviderstaffroletypeid"] = new MultiSelectBusinessObjectDataCollection();
            if (CareProviderStaffRoleTypes != null && CareProviderStaffRoleTypes.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> kvp in CareProviderStaffRoleTypes)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = kvp.Key,
                        ReferenceIdTableName = "CareProviderStaffRoleType",
                        ReferenceName = kvp.Value
                };
                    businessDataObject.MultiSelectBusinessObjectFields["careproviderstaffroletypeid"].Add(dataRecord);
                }
            }

            this.AddFieldToBusinessDataObject(businessDataObject, "iscontractschemeall", IsContractSchemeAll);
            businessDataObject.MultiSelectBusinessObjectFields["contractschemeid"] = new MultiSelectBusinessObjectDataCollection();
            if (ContractSchemes != null && ContractSchemes.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> kvp in ContractSchemes)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = kvp.Key,
                        ReferenceIdTableName = "CareProviderContractScheme",
                        ReferenceName = kvp.Value
                };
                    businessDataObject.MultiSelectBusinessObjectFields["contractschemeid"].Add(dataRecord);
                }
            }

            this.AddFieldToBusinessDataObject(businessDataObject, "ispersoncontractall", IsPersonContractAll);
            
            this.AddFieldToBusinessDataObject(businessDataObject, "issystemuseremploymentcontractall", IsSystemUserEmploymentContractAll);
            businessDataObject.MultiSelectBusinessObjectFields["systemuseremploymentcontractid"] = new MultiSelectBusinessObjectDataCollection();
            if (SystemUserEmploymentContracts != null && SystemUserEmploymentContracts.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> kvp in SystemUserEmploymentContracts)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = kvp.Key,
                        ReferenceIdTableName = "SystemUserEmploymentContract",
                        ReferenceName = kvp.Value
                };
                    businessDataObject.MultiSelectBusinessObjectFields["systemuseremploymentcontractid"].Add(dataRecord);
                }
            }

            this.AddFieldToBusinessDataObject(businessDataObject, "iscptimebandsetall", IsCpTimebandSetAll);
            businessDataObject.MultiSelectBusinessObjectFields["cptimebandsetid"] = new MultiSelectBusinessObjectDataCollection();
            if (CpTimebandSets != null && CpTimebandSets.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> kvp in CpTimebandSets)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = kvp.Key,
                        ReferenceIdTableName = "CpTimebandSet",
                        ReferenceName = kvp.Value
                };
                    businessDataObject.MultiSelectBusinessObjectFields["cptimebandsetid"].Add(dataRecord);
                }
            }

            this.AddFieldToBusinessDataObject(businessDataObject, "applyminbookinglength", applyminbookinglength);
            this.AddFieldToBusinessDataObject(businessDataObject, "applymaxbookinglength", applymaxbookinglength);

            return this.CreateRecord(businessDataObject);

        }

        public CareProviderMasterPayArrangement(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetById(Guid CareProviderMasterPayArrangementId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderMasterPayArrangementId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateRate(Guid CareProviderMasterPayArrangementId, decimal defaultrate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderMasterPayArrangementId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "defaultrate", defaultrate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void Delete(Guid CareProviderMasterPayArrangementID)
        {
            this.DeleteRecord(TableName, CareProviderMasterPayArrangementID);
        }

    }
}
