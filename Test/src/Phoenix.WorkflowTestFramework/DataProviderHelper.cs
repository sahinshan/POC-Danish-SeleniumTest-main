//using CareDirector.Sdk.Enums;
//using CareDirector.Sdk.SystemEntities;
//using CareWorks.Foundation.Enums;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Phoenix.WorkflowTestFramework
//{
//    public partial class PhoenixPlatformServiceHelper
//    {


//        public CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest GetRetrieveMultipleRequest(string BusinessObjectName, Guid DataViewId)
//        {
//            return new CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest
//            {
//                BusinessObjectName = BusinessObjectName,
//                DataViewId = DataViewId,
//                IncludeFormattedData = true,
//                PageNumber = 1,
//                RecordsPerPage = 100,
//                UsePaging = true,
//                ViewFor = ViewForType.Unknown,
//                ViewGroup = DataViewGroup.System,
//                ViewType = DataViewType.Main,
//            };

//        }


//        public BusinessData GetBusinessDataInstance(string BusinessObjectName, string PrimaryKeyName)
//        {
//            BusinessData businessData = new BusinessData()
//            {
//                BusinessObjectName = BusinessObjectName,
//                MultiSelectBusinessObjectFields = new MultiSelectBusinessObjectDataDictionary(),
//                MultiSelectOptionSetFields = new MultiSelectOptionSetDataDictionary(),
//                PrimaryKeyName = PrimaryKeyName,
//            };

//            return businessData;
//        }


//        public RecordLevelAccess GetRecordLevelAccess(Guid RecordID, string RecordTableName, Guid ShareToId, string ShareToIdName, string ShareToIdTableName, bool CanView = true, bool CanEdit = false, bool CanDelete = false, bool CanShare = false)
//        {
//            return new RecordLevelAccess
//            {
//                RecordId = RecordID,
//                RecordIdTableName = RecordTableName,
//                CanView = CanView,
//                CanEdit = CanEdit,
//                CanDelete = CanDelete,
//                CanShare = CanShare,
//                ShareToId = ShareToId,
//                ShareToIdName = ShareToIdName,
//                ShareToIdTableName = ShareToIdTableName
//            };
//        }



//        #region Phone Call

//        public Guid CreatePhoneCallRecordForPerson(string Subject, string Description,
//            Guid CallerID, string CallerIdTableName, string CallerIDName,
//            Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
//            string PhoneNumber,
//            Guid RegardingID, string RegardingIDName, Guid Ownerid, string RegardingIdTableName = "person")
//        {

//            return CreatePhoneCallRecordForPerson(Subject, Description, CallerID, CallerIdTableName, CallerIDName, RecipientId, RecipientIdTableName, RecipientIdName, PhoneNumber, RegardingID, RegardingIDName, Ownerid, DateTime.Now.WithoutMilliseconds(), RegardingIdTableName);
//        }


//        public Guid CreatePhoneCallRecordForPerson(string Subject, string Description,
//            Guid CallerID, string CallerIdTableName, string CallerIDName,
//            Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
//            string PhoneNumber,
//            Guid RegardingID, string RegardingIDName, Guid Ownerid, DateTime? PhoneCallDate, string RegardingIdTableName = "person")
//        {
//            var businessDataObject = GetBusinessDataInstance("PhoneCall", "PhoneCallId");

//            businessDataObject.FieldCollection.Add("Subject", Subject);
//            businessDataObject.FieldCollection.Add("Notes", Description);

//            businessDataObject.FieldCollection.Add("CallerId", CallerID);
//            businessDataObject.FieldCollection.Add("CallerIdTableName", CallerIdTableName);
//            businessDataObject.FieldCollection.Add("CallerIdName", CallerIDName);

//            businessDataObject.FieldCollection.Add("RecipientId", RecipientId);
//            businessDataObject.FieldCollection.Add("RecipientIdTableName", RecipientIdTableName);
//            businessDataObject.FieldCollection.Add("RecipientIdName", RecipientIdName);

//            businessDataObject.FieldCollection.Add("RegardingId", RegardingID);
//            businessDataObject.FieldCollection.Add("RegardingIdTableName", RegardingIdTableName);
//            businessDataObject.FieldCollection.Add("RegardingIdName", RegardingIDName);

//            businessDataObject.FieldCollection.Add("OwnerId", Ownerid);

//            businessDataObject.FieldCollection.Add("Inactive", false);
//            businessDataObject.FieldCollection.Add("InformationByThirdParty", false);
//            businessDataObject.FieldCollection.Add("IsSignificantEvent", false);
//            businessDataObject.FieldCollection.Add("IsCaseNote", false);

//            businessDataObject.FieldCollection.Add("PhoneNumber", PhoneNumber);

//            businessDataObject.FieldCollection.Add("DirectionId", 1);
//            businessDataObject.FieldCollection.Add("StatusId", 1);

//            //businessDataObject.FieldCollection.Add("PersonID", RegardingID });

//            if (PhoneCallDate.HasValue)
//                businessDataObject.FieldCollection.Add("PhoneCallDate", PhoneCallDate.Value);
//            else
//                businessDataObject.FieldCollection.Add("PhoneCallDate", null);



//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;
//        }

//        public Guid CreatePhoneCallRecordForCase(string Subject, string Description,
//            Guid CallerID, string CallerIdTableName, string CallerIDName,
//            Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
//            string PhoneNumber,
//            Guid RegardingID, string RegardingIDName, Guid Ownerid, DateTime? PhoneCallDate)
//        {
//            var businessDataObject = GetBusinessDataInstance("PhoneCall", "PhoneCallId");

//            businessDataObject.FieldCollection.Add("Subject", Subject);
//            businessDataObject.FieldCollection.Add("Notes", Description);

//            businessDataObject.FieldCollection.Add("CallerId", CallerID);
//            businessDataObject.FieldCollection.Add("CallerIdTableName", CallerIdTableName);
//            businessDataObject.FieldCollection.Add("CallerIdName", CallerIDName);

//            businessDataObject.FieldCollection.Add("RecipientId", RecipientId);
//            businessDataObject.FieldCollection.Add("RecipientIdTableName", RecipientIdTableName);
//            businessDataObject.FieldCollection.Add("RecipientIdName", RecipientIdName);

//            businessDataObject.FieldCollection.Add("RegardingId", RegardingID);
//            businessDataObject.FieldCollection.Add("RegardingIdTableName", "case");
//            businessDataObject.FieldCollection.Add("RegardingIdName", RegardingIDName);

//            businessDataObject.FieldCollection.Add("OwnerId", Ownerid);

//            businessDataObject.FieldCollection.Add("Inactive", false);
//            businessDataObject.FieldCollection.Add("InformationByThirdParty", false);
//            businessDataObject.FieldCollection.Add("IsSignificantEvent", false);
//            businessDataObject.FieldCollection.Add("IsCaseNote", false);

//            businessDataObject.FieldCollection.Add("PhoneNumber", PhoneNumber);

//            businessDataObject.FieldCollection.Add("DirectionId", 1);
//            businessDataObject.FieldCollection.Add("StatusId", 1);

//            businessDataObject.FieldCollection.Add("CaseID", RegardingID);
//            businessDataObject.FieldCollection.Add("personid", null);

//            if (PhoneCallDate.HasValue)
//                businessDataObject.FieldCollection.Add("PhoneCallDate", PhoneCallDate.Value);
//            else
//                businessDataObject.FieldCollection.Add("PhoneCallDate", null);



//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;
//        }

//        public Guid CreatePhoneCallRecordForCase(string Subject, string Description,
//           Guid CallerID, string CallerIdTableName, string CallerIDName,
//           Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
//           string PhoneNumber,
//           Guid RegardingID, string RegardingIDName, Guid PersonID, Guid Ownerid, DateTime? PhoneCallDate, Guid? ResponsibleUser)
//        {
//            var businessDataObject = GetBusinessDataInstance("PhoneCall", "PhoneCallId");

//            businessDataObject.FieldCollection.Add("Subject", Subject);
//            businessDataObject.FieldCollection.Add("Notes", Description);

//            businessDataObject.FieldCollection.Add("CallerId", CallerID);
//            businessDataObject.FieldCollection.Add("CallerIdTableName", CallerIdTableName);
//            businessDataObject.FieldCollection.Add("CallerIdName", CallerIDName);

//            businessDataObject.FieldCollection.Add("RecipientId", RecipientId);
//            businessDataObject.FieldCollection.Add("RecipientIdTableName", RecipientIdTableName);
//            businessDataObject.FieldCollection.Add("RecipientIdName", RecipientIdName);

//            businessDataObject.FieldCollection.Add("RegardingId", RegardingID);
//            businessDataObject.FieldCollection.Add("RegardingIdTableName", "case");
//            businessDataObject.FieldCollection.Add("RegardingIdName", RegardingIDName);

//            businessDataObject.FieldCollection.Add("OwnerId", Ownerid);
//            businessDataObject.FieldCollection.Add("ResponsibleUserId", ResponsibleUser);

//            businessDataObject.FieldCollection.Add("Inactive", false);
//            businessDataObject.FieldCollection.Add("InformationByThirdParty", false);
//            businessDataObject.FieldCollection.Add("IsSignificantEvent", false);
//            businessDataObject.FieldCollection.Add("IsCaseNote", false);

//            businessDataObject.FieldCollection.Add("PhoneNumber", PhoneNumber);

//            businessDataObject.FieldCollection.Add("DirectionId", 1);
//            businessDataObject.FieldCollection.Add("StatusId", 1);

//            businessDataObject.FieldCollection.Add("CaseID", RegardingID);

//            businessDataObject.FieldCollection.Add("PersonID", PersonID);

//            if (PhoneCallDate.HasValue)
//                businessDataObject.FieldCollection.Add("PhoneCallDate", PhoneCallDate.Value);
//            else
//                businessDataObject.FieldCollection.Add("PhoneCallDate", null);



//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;
//        }

//        public Guid CreatePhoneCallRecordForCase(string Subject, string Description,
//           Guid CallerID, string CallerIdTableName, string CallerIDName,
//           Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
//           string PhoneNumber,
//           Guid RegardingID, string RegardingIDName, Guid PersonID, string PersonName, Guid Ownerid, string OwnerIDName,
//           DateTime? PhoneCallDate, Guid? ResponsibleUser, string ResponsibleUserName)
//        {
//            var businessDataObject = GetBusinessDataInstance("PhoneCall", "PhoneCallId");

//            businessDataObject.FieldCollection.Add("Subject", Subject);
//            businessDataObject.FieldCollection.Add("Notes", Description);

//            businessDataObject.FieldCollection.Add("CallerId", CallerID);
//            businessDataObject.FieldCollection.Add("CallerIdTableName", CallerIdTableName);
//            businessDataObject.FieldCollection.Add("CallerIdName", CallerIDName);

//            businessDataObject.FieldCollection.Add("RecipientId", RecipientId);
//            businessDataObject.FieldCollection.Add("RecipientIdTableName", RecipientIdTableName);
//            businessDataObject.FieldCollection.Add("RecipientIdName", RecipientIdName);

//            businessDataObject.FieldCollection.Add("RegardingId", RegardingID);
//            businessDataObject.FieldCollection.Add("RegardingIdTableName", "case");
//            businessDataObject.FieldCollection.Add("RegardingIdName", RegardingIDName);

//            businessDataObject.FieldCollection.Add("OwnerId", Ownerid);
//            businessDataObject.FieldCollection.Add("ownerid_cwname", OwnerIDName);
//            businessDataObject.FieldCollection.Add("ResponsibleUserId", ResponsibleUser);
//            businessDataObject.FieldCollection.Add("responsibleuserid_cwname", ResponsibleUserName);

//            businessDataObject.FieldCollection.Add("Inactive", false);
//            businessDataObject.FieldCollection.Add("InformationByThirdParty", false);
//            businessDataObject.FieldCollection.Add("IsSignificantEvent", false);
//            businessDataObject.FieldCollection.Add("IsCaseNote", false);

//            businessDataObject.FieldCollection.Add("PhoneNumber", PhoneNumber);

//            businessDataObject.FieldCollection.Add("DirectionId", 1);
//            businessDataObject.FieldCollection.Add("StatusId", 1);

//            businessDataObject.FieldCollection.Add("CaseID", RegardingID);

//            businessDataObject.FieldCollection.Add("PersonID", PersonID);
//            businessDataObject.FieldCollection.Add("personid_cwname", PersonName);

//            if (PhoneCallDate.HasValue)
//                businessDataObject.FieldCollection.Add("PhoneCallDate", PhoneCallDate.Value);
//            else
//                businessDataObject.FieldCollection.Add("PhoneCallDate", null);



//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;
//        }

//        public Guid CreatePhoneCallRecordForPerson(string Subject, string Description,
//            Guid CallerID, string CallerIdTableName, string CallerIDName,
//            Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
//            string PhoneNumber,
//            Guid RegardingID, string RegardingIDName, Guid Ownerid, DateTime? PhoneCallDate, Guid? ResponsibleUser,
//            Guid? ActivityCategoryID = null, bool InformationByThirdParty = false, int DirectionId = 1,
//            bool IsSignificantEvent = false, DateTime? SignificantEventDate = null, Guid? SignificantEventCategoryId = null, bool IsCaseNote = false)
//        {
//            var businessDataObject = GetBusinessDataInstance("PhoneCall", "PhoneCallId");

//            businessDataObject.FieldCollection.Add("Subject", Subject);
//            businessDataObject.FieldCollection.Add("Notes", Description);

//            businessDataObject.FieldCollection.Add("CallerId", CallerID);
//            businessDataObject.FieldCollection.Add("CallerIdTableName", CallerIdTableName);
//            businessDataObject.FieldCollection.Add("CallerIdName", CallerIDName);

//            businessDataObject.FieldCollection.Add("RecipientId", RecipientId);
//            businessDataObject.FieldCollection.Add("RecipientIdTableName", RecipientIdTableName);
//            businessDataObject.FieldCollection.Add("RecipientIdName", RecipientIdName);

//            businessDataObject.FieldCollection.Add("RegardingId", RegardingID);
//            businessDataObject.FieldCollection.Add("RegardingIdTableName", "person");
//            businessDataObject.FieldCollection.Add("RegardingIdName", RegardingIDName);

//            businessDataObject.FieldCollection.Add("OwnerId", Ownerid);
//            businessDataObject.FieldCollection.Add("ResponsibleUserId", ResponsibleUser);

//            businessDataObject.FieldCollection.Add("Inactive", false);
//            businessDataObject.FieldCollection.Add("InformationByThirdParty", InformationByThirdParty);
//            businessDataObject.FieldCollection.Add("IsSignificantEvent", IsSignificantEvent);
//            businessDataObject.FieldCollection.Add("IsCaseNote", IsCaseNote);

//            businessDataObject.FieldCollection.Add("PhoneNumber", PhoneNumber);

//            businessDataObject.FieldCollection.Add("DirectionId", DirectionId);
//            businessDataObject.FieldCollection.Add("StatusId", 1);

//            businessDataObject.FieldCollection.Add("PersonID", RegardingID);
//            businessDataObject.FieldCollection.Add("PersonID_cwname", RegardingIDName);

//            if (PhoneCallDate.HasValue)
//                businessDataObject.FieldCollection.Add("PhoneCallDate", PhoneCallDate.Value);
//            else
//                businessDataObject.FieldCollection.Add("PhoneCallDate", null);

//            businessDataObject.FieldCollection.Add("ActivityCategoryId", ActivityCategoryID);

//            if (SignificantEventDate.HasValue)
//                businessDataObject.FieldCollection.Add("SignificantEventDate", SignificantEventDate.Value);
//            else
//                businessDataObject.FieldCollection.Add("SignificantEventDate", null);

//            if (SignificantEventCategoryId.HasValue)
//                businessDataObject.FieldCollection.Add("SignificantEventCategoryId", SignificantEventCategoryId.Value);


//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(string.Format("{0} - {1} - {2}", response.Exception.ErrorCode, response.Exception.Message, response.Exception.InnerExceptionMessage));

//            return response.Id.Value;
//        }

//        public Guid CreatePhoneCallRecordForPerson(string Subject, string Description,
//            Guid CallerID, string CallerIdTableName, string CallerIDName,
//            Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
//            string PhoneNumber,
//            Guid RegardingID, string RegardingIDName, Guid Ownerid, DateTime? PhoneCallDate, Guid? ResponsibleUser, string ResponsibleUserName,
//            Guid? ActivityCategoryID = null, bool InformationByThirdParty = false, int DirectionId = 1,
//            bool IsSignificantEvent = false, DateTime? SignificantEventDate = null, Guid? SignificantEventCategoryId = null, bool IsCaseNote = false)
//        {
//            var businessDataObject = GetBusinessDataInstance("PhoneCall", "PhoneCallId");

//            businessDataObject.FieldCollection.Add("Subject", Subject);
//            businessDataObject.FieldCollection.Add("Notes", Description);

//            businessDataObject.FieldCollection.Add("CallerId", CallerID);
//            businessDataObject.FieldCollection.Add("CallerIdTableName", CallerIdTableName);
//            businessDataObject.FieldCollection.Add("CallerIdName", CallerIDName);

//            businessDataObject.FieldCollection.Add("RecipientId", RecipientId);
//            businessDataObject.FieldCollection.Add("RecipientIdTableName", RecipientIdTableName);
//            businessDataObject.FieldCollection.Add("RecipientIdName", RecipientIdName);

//            businessDataObject.FieldCollection.Add("RegardingId", RegardingID);
//            businessDataObject.FieldCollection.Add("RegardingIdTableName", "person");
//            businessDataObject.FieldCollection.Add("RegardingIdName", RegardingIDName);

//            businessDataObject.FieldCollection.Add("OwnerId", Ownerid);
//            businessDataObject.FieldCollection.Add("ResponsibleUserId", ResponsibleUser);
//            businessDataObject.FieldCollection.Add("ResponsibleUserId_cwname", ResponsibleUserName);

//            businessDataObject.FieldCollection.Add("Inactive", false);
//            businessDataObject.FieldCollection.Add("InformationByThirdParty", InformationByThirdParty);
//            businessDataObject.FieldCollection.Add("IsSignificantEvent", IsSignificantEvent);
//            businessDataObject.FieldCollection.Add("IsCaseNote", IsCaseNote);

//            businessDataObject.FieldCollection.Add("PhoneNumber", PhoneNumber);

//            businessDataObject.FieldCollection.Add("DirectionId", DirectionId);
//            businessDataObject.FieldCollection.Add("StatusId", 1);

//            businessDataObject.FieldCollection.Add("PersonID", RegardingID);
//            businessDataObject.FieldCollection.Add("PersonID_cwname", RegardingIDName);

//            if (PhoneCallDate.HasValue)
//                businessDataObject.FieldCollection.Add("PhoneCallDate", PhoneCallDate.Value);
//            else
//                businessDataObject.FieldCollection.Add("PhoneCallDate", null);

//            businessDataObject.FieldCollection.Add("ActivityCategoryId", ActivityCategoryID);

//            if (SignificantEventDate.HasValue)
//                businessDataObject.FieldCollection.Add("SignificantEventDate", SignificantEventDate.Value);
//            else
//                businessDataObject.FieldCollection.Add("SignificantEventDate", null);

//            if (SignificantEventCategoryId.HasValue)
//                businessDataObject.FieldCollection.Add("SignificantEventCategoryId", SignificantEventCategoryId.Value);


//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(string.Format("{0} - {1} - {2}", response.Exception.ErrorCode, response.Exception.Message, response.Exception.InnerExceptionMessage));

//            return response.Id.Value;
//        }




//        public void UpdatePhoneCallSubject(Guid PhoneCallID, string subject)
//        {
//            var businessDataObject = GetBusinessDataInstance("PhoneCall", "PhoneCallId");

//            businessDataObject.FieldCollection.Add("PhoneCallId", PhoneCallID);
//            businessDataObject.FieldCollection.Add("subject", subject);

//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        public void RestrictPhoneCall(Guid PhoneCallID, Guid DataRestrictionID)
//        {
//            var updateResponse = DataProvider.RestrictRecords(PhoneCallID, "phonecall", DataRestrictionID, AssignDataRestrictionOptionType.All);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        public void UpdatePhoneCallRecordDescription(Guid PhoneCallID, string Description)
//        {
//            var businessDataObject = GetBusinessDataInstance("PhoneCall", "PhoneCallId");

//            businessDataObject.FieldCollection.Add("PhoneCallId", PhoneCallID);
//            businessDataObject.FieldCollection.Add("Notes", Description);

//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        public void UpdatePhoneCallRecordStatus(Guid PhoneCallID, int NewStatus, bool Inactive)
//        {
//            var businessDataObject = GetBusinessDataInstance("PhoneCall", "PhoneCallId");

//            businessDataObject.FieldCollection.Add("PhoneCallId", PhoneCallID);
//            businessDataObject.FieldCollection.Add("StatusId", NewStatus);
//            businessDataObject.FieldCollection.Add("Inactive", Inactive);

//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        public void UpdatePhoneCallDateField(Guid PhoneCallID, DateTime PhoneCallDate)
//        {
//            var businessDataObject = GetBusinessDataInstance("PhoneCall", "PhoneCallId");

//            businessDataObject["phonecallid".ToLower()] = PhoneCallID;
//            businessDataObject["phonecalldate".ToLower()] = PhoneCallDate;

//            businessDataObject.SetChanged("phonecallid", false);
//            businessDataObject.SetChanged("phonecalldate", true);

//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        public void UpdatePhoneCallResponsibleTeam(Guid PhoneCallID, Guid ResponsibleTeam)
//        {
//            var businessDataObject = GetBusinessDataInstance("PhoneCall", "PhoneCallId");

//            businessDataObject.FieldCollection.Add("PhoneCallId", PhoneCallID);
//            businessDataObject.FieldCollection.Add("OwnerId", ResponsibleTeam);

//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        public void AssignPhoneCallRecords(Guid PhoneCallID, Guid ResponsibleTeam, Guid OwningBusinessUnitID, Guid? ResponsibleUser)
//        {
//            CareDirector.Sdk.ServiceRequest.AssignRequest ar = new CareDirector.Sdk.ServiceRequest.AssignRequest
//            {
//                ActionType = CareDirector.Sdk.Enums.AssignDataRestrictionActionType.Save,
//                BusinessObjectId = new Guid("49353AAB-F3A5-E811-80DC-0050560502CC"),
//                BusinessObjectName = "phonecall",
//                RecordId = PhoneCallID,
//                OwnerId = ResponsibleTeam,
//                ResponsibleUserId = ResponsibleUser
//            };

//            CareDirector.Sdk.ServiceResponse.AssignResponse response = DataProvider.Assign(ar);


//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);
//        }

//        public void UpdatePhoneCallResponsibleUser(Guid PhoneCallID, Guid ResponsibleUser)
//        {
//            var businessDataObject = GetBusinessDataInstance("PhoneCall", "PhoneCallId");

//            businessDataObject.FieldCollection.Add("PhoneCallId", PhoneCallID);
//            businessDataObject.FieldCollection.Add("ResponsibleUserId", ResponsibleUser);

//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }


//        public List<Guid> GetPhoneCallForPersonRecord(Guid PersonID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PhoneCall", true, "PhoneCallId");
//            query.PrimaryKeyName = "PhoneCallId";

//            query.Filter.AddCondition("PhoneCall", "RegardingId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["PhoneCallId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public List<Guid> GetPhoneCallForPersonRecord(Guid PersonID, string subject)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PhoneCall", true, "PhoneCallId");
//            query.PrimaryKeyName = "PhoneCallId";

//            query.Filter.AddCondition("PhoneCall", "RegardingId", ConditionOperatorType.Equal, PersonID);
//            query.Filter.AddCondition("PhoneCall", "subject", ConditionOperatorType.Equal, subject);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["PhoneCallId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public List<Guid> GetPhoneCallForCaseRecord(Guid CaseID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PhoneCall", true, "PhoneCallId");
//            query.PrimaryKeyName = "PhoneCallId";

//            query.Filter.AddCondition("PhoneCall", "RegardingId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["PhoneCallId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public int GetPhoneCallStatus(Guid PhoneCallID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PhoneCall", true, "StatusId");
//            query.PrimaryKeyName = "PhoneCallId";

//            query.Filter.AddCondition("PhoneCall", "PhoneCallId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PhoneCallID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (int)c.FieldCollection["StatusId"]).FirstOrDefault();
//            else
//                return -1;
//        }

//        public bool GetPhoneCallInactiveField(Guid PhoneCallID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PhoneCall", true, "Inactive");
//            query.PrimaryKeyName = "PhoneCallId";

//            query.Filter.AddCondition("PhoneCall", "PhoneCallId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PhoneCallID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (bool)c.FieldCollection["Inactive"]).FirstOrDefault();
//            else
//                throw new Exception("Phone Call record not found");
//        }

//        public string GetPhoneCallSubjectField(Guid PhoneCallID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PhoneCall", true, "Subject");
//            query.PrimaryKeyName = "PhoneCallId";

//            query.Filter.AddCondition("PhoneCall", "PhoneCallId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PhoneCallID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (string)c.FieldCollection["Subject"]).FirstOrDefault();
//            else
//                throw new Exception("Phone Call record not found");
//        }

//        public string GetPhoneCallDescriptionField(Guid PhoneCallID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PhoneCall", true, "Notes");
//            query.PrimaryKeyName = "PhoneCallId";

//            query.Filter.AddCondition("PhoneCall", "PhoneCallId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PhoneCallID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (string)c.FieldCollection["Notes"]).FirstOrDefault();
//            else
//                throw new Exception("Phone Call record not found");
//        }

//        public NullSafeDictionary<string, object> GetPhoneCallByID(Guid PhoneCallID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PhoneCall", true, fields);
//            query.PrimaryKeyName = "PhoneCallId";

//            query.Filter.AddCondition("PhoneCall", "PhoneCallId", ConditionOperatorType.Equal, PhoneCallID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("Phone Call record not found");
//        }

//        public void DeletePhoneCall(Guid PhoneCallID)
//        {
//            //if the record is restricted, remove the restriction
//            DataProvider.RemoveDataRestriction(PhoneCallID, "phonecall", AssignDataRestrictionOptionType.All);

//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("PhoneCall", PhoneCallID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }


//        #endregion

//        #region Workflow

//        public List<Guid> GetWorkflowIdByWorkflowName(string WorkflowName)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Workflow", true, "WorkflowId");
//            query.PrimaryKeyName = "WorkflowId";

//            query.Filter.AddCondition("Workflow", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, WorkflowName);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["WorkflowId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region Workflow Job

//        public int CountWorkflowJobsForWorkflowID(Guid WorkflowID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("WorkflowJob", true, "WorkflowJobId");
//            query.PrimaryKeyName = "WorkflowJobId";

//            query.Filter.AddCondition("WorkflowJob", "WorkflowId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, WorkflowID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.BusinessDataCollection.Count;
//        }

//        public int CountWorkflowJobsForWorkflow(Guid WorkflowID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("WorkflowJob", true, "WorkflowJobId");
//            query.PrimaryKeyName = "WorkflowJobId";

//            query.Filter.AddCondition("WorkflowJob", "WorkflowId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, WorkflowID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.BusinessDataCollection.Count;
//        }

//        public List<Guid> GetWorkflowJobsForWorkflow(Guid WorkflowID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("WorkflowJob", true, "WorkflowJobId");
//            query.PrimaryKeyName = "WorkflowJobId";

//            query.Filter.AddCondition("WorkflowJob", "WorkflowId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, WorkflowID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["WorkflowJobId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public int CountWorkflowJobsForWorkflowIDAndTargetRecord(Guid WorkflowID, Guid RegardingID, string RegardingIdTableName)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("WorkflowJob", true, "WorkflowJobId");
//            query.PrimaryKeyName = "WorkflowJobId";

//            query.Filter.AddCondition("WorkflowJob", "WorkflowId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, WorkflowID);
//            query.Filter.AddCondition("WorkflowJob", "RegardingId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, RegardingID);
//            query.Filter.AddCondition("WorkflowJob", "RegardingIdTableName", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, RegardingIdTableName);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.BusinessDataCollection.Count;
//        }

//        public NullSafeDictionary<string, object> GetWorkflowJobForRecord(Guid RecordID, Guid WorkflowID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("WorkflowJob", "WorkflowJob", true, fields);
//            query.PrimaryKeyName = "WorkflowJobId";

//            query.Filter.AddCondition("WorkflowJob", "WorkflowId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, WorkflowID);
//            query.Filter.AddCondition("WorkflowJob", "RegardingId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, RecordID);

//            query.Orders.Add(new CareDirector.Sdk.Query.OrderBy("CreatedOn", SortOrder.Descending, "WorkflowJob"));

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("WorkflowJob record not found");
//        }

//        public NullSafeDictionary<string, object> GetWorkflowJobById(Guid WorkflowJobID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("WorkflowJob", "WorkflowJob", true, fields);
//            query.PrimaryKeyName = "WorkflowJobId";

//            query.Filter.AddCondition("WorkflowJob", "WorkflowJobID", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, WorkflowJobID);

//            query.Orders.Add(new CareDirector.Sdk.Query.OrderBy("CreatedOn", SortOrder.Descending, "WorkflowJob"));

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("WorkflowJob record not found");
//        }

//        public void WaitForWorkflowJobToFinish(Guid RecordID, Guid WorkflowID)
//        {
//            var fields = GetWorkflowJobForRecord(RecordID, WorkflowID, "StatusId", "CompletedOn", "CreatedOn");

//            int StatusId = (int)fields["StatusId".ToLower()];
//            DateTime? CompletedOn = fields.ContainsKey("CompletedOn".ToLower()) && fields["CompletedOn".ToLower()] != null ? (DateTime?)fields["CompletedOn".ToLower()] : null;
//            int count = 0;

//            while (StatusId != 3 || !CompletedOn.HasValue)
//            {
//                if (StatusId == 5) //WF finished with an error
//                    throw new Exception("The workflow job was not completed after 75 seconds.");

//                count++;
//                if (count > 120)
//                    throw new Exception("The workflow job was not completed after 75 seconds.");

//                System.Threading.Thread.Sleep(1000);

//                fields = GetWorkflowJobForRecord(RecordID, WorkflowID, "StatusId", "CompletedOn", "CreatedOn");
//                StatusId = (int)fields["StatusId".ToLower()];
//                CompletedOn = fields.ContainsKey("CompletedOn".ToLower()) && fields["CompletedOn".ToLower()] != null ? (DateTime?)fields["CompletedOn".ToLower()] : null;
//            }

//            System.Threading.Thread.Sleep(500);
//        }

//        public void DeleteWorkflowJob(Guid WorkflowJobID)
//        {
//            //if the record is restricted, remove the restriction
//            DataProvider.RemoveDataRestriction(WorkflowJobID, "WorkflowJob", AssignDataRestrictionOptionType.All);

//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("WorkflowJob", WorkflowJobID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region Contact

//        public Guid CreateContact(Guid OwnerId, Guid ContactTypeId, Guid ContactSourceId, Guid ContactReasonId, Guid ContactPresentingPriorityId, Guid ContactStatusId, Guid ContactOutcomeId, Guid ReferralPriorityId,
//            Guid RegardingId, string regardingidtablename, string regardingidname,
//            Guid ContactMadeById, string contactmadebyidtablename, string contactmadebyidname, string ContactMadeByFreetext, Guid CommunityAndClinicTeamId,
//            string PresentingNeed, string AdditionalInformation, string ContactSummary,
//            DateTime ContactReceivedDateTime, DateTime ContactAcceptedDateTime, DateTime ContactAssignedDateTime)
//        {
//            var businessDataObject = GetBusinessDataInstance("Contact", "ContactId");

//            businessDataObject.FieldCollection.Add("OwnerId", OwnerId);
//            businessDataObject.FieldCollection.Add("ContactTypeId", ContactTypeId);
//            businessDataObject.FieldCollection.Add("ContactSourceId", ContactSourceId);
//            businessDataObject.FieldCollection.Add("ContactReasonId", ContactReasonId);
//            businessDataObject.FieldCollection.Add("ContactPresentingPriorityId", ContactPresentingPriorityId);
//            businessDataObject.FieldCollection.Add("ContactStatusId", ContactStatusId);
//            businessDataObject.FieldCollection.Add("ContactOutcomeId", ContactOutcomeId);
//            businessDataObject.FieldCollection.Add("ReferralPriorityId", ReferralPriorityId);

//            businessDataObject.FieldCollection.Add("RegardingId", RegardingId);
//            businessDataObject.FieldCollection.Add("regardingidtablename", regardingidtablename);
//            businessDataObject.FieldCollection.Add("regardingidname", regardingidname);

//            businessDataObject.FieldCollection.Add("ContactMadeById", ContactMadeById);
//            businessDataObject.FieldCollection.Add("contactmadebyidtablename", contactmadebyidtablename);
//            businessDataObject.FieldCollection.Add("contactmadebyidname", contactmadebyidname);

//            businessDataObject.FieldCollection.Add("ContactMadeByFreetext", ContactMadeByFreetext);
//            businessDataObject.FieldCollection.Add("CommunityAndClinicTeamId", CommunityAndClinicTeamId);

//            businessDataObject.FieldCollection.Add("PersonGroupAwareOfContactId", 1);
//            businessDataObject.FieldCollection.Add("PersonGroupSupportContactId", 1);
//            businessDataObject.FieldCollection.Add("CarerAwareOfContactId", 1);
//            businessDataObject.FieldCollection.Add("CarerSupportContactId", 1);
//            businessDataObject.FieldCollection.Add("NextOfKinAwareOfContactId", 1);

//            businessDataObject.FieldCollection.Add("Inactive", false);

//            businessDataObject.FieldCollection.Add("PresentingNeed", PresentingNeed);
//            businessDataObject.FieldCollection.Add("AdditionalInformation", AdditionalInformation);
//            businessDataObject.FieldCollection.Add("ContactSummary", ContactSummary);

//            businessDataObject.FieldCollection.Add("ContactReceivedDateTime", ContactReceivedDateTime);
//            businessDataObject.FieldCollection.Add("ContactAcceptedDateTime", ContactAcceptedDateTime);
//            businessDataObject.FieldCollection.Add("ContactAssignedDateTime", ContactAssignedDateTime);

//            businessDataObject.FieldCollection.Add("ContactAdministrativeCategoryId", null);

//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;


//        }

//        public List<Guid> GetContactsForPerson(Guid PersonID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Contact", true, "ContactId");
//            query.PrimaryKeyName = "ContactId";

//            query.Filter.AddCondition("Contact", "RegardingId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["ContactId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public void DeleteContact(Guid ContactID)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("Contact", ContactID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region ContactType

//        public List<Guid> GetContactTypeByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("ContactType", true, "ContactTypeId");
//            query.PrimaryKeyName = "ContactTypeId";

//            query.Filter.AddCondition("ContactType", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["ContactTypeId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region ContactSource

//        public List<Guid> GetContactSourceByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("ContactSource", true, "ContactSourceId");
//            query.PrimaryKeyName = "ContactSourceId";

//            query.Filter.AddCondition("ContactSource", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["ContactSourceId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region CaseActionType

//        public List<Guid> GetCaseActionTypeByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("CaseActionType", true, "CaseActionTypeId");
//            query.PrimaryKeyName = "CaseActionTypeId";

//            query.Filter.AddCondition("CaseActionType", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["CaseActionTypeId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region ContactReason

//        public List<Guid> GetContactReasonByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("ContactReason", true, "ContactReasonId");
//            query.PrimaryKeyName = "ContactReasonId";

//            query.Filter.AddCondition("ContactReason", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["ContactReasonId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region ContactPresentingPriority

//        public List<Guid> GetContactPresentingPriorityByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("ContactPresentingPriority", true, "ContactPresentingPriorityId");
//            query.PrimaryKeyName = "ContactPresentingPriorityId";

//            query.Filter.AddCondition("ContactPresentingPriority", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["ContactPresentingPriorityId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region ContactStatus

//        public List<Guid> GetContactStatusByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("ContactStatus", true, "ContactStatusId");
//            query.PrimaryKeyName = "ContactStatusId";

//            query.Filter.AddCondition("ContactStatus", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["ContactStatusId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region ContactOutcome

//        public List<Guid> GetContactOutcomeByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("ContactOutcome", true, "ContactOutcomeId");
//            query.PrimaryKeyName = "ContactOutcomeId";

//            query.Filter.AddCondition("ContactOutcome", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["ContactOutcomeId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region ReferralPriority

//        public List<Guid> GetReferralPriorityByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("ReferralPriority", true, "ReferralPriorityId");
//            query.PrimaryKeyName = "ReferralPriorityId";

//            query.Filter.AddCondition("ReferralPriority", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["ReferralPriorityId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion


//        #region inpatient Enhanced Observation

//        public List<Guid> GetInpatientEnhancedObservationsByCaseId(Guid CaseId)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("InpatientEnhancedObservation", true, "InpatientEnhancedObservationId");
//            query.PrimaryKeyName = "InpatientEnhancedObservationId";

//            query.Filter.AddCondition("InpatientEnhancedObservation", "CaseId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseId);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["InpatientEnhancedObservationId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public NullSafeDictionary<string, object> GetInpatientEnhancedObservationById(Guid InpatientEnhancedObservationID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("InpatientEnhancedObservation", true, fields);
//            query.PrimaryKeyName = "InpatientEnhancedObservationId";

//            query.Filter.AddCondition("InpatientEnhancedObservation", "InpatientEnhancedObservationId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, InpatientEnhancedObservationID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("InpatientEnhancedObservation record not found");
//        }

//        public void DeleteInpatientEnhancedObservation(Guid InpatientEnhancedObservationID)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("InpatientEnhancedObservation", InpatientEnhancedObservationID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region InpatientEnhancedObservationNameProfessional

//        public List<Guid> GetInpatientEnhancedObservationNameProfessionalByinpatientEnhancedObservationId(Guid InpatientEnhancedObservationIdid)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("InpatientEnhancedObservationNameProfessional", true, "InpatientEnhancedObservationNameProfessionalId");
//            query.PrimaryKeyName = "InpatientEnhancedObservationNameProfessionalId";

//            query.Filter.AddCondition("InpatientEnhancedObservationNameProfessional", "InpatientEnhancedObservationId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, InpatientEnhancedObservationIdid);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["InpatientEnhancedObservationNameProfessionalId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public NullSafeDictionary<string, object> GetInpatientEnhancedObservationNameProfessionalById(Guid InpatientEnhancedObservationNameProfessionalID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("InpatientEnhancedObservationNameProfessional", true, fields);
//            query.PrimaryKeyName = "InpatientEnhancedObservationNameProfessionalId";

//            query.Filter.AddCondition("InpatientEnhancedObservationNameProfessional", "InpatientEnhancedObservationNameProfessionalId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, InpatientEnhancedObservationNameProfessionalID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("InpatientEnhancedObservationNameProfessional record not found");
//        }

//        public void DeleteInpatientEnhancedObservationNameProfessional(Guid InpatientEnhancedObservationNameProfessionalID)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("InpatientEnhancedObservationNameProfessional", InpatientEnhancedObservationNameProfessionalID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region Email

//        public List<Guid> GetEmailsByRegardingIdField(Guid RegardingID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Email", true, "EmailId");
//            query.PrimaryKeyName = "EmailId";

//            query.Filter.AddCondition("Email", "RegardingId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, RegardingID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["EmailId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public NullSafeDictionary<string, object> GetEmailById(Guid EmailID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Email", true, fields);
//            query.PrimaryKeyName = "EmailId";

//            query.Filter.AddCondition("Email", "EmailId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, EmailID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("Email record not found");
//        }

//        public void DeleteEmail(Guid EmailID)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("Email", EmailID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region HealthAppointment

//        public List<Guid> GetHealthAppointmentsByCaseId(Guid CaseId)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("HealthAppointment", true, "HealthAppointmentId");
//            query.PrimaryKeyName = "HealthAppointmentId";

//            query.Filter.AddCondition("HealthAppointment", "CaseId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseId);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["HealthAppointmentId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public void DeleteHealthAppointment(Guid HealthAppointmentID)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("HealthAppointment", HealthAppointmentID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region EmailTo

//        public List<Guid> GetEmailsToByEmailID(Guid EmailID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("EmailTo", true, "EmailToId");
//            query.PrimaryKeyName = "EmailToId";

//            query.Filter.AddCondition("EmailTo", "EmailID", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, EmailID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["EmailToId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public NullSafeDictionary<string, object> GetEmailToById(Guid EmailToID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("EmailTo", true, fields);
//            query.PrimaryKeyName = "EmailToId";

//            query.Filter.AddCondition("EmailTo", "EmailToId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, EmailToID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("Email record not found");
//        }

//        public void DeleteEmailTo(Guid EmailToID)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("EmailTo", EmailToID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region EmailCc

//        public List<Guid> GetEmailsCcByEmailID(Guid EmailID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("EmailCc", true, "EmailCcId");
//            query.PrimaryKeyName = "EmailCcId";

//            query.Filter.AddCondition("EmailCc", "EmailID", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, EmailID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["emailccid"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public void DeleteEmailCc(Guid EmailCcID)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("EmailCc", EmailCcID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region Person

//        public Guid CreatePersonRecord(string Title, string FirstName, string MiddleName, string LastName, string PreferredName,
//            DateTime DateOfBirth, Guid Ethnicity, Guid OwnerID, int AddressTypeId, int GenderId)
//        {
//            var dataObject = GetBusinessDataInstance("person", "personid");

//            dataObject.FieldCollection.Add("Title", Title);
//            dataObject.FieldCollection.Add("FirstName", FirstName);
//            dataObject.FieldCollection.Add("MiddleName", MiddleName);
//            dataObject.FieldCollection.Add("LastName", LastName);
//            dataObject.FieldCollection.Add("PreferredName", PreferredName);

//            dataObject.FieldCollection.Add("DateOfBirth", DateOfBirth);
//            dataObject.FieldCollection.Add("EthnicityId", Ethnicity);
//            dataObject.FieldCollection.Add("OwnerID", OwnerID);

//            dataObject.FieldCollection.Add("AddressTypeId", AddressTypeId);
//            dataObject.FieldCollection.Add("GenderId", GenderId);

//            dataObject.FieldCollection.Add("AllergiesNotRecorded", true);
//            dataObject.FieldCollection.Add("Deceased", false);
//            dataObject.FieldCollection.Add("Inactive", false);
//            dataObject.FieldCollection.Add("RetainInformationConcern", false);
//            dataObject.FieldCollection.Add("AllowEmail", false);
//            dataObject.FieldCollection.Add("AllowMail", false);
//            dataObject.FieldCollection.Add("AllowPhone", false);
//            dataObject.FieldCollection.Add("IsExternalPerson", false);
//            dataObject.FieldCollection.Add("SuppressStatementInvoices", false);
//            dataObject.FieldCollection.Add("RepresentAlertOrHazard", false);
//            dataObject.FieldCollection.Add("KnownAllergies", false);
//            dataObject.FieldCollection.Add("NoKnownAllergies", false);
//            dataObject.FieldCollection.Add("InterpreterRequired", false);
//            dataObject.FieldCollection.Add("ChildProtectionFlag", false);
//            dataObject.FieldCollection.Add("RelatedChildProtectionFlag", false);
//            dataObject.FieldCollection.Add("AdultSafeguardingFlag", false);
//            dataObject.FieldCollection.Add("RelatedAdultSafeguardingFlag", false);
//            dataObject.FieldCollection.Add("RecordedInError", false);


//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(dataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;
//        }

//        public void UpdatePersonRecord(Guid PersonID, string MiddleName, string Telephone3, string CreditorNumber)
//        {
//            var businessDataObject = GetBusinessDataInstance("Person", "PersonId");

//            businessDataObject.FieldCollection.Add("PersonId", PersonID);
//            businessDataObject.FieldCollection.Add("middlename", MiddleName);
//            businessDataObject.FieldCollection.Add("Telephone3", Telephone3);
//            businessDataObject.FieldCollection.Add("CreditorNumber", CreditorNumber);

//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        public NullSafeDictionary<string, object> GetPersonByID(Guid PersonID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Person", true, fields);
//            query.PrimaryKeyName = "PersonId";

//            query.Filter.AddCondition("Person", "PersonId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("Person record not found");
//        }

//        public void UpdatePersonAllowEmail(Guid PersonID, bool allowemail)
//        {
//            var businessDataObject = GetBusinessDataInstance("Person", "PersonId");

//            businessDataObject.FieldCollection.Add("PersonId", PersonID);
//            businessDataObject.FieldCollection.Add("allowemail", allowemail);

//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        #endregion

//        #region Case

//        public Guid CreateSocialCareCase(Guid ownerid, Guid personid, Guid contactreceivedbyid, Guid responsibleuserid, Guid casestatusid, Guid contactreasonid,
//            Guid dataformid, Guid contactsourceid, DateTime casedatetime, DateTime contactreceiveddatetime, int personage)
//        {
//            var businessDataObject = GetBusinessDataInstance("Case", "CaseId");

//            businessDataObject.FieldCollection.Add("ownerid", ownerid);
//            businessDataObject.FieldCollection.Add("personid", personid);
//            businessDataObject.FieldCollection.Add("contactreceivedbyid", contactreceivedbyid);
//            businessDataObject.FieldCollection.Add("responsibleuserid", responsibleuserid);
//            businessDataObject.FieldCollection.Add("casestatusid", casestatusid);
//            businessDataObject.FieldCollection.Add("contactreasonid", contactreasonid);
//            businessDataObject.FieldCollection.Add("dataformid", dataformid);
//            businessDataObject.FieldCollection.Add("contactsourceid", contactsourceid);
//            businessDataObject.FieldCollection.Add("startdatetime", casedatetime);
//            businessDataObject.FieldCollection.Add("contactreceiveddatetime", contactreceiveddatetime);
//            businessDataObject.FieldCollection.Add("personage", personage);

//            businessDataObject.FieldCollection.Add("policenotified", false);
//            businessDataObject.FieldCollection.Add("rereferral", false);
//            businessDataObject.FieldCollection.Add("responsemadetocontact", false);
//            businessDataObject.FieldCollection.Add("section117aftercareentitlement", false);
//            businessDataObject.FieldCollection.Add("ispersononleave", false);
//            businessDataObject.FieldCollection.Add("isswappinginpatient", false);
//            businessDataObject.FieldCollection.Add("carernoknotified", false);
//            businessDataObject.FieldCollection.Add("inactive", false);
//            businessDataObject.FieldCollection.Add("dischargeperson", false);
//            businessDataObject.FieldCollection.Add("personawareofcontactid", true);
//            businessDataObject.FieldCollection.Add("personsupportcontactid", true);

//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;
//        }

//        public Guid CreateCommunityHealthCase(string PresentingNeedDetails, string AdditionalInformation,
//            DateTime StartDateTime, DateTime ContactReceivedDateTime, DateTime RequestReceivedDateTime, DateTime CaseAcceptedDateTime,
//            Guid OwnerId, Guid ContactReceivedById, Guid CommunityAndClinicTeamId, Guid ResponsibleUserId, Guid SecondaryCaseReasonId, Guid CaseStatusId, Guid ContactReasonId, Guid PresentingPriorityId, Guid AdministrativeCategoryId, Guid ServiceTypeRequestedId, Guid PersonId, Guid ProfessionalTypeId, Guid DataFormId, Guid ContactSourceId)
//        {
//            var businessDataObject = GetBusinessDataInstance("Case", "CaseId");

//            businessDataObject.FieldCollection.Add("PresentingNeedDetails", PresentingNeedDetails);
//            businessDataObject.FieldCollection.Add("AdditionalInformation", AdditionalInformation);

//            businessDataObject.FieldCollection.Add("StartDateTime", StartDateTime);
//            businessDataObject.FieldCollection.Add("ContactReceivedDateTime", ContactReceivedDateTime);
//            businessDataObject.FieldCollection.Add("RequestReceivedDateTime", RequestReceivedDateTime);
//            businessDataObject.FieldCollection.Add("CaseAcceptedDateTime", CaseAcceptedDateTime);

//            businessDataObject.FieldCollection.Add("OwnerId", OwnerId);
//            businessDataObject.FieldCollection.Add("ContactReceivedById", ContactReceivedById);
//            businessDataObject.FieldCollection.Add("CommunityAndClinicTeamId", CommunityAndClinicTeamId);
//            businessDataObject.FieldCollection.Add("ResponsibleUserId", ResponsibleUserId);
//            businessDataObject.FieldCollection.Add("SecondaryCaseReasonId", SecondaryCaseReasonId);
//            businessDataObject.FieldCollection.Add("secondarycasereasonid_cwname", "Medication Review");
//            businessDataObject.FieldCollection.Add("CaseStatusId", CaseStatusId);
//            businessDataObject.FieldCollection.Add("ContactReasonId", ContactReasonId);
//            businessDataObject.FieldCollection.Add("PresentingPriorityId", PresentingPriorityId);
//            businessDataObject.FieldCollection.Add("AdministrativeCategoryId", AdministrativeCategoryId);
//            businessDataObject.FieldCollection.Add("ServiceTypeRequestedId", ServiceTypeRequestedId);
//            businessDataObject.FieldCollection.Add("PersonId", PersonId);
//            businessDataObject.FieldCollection.Add("ProfessionalTypeId", ProfessionalTypeId);
//            businessDataObject.FieldCollection.Add("DataFormId", DataFormId);
//            businessDataObject.FieldCollection.Add("ContactSourceId", ContactSourceId);

//            businessDataObject.FieldCollection.Add("CasePriorityId", null);


//            businessDataObject.FieldCollection.Add("CNACount", 0);
//            businessDataObject.FieldCollection.Add("DNACount", 0);
//            businessDataObject.FieldCollection.Add("PersonAwareOfContactId", 1);
//            businessDataObject.FieldCollection.Add("PersonSupportContactId", 1);
//            businessDataObject.FieldCollection.Add("CarerAwareOfContactId", 1);
//            businessDataObject.FieldCollection.Add("CarerSupportContactId", 1);
//            businessDataObject.FieldCollection.Add("NextOfKinAwareOfContactId", 1);
//            businessDataObject.FieldCollection.Add("CaseAcceptedId", 1);

//            businessDataObject.FieldCollection.Add("Inactive", false);
//            businessDataObject.FieldCollection.Add("DischargePerson", false);
//            businessDataObject.FieldCollection.Add("PoliceNotified", false);
//            businessDataObject.FieldCollection.Add("ReReferral", false);
//            businessDataObject.FieldCollection.Add("ResponseMadeToContact", false);
//            businessDataObject.FieldCollection.Add("Section117AftercareEntitlement", false);
//            businessDataObject.FieldCollection.Add("IsPersonOnLeave", false);
//            businessDataObject.FieldCollection.Add("IsSwappingInpatient", false);
//            businessDataObject.FieldCollection.Add("CarerNokNotified", false);





//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;
//        }

//        public Guid CreateCommunityHealthCase(string PresentingNeedDetails, string AdditionalInformation,
//            DateTime StartDateTime, DateTime ContactReceivedDateTime, DateTime RequestReceivedDateTime, DateTime CaseAcceptedDateTime,
//            Guid OwnerId, string OwnerId_cwname, Guid ContactReceivedById, Guid CommunityAndClinicTeamId, Guid ResponsibleUserId, Guid SecondaryCaseReasonId, string SecondaryCaseReasonId_cwname, Guid CaseStatusId, string CaseStatusId_cwname, Guid ContactReasonId, Guid PresentingPriorityId, Guid AdministrativeCategoryId, Guid ServiceTypeRequestedId, Guid PersonId, Guid ProfessionalTypeId, Guid DataFormId, Guid ContactSourceId)
//        {
//            var businessDataObject = GetBusinessDataInstance("Case", "CaseId");

//            businessDataObject.FieldCollection.Add("PresentingNeedDetails", PresentingNeedDetails);
//            businessDataObject.FieldCollection.Add("AdditionalInformation", AdditionalInformation);

//            businessDataObject.FieldCollection.Add("StartDateTime", StartDateTime);
//            businessDataObject.FieldCollection.Add("ContactReceivedDateTime", ContactReceivedDateTime);
//            businessDataObject.FieldCollection.Add("RequestReceivedDateTime", RequestReceivedDateTime);
//            businessDataObject.FieldCollection.Add("CaseAcceptedDateTime", CaseAcceptedDateTime);

//            businessDataObject.FieldCollection.Add("OwnerId", OwnerId);
//            businessDataObject.FieldCollection.Add("OwnerId_cwname", OwnerId_cwname);
//            businessDataObject.FieldCollection.Add("ContactReceivedById", ContactReceivedById);
//            businessDataObject.FieldCollection.Add("CommunityAndClinicTeamId", CommunityAndClinicTeamId);
//            businessDataObject.FieldCollection.Add("ResponsibleUserId", ResponsibleUserId);
//            businessDataObject.FieldCollection.Add("SecondaryCaseReasonId", SecondaryCaseReasonId);
//            businessDataObject.FieldCollection.Add("secondarycasereasonid_cwname", SecondaryCaseReasonId_cwname);
//            businessDataObject.FieldCollection.Add("CaseStatusId", CaseStatusId);
//            businessDataObject.FieldCollection.Add("CaseStatusId_cwname", CaseStatusId_cwname);
//            businessDataObject.FieldCollection.Add("ContactReasonId", ContactReasonId);
//            businessDataObject.FieldCollection.Add("PresentingPriorityId", PresentingPriorityId);
//            businessDataObject.FieldCollection.Add("AdministrativeCategoryId", AdministrativeCategoryId);
//            businessDataObject.FieldCollection.Add("ServiceTypeRequestedId", ServiceTypeRequestedId);
//            businessDataObject.FieldCollection.Add("PersonId", PersonId);
//            businessDataObject.FieldCollection.Add("ProfessionalTypeId", ProfessionalTypeId);
//            businessDataObject.FieldCollection.Add("DataFormId", DataFormId);
//            businessDataObject.FieldCollection.Add("ContactSourceId", ContactSourceId);

//            businessDataObject.FieldCollection.Add("CasePriorityId", null);


//            businessDataObject.FieldCollection.Add("CNACount", 0);
//            businessDataObject.FieldCollection.Add("DNACount", 0);
//            businessDataObject.FieldCollection.Add("PersonAwareOfContactId", 1);
//            businessDataObject.FieldCollection.Add("PersonAwareOfContactId_cwname", "Yes");
//            businessDataObject.FieldCollection.Add("PersonSupportContactId", 1);
//            businessDataObject.FieldCollection.Add("CarerAwareOfContactId", 1);
//            businessDataObject.FieldCollection.Add("CarerSupportContactId", 1);
//            businessDataObject.FieldCollection.Add("NextOfKinAwareOfContactId", 1);
//            businessDataObject.FieldCollection.Add("CaseAcceptedId", 1);

//            businessDataObject.FieldCollection.Add("Inactive", false);
//            businessDataObject.FieldCollection.Add("DischargePerson", false);
//            businessDataObject.FieldCollection.Add("PoliceNotified", false);
//            businessDataObject.FieldCollection.Add("ReReferral", false);
//            businessDataObject.FieldCollection.Add("ResponseMadeToContact", false);
//            businessDataObject.FieldCollection.Add("Section117AftercareEntitlement", false);
//            businessDataObject.FieldCollection.Add("IsPersonOnLeave", false);
//            businessDataObject.FieldCollection.Add("IsSwappingInpatient", false);
//            businessDataObject.FieldCollection.Add("CarerNokNotified", false);





//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;
//        }

//        public void UpdateCaseAdditionalDetails(Guid RecordID, string AdditionalInformation)
//        {
//            var businessDataObject = GetBusinessDataInstance("Case", "CaseId");
//            businessDataObject.Id = RecordID;

//            businessDataObject.FieldCollection.Add("CaseId", RecordID);
//            businessDataObject.FieldCollection.Add("AdditionalInformation", AdditionalInformation);

//            CareDirector.Sdk.ServiceResponse.UpdateResponse response = DataProvider.Update(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);
//        }



//        public List<Guid> GetCasesForPerson(Guid PersonID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Case", true, "CaseId");
//            query.PrimaryKeyName = "CaseId";

//            query.Filter.AddCondition("Case", "PersonID", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["CaseId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public NullSafeDictionary<string, object> GetCaseById(Guid CaseID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Case", true, fields);
//            query.PrimaryKeyName = "CaseId";

//            query.Filter.AddCondition("Case", "CaseId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("Case record not found");
//        }

//        public void DeleteCase(Guid CaseID)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("Case", CaseID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region SecondaryCaseReason

//        public List<Guid> GetSecondaryCaseReasonByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("SecondaryCaseReason", true, "SecondaryCaseReasonId");
//            query.PrimaryKeyName = "SecondaryCaseReasonId";

//            query.Filter.AddCondition("SecondaryCaseReason", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["SecondaryCaseReasonId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region CaseStatus

//        public List<Guid> GetCaseStatusByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("CaseStatus", true, "CaseStatusId");
//            query.PrimaryKeyName = "CaseStatusId";

//            query.Filter.AddCondition("CaseStatus", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["CaseStatusId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region ContactAdministrativeCategory

//        public List<Guid> GetContactAdministrativeCategoryByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("ContactAdministrativeCategory", true, "ContactAdministrativeCategoryId");
//            query.PrimaryKeyName = "ContactAdministrativeCategoryId";

//            query.Filter.AddCondition("ContactAdministrativeCategory", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["ContactAdministrativeCategoryId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region CaseServiceTypeRequested

//        public List<Guid> GetCaseServiceTypeRequestedByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("CaseServiceTypeRequested", true, "CaseServiceTypeRequestedId");
//            query.PrimaryKeyName = "CaseServiceTypeRequestedId";

//            query.Filter.AddCondition("CaseServiceTypeRequested", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["CaseServiceTypeRequestedId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region ProfessionType

//        public List<Guid> GetProfessionTypeByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("ProfessionType", true, "ProfessionTypeId");
//            query.PrimaryKeyName = "ProfessionTypeId";

//            query.Filter.AddCondition("ProfessionType", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["ProfessionTypeId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region DataForm

//        public List<Guid> GetDataFormByName(string Name)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("DataForm", true, "DataFormId");
//            query.PrimaryKeyName = "DataFormId";

//            query.Filter.AddCondition("DataForm", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["DataFormId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        #endregion

//        #region CaseInvolvement

//        public List<Guid> GetCaseInvolvementsForCase(Guid CaseID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("CaseInvolvement", true, "CaseInvolvementId");
//            query.PrimaryKeyName = "CaseInvolvementId";

//            query.Filter.AddCondition("CaseInvolvement", "CaseID", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["CaseInvolvementId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public void DeleteCaseInvolvement(Guid CaseInvolvementID)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("CaseInvolvement", CaseInvolvementID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region CaseStatusHistory

//        public List<Guid> GetCaseStatusHistoryForCase(Guid CaseID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("CaseStatusHistory", true, "CaseStatusHistoryId");
//            query.PrimaryKeyName = "CaseStatusHistoryId";

//            query.Filter.AddCondition("CaseStatusHistory", "CaseID", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["CaseStatusHistoryId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public void DeleteCaseStatusHistory(Guid CaseStatusHistoryId)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("CaseStatusHistory", CaseStatusHistoryId);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region CaseAction

//        public Guid CreateCaseAction(Guid OwnerId, Guid PersonId, Guid CaseId, Guid CaseActionTypeId, DateTime DueDate)
//        {
//            var businessDataObject = GetBusinessDataInstance("CaseAction", "CaseActionId");

//            businessDataObject.FieldCollection.Add("OwnerId", OwnerId);
//            businessDataObject.FieldCollection.Add("PersonId", PersonId);
//            businessDataObject.FieldCollection.Add("CaseId", CaseId);
//            businessDataObject.FieldCollection.Add("CaseActionTypeId", CaseActionTypeId);
//            businessDataObject.FieldCollection.Add("DueDate", DueDate);
//            businessDataObject.FieldCollection.Add("Inactive", false);



//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;
//        }

//        public List<Guid> GetCaseActionForCase(Guid CaseID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("CaseAction", true, "CaseActionId");
//            query.PrimaryKeyName = "CaseActionId";

//            query.Filter.AddCondition("CaseAction", "CaseID", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["CaseActionId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public NullSafeDictionary<string, object> GetCaseActionByID(Guid CaseActionID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("CaseAction", true, fields);
//            query.PrimaryKeyName = "CaseActionId";

//            query.Filter.AddCondition("CaseAction", "CaseActionId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseActionID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("CaseAction record not found");
//        }

//        public void DeleteCaseAction(Guid CaseActionId)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("CaseAction", CaseActionId);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region Task

//        public Guid CreateCaseTask(Guid CaseId, Guid PersonID, string CaseNumber, string Subject, string Notes, Guid OwnerId)
//        {
//            var businessDataObject = GetBusinessDataInstance("Task", "TaskId");

//            businessDataObject.FieldCollection.Add("OwnerId", OwnerId);
//            businessDataObject.FieldCollection.Add("Subject", Subject);
//            businessDataObject.FieldCollection.Add("RegardingId", CaseId);
//            businessDataObject.FieldCollection.Add("RegardingIdTableName", "case");
//            businessDataObject.FieldCollection.Add("RegardingIdName", CaseNumber);
//            businessDataObject.FieldCollection.Add("Notes", Notes);
//            businessDataObject.FieldCollection.Add("CaseId", CaseId);
//            businessDataObject.FieldCollection.Add("PersonId", PersonID);
//            businessDataObject.FieldCollection.Add("InformationByThirdParty", false);
//            businessDataObject.FieldCollection.Add("IsSignificantEvent", false);
//            businessDataObject.FieldCollection.Add("StatusId", 1);

//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;

//        }

//        public List<Guid> GetTasksForCase(Guid CaseID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Task", true, "TaskId");
//            query.PrimaryKeyName = "TaskId";

//            query.Filter.AddCondition("Task", "CaseID", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["TaskId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public NullSafeDictionary<string, object> GetTaskById(Guid TaskID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Task", true, fields);
//            query.PrimaryKeyName = "TaskId";

//            query.Filter.AddCondition("Task", "TaskId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, TaskID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("Task record not found");
//        }

//        public void DeleteTask(Guid TaskId)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("Task", TaskId);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region PersonForm

//        public List<Guid> GetPersonFormForPerson(Guid PersonID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PersonForm", true, "PersonFormId");
//            query.PrimaryKeyName = "PersonFormId";

//            query.Filter.AddCondition("PersonForm", "PersonID", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["PersonFormId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public void UpdatePersonFormStatus(Guid PersonFormID, int assessmentstatusid)
//        {
//            var businessDataObject = GetBusinessDataInstance("PersonForm", "PersonFormId");

//            businessDataObject.FieldCollection.Add("PersonFormId", PersonFormID);
//            businessDataObject.FieldCollection.Add("assessmentstatusid", assessmentstatusid);

//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        public void UpdatePersonFormReviewDate(Guid PersonFormID, DateTime? reviewdate)
//        {
//            var businessDataObject = GetBusinessDataInstance("PersonForm", "PersonFormId");

//            businessDataObject.FieldCollection.Add("PersonFormId", PersonFormID);
//            businessDataObject.FieldCollection.Add("reviewdate", reviewdate);

//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        public NullSafeDictionary<string, object> GetPersonFormByID(Guid PersonFormID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PersonForm", true, fields);
//            query.PrimaryKeyName = "PersonFormId";

//            query.Filter.AddCondition("PersonForm", "PersonFormId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonFormID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("Person Form record not found");
//        }

//        public void DeletePersonForm(Guid PersonFormId)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("PersonForm", PersonFormId);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region PersonCaseNote

//        public List<Guid> GetPersonCaseNoteForPerson(Guid PersonID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PersonCaseNote", true, "PersonCaseNoteId");
//            query.PrimaryKeyName = "PersonCaseNoteId";

//            query.Filter.AddCondition("PersonCaseNote", "PersonID", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["PersonCaseNoteId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public NullSafeDictionary<string, object> GetPersonCaseNoteByID(Guid PersonCaseNoteID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PersonCaseNote", true, fields);
//            query.PrimaryKeyName = "PersonCaseNoteId";

//            query.Filter.AddCondition("PersonCaseNote", "PersonCaseNoteId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonCaseNoteID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("PersonCaseNote record not found");
//        }

//        public void DeletePersonCaseNote(Guid PersonCaseNoteId)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("PersonCaseNote", PersonCaseNoteId);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region LACReview

//        public List<Guid> GetLACReviewsByCaseID(Guid CaseId)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("LACReview", true, "LACReviewId");
//            query.PrimaryKeyName = "LACReviewId";

//            query.Filter.AddCondition("LACReview", "CaseId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseId);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["LACReviewid"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public void DeleteLACReview(Guid LACReviewID)
//        {
//            var response = DataProvider.Delete("LACReview", LACReviewID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);
//        }

//        #endregion

//        #region LACCheck

//        public List<Guid> GetLACChecksByCaseID(Guid CaseId)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("LACCheck", true, "LACCheckId");
//            query.PrimaryKeyName = "LACCheckId";

//            query.Filter.AddCondition("LACCheck", "CaseId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseId);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["LACCheckid"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public void DeleteLACCheck(Guid LACCheckID)
//        {
//            var response = DataProvider.Delete("LACCheck", LACCheckID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);
//        }

//        #endregion

//        #region PersonLACLegalStatus

//        public List<Guid> GetPersonLACLegalStatussByCaseID(Guid CaseId)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PersonLACLegalStatus", true, "PersonLACLegalStatusId");
//            query.PrimaryKeyName = "PersonLACLegalStatusId";

//            query.Filter.AddCondition("PersonLACLegalStatus", "CaseId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseId);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["PersonLACLegalStatusid"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public void DeletePersonLACLegalStatus(Guid PersonLACLegalStatusID)
//        {
//            var response = DataProvider.Delete("PersonLACLegalStatus", PersonLACLegalStatusID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);
//        }

//        #endregion

//        #region LACEpisode

//        public List<Guid> GetLACEpisodesByCaseID(Guid CaseId)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("LACEpisode", true, "LACEpisodeId");
//            query.PrimaryKeyName = "LACEpisodeId";

//            query.Filter.AddCondition("LACEpisode", "CaseId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseId);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["LACEpisodeid"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public void DeleteLACEpisode(Guid LACEpisodeID)
//        {
//            var response = DataProvider.Delete("LACEpisode", LACEpisodeID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);
//        }

//        #endregion



//        //


//        #region CaseForm

//        public List<Guid> GetCaseFormByCaseID(Guid CaseId)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("CaseForm", true, "CaseFormId");
//            query.PrimaryKeyName = "CaseFormId";

//            query.Filter.AddCondition("CaseForm", "CaseId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseId);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["CaseFormId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public void UpdateCaseFormRecord(Guid CaseFormID, DateTime StartDate, bool SeparateAssessment)
//        {
//            var businessDataObject = GetBusinessDataInstance("CaseForm", "CaseFormId");

//            businessDataObject.FieldCollection.Add("CaseFormId", CaseFormID);
//            businessDataObject.FieldCollection.Add("StartDate", StartDate);
//            businessDataObject.FieldCollection.Add("SeparateAssessment", SeparateAssessment);

//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        public void UpdateCaseFormRecord(Guid CaseFormID, DateTime StartDate, DateTime duedate, DateTime reviewdate)
//        {
//            var businessDataObject = GetBusinessDataInstance("CaseForm", "CaseFormId");

//            businessDataObject.FieldCollection.Add("CaseFormId", CaseFormID);
//            businessDataObject.FieldCollection.Add("StartDate", StartDate);
//            businessDataObject.FieldCollection.Add("duedate", duedate);
//            businessDataObject.FieldCollection.Add("reviewdate", reviewdate);

//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        public void UpdateCaseFormRecord(Guid CaseFormID, DateTime StartDate, DateTime? duedate, Guid? ResponsibleUserID, bool SeparateAssessment, bool CarerDeclinedJointAssessment, bool JointCarerAssessment, bool NewPerson)
//        {
//            var businessDataObject = GetBusinessDataInstance("CaseForm", "CaseFormId");

//            businessDataObject.FieldCollection.Add("CaseFormId", CaseFormID);
//            businessDataObject.FieldCollection.Add("StartDate", StartDate);
//            businessDataObject.FieldCollection.Add("duedate", duedate);
//            businessDataObject.FieldCollection.Add("ResponsibleUserId", ResponsibleUserID);
//            businessDataObject.FieldCollection.Add("SeparateAssessment", SeparateAssessment);
//            businessDataObject.FieldCollection.Add("CarerDeclinedJointAssessment", CarerDeclinedJointAssessment);
//            businessDataObject.FieldCollection.Add("JointCarerAssessment", JointCarerAssessment);
//            businessDataObject.FieldCollection.Add("NewPerson", NewPerson);


//            var updateResponse = DataProvider.Update(businessDataObject);

//            if (updateResponse.HasErrors)
//                throw new Exception(updateResponse.Exception.Message);
//        }

//        public NullSafeDictionary<string, object> GetCaseFormByID(Guid CaseFormID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("CaseForm", true, fields);
//            query.PrimaryKeyName = "CaseFormId";

//            query.Filter.AddCondition("CaseForm", "CaseFormId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, CaseFormID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("Case record not found");
//        }

//        public void DeleteCaseForm(Guid CaseFormID)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("CaseForm", CaseFormID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region Person Employment

//        public Guid CreatePersonEmployment(string Title, string Employer, Guid PersonID, Guid Ownerid, Guid EmploymentWeeklyHoursWorkedId,
//            Guid EmploymentStatusId, Guid? EmploymentTypeId, Guid? EmploymentReasonLeftId, DateTime StartDate, DateTime EndDate)
//        {
//            var businessDataObject = GetBusinessDataInstance("PersonEmployment", "PersonEmploymentId");

//            businessDataObject.FieldCollection.Add("Title", Title);
//            businessDataObject.FieldCollection.Add("Employer", Employer);

//            businessDataObject.FieldCollection.Add("Inactive", false);

//            businessDataObject.FieldCollection.Add("PersonID", PersonID);
//            businessDataObject.FieldCollection.Add("Ownerid", Ownerid);

//            businessDataObject.FieldCollection.Add("EmploymentWeeklyHoursWorkedId", EmploymentWeeklyHoursWorkedId);
//            businessDataObject.FieldCollection.Add("EmploymentStatusId", EmploymentStatusId);
//            businessDataObject.FieldCollection.Add("EmploymentTypeId", EmploymentTypeId);
//            businessDataObject.FieldCollection.Add("EmploymentReasonLeftId", EmploymentReasonLeftId);

//            businessDataObject.FieldCollection.Add("StartDate", StartDate);
//            businessDataObject.FieldCollection.Add("EndDate", EndDate);


//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;
//        }

//        public List<Guid> GetPersonEmploymentForPersonRecord(Guid PersonID, string Employer)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PersonEmployment", true, "PersonEmploymentId");
//            query.PrimaryKeyName = "PersonEmploymentId";

//            query.Filter.AddCondition("PersonEmployment", "PersonId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonID);

//            if (!string.IsNullOrEmpty(Employer))
//                query.Filter.AddCondition("PersonEmployment", "Employer", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Employer);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["PersonEmploymentId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public NullSafeDictionary<string, object> GetPersonEmploymentByID(Guid PersonEmploymentID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PersonEmployment", true, fields);
//            query.PrimaryKeyName = "PersonEmploymentId";

//            query.Filter.AddCondition("PersonEmployment", "PersonEmploymentId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonEmploymentID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("Person Employment record not found");
//        }

//        public void DeletePersonEmployment(Guid PersonEmploymentID)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("PersonEmployment", PersonEmploymentID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion

//        #region PersonHeightAndWeight

//        public Guid CreatePersonHeightAndWeight(string Title, Guid PersonID, DateTime DateTimeTaken, int AgeAtTimeTaken,
//            int WeightStones, int? WeightPounds, int WeightOunces, decimal WeightKilos,
//            int HeightFeet, int HeightInches, decimal HeightMetres,
//            Guid OwnerId, string AdditionalComments)
//        {
//            var businessDataObject = GetBusinessDataInstance("PersonHeightAndWeight", "PersonHeightAndWeightId");

//            businessDataObject.FieldCollection.Add("Title", Title);

//            businessDataObject.FieldCollection.Add("Inactive", false);

//            businessDataObject.FieldCollection.Add("PersonID", PersonID);

//            businessDataObject.FieldCollection.Add("DateTimeTaken", DateTimeTaken);
//            businessDataObject.FieldCollection.Add("AgeAtTimeTaken", AgeAtTimeTaken);

//            businessDataObject.FieldCollection.Add("WeightStones", WeightStones);
//            businessDataObject.FieldCollection.Add("WeightPounds", WeightPounds);
//            businessDataObject.FieldCollection.Add("WeightOunces", WeightOunces);
//            businessDataObject.FieldCollection.Add("WeightKilos", WeightKilos);

//            businessDataObject.FieldCollection.Add("HeightFeet", HeightFeet);
//            businessDataObject.FieldCollection.Add("HeightInches", HeightInches);
//            businessDataObject.FieldCollection.Add("HeightMetres", HeightMetres);


//            businessDataObject.FieldCollection.Add("BMIMUSTScore", 0);
//            businessDataObject.FieldCollection.Add("BMIResult", "Normal");
//            businessDataObject.FieldCollection.Add("WeightLossMUSTScore", 0);
//            businessDataObject.FieldCollection.Add("AcuteDiseaseEffect", 0);
//            businessDataObject.FieldCollection.Add("AcuteDiseaseMUSTScore", 0);
//            businessDataObject.FieldCollection.Add("MUSTTotalScore", 0);
//            businessDataObject.FieldCollection.Add("Risk", "Low Risk");

//            businessDataObject.FieldCollection.Add("CarePlanNeeded", 0);
//            businessDataObject.FieldCollection.Add("MonitorFoodAndFluid", 0);
//            businessDataObject.FieldCollection.Add("CaseRequired", 0);

//            businessDataObject.FieldCollection.Add("BMIScore", "21.33");


//            businessDataObject.FieldCollection.Add("OwnerId", OwnerId);

//            businessDataObject.FieldCollection.Add("AdditionalComments", AdditionalComments);


//            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(businessDataObject);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            return response.Id.Value;
//        }

//        public List<Guid> GetPersonHeightAndWeightForPersonRecord(Guid PersonID)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PersonHeightAndWeight", true, "PersonHeightAndWeightId");
//            query.PrimaryKeyName = "PersonHeightAndWeightId";

//            query.Filter.AddCondition("PersonHeightAndWeight", "PersonId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["PersonHeightAndWeightId"]).ToList();
//            else
//                return new List<Guid>();
//        }

//        public NullSafeDictionary<string, object> GetPersonHeightAndWeightByID(Guid PersonHeightAndWeightID, params string[] fields)
//        {
//            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PersonHeightAndWeight", true, fields);
//            query.PrimaryKeyName = "PersonHeightAndWeightId";

//            query.Filter.AddCondition("PersonHeightAndWeight", "PersonHeightAndWeightId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonHeightAndWeightID);

//            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//            if (response.BusinessDataCollection.Count > 0)
//            {
//                NullSafeDictionary<string, object> dic = new NullSafeDictionary<string, object>();
//                (from fc in response.BusinessDataCollection[0].FieldCollection
//                 select new
//                 {
//                     key = fc.Key,
//                     value = fc.Value
//                 })
//                 .ToList()
//                 .ForEach(c =>
//                 {
//                     dic.Add(c.key, c.value);
//                 });

//                return dic;
//            }
//            else
//                throw new Exception("PersonHeightAndWeight record not found");
//        }

//        public void DeletePersonHeightAndWeight(Guid PersonHeightAndWeightID)
//        {
//            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("PersonHeightAndWeight", PersonHeightAndWeightID);

//            if (response.HasErrors)
//                throw new Exception(response.Exception.Message);

//        }

//        #endregion
//    }
//}
