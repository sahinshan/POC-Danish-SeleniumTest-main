using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Globalization;

namespace Phoenix.UnitTests
{
    [TestClass]
    public class Security_Sharing_UnitTest
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



        [Description("Dynamic test - Share a record with a user (share privilege) - validate that the user can share the record. Remove the share - validate that the user can no longer share the record")]
        [TestMethod]
        public void PhoenixSecurity_RevokingShare_TestMethod1()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc");
            Guid SecurityTestUser19ID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            Guid SecurityTestUser20ID = new Guid("89B6B8AC-DDC7-E811-80DC-0050560502CC");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser20
            List<Guid> User20RecordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                                     where ent.ShareToId == SecurityTestUser20ID && ent.Id.HasValue
                                                     select ent.Id.Value).ToList();

            //remove all shares to SecurityTestUser20
            foreach(Guid id in User20RecordLevelAccessIDs)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(new List<Guid> { id });
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //get all shares to the user SecurityTestUser19
            List<Guid> User19RecordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                                     where ent.ShareToId == SecurityTestUser19ID && ent.Id.HasValue
                                                     select ent.Id.Value).ToList();

            //remove the shares to the user SecurityTestUser19
            foreach (Guid id in User19RecordLevelAccessIDs)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(new List<Guid> { id });
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //share the record with SecurityTestUser19 with sharing privileges
            CareDirector.Sdk.SystemEntities.RecordLevelAccess rla = GetRecordLevelAccess(providerID, "provider", SecurityTestUser19ID, "Security Test User 19", "systemuser", true, false, false, true);
            CareDirector.Sdk.ServiceResponse.CreateResponse crlar = SecurityDataProvider.GrantRecordLevelAccess(rla);






            //login as SecurityTestUser19
            authenticateRequest = GetAuthenticationRequest("SecurityTestUser19", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //SecurityTestUser19 try to share the record to SecurityTestUser20
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", SecurityTestUser20ID, "Security Test User 20", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);






            //login as the admin
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //Get all shares to the provider record
            recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser20
            User20RecordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                                     where ent.ShareToId == SecurityTestUser20ID && ent.Id.HasValue
                                                     select ent.Id.Value).ToList();

            //remove all shares to SecurityTestUser20
            foreach (Guid id in User20RecordLevelAccessIDs)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(new List<Guid> { id });
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //get all shares to the user SecurityTestUser19
            User19RecordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                                     where ent.ShareToId == SecurityTestUser19ID && ent.Id.HasValue
                                                     select ent.Id.Value).ToList();

            //if no share exist for SecurityTestUser19 then create one
            foreach (Guid id in User19RecordLevelAccessIDs)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(new List<Guid> { id });
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }




            //login as SecurityTestUser19
            authenticateRequest = GetAuthenticationRequest("SecurityTestUser19", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //SecurityTestUser19 try to share the record to SecurityTestUser20
            recordLevelAccess = GetRecordLevelAccess(providerID, "provider", SecurityTestUser20ID, "Security Test User 20", "systemuser");
            createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);

        }





        [Description("Test Case - Sharing Shared Records (1) - Case 1 - Share Record with view access with a User - user do not have share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords1_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser3", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (1) - Case 2 - Share Record with view access with a User - user have Team Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords1_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser15", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (1) - Case 3 - Share Record with view access with a User - user have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords1_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser16", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (1) - Case 4 - Share Record with view access with a User - user have PCBU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords1_TestMethod4()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser17", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (1) - Case 5 - Share Record with view access with a User - user have Org Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords1_TestMethod5()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }


            //login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUser18", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //try to share the record
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }



        [Description("Test Case - Sharing Shared Records (2) - Case 3 - Share Record with edit access with a User - user have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords2_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser16", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("09963924-96b6-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }




        [Description("Test Case - Sharing Shared Records (3) - Case 3 - Share Record with delete access with a User - user have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords3_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser16", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("50fc1b5a-3fb7-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }




        [Description("Test Case - Sharing Shared Records (4) - Case 1 - Share Record with sharing privileges with a User - user do not have share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords4_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser3", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("3bd9c2d3-8aba-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (4) - Case 2 - Share Record with sharing privileges with a User - user have Team Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords4_TestMethod2()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("3bd9c2d3-8aba-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUser15", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }

        [Description("Test Case - Sharing Shared Records (4) - Case 3 - Share Record with sharing privileges with a User - user have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords4_TestMethod3()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("3bd9c2d3-8aba-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUser16", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }

        [Description("Test Case - Sharing Shared Records (4) - Case 4 - Share Record with sharing privileges with a User - user have PCBU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords4_TestMethod4()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("3bd9c2d3-8aba-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUser17", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }





        [Description("Test Case - Sharing Shared Records (5) - Case 1 - Share Record with view access with a User - user team do not have share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords5_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP3", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (5) - Case 2 - Share Record with view access with a User - user team have Team Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords5_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP18", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (5) - Case 3 - Share Record with view access with a User - user team have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords5_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP19", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (5) - Case 4 - Share Record with view access with a User - user team have PCBU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords5_TestMethod4()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP20", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (5) - Case 5 - Share Record with view access with a User - user team have Org Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords5_TestMethod5()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }


            //login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP21", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //try to share the record
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }



        [Description("Test Case - Sharing Shared Records (6) - Case 3 - Share Record with edit access with a User - user team have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords6_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP19", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("e3dadccc-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }



        [Description("Test Case - Sharing Shared Records (7) - Case 3 - Share Record with delete access with a User - user team have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords7_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP19", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("ab0f80f9-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }





        [Description("Test Case - Sharing Shared Records (8) - Case 1 - Share Record with sharing privileges with a User - user Team do not have share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords8_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP3", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("d5ecee40-93c1-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (8) - Case 2 - Share Record with sharing privileges with a User - user Team have Team Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords8_TestMethod2()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("d5ecee40-93c1-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP18", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }

        [Description("Test Case - Sharing Shared Records (8) - Case 3 - Share Record with sharing privileges with a User - user Team have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords8_TestMethod3()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("d5ecee40-93c1-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP19", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }

        [Description("Test Case - Sharing Shared Records (8) - Case 4 - Share Record with sharing privileges with a User - user Team have PCBU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords8_TestMethod4()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("d5ecee40-93c1-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP20", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }




        [Description("Test Case - Sharing Shared Records (9) - Case 1 - Share Record with view access with a Team - user do not have share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords9_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser3", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (9) - Case 2 - Share Record with view access with a Team - user have Team Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords9_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser15", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (9) - Case 3 - Share Record with view access with a Team - user have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords9_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser16", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (9) - Case 4 - Share Record with view access with a Team - user have PCBU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords9_TestMethod4()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser17", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (9) - Case 5 - Share Record with view access with a Team - user have Org Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords9_TestMethod5()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }


            //login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUser18", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //try to share the record
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }




        [Description("Test Case - Sharing Shared Records (10) - Case 3 - Share Record with edit access with a Team - user have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords10_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser16", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("e3dadccc-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }



        [Description("Test Case - Sharing Shared Records (11) - Case 3 - Share Record with delete access with a Team - user have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords11_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser16", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("ab0f80f9-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }





        [Description("Test Case - Sharing Shared Records (12) - Case 1 - Share Record with sharing privileges with a Team - user do not have share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords12_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser3", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("d5ecee40-93c1-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (12) - Case 2 - Share Record with sharing privileges with a Team - user have Team Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords12_TestMethod2()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("d5ecee40-93c1-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUser15", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }

        [Description("Test Case - Sharing Shared Records (12) - Case 3 - Share Record with sharing privileges with a Team - user have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords12_TestMethod3()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("d5ecee40-93c1-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUser16", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }

        [Description("Test Case - Sharing Shared Records (12) - Case 4 - Share Record with sharing privileges with a Team - user have PCBU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords12_TestMethod4()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("d5ecee40-93c1-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUser17", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }





        [Description("Test Case - Sharing Shared Records (13) - Case 1 - Share Record with view access with a Team - user Team do not have share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords13_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP3", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (13) - Case 2 - Share Record with view access with a Team - user Team have Team Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords13_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP18", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (13) - Case 3 - Share Record with view access with a Team - user Team have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords13_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP19", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (13) - Case 4 - Share Record with view access with a Team - user Team have PCBU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords13_TestMethod4()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP20", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (13) - Case 5 - Share Record with view access with a Team - user Team have Org Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords13_TestMethod5()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }


            //login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP21", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //try to share the record
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }




        [Description("Test Case - Sharing Shared Records (14) - Case 3 - Share Record with edit access with a Team - user Team have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords14_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP19", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("09963924-96b6-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }




        [Description("Test Case - Sharing Shared Records (15) - Case 3 - Share Record with delete access with a Team - user Team have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords15_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP19", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("50fc1b5a-3fb7-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("14AB2826-28C7-E811-80DC-0050560502CC");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test Team API Testing", "team");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }




        [Description("Test Case - Sharing Shared Records (16) - Case 1 - Share Record with sharing privileges with a Team - user Team do not have share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords16_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP3", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Guid providerID = new Guid("8d5520f6-35bb-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Sharing Shared Records (16) - Case 2 - Share Record with sharing privileges with a Team - user Team have Team Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords16_TestMethod2()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("8d5520f6-35bb-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP18", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }

        [Description("Test Case - Sharing Shared Records (16) - Case 3 - Share Record with sharing privileges with a Team - user Team have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords16_TestMethod3()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("8d5520f6-35bb-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP19", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }

        [Description("Test Case - Sharing Shared Records (16) - Case 4 - Share Record with sharing privileges with a Team - user Team have PCBU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_SharingSharedRecords16_TestMethod4()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("8d5520f6-35bb-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP20", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);
        }





        [Description("Test Case - UserSharingPrevileges - Case 2 - User have Team Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_UserSharingPrevileges_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserUSPP17", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - UserSharingPrevileges - Case 3 - User have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_UserSharingPrevileges_TestMethod3()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID1 = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid providerID2 = new Guid("e3dadccc-06b8-e811-80dc-0050560502cc");
            Guid providerID3 = new Guid("ab0f80f9-06b8-e811-80dc-0050560502cc");
            Guid providerID4 = new Guid("fc14cd4e-ed5f-43bf-8688-2958041c6bc1");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID1, "provider");
            recordLevelAccessResponse.BusinessDataCollection.AddRange(SecurityDataProvider.RetrieveRecordLevelSharing(providerID2, "provider").BusinessDataCollection);

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserUSPP18", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //share the 1st provider record
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID1, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);

            //share the 2nd provider record
            recordLevelAccess = GetRecordLevelAccess(providerID2, "provider", shareToID, "Security Test User 19", "systemuser");
            createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);

            //share the 3rd provider record
            recordLevelAccess = GetRecordLevelAccess(providerID3, "provider", shareToID, "Security Test User 19", "systemuser");
            createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);

            //share the 4th provider record
            recordLevelAccess = GetRecordLevelAccess(providerID4, "provider", shareToID, "Security Test User 19", "systemuser");
            createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - UserSharingPrevileges - Case 4 - User have PCBU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_UserSharingPrevileges_TestMethod4()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID1 = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid providerID2 = new Guid("e3dadccc-06b8-e811-80dc-0050560502cc");
            Guid providerID3 = new Guid("ab0f80f9-06b8-e811-80dc-0050560502cc");
            Guid providerID4 = new Guid("fc14cd4e-ed5f-43bf-8688-2958041c6bc1");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID1, "provider");
            recordLevelAccessResponse.BusinessDataCollection.AddRange(SecurityDataProvider.RetrieveRecordLevelSharing(providerID2, "provider").BusinessDataCollection);

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            foreach (Guid id in recordLevelAccessIDs)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(new List<Guid> { id });
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserUSPP19", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //share the 1st provider record
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID1, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);

            //share the 2nd provider record
            recordLevelAccess = GetRecordLevelAccess(providerID2, "provider", shareToID, "Security Test User 19", "systemuser");
            createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);

            //share the 3rd provider record
            recordLevelAccess = GetRecordLevelAccess(providerID3, "provider", shareToID, "Security Test User 19", "systemuser");
            createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);

            //share the 4th provider record
            recordLevelAccess = GetRecordLevelAccess(providerID4, "provider", shareToID, "Security Test User 19", "systemuser");
            createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }




        [Description("Test Case - Team Sharing Previleges - Case 2 - User Team have Team Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_TeamSharingPrevileges_TestMethod2()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID1 = new Guid("e8ab3ac3-edc6-e811-80dc-0050560502cc");
            Guid providerID2 = new Guid("3bd9c2d3-8aba-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID1, "provider");

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            if (recordLevelAccessIDs != null && recordLevelAccessIDs.Count > 0)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(recordLevelAccessIDs);
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP18", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //Share the 2nd record
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID1, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);

            //Share the 2nd record
            recordLevelAccess = GetRecordLevelAccess(providerID2, "provider", shareToID, "Security Test User 19", "systemuser");
            createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);
        }

        [Description("Test Case - Team Sharing Previleges - Case 3 - User have BU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_TeamSharingPrevileges_TestMethod3()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID1 = new Guid("3bd9c2d3-8aba-e811-80dc-0050560502cc");
            Guid providerID2 = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID1, "provider");
            recordLevelAccessResponse.BusinessDataCollection.AddRange(SecurityDataProvider.RetrieveRecordLevelSharing(providerID2, "provider").BusinessDataCollection);

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            foreach (Guid id in recordLevelAccessIDs)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(new List<Guid> { id });
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP19", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //share the 1st provider record
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID1, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);

            //share the 2nd provider record
            recordLevelAccess = GetRecordLevelAccess(providerID2, "provider", shareToID, "Security Test User 19", "systemuser");
            createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);

        }

        [Description("Test Case - Team Sharing Previleges - Case 4 - User have PCBU Share security profile")]
        [TestMethod]
        public void PhoenixSecurity_TeamSharingPrevileges_TestMethod4()
        {
            Assert.Inconclusive("Sharing security profiles no longer exist in the system, making this test obsolete. If a sharing security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //the the id of the provider and the id of the user to share the provider with
            Guid providerID1 = new Guid("3bd9c2d3-8aba-e811-80dc-0050560502cc");
            Guid providerID2 = new Guid("8d5520f6-35bb-e811-80dc-0050560502cc");
            Guid providerID3 = new Guid("f5a33708-06b8-e811-80dc-0050560502cc");
            Guid shareToID = new Guid("D3396424-19C7-E811-9C3D-F8B156AF4F99");
            CareDirector.Sdk.ServiceResponse.RetrieveMultipleResponse<CareDirector.Sdk.SystemEntities.RecordLevelAccess> recordLevelAccessResponse = SecurityDataProvider.RetrieveRecordLevelSharing(providerID1, "provider");
            recordLevelAccessResponse.BusinessDataCollection.AddRange(SecurityDataProvider.RetrieveRecordLevelSharing(providerID2, "provider").BusinessDataCollection);

            //get all shares to the user SecurityTestUser19
            List<Guid> recordLevelAccessIDs = (from ent in recordLevelAccessResponse.BusinessDataCollection
                                               where ent.ShareToId == shareToID
                                               && ent.Id.HasValue
                                               select ent.Id.Value).ToList();

            //if any share exist remove it!
            foreach (Guid id in recordLevelAccessIDs)
            {
                CareDirector.Sdk.ServiceResponse.DeleteResponse deleteResponse = SecurityDataProvider.RevokeRecordLevelAccess(new List<Guid> { id });
                
                Assert.IsFalse(deleteResponse.HasErrors);
            }

            //Login as the test user
            authenticateRequest = GetAuthenticationRequest("SecurityTestUserTSPP20", "Passw0rd_!");
            authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            //share the 1st provider record
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerID1, "provider", shareToID, "Security Test User 19", "systemuser");
            CareDirector.Sdk.ServiceResponse.CreateResponse createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);

            //share the 2nd provider record
            recordLevelAccess = GetRecordLevelAccess(providerID2, "provider", shareToID, "Security Test User 19", "systemuser");
            createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsTrue(createRecordLevelAccessResponse.Success);
            Assert.IsFalse(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsFalse(createRecordLevelAccessResponse.HasErrors);
            Assert.IsNull(createRecordLevelAccessResponse.Error);

            //share the 3rd provider record
            recordLevelAccess = GetRecordLevelAccess(providerID3, "provider", shareToID, "Security Test User 19", "systemuser");
            createRecordLevelAccessResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);

            //Asserts
            //Assert.IsFalse(createRecordLevelAccessResponse.Success);
            Assert.IsTrue(createRecordLevelAccessResponse.UnauthorisedAccess);
            Assert.IsTrue(createRecordLevelAccessResponse.HasErrors);

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

        private CareDirector.Sdk.SystemEntities.BusinessData GetBusinessDataServiceBusinessData(string BusinessObjectName, string PrimaryKeyName)
        {
            CareDirector.Sdk.SystemEntities.BusinessData businessData = new CareDirector.Sdk.SystemEntities.BusinessData()
            {
                BusinessObjectName = BusinessObjectName,
                MultiSelectBusinessObjectFields = new CareDirector.Sdk.SystemEntities.MultiSelectBusinessObjectDataDictionary(),
                MultiSelectOptionSetFields = new CareDirector.Sdk.SystemEntities.MultiSelectOptionSetDataDictionary(),
                PrimaryKeyName = PrimaryKeyName,
            };

            return businessData;
        }

        private Provider GetProviderToCreate(Guid ProviderID, string Name, Guid OwnerId, Guid OwningBusinessUnitId)
        {
            return new Provider()
            {
                ProviderId = ProviderID,
                CreatedBy = new Guid("A2327B85-99F3-E611-80D4-0050560502CC"),
                CreatedOn = DateTime.Now,
                ModifiedBy = new Guid("A2327B85-99F3-E611-80D4-0050560502CC"),
                ModifiedOn = DateTime.Now,
                OwnerId = OwnerId,
                Inactive = false,
                Name = Name,
                OwningBusinessUnitId = OwningBusinessUnitId,
                Description = "Do not delete. Used for security testing.",
                ProviderTypeId = 6
            };
        }

        private CareDirector.Sdk.SystemEntities.RecordLevelAccess GetRecordLevelAccess(Guid RecordID, string RecordTableName, Guid ShareToId, string ShareToIdName, string ShareToIdTableName, bool CanView = true, bool CanEdit = false, bool CanDelete = false, bool CanShare = false)
        {
            return new CareDirector.Sdk.SystemEntities.RecordLevelAccess
            {
                RecordId = RecordID,
                RecordIdTableName = RecordTableName,
                CanView = CanView,
                CanEdit = CanEdit,
                CanDelete = CanDelete,
                CanShare = CanShare,
                ShareToId = ShareToId,
                ShareToIdName = ShareToIdName,
                ShareToIdTableName = ShareToIdTableName
            };
        }

    }
}
