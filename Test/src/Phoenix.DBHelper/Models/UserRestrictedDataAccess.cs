using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class UserRestrictedDataAccess : BaseClass
    {

        public string TableName = "UserRestrictedDataAccess";
        public string PrimaryKeyName = "UserRestrictedDataAccessId";


        public UserRestrictedDataAccess()
        {
            AuthenticateUser();
        }

        public UserRestrictedDataAccess(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateUserRestrictedDataAccess(Guid DataRestrictionID, Guid UserID, DateTime StartDate, DateTime? EndDate, Guid OwnerId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerID", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "UserID", UserID);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", EndDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "DataRestrictionReferenceId", DataRestrictionID);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByUserIDAndDataRestrictionID(Guid DataRestrictionID, Guid UserID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "UserID", ConditionOperatorType.Equal, UserID);
            this.BaseClassAddTableCondition(query, "DataRestrictionReferenceId", ConditionOperatorType.Equal, DataRestrictionID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetUserRestrictedDataAccessByUserID(Guid UserID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "UserID", ConditionOperatorType.Equal, UserID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetUserRestrictedDataAccessByUserID(Guid UserID, Guid DataRestrictionID)
        {
            var query = GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            BaseClassAddTableCondition(query, "UserID", ConditionOperatorType.Equal, UserID);
            BaseClassAddTableCondition(query, "DataRestrictionReferenceId", ConditionOperatorType.Equal, DataRestrictionID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public Dictionary<string, object> GetUserRestrictedDataAccessByID(Guid UserRestrictedDataAccessId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, UserRestrictedDataAccessId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteUserRestrictedDataAccess(Guid DataRestrictionID, Guid UserID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                var dataAccesses = (from ent in entity.UserRestrictedDataAccesses
                                    where ent.DataRestrictionReferenceId == DataRestrictionID
                                    && ent.UserId == UserID
                                    select ent).ToList();

                entity.UserRestrictedDataAccesses.RemoveRange(dataAccesses);
                entity.SaveChanges();
            }
        }
    }
}
