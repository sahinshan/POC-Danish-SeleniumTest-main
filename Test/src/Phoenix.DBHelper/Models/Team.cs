using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class Team : BaseClass
    {
        public string TableName = "team";
        public string PrimaryKeyName = "teamid";

        public Team()
        {
            AuthenticateUser();
        }

        public Team(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateTeam(string Name, Guid? TeamManagerId, Guid OwningBusinessUnitId, string Code, string EmailAddress, string Description, string PhoneNumber)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "TeamManagerId", TeamManagerId);
            AddFieldToBusinessDataObject(dataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "EmailAddress", EmailAddress);
            AddFieldToBusinessDataObject(dataObject, "Description", Description);
            AddFieldToBusinessDataObject(dataObject, "PhoneNumber", PhoneNumber);
            AddFieldToBusinessDataObject(dataObject, "ReferenceDataOwner", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateTeam(Guid TeamId, string Name, Guid? TeamManagerId, Guid OwningBusinessUnitId, string Code, string EmailAddress, string Description, string PhoneNumber)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, TeamId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "TeamManagerId", TeamManagerId);
            AddFieldToBusinessDataObject(dataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "EmailAddress", EmailAddress);
            AddFieldToBusinessDataObject(dataObject, "Description", Description);
            AddFieldToBusinessDataObject(dataObject, "PhoneNumber", PhoneNumber);
            AddFieldToBusinessDataObject(dataObject, "ReferenceDataOwner", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public void UpdateTeamManager(Guid TeamId, Guid teammanagerid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, TeamId);
            AddFieldToBusinessDataObject(buisinessDataObject, "teammanagerid", teammanagerid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateEmailAddress(Guid TeamId, string emailaddress)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, TeamId);
            AddFieldToBusinessDataObject(buisinessDataObject, "emailaddress", emailaddress);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateInactive(Guid TeamId, bool inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, TeamId);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateIncludeInSchedulingScreens(Guid TeamId, bool includeinschedulingscreens)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, TeamId);
            AddFieldToBusinessDataObject(buisinessDataObject, "includeinschedulingscreens", includeinschedulingscreens);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetTeamIdByName(string TeamName)
        {
            var query = new DataQuery(TableName, true, PrimaryKeyName);
            query.PrimaryKeyName = PrimaryKeyName;

            query.Filter.AddCondition(TableName, "name", ConditionOperatorType.Equal, TeamName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
            {

                return response.BusinessDataCollection.Select(c => Guid.Parse(c.FieldCollection[PrimaryKeyName].ToString())).ToList();
            }
            else
            {
                return new List<Guid>();
            }
        }

        public List<Guid> GetMatchingTeamsByName(string TeamName)
        {
            var query = new DataQuery(TableName, true, PrimaryKeyName);
            query.PrimaryKeyName = PrimaryKeyName;

            query.Filter.AddCondition(TableName, "name", ConditionOperatorType.StartsWith, TeamName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
            {

                return response.BusinessDataCollection.Select(c => Guid.Parse(c.FieldCollection[PrimaryKeyName].ToString())).ToList();
            }
            else
            {
                return new List<Guid>();
            }
        }

        public List<Guid> GetAllTeams()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetFirstTeams(int RecordsPerPage, int PageNumber, bool SortAscending)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.RecordsPerPage = RecordsPerPage;
            query.PageNumber = PageNumber;

            if(SortAscending)
                query.Orders.Add(new OrderBy("createdon", SortOrder.Ascending, TableName));
            else
                query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetTeamByID(Guid TeamId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, TeamId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
