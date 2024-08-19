//using System;
//using System.Linq;
//using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Text;
//using System.Configuration;

//namespace Phoenix.WorkflowTests.WorkflowDetailsSection
//{
//    [TestClass]
//    public class PreSyncWorkflowDetailsSectionTests
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

//        //#region Date / Time Field

//        //[TestProperty("JiraIssueID", "CDV6-4355")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 01 - Does Not Contain Data Operator - Phone Call Date field does not contains data - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod01()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 01";
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

//        //    //create the record
//        //    try
//        //    {
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 01 Activated - Does Not Contain Data", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4356")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 01 - Equals Operator - Phone Call Date field equals 01/08/2020 - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod01_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 01";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 01 Activated - Equals", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4357")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 01 - Between Operator - Phone Call Date field between 01/07/2020 and 03/07/2020 (set date to 01/07/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod01_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 01";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 01 - Between", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4358")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 01 - Between Operator - Phone Call Date field between 01/07/2020 and 03/07/2020 (set date to 02/07/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod01_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 01";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 2);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 01 - Between", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4359")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 01 - Between Operator - Phone Call Date field between 01/07/2020 and 03/07/2020 (set date to 03/07/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod01_4()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 01";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 3);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 01 - Between", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4360")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 01 - Between Operator - Phone Call Date field smaller than 01/07/2020 (set date to 25/06/2020) - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod01_5()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 01";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 6, 25);

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


//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4361")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 01 - Between Operator - Phone Call Date field greater than 03/07/2020 (set date to 04/07/2020) - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod01_6()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 01";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 4);

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


//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4362")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 02 - Contains Data Operator - Phone Call Date field is set - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod02()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 02";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 02 Activated - Contains Data", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4363")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 02 - Contains Data Operator - Phone Call Date field is NOT set - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod02_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 02";
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

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4364")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 03 - Does Not Equal Operator - Phone Call Date field is not set to 01/08/2020 - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod03()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 03";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 2);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 03 Activated - Does Not Equal", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4365")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 03 - Does Not Equal Operator - Phone Call Date field is set to 01/08/2020 - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod03_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 03";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);

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

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4366")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 04 - Not Between Operator - Phone Call Date field is not between to 06/07/2020 and 09/07/2020 (set date to 05/07/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod04()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 04";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 5);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 04 Activated - Not Between", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4367")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 04 - Not Between Operator - Phone Call Date field is not between to 06/07/2020 and 09/07/2020 (set date to 10/07/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod04_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 04";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 10);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 04 Activated - Not Between", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4368")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 04 - Contains Data Operator - Phone Call Date field is between to 06/07/2020 and 09/07/2020 (set date to 06/07/2020) - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod04_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 04";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 6);

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

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4369")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 04 - Contains Data Operator - Phone Call Date field is between to 06/07/2020 and 09/07/2020 (set date to 07/07/2020) - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod04_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 04";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 7);

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

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4370")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 04 - Contains Data Operator - Phone Call Date field is between to 06/07/2020 and 09/07/2020 (set date to 09/07/2020) - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod04_4()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 04";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 9);

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

//        //}


//        //[TestProperty("JiraIssueID", "CDV6-4371")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 05 - Is Greater Than Operator - Phone Call Date field is greater than 01/08/2020 (set date to 02/08/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod05()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 05";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 2);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 05 Activated - Is Greater Than", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4372")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 05 - Is Greater Than Operator - Phone Call Date field equals 01/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod05_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 05";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);

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

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4373")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 05 - Is Greater Than Operator - Phone Call Date field smaller than 01/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod05_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 05";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 1);

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

//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4374")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 06 - Is Greater Than or Equal To Operator - Phone Call Date field is greater than 02/08/2020 (set date to 03/08/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod06()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 06";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 3);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 06 Activated - Is Greater Than or Equal To", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4375")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 06 - Is Greater Than or Equal To  Operator - Phone Call Date field equals 02/08/2020  - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod06_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 06";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 2);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 06 Activated - Is Greater Than or Equal To", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4376")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 06 - Is Greater Than or Equal To  Operator - Phone Call Date field smaller than 02/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod06_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 06";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);

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

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4377")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 07 - Is Less Than Operator - Phone Call Date field is smaller than 01/08/2020 (set date to 01/07/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod07()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 07";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 07 Activated - Is Less Than", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4378")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 07 - Is Less Than Operator - Phone Call Date field equals 01/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod07_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 07";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);

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

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4379")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 07 - Is Less Than Operator - Phone Call Date field bigger than 01/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod07_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 07";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 2);

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

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4380")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 08 - Is Less Than Or Equal To Operator - Phone Call Date field is smaller than 02/08/2020 (set date to 01/08/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod08()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 08";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 08 Activated - Is Less Than or Equal To", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4381")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 08 - Is Less Than Or Equal To Operator - Phone Call Date field equals 02/08/2020  - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod08_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 08";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 2);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 08 Activated - Is Less Than or Equal To", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4382")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 08 - Is Less Than Or Equal To Operator - Phone Call Date field bigger than 02/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod08_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 08";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 3);

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

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4383")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 09 - In Future Operator - Phone Call Date field is set to tomorrow´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod09()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 09";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 09 Activated - In Future", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4384")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 09 - In Future To Operator - Phone Call Date field is set to yesterday´s date  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod09_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 09";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);

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

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4385")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 10 - In Past Operator - Phone Call Date field is set to Yesterday´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod10()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 10";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 10 Activated - In Past", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4386")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 10 - In Past To Operator - Phone Call Date field is set to Tomorrow´s date  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod10_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 10";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);

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
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4387")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 11 - Is Greater Than Today's Date Operator - Phone Call Date field is set to tomorrow´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod11()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 11";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(2);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 11 Activated - Is Greater Than Today´s Date", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4388")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 11 - Is Greater Than Today's Date To Operator - Phone Call Date field is set to yesterday´s date  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod11_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 11";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);

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

//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4389")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 12 - Is Less Than Today's Date Operator - Phone Call Date field is set to Yesterday´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod12()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 12";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 12 Activated - Is Less Than Today´s Date", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4390")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 12 - Is Less Than Today's Date To Operator - Phone Call Date field is set to Tomorrow´s date  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod12_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 12";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);

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
//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4391")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 13 - Last N Days Operator - Phone Call Date field is set to Yesterday´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod13()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 13";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 13 Activated - Last N Days", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4392")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 13 - Last N Days Operator - Phone Call Date field is set to 3 days in the past - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod13_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 13";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-3);

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
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4393")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 13 - Last N Days Operator - Phone Call Date field is set to 1 day in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod13_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 13";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);

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
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4394")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 14 - Last N Months Operator - Phone Call Date field is set to 1 month in the past - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod14()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 14";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddMonths(-1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 14 Activated - Last N Months", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4395")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 14 - Last N Months Operator - Phone Call Date field is set to 3 months in the past - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod14_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 14";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddMonths(-3);

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
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4396")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 14 - Last N Months Operator - Phone Call Date field is set to 1 day in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod14_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 14";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);

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
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4397")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 15 - Last N Years Operator - Phone Call Date field is set to 1 year in the past - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod15()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 15";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddYears(-1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 15 Activated - Last N Years", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4398")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 15 - Last N Years Operator - Phone Call Date field is set to 3 years in the past - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod15_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 15";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddYears(-3);

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
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4399")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 15 - Last N Years Operator - Phone Call Date field is set to 1 day in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod15_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 15";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);

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
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4400")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 16 - Next N Days Operator - Phone Call Date field is set to Tomorrow´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod16()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 16";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 16 Activated - Next N Days", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4401")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 16 - Next N Days Operator - Phone Call Date field is set to 3 days in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod16_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 16";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(3);

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
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4402")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 16 - Next N Days Operator - Phone Call Date field is set to 1 day in the past - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod16_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 16";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);

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
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4403")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 17 - Next N Months Operator - Phone Call Date field is set to Tomorrow´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod17()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 17";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddMonths(1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 17 Activated - Next N Months", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4404")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 17 - Next N Months Operator - Phone Call Date field is set to 3 months in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod17_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 17";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddMonths(3);

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
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4405")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 17 - Next N Months Operator - Phone Call Date field is set to 1 day in the past - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod17_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 17";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);

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
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4406")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 18 - Next N Years Operator - Phone Call Date field is set to Tomorrow´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod18()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 18";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddYears(1);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 18 Activated - Next N Years", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4407")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 18 - Next N Years Operator - Phone Call Date field is set to 3 Years in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod18_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 18";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddYears(3);

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
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4408")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 18 - Next N Years Operator - Phone Call Date field is set to 1 day in the past - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod18_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 18";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);

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
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4409")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 19 - Today Operator - Phone Call Date field is set to Tomorrow´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod19()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 19";
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
//        //    DateTime? PhoneCallDate = DateTime.Now;

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 19 Activated - Today", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4410")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 19 - Today Operator - Phone Call Date field is set to 1 day in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod19_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 19";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);

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
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4411")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 19 - Today Operator - Phone Call Date field is set to 1 day in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod19_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 19";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.AddDays(1);

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
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4412")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 20 - After N Days Operator - Phone Call Date field is set to 3 days in the future - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod20()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 20";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.AddDays(2);

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 20 Activated - After N Days", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4413")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Scenario 20 - After N Days Operator - Phone Call Date field is set to 1 day in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_TestMethod20_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync - Scenario 20";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);

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
//        //}

//        //#endregion

//        #region DateField

//        //[TestProperty("JiraIssueID", "CDV6-4460")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 01 - Does Not Contain Data Operator - Phone Call Date field does not contains data - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod01()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 01";
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
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing':  WF Testing - PreSync - Scenario 01 Activated - (DateField) Does Not Contain Data - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4461")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 01 - Equals Operator - Phone Call Date field equals 01/08/2020 - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod01_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 01";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 01 Activated - (DateField) Equals - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4462")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 01 - Between Operator - Phone Call Date field between 01/07/2020 and 03/07/2020 (set date to 01/07/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod01_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 01";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 7, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 01 - (DateField) Between - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4463")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 01 - Between Operator - Phone Call Date field between 01/07/2020 and 03/07/2020 (set date to 02/07/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod01_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 01";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 2);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 7, 2);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 01 - (DateField) Between - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4464")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 01 - Between Operator - Phone Call Date field between 01/07/2020 and 03/07/2020 (set date to 03/07/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod01_4()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 01";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 3);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 7, 3);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 01 - (DateField) Between - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4465")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 01 - Between Operator - Phone Call Date field smaller than 01/07/2020 (set date to 25/06/2020) - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod01_5()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 01";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 6, 25);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 6, 25);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4466")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 01 - Between Operator - Phone Call Date field greater than 03/07/2020 (set date to 04/07/2020) - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod01_6()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 01";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 4);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 7, 4);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);


//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4467")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 02 - Contains Data Operator - Phone Call Date field is set - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod02()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 02";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 02 Activated - (DateField) Contains Data - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4468")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 02 - Contains Data Operator - Phone Call Date field is NOT set - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod02_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 02";
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
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4469")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 03 - Does Not Equal Operator - Phone Call Date field is not set to 01/08/2020 - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod03()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 03";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 2);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 2);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 03 Activated - (DateField) Does Not Equal - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4470")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 03 - Does Not Equal Operator - Phone Call Date field is set to 01/08/2020 - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod03_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 03";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4471")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 04 - Not Between Operator - Phone Call Date field is not between to 06/07/2020 and 09/07/2020 (set date to 05/07/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod04()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 04";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 5);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 7, 5);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 04 Activated - (DateField) Not Between - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4472")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 04 - Not Between Operator - Phone Call Date field is not between to 06/07/2020 and 09/07/2020 (set date to 10/07/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod04_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 04";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 10);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 7, 10);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 04 Activated - (DateField) Not Between - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4473")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 04 - Contains Data Operator - Phone Call Date field is between to 06/07/2020 and 09/07/2020 (set date to 06/07/2020) - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod04_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 04";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 6);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 7, 6);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4474")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 04 - Contains Data Operator - Phone Call Date field is between to 06/07/2020 and 09/07/2020 (set date to 07/07/2020) - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod04_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 04";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 7);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 7, 7);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4475")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 04 - Contains Data Operator - Phone Call Date field is between to 06/07/2020 and 09/07/2020 (set date to 09/07/2020) - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod04_4()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 04";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 9);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 7, 9);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}


//        //[TestProperty("JiraIssueID", "CDV6-4476")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 05 - Is Greater Than Operator - Phone Call Date field is greater than 01/08/2020 (set date to 02/08/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod05()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 05";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 2);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 2);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 05 Activated - (DateField) Is Greater Than - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4477")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 05 - Is Greater Than Operator - Phone Call Date field equals 01/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod05_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 05";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4478")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 05 - Is Greater Than Operator - Phone Call Date field smaller than 01/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod05_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 05";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 7, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4479")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 06 - Is Greater Than or Equal To Operator - Phone Call Date field is greater than 02/08/2020 (set date to 03/08/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod06()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 06";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 3);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 3);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 06 Activated - (DateField) Is Greater Than or Equal To - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4480")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 06 - Is Greater Than or Equal To  Operator - Phone Call Date field equals 02/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod06_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 06";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 2);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 2);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 06 Activated - (DateField) Is Greater Than or Equal To - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4481")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 06 - Is Greater Than or Equal To  Operator - Phone Call Date field smaller than 02/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod06_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 06";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4482")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 07 - Is Less Than Operator - Phone Call Date field is smaller than 01/08/2020 (set date to 01/07/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod07()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 07";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 7, 1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 7, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 07 Activated - (DateField) Is Less Than - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4483")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 07 - Is Less Than Operator - Phone Call Date field equals 01/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod07_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 07";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4484")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 07 - Is Less Than Operator - Phone Call Date field bigger than 01/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod07_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 07";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 2);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 2);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4485")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 08 - Is Less Than Or Equal To Operator - Phone Call Date field is smaller than 02/08/2020 (set date to 01/08/2020) - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod08()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 08";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 08 Activated - (DateField) Is Less Than or Equal To - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4486")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 08 - Is Less Than Or Equal To Operator - Phone Call Date field equals 02/08/2020  - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod08_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 08";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 2);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 2);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 08 Activated - (DateField) Is Less Than or Equal To - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4487")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 08 - Is Less Than Or Equal To Operator - Phone Call Date field bigger than 02/08/2020  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod08_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 08";
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
//        //    DateTime? PhoneCallDate = new DateTime(2020, 8, 3);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = new DateTime(2020, 8, 3);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4488")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 09 - In Future Operator - Phone Call Date field is set to tomorrow´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod09()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 09";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 09 Activated - (DateField) In Future - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4489")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 09 - In Future To Operator - Phone Call Date field is set to yesterday´s date  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod09_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 09";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(-1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record

//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4490")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 10 - In Past Operator - Phone Call Date field is set to Yesterday´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod10()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 10";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(-1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 10 Activated - (DateField) In Past - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4491")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 10 - In Past To Operator - Phone Call Date field is set to Tomorrow´s date  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod10_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 10";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4492")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 11 - Is Greater Than Today's Date Operator - Phone Call Date field is set to tomorrow´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod11()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 11";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 11 Activated - (DateField) Is Greater Than Today´s Date - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4493")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 11 - Is Greater Than Today's Date To Operator - Phone Call Date field is set to yesterday´s date  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod11_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 11";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(-1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record

//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4494")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 12 - Is Less Than Today's Date Operator - Phone Call Date field is set to Yesterday´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod12()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 12";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(-1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 12 Activated - (DateField) Is Less Than Today´s Date - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4495")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 12 - Is Less Than Today's Date To Operator - Phone Call Date field is set to Tomorrow´s date  - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod12_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 12";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}



//        //[TestProperty("JiraIssueID", "CDV6-4496")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 13 - Last N Days Operator - Phone Call Date field is set to Yesterday´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod13()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 13";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(-1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 13 Activated - (DateField) Last N Days - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4497")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 13 - Last N Days Operator - Phone Call Date field is set to 3 days in the past - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod13_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 13";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-3);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(-3);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4498")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 13 - Last N Days Operator - Phone Call Date field is set to 1 day in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod13_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 13";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4499")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 14 - Last N Months Operator - Phone Call Date field is set to 1 month in the past - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod14()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 14";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddMonths(-1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddMonths(-1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 14 Activated - (DateField) Last N Months - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4500")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 14 - Last N Months Operator - Phone Call Date field is set to 3 months in the past - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod14_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 14";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddMonths(-3);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddMonths(-3);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4501")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 14 - Last N Months Operator - Phone Call Date field is set to 1 day in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod14_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 14";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4502")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 15 - Last N Years Operator - Phone Call Date field is set to 1 year in the past - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod15()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 15";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddYears(-1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddYears(-1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 15 Activated - (DateField) Last N Years - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4503")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 15 - Last N Years Operator - Phone Call Date field is set to 3 years in the past - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod15_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 15";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddYears(-3);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddYears(-3);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4504")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 15 - Last N Years Operator - Phone Call Date field is set to 1 day in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod15_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 15";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4505")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 16 - Next N Days Operator - Phone Call Date field is set to Tomorrow´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod16()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 16";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 16 Activated - (DateField) Next N Days - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4506")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 16 - Next N Days Operator - Phone Call Date field is set to 3 days in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod16_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 16";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(3);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(3);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4507")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 16 - Next N Days Operator - Phone Call Date field is set to 1 day in the past - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod16_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 16";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(-1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4508")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 17 - Next N Months Operator - Phone Call Date field is set to Tomorrow´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod17()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 17";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddMonths(1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddMonths(1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 17 Activated - (DateField) Next N Months - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4509")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 17 - Next N Months Operator - Phone Call Date field is set to 3 months in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod17_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 17";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddMonths(3);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddMonths(3);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4510")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 17 - Next N Months Operator - Phone Call Date field is set to 1 day in the past - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod17_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 17";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(-1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4511")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 18 - Next N Years Operator - Phone Call Date field is set to Tomorrow´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod18()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 18";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddYears(1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddYears(1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 18 Activated - (DateField) Next N Years - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4512")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 18 - Next N Years Operator - Phone Call Date field is set to 3 Years in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod18_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 18";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddYears(3);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddYears(3);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4513")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 18 - Next N Years Operator - Phone Call Date field is set to 1 day in the past - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod18_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 18";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(-1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4514")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 19 - Today Operator - Phone Call Date field is set to Tomorrow´s date - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod19()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 19";
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
//        //    DateTime? PhoneCallDate = DateTime.Now;
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now;
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 19 Activated - (DateField) Today - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4515")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 19 - Today Operator - Phone Call Date field is set to 1 day in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod19_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 19";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(-1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4516")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 19 - Today Operator - Phone Call Date field is set to 1 day in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod19_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 19";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.AddDays(1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.AddDays(1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}




//        //[TestProperty("JiraIssueID", "CDV6-4517")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 20 - After N Days Operator - Phone Call Date field is set to 3 days in the future - Workflow should be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod20()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 20";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.AddDays(2);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.AddDays(2).Date;
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

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
//        //        Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        Assert.AreEqual("UnknownError - 'WF Automated Testing - Pre-Sync Workflow testing': WF Testing - PreSync - Scenario 20 Activated - (DateField) After N Days - ", ex.Message);
//        //        return;
//        //    }

//        //    Assert.Fail("Expected message was not displayed.");
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-4518")]
//        //[TestMethod]
//        //[Description("Automation Script for Pre-Sync workflows - Date Field - Scenario 20 - After N Days Operator - Phone Call Date field is set to 1 day in the future - Workflow should NOT be triggered by creation of the phone call record")]
//        //public void WorkflowDetailsSection_DateField_TestMethod20_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Testing - PreSync (DateField) - Scenario 20";
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
//        //    DateTime? PhoneCallDate = DateTime.Now.Date.AddDays(-1);
//        //    bool IsSignificantEvent = true;
//        //    DateTime? SignificantEventDate = DateTime.Now.Date.AddDays(-1);
//        //    Guid eventCathegoryID = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4");

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }

//        //    //ACT

//        //    //create the record
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);
//        //}

//        #endregion

//        #region https://advancedcsg.atlassian.net/browse/CDV6-8863

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 1 - trigger the pre-sync workflow on the record creation")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod01_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-8863 - Scenario 1";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
            
//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, 
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate, 
//        //        null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //ASSERT
//        //    var expectedresponsibleUser = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4"); //Workflow_Test_User_0
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "responsibleuserid");
//        //    Assert.AreEqual(expectedresponsibleUser, fields["responsibleuserid"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 1 - trigger the pre-sync workflow on the record update")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod01_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "Phone Call Subject ...";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //assert that the responsible user is not set yet
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "responsibleuserid");
//        //    Assert.AreEqual(null, fields["responsibleuserid"]);


//        //    //Update the record subject
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "CDV6-8863 - Scenario 1");


//        //    //ASSERT
//        //    var expectedresponsibleUser = new Guid("1AB2F044-0352-E911-A2C5-005056926FE4"); //Workflow_Test_User_0
//        //    fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "responsibleuserid");
//        //    Assert.AreEqual(expectedresponsibleUser, fields["responsibleuserid"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 2 - trigger the pre-sync workflow on the record creation")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod02_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-8863 - Scenario 2";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record

//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //ASSERT
//        //    var expectedresponsibleUser = new Guid("ce0b6a33-66ef-e911-a2c7-005056926fe4"); //ALBERT Einstein
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "responsibleuserid");
//        //    Assert.AreEqual(expectedresponsibleUser, fields["responsibleuserid"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 2 - trigger the pre-sync workflow on the record update")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod02_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "Phone Call Subject ...";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //assert that the responsible user is not set yet
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "responsibleuserid");
//        //    Assert.AreEqual(null, fields["responsibleuserid"]);

//        //    //Update the record subject
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "CDV6-8863 - Scenario 2");

//        //    //ASSERT
//        //    var expectedresponsibleUser = new Guid("ce0b6a33-66ef-e911-a2c7-005056926fe4"); //ALBERT Einstein
//        //    fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "responsibleuserid");
//        //    Assert.AreEqual(expectedresponsibleUser, fields["responsibleuserid"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 3 - trigger the pre-sync workflow on the record creation")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod03_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-8863 - Scenario 3";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record

//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //ASSERT
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "phonenumber", "directionid", "statusid", "subject", "notes", "informationbythirdparty", "iscasenote");
//        //    Assert.AreEqual("999888777", fields["phonenumber"]);
//        //    Assert.AreEqual(2, fields["directionid"]);
//        //    Assert.AreEqual(2, fields["statusid"]);
//        //    Assert.AreEqual("CDV6-8863 - Scenario 3 Activated", fields["subject"]);
//        //    Assert.AreEqual("<p>CDV6-8863 - Scenario 3&nbsp;- Desc - Activated</p>", fields["notes"]);
//        //    Assert.AreEqual(true, fields["informationbythirdparty"]);
//        //    Assert.AreEqual(true, fields["iscasenote"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 3 - trigger the pre-sync workflow on the record update")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod03_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "Phone Call Subject ...";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT

//        //    //create the record
//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //assert that the responsible user is not set yet
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "phonenumber", "directionid", "statusid", "subject", "notes", "informationbythirdparty", "iscasenote");
//        //    Assert.AreEqual("0987654321234", fields["phonenumber"]);
//        //    Assert.AreEqual(1, fields["directionid"]);
//        //    Assert.AreEqual(1, fields["statusid"]);
//        //    Assert.AreEqual("Phone Call Subject ...", fields["subject"]);
//        //    Assert.AreEqual(null, fields["notes"]);
//        //    Assert.AreEqual(false, fields["informationbythirdparty"]);
//        //    Assert.AreEqual(false, fields["iscasenote"]);

//        //    //Update the record subject
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "CDV6-8863 - Scenario 3");

//        //    //ASSERT
//        //    fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "phonenumber", "directionid", "statusid", "subject", "notes", "informationbythirdparty", "iscasenote");
//        //    Assert.AreEqual("999888777", fields["phonenumber"]);
//        //    Assert.AreEqual(2, fields["directionid"]);
//        //    Assert.AreEqual(2, fields["statusid"]);
//        //    Assert.AreEqual("CDV6-8863 - Scenario 3 Activated", fields["subject"]);
//        //    Assert.AreEqual("<p>CDV6-8863 - Scenario 3&nbsp;- Desc - Activated</p>", fields["notes"]);
//        //    Assert.AreEqual(true, fields["informationbythirdparty"]);
//        //    Assert.AreEqual(true, fields["iscasenote"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 4 - trigger the pre-sync workflow on the record creation")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod04_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-8863 - Scenario 4";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT
//        //    //create the record
//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //ASSERT
//        //    var expectedReason = new Guid("b9ec74e3-9c45-e911-a2c5-005056926fe4"); //First Response
//        //    var expectedPriority = new Guid("5246a13f-9d45-e911-a2c5-005056926fe4"); //Normal
//        //    var expectedCategory = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"); //Assessment
//        //    var expectedSubCategory = new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"); //Health Assessment

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "activityreasonid", "activitypriorityid", "activitycategoryid", "activitysubcategoryid");
//        //    Assert.AreEqual(expectedReason, fields["activityreasonid"]);
//        //    Assert.AreEqual(expectedPriority, fields["activitypriorityid"]);
//        //    Assert.AreEqual(expectedCategory, fields["activitycategoryid"]);
//        //    Assert.AreEqual(expectedSubCategory, fields["activitysubcategoryid"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 4 - trigger the pre-sync workflow on the record update")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod04_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "Phone call subject...";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT
//        //    //create the record
//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //validate that no field is updated yet
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "activityreasonid", "activitypriorityid", "activitycategoryid", "activitysubcategoryid");
//        //    Assert.AreEqual(null, fields["activityreasonid"]);
//        //    Assert.AreEqual(null, fields["activitypriorityid"]);
//        //    Assert.AreEqual(null, fields["activitycategoryid"]);
//        //    Assert.AreEqual(null, fields["activitysubcategoryid"]);


//        //    //Update the record
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "CDV6-8863 - Scenario 4");


//        //    //ASSERT
//        //    var expectedReason = new Guid("b9ec74e3-9c45-e911-a2c5-005056926fe4"); //First Response
//        //    var expectedPriority = new Guid("5246a13f-9d45-e911-a2c5-005056926fe4"); //Normal
//        //    var expectedCategory = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"); //Assessment
//        //    var expectedSubCategory = new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"); //Health Assessment

//        //    fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "activityreasonid", "activitypriorityid", "activitycategoryid", "activitysubcategoryid");
//        //    Assert.AreEqual(expectedReason, fields["activityreasonid"]);
//        //    Assert.AreEqual(expectedPriority, fields["activitypriorityid"]);
//        //    Assert.AreEqual(expectedCategory, fields["activitycategoryid"]);
//        //    Assert.AreEqual(expectedSubCategory, fields["activitysubcategoryid"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 5 - trigger the pre-sync workflow on the record creation")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod05_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-8863 - Scenario 5";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT
//        //    //create the record
//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //ASSERT
//        //    var expectedCallerID = new Guid("41c83de2-3946-42c4-890e-dd1a5bb13bb2");
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "callerid", "calleridtablename", "calleridname");
//        //    Assert.AreEqual(expectedCallerID, fields["callerid"]);
//        //    Assert.AreEqual("person", fields["calleridtablename"]);
//        //    Assert.AreEqual("Tameka Whitaker", fields["calleridname"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 5 - trigger the pre-sync workflow on the record creation")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod05_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "Phone call subject...";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //    {
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);
//        //    }


//        //    //ACT
//        //    //create the record
//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        null, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //check that no update happens
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "callerid", "calleridtablename", "calleridname");
//        //    Assert.AreEqual(callerID, fields["callerid"]);
//        //    Assert.AreEqual(callerIdTableName, fields["calleridtablename"]);
//        //    Assert.AreEqual(callerIDName, fields["calleridname"]);


//        //    //update the phone call
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "CDV6-8863 - Scenario 5");

//        //    //ASSERT
//        //    var expectedCallerID = new Guid("41c83de2-3946-42c4-890e-dd1a5bb13bb2");
//        //    fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "callerid", "calleridtablename", "calleridname");
//        //    Assert.AreEqual(expectedCallerID, fields["callerid"]);
//        //    Assert.AreEqual("person", fields["calleridtablename"]);
//        //    Assert.AreEqual("Tameka Whitaker", fields["calleridname"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 6 - trigger the pre-sync workflow on the record creation (Responsible User not set)")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod06_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-8863 - Scenario 6";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);


//        //    //ACT
//        //    //create the record
//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        null, "", null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //ASSERT
//        //    var expectedCallerID = new Guid("41c83de2-3946-42c4-890e-dd1a5bb13bb2");
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "callerid", "calleridtablename", "calleridname");
//        //    Assert.AreEqual(null, fields["callerid"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 6 - trigger the pre-sync workflow on the record creation (Responsible User is set)")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod06_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "CDV6-8863 - Scenario 6";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    Guid responsibleUserID = new Guid("32972024-0839-E911-A2C5-005056926FE4"); //José Brazeta

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);


//        //    //ACT
//        //    //create the record
//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        responsibleUserID, "José Brazeta", null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //ASSERT
//        //    var expectedCallerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "callerid", "calleridtablename", "calleridname");
//        //    Assert.AreEqual(expectedCallerID, fields["callerid"]);
//        //    Assert.AreEqual("systemuser", fields["calleridtablename"]);
//        //    Assert.AreEqual("José Brazeta", fields["calleridname"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 6 - trigger the pre-sync workflow on the record update (Responsible User not set)")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod06_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "phone call subject....";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);


//        //    //ACT
//        //    //create the record
//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        null, "", null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //validate that no update to the record took place
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "callerid", "calleridtablename", "calleridname");
//        //    Assert.AreEqual(callerID, fields["callerid"]);
//        //    Assert.AreEqual(callerIdTableName, fields["calleridtablename"]);
//        //    Assert.AreEqual(callerIDName, fields["calleridname"]);

//        //    //update the record
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "CDV6-8863 - Scenario 6");


//        //    //ASSERT
//        //    var expectedCallerID = new Guid("41c83de2-3946-42c4-890e-dd1a5bb13bb2");
//        //    fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "callerid", "calleridtablename", "calleridname");
//        //    Assert.AreEqual(null, fields["callerid"]);
//        //}

//        //[TestProperty("JiraIssueID", "")]
//        //[TestMethod]
//        //[Description("Test changes in issue CDV6-8863 - 'Update record and Set Default Values' action - Scenario 6 - trigger the pre-sync workflow on the record update (Responsible User is set)")]
//        //public void Add_action_to_presync_workflow_to_allow_defaults_to_be_set_TestMethod06_4()
//        //{
//        //    //ARRANGE 
//        //    string subject = "phone call sub...";
//        //    string description = "";

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

//        //    bool IsSignificantEvent = false;
//        //    DateTime? SignificantEventDate = null;
//        //    Guid? eventCathegoryID = null;

//        //    Guid responsibleUserID = new Guid("32972024-0839-E911-A2C5-005056926FE4"); //José Brazeta

//        //    //get all phone call records for the person
//        //    List<Guid> phoneCallIDs = phoenixPlatformServiceHelper.GetPhoneCallForPersonRecord(regardingID);

//        //    //Delete the records
//        //    foreach (Guid phonecall in phoneCallIDs)
//        //        phoenixPlatformServiceHelper.DeletePhoneCall(phonecall);


//        //    //ACT
//        //    //create the record
//        //    var phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName,
//        //        callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, PhoneCallDate,
//        //        responsibleUserID, null, false, 1, IsSignificantEvent, SignificantEventDate, eventCathegoryID);

//        //    //validate that no update to the record took place
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "callerid", "calleridtablename", "calleridname");
//        //    Assert.AreEqual(callerID, fields["callerid"]);
//        //    Assert.AreEqual(callerIdTableName, fields["calleridtablename"]);
//        //    Assert.AreEqual(callerIDName, fields["calleridname"]);

//        //    //update the record
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "CDV6-8863 - Scenario 6");

//        //    //ASSERT
//        //    var expectedCallerID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "callerid", "calleridtablename", "calleridname");
//        //    Assert.AreEqual(expectedCallerID, fields["callerid"]);
//        //    Assert.AreEqual("systemuser", fields["calleridtablename"]);
//        //    Assert.AreEqual("José Brazeta", fields["calleridname"]);
//        //}

//        #endregion


//        [Description("Method will return the name of all tests and the Description of each one")]
//        [TestMethod]
//        public void GetTestNames()
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.AppendLine("TestName,Description,JiraIssueID");

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

//                if (testMethod != null && propertyAttr != null && !string.IsNullOrEmpty((propertyAttr as TestPropertyAttribute).Value))
//                {
//                    //sb.AppendLine(method.Name + "," + descAttr.Description.Replace(",", ";") + "," + (propertyAttr as TestPropertyAttribute).Value);
//                    sb.AppendLine((propertyAttr as TestPropertyAttribute).Value);
//                    continue;
//                }
//                if (testMethod != null)
//                {
//                    sb.AppendLine(method.Name + "," + descAttr.Description.Replace(",", ";"));
//                    continue;
//                }
//                if (testMethod != null)
//                {
//                    sb.AppendLine(method.Name + "," + t.FullName + "." + method.Name);
//                    continue;
//                }

//            }

//            Console.WriteLine(sb.ToString());
//        }




//        //private TestContext testContextInstance;

//        //public TestContext TestContext
//        //{
//        //    get
//        //    {
//        //        return testContextInstance;
//        //    }
//        //    set
//        //    {
//        //        testContextInstance = value;
//        //    }
//        //}



//        //[TestCleanup()]
//        //public virtual void MyTestCleanup()
//        //{
//        //    string jiraIssueID = (string)this.TestContext.Properties["JiraIssueID"];

//        //    //if we have a jira id for the test then we will update its status in jira
//        //    if (jiraIssueID != null)
//        //    {
//        //        bool testPassed = this.TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;

//        //        var zapi = new AtlassianServiceAPI.Models.Zapi()
//        //        {
//        //            AccessKey = ConfigurationManager.AppSettings["AccessKey"],
//        //            SecretKey = ConfigurationManager.AppSettings["SecretKey"],
//        //            User = ConfigurationManager.AppSettings["User"],
//        //        };

//        //        var jiraAPI = new AtlassianServiceAPI.Models.JiraApi()
//        //        {
//        //            Authentication = ConfigurationManager.AppSettings["Authentication"],
//        //            JiraCloudUrl = ConfigurationManager.AppSettings["JiraCloudUrl"],
//        //            ProjectKey = ConfigurationManager.AppSettings["ProjectKey"]
//        //        };

//        //        AtlassianServicesAPI.AtlassianService atlassianService = new AtlassianServicesAPI.AtlassianService(zapi, jiraAPI);

//        //        string versionName = ConfigurationManager.AppSettings["CurrentVersionName"];

//        //        if (testPassed)
//        //            atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automated Testing Workflows", AtlassianServiceAPI.Models.JiraTestOutcome.Passed);
//        //        else
//        //            atlassianService.UpdateTestStatus(jiraIssueID, versionName, "Automated Testing Workflows", AtlassianServiceAPI.Models.JiraTestOutcome.Failed);


//        //    }

//        //}

//    }
//}
