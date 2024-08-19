//using System;
//using System.Linq;
//using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Text;
//using Phoenix.WorkflowTestFramework;
//using System.Configuration;

//namespace Phoenix.WorkflowTests.WorkflowDetailsSection
//{
//    [TestClass]
//    public class AsyncWorkflowDetailsSectionTests
//    {
//        public Phoenix.WorkflowTestFramework.PhoenixPlatformServiceHelper phoenixPlatformServiceHelper { get; set; }
//        readonly Guid AsyncTestWorkflowConditionsAndStepsID = new Guid("71E2AF9B-8480-E911-A2C5-005056926FE4"); //WF Async Workflow Testing - Validations of Conditions and Steps
//        readonly Guid AsyncTestWorkflowOperatorsInConditionsID = new Guid("0E008BF1-4881-E911-A2C5-005056926FE4"); //WF Async Workflow Testing - Validations of Conditions and Steps
//        readonly Guid AsyncTestWorkflowComplexScenariosID = new Guid("523ef4dd-f581-e911-a2c5-005056926fe4"); //WF Async Workflow Testing - Validations of Conditions and Steps

//        readonly Guid AsyncTestWorkflowValidateActions1ID = new Guid("29fd047f-2982-e911-a2c5-005056926fe4"); //WF Async Automated Testing - Validate Actions for Async workflow
//        readonly Guid AsyncTestWorkflowValidateActions2ID = new Guid("3b9cf153-c482-e911-a2c5-005056926fe4"); //WF Async Automated Testing - Validate Actions for Async workflow 2

//        //[TestInitialize]
//        //public void TestInitializationMethod()
//        //{
//        //    phoenixPlatformServiceHelper = new WorkflowTestFramework.PhoenixPlatformServiceHelper();
//        //    var authRequest = phoenixPlatformServiceHelper.GetAuthenticationRequest("Workflow_Test_User_0", "Passw0rd_!");
//        //    var authResponse = phoenixPlatformServiceHelper.AuthenticateUser(authRequest);
//        //    phoenixPlatformServiceHelper.SetServiceConnectionDataFromAuthenticationResponse(authResponse);
//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7476")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 28 - Test update record action")]
//        //public void WorkflowDetailsSection_TestMethod028()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 28";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID, AsyncTestWorkflowValidateActions1ID);

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
//        //    Assert.AreEqual("WF Async Testing - Scenario 28 - Action 1 Activated", (string)phoneCallFields["subject".ToLower()]);
//        //    Assert.AreEqual(new DateTime(2019, 3, 1, 9, 0, 0), (DateTime)phoneCallFields["PhoneCallDate".ToLower()]);

//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7477")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 29 - Test Assign record action")]
//        //public void WorkflowDetailsSection_TestMethod029()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 29";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID, AsyncTestWorkflowValidateActions1ID);

//        //    var phoneCallFields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "OwnerId", "OwningBusinessUnitId");

//        //    Guid blaenauGwentPrimaryHealthSocialWorkerTeamID = new Guid("214a6bd2-9adf-4b1c-a0c0-a123b58471bd");
//        //    Guid healthBusinessUnitID = new Guid("4567d62a-1039-e911-a2c5-005056926fe4");

//        //    Assert.AreEqual(blaenauGwentPrimaryHealthSocialWorkerTeamID, (Guid)phoneCallFields["OwnerId".ToLower()]);
//        //    Assert.AreEqual(healthBusinessUnitID, (Guid)phoneCallFields["OwningBusinessUnitId".ToLower()]);

//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7478")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 30 - Test Send Email record action")]
//        //public void WorkflowDetailsSection_TestMethod030()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 30";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID, AsyncTestWorkflowValidateActions1ID);


//        //    //get all Email records for the person
//        //    emailIDs = phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(regardingID);
//        //    Assert.AreEqual(1, emailIDs.Count);

//        //    //get the email info
//        //    var email = phoenixPlatformServiceHelper.GetEmailById(emailIDs[0],
//        //        "Subject", "RegardingID", "RegardingIDName", "RegardingIdTableName", "DueDate", "EmailFromId", "Notes",
//        //        "ActivityReasonId", "ActivityOutcomeId", "ActivityCategoryId", "ActivitySubCategoryId", "PersonId", "ResponsibleUserId");

//        //    Guid jbrazetaSystemUserID = new Guid("32972024-0839-E911-A2C5-005056926FE4");

//        //    Assert.AreEqual("WF Async Testing - Scenario 30 - Action 1 Activated", (string)email["subject".ToLower()]);
//        //    Assert.AreEqual(regardingID, (Guid)email["RegardingID".ToLower()]);
//        //    Assert.AreEqual(regardingName, (string)email["RegardingIDName".ToLower()]);
//        //    Assert.AreEqual("person", (string)email["RegardingIdTableName".ToLower()]);
//        //    Assert.AreEqual(new DateTime(2019, 03, 01, 9, 0, 0), (DateTime)email["DueDate".ToLower()]);
//        //    Assert.AreEqual(jbrazetaSystemUserID, (Guid)email["EmailFromId".ToLower()]);
//        //    Assert.AreEqual("Mail Description ...", (string)email["notes".ToLower()]);
//        //    Assert.AreEqual(new Guid("b9ec74e3-9c45-e911-a2c5-005056926fe4"), (Guid)email["ActivityReasonId".ToLower()]);
//        //    Assert.AreEqual(new Guid("4c2bec1c-9e45-e911-a2c5-005056926fe4"), (Guid)email["ActivityOutcomeId".ToLower()]);
//        //    Assert.AreEqual(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), (Guid)email["ActivityCategoryId".ToLower()]);
//        //    Assert.AreEqual(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), (Guid)email["ActivitySubCategoryId".ToLower()]);
//        //    Assert.AreEqual(regardingID, (Guid)email["PersonId".ToLower()]);
//        //    Assert.AreEqual(jbrazetaSystemUserID, (Guid)email["ResponsibleUserId".ToLower()]);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7479")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 31 - Test Start Wrokflow action")]
//        //public void WorkflowDetailsSection_TestMethod031()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 31";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID, AsyncTestWorkflowValidateActions1ID);


//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Testing - Scenario 31 - Action 1 Activated", (string)phoneCallfields["subject".ToLower()]);
//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7480")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 32 - Test Change Record Status action")]
//        //public void WorkflowDetailsSection_TestMethod032()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 32";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID, AsyncTestWorkflowValidateActions1ID);


//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "Inactive");
//        //    Assert.AreEqual(true, (bool)phoneCallfields["inactive".ToLower()]);
//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7481")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 33 - Test Stop Workflow action")]
//        //public void WorkflowDetailsSection_TestMethod033()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 33";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID, AsyncTestWorkflowValidateActions1ID);


//        //    //get all Email records for the person
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "subject");
//        //    Assert.AreEqual("WF Async Testing - Scenario 33", (string)phoneCallfields["subject".ToLower()]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7482")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 34 - Test Apply Data Restriction action")]
//        //public void WorkflowDetailsSection_TestMethod034()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 34";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID, AsyncTestWorkflowValidateActions1ID);



//        //    Guid expectedDataRestriction = new Guid("00ea02a5-2852-e911-a2c5-005056926fe4");
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "datarestrictionid");
//        //    Assert.AreEqual(expectedDataRestriction, (Guid)phoneCallfields["DataRestrictionId".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7483")]
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
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "WF Async Testing - Scenario 34");

//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID, AsyncTestWorkflowValidateActions1ID);



//        //    Guid expectedDataRestriction = new Guid("00ea02a5-2852-e911-a2c5-005056926fe4");
//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "DataRestrictionId");
//        //    Assert.AreEqual(expectedDataRestriction, (Guid)phoneCallfields["DataRestrictionId".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7484")]
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
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "WF Async Testing - Scenario 34.2");



//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID, AsyncTestWorkflowValidateActions1ID);


//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "DataRestrictionId");
//        //    Assert.AreEqual(dataRestrictionID, (Guid)phoneCallfields["DataRestrictionId".ToLower()]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7485")]
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
//        //    Guid phoneCallID = phoenixPlatformServiceHelper.CreatePhoneCallRecordForPerson(subject, description, callerID, callerIdTableName, callerIDName, recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, teamID, DateTime.Now, responsibleUserID);


//        //    //ACT

//        //    //Update the record
//        //    Guid dataRestrictionID = new Guid("66f587ed-2752-e911-a2c5-005056926fe4");
//        //    phoenixPlatformServiceHelper.RestrictPhoneCall(phoneCallID, dataRestrictionID);
//        //    phoenixPlatformServiceHelper.UpdatePhoneCallSubject(phoneCallID, "WF Async Testing - Scenario 34.3");



//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID, AsyncTestWorkflowValidateActions1ID);


//        //    var phoneCallfields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID, "DataRestrictionId");
//        //    Assert.AreEqual(dataRestrictionID, (Guid)phoneCallfields["DataRestrictionId".ToLower()]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations to Business Data Actions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7486")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 35 - Test Business Data Count action")]
//        //public void WorkflowDetailsSection_TestMethod035()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 35";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID2, AsyncTestWorkflowValidateActions1ID);

//        //    string phonecall1Description = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID1);
//        //    string phonecall2Description = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID2);

//        //    Assert.AreEqual("Sample Description ...", phonecall1Description);
//        //    Assert.AreEqual("3", phonecall2Description); //if the count operation succeded then the description field should be updated
//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7487")]
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
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2018, 1, 1), false);


//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(caseFormID, AsyncTestWorkflowValidateActions2ID);


//        //    //Separate assessment field should not be updated by the WF
//        //    var fields = phoenixPlatformServiceHelper.GetCaseFormByID(caseFormID, "startdate", "separateassessment");
//        //    Assert.AreEqual(new DateTime(2018, 1, 1), (DateTime)fields["startdate"]);
//        //    Assert.AreEqual(false, (bool)fields["separateassessment"]);

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7488")]
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
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2018, 1, 1), false);


//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(caseFormID, AsyncTestWorkflowValidateActions2ID);


//        //    //Separate assessment field should be updated by the WF
//        //    var fields = phoenixPlatformServiceHelper.GetCaseFormByID(caseFormID, "startdate", "separateassessment");
//        //    Assert.AreEqual(new DateTime(2018, 1, 1), (DateTime)fields["startdate"]);
//        //    Assert.AreEqual(true, (bool)fields["separateassessment"]);

//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7489")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 39 - Test Get Initiating User Action action (Initiating user is Workflow_Test_User_1)")]
//        //public void WorkflowDetailsSection_TestMethod039()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 39";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowValidateActions1ID);

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

//        //[TestProperty("JiraIssueID", "CDV6-7490")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 39.1 - Test Get Initiating User Action action (Initiating user is NOT Workflow_Test_User_1)")]
//        //public void WorkflowDetailsSection_TestMethod039_1()
//        //{

//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 39";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowValidateActions1ID);

//        //    Guid Workflow_Test_User_0UserID = new Guid("3D17AF27-F74E-E911-9C54-F8B156AF4F99");
//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "responsibleuserid");
//        //    Assert.IsNull(fields["responsibleuserid"]);
//        //}



//        //[TestProperty("JiraIssueID", "CDV6-7491")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 40 - Test Get Answer By Identifier action (Answers should activate WF actions)")]
//        //public void WorkflowDetailsSection_TestMethod040()
//        //{
//        //    //ARRANGE 

//        //    Guid caseFormID = new Guid("34241d9d-4152-e911-a2c5-005056926fe4");  //case form that will activate WF actions

//        //    //reset the case form fields
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2018, 2, 1), null, null, false, false, false, false);


//        //    //ACT

//        //    //set the date to activate the workflow
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2018, 3, 1), false);


//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(caseFormID, AsyncTestWorkflowValidateActions2ID);


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

//        //[TestProperty("JiraIssueID", "CDV6-7492")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 40.1 - Test Get Answer By Identifier action (Answers should NOT activate WF actions)")]
//        //public void WorkflowDetailsSection_TestMethod040_1()
//        //{
//        //    //ARRANGE 

//        //    Guid caseFormID = new Guid("a6bcd284-4352-e911-a2c5-005056926fe4");  //case form that will NOT activate WF actions

//        //    //reset the case form fields
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2018, 1, 1), null, null, false, false, false, false);


//        //    //ACT

//        //    //set the date to activate the workflow
//        //    phoenixPlatformServiceHelper.UpdateCaseFormRecord(caseFormID, new DateTime(2018, 3, 1), false);


//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(caseFormID, AsyncTestWorkflowValidateActions2ID);


//        //    //validate updates performed by the workflow (all if statments to the Get Answer By Identifier action should NOT be activated)
//        //    var fields = phoenixPlatformServiceHelper.GetCaseFormByID(caseFormID, "duedate", "ResponsibleUserID", "SeparateAssessment", "CarerDeclinedJointAssessment", "JointCarerAssessment", "NewPerson");
//        //    Assert.AreEqual(null, fields["duedate"]);
//        //    Assert.AreEqual(null, fields["ResponsibleUserID".ToLower()]);
//        //    Assert.AreEqual(false, (bool)fields["SeparateAssessment".ToLower()]);
//        //    Assert.AreEqual(false, (bool)fields["CarerDeclinedJointAssessment".ToLower()]);
//        //    Assert.AreEqual(false, (bool)fields["JointCarerAssessment".ToLower()]);
//        //    Assert.AreEqual(false, (bool)fields["NewPerson".ToLower()]);
//        //}


//        //[TestProperty("JiraIssueID", "CDV6-7493")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 41 - Test Add Days action")]
//        //public void WorkflowDetailsSection_TestMethod041()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 41";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowValidateActions1ID);


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

//        //[TestProperty("JiraIssueID", "CDV6-7494")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 41 - Test Add Days action - phone call date is null")]
//        //public void WorkflowDetailsSection_TestMethod041_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 41";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowValidateActions1ID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("01/01/0001 00:00:00", (string)fields["notes"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7495")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 42 - Test Get Higher Date action (event date is the bigger date)")]
//        //public void WorkflowDetailsSection_TestMethod042()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 42";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowValidateActions1ID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(SignificantEventDate.AddSeconds(10).ToString(), (string)fields["notes"]); //we need to add 10 seconds due to a issue with phoenix - //Some times while executing modified on adding on start date is greater than modified of due date, hence increase 10 second of due date to make start date always less than due date.
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7496")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 42.1 - Test Get Higher Date action (phone call date is the bigger date)")]
//        //public void WorkflowDetailsSection_TestMethod042_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 42";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowValidateActions1ID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(phoneCallDate.AddSeconds(10).ToString(), (string)fields["notes"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7497")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 42.1 - Test Get Higher Date action (phone call date is null)")]
//        //public void WorkflowDetailsSection_TestMethod042_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 42";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowValidateActions1ID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(SignificantEventDate.AddSeconds(10).ToString(), (string)fields["notes"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7498")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 42.1 - Test Get Higher Date action (Event date is null)")]
//        //public void WorkflowDetailsSection_TestMethod042_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 42";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowValidateActions1ID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(phoneCallDate.AddSeconds(10).ToString(), (string)fields["notes"]);
//        //}

//        //[TestProperty("JiraIssueID", "CDV6-7499")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 43 - Test Subtract Days action")]
//        //public void WorkflowDetailsSection_TestMethod043()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 43";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowValidateActions1ID);

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

//        //[TestProperty("JiraIssueID", "CDV6-7500")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 43 - Test Subtract Days action - phone call date is null")]
//        //public void WorkflowDetailsSection_TestMethod043_0()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 43";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowValidateActions1ID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("01/01/0001 00:00:00", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7501")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 46 - WF step with 2 inner condition statments")]
//        //public void WorkflowDetailsSection_TestMethod046()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 46";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID1);
//        //    Assert.AreEqual("WF Async Testing - Scenario 46 - Action 1 Activated", descriptionAfterSave);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7502")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 47 - WF step with 2 inner condition statments (2nd if statment not executing)")]
//        //public void WorkflowDetailsSection_TestMethod047()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 46";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    string descriptionAfterSave = phoenixPlatformServiceHelper.GetPhoneCallDescriptionField(phoneCallID1);
//        //    Assert.AreEqual("Sample Description ...", descriptionAfterSave);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7503")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 48 - Multiple Steps validation")]
//        //public void WorkflowDetailsSection_TestMethod048()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 48";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 48 - Action 1 Activated", (string)fields["notes"]);
//        //    Assert.AreEqual("01/02/2019 09:15:30 - 0987654321234", (string)fields["subject"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7504")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 49 - Multiple Steps validation")]
//        //public void WorkflowDetailsSection_TestMethod049()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 48";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = null;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 48 - Action 1 Activated", (string)fields["notes"]);
//        //    Assert.AreEqual("-", (string)fields["subject"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7505")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 50 - If Else statments validation")]
//        //public void WorkflowDetailsSection_TestMethod050()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 50 - 1";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 50 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7506")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 51 - If Else statments validation")]
//        //public void WorkflowDetailsSection_TestMethod051()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 50 - 2";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 50 - Action 2 Activated", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7507")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 52 - Validation for a Business Object Reference field")]
//        //public void WorkflowDetailsSection_TestMethod052()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 52";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 52 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7508")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 52 - Validation for a Business Object Reference field")]
//        //public void WorkflowDetailsSection_TestMethod053()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 52";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7509")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 54 - Validation for a boolean field")]
//        //public void WorkflowDetailsSection_TestMethod054()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 54";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 54 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7510")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 54 - Validation for a boolean field")]
//        //public void WorkflowDetailsSection_TestMethod055()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 54";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7511")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 56 - Validation for a Picklist field")]
//        //public void WorkflowDetailsSection_TestMethod056()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 56";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 56 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7512")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 56 - Validation for a Picklist field")]
//        //public void WorkflowDetailsSection_TestMethod057()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 56";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7513")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 60 - Validation for a Date field")]
//        //public void WorkflowDetailsSection_TestMethod060()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 60";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 60 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7514")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 60 - Validation for a Date field")]
//        //public void WorkflowDetailsSection_TestMethod061()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 60";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7515")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 62 - Validation for a DateTime field")]
//        //public void WorkflowDetailsSection_TestMethod062()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 62";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 62 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7516")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 62 - Validation for a DateTime field")]
//        //public void WorkflowDetailsSection_TestMethod063()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 62";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7517")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 64 - Validation for a MultiLookup field")]
//        //public void WorkflowDetailsSection_TestMethod064()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 64";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 64 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7518")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 64 - Validation for a MultiLookup field")]
//        //public void WorkflowDetailsSection_TestMethod065()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 64";
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
//        //    Guid phoneCallID1 = phoenixPlatformServiceHelper.CreatePhoneCallRecordForCase(subject, description, callerID, callerIdTableName, callerIDName,
//        //        recipientID, recipientIdTableName, recipientIDName, phoneNumber, regardingID, regardingName, personid, personidName, teamID, "CareDirector QA", DateTime.Now.WithoutMilliseconds(), null, null);


//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7519")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 66 - Validation for a Large Data Textbox field")]
//        //public void WorkflowDetailsSection_TestMethod066()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 66";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 66 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7520")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 66 - Validation for a Large Data Textbox field")]
//        //public void WorkflowDetailsSection_TestMethod067()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 66";
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual(null, (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7521")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 68 - Validation for a Phone field")]
//        //public void WorkflowDetailsSection_TestMethod068()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 68";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 68 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7522")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 68 - Validation for a Phone field")]
//        //public void WorkflowDetailsSection_TestMethod069()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 68";
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7523")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod070()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 70";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 70 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7524")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod071()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 70";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7525")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod071_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 70";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7526")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 70 - Validation for 'And' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod071_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 70";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7527")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 72 - Validation for 'Or' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod072()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 72";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 72 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7528")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 73 - Validation for 'Or' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod073()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 72";
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 72 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7529")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 74 - Validation for 'Or' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod074()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 72";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 72 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7530")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 75 - Validation for 'Or' condition in if statment")]
//        //public void WorkflowDetailsSection_TestMethod075()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 72";
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7531")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 76 - Condition for related business object")]
//        //public void WorkflowDetailsSection_TestMethod076()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 76";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 76 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7532")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 77 - Condition for related business object")]
//        //public void WorkflowDetailsSection_TestMethod077()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 76";
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}





//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7533")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 78 - Condition for related business object")]
//        //public void WorkflowDetailsSection_TestMethod078()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 78.1";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 78 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7534")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 79 - Condition for related business object")]
//        //public void WorkflowDetailsSection_TestMethod079()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 78.2";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 78 - Action 2 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Validations of Conditions and Steps
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7535")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 80 - Condition for related business object")]
//        //public void WorkflowDetailsSection_TestMethod080()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 78.3";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowConditionsAndStepsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



















//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7536")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 81 - Validate the 'Equals' operator")]
//        //public void WorkflowDetailsSection_TestMethod081()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 81";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phonecalldate = DateTime.Now.Date;
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 81 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7537")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 82 - Validate the 'Equals' operator")]
//        //public void WorkflowDetailsSection_TestMethod082()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 81";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phonecalldate = DateTime.Now.Date;
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 81 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7538")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 83 - Validate the 'Equals' operator")]
//        //public void WorkflowDetailsSection_TestMethod083()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 81";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phonecalldate = DateTime.Now.Date;
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7539")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 83 - Validate the 'Equals' operator")]
//        //public void WorkflowDetailsSection_TestMethod083_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 81";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phonecalldate = DateTime.Now.Date;
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7540")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 84 - Validate the 'Does Not Equal' operator")]
//        //public void WorkflowDetailsSection_TestMethod084()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 84";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    DateTime phonecalldate = DateTime.Now.Date;
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 84 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7541")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 84.1 - Validate the 'Does Not Equal' operator (responsible user field is null)")]
//        //public void WorkflowDetailsSection_TestMethod084_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 84";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team


//        //    DateTime phonecalldate = DateTime.Now.Date;
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7542")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 84 - Validate the 'Does Not Equal' operator")]
//        //public void WorkflowDetailsSection_TestMethod085()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 84";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    DateTime phonecalldate = DateTime.Now.Date;
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7543")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 86 - Validate the 'contains data' operator")]
//        //public void WorkflowDetailsSection_TestMethod086()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 86";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = DateTime.Now.Date;
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 86 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7544")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 87 - Validate the 'contains data' operator")]
//        //public void WorkflowDetailsSection_TestMethod087()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 86";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7545")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 88 - Validate the 'does not contains data' operator")]
//        //public void WorkflowDetailsSection_TestMethod088()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 88";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 88 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7546")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 89 - Validate the 'does not contains data' operator")]
//        //public void WorkflowDetailsSection_TestMethod089()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 88";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime phoneCallDate = DateTime.Now;



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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7547")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 97 - Validate the 'Like' operator")]
//        //public void WorkflowDetailsSection_TestMethod097()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 97";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 97 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7548")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 97.1 - Validate the 'Like' operator")]
//        //public void WorkflowDetailsSection_TestMethod097_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 97";
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 97 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7549")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 98 - Validate the 'Like' operator")]
//        //public void WorkflowDetailsSection_TestMethod098()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 97";
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7550")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 98.1 - Validate the 'Like' operator")]
//        //public void WorkflowDetailsSection_TestMethod098_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 97";
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7551")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 99 - Validate the 'Starts With' operator")]
//        //public void WorkflowDetailsSection_TestMethod099()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 99";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 99 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7552")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 100 - Validate the 'Starts With' operator")]
//        //public void WorkflowDetailsSection_TestMethod100()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 99";
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7553")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 100.1 - Validate the 'Starts With' operator")]
//        //public void WorkflowDetailsSection_TestMethod100_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 99";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = null;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7554")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 101 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod101()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 101 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7555")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 101.1 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod101_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 101 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7556")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 101.2 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod101_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 101 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7557")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 102 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod102()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7558")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 102.1 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod102_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7559")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 102.2 - Validate the 'Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod102_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 101";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7560")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 103 - Validate the 'Is Grated Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod103()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 103";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 103 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7561")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 104 - Validate the 'Is Grated Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod104()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 103";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7562")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 103.1 - Validate the 'Is Grated Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod104_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 103";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7563")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 103.1 - Validate the 'Is Grated Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod104_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 103";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7564")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 105 - Validate the 'In Future' operator")]
//        //public void WorkflowDetailsSection_TestMethod105()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 105";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 105 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7565")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 106 - Validate the 'In Future' operator")]
//        //public void WorkflowDetailsSection_TestMethod106()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 105";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(-1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7566")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 106.1 - Validate the 'In Future' operator")]
//        //public void WorkflowDetailsSection_TestMethod106_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 105";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7567")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 107 - Validate the 'Next N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod107()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 107";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 107 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7568")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 107.1 - Validate the 'Next N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod107_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 107";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(2).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 107 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7569")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 108 - Validate the 'Next N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod108()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 107";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(4).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7570")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 108.1 - Validate the 'Next N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod108_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 107";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(-1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7571")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 108.2 - Validate the 'Next N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod108_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 107";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7572")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 109 - Validate the 'Last N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod109()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 109";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddMonths(-1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 109 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7573")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 109.1 - Validate the 'Last N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod109_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 109";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddMonths(-2).AddDays(1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 109 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7574")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 109.2 - Validate the 'Last N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod109_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 109";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.Date.AddMonths(-2).AddDays(-1);

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7575")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 109.3 - Validate the 'Last N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod109_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 109";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7576")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 109.4 - Validate the 'Last N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod109_4()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 109";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7577")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 111 - Validate the 'Older Than N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod111()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 111";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddYears(-2).AddDays(-1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 111 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7578")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 112 - Validate the 'Older Than N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod112()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 111";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddYears(-2).AddDays(1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7579")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 112.1 - Validate the 'Older Than N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod112_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 111";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7580")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 112.2 - Validate the 'Older Than N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod112_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 111";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7581")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 113 - Validate the 'Today' operator")]
//        //public void WorkflowDetailsSection_TestMethod113()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 113";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.Date.AddHours(8);

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 113 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7582")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 114 - Validate the 'Today' operator")]
//        //public void WorkflowDetailsSection_TestMethod114()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 113";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(-1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7583")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 114.1 - Validate the 'Today' operator")]
//        //public void WorkflowDetailsSection_TestMethod114_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 113";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(1).Date.AddHours(7);

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7584")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 114.2 - Validate the 'Today' operator")]
//        //public void WorkflowDetailsSection_TestMethod114_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 113";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7585")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 115 - Validate the 'Not Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod115()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 115";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 115 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7586")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 115.1 - Validate the 'Not Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod115_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 115";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 115 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7587")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 116 - Validate the 'Not Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod116()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 115";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7588")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 116.1 - Validate the 'Not Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod116_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 115";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7589")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 116.2 - Validate the 'Not Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod116_2()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 115";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7590")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 116.3 - Validate the 'Not Between' operator")]
//        //public void WorkflowDetailsSection_TestMethod116_3()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 115";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7591")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 117 - Validate the 'Is Grated Than Or Equal To' operator")]
//        //public void WorkflowDetailsSection_TestMethod117()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 117";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 117 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7592")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 118 - Validate the 'Is Grated Than Or Equal To' operator")]
//        //public void WorkflowDetailsSection_TestMethod118()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 117";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 117 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7593")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 119 - Validate the 'Is Grated Than Or Equal To' operator")]
//        //public void WorkflowDetailsSection_TestMethod119()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 117";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7594")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 120 - Validate the 'Is Less Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod120()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 120";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 120 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7595")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 121 - Validate the 'Is Less Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod121()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 120";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7596")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 121.1 - Validate the 'Is Less Than' operator")]
//        //public void WorkflowDetailsSection_TestMethod121_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 120";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7597")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 122 - Validate the 'Is Less Than Or Equal To' operator")]
//        //public void WorkflowDetailsSection_TestMethod122()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 122";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 122 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7598")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 123 - Validate the 'Is Less Than Or Equal To' operator")]
//        //public void WorkflowDetailsSection_TestMethod123()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 122";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 122 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7599")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 124 - Validate the 'Is Less Than Or Equal To' operator")]
//        //public void WorkflowDetailsSection_TestMethod124()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 122";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7600")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 125 - Validate the 'In Past' operator")]
//        //public void WorkflowDetailsSection_TestMethod125()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 125";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(-1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 125 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7601")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 126 - Validate the 'In Past' operator")]
//        //public void WorkflowDetailsSection_TestMethod126()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 125";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddHours(2);

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7602")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 127 - Validate the 'Is Grated Than Today's Date' operator")]
//        //public void WorkflowDetailsSection_TestMethod127()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 127";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 127 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7603")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 128 - Validate the 'Is Grated Than Today's Date' operator")]
//        //public void WorkflowDetailsSection_TestMethod128()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 127";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7604")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 128.1 - Validate the 'Is Grated Than Today's Date' operator")]
//        //public void WorkflowDetailsSection_TestMethod128_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 127";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(-1);

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7605")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 129 - Validate the 'Is Less Than Today's Date' operator")]
//        //public void WorkflowDetailsSection_TestMethod129()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 129";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(-1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 129 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7606")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 130 - Validate the 'Is Less Than Today's Date' operator")]
//        //public void WorkflowDetailsSection_TestMethod130()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 129";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7607")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 131 - Validate the 'Last N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod131()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 131";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(-1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 131 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7608")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 132 - Validate the 'Last N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod132()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 131";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(-3).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7609")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 132.1 - Validate the 'Last N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod132_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 131";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7610")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 133 - Validate the 'Last N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod133()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 133";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddYears(-1).AddMonths(-6).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 133 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7611")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 134 - Validate the 'Last N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod134()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 133";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddYears(-2).AddMonths(-6).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7612")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 134.1 - Validate the 'Last N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod134_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 133";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7613")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 135 - Validate the 'Next N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod135()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 135";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddMonths(2).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 135 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7614")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 136 - Validate the 'Next N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod136()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 135";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddMonths(4).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7615")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 136.1 - Validate the 'Next N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod136_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 135";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(-1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7616")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 137 - Validate the 'Next N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod137()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 137";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddYears(1).AddMonths(6).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 137 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7617")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 138 - Validate the 'Next N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod138()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 137";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddYears(2).AddMonths(1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7618")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 138.1 - Validate the 'Next N Years' operator")]
//        //public void WorkflowDetailsSection_TestMethod138_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 137";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(-1).Date;

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7619")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 139 - Validate the 'Older Than N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod139()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 139";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(-3);

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 139 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7620")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 140 - Validate the 'Older Than N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod140()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 139";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(-1);

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7621")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 140.1 - Validate the 'Older Than N Days' operator")]
//        //public void WorkflowDetailsSection_TestMethod140_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 139";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(1);

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}



//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7622")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 141 - Validate the 'Older Than N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod141()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 141";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddMonths(-3);

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 141 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7623")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 142 - Validate the 'Older Than N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod142()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 141";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddMonths(-1).AddDays(-10);

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7624")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 142.1 - Validate the 'Older Than N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod142_1()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 141";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string callerIdTableName = "person";
//        //    string callerIDName = "Adolfo Abbott";

//        //    Guid recipientID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
//        //    string recipientIdTableName = "systemuser";
//        //    string recipientIDName = "José Brazeta";

//        //    string phoneNumber = "0987654321234";

//        //    Guid regardingID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
//        //    string regardingName = "Adolfo Abbott";

//        //    Guid teamID = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA Team

//        //    DateTime PhoneCallDate = DateTime.Now.AddDays(1);

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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}




//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7625")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 143 - Validate the 'Older Than N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod143()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 143";
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("WF Async Testing - Scenario 143 - Action 1 Activated", (string)fields["notes"]);
//        //}

//        ///// <summary>
//        ///// Workflow under test: WF Automated Testing - Testing the operators in conditions
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7626")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case - Scenario 144 - Validate the 'Older Than N Months' operator")]
//        //public void WorkflowDetailsSection_TestMethod144()
//        //{
//        //    //ARRANGE 
//        //    string subject = "WF Async Testing - Scenario 143";
//        //    string description = "Sample Description ...";
//        //    Guid callerID = new Guid("78A78172-6135-4D9F-9ABB-D079A12B253D");
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
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(phoneCallID1, AsyncTestWorkflowOperatorsInConditionsID);

//        //    var fields = phoenixPlatformServiceHelper.GetPhoneCallByID(phoneCallID1, "subject", "notes");
//        //    Assert.AreEqual("Sample Description ...", (string)fields["notes"]);
//        //}


















//        ///// <summary>
//        ///// 
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7627")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case 183 - Testing the use of a Create Action as 'Local Value' ")]
//        //public void WorkflowDetailsSection_TestMethod183()
//        //{
//        //    //ARRANGE 

//        //    Guid PersonId = new Guid("C4AB357E-B8A7-44F4-98DC-2DAA8E4CFD79"); //Dawn Abbott

//        //    //remove all forms for the person
//        //    foreach (var formid in phoenixPlatformServiceHelper.GetPersonFormForPerson(PersonId))
//        //        phoenixPlatformServiceHelper.DeletePersonForm(formid);

//        //    //remove all case notes for the person
//        //    foreach (var casenoteid in phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(PersonId))
//        //        phoenixPlatformServiceHelper.DeletePersonCaseNote(casenoteid);

//        //    /*remove all cases for the person*/
//        //    foreach (Guid CaseId in phoenixPlatformServiceHelper.GetCasesForPerson(PersonId))
//        //    {
//        //        foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteEmail(mailId);

//        //        foreach (var caseinvolvementid in phoenixPlatformServiceHelper.GetCaseInvolvementsForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseInvolvement(caseinvolvementid);

//        //        foreach (var casestatushistoryid in phoenixPlatformServiceHelper.GetCaseStatusHistoryForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseStatusHistory(casestatushistoryid);

//        //        foreach (var caseActionID in phoenixPlatformServiceHelper.GetCaseActionForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseAction(caseActionID);

//        //        foreach (var taskID in phoenixPlatformServiceHelper.GetTasksForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteTask(taskID);

//        //        foreach (var taskID in phoenixPlatformServiceHelper.GetHealthAppointmentsByCaseId(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteHealthAppointment(taskID);

//        //        foreach (var casestatushistoryid in phoenixPlatformServiceHelper.GetCaseStatusHistoryForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseStatusHistory(casestatushistoryid);

//        //        phoenixPlatformServiceHelper.DeleteCase(CaseId);
//        //    }


//        //    string PresentingNeedDetails = "WF Async Testing - PN - Scenario 183";
//        //    string AdditionalInformation = "WF Async Testing - Scenario 183";
//        //    DateTime StartDateTime = DateTime.Now;
//        //    DateTime ContactReceivedDateTime = DateTime.Now;
//        //    DateTime RequestReceivedDateTime = DateTime.Now;
//        //    DateTime CaseAcceptedDateTime = DateTime.Now;
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
//        //        OwnerId, ContactReceivedById, CommunityAndClinicTeamId, ResponsibleUserId, SecondaryCaseReasonId, CaseStatusId, ContactReasonId, PresentingPriorityId,
//        //        AdministrativeCategoryId, ServiceTypeRequestedId, PersonId, ProfessionalTypeId, DataFormId, ContactSourceId);





//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(CaseID, AsyncTestWorkflowComplexScenariosID);

//        //    var caseActions = phoenixPlatformServiceHelper.GetCaseActionForCase(CaseID);
//        //    var caseNotes = phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(PersonId);


//        //    Assert.AreEqual(1, caseActions.Count);
//        //    Assert.AreEqual(1, caseNotes.Count);



//        //    var caseActionFields = phoenixPlatformServiceHelper.GetCaseActionByID(caseActions[0], "personid", "responsibleuserid", "duedate", "casepriorityid", "actiondetails", "title");
//        //    Assert.AreEqual(PersonId, (Guid)caseActionFields["personid".ToString()]);
//        //    Assert.AreEqual(ResponsibleUserId, (Guid)caseActionFields["responsibleuserid".ToString()]);
//        //    Assert.AreEqual(null, caseActionFields["duedate".ToString()]);
//        //    Assert.AreEqual(null, caseActionFields["casepriorityid".ToString()]);
//        //    Assert.AreEqual(AdditionalInformation, (string)caseActionFields["actiondetails".ToString()]);



//        //    var casenoteFields = phoenixPlatformServiceHelper.GetPersonCaseNoteByID(caseNotes[0], "subject", "notes");
//        //    Assert.AreEqual("Case Note for Case Action --> Core Assessment", (string)casenoteFields["subject".ToString()]);
//        //    string expectedDescription = string.Format("In Progress <->  <-> {0} <-> Workflow Test User 0 <->  <-> {1} <-> In Progress <-> CareDirector QA", AdditionalInformation, (string)caseActionFields["title"]);
//        //    Assert.AreEqual(expectedDescription, (string)casenoteFields["notes".ToString()]);
//        //}

//        ///// <summary>
//        ///// 
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7628")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case 184 - Testing the use of a Create Action as 'Local Value' - Create action not executed")]
//        //public void WorkflowDetailsSection_TestMethod184()
//        //{
//        //    //ARRANGE 

//        //    Guid PersonId = new Guid("C4AB357E-B8A7-44F4-98DC-2DAA8E4CFD79"); //Dawn Abbott

//        //    //remove all forms for the person
//        //    foreach (var formid in phoenixPlatformServiceHelper.GetPersonFormForPerson(PersonId))
//        //        phoenixPlatformServiceHelper.DeletePersonForm(formid);

//        //    //remove all case notes for the person
//        //    foreach (var casenoteid in phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(PersonId))
//        //        phoenixPlatformServiceHelper.DeletePersonCaseNote(casenoteid);

//        //    /*remove all cases for the person*/
//        //    foreach (Guid CaseId in phoenixPlatformServiceHelper.GetCasesForPerson(PersonId))
//        //    {
//        //        foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteEmail(mailId);

//        //        foreach (var caseinvolvementid in phoenixPlatformServiceHelper.GetCaseInvolvementsForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseInvolvement(caseinvolvementid);

//        //        foreach (var casestatushistoryid in phoenixPlatformServiceHelper.GetCaseStatusHistoryForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseStatusHistory(casestatushistoryid);

//        //        foreach (var caseActionID in phoenixPlatformServiceHelper.GetCaseActionForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseAction(caseActionID);

//        //        foreach (var taskID in phoenixPlatformServiceHelper.GetTasksForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteTask(taskID);

//        //        foreach (var taskID in phoenixPlatformServiceHelper.GetHealthAppointmentsByCaseId(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteHealthAppointment(taskID);

//        //        phoenixPlatformServiceHelper.DeleteCase(CaseId);
//        //    }


//        //    string PresentingNeedDetails = "...";
//        //    string AdditionalInformation = "WF Async Testing - Scenario 183";
//        //    DateTime StartDateTime = DateTime.Now;
//        //    DateTime ContactReceivedDateTime = DateTime.Now;
//        //    DateTime RequestReceivedDateTime = DateTime.Now;
//        //    DateTime CaseAcceptedDateTime = DateTime.Now;
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
//        //        OwnerId, ContactReceivedById, CommunityAndClinicTeamId, ResponsibleUserId, SecondaryCaseReasonId, CaseStatusId, ContactReasonId, PresentingPriorityId,
//        //        AdministrativeCategoryId, ServiceTypeRequestedId, PersonId, ProfessionalTypeId, DataFormId, ContactSourceId);





//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(CaseID, AsyncTestWorkflowComplexScenariosID);


//        //    var caseActions = phoenixPlatformServiceHelper.GetCaseActionForCase(CaseID);
//        //    var caseNotes = phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(PersonId);


//        //    Assert.AreEqual(0, caseActions.Count);
//        //    Assert.AreEqual(1, caseNotes.Count);



//        //    var casenoteFields = phoenixPlatformServiceHelper.GetPersonCaseNoteByID(caseNotes[0], "subject", "notes");
//        //    Assert.AreEqual("Case Note for Case Action -->", (string)casenoteFields["subject".ToString()]);
//        //    string expectedDescription = string.Format("<->  <->  <->  <->  <->  <->  <->");
//        //    Assert.AreEqual(expectedDescription, (string)casenoteFields["notes".ToString()]);
//        //}

//        ///// <summary>
//        ///// 
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7629")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case 185 - Testing the use of a child record validation in a If statment - Child record contains data")]
//        //public void WorkflowDetailsSection_TestMethod185()
//        //{
//        //    //ARRANGE 

//        //    Guid PersonId = new Guid("C4AB357E-B8A7-44F4-98DC-2DAA8E4CFD79"); //Dawn Abbott

//        //    //remove all forms for the person
//        //    foreach (var formid in phoenixPlatformServiceHelper.GetPersonFormForPerson(PersonId))
//        //        phoenixPlatformServiceHelper.DeletePersonForm(formid);

//        //    //remove all case notes for the person
//        //    foreach (var casenoteid in phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(PersonId))
//        //        phoenixPlatformServiceHelper.DeletePersonCaseNote(casenoteid);

//        //    /*remove all cases for the person*/
//        //    foreach (Guid CaseId in phoenixPlatformServiceHelper.GetCasesForPerson(PersonId))
//        //    {
//        //        foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteEmail(mailId);

//        //        foreach (var caseinvolvementid in phoenixPlatformServiceHelper.GetCaseInvolvementsForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseInvolvement(caseinvolvementid);

//        //        foreach (var casestatushistoryid in phoenixPlatformServiceHelper.GetCaseStatusHistoryForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseStatusHistory(casestatushistoryid);

//        //        foreach (var caseActionID in phoenixPlatformServiceHelper.GetCaseActionForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseAction(caseActionID);

//        //        foreach (var taskID in phoenixPlatformServiceHelper.GetTasksForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteTask(taskID);

//        //        foreach (var taskID in phoenixPlatformServiceHelper.GetHealthAppointmentsByCaseId(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteHealthAppointment(taskID);

//        //        phoenixPlatformServiceHelper.DeleteCase(CaseId);
//        //    }


//        //    string PresentingNeedDetails = "...";
//        //    string AdditionalInformation = "Workflow Test";
//        //    DateTime StartDateTime = DateTime.Now;
//        //    DateTime ContactReceivedDateTime = DateTime.Now;
//        //    DateTime RequestReceivedDateTime = DateTime.Now;
//        //    DateTime CaseAcceptedDateTime = DateTime.Now;
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
//        //        OwnerId, ContactReceivedById, CommunityAndClinicTeamId, ResponsibleUserId, SecondaryCaseReasonId, CaseStatusId, ContactReasonId, PresentingPriorityId,
//        //        AdministrativeCategoryId, ServiceTypeRequestedId, PersonId, ProfessionalTypeId, DataFormId, ContactSourceId);


//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(CaseID, AsyncTestWorkflowComplexScenariosID);

//        //    var formIDs = phoenixPlatformServiceHelper.GetPersonFormForPerson(PersonId);
//        //    var caseNotes = phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(PersonId);

//        //    Assert.AreEqual(0, formIDs.Count);
//        //    Assert.AreEqual(0, caseNotes.Count);



//        //    //ACT

//        //    //Create a Case Action record
//        //    Guid caseActionTypeID = phoenixPlatformServiceHelper.GetCaseActionTypeByName("Core Assessment")[0];
//        //    phoenixPlatformServiceHelper.CreateCaseAction(OwnerId, PersonId, CaseID, caseActionTypeID, DateTime.Now.Date);

//        //    //Update the case additional details
//        //    phoenixPlatformServiceHelper.UpdateCaseAdditionalDetails(CaseID, "WF Async Testing - Scenario 185");


//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(CaseID, AsyncTestWorkflowComplexScenariosID);

//        //    formIDs = phoenixPlatformServiceHelper.GetPersonFormForPerson(PersonId);
//        //    caseNotes = phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(PersonId);

//        //    Assert.AreEqual(1, formIDs.Count);
//        //    Assert.AreEqual(0, caseNotes.Count);


//        //}



//        ///// <summary>
//        ///// 
//        ///// </summary>
//        //[TestProperty("JiraIssueID", "CDV6-7630")]
//        //[TestMethod]
//        //[Description("Automation Script for the Test Case 185 - Testing the use of a child record validation in a If statment - Child record does not contain data")]
//        //public void WorkflowDetailsSection_TestMethod186()
//        //{
//        //    //ARRANGE 

//        //    Guid PersonId = new Guid("C4AB357E-B8A7-44F4-98DC-2DAA8E4CFD79"); //Dawn Abbott

//        //    //remove all forms for the person
//        //    foreach (var formid in phoenixPlatformServiceHelper.GetPersonFormForPerson(PersonId))
//        //        phoenixPlatformServiceHelper.DeletePersonForm(formid);

//        //    //remove all case notes for the person
//        //    foreach (var casenoteid in phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(PersonId))
//        //        phoenixPlatformServiceHelper.DeletePersonCaseNote(casenoteid);

//        //    /*remove all cases for the person*/
//        //    foreach (Guid CaseId in phoenixPlatformServiceHelper.GetCasesForPerson(PersonId))
//        //    {
//        //        foreach (Guid mailId in phoenixPlatformServiceHelper.GetEmailsByRegardingIdField(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteEmail(mailId);

//        //        foreach (var caseinvolvementid in phoenixPlatformServiceHelper.GetCaseInvolvementsForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseInvolvement(caseinvolvementid);

//        //        foreach (var casestatushistoryid in phoenixPlatformServiceHelper.GetCaseStatusHistoryForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseStatusHistory(casestatushistoryid);

//        //        foreach (var caseActionID in phoenixPlatformServiceHelper.GetCaseActionForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteCaseAction(caseActionID);

//        //        foreach (var taskID in phoenixPlatformServiceHelper.GetTasksForCase(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteTask(taskID);

//        //        foreach (var taskID in phoenixPlatformServiceHelper.GetHealthAppointmentsByCaseId(CaseId))
//        //            phoenixPlatformServiceHelper.DeleteHealthAppointment(taskID);

//        //        phoenixPlatformServiceHelper.DeleteCase(CaseId);
//        //    }


//        //    string PresentingNeedDetails = "...";
//        //    string AdditionalInformation = "Workflow Test";
//        //    DateTime StartDateTime = DateTime.Now;
//        //    DateTime ContactReceivedDateTime = DateTime.Now;
//        //    DateTime RequestReceivedDateTime = DateTime.Now;
//        //    DateTime CaseAcceptedDateTime = DateTime.Now;
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
//        //        OwnerId, ContactReceivedById, CommunityAndClinicTeamId, ResponsibleUserId, SecondaryCaseReasonId, CaseStatusId, ContactReasonId, PresentingPriorityId,
//        //        AdministrativeCategoryId, ServiceTypeRequestedId, PersonId, ProfessionalTypeId, DataFormId, ContactSourceId);


//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(CaseID, AsyncTestWorkflowComplexScenariosID);

//        //    var formIDs = phoenixPlatformServiceHelper.GetPersonFormForPerson(PersonId);
//        //    var caseNotes = phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(PersonId);

//        //    Assert.AreEqual(0, formIDs.Count);
//        //    Assert.AreEqual(0, caseNotes.Count);



//        //    //ACT

//        //    //Update the case additional details
//        //    phoenixPlatformServiceHelper.UpdateCaseAdditionalDetails(CaseID, "WF Async Testing - Scenario 185");


//        //    //ASSERT
//        //    phoenixPlatformServiceHelper.WaitForWorkflowJobToFinish(CaseID, AsyncTestWorkflowComplexScenariosID);

//        //    formIDs = phoenixPlatformServiceHelper.GetPersonFormForPerson(PersonId);
//        //    caseNotes = phoenixPlatformServiceHelper.GetPersonCaseNoteForPerson(PersonId);

//        //    Assert.AreEqual(0, formIDs.Count);
//        //    Assert.AreEqual(1, caseNotes.Count);


//        //}


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
//                    if (!string.IsNullOrEmpty(propertyAttr.Value))
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
