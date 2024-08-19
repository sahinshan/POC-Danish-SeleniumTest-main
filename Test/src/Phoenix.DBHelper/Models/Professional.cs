using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class Professional : BaseClass
    {

        private string tableName = "Professional";
        private string primaryKeyName = "ProfessionalId";

        public Professional()
        {
            AuthenticateUser();
        }

        public Professional(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateProfessional(Guid ownerid, Guid professiontypeid, string title, string firstname, string lastname, string email = "")
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "professiontypeid", professiontypeid);
            AddFieldToBusinessDataObject(dataObject, "title", title);
            AddFieldToBusinessDataObject(dataObject, "firstname", firstname);
            AddFieldToBusinessDataObject(dataObject, "lastname", lastname);
            AddFieldToBusinessDataObject(dataObject, "email", email);
            AddFieldToBusinessDataObject(dataObject, "addresstypeid", 8);
            AddFieldToBusinessDataObject(dataObject, "allowemail", 1);
            AddFieldToBusinessDataObject(dataObject, "allowphone", 1);
            AddFieldToBusinessDataObject(dataObject, "allowmail", 1);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetProfessionalIdByFirstName(string firstname)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "firstname", ConditionOperatorType.Equal, firstname);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetProfessionalByID(Guid ProfessionalId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, ProfessionalId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteProfessional(Guid ProfessionalID)
        {
            this.DeleteRecord(tableName, ProfessionalID);
        }



    }
}
