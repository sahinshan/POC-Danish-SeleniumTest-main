//using System;
//using System.Linq;
//using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Text;
//using System.Configuration;
//using Phoenix.WorkflowTestFramework;

//namespace Phoenix.WorkflowTests.WorkflowDetailsSection
//{
//    [TestClass]
//    public class WorkflowDetailsSectionTests
//    {
//        public Phoenix.WorkflowTestFramework.PhoenixPlatformServiceHelper phoenixPlatformServiceHelper { get; set; }

//        [TestInitialize]
//        public void TestInitializationMethod()
//        {
//            phoenixPlatformServiceHelper = new WorkflowTestFramework.PhoenixPlatformServiceHelper();
//            var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_0", "Passw0rd_!");
//            var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//            phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);
//        }


//        //[TestProperty("JiraIssueID", "CDV6-7631")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 1 - User creating the record do not belongs to the team that owns the workflow")]
//        //public void WorkflowDetailsSection_TestMethod001()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 1";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //login with a user that do not belongs to Caredirector team
//        //    var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_1", "Passw0rd_!");
//        //    var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//        //    phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);

//        //    //create the phone call record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    bool inactive = phoenixPlatformServiceHelper.GetPhoneCallInactiveField(phoneCallID);
//        //    Assert.IsFalse(inactive);


//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7632")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 2 - User creating the record belongs to the team that owns the workflow")]
//        //public void WorkflowDetailsSection_TestMethod002()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 1";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the phone call record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    bool inactive = phoenixPlatformServiceHelper.GetPhoneCallInactiveField(phoneCallID);
//        //    Assert.IsTrue(inactive);
//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7633")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 3 - User creating the record do not belongs to the Business Unit that owns the workflow")]
//        //public void WorkflowDetailsSection_TestMethod003()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 3";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //login with a user that do not belongs to Caredirector QA Business Unit
//        //    var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_2", "Passw0rd_!");
//        //    var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//        //    phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);

//        //    //create the phone call record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    bool inactive = phoenixPlatformServiceHelper.GetPhoneCallInactiveField(phoneCallID);
//        //    Assert.IsFalse(inactive);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7634")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 4 - User creating the record belongs to the Business Unit that owns the workflow")]
//        //public void WorkflowDetailsSection_TestMethod004()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 3";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the phone call record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    bool inactive = phoenixPlatformServiceHelper.GetPhoneCallInactiveField(phoneCallID);
//        //    Assert.IsTrue(inactive);

//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7635")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 5 - User creating the record do not belongs to a Child Business Unit that owns the workflow")]
//        //public void WorkflowDetailsSection_TestMethod005()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 5";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the phone call record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    bool inactive = phoenixPlatformServiceHelper.GetPhoneCallInactiveField(phoneCallID);
//        //    Assert.IsFalse(inactive);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7636")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 6 - User creating the record belongs to a Child Business Unit that owns the workflow")]
//        //public void WorkflowDetailsSection_TestMethod006()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 5";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //login with a user that belongs to a child Business Unit
//        //    var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_3", "Passw0rd_!");
//        //    var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//        //    phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);

//        //    //create the phone call record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    bool inactive = phoenixPlatformServiceHelper.GetPhoneCallInactiveField(phoneCallID);
//        //    Assert.IsTrue(inactive);

//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7637")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 7 - WF scope is set to Organization - User creating the record do not belongs to the BU (or child BU) that owns the workflow")]
//        //public void WorkflowDetailsSection_TestMethod007()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 7";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the phone call record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    bool inactive = phoenixPlatformServiceHelper.GetPhoneCallInactiveField(phoneCallID);
//        //    Assert.IsTrue(inactive);

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7638")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 8 - Create a record when the WF Start options are all set to No")]
//        //public void WorkflowDetailsSection_TestMethod008()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 8";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the phone call record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual(description, descriptionAfterSave);

//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7639")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 9 - Update a record when the WF Start options are all set to No")]
//        //public void WorkflowDetailsSection_TestMethod009()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 8";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //create the phone call record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);



//        //    //ACT
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallRecordDescription(phoneCallID, "Description field update ...");



//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("Description field update ...", descriptionAfterSave);

//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7640")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 10 - Create a record when the only the WF 'Record Is Created' is set to Yes")]
//        //public void WorkflowDetailsSection_TestMethod010()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 10";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("<p>WF Testinng - Scenario 10 - Action 1 Activated</p>", descriptionAfterSave);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7641")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 11 - Update a record when the only the WF 'Record Is Created' is set to Yes")]
//        //public void WorkflowDetailsSection_TestMethod011()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 10";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);

//        //    //ACT

//        //    //update the record
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 10 - Update");


//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("WF Testinng - Scenario 10 - Update", descriptionAfterSave);

//        //}




//        //[TestProperty("JiraIssueID", "CDV6-7642")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 12 - Update a record without updating the record status")]
//        //public void WorkflowDetailsSection_TestMethod012()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 12";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);

//        //    //ACT

//        //    //update the record 
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 12 - Update");


//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("WF Testinng - Scenario 12 - Update", descriptionAfterSave);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7643")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 13 - Update the record statusId to completed and set the record as inactive")]
//        //public void WorkflowDetailsSection_TestMethod013()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 12";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);

//        //    //ACT

//        //    //update the record
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 12 - Update");
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallRecordStatus(phoneCallID, 2, true);


//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("<p>WF Testinng - Scenario 12 - Action 1 Activated</p>", descriptionAfterSave);
//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7644")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 14 - Update the Phone Call Date field")]
//        //public void WorkflowDetailsSection_TestMethod014()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 14";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);

//        //    //ACT

//        //    //update the record 
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 14 - Update");
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallDateField(phoneCallID, DateTime.Now.WithoutMilliseconds().AddHours(-1));


//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("<p>WF Testinng - Scenario 14 - Action 1 Activated</p>", descriptionAfterSave);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7645")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 15 - Update a field different from the Phone Call Date field")]
//        //public void WorkflowDetailsSection_TestMethod015()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 14";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);

//        //    //ACT

//        //    //update the record 
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 14 - Update");


//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("WF Testinng - Scenario 14 - Update", descriptionAfterSave);
//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7646")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 16 - Update a field other than the responsible team")]
//        //public void WorkflowDetailsSection_TestMethod016()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 16";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);

//        //    //ACT

//        //    //update the record 
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 16 - Update");


//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("WF Testinng - Scenario 16 - Update", descriptionAfterSave);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7647")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 17 - Update the Responsible Team")]
//        //public void WorkflowDetailsSection_TestMethod017()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 16";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);

//        //    //ACT

//        //    //update the record 
//        //    Guid BridgendAdoptionTeamID = new Guid("12441897-d072-4bde-a94e-e0c9c2b48bde");
//        //    Guid socialCareOwningBU = new Guid("d8031233-1039-e911-a2c5-005056926fe4");
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 16 - Update");
//        //    phoenixPlatformServiceHelper.AssignPhoneCallRecords(phoneCallID, BridgendAdoptionTeamID, socialCareOwningBU, null);


//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("<p>WF Testinng - Scenario 16 - Action 1 Activated</p>", descriptionAfterSave);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7648")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 17.1 - Workflow should NOT be triggered by the create event")]
//        //public void WorkflowDetailsSection_TestMethod017_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 16";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);

//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("Sample Description ...", descriptionAfterSave);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7649")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 18 - Update the Responsible User")]
//        //public void WorkflowDetailsSection_TestMethod018()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 16";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);

//        //    //ACT

//        //    //update the record 
//        //    Guid reponsibleUserId = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 16 - Update");
//        //    phoenixPlatformServiceHelper.AssignPhoneCallRecords(phoneCallID, teamID, teamOwningBU, reponsibleUserId);


//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("<p>WF Testinng - Scenario 16 - Action 1 Activated</p>", descriptionAfterSave);
//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7650")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 19 - Creating a new record should not trigger the WF")]
//        //public void WorkflowDetailsSection_TestMethod019()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 19";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all phone call records for the person
//        //    phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);
//        //    Assert.AreEqual(1, phoneCallIDs.Count);
//        //    Assert.AreEqual(phoneCallID, phoneCallIDs[0]);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7651")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 20 - Updating a record should not trigger the WF")]
//        //public void WorkflowDetailsSection_TestMethod020()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 19";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ACT

//        //    phoenixPlatformServiceHelper.UpdatePhoneCallRecordDescription(phoneCallID, "WF Testinng - Scenario 19 - Update");


//        //    //ASSERT

//        //    //get all phone call records for the person
//        //    System.Threading.Thread.Sleep(2000);
//        //    phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);
//        //    Assert.AreEqual(1, phoneCallIDs.Count);
//        //    Assert.AreEqual(phoneCallID, phoneCallIDs[0]);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7652")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 21 - Deleting a record should not trigger the WF")]
//        //public void WorkflowDetailsSection_TestMethod021()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 19";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ACT

//        //    phoenixPlatformServiceHelper.DeletePhoneCall(phoneCallID);


//        //    //ASSERT

//        //    //get all phone call records for the person
//        //    System.Threading.Thread.Sleep(2000);
//        //    phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);
//        //    Assert.AreEqual(1, phoneCallIDs.Count);
//        //    Assert.AreNotEqual(phoneCallID, phoneCallIDs[0]);

//        //    string phoneCallSubject = phoenixPlatformServiceHelper.GetPhoneCallSubjectField(phoneCallIDs[0]);
//        //    string phoneCallDescription = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallIDs[0]);

//        //    Assert.AreEqual("Phone Call record generated by WF Automated Testing - S19", phoneCallSubject);
//        //    Assert.AreEqual("<p>WF Testinng - Scenario 19 - Action 1 Activated</p>", phoneCallDescription);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-7654")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 22 - WF triggering another WF")]
//        //public void WorkflowDetailsSection_TestMethod022()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 22";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 2);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT

//        //    string phoneCallDescription = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("<p>WF Testinng - Scenario 22 - Action 1 Activated</p>", phoneCallDescription);

//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7655")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 23 - WF can be triggered on demand")]
//        //public void WorkflowDetailsSection_TestMethod023()
//        //{
//        //    Assert.Inconclusive("The 'Is On Demand Process' functionaity is not finished. After thefunctionality is finished it is necessary to update this test to call the service method that will allow the on demand execution of workflows");


//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 23";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ACT

//        //    //Execute the service method to trigger the WF 23


//        //    //ASSERT

//        //    string phoneCallDescription = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("<p>WF Testinng - Scenario 23 - Action 1 Activated</p>\n", phoneCallDescription);

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7656")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 24 - Automatically delete completed Jobs")]
//        //public void WorkflowDetailsSection_TestMethod024()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 24";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    string phoneCallDescription = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID);
//        //    Assert.AreEqual("<p>WF Testinng - Scenario 24 - Action 1 Activated</p>", phoneCallDescription);

//        //    List<Guid> workflowIds = phoenixPlatformServiceHelper.GetWorkflowIdByWorkflowName("WF Automated Testing - S24");
//        //    if (workflowIds.Count > 1)
//        //        Assert.Fail("We have more than 1 workflow with the same name");

//        //    int totalJobsForWF = phoenixPlatformServiceHelper.CountWorkflowJobsForWorkflowID(workflowIds[0]);
//        //    Assert.AreEqual(0, totalJobsForWF);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-7657")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 25 - Pre-Sync WF will display a message")]
//        //public void WorkflowDetailsSection_TestMethod025()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 25";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    try
//        //    {
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        //ASSERT
//        //        Assert.AreEqual("'WF Automated Testing - S25': WF Testinng - Scenario 25 - Action 1 Activated", ex.Message);
//        //        return;
//        //    }


//        //    Assert.Fail("An error should have been triggered by the Workflow 'WF Automated Testing - S25' ");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7658")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 25.1 - Create a record with a different subject and validate that the WF is not triggered")]
//        //public void WorkflowDetailsSection_TestMethod025_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 25.1";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT


//        //    //create the record (if an exception is triggered by the WF the test will fail)
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7659")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 26 - Pre-Sync WF with Stop Workflow action")]
//        //public void WorkflowDetailsSection_TestMethod026()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 26";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT


//        //    //create the record (if an exception is triggered by the WF the test will fail)
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //}





//        //[TestProperty("JiraIssueID", "CDV6-7660")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 27 - Test create record action")]
//        //public void WorkflowDetailsSection_TestMethod027()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 27";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all phone call records for the person
//        //    phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);
//        //    Assert.AreEqual(2, phoneCallIDs.Count);

//        //    //validate the description field of the record created by the WF
//        //    Guid newPhoneCallRecord = phoneCallIDs.FirstOrDefault(c => c != phoneCallID);
//        //    string recordSubjectField = phoenixPlatformServiceHelper.GetPhoneCallSubjectField(newPhoneCallRecord);
//        //    Assert.AreEqual("WF Testinng - Scenario 27 - Action 1 Activated", recordSubjectField);
//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7661")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 28 - Test update record action")]
//        //public void WorkflowDetailsSection_TestMethod028()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 28";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var phoneCallFields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID
//        //        , "CallerId", "CallerIdTableName", "CallerIdName", "PhoneNumber", "DirectionId", "subject", "PhoneCallDate");

//        //    Guid providerCallerID = new Guid("81cc3d13-c7cd-4118-b60c-9f6596f966a4");
//        //    string providerCallerIDTableName = "provider";
//        //    string providerName = "Ynys Mon - Mental Health - Provider";

//        //    Assert.AreEqual(providerCallerID, (Guid)phoneCallFields["CallerId".ToLower()]);
//        //    Assert.AreEqual(providerCallerIDTableName, (string)phoneCallFields["CallerIdTableName".ToLower()]);
//        //    Assert.AreEqual(providerName, (string)phoneCallFields["CallerIdName".ToLower()]);
//        //    Assert.AreEqual("987654321", (string)phoneCallFields["PhoneNumber".ToLower()]);
//        //    Assert.AreEqual(2, (int)phoneCallFields["DirectionId".ToLower()]);
//        //    Assert.AreEqual("WF Testinng - Scenario 28 - Action 1 Activated", (string)phoneCallFields["subject".ToLower()]);
//        //    Assert.AreEqual(new DateTime(2019, 3, 1, 9, 0, 0), (DateTime)phoneCallFields["PhoneCallDate".ToLower()]);

//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7662")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 29 - Test Assign record action")]
//        //public void WorkflowDetailsSection_TestMethod029()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testinng - Scenario 29";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var phoneCallFields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "OwnerId", "OwningBusinessUnitId");

//        //    Guid blaenauGwentPrimaryHealthSocialWorkerTeamID = new Guid("214a6bd2-9adf-4b1c-a0c0-a123b58471bd");
//        //    Guid healthBusinessUnitID = new Guid("4567d62a-1039-e911-a2c5-005056926fe4");

//        //    Assert.AreEqual(blaenauGwentPrimaryHealthSocialWorkerTeamID, (Guid)phoneCallFields["OwnerId".ToLower()]);
//        //    Assert.AreEqual(healthBusinessUnitID, (Guid)phoneCallFields["OwningBusinessUnitId".ToLower()]);

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7663")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 30 - Test Send Email record action")]
//        //public void WorkflowDetailsSection_TestMethod030()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 30";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emailIDs.Count);

//        //    //get the email info
//        //    var email = phoenixPlatformServiceHelper.GetEmailById(emailIDs[0],
//        //        "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "DueDate", "EmailFromId", "Notes",
//        //        "ActivityReasonId", "ActivityOutcomeId", "ActivityCategoryId", "ActivitySubCategoryId", "PersonId", "ResponsibleUserId");

//        //    Guid jbrazetaSystemUserID = new Guid("32972024-0839-E911-A2C5-005056926FE4");

//        //    Assert.AreEqual("WF Testinng - Scenario 30 - Action 1 Activated", (string)email["subject".ToLower()]);
//        //    Assert.AreEqual(regardingID, (Guid)email["RegardingID".ToLower()]);
//        //    Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
//        //    Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);
//        //    Assert.AreEqual(new DateTime(2019, 03, 01, 9, 0, 0), (DateTime)email["DueDate".ToLower()]);
//        //    Assert.AreEqual(jbrazetaSystemUserID, (Guid)email["EmailFromId".ToLower()]);
//        //    Assert.AreEqual("<p>Mail Description ...</p>", (string)email["notes".ToLower()]);
//        //    Assert.AreEqual(new Guid("b9ec74e3-9c45-e911-a2c5-005056926fe4"), (Guid)email["ActivityReasonId".ToLower()]);
//        //    Assert.AreEqual(new Guid("4c2bec1c-9e45-e911-a2c5-005056926fe4"), (Guid)email["ActivityOutcomeId".ToLower()]);
//        //    Assert.AreEqual(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), (Guid)email["ActivityCategoryId".ToLower()]);
//        //    Assert.AreEqual(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), (Guid)email["ActivitySubCategoryId".ToLower()]);
//        //    Assert.AreEqual(regardingID, (Guid)email["PersonId".ToLower()]);
//        //    Assert.AreEqual(jbrazetaSystemUserID, (Guid)email["ResponsibleUserId".ToLower()]);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7664")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 31 - Test Start Workflow action")]
//        //public void WorkflowDetailsSection_TestMethod031()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 31";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - Scenario 31 - Action 1 Activated", (string)phoneCallfields["subject".ToLower()]);
//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7665")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 32 - Test Change Record Status action")]
//        //public void WorkflowDetailsSection_TestMethod032()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 32";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "Inactive");
//        //    Assert.AreEqual(true, (bool)phoneCallfields["inactive".ToLower()]);
//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7666")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 33 - Test Stop Workflow action")]
//        //public void WorkflowDetailsSection_TestMethod033()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 33";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - Scenario 33", (string)phoneCallfields["subject".ToLower()]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7667")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 34 - Test Apply Data Restriction action")]
//        //public void WorkflowDetailsSection_TestMethod034()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 34";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT


//        //    Guid expectedDataRestriction = new Guid("00ea02a5-2852-e911-a2c5-005056926fe4");
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "datarestrictionid");
//        //    Assert.AreEqual(expectedDataRestriction, (Guid)phoneCallfields["DataRestrictionId".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7668")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 34 - Test Apply Data Restriction action (WF action should override existing data restrictions)")]
//        //public void WorkflowDetailsSection_TestMethod034_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "Temporary Subject";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ACT

//        //    //Update the record
//        //    Guid dataRestrictionID = new Guid("66f587ed-2752-e911-a2c5-005056926fe4");
//        //    phoenixPlatformServiceHelper.RestrictPhoneCall(phoneCallID, dataRestrictionID);
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "WF Testing - Scenario 34");

//        //    //ASSERT


//        //    Guid expectedDataRestriction = new Guid("00ea02a5-2852-e911-a2c5-005056926fe4");
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "DataRestrictionId");
//        //    Assert.AreEqual(expectedDataRestriction, (Guid)phoneCallfields["DataRestrictionId".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7669")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 34.2 - Test Apply Data Restriction action (WF action should not be able to override existing data restrictions)")]
//        //public void WorkflowDetailsSection_TestMethod034_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "Temporary Subject";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ACT

//        //    //Update the record
//        //    Guid dataRestrictionID = new Guid("00ea02a5-2852-e911-a2c5-005056926fe4");
//        //    phoenixPlatformServiceHelper.RestrictPhoneCall(phoneCallID, dataRestrictionID);
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "WF Testing - Scenario 34.2");



//        //    //ASSERT

//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "DataRestrictionId");
//        //    Assert.AreEqual(dataRestrictionID, (Guid)phoneCallfields["DataRestrictionId".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7670")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 34.3 - Test Apply Data Restriction action (WF action should NOT take effect if the restriction will restrict the responsible user)")]
//        //public void WorkflowDetailsSection_TestMethod034_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "Temporary Subject";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    Guid responsibleUserID = new Guid("32972024-0839-E911-A2C5-005056926FE4");


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, DateTime.Now.WithoutMilliseconds(), responsibleUserID);


//        //    //ACT

//        //    //Update the record
//        //    Guid dataRestrictionID = new Guid("66f587ed-2752-e911-a2c5-005056926fe4");
//        //    phoenixPlatformServiceHelper.RestrictPhoneCall(phoneCallID, dataRestrictionID);
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "WF Testing - Scenario 34.3");



//        //    //ASSERT

//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "DataRestrictionId");
//        //    Assert.AreEqual(dataRestrictionID, (Guid)phoneCallfields["DataRestrictionId".ToLower()]);
//        //}





//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7671")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 35 - Test Business Data Count action")]
//        //public void WorkflowDetailsSection_TestMethod035()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 35";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }




//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson("Phone call 1", description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);
//        //    Guid phoneCallID2 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);



//        //    //ASSERT
//        //    string phonecall1Description = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID1);
//        //    string phonecall2Description = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID2);

//        //    Assert.AreEqual("Sample Description ...", phonecall1Description);
//        //    Assert.AreEqual("2", phonecall2Description); //if the count operation succeded then the description field should be updated (there is a restricted record that is also being counted)
//        //}


//        ///// <summary>
//        ///// Workflow Under Test: Person Employment Date Overlap
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7672")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 36 - Test Business Data Overlap action (dates will everlap)")]
//        //public void WorkflowDetailsSection_TestMethod036()
//        //{
//        //    //ARRANGE 
//        //    string title = "Employment for Adolfo Abbott created by José Brazeta on" + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");
//        //    string employer = "Workflow Testing - Employer 2";

//        //    Guid EmploymentWeeklyHoursWorkedId = new Guid("09320bac-a92e-e911-80dc-0050560502cc");
//        //    Guid EmploymentStatusId = new Guid("19e341fc-a82e-e911-80dc-0050560502cc");
//        //    Guid? EmploymentTypeId = null;
//        //    Guid? EmploymentReasonLeftId = null;

//        //    Guid PersonID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime startDate = new DateTime(2019, 3, 16);
//        //    DateTime endDate = new DateTime(2019, 3, 17);

//        //    //get all Person Employment records for the person
//        //    List<Guid> employments = phoenixPlatformServiceHelper.GetPersonEmploymentForPersonRecord(PersonID, employer);

//        //    //Delete the records
//        //    foreach (Guid personEmploymentid in employments)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonEmployment(personEmploymentid);
//        //    }




//        //    //ACT

//        //    //create the record
//        //    try
//        //    {
//        //        Guid employmentID = phoenixPlatformServiceHelper.CreatePersonEmployment(title, employer, PersonID, teamID, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate, endDate);

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Assert.AreEqual("'Person Employment Date Overlap': An employment record for this person already exists for the period.", ex.Message);
//        //        return;
//        //    }


//        //    //ASSERT
//        //    Assert.Fail("Workflow should throw an exception");
//        //}

//        ///// <summary>
//        ///// Workflow Under Test: Person Employment Date Overlap
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7673")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 36.1 - Test Business Data Overlap action (dates will everlap)")]
//        //public void WorkflowDetailsSection_TestMethod036_1()
//        //{
//        //    //ARRANGE 
//        //    string title = "Employment for Adolfo Abbott created by José Brazeta on" + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");
//        //    string employer = "Workflow Testing - Employer 2";

//        //    Guid EmploymentWeeklyHoursWorkedId = new Guid("09320bac-a92e-e911-80dc-0050560502cc");
//        //    Guid EmploymentStatusId = new Guid("19e341fc-a82e-e911-80dc-0050560502cc");
//        //    Guid? EmploymentTypeId = null;
//        //    Guid? EmploymentReasonLeftId = null;

//        //    Guid PersonID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime startDate = new DateTime(2019, 3, 14);
//        //    DateTime endDate = new DateTime(2019, 3, 21);

//        //    //get all Person Employment records for the person
//        //    List<Guid> employments = phoenixPlatformServiceHelper.GetPersonEmploymentForPersonRecord(PersonID, employer);

//        //    //Delete the records
//        //    foreach (Guid personEmploymentid in employments)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonEmployment(personEmploymentid);
//        //    }




//        //    //ACT

//        //    //create the record
//        //    try
//        //    {
//        //        Guid employmentID = phoenixPlatformServiceHelper.CreatePersonEmployment(title, employer, PersonID, teamID, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate, endDate);

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Assert.AreEqual("'Person Employment Date Overlap': An employment record for this person already exists for the period.", ex.Message);
//        //        return;
//        //    }


//        //    //ASSERT
//        //    Assert.Fail("Workflow should throw an exception");
//        //}

//        ///// <summary>
//        ///// Workflow Under Test: Person Employment Date Overlap
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7674")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 36.2 - Test Business Data Overlap action (dates will everlap)")]
//        //public void WorkflowDetailsSection_TestMethod036_2()
//        //{
//        //    //ARRANGE 
//        //    string title = "Employment for Adolfo Abbott created by José Brazeta on" + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");
//        //    string employer = "Workflow Testing - Employer 2";

//        //    Guid EmploymentWeeklyHoursWorkedId = new Guid("09320bac-a92e-e911-80dc-0050560502cc");
//        //    Guid EmploymentStatusId = new Guid("19e341fc-a82e-e911-80dc-0050560502cc");
//        //    Guid? EmploymentTypeId = null;
//        //    Guid? EmploymentReasonLeftId = null;

//        //    Guid PersonID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime startDate = new DateTime(2019, 3, 14);
//        //    DateTime endDate = new DateTime(2019, 3, 16);

//        //    //get all Person Employment records for the person
//        //    List<Guid> employments = phoenixPlatformServiceHelper.GetPersonEmploymentForPersonRecord(PersonID, employer);

//        //    //Delete the records
//        //    foreach (Guid personEmploymentid in employments)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonEmployment(personEmploymentid);
//        //    }




//        //    //ACT

//        //    //create the record
//        //    try
//        //    {
//        //        Guid employmentID = phoenixPlatformServiceHelper.CreatePersonEmployment(title, employer, PersonID, teamID, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate, endDate);

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Assert.AreEqual("'Person Employment Date Overlap': An employment record for this person already exists for the period.", ex.Message);
//        //        return;
//        //    }


//        //    //ASSERT
//        //    Assert.Fail("Workflow should throw an exception");
//        //}

//        ///// <summary>
//        ///// Workflow Under Test: Person Employment Date Overlap
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7675")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 36.3 - Test Business Data Overlap action (dates will everlap)")]
//        //public void WorkflowDetailsSection_TestMethod036_3()
//        //{
//        //    //ARRANGE 
//        //    string title = "Employment for Adolfo Abbott created by José Brazeta on" + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");
//        //    string employer = "Workflow Testing - Employer 2";

//        //    Guid EmploymentWeeklyHoursWorkedId = new Guid("09320bac-a92e-e911-80dc-0050560502cc");
//        //    Guid EmploymentStatusId = new Guid("19e341fc-a82e-e911-80dc-0050560502cc");
//        //    Guid? EmploymentTypeId = null;
//        //    Guid? EmploymentReasonLeftId = null;

//        //    Guid PersonID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime startDate = new DateTime(2019, 3, 16);
//        //    DateTime endDate = new DateTime(2019, 3, 22);

//        //    //get all Person Employment records for the person
//        //    List<Guid> employments = phoenixPlatformServiceHelper.GetPersonEmploymentForPersonRecord(PersonID, employer);

//        //    //Delete the records
//        //    foreach (Guid personEmploymentid in employments)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonEmployment(personEmploymentid);
//        //    }




//        //    //ACT

//        //    //create the record
//        //    try
//        //    {
//        //        Guid employmentID = phoenixPlatformServiceHelper.CreatePersonEmployment(title, employer, PersonID, teamID, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate, endDate);

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Assert.AreEqual("'Person Employment Date Overlap': An employment record for this person already exists for the period.", ex.Message);
//        //        return;
//        //    }


//        //    //ASSERT
//        //    Assert.Fail("Workflow should throw an exception");
//        //}


//        ///// <summary>
//        ///// Workflow Under Test: Person Employment Date Overlap
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7676")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 36.4 - Test Business Data Overlap action (dates will NOT everlap)")]
//        //public void WorkflowDetailsSection_TestMethod036_4()
//        //{
//        //    //ARRANGE 
//        //    string title = "Employment for Adolfo Abbott created by José Brazeta on" + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");
//        //    string employer = "Workflow Testing - Employer 2";

//        //    Guid EmploymentWeeklyHoursWorkedId = new Guid("09320bac-a92e-e911-80dc-0050560502cc");
//        //    Guid EmploymentStatusId = new Guid("19e341fc-a82e-e911-80dc-0050560502cc");
//        //    Guid? EmploymentTypeId = null;
//        //    Guid? EmploymentReasonLeftId = null;

//        //    Guid PersonID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime startDate = new DateTime(2019, 3, 10);
//        //    DateTime endDate = new DateTime(2019, 3, 14);

//        //    //get all Person Employment records for the person
//        //    List<Guid> employments = phoenixPlatformServiceHelper.GetPersonEmploymentForPersonRecord(PersonID, employer);

//        //    //Delete the records
//        //    foreach (Guid personEmploymentid in employments)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonEmployment(personEmploymentid);
//        //    }




//        //    //ACT

//        //    //create the record
//        //    Guid employmentID = phoenixPlatformServiceHelper.CreatePersonEmployment(title, employer, PersonID, teamID, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate, endDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPersonEmploymentByID(employmentID, "Employer");
//        //    Assert.AreEqual(employer, (string)fields["employer"]);
//        //}

//        ///// <summary>
//        ///// Workflow Under Test: Person Employment Date Overlap
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7677")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 36.5 - Test Business Data Overlap action (dates will NOT everlap)")]
//        //public void WorkflowDetailsSection_TestMethod036_5()
//        //{
//        //    //ARRANGE 
//        //    string title = "Employment for Adolfo Abbott created by José Brazeta on" + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");
//        //    string employer = "Workflow Testing - Employer 2";

//        //    Guid EmploymentWeeklyHoursWorkedId = new Guid("09320bac-a92e-e911-80dc-0050560502cc");
//        //    Guid EmploymentStatusId = new Guid("19e341fc-a82e-e911-80dc-0050560502cc");
//        //    Guid? EmploymentTypeId = null;
//        //    Guid? EmploymentReasonLeftId = null;

//        //    Guid PersonID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime startDate = new DateTime(2019, 3, 21);
//        //    DateTime endDate = new DateTime(2019, 3, 23);

//        //    //get all Person Employment records for the person
//        //    List<Guid> employments = phoenixPlatformServiceHelper.GetPersonEmploymentForPersonRecord(PersonID, employer);

//        //    //Delete the records
//        //    foreach (Guid personEmploymentid in employments)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonEmployment(personEmploymentid);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid employmentID = phoenixPlatformServiceHelper.CreatePersonEmployment(title, employer, PersonID, teamID, EmploymentWeeklyHoursWorkedId, EmploymentStatusId, EmploymentTypeId, EmploymentReasonLeftId, startDate, endDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPersonEmploymentByID(employmentID, "Employer");
//        //    Assert.AreEqual(employer, (string)fields["employer"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7678")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 38 - Test CHECK MULTIPLE RESPONSE ITEM BY IDENTIFIER  action (checklist answer NOT selected)")]
//        //public void WorkflowDetailsSection_TestMethod038()
//        //{
//        //    //ARRANGE 

//        //    Guid caseFormID = new Guid("3975da51-4152-e911-a2c5-005056926fe4");

//        //    //reset the case form fields
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2017, 1, 1), false);


//        //    //ACT

//        //    //set the date to activate the workflow
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2019, 1, 1), false);


//        //    //ASSERT

//        //    //Separate assessment field should not be updated by the WF
//        //    var fields = phoenixPlatformServiceHelper.GetCaseFormByID(caseFormID, "startdate", "separateassessment");
//        //    Assert.AreEqual(new DateTime(2019, 1, 1), (DateTime)fields["startdate"]);
//        //    Assert.AreEqual(false, (bool)fields["separateassessment"]);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7679")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 38.1 - Test CHECK MULTIPLE RESPONSE ITEM BY IDENTIFIER  action (checklist answer selected)")]
//        //public void WorkflowDetailsSection_TestMethod038_1()
//        //{
//        //    //ARRANGE 

//        //    Guid caseFormID = new Guid("68d16a3a-4052-e911-a2c5-005056926fe4");

//        //    //reset the case form fields
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2017, 1, 1), false);


//        //    //ACT

//        //    //set the date to activate the workflow
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2019, 1, 1), false);


//        //    //ASSERT

//        //    //Separate assessment field should be updated by the WF
//        //    var fields = phoenixPlatformServiceHelper.GetCaseFormByID(caseFormID, "startdate", "separateassessment");
//        //    Assert.AreEqual(new DateTime(2019, 1, 1), (DateTime)fields["startdate"]);
//        //    Assert.AreEqual(true, (bool)fields["separateassessment"]);

//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7680")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 39 - Test Get Initiating User Action action (Initiating user is Workflow_Test_User_1)")]
//        //public void WorkflowDetailsSection_TestMethod039()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 39";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }




//        //    //ACT

//        //    //login with Workflow_Test_User_1
//        //    var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_1", "Passw0rd_!");
//        //    var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//        //    phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);




//        //    //ASSERT
//        //    Guid Workflow_Test_User_1UserID = new Guid("B9B8B4C7-1552-E911-A2C5-005056926FE4");
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "responsibleuserid", "callerid", "calleridname", "CallerIdTableName", "recipientid", "recipientIdTableName", "recipientIdName");
//        //    Assert.AreEqual(Workflow_Test_User_1UserID, (Guid)fields["responsibleuserid"]);
//        //    Assert.AreEqual(Workflow_Test_User_1UserID, (Guid)fields["callerid"]);
//        //    Assert.AreEqual("Workflow Test User 1", (string)fields["calleridname"]);
//        //    Assert.AreEqual("systemuser", (string)fields["calleridtablename"]);
//        //    Assert.AreEqual(Workflow_Test_User_1UserID, (Guid)fields["recipientid"]);
//        //    Assert.AreEqual("Workflow Test User 1", (string)fields["recipientidname"]);
//        //    Assert.AreEqual("systemuser", (string)fields["recipientidtablename"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7681")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 39.1 - Test Get Initiating User Action action (Initiating user is NOT Workflow_Test_User_1)")]
//        //public void WorkflowDetailsSection_TestMethod039_1()
//        //{

//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 39";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }




//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);




//        //    //ASSERT
//        //    Guid Workflow_Test_User_0UserID = new Guid("3D17AF27-F74E-E911-9C54-F8B156AF4F99");
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "responsibleuserid");
//        //    Assert.IsNull(fields["responsibleuserid"]);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-7682")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 39.2 - Test setting the value of a multi-lookup to another multi-lookup (multi-lookup options match) - exctract the value from a Recipient and set it to Caller - (this is an additional scenario created to validate a bug fix)")]
//        //public void WorkflowDetailsSection_TestMethod039_2_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 39.2";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }




//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);




//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "callerid", "calleridname", "CallerIdTableName");

//        //    Assert.AreEqual(recipientID, (Guid)fields["callerid"]);
//        //    Assert.AreEqual(recipientIDName, (string)fields["calleridname"]);
//        //    Assert.AreEqual(recipientIdTableName, (string)fields["calleridtablename"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7683")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 39.2 - Test setting the value of a multi-lookup to another multi-lookup (multi-lookup options match) - exctract the value from a Recipient and set it to Caller - (this is an additional scenario created to validate a bug fix)")]
//        //public void WorkflowDetailsSection_TestMethod039_2_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 39.2";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("81cc3d13-c7cd-4118-b60c-9f6596f966a4");
//        //    string recipientIdTableName = "provider";
//        //    string recipientIDName = "Ynys Mon - Mental Health - Provider";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }




//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);




//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "callerid", "calleridname", "CallerIdTableName");

//        //    Assert.AreEqual(recipientID, (Guid)fields["callerid"]);
//        //    Assert.AreEqual(recipientIDName, (string)fields["calleridname"]);
//        //    Assert.AreEqual(recipientIdTableName, (string)fields["calleridtablename"]);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-7684")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 39.3 - Test setting the value of a multi-lookup to another multi-lookup (multi-lookup options may not math) - exctract the value from the Regarding field and set it to Caller - (this is an additional scenario created to validate a bug fix)")]
//        //public void WorkflowDetailsSection_TestMethod039_3_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 39.3";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("81cc3d13-c7cd-4118-b60c-9f6596f966a4");
//        //    string callerIdTableName = "provider";
//        //    string callerIDName = "Ynys Mon - Mental Health - Provider";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingIdName = "Adolfo Abbott";
//        //    string regardingIdTableName = "person";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }




//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingIdName, teamID, regardingIdTableName);




//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "callerid", "calleridname", "CallerIdTableName");

//        //    Assert.AreEqual(regardingID, (Guid)fields["callerid"]);
//        //    Assert.AreEqual(regardingIdName, (string)fields["calleridname"]);
//        //    Assert.AreEqual(regardingIdTableName, (string)fields["calleridtablename"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7685")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 39.3 - Test setting the value of a multi-lookup to another multi-lookup (multi-lookup options may not math) - exctract the value from the Regarding field and set it to Caller - (this is an additional scenario created to validate a bug fix)")]
//        //public void WorkflowDetailsSection_TestMethod039_3_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 39.3";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("81cc3d13-c7cd-4118-b60c-9f6596f966a4");
//        //    string callerIdTableName = "provider";
//        //    string callerIDName = "Ynys Mon - Mental Health - Provider";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("0462352f-e53a-e911-a2c5-005056926fe4");
//        //    string regardingIdName = "Abbott Adolfo - (2015-01-06 00:00:00) [QA-CAS-000001-0098523]";

//        //    Guid personid = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string personidName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForCaseRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForCase(
//        //        subject, description, callerID, callerIdTableName, callerIDName,
//        //        recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingIdName, personid, personidName, teamID, "CareDirector QA", DateTime.Now.WithoutMilliseconds(), null, null);




//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "callerid", "calleridname", "CallerIdTableName");

//        //    Assert.IsNull(fields["callerid"]);
//        //    Assert.IsNull(fields["calleridname"]);
//        //    Assert.IsNull(fields["calleridtablename"]);
//        //}





//        //[TestProperty("JiraIssueID", "CDV6-7686")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 40 - Test Get Answer By Identifier action (Answers should activate WF actions)")]
//        //public void WorkflowDetailsSection_TestMethod040()
//        //{
//        //    //ARRANGE 

//        //    Guid caseFormID = new Guid("34241d9d-4152-e911-a2c5-005056926fe4");  //case form that will activate WF actions

//        //    //reset the case form fields
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2019, 1, 1), null, null, false, false, false, false);


//        //    //ACT

//        //    //set the date to activate the workflow
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2019, 3, 1), false);


//        //    //ASSERT

//        //    //validate updates performed by the workflow (all if statments to the Get Answer By Identifier action should be activated)
//        //    Guid Workflow_Test_User_0UserID = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4");
//        //    var fields = phoenixPlatformServiceHelper.GetCaseFormByID(caseFormID, "duedate", "ResponsibleUserID", "SeparateAssessment", "CarerDeclinedJointAssessment", "JointCarerAssessment", "NewPerson");
//        //    Assert.AreEqual(new DateTime(2019, 3, 2), (DateTime)fields["duedate"]);
//        //    Assert.AreEqual(Workflow_Test_User_0UserID, (Guid)fields["responsibleuserid"]);
//        //    Assert.AreEqual(true, (bool)fields["separateassessment"]);
//        //    Assert.AreEqual(true, (bool)fields["carerdeclinedjointassessment"]);
//        //    Assert.AreEqual(true, (bool)fields["jointcarerassessment"]);
//        //    Assert.AreEqual(true, (bool)fields["newperson".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7687")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 40.1 - Test Get Answer By Identifier action (Answers should NOT activate WF actions)")]
//        //public void WorkflowDetailsSection_TestMethod040_1()
//        //{
//        //    //ARRANGE 

//        //    Guid caseFormID = new Guid("a6bcd284-4352-e911-a2c5-005056926fe4");  //case form that will NOT activate WF actions

//        //    //reset the case form fields
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2019, 1, 1), null, null, false, false, false, false);


//        //    //ACT

//        //    //set the date to activate the workflow
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2019, 3, 1), false);


//        //    //ASSERT

//        //    //validate updates performed by the workflow (all if statments to the Get Answer By Identifier action should NOT be activated)
//        //    var fields = phoenixPlatformServiceHelper.GetCaseFormByID(caseFormID, "duedate", "ResponsibleUserID", "SeparateAssessment", "CarerDeclinedJointAssessment", "JointCarerAssessment", "NewPerson");
//        //    Assert.AreEqual(null, fields["duedate"]);
//        //    Assert.AreEqual(null, fields["ResponsibleUserID".ToLower()]);
//        //    Assert.AreEqual(false, (bool)fields["SeparateAssessment".ToLower()]);
//        //    Assert.AreEqual(false, (bool)fields["CarerDeclinedJointAssessment".ToLower()]);
//        //    Assert.AreEqual(false, (bool)fields["JointCarerAssessment".ToLower()]);
//        //    Assert.AreEqual(false, (bool)fields["NewPerson".ToLower()]);
//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7688")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 41 - Test Add Days action")]
//        //public void WorkflowDetailsSection_TestMethod041()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 41";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime phonecallDate = new DateTime(2018, 1, 1);


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }




//        //    //ACT

//        //    //login with Workflow_Test_User_1
//        //    var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_1", "Passw0rd_!");
//        //    var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//        //    phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);




//        //    //ASSERT
//        //    DateTime expectedPhonecallDate = phonecallDate
//        //        .AddYears(1)
//        //        .AddMonths(2)
//        //        .AddDays(21) //add 3 weeks = 3 * 7 = 21 days
//        //        .AddDays(4)
//        //        .AddHours(5)
//        //        .AddMinutes(6);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(expectedPhonecallDate.ToString(), (string)fields["notes"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7689")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 41 - Test Add Days action - phone call date is null")]
//        //public void WorkflowDetailsSection_TestMethod041_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 41";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime? phonecallDate = null;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }




//        //    //ACT

//        //    //login with Workflow_Test_User_1
//        //    var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_1", "Passw0rd_!");
//        //    var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//        //    phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);




//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("01/01/0001 00:00:00", (string)fields["notes"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7690")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 42 - Test Get Higher Date action (event date is the bigger date)")]
//        //public void WorkflowDetailsSection_TestMethod042()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 42";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);

//        //    Guid jBrazetaResponsibleUSerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    bool InformationByThirdParty = false;
//        //    int DirectionID = 1; //incoming

//        //    bool IsSignificantEvent = true;
//        //    DateTime SignificantEventDate = new DateTime(2019, 3, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, jBrazetaResponsibleUSerID,
//        //        null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(SignificantEventDate.AddSeconds(10).ToString(), (string)fields["notes"]); //we need to add 10 seconds due to a issue with phoenix - //Some times while executing modified on adding on start date is greater than modified of due date, hence increase 10 second of due date to make start date always less than due date.
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7691")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 42.1 - Test Get Higher Date action (phone call date is the bigger date)")]
//        //public void WorkflowDetailsSection_TestMethod042_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 42";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 4, 1, 9, 15, 30);

//        //    Guid jBrazetaResponsibleUSerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    bool InformationByThirdParty = false;
//        //    int DirectionID = 1; //incoming

//        //    bool IsSignificantEvent = true;
//        //    DateTime SignificantEventDate = new DateTime(2019, 3, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, jBrazetaResponsibleUSerID,
//        //        null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(phoneCallDate.AddSeconds(10).ToString(), (string)fields["notes"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7692")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 42.1 - Test Get Higher Date action (phone call date is null)")]
//        //public void WorkflowDetailsSection_TestMethod042_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 42";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime? phoneCallDate = null;

//        //    Guid jBrazetaResponsibleUSerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    bool InformationByThirdParty = false;
//        //    int DirectionID = 1; //incoming

//        //    bool IsSignificantEvent = true;
//        //    DateTime SignificantEventDate = new DateTime(2019, 3, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, jBrazetaResponsibleUSerID,
//        //        null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(SignificantEventDate.AddSeconds(10).ToString(), (string)fields["notes"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7693")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 42.1 - Test Get Higher Date action (Event date is null)")]
//        //public void WorkflowDetailsSection_TestMethod042_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 42";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 4, 1, 9, 15, 30);


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, 1, false, null, null, false);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(phoneCallDate.AddSeconds(10).ToString(), (string)fields["notes"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7694")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 43 - Test Subtract Days action")]
//        //public void WorkflowDetailsSection_TestMethod043()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 43";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime phonecallDate = new DateTime(2018, 1, 1);


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }




//        //    //ACT

//        //    //login with Workflow_Test_User_1
//        //    var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_1", "Passw0rd_!");
//        //    var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//        //    phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);




//        //    //ASSERT
//        //    var usersettings = phoenixPlatformServiceHelper.GetMetadataUserSettings();

//        //    DateTime expectedPhonecallDate = phonecallDate.ToUniversalTime()
//        //        .AddYears(-1)
//        //        .AddMonths(-2)
//        //        .AddDays(-21) //add 3 weeks = 3 * 7 = 21 days
//        //        .AddDays(-4)
//        //        .AddHours(-5)
//        //        .AddMinutes(-6);

//        //    DateTime finalDate = usersettings.ConvertTimeFromUtc(expectedPhonecallDate).Value;

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(finalDate.ToString(), (string)fields["notes"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7695")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 43 - Test Subtract Days action - phone call date is null")]
//        //public void WorkflowDetailsSection_TestMethod043_0()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 43";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime? phonecallDate = null;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }




//        //    //ACT

//        //    //login with Workflow_Test_User_1
//        //    var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_1", "Passw0rd_!");
//        //    var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//        //    phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecallDate);




//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("01/01/0001 00:00:00", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7696")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 43.1 - Test Concatenate action")]
//        //public void WorkflowDetailsSection_TestMethod043_1()
//        //{
//        //    //Assert.Inconclusive("There is a bug with the concatenate + update description field test. The WF ends up in a infinite loop");

//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 43.1";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }



//        //    //ACT

//        //    //create the 
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject");

//        //    Assert.AreEqual("WF Testing - Scenario 43.1Sample Description ...", (string)fields["subject"]);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-7697")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 44 - Test Update action in a Async workflow (update related person)")]
//        //public void WorkflowDetailsSection_TestMethod044()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 44";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //reset the person data before the WF run
//        //    phoenixPlatformServiceHelper.UpdatePersonRecord(regardingID, "-", "0", "0");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    int numberOfTries = 1;
//        //    while (numberOfTries <= 75)
//        //    {
//        //        var personData = phoenixPlatformServiceHelper.GetPersonByID(regardingID, "middlename", "Telephone3", "CreditorNumber");

//        //        string middleName = (string)personData["middlename"];
//        //        string telephone3 = (string)personData["telephone3"];
//        //        string creditornumber = (string)personData["creditornumber"];

//        //        if (middleName == "Sanders" && telephone3 == "+0351987780" && creditornumber == "13579A")
//        //            return;

//        //        System.Threading.Thread.Sleep(1000);
//        //        numberOfTries++;

//        //        if (numberOfTries >= 75)
//        //            Assert.Fail("Workflow took more than 75 seconds to run");
//        //    }
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7698")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 46 - WF step with 2 inner condition statments")]
//        //public void WorkflowDetailsSection_TestMethod046()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 46";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1);


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID1);
//        //    Assert.AreEqual("WF Testinng - Scenario 46 - Action 1 Activated", descriptionAfterSave);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7699")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 47 - WF step with 2 inner condition statments (2nd if statment not executing)")]
//        //public void WorkflowDetailsSection_TestMethod047()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 46";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 3, 1); //this phone call date should not trigger the 2nd if statment in the workflow


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID1);
//        //    Assert.AreEqual("Sample Description ...", descriptionAfterSave);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7700")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 48 - Multiple Steps validation")]
//        //public void WorkflowDetailsSection_TestMethod048()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 48";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 48 - Action 1 Activated", (string)fields["notes"]);
//        //    Assert.AreEqual("01/02/2019 09:15:30 - 0987654321234", (string)fields["subject"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7701")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 49 - Multiple Steps validation")]
//        //public void WorkflowDetailsSection_TestMethod049()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 48";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime? phoneCallDate = null;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 48 - Action 1 Activated", (string)fields["notes"]);
//        //    Assert.AreEqual("-", (string)fields["subject"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7702")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 50 - If Else statments validation")]
//        //public void WorkflowDetailsSection_TestMethod050()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 50 - 1";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 50 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7703")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 51 - If Else statments validation")]
//        //public void WorkflowDetailsSection_TestMethod051()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 50 - 2";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 50 - Action 2 Activated", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7704")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 52 - Validation for a Business Object Reference field")]
//        //public void WorkflowDetailsSection_TestMethod052()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 52";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);

//        //    Guid jBrazetaResponsibleUSerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");

//        //    Guid adviceActivityCategoryID = new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, jBrazetaResponsibleUSerID, adviceActivityCategoryID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 52 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7705")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 52 - Validation for a Business Object Reference field")]
//        //public void WorkflowDetailsSection_TestMethod053()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 52";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);

//        //    Guid jBrazetaResponsibleUSerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");

//        //    Guid assessmentActivityCategoryID = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, jBrazetaResponsibleUSerID, assessmentActivityCategoryID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7706")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 54 - Validation for a boolean field")]
//        //public void WorkflowDetailsSection_TestMethod054()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 54";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);

//        //    Guid jBrazetaResponsibleUSerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");

//        //    bool InformationByThirdParty = true;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, jBrazetaResponsibleUSerID, null, InformationByThirdParty);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 54 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7707")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 54 - Validation for a boolean field")]
//        //public void WorkflowDetailsSection_TestMethod055()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 54";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);

//        //    Guid jBrazetaResponsibleUSerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");

//        //    bool InformationByThirdParty = false;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, jBrazetaResponsibleUSerID, null, InformationByThirdParty);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7708")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 56 - Validation for a Picklist field")]
//        //public void WorkflowDetailsSection_TestMethod056()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 56";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);

//        //    Guid jBrazetaResponsibleUSerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    bool InformationByThirdParty = false;
//        //    int DirectionID = 1; //incoming

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, jBrazetaResponsibleUSerID,
//        //        null, InformationByThirdParty, DirectionID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 56 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7709")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 56 - Validation for a Picklist field")]
//        //public void WorkflowDetailsSection_TestMethod057()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 56";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);

//        //    Guid jBrazetaResponsibleUSerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    bool InformationByThirdParty = false;
//        //    int DirectionID = 2; //outgoing

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, jBrazetaResponsibleUSerID,
//        //        null, InformationByThirdParty, DirectionID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7710")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 60 - Validation for a Date field")]
//        //public void WorkflowDetailsSection_TestMethod060()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 60";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);

//        //    Guid jBrazetaResponsibleUSerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    bool InformationByThirdParty = false;
//        //    int DirectionID = 1; //incoming

//        //    bool IsSignificantEvent = true;
//        //    DateTime SignificantEventDate = new DateTime(2019, 3, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, jBrazetaResponsibleUSerID,
//        //        null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 60 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7711")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 60 - Validation for a Date field")]
//        //public void WorkflowDetailsSection_TestMethod061()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 60";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");
//        //    DateTime phoneCallDate = new DateTime(2019, 2, 1, 9, 15, 30);

//        //    Guid jBrazetaResponsibleUSerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    bool InformationByThirdParty = false;
//        //    int DirectionID = 1;

//        //    bool IsSignificantEvent = true;
//        //    DateTime SignificantEventDate = new DateTime(2018, 10, 12);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, jBrazetaResponsibleUSerID,
//        //        null, InformationByThirdParty, DirectionID, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7712")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 62 - Validation for a DateTime field")]
//        //public void WorkflowDetailsSection_TestMethod062()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 62";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 4);



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 62 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7713")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 62 - Validation for a DateTime field")]
//        //public void WorkflowDetailsSection_TestMethod063()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 62";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime phoneCallDate = new DateTime(2019, 2, 10);



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7714")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 64 - Validation for a MultiLookup field")]
//        //public void WorkflowDetailsSection_TestMethod064()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 64";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 4);



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 64 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7715")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 64 - Validation for a MultiLookup field")]
//        //public void WorkflowDetailsSection_TestMethod065()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 64";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("a344edda-e33a-e911-a2c5-005056926fe4");
//        //    string regardingName = "Abbott Adrian - (2008-02-29 00:00:00) [QA-CAS-000001-0031779]";

//        //    var personid = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
//        //    var personidName = "Adrian Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 4);



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForCase(
//        //        subject, description, callerID, callerIdTableName, callerIDName,
//        //        recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, personid, personidName, teamID, "CareDirector QA", DateTime.Now.WithoutMilliseconds(), null, null);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7716")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 66 - Validation for a Large Data Textbox field")]
//        //public void WorkflowDetailsSection_TestMethod066()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 66";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 66 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7717")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 66 - Validation for a Large Data Textbox field")]
//        //public void WorkflowDetailsSection_TestMethod067()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 66";
//        //    string description = null;
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(null, (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7718")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 68 - Validation for a Phone field")]
//        //public void WorkflowDetailsSection_TestMethod068()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 68";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 68 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7719")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 68 - Validation for a Phone field")]
//        //public void WorkflowDetailsSection_TestMethod069()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 68";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "9462748";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7720")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod070()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 70";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 11);
//        //    int direction = 1; //Incoming



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, direction);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 70 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7721")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod071()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 70";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 9);
//        //    int direction = 1; //Incoming



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, direction);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7722")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod071_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 70";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 11);
//        //    int direction = 2; //Incoming



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, direction);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7723")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod071_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 70";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 4);
//        //    int direction = 2; //Incoming



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, direction);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7724")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 72 - Validation for 'Or' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod072()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 72";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 11);
//        //    int direction = 1; //Incoming



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, direction);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 72 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7725")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 73 - Validation for 'Or' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod073()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 72";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "234234234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 11);
//        //    int direction = 1; //Incoming



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, direction);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 72 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7726")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 74 - Validation for 'Or' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod074()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 72";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 11);
//        //    int direction = 2; //outgoing



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, direction);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 72 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7727")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 75 - Validation for 'Or' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod075()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 72";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "23424";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 11);
//        //    int direction = 2; //outgoing



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, direction);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7728")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 76 - Condition for related business object")]
//        //public void WorkflowDetailsSection_TestMethod076()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 76";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 76 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7729")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 77 - Condition for related business object")]
//        //public void WorkflowDetailsSection_TestMethod077()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 76";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adrian Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
//        //    string regardingName = "Adrian Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}





//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7730")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 78 - Condition for related business object")]
//        //public void WorkflowDetailsSection_TestMethod078()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 78.1";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 78 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7731")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 79 - Condition for related business object")]
//        //public void WorkflowDetailsSection_TestMethod079()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 78.2";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 78 - Action 2 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7732")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 80 - Condition for related business object")]
//        //public void WorkflowDetailsSection_TestMethod080()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 78.3";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}





//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7733")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 81 - Validate the 'Equals' operator")]
//        //public void WorkflowDetailsSection_TestMethod081()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 81";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
//        //    bool InformationByThirdParty = false;
//        //    bool IsCaseNote = false;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 81 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7734")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 82 - Validate the 'Equals' operator")]
//        //public void WorkflowDetailsSection_TestMethod082()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 81";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
//        //    bool InformationByThirdParty = true;
//        //    bool IsCaseNote = true;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 81 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7735")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 83 - Validate the 'Equals' operator")]
//        //public void WorkflowDetailsSection_TestMethod083()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 81";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
//        //    bool InformationByThirdParty = true;
//        //    bool IsCaseNote = false;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7736")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 83 - Validate the 'Equals' operator")]
//        //public void WorkflowDetailsSection_TestMethod083_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 81";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
//        //    bool InformationByThirdParty = false;
//        //    bool IsCaseNote = true;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7737")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 84 - Validate the 'Does Not Equal' operator")]
//        //public void WorkflowDetailsSection_TestMethod084()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 84";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    Guid jbrazetaUserID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
//        //    bool InformationByThirdParty = false;
//        //    bool IsCaseNote = false;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecalldate, jbrazetaUserID, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 84 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7738")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 84.1 - Validate the 'Does Not Equal' operator (responsible user field is null)")]
//        //public void WorkflowDetailsSection_TestMethod084_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 84";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
//        //    bool InformationByThirdParty = false;
//        //    bool IsCaseNote = false;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecalldate, null, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7739")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 84 - Validate the 'Does Not Equal' operator")]
//        //public void WorkflowDetailsSection_TestMethod085()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 84";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    Guid Workflow_Test_User_1USerID = new Guid("B9B8B4C7-1552-E911-A2C5-005056926FE4");
//        //    DateTime phonecalldate = DateTime.Now.WithoutMilliseconds().Date;
//        //    bool InformationByThirdParty = false;
//        //    bool IsCaseNote = false;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phonecalldate, Workflow_Test_User_1USerID, null, InformationByThirdParty, 1, false, null, null, IsCaseNote);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7740")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 86 - Validate the 'contains data' operator")]
//        //public void WorkflowDetailsSection_TestMethod086()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 86";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = DateTime.Now.WithoutMilliseconds().Date;
//        //    bool IsSignificantEvent = true;
//        //    DateTime SignificantEventDate = new DateTime(2019, 3, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 86 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7741")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 87 - Validate the 'contains data' operator")]
//        //public void WorkflowDetailsSection_TestMethod087()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 86";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7742")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 88 - Validate the 'does not contains data' operator")]
//        //public void WorkflowDetailsSection_TestMethod088()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 88";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? phoneCallDate = null;



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 88 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7743")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 89 - Validate the 'does not contains data' operator")]
//        //public void WorkflowDetailsSection_TestMethod089()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 88";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = DateTime.Now.WithoutMilliseconds();



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7744")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 90 - Validate the 'In' operator")]
//        //public void WorkflowDetailsSection_TestMethod090()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 90";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("DA2FAF81-C7CE-40CD-A2B6-7D056A88C317");
//        //    string regardingName = "Maureen Wheeler";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? phoneCallDate = null;



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 90 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7745")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 91 - Validate the 'In' operator")]
//        //public void WorkflowDetailsSection_TestMethod091()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 90";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("E6795D93-73DB-4797-B8DE-79F176838111");
//        //    string regardingName = "Ruben Stein";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? phoneCallDate = null;



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 90 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7746")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 92 - Validate the 'In' operator")]
//        //public void WorkflowDetailsSection_TestMethod092()
//        //{
//        //    Assert.Inconclusive("Condition 'In' was removed from the WF condition builder. not sure if it will be added in the future.");

//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 90";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
//        //    string regardingName = "Adrian Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? phoneCallDate = null;



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 90.2 - Validate the 'In' operator with only 1 option inside")]
//        //public void WorkflowDetailsSection_TestMethod090_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 90 (2)";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("DA2FAF81-C7CE-40CD-A2B6-7D056A88C317");
//        //    string regardingName = "Maureen Wheeler";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? phoneCallDate = null;



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("<p>WF Testinng - Scenario 90 - Action 2&nbsp;Activated</p>", (string)fields["notes"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 90.2 - Validate the 'In' operator with only 1 option inside")]
//        //public void WorkflowDetailsSection_TestMethod090_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 90 (2)";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("E6795D93-73DB-4797-B8DE-79F176838111");
//        //    string regardingName = "Ruben Stein";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? phoneCallDate = null;



//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7747")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 93 - Validate the 'Is Current User' operator")]
//        //public void WorkflowDetailsSection_TestMethod093()
//        //{
//        //    Assert.Inconclusive("Condition 'Is Current User' was removed from the WF condition builder. not sure if it will be added in the future.");

//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 93";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = DateTime.Now.WithoutMilliseconds().Date;
//        //    Guid Workflow_Test_User_0UserID = new Guid("3D17AF27-F74E-E911-9C54-F8B156AF4F99");


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, Workflow_Test_User_0UserID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 93 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7748")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 94 - Validate the 'Is Current User' operator")]
//        //public void WorkflowDetailsSection_TestMethod094()
//        //{
//        //    Assert.Inconclusive("Condition 'Is Current User' was removed from the WF condition builder. not sure if it will be added in the future.");

//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 93";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = DateTime.Now.WithoutMilliseconds().Date;
//        //    Guid Workflow_Test_User_1UserID = new Guid("B9B8B4C7-1552-E911-A2C5-005056926FE4");


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, Workflow_Test_User_1UserID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7749")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 95 - Validate the 'Equals Current User´s Team Members' operator")]
//        //public void WorkflowDetailsSection_TestMethod095()
//        //{
//        //    Assert.Inconclusive("Condition 'Equals Current User´s Team Members' was removed from the WF condition builder. not sure if it will be added in the future.");

//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 95";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = DateTime.Now.WithoutMilliseconds().Date;
//        //    Guid Workflow_Test_User_1UserID = new Guid("B9B8B4C7-1552-E911-A2C5-005056926FE4"); //user Workflow_Test_User_1 belongs to Workflow_Test_User_0 team members (Workflow_Test_User_0 is the one triggering the WF)


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, Workflow_Test_User_1UserID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 95 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7750")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 96 - Validate the 'Equals Current User´s Team Members' operator")]
//        //public void WorkflowDetailsSection_TestMethod096()
//        //{
//        //    Assert.Inconclusive("Condition 'Equals Current User´s Team Members' was removed from the WF condition builder. not sure if it will be added in the future.");

//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 95";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = DateTime.Now.WithoutMilliseconds().Date;
//        //    Guid Workflow_Test_User_2UserID = new Guid("7F1BF714-6B4A-E911-A2C4-0050569231CF"); //user Workflow_Test_User_2 DO NOT belong to Workflow_Test_User_0 team members (Workflow_Test_User_0 is the one triggering the WF)


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, Workflow_Test_User_2UserID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7751")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 97 - Validate the 'Like' operator")]
//        //public void WorkflowDetailsSection_TestMethod097()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 97";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 97 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7752")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 97.1 - Validate the 'Like' operator")]
//        //public void WorkflowDetailsSection_TestMethod097_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 97";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "000098765432123444444";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 97 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7753")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 98 - Validate the 'Like' operator")]
//        //public void WorkflowDetailsSection_TestMethod098()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 97";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "123456";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7754")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 98.1 - Validate the 'Like' operator")]
//        //public void WorkflowDetailsSection_TestMethod098_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 97";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7755")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 99 - Validate the 'Starts With' operator")]
//        //public void WorkflowDetailsSection_TestMethod099()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 99";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 99 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7756")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 100 - Validate the 'Starts With' operator")]
//        //public void WorkflowDetailsSection_TestMethod100()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 99";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0986654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7757")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 100.1 - Validate the 'Starts With' operator")]
//        //public void WorkflowDetailsSection_TestMethod100_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 99";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7758")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 101 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod101()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 6);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 101 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7759")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 101.1 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod101_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 7);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 101 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7760")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 101.2 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod101_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 8);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 101 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7761")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 101.2 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod101_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? PhoneCallDate = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7762")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 102 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod102()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 9);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7763")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 102.1 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod102_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 2, 28);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7764")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 102.2 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod102_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? PhoneCallDate = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7765")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 103 - Validate the 'Is Grated Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod103()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 103";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 26);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 103 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7766")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 104 - Validate the 'Is Grated Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod104()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 103";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 25);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7767")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 103.1 - Validate the 'Is Grated Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod104_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 103";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 24);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7768")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 103.1 - Validate the 'Is Grated Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod104_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 103";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? PhoneCallDate = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7769")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 105 - Validate the 'In Future' operator")]
//        //public void WorkflowDetailsSection_TestMethod105()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 105";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 105 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7770")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 106 - Validate the 'In Future' operator")]
//        //public void WorkflowDetailsSection_TestMethod106()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 105";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7771")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 106.1 - Validate the 'In Future' operator")]
//        //public void WorkflowDetailsSection_TestMethod106_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 105";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? PhoneCallDate = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7772")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 107 - Validate the 'Next N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod107()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 107";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 107 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7773")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 107.1 - Validate the 'Next N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod107_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 107";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(2).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 107 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7774")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 108 - Validate the 'Next N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod108()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 107";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(4).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7775")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 108.1 - Validate the 'Next N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod108_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 107";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7776")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 108.2 - Validate the 'Next N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod108_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 107";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? PhoneCallDate = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7777")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 109 - Validate the 'Last N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod109()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 109";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddMonths(-1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 109 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7778")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 109.1 - Validate the 'Last N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod109_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 109";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddMonths(-2).AddDays(1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 109 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7779")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 109.2 - Validate the 'Last N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod109_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 109";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().Date.AddMonths(-2).AddDays(-1);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7780")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 109.3 - Validate the 'Last N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod109_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 109";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7781")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 109.4 - Validate the 'Last N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod109_4()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 109";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? PhoneCallDate = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7782")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 111 - Validate the 'Older Than N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod111()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 111";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddYears(-2).AddDays(-1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 111 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7783")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 112 - Validate the 'Older Than N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod112()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 111";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddYears(-2).AddDays(1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7784")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 112.1 - Validate the 'Older Than N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod112_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 111";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7785")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 112.2 - Validate the 'Older Than N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod112_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 111";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? PhoneCallDate = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7786")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 113 - Validate the 'Today' operator")]
//        //public void WorkflowDetailsSection_TestMethod113()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 113";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().Date.AddHours(8);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 113 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7787")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 114 - Validate the 'Today' operator")]
//        //public void WorkflowDetailsSection_TestMethod114()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 113";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7788")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 114.1 - Validate the 'Today' operator")]
//        //public void WorkflowDetailsSection_TestMethod114_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 113";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date.AddHours(7);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7789")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 114.2 - Validate the 'Today' operator")]
//        //public void WorkflowDetailsSection_TestMethod114_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 113";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? PhoneCallDate = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7790")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 115 - Validate the 'Not Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod115()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 115";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 11);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 115 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7791")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 115.1 - Validate the 'Not Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod115_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 115";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 15);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 115 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7792")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 116 - Validate the 'Not Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod116()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 115";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 12);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7793")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 116.1 - Validate the 'Not Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod116_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 115";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 13);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7794")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 116.2 - Validate the 'Not Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod116_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 115";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 14);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7795")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 116.3 - Validate the 'Not Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod116_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 115";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? PhoneCallDate = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7796")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 117 - Validate the 'Is Grated Than Or Equal To' operator")]
//        //public void WorkflowDetailsSection_TestMethod117()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 117";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 14);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 117 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7797")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 118 - Validate the 'Is Grated Than Or Equal To' operator")]
//        //public void WorkflowDetailsSection_TestMethod118()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 117";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 15);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 117 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7798")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 119 - Validate the 'Is Grated Than Or Equal To' operator")]
//        //public void WorkflowDetailsSection_TestMethod119()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 117";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 13);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7799")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 120 - Validate the 'Is Less Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod120()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 120";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 13);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 120 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7800")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 121 - Validate the 'Is Less Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod121()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 120";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 14);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7801")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 121.1 - Validate the 'Is Less Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod121_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 120";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 15);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7802")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 122 - Validate the 'Is Less Than Or Equal To' operator")]
//        //public void WorkflowDetailsSection_TestMethod122()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 122";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 13);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 122 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7803")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 123 - Validate the 'Is Less Than Or Equal To' operator")]
//        //public void WorkflowDetailsSection_TestMethod123()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 122";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 14);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 122 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7804")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 124 - Validate the 'Is Less Than Or Equal To' operator")]
//        //public void WorkflowDetailsSection_TestMethod124()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 122";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 15);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7805")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 125 - Validate the 'In Past' operator")]
//        //public void WorkflowDetailsSection_TestMethod125()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 125";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 125 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7806")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 126 - Validate the 'In Past' operator")]
//        //public void WorkflowDetailsSection_TestMethod126()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 125";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddHours(2);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7807")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 127 - Validate the 'Is Grated Than Today's Date' operator")]
//        //public void WorkflowDetailsSection_TestMethod127()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 127";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 127 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7808")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 128 - Validate the 'Is Grated Than Today's Date' operator")]
//        //public void WorkflowDetailsSection_TestMethod128()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 127";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7809")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 128.1 - Validate the 'Is Grated Than Today's Date' operator")]
//        //public void WorkflowDetailsSection_TestMethod128_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 127";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7810")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 129 - Validate the 'Is Less Than Today's Date' operator")]
//        //public void WorkflowDetailsSection_TestMethod129()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 129";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 129 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7811")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 130 - Validate the 'Is Less Than Today's Date' operator")]
//        //public void WorkflowDetailsSection_TestMethod130()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 129";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds();

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7812")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 131 - Validate the 'Last N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod131()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 131";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 131 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7813")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 132 - Validate the 'Last N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod132()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 131";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-3).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7814")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 132.1 - Validate the 'Last N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod132_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 131";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7815")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 133 - Validate the 'Last N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod133()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 133";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddYears(-1).AddMonths(-6).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 133 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7816")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 134 - Validate the 'Last N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod134()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 133";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddYears(-2).AddMonths(-6).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7817")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 134.1 - Validate the 'Last N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod134_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 133";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7818")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 135 - Validate the 'Next N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod135()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 135";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddMonths(2).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 135 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7819")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 136 - Validate the 'Next N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod136()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 135";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddMonths(4).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7820")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 136.1 - Validate the 'Next N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod136_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 135";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7821")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 137 - Validate the 'Next N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod137()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 137";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddYears(1).AddMonths(6).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 137 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7822")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 138 - Validate the 'Next N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod138()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 137";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddYears(2).AddMonths(1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7823")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 138.1 - Validate the 'Next N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod138_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 137";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1).Date;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7824")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 139 - Validate the 'Older Than N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod139()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 139";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-3);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 139 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7825")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 140 - Validate the 'Older Than N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod140()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 139";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(-1);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7826")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 140.1 - Validate the 'Older Than N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod140_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 139";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7827")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 141 - Validate the 'Older Than N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod141()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 141";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddMonths(-3);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 141 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7828")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 142 - Validate the 'Older Than N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod142()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 141";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddMonths(-1).AddDays(-10);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7829")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 142.1 - Validate the 'Older Than N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod142_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 141";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.WithoutMilliseconds().AddDays(1);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7830")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 143 - Validate the 'Older Than N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod143()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 143";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0997654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Testinng - Scenario 143 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7831")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 144 - Validate the 'Older Than N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod144()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 143";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7832")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 149 - Custom Fields Tool - test Clear action")]
//        //public void WorkflowDetailsSection_TestMethod149()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 149";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0997654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(null, fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7833")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 150 - Custom Fields Tool - extract caller field ")]
//        //public void WorkflowDetailsSection_TestMethod150()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 150";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0997654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Adolfo Abbott", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7834")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 151 - Custom Fields Tool - extract phone number field")]
//        //public void WorkflowDetailsSection_TestMethod151()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 151";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0997654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("0997654321234", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7835")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 152 - Custom Fields Tool - extract phone number field")]
//        //public void WorkflowDetailsSection_TestMethod152()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 151";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual(null, fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7836")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 153 - Custom Fields Tool - Test Default Value")]
//        //public void WorkflowDetailsSection_TestMethod153()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 153";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("No Phone Number", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7837")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 153.1 - Custom Fields Tool - Test Default Value")]
//        //public void WorkflowDetailsSection_TestMethod153_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 153";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("0987654321", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7838")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 154 - Custom Fields Tool - Extract Direction field")]
//        //public void WorkflowDetailsSection_TestMethod154()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 154";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("Incoming", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7839")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 155 - Custom Fields Tool - Extract Phone Call Date field")]
//        //public void WorkflowDetailsSection_TestMethod155()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 155";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("27/03/2019 10:30:00", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7840")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 156 - Custom Fields Tool - Extract Phone Call Date field")]
//        //public void WorkflowDetailsSection_TestMethod156()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 155";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime? phoneCallDate = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual(null, fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7841")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 157 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod157()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 157";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, true);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("Yes", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7842")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 158 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod158()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 158";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);

//        //    DateTime eventDate = new DateTime(2019, 3, 26);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, 1, true, eventDate, eventCathegoryID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("26/03/2019", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7843")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 159 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod159()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 158";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, 1, false, null, null, false);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual(null, fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7844")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 160 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod160()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 160";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);
//        //    bool IsCaseNote = true;
//        //    bool informationByThirdParty = false;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, informationByThirdParty, 1, false, null, null, IsCaseNote);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "InformationByThirdParty");
//        //    Assert.AreEqual(true, (bool)fields["InformationByThirdParty".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7845")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 161 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod161()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 160";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);
//        //    bool IsCaseNote = false;
//        //    bool informationByThirdParty = true;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, informationByThirdParty, 1, false, null, null, IsCaseNote);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "InformationByThirdParty");
//        //    Assert.AreEqual(false, (bool)fields["InformationByThirdParty".ToLower()]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7846")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 162 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod162()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 162";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = new DateTime(2019, 3, 27, 10, 30, 00);
//        //    bool IsCaseNote = false;
//        //    bool informationByThirdParty = false;


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, informationByThirdParty, 1, false, null, null, IsCaseNote);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "iscasenote");
//        //    Assert.AreEqual(true, (bool)fields["iscasenote".ToLower()]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7847")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 163 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod163()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 163";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    Guid Workflow_Test_User_0UserID = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4");
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "ResponsibleUserId");
//        //    Assert.AreEqual(Workflow_Test_User_0UserID, (Guid)fields["ResponsibleUserId".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7848")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 164 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod164()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 164";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    Guid Workflow_Test_User_1UserID = new Guid("B9B8B4C7-1552-E911-A2C5-005056926FE4");
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "ResponsibleUserId");
//        //    Assert.AreEqual(Workflow_Test_User_1UserID, (Guid)fields["ResponsibleUserId".ToLower()]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7849")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 165 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod165()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 165";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("Adolfo Abbott", (string)fields["notes".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7850")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 166 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod166()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 165";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("1ab2f044-0352-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "systemuser";
//        //    string callerIDName = "Workflow Test User 0";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual(null, fields["notes".ToLower()]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7851")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 167 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod167()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 167";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("1ab2f044-0352-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "systemuser";
//        //    string callerIDName = "Workflow Test User 0";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("Workflow Test User 0", (string)fields["notes".ToLower()]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7852")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 168 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod168()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 168";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);


//        //    phoenixPlatformServiceHelper.UpdatePersonAllowEmail(regardingID, true);



//        //    //ACT

//        //    //Create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "IsCaseNote");
//        //    Assert.AreEqual(true, (bool)fields["IsCaseNote".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7853")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 169 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod169()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 168";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adrian Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
//        //    string regardingName = "Adrian Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "IsCaseNote");
//        //    Assert.AreEqual(false, (bool)fields["IsCaseNote".ToLower()]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7854")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 170 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod170()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 170";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "PhoneCallDate");
//        //    Assert.AreEqual(new DateTime(2015, 1, 6), (DateTime)fields["PhoneCallDate".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7855")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 170.1 - Custom Fields Tool - Extract boolean field")]
//        //public void WorkflowDetailsSection_TestMethod170_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 170";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adrian Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
//        //    string regardingName = "Adrian Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "PhoneCallDate");
//        //    Assert.AreEqual(null, fields["PhoneCallDate".ToLower()]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7856")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 171 - Custom Fields Tool - Append to existing")]
//        //public void WorkflowDetailsSection_TestMethod171()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 171";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("Sample Description ...WF Testing - Scenario 171", (string)fields["notes".ToLower()]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7857")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 172 - Custom Fields Tool - Prepend to existing")]
//        //public void WorkflowDetailsSection_TestMethod172()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 172";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("WF Testing - Scenario 172Sample Description ...", (string)fields["notes".ToLower()]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7858")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 173 - Custom Fields Tool - Set to Before")]
//        //public void WorkflowDetailsSection_TestMethod173()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 173";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    DateTime expectedPhoneCallDate = new DateTime(2015, 1, 6).AddMonths(-2).AddDays(-3).AddHours(-4).AddMinutes(-5);
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "PhoneCallDate");
//        //    Assert.AreEqual(expectedPhoneCallDate, (DateTime)fields["PhoneCallDate".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7859")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 173.1 - Custom Fields Tool - Set to Before (person has no DOB)")]
//        //public void WorkflowDetailsSection_TestMethod173_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 173";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adrian Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
//        //    string regardingName = "Adrian Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "PhoneCallDate");
//        //    Assert.AreEqual(null, fields["PhoneCallDate".ToLower()]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7860")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 174 - Custom Fields Tool - Set to After")]
//        //public void WorkflowDetailsSection_TestMethod174()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 174";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    DateTime expectedPhoneCallDate = new DateTime(2015, 1, 6).AddMonths(2).AddDays(3).AddHours(4).AddMinutes(5);
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "PhoneCallDate");
//        //    Assert.AreEqual(expectedPhoneCallDate, (DateTime)fields["PhoneCallDate".ToLower()]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7861")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 174.1 - Custom Fields Tool - Set to After")]
//        //public void WorkflowDetailsSection_TestMethod174_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 174";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adrian Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("1b18dee7-e41e-464b-afe4-50070253de4b");
//        //    string regardingName = "Adrian Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "PhoneCallDate");
//        //    Assert.AreEqual(null, fields["PhoneCallDate".ToLower()]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7862")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 175 - Custom Fields Tool - Set to Concatenation result")]
//        //public void WorkflowDetailsSection_TestMethod175()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 175";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("Residential - CareDirector QA", (string)fields["notes".ToLower()]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7863")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 176 - Custom Fields Tool - Increment By")]
//        //public void WorkflowDetailsSection_TestMethod176()
//        //{
//        //    //ARRANGE 
//        //    string title = "Height And Weight Observation for Adolfo Abbott created by Workflow Test User 0 on " + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");

//        //    Guid personID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    DateTime dateTimeTake = new DateTime(2019, 4, 1);
//        //    int AgeAtTimeTaken = 4;

//        //    int WeightStones = 10;
//        //    int WeightPounds = 20;
//        //    int WeightOunces = 15;
//        //    decimal WeightKilos = 73;

//        //    int HeightFeet = 6;
//        //    int HeightInches = 1;
//        //    decimal HeightMetres = 1.73M;

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    string additionalComments = "WF Testing - Scenario 176";

//        //    //get all PersonHeightAndWeight records for the person
//        //    List<Guid> PersonHeightAndWeightIDs = phoenixPlatformServiceHelper.GetPersonHeightAndWeightForPersonRecord(personID);


//        //    //Delete the records
//        //    foreach (Guid PersonHeightAndWeightId in PersonHeightAndWeightIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonHeightAndWeight(PersonHeightAndWeightId);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid PersonHeightAndWeightID1 = phoenixPlatformServiceHelper.CreatePersonHeightAndWeight(title, personID, dateTimeTake, AgeAtTimeTaken,
//        //        WeightStones, WeightPounds, WeightOunces, WeightKilos,
//        //        HeightFeet, HeightInches, HeightMetres,
//        //        teamID, additionalComments);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPersonHeightAndWeightByID(PersonHeightAndWeightID1, "WeightStones");
//        //    Assert.AreEqual(30, (int)fields["WeightStones".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7864")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 176.1 - Custom Fields Tool - Increment By")]
//        //public void WorkflowDetailsSection_TestMethod176_1()
//        //{
//        //    //ARRANGE 
//        //    string title = "Height And Weight Observation for Adolfo Abbott created by Workflow Test User 0 on " + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");

//        //    Guid personID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    DateTime dateTimeTake = new DateTime(2019, 4, 1);
//        //    int AgeAtTimeTaken = 4;

//        //    int WeightStones = 10;
//        //    int? WeightPounds = null;
//        //    int WeightOunces = 15;
//        //    decimal WeightKilos = 73;

//        //    int HeightFeet = 6;
//        //    int HeightInches = 1;
//        //    decimal HeightMetres = 1.73M;

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    string additionalComments = "WF Testing - Scenario 176";

//        //    //get all PersonHeightAndWeight records for the person
//        //    List<Guid> PersonHeightAndWeightIDs = phoenixPlatformServiceHelper.GetPersonHeightAndWeightForPersonRecord(personID);


//        //    //Delete the records
//        //    foreach (Guid PersonHeightAndWeightId in PersonHeightAndWeightIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonHeightAndWeight(PersonHeightAndWeightId);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid PersonHeightAndWeightID1 = phoenixPlatformServiceHelper.CreatePersonHeightAndWeight(title, personID, dateTimeTake, AgeAtTimeTaken,
//        //        WeightStones, WeightPounds, WeightOunces, WeightKilos,
//        //        HeightFeet, HeightInches, HeightMetres,
//        //        teamID, additionalComments);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPersonHeightAndWeightByID(PersonHeightAndWeightID1, "WeightStones");
//        //    Assert.AreEqual(10, (int)fields["WeightStones".ToLower()]);
//        //}





//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7865")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 177 - Custom Fields Tool - Decrement By")]
//        //public void WorkflowDetailsSection_TestMethod177()
//        //{
//        //    //ARRANGE 
//        //    string title = "Height And Weight Observation for Adolfo Abbott created by Workflow Test User 0 on " + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");

//        //    Guid personID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    DateTime dateTimeTake = new DateTime(2019, 4, 1);
//        //    int AgeAtTimeTaken = 4;

//        //    int WeightStones = 25;
//        //    int WeightPounds = 5;
//        //    int WeightOunces = 15;
//        //    decimal WeightKilos = 73;

//        //    int HeightFeet = 6;
//        //    int HeightInches = 1;
//        //    decimal HeightMetres = 1.73M;

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    string additionalComments = "WF Testing - Scenario 177";

//        //    //get all PersonHeightAndWeight records for the person
//        //    List<Guid> PersonHeightAndWeightIDs = phoenixPlatformServiceHelper.GetPersonHeightAndWeightForPersonRecord(personID);


//        //    //Delete the records
//        //    foreach (Guid PersonHeightAndWeightId in PersonHeightAndWeightIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonHeightAndWeight(PersonHeightAndWeightId);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid PersonHeightAndWeightID1 = phoenixPlatformServiceHelper.CreatePersonHeightAndWeight(title, personID, dateTimeTake, AgeAtTimeTaken,
//        //        WeightStones, WeightPounds, WeightOunces, WeightKilos,
//        //        HeightFeet, HeightInches, HeightMetres,
//        //        teamID, additionalComments);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPersonHeightAndWeightByID(PersonHeightAndWeightID1, "WeightStones");
//        //    Assert.AreEqual(20, (int)fields["WeightStones".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7866")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 177.1 - Custom Fields Tool - Decrement By")]
//        //public void WorkflowDetailsSection_TestMethod177_1()
//        //{
//        //    //ARRANGE 
//        //    string title = "Height And Weight Observation for Adolfo Abbott created by Workflow Test User 0 on " + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");

//        //    Guid personID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    DateTime dateTimeTake = new DateTime(2019, 4, 1);
//        //    int AgeAtTimeTaken = 4;

//        //    int WeightStones = 25;
//        //    int? WeightPounds = null;
//        //    int WeightOunces = 15;
//        //    decimal WeightKilos = 73;

//        //    int HeightFeet = 6;
//        //    int HeightInches = 1;
//        //    decimal HeightMetres = 1.73M;

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    string additionalComments = "WF Testing - Scenario 177";

//        //    //get all PersonHeightAndWeight records for the person
//        //    List<Guid> PersonHeightAndWeightIDs = phoenixPlatformServiceHelper.GetPersonHeightAndWeightForPersonRecord(personID);


//        //    //Delete the records
//        //    foreach (Guid PersonHeightAndWeightId in PersonHeightAndWeightIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonHeightAndWeight(PersonHeightAndWeightId);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid PersonHeightAndWeightID1 = phoenixPlatformServiceHelper.CreatePersonHeightAndWeight(title, personID, dateTimeTake, AgeAtTimeTaken,
//        //        WeightStones, WeightPounds, WeightOunces, WeightKilos,
//        //        HeightFeet, HeightInches, HeightMetres,
//        //        teamID, additionalComments);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPersonHeightAndWeightByID(PersonHeightAndWeightID1, "WeightStones");
//        //    Assert.AreEqual(25, (int)fields["WeightStones".ToLower()]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7867")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 178 - Custom Fields Tool - Multiply By")]
//        //public void WorkflowDetailsSection_TestMethod178()
//        //{
//        //    //ARRANGE 
//        //    string title = "Height And Weight Observation for Adolfo Abbott created by Workflow Test User 0 on " + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");

//        //    Guid personID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    DateTime dateTimeTake = new DateTime(2019, 4, 1);
//        //    int AgeAtTimeTaken = 4;

//        //    int WeightStones = 5;
//        //    int WeightPounds = 5;
//        //    int WeightOunces = 15;
//        //    decimal WeightKilos = 73;

//        //    int HeightFeet = 6;
//        //    int HeightInches = 1;
//        //    decimal HeightMetres = 1.73M;

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    string additionalComments = "WF Testing - Scenario 178";

//        //    //get all PersonHeightAndWeight records for the person
//        //    List<Guid> PersonHeightAndWeightIDs = phoenixPlatformServiceHelper.GetPersonHeightAndWeightForPersonRecord(personID);


//        //    //Delete the records
//        //    foreach (Guid PersonHeightAndWeightId in PersonHeightAndWeightIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonHeightAndWeight(PersonHeightAndWeightId);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid PersonHeightAndWeightID1 = phoenixPlatformServiceHelper.CreatePersonHeightAndWeight(title, personID, dateTimeTake, AgeAtTimeTaken,
//        //        WeightStones, WeightPounds, WeightOunces, WeightKilos,
//        //        HeightFeet, HeightInches, HeightMetres,
//        //        teamID, additionalComments);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPersonHeightAndWeightByID(PersonHeightAndWeightID1, "WeightStones");
//        //    Assert.AreEqual(25, (int)fields["WeightStones".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7868")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 178.1 - Custom Fields Tool - Multiply By")]
//        //public void WorkflowDetailsSection_TestMethod178_1()
//        //{
//        //    //ARRANGE 
//        //    string title = "Height And Weight Observation for Adolfo Abbott created by Workflow Test User 0 on " + DateTime.Now.WithoutMilliseconds().ToString("dd/MM/yyyy hh:mm:ss");

//        //    Guid personID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    DateTime dateTimeTake = new DateTime(2019, 4, 1);
//        //    int AgeAtTimeTaken = 4;

//        //    int WeightStones = 5;
//        //    int? WeightPounds = null;
//        //    int WeightOunces = 15;
//        //    decimal WeightKilos = 73;

//        //    int HeightFeet = 6;
//        //    int HeightInches = 1;
//        //    decimal HeightMetres = 1.73M;

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    string additionalComments = "WF Testing - Scenario 178";

//        //    //get all PersonHeightAndWeight records for the person
//        //    List<Guid> PersonHeightAndWeightIDs = phoenixPlatformServiceHelper.GetPersonHeightAndWeightForPersonRecord(personID);


//        //    //Delete the records
//        //    foreach (Guid PersonHeightAndWeightId in PersonHeightAndWeightIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonHeightAndWeight(PersonHeightAndWeightId);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid PersonHeightAndWeightID1 = phoenixPlatformServiceHelper.CreatePersonHeightAndWeight(title, personID, dateTimeTake, AgeAtTimeTaken,
//        //        WeightStones, WeightPounds, WeightOunces, WeightKilos,
//        //        HeightFeet, HeightInches, HeightMetres,
//        //        teamID, additionalComments);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPersonHeightAndWeightByID(PersonHeightAndWeightID1, "WeightStones");
//        //    Assert.AreEqual(0, (int)fields["WeightStones".ToLower()]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7869")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 179 - Custom Fields Tool - Test to the 'FirstNotNull' behaviour")]
//        //public void WorkflowDetailsSection_TestMethod179()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 179";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";
//        //    DateTime phoneCallDate = DateTime.Now.WithoutMilliseconds();

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("0987654321", (string)fields["notes".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7870")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 180 - Custom Fields Tool - Test to the 'FirstNotNull' behaviour")]
//        //public void WorkflowDetailsSection_TestMethod180()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 179";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";
//        //    DateTime? phoneCallDate = null;

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("0987654321", (string)fields["notes".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7871")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 181 - Custom Fields Tool - Test to the 'FirstNotNull' behaviour")]
//        //public void WorkflowDetailsSection_TestMethod181()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 179";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "";
//        //    DateTime? phoneCallDate = DateTime.UtcNow.WithoutMilliseconds();

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual(phoneCallDate.Value.ToString(), (string)fields["notes".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7872")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 182 - Custom Fields Tool - Test to the 'FirstNotNull' behaviour")]
//        //public void WorkflowDetailsSection_TestMethod182()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 179";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "";
//        //    DateTime? phoneCallDate = null;

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "notes");
//        //    Assert.AreEqual("Sample Description ...", fields["notes".ToLower()]);
//        //}


//        #region Bugs

//        ///// <summary>
//        ///// When we create an update action in a workflow some fields are filled by default.
//        ///// In the case of Phone Call records all bolean fields and the Direction field (picklist) have values by default.
//        ///// This means even that we are forced to update those fields even if we don´t want to.
//        ///// If a WF action is triggered all booleans will be changed to the default values ("No"), and the Direction field will be changed to "Incoming"
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7881")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Bug 1 - Validate the fix for the bug that forced updates on boolean fields, even when those fields were not meant to be updated in the WF Update action")]
//        //public void WorkflowDetailsSection_Bugs_TestMethod1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Bug 1";
//        //    string description = "Sample Description ...";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = DateTime.Now.WithoutMilliseconds().Date;
//        //    bool informationByThirdParty = true;
//        //    bool isCaseNote = true;
//        //    bool isSignificantEvent = true;
//        //    DateTime SignificantEventDate = new DateTime(2019, 3, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);


//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }



//        //    //ACT

//        //    //create the records
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, informationByThirdParty, 2, isSignificantEvent, SignificantEventDate, eventCathegoryID, isCaseNote);


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "DirectionId", "InformationByThirdParty", "IsCaseNote", "IsSignificantEvent", "SignificantEventDate", "SignificantEventCategoryId");
//        //    Assert.AreEqual(2, (int)fields["DirectionId".ToLower()]);
//        //    Assert.AreEqual(true, (bool)fields["InformationByThirdParty".ToLower()]);
//        //    Assert.AreEqual(true, (bool)fields["IsCaseNote".ToLower()]);
//        //    Assert.AreEqual(true, (bool)fields["IsSignificantEvent".ToLower()]);
//        //    Assert.AreEqual(SignificantEventDate, (DateTime)fields["SignificantEventDate".ToLower()]);
//        //    Assert.AreEqual(eventCathegoryID, (Guid)fields["SignificantEventCategoryId".ToLower()]);
//        //}



//        ///// <summary>
//        ///// There was an issue identified by Andy when he created a workkflow in the test environment to automatically create Case records when a user create a Person Contact
//        ///// Andy workflow was called "Contact to Case auto complete"
//        ///// 
//        ///// If Contact Status Equals [Accepted] And Contact Outcome Equals [Community Outcome] And Date/Time Contact Accepted Contains Data Then
//        /////         Create Record: 'Case'
//        ///// 
//        ///// "Description" : "when contact is accepted it creates the case record"
//        ///// 
//        ///// 
//        ///// 
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7882")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Bug 2 - Validate the fix for the bugs identified by Andy - WF should create a case record when a new person contact is created in the system")]
//        //public void WorkflowDetailsSection_Bugs_TestMethod2_1()
//        //{
//        //    //ARRANGE 

//        //    Guid ownerID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid ContactTypeId = phoenixPlatformServiceHelper.GetContactTypeByName("First Response")[0];
//        //    Guid ContactSourceId = phoenixPlatformServiceHelper.GetContactSourceByName("Family")[0];
//        //    Guid ContactReasonId = phoenixPlatformServiceHelper.GetContactReasonByName("Ongoing Psychosis")[0];
//        //    Guid ContactPresentingPriorityId = phoenixPlatformServiceHelper.GetContactPresentingPriorityByName("Other")[0];
//        //    Guid ContactStatusId = phoenixPlatformServiceHelper.GetContactStatusByName("Accept First Response Referral")[0];
//        //    Guid ContactOutcomeId = phoenixPlatformServiceHelper.GetContactOutcomeByName("Community Outcome")[0];
//        //    Guid ReferralPriorityId = phoenixPlatformServiceHelper.GetReferralPriorityByName("Routine")[0];

//        //    Guid RegardingId = new Guid("CB514D78-B0F2-4C79-8E6C-0A2044DC56B1");
//        //    string regardingidtablename = "person";
//        //    string regardingidname = "Clay Abbott";

//        //    Guid ContactMadeById = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4");
//        //    string contactmadebyidtablename = "systemuser";
//        //    string contactmadebyidname = "Workflow Test User 0";

//        //    string ContactMadeByFreetext = "ContactMadeByFreetext ...";
//        //    Guid CommunityAndClinicTeamId = new Guid("F9D45BC4-1155-E911-A2C5-005056926FE4"); //Appointment Team

//        //    string PresentingNeed = "pn ....";
//        //    string AdditionalInformation = "ai ...";
//        //    string ContactSummary = "WF Testing - Bug 2";

//        //    DateTime contactRecievedDateTime = DateTime.Now.WithoutMilliseconds().AddHours(-8);
//        //    DateTime contactAssignedDateTime = contactRecievedDateTime.AddHours(3);
//        //    DateTime contactAcceptedDateTime = contactAssignedDateTime.AddHours(3);


//        //    /*remove all cases for the person*/
//        //    foreach (Guid CaseId in phoenixPlatformServiceHelper.GetCasesForPerson(RegardingId))
//        //    {
//        //        foreach (var caseinvolvementid in phoenixPlatformServiceHelper.GetCaseInvolvementsForCase(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeleteCaseInvolvement(caseinvolvementid);
//        //        }

//        //        foreach (var casestatushistoryid in phoenixPlatformServiceHelper.GetCaseStatusHistoryForCase(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeleteCaseStatusHistory(casestatushistoryid);
//        //        }

//        //        foreach (var caseformid in phoenixPlatformServiceHelper.GetCaseFormByCaseID(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeleteCaseForm(caseformid);
//        //        }

//        //        phoenixPlatformServiceHelper.DeleteCase(CaseId);
//        //    }


//        //    //ACT
//        //    Guid contactID = phoenixPlatformServiceHelper.CreateContact(ownerID, ContactTypeId, ContactSourceId, ContactReasonId, ContactPresentingPriorityId, ContactStatusId, ContactOutcomeId, ReferralPriorityId,
//        //        RegardingId, regardingidtablename, regardingidname,
//        //        ContactMadeById, contactmadebyidtablename, contactmadebyidname, ContactMadeByFreetext, CommunityAndClinicTeamId,
//        //        PresentingNeed, AdditionalInformation, ContactSummary,
//        //        contactRecievedDateTime, contactAcceptedDateTime, contactAssignedDateTime);


//        //    //ASSERT
//        //    List<Guid> cases = phoenixPlatformServiceHelper.GetCasesForPerson(RegardingId);
//        //    Assert.AreEqual(1, cases.Count());

//        //    var caseFields = phoenixPlatformServiceHelper.GetCaseById(cases[0], "OwnerId", "PersonId", "InitialContactId", "ContactReceivedDateTime", "ContactReceivedById", "RequestReceivedDateTime", "ResponsibleUserId", "ContactReasonId", "PresentingPriorityId", "PresentingNeedDetails", "AdditionalInformation", "ContactSourceId", "ContactMadeById", "ContactMadeByIdTableName", "ContactMadeByIdName", "ContactMadeByName", "PersonAwareOfContactId", "NextOfKinAwareOfContactId", "communityandclinicteamid", "ServiceTypeRequestedId", "CaseStatusId");

//        //    Assert.AreEqual(ownerID, (Guid)caseFields["OwnerId".ToLower()]);
//        //    Assert.AreEqual(RegardingId, (Guid)caseFields["PersonId".ToLower()]);
//        //    Assert.AreEqual(contactID, (Guid)caseFields["InitialContactId".ToLower()]);
//        //    //Assert.AreEqual(, (Guid)caseFields["ContactReceivedDateTime".ToLower()]);
//        //    Assert.AreEqual(ContactMadeById, (Guid)caseFields["ContactReceivedById".ToLower()]);
//        //    Assert.AreEqual(contactAcceptedDateTime.ToString(), ((DateTime)caseFields["RequestReceivedDateTime".ToLower()]).ToLocalTime().ToString());

//        //    Assert.AreEqual(ContactMadeById, (Guid)caseFields["ResponsibleUserId".ToLower()]);
//        //    Assert.AreEqual(ContactReasonId, (Guid)caseFields["ContactReasonId".ToLower()]);
//        //    Assert.AreEqual(ContactPresentingPriorityId, (Guid)caseFields["PresentingPriorityId".ToLower()]);
//        //    Assert.AreEqual(PresentingNeed, (string)caseFields["PresentingNeedDetails".ToLower()]);
//        //    Assert.AreEqual("WF Testinng - Bug 2 - Action 1 Activated", (string)caseFields["AdditionalInformation".ToLower()]);

//        //    Assert.AreEqual(ContactSourceId, (Guid)caseFields["ContactSourceId".ToLower()]);
//        //    Assert.AreEqual(ContactMadeById, (Guid)caseFields["ContactMadeById".ToLower()]);
//        //    Assert.AreEqual(contactmadebyidtablename, (string)caseFields["ContactMadeByIdTableName".ToLower()]);
//        //    Assert.AreEqual(contactmadebyidname, (string)caseFields["ContactMadeByIdName".ToLower()]);
//        //    Assert.AreEqual(ContactMadeByFreetext, (string)caseFields["ContactMadeByName".ToLower()]);

//        //    Assert.AreEqual(1, (int)caseFields["PersonAwareOfContactId".ToLower()]);
//        //    Assert.AreEqual(1, (int)caseFields["NextOfKinAwareOfContactId".ToLower()]);

//        //    Assert.AreEqual(CommunityAndClinicTeamId, (Guid)caseFields["communityandclinicteamid".ToLower()]);
//        //    Assert.AreEqual(new Guid("AD34DF09-9E50-E911-A2C5-005056926FE4"), (Guid)caseFields["ServiceTypeRequestedId".ToLower()]);
//        //    Assert.AreEqual(new Guid("11FCECFF-42C2-E811-80DC-0050560502CC"), (Guid)caseFields["CaseStatusId".ToLower()]);

//        //}

//        ///// <summary>
//        ///// There was an issue identified by Andy when he created a workkflow in the test environment to automatically create Case records when a user create a Person Contact
//        ///// Andy workflow was called "Contact to Case auto complete"
//        ///// 
//        ///// If Contact Status Equals [Accepted] And Contact Outcome Equals [Community Outcome] And Date/Time Contact Accepted Contains Data Then
//        /////         Create Record: 'Case'
//        ///// 
//        ///// "Description" : "when contact is accepted it creates the case record"
//        ///// 
//        ///// 
//        ///// 
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7883")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Bug 2 - Validate the fix for the bugs identified by Andy - WF should create a case record when a new person contact is created in the system. In this test the ContactMadeById record is set to a Person business object (instead of a system user)")]
//        //public void WorkflowDetailsSection_Bugs_TestMethod2_2()
//        //{
//        //    //ARRANGE 
//        //    Guid workflow_test_user_0_UserID = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4");

//        //    Guid ownerID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid ContactTypeId = phoenixPlatformServiceHelper.GetContactTypeByName("First Response")[0];
//        //    Guid ContactSourceId = phoenixPlatformServiceHelper.GetContactSourceByName("Family")[0];
//        //    Guid ContactReasonId = phoenixPlatformServiceHelper.GetContactReasonByName("Ongoing Psychosis")[0];
//        //    Guid ContactPresentingPriorityId = phoenixPlatformServiceHelper.GetContactPresentingPriorityByName("Other")[0];
//        //    Guid ContactStatusId = phoenixPlatformServiceHelper.GetContactStatusByName("Accept First Response Referral")[0];
//        //    Guid ContactOutcomeId = phoenixPlatformServiceHelper.GetContactOutcomeByName("Community Outcome")[0];
//        //    Guid ReferralPriorityId = phoenixPlatformServiceHelper.GetReferralPriorityByName("Routine")[0];

//        //    Guid RegardingId = new Guid("CB514D78-B0F2-4C79-8E6C-0A2044DC56B1");
//        //    string regardingidtablename = "person";
//        //    string regardingidname = "Clay Abbott";

//        //    Guid ContactMadeById = new Guid("CB514D78-B0F2-4C79-8E6C-0A2044DC56B1");
//        //    string contactmadebyidtablename = "person";
//        //    string contactmadebyidname = "Clay Abbott";

//        //    string ContactMadeByFreetext = "ContactMadeByFreetext ...";
//        //    Guid CommunityAndClinicTeamId = new Guid("F9D45BC4-1155-E911-A2C5-005056926FE4"); //Appointment Team

//        //    string PresentingNeed = "pn ....";
//        //    string AdditionalInformation = "ai ...";
//        //    string ContactSummary = "WF Testing - Bug 2";

//        //    DateTime contactRecievedDateTime = DateTime.Now.WithoutMilliseconds().AddHours(-8);
//        //    DateTime contactAssignedDateTime = contactRecievedDateTime.AddHours(3);
//        //    DateTime contactAcceptedDateTime = contactAssignedDateTime.AddHours(3);


//        //    /*remove all cases for the person*/
//        //    foreach (Guid CaseId in phoenixPlatformServiceHelper.GetCasesForPerson(RegardingId))
//        //    {
//        //        foreach (var caseinvolvementid in phoenixPlatformServiceHelper.GetCaseInvolvementsForCase(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeleteCaseInvolvement(caseinvolvementid);
//        //        }

//        //        foreach (var casestatushistoryid in phoenixPlatformServiceHelper.GetCaseStatusHistoryForCase(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeleteCaseStatusHistory(casestatushistoryid);
//        //        }

//        //        foreach (var caseformid in phoenixPlatformServiceHelper.GetCaseFormByCaseID(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeleteCaseForm(caseformid);
//        //        }

//        //        phoenixPlatformServiceHelper.DeleteCase(CaseId);
//        //    }


//        //    //ACT
//        //    Guid contactID = phoenixPlatformServiceHelper.CreateContact(ownerID, ContactTypeId, ContactSourceId, ContactReasonId, ContactPresentingPriorityId, ContactStatusId, ContactOutcomeId, ReferralPriorityId,
//        //        RegardingId, regardingidtablename, regardingidname,
//        //        ContactMadeById, contactmadebyidtablename, contactmadebyidname, ContactMadeByFreetext, CommunityAndClinicTeamId,
//        //        PresentingNeed, AdditionalInformation, ContactSummary,
//        //        contactRecievedDateTime, contactAcceptedDateTime, contactAssignedDateTime);


//        //    //ASSERT
//        //    List<Guid> cases = phoenixPlatformServiceHelper.GetCasesForPerson(RegardingId);
//        //    Assert.AreEqual(1, cases.Count());

//        //    var caseFields = phoenixPlatformServiceHelper.GetCaseById(cases[0], "OwnerId", "PersonId", "InitialContactId", "ContactReceivedDateTime", "ContactReceivedById", "RequestReceivedDateTime", "ResponsibleUserId", "ContactReasonId", "PresentingPriorityId", "PresentingNeedDetails", "AdditionalInformation", "ContactSourceId", "ContactMadeById", "ContactMadeByIdTableName", "ContactMadeByIdName", "ContactMadeByName", "PersonAwareOfContactId", "NextOfKinAwareOfContactId", "communityandclinicteamid", "ServiceTypeRequestedId", "CaseStatusId");

//        //    Assert.AreEqual(ownerID, (Guid)caseFields["OwnerId".ToLower()]);
//        //    Assert.AreEqual(RegardingId, (Guid)caseFields["PersonId".ToLower()]);
//        //    Assert.AreEqual(contactID, (Guid)caseFields["InitialContactId".ToLower()]);
//        //    //Assert.AreEqual(, (Guid)caseFields["ContactReceivedDateTime".ToLower()]);
//        //    Assert.AreEqual(workflow_test_user_0_UserID, (Guid)caseFields["ContactReceivedById".ToLower()]);
//        //    Assert.AreEqual(contactAcceptedDateTime.ToString(), ((DateTime)caseFields["RequestReceivedDateTime".ToLower()]).ToLocalTime().ToString());

//        //    Assert.AreEqual(workflow_test_user_0_UserID, (Guid)caseFields["ResponsibleUserId".ToLower()]);
//        //    Assert.AreEqual(ContactReasonId, (Guid)caseFields["ContactReasonId".ToLower()]);
//        //    Assert.AreEqual(ContactPresentingPriorityId, (Guid)caseFields["PresentingPriorityId".ToLower()]);
//        //    Assert.AreEqual(PresentingNeed, (string)caseFields["PresentingNeedDetails".ToLower()]);
//        //    Assert.AreEqual("WF Testinng - Bug 2 - Action 1 Activated", (string)caseFields["AdditionalInformation".ToLower()]);

//        //    Assert.AreEqual(ContactSourceId, (Guid)caseFields["ContactSourceId".ToLower()]);
//        //    Assert.AreEqual(null, caseFields["ContactMadeById".ToLower()]);
//        //    Assert.AreEqual(null, caseFields["ContactMadeByIdTableName".ToLower()]);
//        //    Assert.AreEqual(null, caseFields["ContactMadeByIdName".ToLower()]);
//        //    Assert.AreEqual(ContactMadeByFreetext, (string)caseFields["ContactMadeByName".ToLower()]);

//        //    Assert.AreEqual(1, (int)caseFields["PersonAwareOfContactId".ToLower()]);
//        //    Assert.AreEqual(1, (int)caseFields["NextOfKinAwareOfContactId".ToLower()]);

//        //    Assert.AreEqual(CommunityAndClinicTeamId, (Guid)caseFields["communityandclinicteamid".ToLower()]);
//        //    Assert.AreEqual(new Guid("AD34DF09-9E50-E911-A2C5-005056926FE4"), (Guid)caseFields["ServiceTypeRequestedId".ToLower()]);
//        //    Assert.AreEqual(new Guid("11FCECFF-42C2-E811-80DC-0050560502CC"), (Guid)caseFields["CaseStatusId".ToLower()]);

//        //}




//        ///// <summary>
//        ///// There was an issue identified by Andy when he created a workkflow in the test environment to automatically create person forms and case actions when a case record was accepted
//        ///// Workflow was async and had conditions who compare integer values
//        ///// 
//        ///// 
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7884")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Bug 3 - Validate the fix for the bugs identified by Andy - Async workflows with conditions that compare integer values were failing")]
//        //public void WorkflowDetailsSection_Bugs_TestMethod3_1()
//        //{
//        //    //ARRANGE 

//        //    Guid PersonId = new Guid("C4AB357E-B8A7-44F4-98DC-2DAA8E4CFD79"); //Dawn Abbott

//        //    //remove all forms for the person
//        //    foreach (var formid in phoenixPlatformServiceHelper.GetPersonFormForPerson(PersonId))
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonForm(formid);
//        //    }

//        //    //remove all case notes for the person
//        //    foreach (var casenoteid in phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(PersonId))
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePersonCaseNote(casenoteid);
//        //    }

//        //    /*remove all cases for the person*/
//        //    foreach (Guid CaseId in phoenixPlatformServiceHelper.GetCasesForPerson(PersonId))
//        //    {
//        //        foreach (var caseinvolvementid in phoenixPlatformServiceHelper.GetCaseInvolvementsForCase(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeleteCaseInvolvement(caseinvolvementid);
//        //        }

//        //        foreach (var casestatushistoryid in phoenixPlatformServiceHelper.GetCaseStatusHistoryForCase(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeleteCaseStatusHistory(casestatushistoryid);
//        //        }

//        //        foreach (var caseActionID in phoenixPlatformServiceHelper.GetCaseActionForCase(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeleteCaseAction(caseActionID);
//        //        }

//        //        foreach (var lacReviewID in phoenixPlatformServiceHelper.GetLACReviewsByCaseID(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeleteLACReview(lacReviewID);
//        //        }

//        //        foreach (var lacCheckID in phoenixPlatformServiceHelper.GetLACChecksByCaseID(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeleteLACCheck(lacCheckID);
//        //        }

//        //        foreach (var lacLegalStatusID in phoenixPlatformServiceHelper.GetPersonLACLegalStatussByCaseID(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeletePersonLACLegalStatus(lacLegalStatusID);
//        //        }

//        //        foreach (var lacEpisode in phoenixPlatformServiceHelper.GetLACEpisodesByCaseID(CaseId))
//        //        {
//        //            phoenixPlatformServiceHelper.DeleteLACEpisode(lacEpisode);
//        //        }

//        //        phoenixPlatformServiceHelper.DeleteCase(CaseId);
//        //    }


//        //    string PresentingNeedDetails = "P N D ....";
//        //    string AdditionalInformation = "WF Testing - Bug 3";
//        //    DateTime StartDateTime = DateTime.Now.WithoutMilliseconds();
//        //    DateTime ContactReceivedDateTime = DateTime.Now.WithoutMilliseconds();
//        //    DateTime RequestReceivedDateTime = DateTime.Now.WithoutMilliseconds();
//        //    DateTime CaseAcceptedDateTime = DateTime.Now.WithoutMilliseconds();
//        //    Guid OwnerId = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team;
//        //    Guid ContactReceivedById = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4"); //workflow test user 0
//        //    Guid CommunityAndClinicTeamId = new Guid("F9D45BC4-1155-E911-A2C5-005056926FE4"); //Appointment Team
//        //    Guid ResponsibleUserId = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4"); //workflow test user 0;
//        //    Guid SecondaryCaseReasonId = phoenixPlatformServiceHelper.GetSecondaryCaseReasonByName("Medication Review")[0];
//        //    Guid CaseStatusId = phoenixPlatformServiceHelper.GetCaseStatusByName("Waiting List - First Appointment Required")[0];
//        //    Guid ContactReasonId = phoenixPlatformServiceHelper.GetContactReasonByName("Advice/Consultation")[0];
//        //    Guid PresentingPriorityId = phoenixPlatformServiceHelper.GetContactPresentingPriorityByName("Other")[0];
//        //    Guid AdministrativeCategoryId = phoenixPlatformServiceHelper.GetContactAdministrativeCategoryByName("Amenity Patient")[0];
//        //    Guid ServiceTypeRequestedId = phoenixPlatformServiceHelper.GetCaseServiceTypeRequestedByName("Advice and Consultation")[0];
//        //    Guid ProfessionalTypeId = phoenixPlatformServiceHelper.GetProfessionTypeByName("Care Coordinatior")[0];
//        //    Guid DataFormId = phoenixPlatformServiceHelper.GetDataFormByName("CommunityHealthCase")[0];
//        //    Guid ContactSourceId = phoenixPlatformServiceHelper.GetContactSourceByName("Family")[0];



//        //    //ACT
//        //    Guid CaseID = phoenixPlatformServiceHelper.CreateCommunityHealthCase(
//        //        PresentingNeedDetails, AdditionalInformation,
//        //        StartDateTime, ContactReceivedDateTime, RequestReceivedDateTime, CaseAcceptedDateTime,
//        //        OwnerId, "CareDirector QA", ContactReceivedById, CommunityAndClinicTeamId, ResponsibleUserId, SecondaryCaseReasonId, "Medication Review", CaseStatusId, "Waiting List - First Appointment Required", ContactReasonId, PresentingPriorityId,
//        //        AdministrativeCategoryId, ServiceTypeRequestedId, PersonId, ProfessionalTypeId, DataFormId, ContactSourceId);





//        //    //ASSERT
//        //    var caseFields = phoenixPlatformServiceHelper.GetCaseById(CaseID, "title");
//        //    string caseTitle = (string)caseFields["title".ToLower()];

//        //    var formIDs = phoenixPlatformServiceHelper.GetPersonFormForPerson(PersonId);
//        //    var caseActions = phoenixPlatformServiceHelper.GetCaseActionForCase(CaseID);
//        //    var caseNotes = phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(CaseID);
//        //    int totalCount = 0;

//        //    while (formIDs.Count < 1 || caseActions.Count < 1 || caseNotes.Count < 1)
//        //    {
//        //        totalCount++;

//        //        if (totalCount > 75)
//        //            Assert.Fail("It took more than 75 seconds for the workflow to execute");

//        //        System.Threading.Thread.Sleep(1000);

//        //        formIDs = phoenixPlatformServiceHelper.GetPersonFormForPerson(PersonId);
//        //        caseActions = phoenixPlatformServiceHelper.GetCaseActionForCase(CaseID);
//        //        caseNotes = phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(PersonId);
//        //    }


//        //    Assert.AreEqual(1, formIDs.Count);
//        //    Assert.AreEqual(1, caseActions.Count);
//        //    Assert.AreEqual(1, caseNotes.Count);




//        //    var personFormFields = phoenixPlatformServiceHelper.GetPersonFormByID(formIDs[0], "documentid", "assessmentstatusid", "startdate", "responsibleuserid", "caseid");
//        //    Assert.AreEqual(new Guid("fa24bd3d-313f-e911-a2c5-005056926fe4"), (Guid)personFormFields["documentid".ToString()]);
//        //    Assert.AreEqual(1, (int)personFormFields["assessmentstatusid".ToString()]);
//        //    Assert.AreEqual(new DateTime(2019, 5, 23), (DateTime)personFormFields["startdate".ToString()]);
//        //    Assert.AreEqual(ResponsibleUserId, (Guid)personFormFields["responsibleuserid".ToString()]);
//        //    Assert.AreEqual(CaseID, (Guid)personFormFields["caseid".ToString()]);



//        //    var caseActionFields = phoenixPlatformServiceHelper.GetCaseActionByID(caseActions[0], "personid", "caseactiontypeid", "duedate", "casepriorityid", "actiondetails", "caseactionstatusid");
//        //    Assert.AreEqual(PersonId, (Guid)caseActionFields["personid".ToString()]);
//        //    Assert.AreEqual(new Guid("a1dce1ba-5c75-e911-a2c5-005056926fe4"), (Guid)caseActionFields["caseactiontypeid".ToString()]);
//        //    Assert.AreEqual(new DateTime(2019, 5, 23), (DateTime)caseActionFields["duedate".ToString()]);
//        //    Assert.AreEqual(null, caseActionFields["casepriorityid".ToString()]);
//        //    string expectedActionDetails = string.Format("{0} <-> {1} <-> {2} <-> {3} <-> {4} <-> {5} <-> {6} <-> {7} <->", caseTitle, "CareDirector QA", "Medication Review", "Yes", ContactReceivedDateTime.ToString(), "", "", "No");
//        //    Assert.AreEqual(expectedActionDetails, (string)caseActionFields["actiondetails".ToString()]);
//        //    Assert.AreEqual(new Guid("ce9752ed-5c75-e911-a2c5-005056926fe4"), (Guid)caseActionFields["caseactionstatusid".ToString()]);



//        //    var casenoteFields = phoenixPlatformServiceHelper.GetPersonCaseNoteByID(caseNotes[0], "subject", "casenotedate");
//        //    Assert.AreEqual(caseTitle + " <-> Waiting List - First Appointment Required", (string)casenoteFields["subject".ToString()]);
//        //    Assert.AreEqual(CaseAcceptedDateTime.ToUniversalTime().ToString(), ((DateTime)casenoteFields["casenotedate".ToString()]).ToUniversalTime().ToString());

//        //}


//        #endregion


//        //[TestProperty("JiraIssueID", "CDV6-7873")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 183 - Testing the latest changes to the to let the users set a lookup field to a MultiSelectMultiLookup field (e.g. set a PersonId to the Email’s “To” field when using the Send Email Activity)")]
//        //public void WorkflowDetailsSection_TestMethod183()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 183";
//        //    string description = "WF Testing - Scenario 183";

//        //    Guid callerID = new Guid("1ab2f044-0352-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "systemuser";
//        //    string callerIDName = "Workflow Test User 0";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emailIDs.Count);

//        //    //get the email info
//        //    var email = phoenixPlatformServiceHelper.GetEmailById(emailIDs[0], "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "EmailFromId");

//        //    Guid Workflow_Test_User_0UserID = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4");

//        //    Assert.AreEqual("WF Testinng - Scenario 183 - Action 1 Activated", (string)email["subject".ToLower()]);
//        //    Assert.AreEqual(Workflow_Test_User_0UserID, (Guid)email["EmailFromId".ToLower()]);

//        //    Assert.AreEqual(regardingID, (Guid)email["RegardingID".ToLower()]);
//        //    Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
//        //    Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);


//        //    List<Guid> emailToID = phoenixPlatformServiceHelper.GetEmailsToByEmailID(emailIDs[0]);
//        //    Assert.AreEqual(1, emailToID.Count);

//        //    var emailTo = phoenixPlatformServiceHelper.GetEmailToById(emailToID[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
//        //    Assert.AreEqual(regardingID, (Guid)emailTo["RegardingId".ToLower()]);
//        //    Assert.AreEqual(regardingName, (string)emailTo["RegardingIdName".ToLower()]);
//        //    Assert.AreEqual("person", (string)emailTo["RegardingIdTableName".ToLower()]);
//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7874")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 184 - Testing the latest changes to the to let the users set a lookup field to a MultiSelectMultiLookup field (e.g. set a PersonId to the Email’s “To” field when using the Send Email Activity). In this scenario the phone call caller field is set to a person record")]
//        //public void WorkflowDetailsSection_TestMethod184_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 184";
//        //    string description = "WF Testing - Scenario 184";

//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emailIDs.Count);

//        //    //get the email info
//        //    var email = phoenixPlatformServiceHelper.GetEmailById(emailIDs[0], "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "EmailFromId");

//        //    Guid Workflow_Test_User_0UserID = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4");

//        //    Assert.AreEqual("WF Testinng - Scenario 184 - Action 1 Activated", (string)email["subject".ToLower()]);
//        //    Assert.AreEqual(Workflow_Test_User_0UserID, (Guid)email["EmailFromId".ToLower()]);

//        //    Assert.AreEqual(regardingID, (Guid)email["RegardingID".ToLower()]);
//        //    Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
//        //    Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);


//        //    List<Guid> emailToID = phoenixPlatformServiceHelper.GetEmailsToByEmailID(emailIDs[0]);
//        //    Assert.AreEqual(1, emailToID.Count);

//        //    var emailTo = phoenixPlatformServiceHelper.GetEmailToById(emailToID[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
//        //    Assert.AreEqual(callerID, (Guid)emailTo["RegardingId".ToLower()]);
//        //    Assert.AreEqual(callerIDName, (string)emailTo["RegardingIdName".ToLower()]);
//        //    Assert.AreEqual(callerIdTableName, (string)emailTo["RegardingIdTableName".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7875")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 184 - Testing the latest changes to the to let the users set a lookup field to a MultiSelectMultiLookup field (e.g. set a PersonId to the Email’s “To” field when using the Send Email Activity). In this scenario the phone call caller field is set to a provider record")]
//        //public void WorkflowDetailsSection_TestMethod184_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 184";
//        //    string description = "WF Testing - Scenario 184";

//        //    Guid callerID = new Guid("81cc3d13-c7cd-4118-b60c-9f6596f966a4");
//        //    string callerIdTableName = "provider";
//        //    string callerIDName = "Ynys Mon - Mental Health - Provider";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emailIDs.Count);

//        //    //get the email info
//        //    var email = phoenixPlatformServiceHelper.GetEmailById(emailIDs[0], "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "EmailFromId");

//        //    Guid Workflow_Test_User_0UserID = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4");

//        //    Assert.AreEqual("WF Testinng - Scenario 184 - Action 1 Activated", (string)email["subject".ToLower()]);
//        //    Assert.AreEqual(Workflow_Test_User_0UserID, (Guid)email["EmailFromId".ToLower()]);

//        //    Assert.AreEqual(regardingID, (Guid)email["RegardingID".ToLower()]);
//        //    Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
//        //    Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);


//        //    List<Guid> emailToID = phoenixPlatformServiceHelper.GetEmailsToByEmailID(emailIDs[0]);
//        //    Assert.AreEqual(1, emailToID.Count);

//        //    var emailTo = phoenixPlatformServiceHelper.GetEmailToById(emailToID[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
//        //    Assert.AreEqual(callerID, (Guid)emailTo["RegardingId".ToLower()]);
//        //    Assert.AreEqual(callerIDName, (string)emailTo["RegardingIdName".ToLower()]);
//        //    Assert.AreEqual(callerIdTableName, (string)emailTo["RegardingIdTableName".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7876")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 184 - Testing the latest changes to the to let the users set a lookup field to a MultiSelectMultiLookup field (e.g. set a PersonId to the Email’s “To” field when using the Send Email Activity). In this scenario the phone call caller field is set to a professional record")]
//        //public void WorkflowDetailsSection_TestMethod184_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 184";
//        //    string description = "WF Testing - Scenario 184";

//        //    Guid callerID = new Guid("368aaba7-443f-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "professional";
//        //    string callerIDName = "Blanca Calderon";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emailIDs.Count);

//        //    //get the email info
//        //    var email = phoenixPlatformServiceHelper.GetEmailById(emailIDs[0], "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "EmailFromId");

//        //    Guid Workflow_Test_User_0UserID = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4");

//        //    Assert.AreEqual("WF Testinng - Scenario 184 - Action 1 Activated", (string)email["subject".ToLower()]);
//        //    Assert.AreEqual(Workflow_Test_User_0UserID, (Guid)email["EmailFromId".ToLower()]);

//        //    Assert.AreEqual(regardingID, (Guid)email["RegardingID".ToLower()]);
//        //    Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
//        //    Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);


//        //    List<Guid> emailToID = phoenixPlatformServiceHelper.GetEmailsToByEmailID(emailIDs[0]);
//        //    Assert.AreEqual(1, emailToID.Count);

//        //    var emailTo = phoenixPlatformServiceHelper.GetEmailToById(emailToID[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
//        //    Assert.AreEqual(callerID, (Guid)emailTo["RegardingId".ToLower()]);
//        //    Assert.AreEqual(callerIDName, (string)emailTo["RegardingIdName".ToLower()]);
//        //    Assert.AreEqual(callerIdTableName, (string)emailTo["RegardingIdTableName".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7877")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 184 - Testing the latest changes to the to let the users set a lookup field to a MultiSelectMultiLookup field (e.g. set a PersonId to the Email’s “To” field when using the Send Email Activity). In this scenario the phone call caller field is set to a system user record")]
//        //public void WorkflowDetailsSection_TestMethod184_4()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 184";
//        //    string description = "WF Testing - Scenario 184";

//        //    Guid callerID = new Guid("1ab2f044-0352-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "systemuser";
//        //    string callerIDName = "Workflow Test User 0";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emailIDs.Count);

//        //    //get the email info
//        //    var email = phoenixPlatformServiceHelper.GetEmailById(emailIDs[0], "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "EmailFromId");

//        //    Guid Workflow_Test_User_0UserID = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4");

//        //    Assert.AreEqual("WF Testinng - Scenario 184 - Action 1 Activated", (string)email["subject".ToLower()]);
//        //    Assert.AreEqual(Workflow_Test_User_0UserID, (Guid)email["EmailFromId".ToLower()]);

//        //    Assert.AreEqual(regardingID, (Guid)email["RegardingID".ToLower()]);
//        //    Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
//        //    Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);


//        //    List<Guid> emailToID = phoenixPlatformServiceHelper.GetEmailsToByEmailID(emailIDs[0]);
//        //    Assert.AreEqual(1, emailToID.Count);

//        //    var emailTo = phoenixPlatformServiceHelper.GetEmailToById(emailToID[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
//        //    Assert.AreEqual(callerID, (Guid)emailTo["RegardingId".ToLower()]);
//        //    Assert.AreEqual(callerIDName, (string)emailTo["RegardingIdName".ToLower()]);
//        //    Assert.AreEqual(callerIdTableName, (string)emailTo["RegardingIdTableName".ToLower()]);
//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7878")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 185 - Testing the latest changes to the to let the users set a lookup field to a MultiSelectMultiLookup field (e.g. set a PersonId to the Email’s “To” field when using the Send Email Activity). In this scenario the To field (email) will be set using a Responsible User reference (phone call)")]
//        //public void WorkflowDetailsSection_TestMethod185()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 185";
//        //    string description = "WF Testing - Scenario 185";

//        //    Guid callerID = new Guid("1ab2f044-0352-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "systemuser";
//        //    string callerIDName = "Workflow Test User 0";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid responsibleUser = new Guid("32972024-0839-E911-A2C5-005056926FE4"); //José Brazeta
//        //    string responsibleUserName = "José Brazeta"; //José Brazeta

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
//        //        recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, DateTime.Now.WithoutMilliseconds(), responsibleUser, responsibleUserName);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emailIDs.Count);

//        //    //get the email info
//        //    var email = phoenixPlatformServiceHelper.GetEmailById(emailIDs[0], "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "EmailFromId");

//        //    Guid Workflow_Test_User_0UserID = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4");

//        //    Assert.AreEqual("WF Testinng - Scenario 185 - Action 1 Activated", (string)email["subject".ToLower()]);
//        //    Assert.AreEqual(Workflow_Test_User_0UserID, (Guid)email["EmailFromId".ToLower()]);

//        //    Assert.AreEqual(regardingID, (Guid)email["RegardingID".ToLower()]);
//        //    Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
//        //    Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);


//        //    List<Guid> emailToID = phoenixPlatformServiceHelper.GetEmailsToByEmailID(emailIDs[0]);
//        //    Assert.AreEqual(1, emailToID.Count);

//        //    var emailTo = phoenixPlatformServiceHelper.GetEmailToById(emailToID[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
//        //    Assert.AreEqual(responsibleUser, (Guid)emailTo["RegardingId".ToLower()]);
//        //    Assert.AreEqual("José Brazeta", (string)emailTo["RegardingIdName".ToLower()]);
//        //    Assert.AreEqual("systemuser", (string)emailTo["RegardingIdTableName".ToLower()]);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-7879")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 186 - Testing the latest changes to let the users set a the value of a MultiSelectBusinessObject - In this scenario a new Inpatient Enhanced Observation record is created and the 'Names of professionals' field is set using the CreatedBy value from the Phone Call record")]
//        //public void WorkflowDetailsSection_TestMethod186()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 186";
//        //    string description = "WF Testing - Scenario 186";

//        //    Guid callerID = new Guid("1ab2f044-0352-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "systemuser";
//        //    string callerIDName = "Workflow Test User 0";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("eed11c7c-1541-ea11-a2c8-005056926fe4");
//        //    string regardingName = "Castaneda, Delia - (16/06/2000) [QA-CAS-000001-8838]";

//        //    Guid personid = new Guid("cc61f02d-4514-4e94-b27f-021aa7ef3c7e");

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid responsibleUser = new Guid("32972024-0839-E911-A2C5-005056926FE4"); //José Brazeta

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all InpatientEnhancedObservation for the Case
//        //    List<Guid> enhancedObservations = phoenixPlatformServiceHelper.GetInpatientEnhancedObservationsByCaseId(regardingID);

//        //    //Delete the records
//        //    foreach (Guid enhancedObservationid in enhancedObservations)
//        //    {
//        //        foreach (Guid enhancedObservationNameProfessionalId in phoenixPlatformServiceHelper.GetInpatientEnhancedObservationNameProfessionalByinpatientEnhancedObservationId(enhancedObservationid))
//        //            phoenixPlatformServiceHelper.DeleteInpatientEnhancedObservationNameProfessional(enhancedObservationNameProfessionalId);

//        //        phoenixPlatformServiceHelper.DeleteInpatientEnhancedObservation(enhancedObservationid);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForCase(subject, description, callerID, callerIdTableName, callerIDName,
//        //        recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, personid, "Delia Castaneda", teamID, "CareDirector QA", DateTime.Now.WithoutMilliseconds(), responsibleUser, "José Brazeta");


//        //    //ASSERT

//        //    //get the phone call created on field
//        //    var phonecallRecord = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "createdon");
//        //    DateTime phonecallCreatedOn = (DateTime)phonecallRecord["createdon".ToLower()];

//        //    //get all InpatientEnhancedObservation records for the Case
//        //    enhancedObservations = phoenixPlatformServiceHelper.GetInpatientEnhancedObservationsByCaseId(regardingID);
//        //    Assert.AreEqual(1, enhancedObservations.Count);

//        //    //get the InpatientEnhancedObservation info
//        //    var enhancedObservation = phoenixPlatformServiceHelper.GetInpatientEnhancedObservationById(enhancedObservations[0], "CaseId", "PersonId", "ConsultantId", "NamedProfessionalId", "DateCreated", "StartDateTime");

//        //    Assert.AreEqual(regardingID, (Guid)enhancedObservation["CaseId".ToLower()]);
//        //    Assert.AreEqual(personid, (Guid)enhancedObservation["PersonId".ToLower()]);
//        //    Assert.AreEqual(responsibleUser, (Guid)enhancedObservation["ConsultantId".ToLower()]);
//        //    Assert.AreEqual(responsibleUser, (Guid)enhancedObservation["NamedProfessionalId".ToLower()]);
//        //    Assert.AreEqual(phonecallCreatedOn, (DateTime)enhancedObservation["DateCreated".ToLower()]);
//        //    Assert.AreEqual(phonecallCreatedOn, (DateTime)enhancedObservation["StartDateTime".ToLower()]);


//        //    //get the Names of professionals for the InpatientEnhancedObservation record
//        //    var namesOfProfessionals = phoenixPlatformServiceHelper.GetInpatientEnhancedObservationNameProfessionalByinpatientEnhancedObservationId(enhancedObservations[0]);
//        //    Assert.AreEqual(1, namesOfProfessionals.Count);

//        //    //InpatientEnhancedObservation named professional must be set to the phone call createdby field
//        //    Guid Workflow_Test_User_0UserID = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4");
//        //    var professionalInfo = phoenixPlatformServiceHelper.GetInpatientEnhancedObservationNameProfessionalById(namesOfProfessionals[0], "SystemUserId");
//        //    Assert.AreEqual(Workflow_Test_User_0UserID, (Guid)professionalInfo["SystemUserId".ToLower()]);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-7880")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 187 - Testing the latest changes allow the set of a datetime field using a date field and the reverse operation (setting a date field using a datetime field). in this scenario the date and datetime fields in the phonecall record have values (not null)")]
//        //public void WorkflowDetailsSection_TestMethod187_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 186";
//        //    string description = "WF Testing - Scenario 186 - desc....";

//        //    Guid callerID = new Guid("1ab2f044-0352-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "systemuser";
//        //    string callerIDName = "Workflow Test User 0";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime phoneCallDate = new DateTime(2020, 2, 1, 9, 30, 0);
//        //    DateTime eventDate = new DateTime(2020, 2, 3, 0, 0, 0);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
//        //        recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, 1, true, eventDate);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emailIDs.Count);

//        //    //get the email info
//        //    var email = phoenixPlatformServiceHelper.GetEmailById(emailIDs[0], "duedate", "significanteventdate");

//        //    Assert.AreEqual(eventDate, (DateTime)email["duedate".ToLower()]);
//        //    Assert.AreEqual(phoneCallDate.Date, (DateTime)email["significanteventdate".ToLower()]);

//        //}




//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("")]
//        //public void WorkflowDetailsSection_RichTextBoxUpdates_01()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Rich Text Editor - Scenario 1";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2019, 3, 2);

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //login with a user that do not belongs to Caredirector team
//        //    var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_1", "Passw0rd_!");
//        //    var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//        //    phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);

//        //    //create the phone call record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName,
//        //        phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, "person");


//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "notes");
//        //    Assert.AreEqual("<p>Subject:&nbsp;WF Testing - Rich Text Editor - Scenario 1</p>\n\n<p>Phone Number:&nbsp;0987654321234</p>\n\n<p>Recipient:&nbsp;José Brazeta</p>\n\n<p>Caller:&nbsp;Adolfo Abbott</p>\n\n<p>Regarding:&nbsp;Adolfo Abbott</p>\n\n<p>Phone Call Date:&nbsp;02/03/2019 00:00:00</p>\n\n<p>&nbsp;</p>", fields["notes"]);

//        //}



//        //#region https://advancedcsg.atlassian.net/browse/CDV6-8028

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8028 - Scenario 1 - " +
//        //    "Validate that workflows can now use picklist values in conditions 'Business Data: Get Answer By Identifier: Picklist Answer (Document Pick List Value)' - " +
//        //    "In this scenario the picklist value will match the WF condition")]
//        //public void WorkflowDetailsSection_TestMethod188()
//        //{
//        //    var personFormId = new Guid("bc195cea-0254-eb11-a2fb-005056926fe4");

//        //    //reset the person form status
//        //    phoenixPlatformServiceHelper.UpdatePersonFormStatus(personFormId, 1); //set to In Progress

//        //    //remove any value from the review date
//        //    phoenixPlatformServiceHelper.UpdatePersonFormReviewDate(personFormId, null);


//        //    //ACT

//        //    //change the status again to trigger the WF
//        //    phoenixPlatformServiceHelper.UpdatePersonFormStatus(personFormId, 4); //set to Not Started


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var fields = phoenixPlatformServiceHelper.GetPersonFormByID(personFormId, "reviewdate");
//        //    Assert.AreEqual(new DateTime(2021, 1, 11), fields["reviewdate"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8028 - Scenario 1 - " +
//        //    "Validate that workflows can now use picklist values in conditions 'Business Data: Get Answer By Identifier: Picklist Answer (Document Pick List Value)' - " +
//        //    "In this scenario the picklist value will NOT match the WF condition")]
//        //public void WorkflowDetailsSection_TestMethod188_1()
//        //{
//        //    var personFormId = new Guid("101cc3fc-0254-eb11-a2fb-005056926fe4");

//        //    //reset the person form status
//        //    phoenixPlatformServiceHelper.UpdatePersonFormStatus(personFormId, 1); //set to In Progress

//        //    //remove any value from the review date
//        //    phoenixPlatformServiceHelper.UpdatePersonFormReviewDate(personFormId, null);


//        //    //ACT

//        //    //change the status again to trigger the WF
//        //    phoenixPlatformServiceHelper.UpdatePersonFormStatus(personFormId, 4); //set to Not Started


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var fields = phoenixPlatformServiceHelper.GetPersonFormByID(personFormId, "reviewdate");
//        //    Assert.IsFalse(fields.ContainsKey("reviewdate"));
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8028 - Scenario 2 - " +
//        //   "Validate that workflow conditions can now reference the primary key of the record that triggered the update - " +
//        //   "Positive scenario where the record triggering the workflow will match the workflow condition")]
//        //public void WorkflowDetailsSection_TestMethod188_2()
//        //{
//        //    var personFormId = new Guid("ea030c0f-0754-eb11-a2fb-005056926fe4");

//        //    //reset the person form status
//        //    phoenixPlatformServiceHelper.UpdatePersonFormStatus(personFormId, 1); //set to In Progress

//        //    //remove any value from the review date
//        //    phoenixPlatformServiceHelper.UpdatePersonFormReviewDate(personFormId, null);


//        //    //ACT

//        //    //change the status again to trigger the WF
//        //    phoenixPlatformServiceHelper.UpdatePersonFormStatus(personFormId, 4); //set to Not Started


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var fields = phoenixPlatformServiceHelper.GetPersonFormByID(personFormId, "reviewdate");
//        //    Assert.AreEqual(new DateTime(2021, 1, 31), fields["reviewdate"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8028 - Scenario 2 - " +
//        //   "Validate that workflow conditions can now reference the primary key of the record that triggered the update - " +
//        //   "Positive scenario where the record triggering the workflow will match the workflow condition")]
//        //public void WorkflowDetailsSection_TestMethod188_3()
//        //{
//        //    var personFormId = new Guid("f8e4b3f4-0754-eb11-a2fb-005056926fe4");

//        //    //reset the person form status
//        //    phoenixPlatformServiceHelper.UpdatePersonFormStatus(personFormId, 1); //set to In Progress

//        //    //remove any value from the review date
//        //    phoenixPlatformServiceHelper.UpdatePersonFormReviewDate(personFormId, null);


//        //    //ACT

//        //    //change the status again to trigger the WF
//        //    phoenixPlatformServiceHelper.UpdatePersonFormStatus(personFormId, 4); //set to Not Started


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var fields = phoenixPlatformServiceHelper.GetPersonFormByID(personFormId, "reviewdate");
//        //    Assert.IsFalse(fields.ContainsKey("reviewdate"));
//        //}

//        //#endregion

//        //#region https://advancedcsg.atlassian.net/browse/CDV6-8283

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8283 - Scenario 1 - " +
//        //   "Validate that the Responsible team can be set to the default value when using the Create Record action")]
//        //public void WorkflowDetailsSection_SetOwnerOnCreateAction_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing CDV6-8283 Default Owner";
//        //    string description = "desc....";

//        //    Guid callerID = new Guid("1ab2f044-0352-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "systemuser";
//        //    string callerIDName = "Workflow Test User 0";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime phoneCallDate = new DateTime(2020, 2, 1, 9, 30, 0);
//        //    DateTime eventDate = new DateTime(2020, 2, 3, 0, 0, 0);


//        //    //Delete the phone calls for the person
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);


//        //    //Delete the phone calls for the person
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
//        //        recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, 1, true, eventDate);


//        //    //ASSERT

//        //    //get all Phone Call records for the person
//        //    var phoneCalls = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID, "Workflow Activated - Use default team");
//        //    Assert.AreEqual(1, phoneCalls.Count);

//        //    //validate the ownerid field is set to the default value
//        //    var expectedOwnerID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCalls[0], "ownerid");
//        //    Assert.AreEqual(expectedOwnerID, fields["ownerid"]);

//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8283 - Scenario 1 - " +
//        //   "Validate that the Responsible team can be set to the default value when using the Send Email action")]
//        //public void WorkflowDetailsSection_SetOwnerOnSendEmailAction_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing CDV6-8283 Default Owner";
//        //    string description = "desc....";

//        //    Guid callerID = new Guid("1ab2f044-0352-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "systemuser";
//        //    string callerIDName = "Workflow Test User 0";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime phoneCallDate = new DateTime(2020, 2, 1, 9, 30, 0);
//        //    DateTime eventDate = new DateTime(2020, 2, 3, 0, 0, 0);


//        //    //Delete the phone calls for the person
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);


//        //    //Delete the phone calls for the person
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
//        //        recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, 1, true, eventDate);


//        //    //ASSERT

//        //    //get all Phone Call records for the person
//        //    var emails = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emails.Count);

//        //    //validate the ownerid field is set to the default value
//        //    var expectedOwnerID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA
//        //    var fields = phoenixPlatformServiceHelper.GetEmailById(emails[0], "ownerid");
//        //    Assert.AreEqual(expectedOwnerID, fields["ownerid"]);

//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8283 - Scenario 2 - " +
//        //   "Validate that the Responsible team can be set to a specified value when using the Create Record action")]
//        //public void WorkflowDetailsSection_SetOwnerOnCreateAction_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing CDV6-8283 Set Owner";
//        //    string description = "desc....";

//        //    Guid callerID = new Guid("1ab2f044-0352-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "systemuser";
//        //    string callerIDName = "Workflow Test User 0";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime phoneCallDate = new DateTime(2020, 2, 1, 9, 30, 0);
//        //    DateTime eventDate = new DateTime(2020, 2, 3, 0, 0, 0);


//        //    //Delete the phone calls for the person
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);


//        //    //Delete the phone calls for the person
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
//        //        recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, 1, true, eventDate);


//        //    //ASSERT

//        //    //get all Phone Call records for the person
//        //    var phoneCalls = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID, "Workflow Activated - Set team");
//        //    Assert.AreEqual(1, phoneCalls.Count);

//        //    //validate the ownerid field is set to the specified value in the create action
//        //    var expectedOwnerID = new Guid("3676bff7-f81f-ea11-a2c8-005056926fe4"); //Advanced
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCalls[0], "ownerid");
//        //    Assert.AreEqual(expectedOwnerID, fields["ownerid"]);

//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8283 - Scenario 2 - " +
//        //   "Validate that the Responsible team can be set to a specified value when using the Create Record action")]
//        //public void WorkflowDetailsSection_SetOwnerOnSendEmailAction_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing CDV6-8283 Set Owner";
//        //    string description = "desc....";

//        //    Guid callerID = new Guid("1ab2f044-0352-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "systemuser";
//        //    string callerIDName = "Workflow Test User 0";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime phoneCallDate = new DateTime(2020, 2, 1, 9, 30, 0);
//        //    DateTime eventDate = new DateTime(2020, 2, 3, 0, 0, 0);


//        //    //Delete the phone calls for the person
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);


//        //    //Delete the phone calls for the person
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
//        //        recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, 1, true, eventDate);


//        //    //ASSERT

//        //    //get all Phone Call records for the person
//        //    var emails = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emails.Count);

//        //    //validate the ownerid field is set to the specified value in the create action
//        //    var expectedOwnerID = new Guid("3676bff7-f81f-ea11-a2c8-005056926fe4"); //Advanced
//        //    var fields = phoenixPlatformServiceHelper.GetEmailById(emails[0], "ownerid");
//        //    Assert.AreEqual(expectedOwnerID, fields["ownerid"]);

//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Automation Script for the defect https://advancedcsg.atlassian.net/browse/CDV6-8283 - Scenario 3 - " +
//        //   "Validate that the Responsible team is not changed in a Update action")]
//        //public void WorkflowDetailsSection_SetOwnerOnCreateAction_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing CDV6-8283 Update";
//        //    string description = "desc....";

//        //    Guid callerID = new Guid("1ab2f044-0352-e911-a2c5-005056926fe4");
//        //    string callerIdTableName = "systemuser";
//        //    string callerIDName = "Workflow Test User 0";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("3676bff7-f81f-ea11-a2c8-005056926fe4"); //Advanced
//        //    Guid teamOwningBU = new Guid("549163DD-2C5A-E511-80C8-0050560502CC");

//        //    DateTime phoneCallDate = new DateTime(2020, 2, 1, 9, 30, 0);
//        //    DateTime eventDate = new DateTime(2020, 2, 3, 0, 0, 0);


//        //    //Delete the phone calls for the person
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);


//        //    //Delete the phone calls for the person
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID,
//        //        recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, phoneCallDate, null, null, false, 1, true, eventDate);


//        //    //ASSERT

//        //    //get all Phone Call records for the person
//        //    var phoneCalls = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);
//        //    Assert.AreEqual(1, phoneCalls.Count);

//        //    //validate the ownerid field is set to the specified value in the create action
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCalls[0], "ownerid", "notes");
//        //    Assert.AreEqual(teamID, fields["ownerid"]);
//        //    Assert.AreEqual("<p>UPDATE</p>", fields["notes"]);

//        //}

//        //#endregion

//        //#region https://advancedcsg.atlassian.net/browse/CDV6-8527

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("https://advancedcsg.atlassian.net/browse/CDV6-8527 - " +
//        //    "Test for the defect identified in CDV6-8527 - Test the Assign of a record that contains a data restriction.")]
//        //public void WorkflowDetailsSection_AssignRestrictionRecord_TestMethod01()
//        //{
//        //    var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("SecurityTestUserAdmin", "Passw0rd_!");
//        //    var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//        //    phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);

//        //    //ARRANGE 
//        //    string subject = "WF Testing CDV68527 Scenario 1";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT
//        //    Guid expectedDataRestriction = new Guid("85fa2adb-78d2-ea11-a2cd-005056926fe4"); //Mobile Data Restriction - Allow User (1)
//        //    Guid expectedTeamId = new Guid("824f20c4-bf70-e911-a2c5-005056926fe4"); //CMHT Adult
//        //    Guid expectedBusinessUnitId = new Guid("c3476351-bf70-e911-a2c5-005056926fe4"); //Care Service - Community & Rehabilitation

//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "datarestrictionid", "OwnerId", "OwningBusinessUnitId");

//        //    Assert.AreEqual(expectedDataRestriction, phoneCallfields["DataRestrictionId".ToLower()]);
//        //    Assert.AreEqual(expectedTeamId, phoneCallfields["OwnerId".ToLower()]);
//        //    Assert.AreEqual(expectedBusinessUnitId, phoneCallfields["OwningBusinessUnitId".ToLower()]);
//        //}

//        //#endregion

//        //#region https://advancedcsg.atlassian.net/browse/CDV6-8799

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test for the story identified in CDV6-8799 - Allow Team Email to be used in Workflow")]
//        //public void WorkflowDetailsSection_CDV68799_TestMethod001()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 30.1";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("3676bff7-f81f-ea11-a2c8-005056926fe4"); //Advanced

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emailIDs.Count);

//        //    //get the email info
//        //    var email = phoenixPlatformServiceHelper.GetEmailById(emailIDs[0], "Subject");
//        //    Assert.AreEqual("WF Testinng - Scenario 30.1 - Action 1 Activated", (string)email["subject".ToLower()]);

//        //    var emailToIds = phoenixPlatformServiceHelper.GetEmailsToByEmailID(emailIDs[0]);
//        //    Assert.AreEqual(1, emailToIds.Count);

//        //    var caredirectorQATeamId = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5");
//        //    var emailTo = phoenixPlatformServiceHelper.GetEmailToById(emailToIds[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
//        //    Assert.AreEqual(caredirectorQATeamId, (Guid)emailTo["RegardingId".ToLower()]);
//        //    Assert.AreEqual("CareDirector QA", (string)emailTo["RegardingIdName".ToLower()]);
//        //    Assert.AreEqual("team", (string)emailTo["RegardingIdTableName".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test for the story identified in CDV6-8799 - Allow Team Email to be used in Workflow")]
//        //public void WorkflowDetailsSection_CDV68799_TestMethod002()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - Scenario 30.2";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("3676bff7-f81f-ea11-a2c8-005056926fe4"); //Advanced

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //get all Email records for the person
//        //    List<Guid> emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);

//        //    //Delete the records
//        //    foreach (Guid mailId in emailIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);
//        //    }


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emailIDs.Count);

//        //    //get the email info
//        //    var email = phoenixPlatformServiceHelper.GetEmailById(emailIDs[0], "Subject");
//        //    Assert.AreEqual("WF Testinng - Scenario 30.2 - Action 1 Activated", (string)email["subject".ToLower()]);

//        //    var emailToIds = phoenixPlatformServiceHelper.GetEmailsToByEmailID(emailIDs[0]);
//        //    Assert.AreEqual(1, emailToIds.Count);

//        //    var emailTo = phoenixPlatformServiceHelper.GetEmailToById(emailToIds[0], "RegardingId", "RegardingIdTableName", "RegardingIdName");
//        //    Assert.AreEqual(teamID, (Guid)emailTo["RegardingId".ToLower()]);
//        //    Assert.AreEqual("Advanced", (string)emailTo["RegardingIdName".ToLower()]);
//        //    Assert.AreEqual("team", (string)emailTo["RegardingIdTableName".ToLower()]);
//        //}

//        //#endregion

//        //#region https://advancedcsg.atlassian.net/browse/CDV6-9096

//        ///// <summary>
//        ///// https://advancedcsg.atlassian.net/browse/CDV6-9096
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test for the story identified in CDV6-9096 - Allow a child workflow to be unpublished when linked to a parent workflow - Parent has call to published WF")]
//        //public void WorkflowDetailsSection_CDV69096_TestMethod001()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Automated Testing - CDV6-9096 - Scenario 1";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Automated Testing - CDV6-9096 - Published Child WF Triggered", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        ///// <summary>
//        ///// https://advancedcsg.atlassian.net/browse/CDV6-9096
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test for the story identified in CDV6-9096 - Allow a child workflow to be unpublished when linked to a parent workflow - Parent has call to unpublished WF")]
//        //public void WorkflowDetailsSection_CDV69096_TestMethod002()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Automated Testing - CDV6-9096 - Scenario 2";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT


//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Automated Testing - CDV6-9096 - Scenario 2", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        //#endregion

//        //#region https://advancedcsg.atlassian.net/browse/CDV6-9097

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-9097' - Step 1 (Action, Condition, Inner Action) ")]
//        //public void WorkflowDetailsSection_CDV69097_TestMethod001()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-9097 Step 1 Test -";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "notes");
//        //    Assert.AreEqual("<p>CDV6-9097 Step 1 Test -Desc...</p>", (string)phoneCallfields["notes".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-9097' - Step 2 (Action, Action, Condition, Inner Action) ")]
//        //public void WorkflowDetailsSection_CDV69097_TestMethod002()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-9097 Step 2 Test -";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2021, 4, 3, 9, 45, 0);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, 
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("CDV6-9097 Step 2 Test - 2nd If activated", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-9097' - Step 3 (Action, Condition, Inner Condition, Inner Action) ")]
//        //public void WorkflowDetailsSection_CDV69097_TestMethod003()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-9097 Step 3 Test -";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2021, 4, 7, 15, 30, 0);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "notes");
//        //    Assert.AreEqual("<p>CDV6-9097 Step 3 Test -07/04/2021 15:30:00</p>", (string)phoneCallfields["notes".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-9097' - Step 4 (Action, Condition, Inner Action, Action) ")]
//        //public void WorkflowDetailsSection_CDV69097_TestMethod004()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-9097 Step 4 Test";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2021, 4, 7, 15, 30, 0);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("CDV6-9097 Step 4 Test - Activated", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-9097' - Step 5 (Action, Condition, Inner Action, Action, Condition, Inner Action) ")]
//        //public void WorkflowDetailsSection_CDV69097_TestMethod005_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-9097 Step 5 - Condition 1";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2021, 4, 7, 15, 30, 0);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "notes");
//        //    Assert.AreEqual("<p>04/04/2021 15:30:00</p>", (string)phoneCallfields["notes".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-9097' - Step 5 (Action, Condition, Inner Action, Action, Condition, Inner Action) ")]
//        //public void WorkflowDetailsSection_CDV69097_TestMethod005_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-9097 Step 5 - Condition 2";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = new DateTime(2021, 4, 7, 15, 30, 0);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(
//        //        subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName,
//        //        recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "notes");
//        //    Assert.AreEqual("<p>Workflow Test User 0</p>", (string)phoneCallfields["notes".ToLower()]);
//        //}

//        //#endregion

//        //#region https://advancedcsg.atlassian.net/browse/CDV6-8893

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - First if condition (positive scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod001()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - CDV6-8893-Scenario 1";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("94f669d6-2811-45d5-b649-f390de5e1177");
//        //    string regardingName = "Amy Strong";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - CDV6-8893-Scenario 1 - Activated", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - First if condition (positive scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod002()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - CDV6-8893-Scenario 1";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("13fc7ce3-6d95-47ae-b120-5277f699a1e2");
//        //    string regardingName = "Patrick Rogers";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - CDV6-8893-Scenario 1 - Activated", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - First if condition (positive scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod003()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - CDV6-8893-Scenario 1";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("045e13fe-3b4e-4700-b96a-da606f1e607e");
//        //    string regardingName = "Marci Burnett";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - CDV6-8893-Scenario 1 - Activated", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - First if condition (negative scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod004()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - CDV6-8893-Scenario 1";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";


//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - CDV6-8893-Scenario 1", (string)phoneCallfields["subject".ToLower()]);
//        //}



//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Second if condition (positive scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod005()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - CDV6-8893-Scenario 2";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("94f669d6-2811-45d5-b649-f390de5e1177");
//        //    string regardingName = "Amy Strong";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - CDV6-8893-Scenario 2 - Activated", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Second if condition (positive scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod006()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - CDV6-8893-Scenario 2";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("13fc7ce3-6d95-47ae-b120-5277f699a1e2");
//        //    string regardingName = "Patrick Rogers";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - CDV6-8893-Scenario 2 - Activated", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Second if condition (positive scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod007()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - CDV6-8893-Scenario 2";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("045e13fe-3b4e-4700-b96a-da606f1e607e");
//        //    string regardingName = "Marci Burnett";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - CDV6-8893-Scenario 2 - Activated", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Second if condition (negative scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod008()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - CDV6-8893-Scenario 2";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";


//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - CDV6-8893-Scenario 2", (string)phoneCallfields["subject".ToLower()]);
//        //}



//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Third if condition (positive scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod009()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - CDV6-8893-Scenario 3";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";


//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - CDV6-8893-Scenario 3 - Activated", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Third if condition (negative scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod010()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - CDV6-8893-Scenario 3";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("94f669d6-2811-45d5-b649-f390de5e1177");
//        //    string regardingName = "Amy Strong";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - CDV6-8893-Scenario 3", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Third if condition (negative scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod011()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - CDV6-8893-Scenario 3";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("13fc7ce3-6d95-47ae-b120-5277f699a1e2");
//        //    string regardingName = "Patrick Rogers";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - CDV6-8893-Scenario 3", (string)phoneCallfields["subject".ToLower()]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893' - Step 1 - Third if condition (negative scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod012()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - CDV6-8893-Scenario 3";
//        //    string description = "Desc...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("045e13fe-3b4e-4700-b96a-da606f1e607e");
//        //    string regardingName = "Marci Burnett";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID))
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);

//        //    //Delete the records
//        //    foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID))
//        //        phoenixPlatformServiceHelper.DeleteEmail(mailId);


//        //    //ACT: create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID);


//        //    //ASSERT

//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - CDV6-8893-Scenario 3", (string)phoneCallfields["subject".ToLower()]);
//        //}




//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893 (Case Form)' - Step 1 - first if condition (positive scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod013()
//        //{
//        //    //ARRANGE 
//        //    Guid caseFormID = new Guid("6e3a7291-3e99-eb11-a323-005056926fe4"); //case number: QA-CAS-000001-38720

//        //    //reset all dates
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2021, 4, 3), new DateTime(2021, 4, 4), new DateTime(2021, 4, 5));


//        //    //ACT
//        //    //set the dates that will trigger the workflow
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2021, 4, 1), new DateTime(2021, 4, 2), new DateTime(2021, 4, 3));


//        //    //ASSERT
//        //    //if the workflow was triggered then the review date shoudl be set to 09/04/2021
//        //    var fields = phoenixPlatformServiceHelper.GetCaseFormByID(caseFormID, "reviewdate");
//        //    Assert.AreEqual(new DateTime(2021, 4, 9), (DateTime)fields["reviewdate"]);

//        //}

//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893 (Case Form)' - Step 1 - first if condition (positive scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod014()
//        //{
//        //    //ARRANGE 
//        //    Guid caseFormID = new Guid("e1ed2897-3f99-eb11-a323-005056926fe4"); //case number: QA-CAS-000001-38721

//        //    //reset all dates
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2021, 4, 3), new DateTime(2021, 4, 4), new DateTime(2021, 4, 5));


//        //    //ACT
//        //    //set the dates that will trigger the workflow
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2021, 4, 1), new DateTime(2021, 4, 2), new DateTime(2021, 4, 3));


//        //    //ASSERT
//        //    //if the workflow was triggered then the review date shoudl be set to 09/04/2021
//        //    var fields = phoenixPlatformServiceHelper.GetCaseFormByID(caseFormID, "reviewdate");
//        //    Assert.AreEqual(new DateTime(2021, 4, 9), (DateTime)fields["reviewdate"]);

//        //}

//        //[TestMethod]
//        //[Description("Workflow under test 'WF Automated Testing - CDV6-8893 (Case Form)' - Step 1 - first if condition (negative scenario)")]
//        //public void WorkflowDetailsSection_CDV8893_TestMethod015()
//        //{
//        //    //ARRANGE 
//        //    Guid caseFormID = new Guid("82d247e4-3f99-eb11-a323-005056926fe4"); //case number: QA-CAS-000001-38722

//        //    //reset all dates
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2021, 4, 3), new DateTime(2021, 4, 4), new DateTime(2021, 4, 5));


//        //    //ACT
//        //    //set the dates that will trigger the workflow
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2021, 4, 1), new DateTime(2021, 4, 2), new DateTime(2021, 4, 3));


//        //    //ASSERT
//        //    //if the workflow was triggered then the review date shoudl be set to 09/04/2021
//        //    var fields = phoenixPlatformServiceHelper.GetCaseFormByID(caseFormID, "reviewdate");
//        //    Assert.AreEqual(new DateTime(2021, 4, 3), (DateTime)fields["reviewdate"]);

//        //}

//        //#endregion

//        //#region https://advancedcsg.atlassian.net/browse/CDV6-15933

//        //[TestProperty("JiraIssueID", "CDV6-16457")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - CDV6-15933 - Test Assign record action")]
//        //public void WorkflowDetailsSection_CDV6_15933_TestMethod01()
//        //{
//        //    //ARRANGE 
//        //    var teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    var middleName = DateTime.Now.ToString("yyyyMMddHHmmss");
//        //    var ethnicity = new Guid("c80ef805-bf68-ec11-a32d-f90a4322a942"); //Irish
//        //    var contactReceivedBy = new Guid("ab530762-b8a3-ec11-a334-005056926fe4"); //Testing CDV6-15933
//        //    var responsibleUser = new Guid("32972024-0839-E911-A2C5-005056926FE4"); //José Brazeta
//        //    var caseStatusId = new Guid("bc156ac3-bafe-e811-80dc-0050560502cc"); //Allocate to Team
//        //    var contactreason = new Guid("3784785b-9750-e911-a2c5-005056926fe4"); //Advice/Consultation
//        //    var dataformid = new Guid("EDF4EFC4-BFB1-E811-80DC-0050560502CC"); //Social Care Case
//        //    var contactSource = new Guid("31898db2-aa3e-ea11-a2c8-005056926fe4"); //‘ANONYMOUS’
//        //    var caseDateTime = new DateTime(2022, 03, 01);
//        //    var dateTimeContactReceived = new DateTime(2022, 03, 02);

//        //    var personId = phoenixPlatformServiceHelper.CreatePersonRecord("mr", "Jhon", middleName, "MCMan", "CDV6-15933", DateTime.Now.Date.AddYears(-20), ethnicity, teamID, 1, 1);

//        //    var caseId = phoenixPlatformServiceHelper.CreateSocialCareCase(teamID, personId, contactReceivedBy, responsibleUser, caseStatusId, contactreason, dataformid, contactSource , caseDateTime, dateTimeContactReceived, 20);

//        //    var taskId = phoenixPlatformServiceHelper.CreateCaseTask(caseId, personId, "", "Test Task", "desc ....", teamID);

//        //    //ACT
//        //    //update the Case record
//        //    phoenixPlatformServiceHelper.UpdateCaseAdditionalDetails(caseId, "updated ....");


//        //    //ASSERT
//        //    var expectedTeam = new Guid("b1023b89-4272-e911-a2c5-005056926fe4"); //Acute & PICU

//        //    var caseFields = phoenixPlatformServiceHelper.GetCaseById(caseId, "ownerid");
//        //    Assert.AreEqual(expectedTeam, caseFields["ownerid"]);

//        //    var taskFields = phoenixPlatformServiceHelper.GetTaskById(taskId, "ownerid");
//        //    Assert.AreEqual(expectedTeam, taskFields["ownerid"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-16462")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - CDV6-15933 - Test Assign record action")]
//        //public void WorkflowDetailsSection_CDV6_15933_TestMethod02()
//        //{
//        //    //ARRANGE 
//        //    var teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team
//        //    var middleName = DateTime.Now.ToString("yyyyMMddHHmmss");
//        //    var ethnicity = new Guid("c80ef805-bf68-ec11-a32d-f90a4322a942"); //Irish
//        //    var contactReceivedBy = new Guid("ab530762-b8a3-ec11-a334-005056926fe4"); //Testing CDV6-15933
//        //    var responsibleUser = new Guid("32972024-0839-E911-A2C5-005056926FE4"); //José Brazeta
//        //    var caseStatusId = new Guid("bc156ac3-bafe-e811-80dc-0050560502cc"); //Allocate to Team
//        //    var contactreason = new Guid("3784785b-9750-e911-a2c5-005056926fe4"); //Advice/Consultation
//        //    var dataformid = new Guid("EDF4EFC4-BFB1-E811-80DC-0050560502CC"); //Social Care Case
//        //    var contactSource = new Guid("31898db2-aa3e-ea11-a2c8-005056926fe4"); //‘ANONYMOUS’
//        //    var caseDateTime = new DateTime(2022, 03, 07);
//        //    var dateTimeContactReceived = new DateTime(2022, 03, 08);

//        //    var personId = phoenixPlatformServiceHelper.CreatePersonRecord("mr", "Jhon", middleName, "MCMan", "CDV6-15933", DateTime.Now.Date.AddYears(-20), ethnicity, teamID, 1, 1);

//        //    var caseId = phoenixPlatformServiceHelper.CreateSocialCareCase(teamID, personId, contactReceivedBy, responsibleUser, caseStatusId, contactreason, dataformid, contactSource, caseDateTime, dateTimeContactReceived, 20);

//        //    var taskId = phoenixPlatformServiceHelper.CreateCaseTask(caseId, personId, "", "Test Task", "desc ....", teamID);

//        //    //ACT
//        //    //update the Case record
//        //    phoenixPlatformServiceHelper.UpdateCaseAdditionalDetails(caseId, "updated ....");


//        //    //ASSERT
//        //    var expectedTeam = new Guid("6a7285c3-e083-ea11-a2cd-005056926fe4"); //Adult Safeguarding

//        //    var caseFields = phoenixPlatformServiceHelper.GetCaseById(caseId, "ownerid");
//        //    Assert.AreEqual(expectedTeam, caseFields["ownerid"]);

//        //    var taskFields = phoenixPlatformServiceHelper.GetTaskById(taskId, "ownerid");
//        //    Assert.AreEqual(teamID, taskFields["ownerid"]);
//        //}

//        //#endregion


//        [Description("Method will return the name of all tests and the Description of each one")]
//        [TestMethod]
//        public void GetTestNames()
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.AppendLine("TestName,Description,JiraID");

//            Type t = this.GetType();

//            foreach (var method in t.GetMethods())
//            {
//                TestMethodAttribute testMethod = null;
//                DescriptionAttribute descAttr = null;
//                TestPropertyAttribute propertyAttr = null;

//                foreach (var attribute in method.GetCustomAttributes(false))
//                {
//                    if (attribute is TestMethodAttribute)
//                        testMethod = attribute as TestMethodAttribute;

//                    if (attribute is DescriptionAttribute)
//                        descAttr = attribute as DescriptionAttribute;

//                    if (attribute is TestPropertyAttribute && (attribute as TestPropertyAttribute).Name == "JiraIssueID")
//                        propertyAttr = attribute as TestPropertyAttribute;
//                }

//                if (testMethod != null && propertyAttr != null)
//                {
//                    if(!string.IsNullOrEmpty(propertyAttr.Value))
//                        sb.AppendLine(propertyAttr.Value);
//                    //sb.AppendLine(method.Name + "," + descAttr.Description + "," + propertyAttr.Value);
//                    continue;
//                }
//                //if (testMethod != null)
//                //{
//                //    sb.AppendLine(method.Name + "," + descAttr.Description);
//                //    continue;
//                //}

//            }

//            Console.WriteLine(sb.ToString());
//        }



//        private TestContext testContextInstance;

//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }



//        [TestCleanup()]
//        public virtual void MyTestCleanup()
//        {
//            string jiraIssueID = (string)this.TestContext.Properties["JiraIssueID"];

//            //if we have a jira id for the test then we will update its status in jira
//            if (jiraIssueID != null)
//            {
//                bool testPassed = this.TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;

//                var zapi = new AtlassianServiceAPI.Models.Zapi()
//                {
//                    AccessKey = ConfigurationManager.AppSettings["AccessKey"],
//                    SecretKey = ConfigurationManager.AppSettings["SecretKey"],
//                    User = ConfigurationManager.AppSettings["User"],
//                };

//                var jiraAPI = new AtlassianServiceAPI.Models.JiraApi()
//                {
//                    Authentication = ConfigurationManager.AppSettings["Authentication"],
//                    JiraCloudUrl = ConfigurationManager.AppSettings["JiraCloudUrl"],
//                    ProjectKey = ConfigurationManager.AppSettings["ProjectKey"]
//                };

//                AtlassianServicesAPI.AtlassianService atlassianService = new AtlassianServicesAPI.AtlassianService(zapi, jiraAPI);

//                string versionName = ConfigurationManager.AppSettings["CurrentVersionName"];

//                if (testPassed)
//                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automated Testing Workflows", AtlassianServiceAPI.Models.JiraTestOutcome.Passed);
//                else
//                    atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automated Testing Workflows", AtlassianServiceAPI.Models.JiraTestOutcome.Failed);


//            }

//        }

//    }
//}
