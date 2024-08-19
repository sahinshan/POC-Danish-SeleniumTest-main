using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceProvision : BaseClass
    {
        public string TableName { get { return "ServiceProvision"; } }
        public string PrimaryKeyName { get { return "serviceprovisionid"; } }

        public ServiceProvision()
        {
            AuthenticateUser();
        }

        public ServiceProvision(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceProvision(Guid ownerid, Guid responsibleuserid, Guid personid, Guid serviceprovisionstatusid, Guid serviceelement1id, Guid serviceelement2id,
            Guid financeclientcategoryid, Guid? glcodeid, Guid rateunitid, Guid serviceprovisionstartreasonid, Guid? serviceprovisionendreasonid, Guid purchasingteamid,
            Guid serviceprovidedid, Guid providerid, Guid authorisedbysystemuserid, Guid placementroomtypeid,
            DateTime? actualstartdate, DateTime? actualenddate, DateTime? authorisationdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstatusid", serviceprovisionstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeclientcategoryid", financeclientcategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "glcodeid", glcodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstartreasonid", serviceprovisionstartreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionendreasonid", serviceprovisionendreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "purchasingteamid", purchasingteamid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisedbysystemuserid", authorisedbysystemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "placementroomtypeid", placementroomtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdate", actualstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualenddate", actualenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisationdate", authorisationdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "frequencyinweeks", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalunits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalvisits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "raterequired", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialassessmentallocated", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowanceallocated", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "linkedtos117", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceProvision(Guid ownerid, Guid responsibleuserid, Guid personid, Guid serviceprovisionstatusid, Guid serviceelement1id, Guid serviceelement2id,
                   Guid? financeclientcategoryid, Guid? glcodeid, Guid rateunitid, Guid serviceprovisionstartreasonid, Guid? serviceprovisionendreasonid, Guid? purchasingteamid,
                   Guid? serviceprovidedid, Guid? providerid, Guid? authorisedbysystemuserid, Guid placementroomtypeid,
                   DateTime? plannedstartdate, DateTime? actualstartdate, DateTime? plannedenddate, DateTime? actualenddate, DateTime? authorisationdate, Guid? servicepackageid = null, bool raterequired = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstatusid", serviceprovisionstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeclientcategoryid", financeclientcategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "glcodeid", glcodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstartreasonid", serviceprovisionstartreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionendreasonid", serviceprovisionendreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "purchasingteamid", purchasingteamid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisedbysystemuserid", authorisedbysystemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "placementroomtypeid", placementroomtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdate", plannedstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdate", actualstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedenddate", plannedenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualenddate", actualenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisationdate", authorisationdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "servicepackageid", servicepackageid);
            AddFieldToBusinessDataObject(buisinessDataObject, "frequencyinweeks", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalunits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalvisits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "raterequired", raterequired);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialassessmentallocated", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowanceallocated", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "linkedtos117", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceProvisionForBrokerageEpisode(Guid ownerid, Guid responsibleuserid, Guid personid, Guid serviceprovisionstatusid, Guid serviceelement1id, Guid serviceelement2id,
            Guid? financeclientcategoryid, Guid glcodeid, Guid rateunitid, Guid serviceprovisionstartreasonid, Guid? serviceprovisionendreasonid, Guid purchasingteamid,
            Guid? serviceprovidedid, Guid? providerid, Guid? authorisedbysystemuserid, Guid placementroomtypeid,
            DateTime actualstartdate, DateTime? actualenddate, DateTime? authorisationdate, Guid brokerageepisodeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstatusid", serviceprovisionstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeclientcategoryid", financeclientcategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "glcodeid", glcodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstartreasonid", serviceprovisionstartreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionendreasonid", serviceprovisionendreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "purchasingteamid", purchasingteamid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisedbysystemuserid", authorisedbysystemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "placementroomtypeid", placementroomtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdate", actualstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualenddate", actualenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisationdate", authorisationdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "brokerageepisodeid", brokerageepisodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "frequencyinweeks", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalunits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalvisits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "raterequired", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialassessmentallocated", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowanceallocated", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "linkedtos117", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateNewServiceProvision(Guid ownerid, Guid responsibleuserid, Guid personid, Guid serviceprovisionstatusid, Guid serviceelement1id, Guid serviceelement2id,
        Guid rateunitid, Guid serviceprovisionstartreasonid, int frequencyinweeks, Guid placementroomtype, DateTime actualstartdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstatusid", serviceprovisionstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstartreasonid", serviceprovisionstartreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdate", actualstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "placementroomtype", placementroomtype);
            AddFieldToBusinessDataObject(buisinessDataObject, "frequencyinweeks", frequencyinweeks);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalunits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalvisits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "raterequired", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialassessmentallocated", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowanceallocated", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "linkedtos117", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceProvision(Guid ownerid, Guid responsibleuserid, Guid personid, Guid serviceprovisionstatusid,
            Guid serviceelement1id, Guid serviceelement2id,
            Guid rateunitid, Guid serviceprovisionstartreasonid, Guid? serviceprovisionendreasonid, Guid purchasingteamid,
            Guid serviceprovidedid, Guid providerid, Guid authorisedbysystemuserid, Guid placementroomtypeid,
            DateTime actualstartdate, DateTime actualenddate, DateTime? authorisationdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstatusid", serviceprovisionstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstartreasonid", serviceprovisionstartreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionendreasonid", serviceprovisionendreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "purchasingteamid", purchasingteamid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisedbysystemuserid", authorisedbysystemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "placementroomtypeid", placementroomtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdate", actualstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualenddate", actualenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisationdate", authorisationdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "purchasingteamid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "frequencyinweeks", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalunits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalvisits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "raterequired", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialassessmentallocated", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowanceallocated", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "linkedtos117", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceProvision(Guid ownerid, Guid responsibleuserid, Guid personid, Guid serviceprovisionstatusid, Guid serviceelement1id, Guid CareTypeId,
            Guid serviceprovisionstartreasonid,
            Guid authorisedbysystemuserid, Guid placementroomtypeid,
            DateTime actualstartdate, DateTime authorisationdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstatusid", serviceprovisionstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareTypeId", CareTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstartreasonid", serviceprovisionstartreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisedbysystemuserid", authorisedbysystemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "placementroomtypeid", placementroomtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdate", actualstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisationdate", authorisationdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "frequencyinweeks", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalunits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalvisits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "raterequired", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialassessmentallocated", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowanceallocated", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "linkedtos117", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return CreateRecord(buisinessDataObject);
        }





        public Guid CreateServiceProvision(Guid ownerid, Guid responsibleuserid, Guid personid, Guid serviceprovisionstatusid,
            Guid serviceelement1id, Guid CareTypeId,
            Guid serviceprovisionstartreasonid, DateTime? actualstartdate, DateTime? actualenddate, Guid? serviceprovisionendreasonid,
            Guid purchasingteamid, Guid providerid, Guid approvedcaretypeid,
            Guid placementroomtypeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            //General
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstatusid", serviceprovisionstatusid);

            //Service Request
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareTypeId", CareTypeId);

            //Dates
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstartreasonid", serviceprovisionstartreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdate", actualstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualenddate", actualenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionendreasonid", serviceprovisionendreasonid);

            //Commissioning
            AddFieldToBusinessDataObject(buisinessDataObject, "purchasingteamid", purchasingteamid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "approvedcaretypeid", approvedcaretypeid);


            //Related Information
            AddFieldToBusinessDataObject(buisinessDataObject, "placementroomtypeid", placementroomtypeid);



            AddFieldToBusinessDataObject(buisinessDataObject, "frequencyinweeks", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "raterequired", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialassessmentallocated", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowanceallocated", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "linkedtos117", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return CreateRecord(buisinessDataObject);
        }


        public Guid CreateServiceProvision(Guid ownerid, Guid responsibleuserid, Guid personid, Guid serviceprovisionstatusid,
            Guid serviceelement1id, Guid serviceelement2id, Guid rateunitid, Guid serviceprovisionstartreasonid,
            Guid placementroomtypeid, DateTime actualstartdate, bool raterequired = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstatusid", serviceprovisionstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstartreasonid", serviceprovisionstartreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "placementroomtypeid", placementroomtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdate", actualstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "frequencyinweeks", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalunits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalvisits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "raterequired", raterequired);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialassessmentallocated", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowanceallocated", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "linkedtos117", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceProvisionWithoutRateRequired(Guid ownerid, Guid responsibleuserid, Guid personid, Guid serviceprovisionstatusid, Guid serviceelement1id, Guid serviceelement2id,
                   Guid? financeclientcategoryid, Guid? glcodeid, Guid rateunitid, Guid serviceprovisionstartreasonid, Guid? serviceprovisionendreasonid, Guid purchasingteamid,
                   DateTime? plannedstartdate, DateTime? actualstartdate, DateTime? plannedenddate, DateTime? actualenddate,
                   Guid serviceprovidedid, Guid providerid, Guid authorisedbysystemuserid, Guid placementroomtypeid, DateTime? authorisationdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstatusid", serviceprovisionstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeclientcategoryid", financeclientcategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "glcodeid", glcodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstartreasonid", serviceprovisionstartreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionendreasonid", serviceprovisionendreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "purchasingteamid", purchasingteamid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisedbysystemuserid", authorisedbysystemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "placementroomtypeid", placementroomtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdate", plannedstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdate", actualstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedenddate", plannedenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualenddate", actualenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisationdate", authorisationdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "frequencyinweeks", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalunits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalvisits", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialassessmentallocated", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowanceallocated", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "linkedtos117", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return CreateRecord(buisinessDataObject);
        }



        public List<Guid> GetAll()
        {
            DataQuery query = this.GetDataQueryObject("ServiceProvision", false, "ServiceProvisionId");

            this.AddReturnField(query, "ServiceProvision", "ServiceProvisionid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "ServiceProvisionid");
        }

        public List<Guid> GetServiceProvisionByNumber(string ServiceProvisionNumber)
        {
            DataQuery query = this.GetDataQueryObject("ServiceProvision", false, "ServiceProvisionId");
            this.BaseClassAddTableCondition(query, "ServiceProvisionNumber", ConditionOperatorType.Equal, ServiceProvisionNumber);
            this.AddReturnField(query, "ServiceProvision", "ServiceProvisionid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "ServiceProvisionid");
        }

        public Dictionary<string, object> GetByID(Guid serviceprovisionid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "serviceprovisionid", ConditionOperatorType.Equal, serviceprovisionid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetServiceProvisionById(Guid serviceprovisionid, params string[] fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, fields);
            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, serviceprovisionid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetServiceProvisionByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject("ServiceProvision", false, "ServiceProvisionId");
            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);
            this.AddReturnField(query, "ServiceProvision", "ServiceProvisionid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "ServiceProvisionid");
        }

        public List<Guid> GetServiceProvisionByServiceElement1AndCareType(Guid personid, Guid ServiceElement1Id, Guid CareTypeId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);
            this.BaseClassAddTableCondition(query, "ServiceElement1Id", ConditionOperatorType.Equal, ServiceElement1Id);
            this.BaseClassAddTableCondition(query, "CareTypeId", ConditionOperatorType.Equal, CareTypeId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetServiceProvisionByServiceElement1AndServiceElement2(Guid personid, Guid ServiceElement1Id, Guid ServiceElement2Id)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);
            this.BaseClassAddTableCondition(query, "ServiceElement1Id", ConditionOperatorType.Equal, ServiceElement1Id);
            this.BaseClassAddTableCondition(query, "ServiceElement2Id", ConditionOperatorType.Equal, ServiceElement2Id);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetServiceProvisionByEpisodeID(Guid brokerageepisodeid)
        {
            DataQuery query = this.GetDataQueryObject("ServiceProvision", false, "ServiceProvisionId");
            this.BaseClassAddTableCondition(query, "brokerageepisodeid", ConditionOperatorType.Equal, brokerageepisodeid);
            this.AddReturnField(query, "ServiceProvision", "ServiceProvisionid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "ServiceProvisionid");
        }

        public void UpdateServiceProvisionEndDates(Guid ServiceProvisionID, DateTime? PlannedEndDate, DateTime? ActualEndDate, Guid? ServiceProvisionEndReasonId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ServiceProvision", "serviceprovisionid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ServiceProvisionID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PlannedEndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, PlannedEndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ActualEndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, ActualEndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ServiceProvisionEndReasonId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ServiceProvisionEndReasonId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateServiceProvisionStatus(Guid ServiceProvisionID, Guid StatusID)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ServiceProvision", "serviceprovisionid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ServiceProvisionID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstatusid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, StatusID);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateServiceProvisionAuthorisationdateDate(Guid ServiceProvisionID, DateTime? authorisationdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ServiceProvision", "serviceprovisionid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ServiceProvisionID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "authorisationdate", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, authorisationdate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteServiceProvision(Guid ServiceProvisionID)
        {
            this.DeleteRecord(TableName, ServiceProvisionID);
        }

    }
}
