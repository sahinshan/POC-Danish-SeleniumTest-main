using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Globalization;
using CareWorks.Foundation.Enums;

namespace Phoenix.UnitTests
{
    [TestClass]
    public class Security_DataRestriction_UnitTests
    {
        string appKey = "9B31281E8FB34A992876086FA57C9F695D6A5D5F81E2817A94487E691394878B";
        string appSecret = "E6FF366DC7F9D5B6A7E1F07C6066C039";


        private string _accessToken;
        public string AccessToken { get { return _accessToken; } set { _accessToken = value; } }


        private CareDirector.Sdk.Client.Interfaces.IBusinessDataProvider _DataProvider;
        private CareDirector.Sdk.Client.Interfaces.IBusinessDataProvider DataProvider
        {
            get
            {
                if (_DataProvider == null)
                    _DataProvider = new CareDirector.Sdk.Services.BusinessDataService(AccessToken, 10);

                return _DataProvider;
            }
            set
            {
                _DataProvider = value;
            }
        }


        private CareDirector.Sdk.Client.Interfaces.ISecurityDataProvider _SecurityDataProvider;
        private CareDirector.Sdk.Client.Interfaces.ISecurityDataProvider SecurityDataProvider
        {
            get
            {
                if (_SecurityDataProvider == null)
                    _SecurityDataProvider = new CareDirector.Sdk.Services.SecurityService(AccessToken, 10);

                return _SecurityDataProvider;
            }
            set
            {
                _SecurityDataProvider = value;
            }
        }



        [Description("Data Restriction - Case 3 - User belongs to team that owns the record - Record has 'Allow Users' data restriction - User is NOT in list of allowed users - Users should NOT be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case3_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid viewid = new Guid("8a660418-269b-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("professional", viewid);
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("325fbe72-f8c7-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(null, professionalInfo.DataRestrictionId);
            Assert.IsTrue(professionalInfo.IsRecordRestricted);
            
            Assert.AreEqual(null, professionalInfo.FieldCollection["title"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["email"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("325fbe72-f8c7-e811-80dc-0050560502cc"), professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("f46af4f8-77e4-e611-80d4-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }

        [Description("(ExecuteDataQuery method) Data Restriction - Case 3 - User belongs to team that owns the record - Record has 'Allow Users' data restriction - User is NOT in list of allowed users - Users should NOT be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case3_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("professional", "title", "firstname", "lastname", "ProfessionTypeId", "professionalcode", "email", "mobilephone", "businessphone", "fulladdress", "professionalid", "fullname", "datarestrictionid", "ownerid");
            query.AddThisTableCondition("ProfessionalId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, new Guid("325fbe72-f8c7-e811-80dc-0050560502cc"));
            var bdcResponse = DataProvider.ExecuteDataQuery(query);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("325fbe72-f8c7-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(null, professionalInfo.DataRestrictionId);
            Assert.IsTrue(professionalInfo.IsRecordRestricted);

            Assert.AreEqual(null, professionalInfo.FieldCollection["title"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["email"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("325fbe72-f8c7-e811-80dc-0050560502cc"), professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("f46af4f8-77e4-e611-80d4-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }




        [Description("Data Restriction - Case 4 - User belongs to team that owns the record - Record has 'Allow Users' data restriction - User is in list of allowed users - Users should be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case4_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction1", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid viewid = new Guid("8a660418-269b-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("professional", viewid);
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("325fbe72-f8c7-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(new Guid("93e11a16-70e3-e811-80dc-0050560502cc"), professionalInfo.DataRestrictionId.Value);
            Assert.IsFalse(professionalInfo.IsRecordRestricted);
            Assert.AreEqual("Mr", (string)professionalInfo.FieldCollection["title"]);
            Assert.AreEqual("Jonathan", (string)professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual("Davis", (string)professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(new Guid("3024d767-899c-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual("6543", (string)professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual("jonathandavis@fakemail.com", (string)professionalInfo.FieldCollection["email"]);
            Assert.AreEqual("1993484", (string)professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual("123", (string)professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual("PNA, PNO, ST, VL, TW, CO, PC", (string)professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("325fbe72-f8c7-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(new Guid("93e11a16-70e3-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("f46af4f8-77e4-e611-80d4-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }

        [Description("(ExecuteDataQuery method) Data Restriction - Case 4 - User belongs to team that owns the record - Record has 'Allow Users' data restriction - User is in list of allowed users - Users should be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case4_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction1", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("professional", "title", "firstname", "lastname", "ProfessionTypeId", "professionalcode", "email", "mobilephone", "businessphone", "fulladdress", "professionalid", "fullname", "datarestrictionid", "ownerid");
            query.AddThisTableCondition("ProfessionalId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, new Guid("325fbe72-f8c7-e811-80dc-0050560502cc"));
            var bdcResponse = DataProvider.ExecuteDataQuery(query);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("325fbe72-f8c7-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(new Guid("93e11a16-70e3-e811-80dc-0050560502cc"), professionalInfo.DataRestrictionId.Value);
            Assert.IsFalse(professionalInfo.IsRecordRestricted);
            Assert.AreEqual("Mr", (string)professionalInfo.FieldCollection["title"]);
            Assert.AreEqual("Jonathan", (string)professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual("Davis", (string)professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(new Guid("3024d767-899c-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual("6543", (string)professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual("jonathandavis@fakemail.com", (string)professionalInfo.FieldCollection["email"]);
            Assert.AreEqual("1993484", (string)professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual("123", (string)professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual("PNA, PNO, ST, VL, TW, CO, PC", (string)professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("325fbe72-f8c7-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual("Jonathan Davis", (string)professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(new Guid("93e11a16-70e3-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("f46af4f8-77e4-e611-80d4-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }



        [Description("Data Restriction - Case 5 - User belongs to team that owns the record - Record has 'Allow Team' data restriction - User Team is NOT in list of allowed teams - Users should NOT be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case5_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid viewid = new Guid("8a660418-269b-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("professional", viewid);
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("f08b2769-e9ca-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(null, professionalInfo.DataRestrictionId);
            Assert.IsTrue(professionalInfo.IsRecordRestricted);

            Assert.AreEqual(null, professionalInfo.FieldCollection["title"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["email"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("f08b2769-e9ca-e811-80dc-0050560502cc"), professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("8fc501d0-f6c7-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }

        [Description("(ExecuteDataQuery method) Data Restriction - Case 5 - User belongs to team that owns the record - Record has 'Allow Team' data restriction - User Team is NOT in list of allowed teams - Users should NOT be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case5_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("professional", "title", "firstname", "lastname", "ProfessionTypeId", "professionalcode", "email", "mobilephone", "businessphone", "fulladdress", "professionalid", "fullname", "datarestrictionid", "ownerid");
            query.AddThisTableCondition("ProfessionalId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, new Guid("f08b2769-e9ca-e811-80dc-0050560502cc"));
            var bdcResponse = DataProvider.ExecuteDataQuery(query);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("f08b2769-e9ca-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(null, professionalInfo.DataRestrictionId);
            Assert.IsTrue(professionalInfo.IsRecordRestricted);

            Assert.AreEqual(null, professionalInfo.FieldCollection["title"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["email"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("f08b2769-e9ca-e811-80dc-0050560502cc"), professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("8fc501d0-f6c7-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }




        [Description("Data Restriction - Case 6 - User belongs to team that owns the record - Record has 'Allow Team' data restriction - User Team is in list of allowed Teams - Users should be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case6_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction2", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid viewid = new Guid("8a660418-269b-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("professional", viewid);
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("f08b2769-e9ca-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(new Guid("7840fe09-a7d7-e811-80dc-0050560502cc"), professionalInfo.DataRestrictionId.Value);
            Assert.IsFalse(professionalInfo.IsRecordRestricted);

            Assert.AreEqual("Mr", (string)professionalInfo.FieldCollection["title"]);
            Assert.AreEqual("Antony", (string)professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual("Malleck", (string)professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(new Guid("3024d767-899c-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, (string)professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual("Malleck@tempmail.com", (string)professionalInfo.FieldCollection["email"]);
            Assert.AreEqual("7530823", (string)professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual("111", (string)professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual("PNA, PNO, ST, VL, TW, CO, PC", (string)professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("f08b2769-e9ca-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(new Guid("7840fe09-a7d7-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("8fc501d0-f6c7-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }

        [Description("(ExecuteDataQuery method) Data Restriction - Case 6 - User belongs to team that owns the record - Record has 'Allow Team' data restriction - User Team is in list of allowed Teams - Users should be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case6_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction2", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("professional", "title", "firstname", "lastname", "ProfessionTypeId", "professionalcode", "email", "mobilephone", "businessphone", "fulladdress", "professionalid", "fullname", "datarestrictionid", "ownerid");
            query.AddThisTableCondition("ProfessionalId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, new Guid("f08b2769-e9ca-e811-80dc-0050560502cc"));
            var bdcResponse = DataProvider.ExecuteDataQuery(query);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("f08b2769-e9ca-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(new Guid("7840fe09-a7d7-e811-80dc-0050560502cc"), professionalInfo.DataRestrictionId.Value);
            Assert.IsFalse(professionalInfo.IsRecordRestricted);

            Assert.AreEqual("Mr", (string)professionalInfo.FieldCollection["title"]);
            Assert.AreEqual("Antony", (string)professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual("Malleck", (string)professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(new Guid("3024d767-899c-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, (string)professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual("Malleck@tempmail.com", (string)professionalInfo.FieldCollection["email"]);
            Assert.AreEqual("7530823", (string)professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual("111", (string)professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual("PNA, PNO, ST, VL, TW, CO, PC", (string)professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("f08b2769-e9ca-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual("Antony Malleck", (string)professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(new Guid("7840fe09-a7d7-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("8fc501d0-f6c7-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }




        [Description("Data Restriction - Case 7 - User belongs to team that owns the record - Record has 'Deny User' data restriction - User is NOT in list of Denied users - Users should be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case7_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid viewid = new Guid("8a660418-269b-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("professional", viewid);
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("3346E570-FBCA-E811-80DC-0050560502CC");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(new Guid("ECF99A76-FACA-E811-80DC-0050560502CC"), professionalInfo.DataRestrictionId.Value);
            Assert.IsFalse(professionalInfo.IsRecordRestricted);

            Assert.AreEqual("Mr", (string)professionalInfo.FieldCollection["title"]);
            Assert.AreEqual("Jordan", (string)professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual("Edison", (string)professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(new Guid("3024d767-899c-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, (string)professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual("jordan@tempmail.com", (string)professionalInfo.FieldCollection["email"]);
            Assert.AreEqual("1958689", (string)professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual("111", (string)professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual("PNA, PNO, ST, VL, TW, CO, PC", (string)professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("3346e570-fbca-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(new Guid("ecf99a76-faca-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("8fc501d0-f6c7-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }

        [Description("(ExecuteDataQuery method) Data Restriction - Case 7 - User belongs to team that owns the record - Record has 'Deny User' data restriction - User is NOT in list of Denied users - Users should be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case7_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("professional", "title", "firstname", "lastname", "ProfessionTypeId", "professionalcode", "email", "mobilephone", "businessphone", "fulladdress", "professionalid", "fullname", "datarestrictionid", "ownerid");
            query.AddThisTableCondition("ProfessionalId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, new Guid("3346E570-FBCA-E811-80DC-0050560502CC"));
            var bdcResponse = DataProvider.ExecuteDataQuery(query);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("3346E570-FBCA-E811-80DC-0050560502CC");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(new Guid("ECF99A76-FACA-E811-80DC-0050560502CC"), professionalInfo.DataRestrictionId.Value);
            Assert.IsFalse(professionalInfo.IsRecordRestricted);

            Assert.AreEqual("Mr", (string)professionalInfo.FieldCollection["title"]);
            Assert.AreEqual("Jordan", (string)professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual("Edison", (string)professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(new Guid("3024d767-899c-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, (string)professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual("jordan@tempmail.com", (string)professionalInfo.FieldCollection["email"]);
            Assert.AreEqual("1958689", (string)professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual("111", (string)professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual("PNA, PNO, ST, VL, TW, CO, PC", (string)professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("3346e570-fbca-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual("Jordan EDISON", (string)professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(new Guid("ecf99a76-faca-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("8fc501d0-f6c7-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }



        [Description("Data Restriction - Case 8 - User belongs to team that owns the record - Record has 'Deny User' data restriction - User is NOT in list of Denied users - Users should NOT be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case8_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction3", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid viewid = new Guid("8a660418-269b-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("professional", viewid);
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("3346E570-FBCA-E811-80DC-0050560502CC");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(null, professionalInfo.DataRestrictionId);
            Assert.IsTrue(professionalInfo.IsRecordRestricted);

            Assert.AreEqual(null, professionalInfo.FieldCollection["title"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["email"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("3346E570-FBCA-E811-80DC-0050560502CC"), professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("8fc501d0-f6c7-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }

        [Description("(ExecuteDataQuery method) Data Restriction - Case 8 - User belongs to team that owns the record - Record has 'Deny User' data restriction - User is NOT in list of Denied users - Users should NOT be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case8_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction3", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("professional", "title", "firstname", "lastname", "ProfessionTypeId", "professionalcode", "email", "mobilephone", "businessphone", "fulladdress", "professionalid", "fullname", "datarestrictionid", "ownerid");
            query.AddThisTableCondition("ProfessionalId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, new Guid("3346E570-FBCA-E811-80DC-0050560502CC"));
            var bdcResponse = DataProvider.ExecuteDataQuery(query);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("3346E570-FBCA-E811-80DC-0050560502CC");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(null, professionalInfo.DataRestrictionId);
            Assert.IsTrue(professionalInfo.IsRecordRestricted);

            Assert.AreEqual(null, professionalInfo.FieldCollection["title"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["email"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("3346E570-FBCA-E811-80DC-0050560502CC"), professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("8fc501d0-f6c7-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }




        [Description("Data Restriction - Case 9 - User has record shared with him - Record has 'Allow User' data restriction - User is NOT in list of allowed users - Users should NOT be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case9_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction5", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid viewid = new Guid("8a660418-269b-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("professional", viewid);
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("cce6a061-15cb-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(null, professionalInfo.DataRestrictionId);
            Assert.IsTrue(professionalInfo.IsRecordRestricted);

            Assert.AreEqual(null, professionalInfo.FieldCollection["title"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["email"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("cce6a061-15cb-e811-80dc-0050560502cc"), professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }

        [Description("(ExecuteDataQuery method) Data Restriction - Case 9 - User has record shared with him - Record has 'Allow User' data restriction - User is NOT in list of allowed users - Users should NOT be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case9_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction5", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("professional", "title", "firstname", "lastname", "ProfessionTypeId", "professionalcode", "email", "mobilephone", "businessphone", "fulladdress", "professionalid", "fullname", "datarestrictionid", "ownerid");
            query.AddThisTableCondition("ProfessionalId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, new Guid("cce6a061-15cb-e811-80dc-0050560502cc"));
            var bdcResponse = DataProvider.ExecuteDataQuery(query);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("cce6a061-15cb-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(null, professionalInfo.DataRestrictionId);
            Assert.IsTrue(professionalInfo.IsRecordRestricted);

            
            Assert.AreEqual(null, professionalInfo.FieldCollection["title"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["email"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("cce6a061-15cb-e811-80dc-0050560502cc"), professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }



        [Description("Data Restriction - Case 10 - User has record shared with him - Record has 'Allow User' data restriction - User is in list of allowed users  - Users should be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case10_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction4", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid viewid = new Guid("8a660418-269b-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("professional", viewid);
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("cce6a061-15cb-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(new Guid("805E42C0-F5C7-E811-80DC-0050560502CC"), professionalInfo.DataRestrictionId.Value);
            Assert.IsFalse(professionalInfo.IsRecordRestricted);

             
            Assert.AreEqual(null, (string)professionalInfo.FieldCollection["title"]);
            Assert.AreEqual("Mario", (string)professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual("Raymond", (string)professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(new Guid("3024d767-899c-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, (string)professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual("Raymond@tempemail.com", (string)professionalInfo.FieldCollection["email"]);
            Assert.AreEqual("2317209", (string)professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual("111", (string)professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual("PNA, PNO, ST, VL, TW, CO, PC", (string)professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("cce6a061-15cb-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(new Guid("805E42C0-F5C7-E811-80DC-0050560502CC"), (Guid)professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }

        [Description("(ExecuteDataQuery method) Data Restriction - Case 10 - User has record shared with him - Record has 'Allow User' data restriction - User is in list of allowed users  - Users should be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case10_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction4", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("professional", "title", "firstname", "lastname", "ProfessionTypeId", "professionalcode", "email", "mobilephone", "businessphone", "fulladdress", "professionalid", "fullname", "datarestrictionid", "ownerid");
            query.AddThisTableCondition("ProfessionalId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, new Guid("cce6a061-15cb-e811-80dc-0050560502cc"));
            var bdcResponse = DataProvider.ExecuteDataQuery(query);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("cce6a061-15cb-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(new Guid("805E42C0-F5C7-E811-80DC-0050560502CC"), professionalInfo.DataRestrictionId.Value);
            Assert.IsFalse(professionalInfo.IsRecordRestricted);

            Assert.AreEqual(null, (string)professionalInfo.FieldCollection["title"]);
            Assert.AreEqual("Mario", (string)professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual("Raymond", (string)professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(new Guid("3024d767-899c-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, (string)professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual("Raymond@tempemail.com", (string)professionalInfo.FieldCollection["email"]);
            Assert.AreEqual("2317209", (string)professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual("111", (string)professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual("PNA, PNO, ST, VL, TW, CO, PC", (string)professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("cce6a061-15cb-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual("Mario Raymond", (string)professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(new Guid("805E42C0-F5C7-E811-80DC-0050560502CC"), (Guid)professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }



        [Description("Data Restriction - Case 11 - User has record shared with him - " +
            "Record has 'Allow Team' data restriction - User Team is NOT in list of allowed Teams - " +
            "Users should NOT be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case11_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction4", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid viewid = new Guid("8a660418-269b-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("professional", viewid);
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("3b300f07-3dcd-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(null, professionalInfo.DataRestrictionId);
            Assert.IsTrue(professionalInfo.IsRecordRestricted);

            Assert.AreEqual(null, professionalInfo.FieldCollection["title"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["email"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("3b300f07-3dcd-e811-80dc-0050560502cc"), professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }

        [Description("(ExecuteDataQuery method) Data Restriction - Case 11 - " +
            "User has record shared with him - Record has 'Allow Team' data restriction - User Team is NOT in list of allowed Teams - " +
            "Users should NOT be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case11_TestMethod2()
        {
            Guid professionalID = new Guid("3b300f07-3dcd-e811-80dc-0050560502cc");

            var authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction4", "Passw0rd_!");
            var authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            var query = new CareDirector.Sdk.Query.DataQuery("professional", "title", "firstname", "lastname", "ProfessionTypeId", "professionalcode", "email", "mobilephone", "businessphone", "fulladdress", "professionalid", "fullname", "datarestrictionid", "ownerid");
            query.AddThisTableCondition("ProfessionalId", ConditionOperatorType.Equal, professionalID);
            var bdcResponse = DataProvider.ExecuteDataQuery(query);

            //Validate that the professional information is not accessible
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(null, professionalInfo.DataRestrictionId);
            Assert.IsTrue(professionalInfo.IsRecordRestricted);

            Assert.AreEqual(null, professionalInfo.FieldCollection["title"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["email"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(professionalID, professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }



        [Description("Data Restriction - Case 12 - " +
            "User has record shared with him - Record has 'Allow Team' data restriction - User Team is in list of allowed Teams - " +
            "Users should be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case12_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction2", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid viewid = new Guid("8a660418-269b-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("professional", viewid);
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("3b300f07-3dcd-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(new Guid("8D28AE42-EBCA-E811-80DC-0050560502CC"), professionalInfo.DataRestrictionId.Value);
            Assert.IsFalse(professionalInfo.IsRecordRestricted);

            Assert.AreEqual("Mr", (string)professionalInfo.FieldCollection["title"]);
            Assert.AreEqual("Antonio", (string)professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual("Rogerio", (string)professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(new Guid("3024d767-899c-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, (string)professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual("Rogerio@temp.com", (string)professionalInfo.FieldCollection["email"]);
            Assert.AreEqual("4411543", (string)professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual("111", (string)professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual("PNA, PNO, ST, VL, TO, CO, CR0 3RL", (string)professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("3b300f07-3dcd-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(new Guid("8D28AE42-EBCA-E811-80DC-0050560502CC"), (Guid)professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }

        [Description("(ExecuteDataQuery method) Data Restriction - Case 12 - " +
            "User has record shared with him - Record has 'Allow Team' data restriction - User Team is in list of allowed Teams - " +
            "Users should be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case12_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction2", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("professional", "title", "firstname", "lastname", "ProfessionTypeId", "professionalcode", "email", "mobilephone", "businessphone", "fulladdress", "professionalid", "fullname", "datarestrictionid", "ownerid");
            query.AddThisTableCondition("ProfessionalId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, new Guid("3b300f07-3dcd-e811-80dc-0050560502cc"));
            var bdcResponse = DataProvider.ExecuteDataQuery(query);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("3b300f07-3dcd-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(new Guid("8D28AE42-EBCA-E811-80DC-0050560502CC"), professionalInfo.DataRestrictionId.Value);
            Assert.IsFalse(professionalInfo.IsRecordRestricted);

            Assert.AreEqual("Mr", (string)professionalInfo.FieldCollection["title"]);
            Assert.AreEqual("Antonio", (string)professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual("Rogerio", (string)professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(new Guid("3024d767-899c-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, (string)professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual("Rogerio@temp.com", (string)professionalInfo.FieldCollection["email"]);
            Assert.AreEqual("4411543", (string)professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual("111", (string)professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual("PNA, PNO, ST, VL, TO, CO, CR0 3RL", (string)professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("3b300f07-3dcd-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual("Antonio Rogerio", (string)professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(new Guid("8D28AE42-EBCA-E811-80DC-0050560502CC"), (Guid)professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }




        [Description("Data Restriction - Case 13 - User has record shared with him - Record has 'Deny User' data restriction - User is NOT in list of denied users - Users should be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case13_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction2", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid viewid = new Guid("8a660418-269b-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("professional", viewid);
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("f10f318a-44cd-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(new Guid("ECF99A76-FACA-E811-80DC-0050560502CC"), professionalInfo.DataRestrictionId.Value);
            Assert.IsFalse(professionalInfo.IsRecordRestricted);

            Assert.AreEqual("Mr", (string)professionalInfo.FieldCollection["title"]);
            Assert.AreEqual("Ricardi", (string)professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual("Bosanova", (string)professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(new Guid("3024d767-899c-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, (string)professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual("Ricardi@temp.com", (string)professionalInfo.FieldCollection["email"]);
            Assert.AreEqual("4346286", (string)professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual("111", (string)professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual("PNA, PNO, ST, VL, TW, CO, PC", (string)professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("f10f318a-44cd-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(new Guid("ECF99A76-FACA-E811-80DC-0050560502CC"), (Guid)professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }

        [Description("(ExecuteDataQuery method) Data Restriction - Case 13 - User has record shared with him - Record has 'Deny User' data restriction - User is NOT in list of denied users - Users should be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case13_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction2", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("professional", "title", "firstname", "lastname", "ProfessionTypeId", "professionalcode", "email", "mobilephone", "businessphone", "fulladdress", "professionalid", "fullname", "datarestrictionid", "ownerid");
            query.AddThisTableCondition("ProfessionalId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, new Guid("f10f318a-44cd-e811-80dc-0050560502cc"));
            var bdcResponse = DataProvider.ExecuteDataQuery(query);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("f10f318a-44cd-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(new Guid("ECF99A76-FACA-E811-80DC-0050560502CC"), professionalInfo.DataRestrictionId.Value);
            Assert.IsFalse(professionalInfo.IsRecordRestricted);

            Assert.AreEqual("Mr", (string)professionalInfo.FieldCollection["title"]);
            Assert.AreEqual("Ricardi", (string)professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual("Bosanova", (string)professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(new Guid("3024d767-899c-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, (string)professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual("Ricardi@temp.com", (string)professionalInfo.FieldCollection["email"]);
            Assert.AreEqual("4346286", (string)professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual("111", (string)professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual("PNA, PNO, ST, VL, TW, CO, PC", (string)professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("f10f318a-44cd-e811-80dc-0050560502cc"), (Guid)professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual("Ricardi Bosanova", (string)professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(new Guid("ECF99A76-FACA-E811-80DC-0050560502CC"), (Guid)professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }




        [Description("Data Restriction - Case 14 - User has record shared with him - Record has 'Deny User' data restriction - User Team is in list of denied users - Users should NOT be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case14_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction3", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid viewid = new Guid("8a660418-269b-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("professional", viewid);
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("f10f318a-44cd-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(null, professionalInfo.DataRestrictionId);
            Assert.IsTrue(professionalInfo.IsRecordRestricted);

            Assert.AreEqual(null, professionalInfo.FieldCollection["title"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["email"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("f10f318a-44cd-e811-80dc-0050560502cc"), professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }

        [Description("(ExecuteDataQuery method) Data Restriction - Case 14 - User has record shared with him - Record has 'Deny User' data restriction - User Team is in list of denied users - Users should NOT be able to View/Get the record")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_Case14_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserDataRestriction3", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("professional", "title", "firstname", "lastname", "ProfessionTypeId", "professionalcode", "email", "mobilephone", "businessphone", "fulladdress", "professionalid", "fullname", "datarestrictionid", "ownerid");
            query.AddThisTableCondition("ProfessionalId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, new Guid("f10f318a-44cd-e811-80dc-0050560502cc"));
            var bdcResponse = DataProvider.ExecuteDataQuery(query);

            //Validate that the provider information is not accessible
            Guid professionalID = new Guid("f10f318a-44cd-e811-80dc-0050560502cc");
            var professionalInfo = bdcResponse.BusinessDataCollection.Where(x => x.Id.HasValue && x.Id.Value == professionalID).FirstOrDefault();
            Assert.AreEqual(null, professionalInfo.DataRestrictionId);
            Assert.IsTrue(professionalInfo.IsRecordRestricted);

            Assert.AreEqual(null, professionalInfo.FieldCollection["title"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["firstname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["lastname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professiontypeid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["professionalcode"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["email"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["mobilephone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["businessphone"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fulladdress"]);
            Assert.AreEqual(new Guid("f10f318a-44cd-e811-80dc-0050560502cc"), professionalInfo.FieldCollection["professionalid"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["fullname"]);
            Assert.AreEqual(null, professionalInfo.FieldCollection["datarestrictionid"]);
            Assert.AreEqual(new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), (Guid)professionalInfo.FieldCollection["ownerid"]);
        }




        [Description("Use case CD_DR_01 - Data Restrictions Setup - Try to restrict a record for a business object that have the 'Data Restrictions enabled' property set to false")]
        [TestMethod]
        public void PhoenixSecurity_DataRestriction_CD_DR_01_Case11()
        {
            Assert.Inconclusive("Email records can now be restricted. It is necessary to get another business object that cannot be restricted");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);


            //try to restrict a Email record. This Business Object 'Data Restrictions enabled' do not have the property set to true
            Guid emailID = new Guid("71521bdb-1dd2-e811-80dc-0050560502cc");
            Guid dataRestrictionID = new Guid("805e42c0-f5c7-e811-80dc-0050560502cc");
            CareDirector.Sdk.ServiceResponse.AssignDataRestrictionResponse response = 
                DataProvider.RestrictRecords(emailID, "email", dataRestrictionID, CareDirector.Sdk.Enums.AssignDataRestrictionOptionType.All);

            Assert.IsFalse(response.Success);
            Assert.IsTrue(response.HasErrors);
            Assert.IsNotNull(response.Exception);
            Assert.AreEqual("Data Restriction not enabled for this entity", response.Error);

        }






        private CareDirector.Sdk.ServiceRequest.AuthenticateRequest GetAuthenticationRequest(string UserName, string Password)
        {
            DataProvider = null;
            SecurityDataProvider = null;

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = new CareDirector.Sdk.ServiceRequest.AuthenticateRequest
            {
                ApplicationKey = appKey,
                BrowserType = "InternetExplorer",
                BrowserVersion = "11.0",
                MobileOS = CareDirector.Sdk.Enums.MobileOS.Unknown,
                Password = Password,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko",
                UserIPAddress = "192.168.9.43",
                UserName = UserName,
            };
            return authenticateRequest;
        }

        private void SetServiceConnectionDataFromAuthenticationResponse(CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse)
        {
            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            this.AccessToken = authResponse.AccessToken;
            
            this.SecurityDataProvider = null; /*this will reset the security data provider service. the next time the object is called the new service connection will be used (with the security token and all remain information)*/
            this.DataProvider = null; /*this will reset the data provider service. the next time the object is called the new service connection will be used (with the security token and all remain information)*/
        }

        private CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest GetRetrieveMultipleRequest(string BusinessObjectName, Guid DataViewId)
        {
            return new CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest
            {
                BusinessObjectName = BusinessObjectName,
                DataViewId = DataViewId,
                IncludeFormattedData = true,
                PageNumber = 1,
                RecordsPerPage = 100,
                UsePaging = true,
                ViewFor = CareDirector.Sdk.Enums.ViewForType.Unknown,
                ViewGroup = CareDirector.Sdk.Enums.DataViewGroup.System,
                ViewType = CareDirector.Sdk.Enums.DataViewType.Main,
            };

        }
        

    }
}
