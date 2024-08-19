using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceGLCoding : BaseClass
    {
        public string TableName { get { return "ServiceGLCoding"; } }
        public string PrimaryKeyName { get { return "ServiceGLCodingid"; } }

        public ServiceGLCoding()
        {
            AuthenticateUser();
        }

        public ServiceGLCoding(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceGLCoding(Guid ownerid, bool se2all, bool ccall, Guid? serviceelement1id, Guid? serviceelement2id, Guid? caretypeid, Guid? clientcategoryid, Guid? serviceglcodeid, Guid? TeamId, Guid? teamglcodeid, bool inactive = false, bool validforexport = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            //AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "se2all", se2all);
            AddFieldToBusinessDataObject(buisinessDataObject, "ccall", ccall);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "caretypeid", caretypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "clientcategoryid", clientcategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceglcodeid", serviceglcodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "teamid", TeamId);
            AddFieldToBusinessDataObject(buisinessDataObject, "teamglcodeid", teamglcodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", validforexport);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByServiceElement1Id(Guid serviceelement1id)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "serviceelement1id", ConditionOperatorType.Equal, serviceelement1id);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ServiceGLCodingid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceGLCodingid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteServiceGLCoding(Guid ServiceGLCodingID)
        {
            this.DeleteRecord(TableName, ServiceGLCodingID);
        }

    }
}
