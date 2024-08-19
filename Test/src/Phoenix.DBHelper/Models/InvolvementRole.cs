using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InvolvementRole : BaseClass
    {

        public string TableName = "InvolvementRole";
        public string PrimaryKeyName = "InvolvementRoleId";


        public InvolvementRole()
        {
            AuthenticateUser();
        }

        public InvolvementRole(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateInvolvementRole(Guid ownerid, String name, String legacyid, DateTime startdate, int involvementroletype)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "legacyid", legacyid);
            AddFieldToBusinessDataObject(dataObject, "startdate", DateTime.Now.Date.AddYears(-1));
            AddFieldToBusinessDataObject(dataObject, "islegitmaterelationshipenabled", 0);
            AddFieldToBusinessDataObject(dataObject, "involvementroletype", involvementroletype);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(String Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


    }
}
