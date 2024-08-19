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
    public class Security_UnitTest
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


        


        [Description("Call to the 'RetrieveDataByView' method for the 'BusinessDataServiceInternal.svc' service. User do not have Security Privileges to the Providers ")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod1()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser1", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);
            
            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmr = GetRetrieveMultipleRequest("provider", new Guid("2d153db4-239a-e811-9be8-989096c9be3d"));
            var bdcResponse = DataProvider.RetrieveDataByView(rmr);

            Assert.IsTrue(bdcResponse.HasErrors);
            Assert.IsTrue(bdcResponse.Exception != null);
            Assert.IsTrue(bdcResponse.BusinessDataCollection == null || bdcResponse.BusinessDataCollection.Count() == 0);
        }

        [Description("Call to the 'RetrieveDataByView' method for the 'BusinessDataServiceInternal.svc' service. User have Security Privileges to access Provider records")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod2()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticateRequest = GetAuthenticationRequest("SecurityTestUser2", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticateRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            CareDirector.Sdk.ServiceRequest.RetrieveMultipleRequest rmRequest = GetRetrieveMultipleRequest("provider", new Guid("2d153db4-239a-e811-9be8-989096c9be3d"));
            var bdcResponse = DataProvider.RetrieveDataByView(rmRequest);

            //Validate that the platform returns data. The user "SecurityTestUser2" have security permissions to access provider records
            Assert.IsFalse(bdcResponse.HasErrors);
            Assert.IsTrue(bdcResponse.Exception == null);
            Assert.IsTrue(bdcResponse.BusinessDataCollection != null || bdcResponse.BusinessDataCollection.Count() > 0);
            Assert.AreEqual("Security Test Provider 1", (string)bdcResponse.BusinessDataCollection.ElementAt(6).FieldCollection["name"]);
        }




        [Description("Call to the 'Update' method for the 'BusinessDataServiceInternal.svc' service. User do not have Security Privileges")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod3()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser1", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            CareDirector.Sdk.SystemEntities.BusinessData providerBusinessData = GetBusinessDataServiceBusinessData("provider", "providerid");
            providerBusinessData.FieldCollection.Add("providerid", new Guid("09963924-96b6-e811-80dc-0050560502cc"));
            providerBusinessData.FieldCollection.Add("notes", Guid.NewGuid().ToString());

            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.UpdateResponse updateResponse = DataProvider.Update(providerBusinessData);

            //Validate that the platform do not update the record. The user do not have security profiles
            Assert.IsTrue(updateResponse.HasErrors);
            Assert.IsTrue(updateResponse.UnauthorisedAccess);
        }

        [Description("Call to the 'Update' method for the 'BusinessDataServiceInternal.svc' service. User have Security Privileges, but no Edit Privilege")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod4()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserUSPP2", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            CareDirector.Sdk.SystemEntities.BusinessData providerBusinessData = GetBusinessDataServiceBusinessData("provider", "providerid");
            providerBusinessData.FieldCollection.Add("providerid", new Guid("f5a33708-06b8-e811-80dc-0050560502cc"));
            providerBusinessData.FieldCollection.Add("notes", Guid.NewGuid().ToString());

            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.UpdateResponse updateResponse = DataProvider.Update(providerBusinessData);

            //Validate that the platform do not update the record. The user do not have security profiles
            Assert.IsTrue(updateResponse.HasErrors);
            Assert.IsTrue(updateResponse.UnauthorisedAccess);
        }

        [Description("Call to the 'Update' method for the 'BusinessDataServiceInternal.svc' service. User have Security Privileges, User has Edit Privilege")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod5()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserUSPP7", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            CareDirector.Sdk.SystemEntities.BusinessData providerBusinessData = GetBusinessDataServiceBusinessData("provider", "providerid");
            providerBusinessData.FieldCollection.Add("providerid", new Guid("f5a33708-06b8-e811-80dc-0050560502cc"));
            providerBusinessData.FieldCollection.Add("notes", Guid.NewGuid().ToString());

            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.UpdateResponse updateResponse = DataProvider.Update(providerBusinessData);

            //Validate that the platform do not update the record. The user do not have security profiles
            Assert.IsFalse(updateResponse.HasErrors);
            Assert.IsFalse(updateResponse.UnauthorisedAccess);
            Assert.IsNull(updateResponse.Exception);
        }




        [Description("Call to the 'Create' method for the 'BusinessDataServiceInternal.svc' service. User do not have Security Privileges")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod6()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser1", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            CareDirector.Sdk.SystemEntities.BusinessData providerBusinessData = GetBusinessDataServiceBusinessData("provider", "providerid");
            providerBusinessData.FieldCollection.Add("name", "SecurityCreateTest-" + DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            providerBusinessData.FieldCollection.Add("providertypeid", 6 );
            providerBusinessData.FieldCollection.Add("ownerid", new Guid("226e245c-04b8-e811-80dc-0050560502cc") );
            providerBusinessData.FieldCollection.Add("notes", Guid.NewGuid().ToString() );


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.CreateResponse businessDataCollection = DataProvider.Create(providerBusinessData);

            //Validate that the platform do not update the record. The user do not have security profiles
            Assert.IsNull(businessDataCollection.Id);
            Assert.IsTrue(businessDataCollection.UnauthorisedAccess);
        }

        [Description("Call to the 'Create' method for the 'BusinessDataServiceInternal.svc' service. User have Security Privileges. User do not have Create Privilege")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod7()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser2", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            CareDirector.Sdk.SystemEntities.BusinessData providerBusinessData = GetBusinessDataServiceBusinessData("provider", "providerid");
            providerBusinessData.FieldCollection.Add("name", "SecurityCreateTest-" + DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            providerBusinessData.FieldCollection.Add("providertypeid", 6 );
            providerBusinessData.FieldCollection.Add("ownerid", new Guid("722f67dd-c3b5-e811-80dc-0050560502cc"));
            providerBusinessData.FieldCollection.Add("notes", Guid.NewGuid().ToString());

            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.CreateResponse businessDataCollection = DataProvider.Create(providerBusinessData);

            //Validate that the platform do not update the record. The user do not have security profiles
            Assert.IsNull(businessDataCollection.Id);
            Assert.IsTrue(businessDataCollection.UnauthorisedAccess);
        }

        [Description("Call to the 'Create' method for the 'BusinessDataServiceInternal.svc' service. User have Security Privileges. User have Create Privilege")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod8()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserPlatformAPI1", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            CareDirector.Sdk.SystemEntities.BusinessData providerBusinessData = GetBusinessDataServiceBusinessData("provider", "providerid");
            providerBusinessData.FieldCollection.Add("name", "Security Test - Create - " + DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            providerBusinessData.FieldCollection.Add("providertypeid", 6);
            providerBusinessData.FieldCollection.Add("ownerid", new Guid("eb9a9075-8cc1-e811-80dc-0050560502cc"));
            providerBusinessData.FieldCollection.Add("notes", Guid.NewGuid().ToString());


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.CreateResponse businessDataCollection = DataProvider.Create(providerBusinessData);

            //Validate that the platform do not update the record. The user do not have security profiles
            Assert.IsNotNull(businessDataCollection.Id);
            Assert.IsTrue(businessDataCollection.Id.HasValue);
            Assert.IsFalse(businessDataCollection.UnauthorisedAccess);
        }




        //[Description("Call to the 'FindDuplicatesAndCreate' method for the 'BusinessDataServiceInternal.svc' service. User do not have Security Privileges")]
        //[TestMethod]
        //public void PhoenixSecurity_TestMethod9()
        //{
        //    CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser1", "Passw0rd_!");
        //    CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
        //    SetServiceConnectionDataFromAuthenticationResponse(authResponse);

        //    Assert.IsTrue(authResponse.IsAuthenticated);
        //    Assert.IsFalse(authResponse.HasErrors);
        //    Assert.IsNull(authResponse.Exception);

        //    CareDirector.Sdk.SystemEntities.BusinessData providerBusinessData = GetBusinessDataServiceBusinessData("provider", "providerid");
        //    providerBusinessData.FieldCollection.Add("name", new CareDirector.Sdk.SystemEntities.FieldData() { BusinessObjectFieldName = "name", Changed = true, DataType = CareDirector.Sdk.Enums.DataType.Text, FieldName = "name", FieldType = CareWorks.Foundation.Enums.BusinessObjectFieldType.Unknown, IsUtcDate = false, Value = "SecurityCreateTest-" + DateTime.Now.ToString("yyyyMMddhhmmssfff") });
        //    providerBusinessData.FieldCollection.Add("providertypeid", new CareDirector.Sdk.SystemEntities.FieldData() { BusinessObjectFieldName = "providertypeid", Changed = true, DataType = CareDirector.Sdk.Enums.DataType.Integer, FieldName = "providertypeid", FieldType = CareWorks.Foundation.Enums.BusinessObjectFieldType.Unknown, IsUtcDate = false, Value = 6 });
        //    providerBusinessData.FieldCollection.Add("ownerid", new CareDirector.Sdk.SystemEntities.FieldData() { BusinessObjectFieldName = "ownerid", Changed = true, DataType = CareDirector.Sdk.Enums.DataType.UniqueIdentifier, FieldName = "ownerid", FieldType = CareWorks.Foundation.Enums.BusinessObjectFieldType.Unknown, IsUtcDate = false, Value = new Guid("226e245c-04b8-e811-80dc-0050560502cc") });
        //    providerBusinessData.FieldCollection.Add("notes", new CareDirector.Sdk.SystemEntities.FieldData() { BusinessObjectFieldName = "notes", Changed = true, DataType = CareDirector.Sdk.Enums.DataType.Text, FieldName = "notes", FieldType = CareWorks.Foundation.Enums.BusinessObjectFieldType.Unknown, IsUtcDate = false, OldValue = "f405414f-efb1-43d3-b13f-caa62ce89a69", Value = Guid.NewGuid().ToString() });

        //    //Perform the call to the "Update" method
        //    CareDirector.Sdk.ServiceResponse.CreateResponse businessDataCollection = DataProvider.FindDuplicatesAndCreate(providerBusinessData);

        //    //Validate that the platform do not update the record. The user do not have security profiles
        //    Assert.IsNull(businessDataCollection.Id);
        //    Assert.IsTrue(businessDataCollection.UnauthorisedAccess);
        //}

        //[Description("Call to the 'FindDuplicatesAndCreate' method for the 'BusinessDataServiceInternal.svc' service. User have Security Privileges. User do not have Create Privilege")]
        //[TestMethod]
        //public void PhoenixSecurity_TestMethod10()
        //{
        //    CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser2", "Passw0rd_!");
        //    CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
        //    SetServiceConnectionDataFromAuthenticationResponse(authResponse);

        //    Assert.IsTrue(authResponse.IsAuthenticated);
        //    Assert.IsFalse(authResponse.HasErrors);
        //    Assert.IsNull(authResponse.Exception);

        //    CareDirector.Sdk.SystemEntities.BusinessData providerBusinessData = GetBusinessDataServiceBusinessData("provider", "providerid");
        //    providerBusinessData.FieldCollection.Add("name", new CareDirector.Sdk.SystemEntities.FieldData() { BusinessObjectFieldName = "name", Changed = true, DataType = CareDirector.Sdk.Enums.DataType.Text, FieldName = "name", FieldType = CareWorks.Foundation.Enums.BusinessObjectFieldType.Unknown, IsUtcDate = false, Value = "SecurityCreateTest-" + DateTime.Now.ToString("yyyyMMddhhmmssfff") });
        //    providerBusinessData.FieldCollection.Add("providertypeid", new CareDirector.Sdk.SystemEntities.FieldData() { BusinessObjectFieldName = "providertypeid", Changed = true, DataType = CareDirector.Sdk.Enums.DataType.Integer, FieldName = "providertypeid", FieldType = CareWorks.Foundation.Enums.BusinessObjectFieldType.Unknown, IsUtcDate = false, Value = 6 });
        //    providerBusinessData.FieldCollection.Add("ownerid", new CareDirector.Sdk.SystemEntities.FieldData() { BusinessObjectFieldName = "ownerid", Changed = true, DataType = CareDirector.Sdk.Enums.DataType.UniqueIdentifier, FieldName = "ownerid", FieldType = CareWorks.Foundation.Enums.BusinessObjectFieldType.Unknown, IsUtcDate = false, Value = new Guid("722f67dd-c3b5-e811-80dc-0050560502cc") });
        //    providerBusinessData.FieldCollection.Add("notes", new CareDirector.Sdk.SystemEntities.FieldData() { BusinessObjectFieldName = "notes", Changed = true, DataType = CareDirector.Sdk.Enums.DataType.Text, FieldName = "notes", FieldType = CareWorks.Foundation.Enums.BusinessObjectFieldType.Unknown, IsUtcDate = false, OldValue = "f405414f-efb1-43d3-b13f-caa62ce89a69", Value = Guid.NewGuid().ToString() });


        //    //Perform the call to the "Update" method
        //    CareDirector.Sdk.ServiceResponse.CreateResponse businessDataCollection = DataProvider.FindDuplicatesAndCreate(providerBusinessData);

        //    //Validate that the platform do not update the record. The user do not have security profiles
        //    Assert.IsNull(businessDataCollection.Id);
        //    Assert.IsTrue(businessDataCollection.UnauthorisedAccess);
        //}

        //[Description("Call to the 'FindDuplicatesAndCreate' method for the 'BusinessDataServiceInternal.svc' service. User have Security Privileges. User have Create Privilege")]
        //[TestMethod]
        //public void PhoenixSecurity_TestMethod11()
        //{
        //    CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserPlatformAPI1", "Passw0rd_!");
        //    CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
        //    SetServiceConnectionDataFromAuthenticationResponse(authResponse);

        //    Assert.IsTrue(authResponse.IsAuthenticated);
        //    Assert.IsFalse(authResponse.HasErrors);
        //    Assert.IsNull(authResponse.Exception);

        //    CareDirector.Sdk.SystemEntities.BusinessData providerBusinessData = GetBusinessDataServiceBusinessData("provider", "providerid");
        //    providerBusinessData.FieldCollection.Add("name", new CareDirector.Sdk.SystemEntities.FieldData() { BusinessObjectFieldName = "name", Changed = true, DataType = CareDirector.Sdk.Enums.DataType.Text, FieldName = "name", FieldType = CareWorks.Foundation.Enums.BusinessObjectFieldType.Unknown, IsUtcDate = false, Value = "Security Test - Create - " + DateTime.Now.ToString("yyyyMMddhhmmssfff") });
        //    providerBusinessData.FieldCollection.Add("providertypeid", new CareDirector.Sdk.SystemEntities.FieldData() { BusinessObjectFieldName = "providertypeid", Changed = true, DataType = CareDirector.Sdk.Enums.DataType.Integer, FieldName = "providertypeid", FieldType = CareWorks.Foundation.Enums.BusinessObjectFieldType.Unknown, IsUtcDate = false, Value = 6 });
        //    providerBusinessData.FieldCollection.Add("ownerid", new CareDirector.Sdk.SystemEntities.FieldData() { BusinessObjectFieldName = "ownerid", Changed = true, DataType = CareDirector.Sdk.Enums.DataType.UniqueIdentifier, FieldName = "ownerid", FieldType = CareWorks.Foundation.Enums.BusinessObjectFieldType.Unknown, IsUtcDate = false, Value = new Guid("eb9a9075-8cc1-e811-80dc-0050560502cc") });
        //    providerBusinessData.FieldCollection.Add("notes", new CareDirector.Sdk.SystemEntities.FieldData() { BusinessObjectFieldName = "notes", Changed = true, DataType = CareDirector.Sdk.Enums.DataType.Text, FieldName = "notes", FieldType = CareWorks.Foundation.Enums.BusinessObjectFieldType.Unknown, IsUtcDate = false, OldValue = "f405414f-efb1-43d3-b13f-caa62ce89a69", Value = Guid.NewGuid().ToString() });

        //    //Perform the call to the "Update" method
        //    CareDirector.Sdk.ServiceResponse.CreateResponse businessDataCollection = DataProvider.FindDuplicatesAndCreate(providerBusinessData);

        //    //Validate that the platform do not update the record. The user do not have security profiles
        //    Assert.IsNotNull(businessDataCollection.Id);
        //    Assert.IsTrue(businessDataCollection.Id.HasValue);
        //    Assert.IsFalse(businessDataCollection.UnauthorisedAccess);
        //}




        [Description("Call to the 'Delete' method for the 'BusinessDataServiceInternal.svc' service. User do not have Security Privileges")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod12()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser1", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            //Perform the call to the "Delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("d5ecee40-93c1-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Call to the 'Delete' method for the 'BusinessDataServiceInternal.svc' service. User Team have Security Privileges. User Team do not have Delete Privilege")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod13()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserPlatformAPI1", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("d5ecee40-93c1-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Call to the 'Delete' method for the 'BusinessDataServiceInternal.svc' service. User Team have Security Privileges. User Team have Delete Privilege")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod14()
        {
            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");

            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserPlatformAPI2", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                string providerName = "Security Test Provider Platform API 2 - " + DateTime.Now.ToString("yyyyMMddhhmmssfff");
                Provider p = GetProviderToCreate(providerid, providerName, new Guid("C36ADCFD-91C1-E811-80DC-0050560502CC"), new Guid("CCC6053B-8CC1-E811-80DC-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }


            //Perform the call to the "Delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", providerid);

            //Validate that the record is deleted
            
            Assert.IsFalse(response.UnauthorisedAccess);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(providerid, response.Id);
        }


        [Description("Decision matrix document - Sharing Provider With User (1) - Case 8 - Record shared with user (View permission only). User do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithUser1_Case8()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser8", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With User (1) - Case 11 - Record shared with user (View permission only). User have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithUser1_Case11()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With User (2) - Case 9 - Record shared with user (View and Edit permission). User do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithUser2_Case9()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser8", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("09963924-96b6-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With User (2) - Case 12 - Record shared with user (View and Edit permission). User have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithUser2_Case12()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("09963924-96b6-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With User (3) - Case 8 - Record shared with user (Delete permission). User do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithUser3_Case8()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser8", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("50fc1b5a-3fb7-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With User (3) - Case 11 - Record shared with user (Delete permission). User have delete sec. profile - User should able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithUser3_Case11()
        {
            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");

            //Create the provider
            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                Provider p = GetProviderToCreate(providerid, "Security Test Provider 4 - Delete", new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), new Guid("549163DD-2C5A-E511-80C8-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }

            //Login as admin
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest adminAuthenticationRequest = GetAuthenticationRequest("careworksltd\\jbrazeta", "Voinas__");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse adminAuthResponse = SecurityDataProvider.Authenticate(adminAuthenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(adminAuthResponse);

            //share the record with delete privileges
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerid, "provider", new Guid("3C88ED09-91B6-E811-80DC-0050560502CC"), "Security Test User 11", "systemuser", true, false, true);
            CareDirector.Sdk.ServiceResponse.CreateResponse shareResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);
            Assert.IsFalse(shareResponse.HasErrors);
            Assert.IsNull(shareResponse.Exception);




            //Reset the connection and Login as the SecurityTestUser11
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);



            //Perform the call to the "delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", providerid);



            //Validate that the record is deleted
            Assert.IsFalse(response.UnauthorisedAccess);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(providerid, response.Id);
            Assert.IsTrue(string.IsNullOrEmpty(response.Error));
        }

        [Description("Decision matrix document - Sharing Provider With User (4) - Case 7 - Record shared with user (View permission only). User Team do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithUser4_Case7()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP6", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With User (4) - Case 11 - Record shared with user (View permission only). User Team have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithUser4_Case11()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("5ca31eb3-c7b5-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With User (5) - Case 7 - Record shared with user (View and Edit permission). User Team do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithUser5_Case7()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP6", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("09963924-96b6-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With User (5) - Case 11 - Record shared with user (View and Edit permission). User Team have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithUser5_Case11()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP16", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("09963924-96b6-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With User (6) - Case 7 - Record shared with user (Delete permission). User Team do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithUser6_Case7()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP6", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("50fc1b5a-3fb7-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With User (6) - Case 11 - Record shared with user (Delete permission). User Team have delete sec. profile - User should able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithUser6_Case11()
        {
            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");

            //Create the provider
            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                Provider p = GetProviderToCreate(providerid, "Security Test Provider 4 - Delete", new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), new Guid("549163DD-2C5A-E511-80C8-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }

            //Login as admin
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest adminAuthenticationRequest = GetAuthenticationRequest("careworksltd\\jbrazeta", "Voinas__");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse adminAuthResponse = SecurityDataProvider.Authenticate(adminAuthenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(adminAuthResponse);

            //share the record with delete privileges
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerid, "provider", new Guid("22120E67-97BA-E811-80DC-0050560502CC"), "Security Test User TSPP 11", "systemuser", true, false, true);
            CareDirector.Sdk.ServiceResponse.CreateResponse shareResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);
            Assert.IsFalse(shareResponse.HasErrors);
            Assert.IsNull(shareResponse.Exception);




            //Reset the connection and Login as the SecurityTestUser11
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);



            //Perform the call to the "delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", providerid);



            //Validate that the record is deleted
            Assert.IsFalse(response.UnauthorisedAccess);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(providerid, response.Id);
            Assert.IsTrue(string.IsNullOrEmpty(response.Error));
        }



        [Description("Decision matrix document - Sharing Provider With Team (1) - Case 7 - Record shared with Team (View permission only). User do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam1_Case7()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser7", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("f5a33708-06b8-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With Team (1) - Case 11 - Record shared with Team (View permission only). User have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam1_Case11()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("f5a33708-06b8-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With Team (2) - Case 7 - Record shared with Team (View adn Edit permission). User do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam2_Case7()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser7", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("e3dadccc-06b8-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With Team (2) - Case 11 - Record shared with Team (View adn Edit permission). User have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam2_Case11()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("e3dadccc-06b8-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With Team (3) - Case 7 - Record shared with Team (Delete permission). User do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam3_Case7()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser7", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("ab0f80f9-06b8-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With Team (3) - Case 11 - Record shared with Team (Delete permission). User have delete sec. profile (Team Delete) - User should be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam3_Case11()
        {
            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");

            //Create the provider
            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                Provider p = GetProviderToCreate(providerid, "Security Test Provider 4 - Delete", new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), new Guid("549163DD-2C5A-E511-80C8-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }

            //Login as admin
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest adminAuthenticationRequest = GetAuthenticationRequest("careworksltd\\jbrazeta", "Voinas__");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse adminAuthResponse = SecurityDataProvider.Authenticate(adminAuthenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(adminAuthResponse);

            //share the record with the team with delete privileges
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerid, "provider", new Guid("722F67DD-C3B5-E811-80DC-0050560502CC"), "Security Test Team T1", "team", true, false, true);
            CareDirector.Sdk.ServiceResponse.CreateResponse shareResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);
            Assert.IsTrue(shareResponse.HasErrors);
            Assert.IsNull(shareResponse.Exception);


            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", providerid);

            //Validate that the record is deleted
            Assert.IsFalse(response.UnauthorisedAccess);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(providerid, response.Id);
            Assert.IsTrue(string.IsNullOrEmpty(response.Error));
        }

        [Description("Decision matrix document - Sharing Provider With Team (3) - Case 12 - Record shared with Team (Delete permission). User have delete sec. profile (BU Delete) - User should be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam3_Case12()
        {
            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");

            //Create the provider
            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                Provider p = GetProviderToCreate(providerid, "Security Test Provider 4 - Delete", new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), new Guid("549163DD-2C5A-E511-80C8-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }

            //Login as admin
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest adminAuthenticationRequest = GetAuthenticationRequest("careworksltd\\jbrazeta", "Voinas__");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse adminAuthResponse = SecurityDataProvider.Authenticate(adminAuthenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(adminAuthResponse);

            //share the record with the team with delete privileges
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerid, "provider", new Guid("722F67DD-C3B5-E811-80DC-0050560502CC"), "Security Test Team T1", "team", true, false, true);
            CareDirector.Sdk.ServiceResponse.CreateResponse shareResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);
            Assert.IsFalse(shareResponse.HasErrors);
            Assert.IsNull(shareResponse.Exception);


            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser12", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", providerid);

            //Validate that the record is deleted
            Assert.IsFalse(response.UnauthorisedAccess);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(providerid, response.Id);
            Assert.IsTrue(string.IsNullOrEmpty(response.Error));
        }

        [Description("Decision matrix document - Sharing Provider With Team (3) - Case 12 - Record shared with Team (Delete permission). User have delete sec. profile (PCBU Delete) - User should be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam3_Case13()
        {
            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");

            //Create the provider
            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                Provider p = GetProviderToCreate(providerid, "Security Test Provider 4 - Delete", new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), new Guid("549163DD-2C5A-E511-80C8-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }

            //Login as admin
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest adminAuthenticationRequest = GetAuthenticationRequest("careworksltd\\jbrazeta", "Voinas__");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse adminAuthResponse = SecurityDataProvider.Authenticate(adminAuthenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(adminAuthResponse);

            //share the record with the team with delete privileges
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerid, "provider", new Guid("722F67DD-C3B5-E811-80DC-0050560502CC"), "Security Test Team T1", "team", true, false, true);
            CareDirector.Sdk.ServiceResponse.CreateResponse shareResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);
            Assert.IsFalse(shareResponse.HasErrors);
            Assert.IsNull(shareResponse.Exception);


            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser13", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", providerid);

            //Validate that the record is deleted
            Assert.IsFalse(response.UnauthorisedAccess);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(providerid, response.Id);
            Assert.IsTrue(string.IsNullOrEmpty(response.Error));
        }


        [Description("Decision matrix document - Sharing Provider With Team (4) - Case 7 - Record shared with Team (View permission only). Team do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam4_Case7()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP6", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("f5a33708-06b8-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With Team (4) - Case 11 - Record shared with Team (View permission only). Team have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam4_Case11()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("f5a33708-06b8-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With Team (5) - Case 7 - Record shared with Team (View adn Edit permission). Team do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam5_Case7()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP6", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("e3dadccc-06b8-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With Team (5) - Case 11 - Record shared with Team (View adn Edit permission). Team have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam5_Case11()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("e3dadccc-06b8-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With Team (6) - Case 7 - Record shared with Team (Delete permission). Team do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam6_Case7()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP6", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("ab0f80f9-06b8-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Sharing Provider With Team (6) - Case 11 - Record shared with Team (Delete permission). Team have delete sec. profile (Team Delete) - User should be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_SharingProviderWithTeam6_Case11()
        {
            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");

            //Create the provider
            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                Provider p = GetProviderToCreate(providerid, "Security Test Provider 4 - Delete", new Guid("559163dd-2c5a-e511-80c8-0050560502cc"), new Guid("549163DD-2C5A-E511-80C8-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }

            //Login as admin
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest adminAuthenticationRequest = GetAuthenticationRequest("careworksltd\\jbrazeta", "Voinas__");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse adminAuthResponse = SecurityDataProvider.Authenticate(adminAuthenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(adminAuthResponse);

            //share the record with the team with delete privileges
            CareDirector.Sdk.SystemEntities.RecordLevelAccess recordLevelAccess = GetRecordLevelAccess(providerid, "provider", new Guid("56DB7B9D-6B9C-4DBD-9004-C3306BDB18B1"), "Security Test Team TSPP 10", "team", true, false, true);
            CareDirector.Sdk.ServiceResponse.CreateResponse shareResponse = SecurityDataProvider.GrantRecordLevelAccess(recordLevelAccess);
            Assert.IsFalse(shareResponse.HasErrors);
            Assert.IsNull(shareResponse.Exception);


            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", providerid);

            //Validate that the record is deleted
            Assert.IsFalse(response.UnauthorisedAccess);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(providerid, response.Id);
            Assert.IsTrue(string.IsNullOrEmpty(response.Error));
        }



        [Description("Decision matrix document - User Sec. Profile Previleges - Case 9 - User Team ownes record. User do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_UserSecProfilePrevileges_Case9()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserUSPP7", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("f5a33708-06b8-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - User Sec. Profile Previleges - Case 13 - User Team ownes record. User have delete sec. profile (Team Delete) - User should NOT be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_UserSecProfilePrevileges_Case13()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserUSPP11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("f5a33708-06b8-e811-80dc-0050560502cc"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - User Sec. Profile Previleges - Case 14 - User Team ownes record. User have delete sec. profile (BU Delete) - User should be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_UserSecProfilePrevileges_Case14()
        {
            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");

            //Create the provider
            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                Provider p = GetProviderToCreate(providerid, "Security Test Provider 4 - Delete", new Guid("226E245C-04B8-E811-80DC-0050560502CC"), new Guid("88EB03EE-03B8-E811-80DC-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }

            //Login
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserUSPP12", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", providerid);

            //Validate that the record is deleted
            Assert.IsFalse(response.UnauthorisedAccess);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(providerid, response.Id);
            Assert.IsTrue(string.IsNullOrEmpty(response.Error));
        }

        [Description("Decision matrix document - User Sec. Profile Previleges - Case 14 - User Team ownes record. User have delete sec. profile (PCBU Delete) - User should be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_UserSecProfilePrevileges_Case15()
        {
            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");

            //Create the provider
            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                Provider p = GetProviderToCreate(providerid, "Security Test Provider 4 - Delete", new Guid("226E245C-04B8-E811-80DC-0050560502CC"), new Guid("88EB03EE-03B8-E811-80DC-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }

            //Login
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserUSPP13", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", providerid);

            //Validate that the record is deleted
            Assert.IsFalse(response.UnauthorisedAccess);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(providerid, response.Id);
            Assert.IsTrue(string.IsNullOrEmpty(response.Error));
        }


        [Description("Decision matrix document - Team Sec. Profile Previleges - Case 7 - User Team ownes record. Team do not have delete sec. profile - User should not be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_TeamSecProfilePrevileges_Case7()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP6", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Delete" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", new Guid("c39b5f3e-15a6-48f4-be77-0d8e154c73d8"));

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.UnauthorisedAccess);
        }

        [Description("Decision matrix document - Team Sec. Profile Previleges - Case 12 - User Team ownes record. Team have delete sec. profile (Team Delete) - User should be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_TeamSecProfilePrevileges_Case12()
        {
            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");

            //Create the provider
            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                Provider p = GetProviderToCreate(providerid, "Security Test Provider 4 - Delete", new Guid("56DB7B9D-6B9C-4DBD-9004-C3306BDB18B1"), new Guid("1B05F613-89BA-E811-80DC-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }

            //Login
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP11", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", providerid);

            //Validate that the record is deleted
            Assert.IsFalse(response.UnauthorisedAccess);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(providerid, response.Id);
            Assert.IsTrue(string.IsNullOrEmpty(response.Error));
        }

        [Description("Decision matrix document - Team Sec. Profile Previleges - Case 13 - User Team ownes record. Team have delete sec. profile (Team Delete) - User should be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_TeamSecProfilePrevileges_Case13()
        {
            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");

            //Create the provider
            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                Provider p = GetProviderToCreate(providerid, "Security Test Provider 4 - Delete", new Guid("093053A2-CEA3-4269-B49D-527F0CEC4072"), new Guid("1B05F613-89BA-E811-80DC-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }

            //Login
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP12", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", providerid);

            //Validate that the record is deleted
            Assert.IsFalse(response.UnauthorisedAccess);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(providerid, response.Id);
            Assert.IsTrue(string.IsNullOrEmpty(response.Error));
        }

        [Description("Decision matrix document - Team Sec. Profile Previleges - Case 14 - User Team ownes record. Team have delete sec. profile (Team Delete) - User should be able to delete the record")]
        [TestMethod]
        public void PhoenixSecurity_TeamSecProfilePrevileges_Case14()
        {
            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");

            //Create the provider
            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                Provider p = GetProviderToCreate(providerid, "Security Test Provider 4 - Delete", new Guid("D9AD63CA-01E1-4ADA-91F3-DCF7DD8C46C2"), new Guid("1B05F613-89BA-E811-80DC-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }

            //Login
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserTSPP13", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteResponse response = DataProvider.Delete("provider", providerid);

            //Validate that the record is deleted
            Assert.IsFalse(response.UnauthorisedAccess);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(providerid, response.Id);
            Assert.IsTrue(string.IsNullOrEmpty(response.Error));
        }






        [Description("Call to the 'DeleteMultiple' method for the 'BusinessDataServiceInternal.svc' service. User do not have Security Privileges")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod15()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUser1", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            List<Guid> idsToDelete = new List<Guid> { new Guid("d5ecee40-93c1-e811-80dc-0050560502cc") };
            CareDirector.Sdk.ServiceRequest.DeleteMultipleRequestCollection request = new CareDirector.Sdk.ServiceRequest.DeleteMultipleRequestCollection();
            request.Add("provider", idsToDelete);

            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteMultipleResponse response = DataProvider.DeleteMultiple(request);

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.DeleteResponses[0].UnauthorisedAccess);
        }

        [Description("Call to the 'DeleteMultiple' method for the 'BusinessDataServiceInternal.svc' service. User have Security Privileges. User do not have Delete Privilege")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod16()
        {
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserPlatformAPI1", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);


            List<Guid> idsToDelete = new List<Guid> { new Guid("d5ecee40-93c1-e811-80dc-0050560502cc") };
            CareDirector.Sdk.ServiceRequest.DeleteMultipleRequestCollection request = new CareDirector.Sdk.ServiceRequest.DeleteMultipleRequestCollection();
            request.Add("provider", idsToDelete);


            //Perform the call to the "Update" method
            CareDirector.Sdk.ServiceResponse.DeleteMultipleResponse response = DataProvider.DeleteMultiple(request);

            //Validate that the platform do not update the record. The user do not have security profiles
            
            Assert.IsTrue(response.DeleteResponses[0].UnauthorisedAccess);
        }

        [Description("Call to the 'DeleteMultiple' method for the 'BusinessDataServiceInternal.svc' service. User have Security Privileges. User have Create Privilege")]
        [TestMethod]
        public void PhoenixSecurity_TestMethod17()
        {

            Assert.Inconclusive("Delete security profiles no longer exist in the system, making this test obsolete. If a delete security profile is added then reactivate this test");
            
            CareDirector.Sdk.ServiceRequest.AuthenticateRequest authenticationRequest = GetAuthenticationRequest("SecurityTestUserPlatformAPI2", "Passw0rd_!");
            CareDirector.Sdk.ServiceResponse.AuthenticateResponse authResponse = SecurityDataProvider.Authenticate(authenticationRequest);
            SetServiceConnectionDataFromAuthenticationResponse(authResponse);

            Assert.IsTrue(authResponse.IsAuthenticated);
            Assert.IsFalse(authResponse.HasErrors);
            Assert.IsNull(authResponse.Exception);

            Guid providerid = Guid.NewGuid();
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                string providerName = "Security Test Provider Platform API 2 - " + DateTime.Now.ToString("yyyyMMddhhmmssfff");
                Provider p = GetProviderToCreate(providerid, providerName, new Guid("c36adcfd-91c1-e811-80dc-0050560502cc"), new Guid("CCC6053B-8CC1-E811-80DC-0050560502CC"));
                entity.Providers.Add(p);
                entity.SaveChanges();
            }

            List<Guid> idsToDelete = new List<Guid> { providerid };
            CareDirector.Sdk.ServiceRequest.DeleteMultipleRequestCollection request = new CareDirector.Sdk.ServiceRequest.DeleteMultipleRequestCollection();
            request.Add("provider", idsToDelete);


            //Perform the call to the "DeleteMultiple" method
            CareDirector.Sdk.ServiceResponse.DeleteMultipleResponse response = DataProvider.DeleteMultiple(request);

            //Validate that the platform Delete. The user have delete security profile
            Assert.IsFalse(response.DeleteResponses[0].UnauthorisedAccess);
            Assert.IsNull(response.DeleteResponses[0].Exception);
            Assert.AreEqual(providerid, response.DeleteResponses[0].Id);
        }







        private CareDirector.Sdk.ServiceRequest.AuthenticateRequest GetAuthenticationRequest(string UserName, string Password)
        {
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
