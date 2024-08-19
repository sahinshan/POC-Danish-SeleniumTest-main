using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonHealthProfessional : BaseClass
    {

        public string TableName = "PersonHealthProfessional";
        public string PrimaryKeyName = "PersonHealthProfessionalId";

        public PersonHealthProfessional()
        {
            AuthenticateUser();
        }

        public PersonHealthProfessional(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonHealthProfessional(Guid ownerid, Guid ProfessionTypeId, Guid providerid, Guid personid, Guid professionaluserid,
            DateTime startdate, DateTime enddate,
            Guid professionalid, string freetextname, string phone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ProfessionTypeId", ProfessionTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "professionaluserid", professionaluserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "professionalid", professionalid);
            AddFieldToBusinessDataObject(buisinessDataObject, "freetextname", freetextname);
            AddFieldToBusinessDataObject(buisinessDataObject, "phone", phone);



            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonHealthProfessionalByPersonID(Guid personid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Ascending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public Dictionary<string, object> GetPersonHealthProfessionalByID(Guid PersonHealthProfessionalId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonHealthProfessionalId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonHealthProfessional(Guid PersonHealthProfessionalId)
        {
            this.DeleteRecord(TableName, PersonHealthProfessionalId);
        }
    }
}
