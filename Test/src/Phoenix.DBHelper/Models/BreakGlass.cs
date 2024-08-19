using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class BreakGlass : BaseClass
    {
        public string TableName { get { return "BreakGlass"; } }
        public string PrimaryKeyName { get { return "BreakGlassid"; } }


        public BreakGlass()
        {
            AuthenticateUser();
        }

        public BreakGlass(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetBreakGlassIDsForPerson(Guid PersonID, string Justification)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("BreakGlass", "BreakGlass", true, "BreakGlassId", "Justification", "UserId", "AccessDateTime", "RevokeAccessDateTime", "BreakGlassReasonId");
            query.PrimaryKeyName = "BreakGlassId";
            //query.RecordsPerPage = 100;

            query.Filter.AddCondition("BreakGlass", "PersonId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonID);

            if (!string.IsNullOrEmpty(Justification))
                query.Filter.AddCondition("BreakGlass", "Justification", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Justification);

            query.Fields.Add(new CareDirector.Sdk.Query.DataField("BreakGlassId", "BreakGlass"));

            var response = ExecuteDataQuery(query);

            return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["breakglassid"]).ToList();
        }

        public List<CareDirector.Sdk.SystemEntities.BusinessData> GetAllBreakGlassInfoByPersonID(Guid PersonID, string Justification)
        {
            var query = new CareDirector.Sdk.Query.DataQuery("BreakGlass", true);
            query.PrimaryKeyName = "BreakGlassId";
            query.RecordsPerPage = 100;

            query.Orders.Add(new CareDirector.Sdk.Query.OrderBy("createdon", CareWorks.Foundation.Enums.SortOrder.Descending, "BreakGlass"));

            query.Filter.AddCondition("BreakGlass", "PersonId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonID);

            if (!string.IsNullOrEmpty(Justification))
                query.Filter.AddCondition("BreakGlass", "Justification", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Justification);

            var response = ExecuteDataQuery(query);

            if (response.HasErrors) throw new Exception(response.Error);

            return response.BusinessDataCollection;
        }

        public Guid CreateBreakGlassForPerson(Guid PersonID, string Justification, Guid UserId, DateTime AccessDateTime, DateTime RevokeAccessDateTime, Guid BreakGlassReasonId, Guid OwnerID)
        {
            var serRestrictedDataAccessBusinessData = GetBusinessDataBaseObject("BreakGlass", "BreakGlassId");

            serRestrictedDataAccessBusinessData.FieldCollection.Add("PersonId", PersonID);
            serRestrictedDataAccessBusinessData.FieldCollection.Add("Justification", Justification);
            serRestrictedDataAccessBusinessData.FieldCollection.Add("UserId", UserId);
            serRestrictedDataAccessBusinessData.FieldCollection.Add("AccessDateTime", AccessDateTime);
            serRestrictedDataAccessBusinessData.FieldCollection.Add("RevokeAccessDateTime", RevokeAccessDateTime);
            serRestrictedDataAccessBusinessData.FieldCollection.Add("BreakGlassReasonId", BreakGlassReasonId);
            serRestrictedDataAccessBusinessData.FieldCollection.Add("OwnerId", OwnerID);

            return CreateRecord(serRestrictedDataAccessBusinessData);
        }

        public void DeleteBreakGlassRecord(Guid BreakGlassID)
        {
            DeleteRecord("BreakGlass", BreakGlassID);

        }

    }
}
