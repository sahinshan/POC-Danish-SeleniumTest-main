using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.DBHelper.Models;
using Phoenix.WebAPIHelper.WebAppAPI.Entities.CareDirector;
using System;
using System.Linq;

namespace Phoenix.ApiTests
{
    [TestClass]
    public class API_TestMethods : BaseTestClass
    {
        #region https://advancedcsg.atlassian.net/browse/CDV6-15608

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission")]
        [TestProperty("JiraIssueID", "CDV6-16513")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_TestMethod01()
        {

            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, webAPIHelper.chie.access_token);


            //Assert
            var ofeliaLarsen_PersonId = new Guid("5a913a53-f8c9-4c3a-b726-02eef9a896da"); //332345
            var ofeliaConway_PersonId = new Guid("9f786e9c-4d68-4e9f-b8cc-5c13d5af519a"); //332346
            var ofeliaPratt_PersonId = new Guid("bdfbb7ad-713a-4adf-bc7a-7e847faeee3d"); //332347
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            Assert.AreEqual(false, personData.hasMoreRecords);

            int matchingPersonRecords = personData.data.Where(c => c.id.Equals(ofeliaLarsen_PersonId)).Count();
            Assert.AreEqual(1, matchingPersonRecords);

            matchingPersonRecords = personData.data.Where(c => c.id.Equals(ofeliaConway_PersonId)).Count();
            Assert.AreEqual(1, matchingPersonRecords);

            matchingPersonRecords = personData.data.Where(c => c.id.Equals(ofeliaPratt_PersonId)).Count();
            Assert.AreEqual(0, matchingPersonRecords);

            var personDetails = personData.data.Where(c => c.id.Equals(ofeliaLarsen_PersonId)).First();
            Assert.AreEqual("166427", personDetails.addressline1);
            Assert.AreEqual("Church Street, Quarrington Hill,", personDetails.addressline2);
            Assert.AreEqual("London dst", personDetails.addressline3);
            Assert.AreEqual("London", personDetails.addressline4);
            Assert.AreEqual("Greater London", personDetails.addressline5);
            Assert.AreEqual("1995-07-18", personDetails.dateofbirth);
            Assert.AreEqual("Ofelia Larsen", personDetails.fullname);
            Assert.AreEqual("2", personDetails.genderid.id);
            Assert.AreEqual("Female", personDetails.genderid.name);
            Assert.AreEqual("332345", personDetails.personnumber);
            Assert.AreEqual("BR4 0AG", personDetails.postcode);
            Assert.AreEqual("Larsen", personDetails.preferredname);
        }

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by first name")]
        [TestProperty("JiraIssueID", "CDV6-16514")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_TestMethod02()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "Ofelia", webAPIHelper.chie.access_token);


            //Assert
            var ofeliaLarsen_PersonId = new Guid("5a913a53-f8c9-4c3a-b726-02eef9a896da"); //332345
            var ofeliaConway_PersonId = new Guid("9f786e9c-4d68-4e9f-b8cc-5c13d5af519a"); //332346
            var ofeliaPratt_PersonId = new Guid("bdfbb7ad-713a-4adf-bc7a-7e847faeee3d"); //332347
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(2, personData.data.Count);

            int matchingPersonRecords = personData.data.Where(c => c.id.Equals(ofeliaLarsen_PersonId)).Count();
            Assert.AreEqual(1, matchingPersonRecords);

            matchingPersonRecords = personData.data.Where(c => c.id.Equals(ofeliaConway_PersonId)).Count();
            Assert.AreEqual(1, matchingPersonRecords);

            var personDetails = personData.data.Where(c => c.id.Equals(ofeliaLarsen_PersonId)).First();
            Assert.AreEqual("166427", personDetails.addressline1);
            Assert.AreEqual("Church Street, Quarrington Hill,", personDetails.addressline2);
            Assert.AreEqual("London dst", personDetails.addressline3);
            Assert.AreEqual("London", personDetails.addressline4);
            Assert.AreEqual("Greater London", personDetails.addressline5);
            Assert.AreEqual("1995-07-18", personDetails.dateofbirth);
            Assert.AreEqual("Ofelia Larsen", personDetails.fullname);
            Assert.AreEqual("2", personDetails.genderid.id);
            Assert.AreEqual("Female", personDetails.genderid.name);
            Assert.AreEqual("332345", personDetails.personnumber);
            Assert.AreEqual("BR4 0AG", personDetails.postcode);
            Assert.AreEqual("Larsen", personDetails.preferredname);
        }

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by First Name and Last Name")]
        [TestProperty("JiraIssueID", "CDV6-16515")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_TestMethod03()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "Ofelia", "Larsen", webAPIHelper.chie.access_token);


            //Assert
            var ofeliaLarsen_PersonId = new Guid("5a913a53-f8c9-4c3a-b726-02eef9a896da"); //332345
            var ofeliaConway_PersonId = new Guid("9f786e9c-4d68-4e9f-b8cc-5c13d5af519a"); //332346
            var ofeliaPratt_PersonId = new Guid("bdfbb7ad-713a-4adf-bc7a-7e847faeee3d"); //332347
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(1, personData.data.Count);

            int matchingPersonRecords = personData.data.Where(c => c.id.Equals(ofeliaLarsen_PersonId)).Count();
            Assert.AreEqual(1, matchingPersonRecords);

            var personDetails = personData.data.Where(c => c.id.Equals(ofeliaLarsen_PersonId)).First();
            Assert.AreEqual("166427", personDetails.addressline1);
            Assert.AreEqual("Church Street, Quarrington Hill,", personDetails.addressline2);
            Assert.AreEqual("London dst", personDetails.addressline3);
            Assert.AreEqual("London", personDetails.addressline4);
            Assert.AreEqual("Greater London", personDetails.addressline5);
            Assert.AreEqual("1995-07-18", personDetails.dateofbirth);
            Assert.AreEqual("Ofelia Larsen", personDetails.fullname);
            Assert.AreEqual("2", personDetails.genderid.id);
            Assert.AreEqual("Female", personDetails.genderid.name);
            Assert.AreEqual("332345", personDetails.personnumber);
            Assert.AreEqual("BR4 0AG", personDetails.postcode);
            Assert.AreEqual("Larsen", personDetails.preferredname);
        }

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by First Name, Last Name and Date of Birth")]
        [TestProperty("JiraIssueID", "CDV6-16516")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_TestMethod04()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "Ofelia", "Larsen", new DateTime(1995, 7, 18), webAPIHelper.chie.access_token);


            //Assert
            var ofeliaLarsen_PersonId = new Guid("5a913a53-f8c9-4c3a-b726-02eef9a896da"); //332345
            var ofeliaConway_PersonId = new Guid("9f786e9c-4d68-4e9f-b8cc-5c13d5af519a"); //332346
            var ofeliaPratt_PersonId = new Guid("bdfbb7ad-713a-4adf-bc7a-7e847faeee3d"); //332347
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(1, personData.data.Count);

            int matchingPersonRecords = personData.data.Where(c => c.id.Equals(ofeliaLarsen_PersonId)).Count();
            Assert.AreEqual(1, matchingPersonRecords);

            var personDetails = personData.data.Where(c => c.id.Equals(ofeliaLarsen_PersonId)).First();
            Assert.AreEqual("166427", personDetails.addressline1);
            Assert.AreEqual("Church Street, Quarrington Hill,", personDetails.addressline2);
            Assert.AreEqual("London dst", personDetails.addressline3);
            Assert.AreEqual("London", personDetails.addressline4);
            Assert.AreEqual("Greater London", personDetails.addressline5);
            Assert.AreEqual("1995-07-18", personDetails.dateofbirth);
            Assert.AreEqual("Ofelia Larsen", personDetails.fullname);
            Assert.AreEqual("2", personDetails.genderid.id);
            Assert.AreEqual("Female", personDetails.genderid.name);
            Assert.AreEqual("332345", personDetails.personnumber);
            Assert.AreEqual("BR4 0AG", personDetails.postcode);
            Assert.AreEqual("Larsen", personDetails.preferredname);
        }

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by First Name, Last Name, Date of Birth and Post Code")]
        [TestProperty("JiraIssueID", "CDV6-16517")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_TestMethod05()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "Ofelia", "Larsen", new DateTime(1995, 7, 18), "BR4 0AG", webAPIHelper.chie.access_token);


            //Assert
            var ofeliaLarsen_PersonId = new Guid("5a913a53-f8c9-4c3a-b726-02eef9a896da"); //332345
            var ofeliaConway_PersonId = new Guid("9f786e9c-4d68-4e9f-b8cc-5c13d5af519a"); //332346
            var ofeliaPratt_PersonId = new Guid("bdfbb7ad-713a-4adf-bc7a-7e847faeee3d"); //332347
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(1, personData.data.Count);

            int matchingPersonRecords = personData.data.Where(c => c.id.Equals(ofeliaLarsen_PersonId)).Count();
            Assert.AreEqual(1, matchingPersonRecords);

            var personDetails = personData.data.Where(c => c.id.Equals(ofeliaLarsen_PersonId)).First();
            Assert.AreEqual("166427", personDetails.addressline1);
            Assert.AreEqual("Church Street, Quarrington Hill,", personDetails.addressline2);
            Assert.AreEqual("London dst", personDetails.addressline3);
            Assert.AreEqual("London", personDetails.addressline4);
            Assert.AreEqual("Greater London", personDetails.addressline5);
            Assert.AreEqual("1995-07-18", personDetails.dateofbirth);
            Assert.AreEqual("Ofelia Larsen", personDetails.fullname);
            Assert.AreEqual("2", personDetails.genderid.id);
            Assert.AreEqual("Female", personDetails.genderid.name);
            Assert.AreEqual("332345", personDetails.personnumber);
            Assert.AreEqual("BR4 0AG", personDetails.postcode);
            Assert.AreEqual("Larsen", personDetails.preferredname);
        }

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by first name (No Match for the first name)")]
        [TestProperty("JiraIssueID", "CDV6-16519")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_TestMethod06()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "OfeliaNOMATCH", webAPIHelper.chie.access_token);


            //Assert
            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(0, personData.data.Count);

        }

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by First Name and Last Name (No Match for the Last Name)")]
        [TestProperty("JiraIssueID", "CDV6-16520")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_TestMethod07()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "Ofelia", "LarsenNOMATCH", webAPIHelper.chie.access_token);


            //Assert
            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(0, personData.data.Count);

        }

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by First Name, Last Name and Date of Birth (No Match for the Date of Birth)")]
        [TestProperty("JiraIssueID", "CDV6-16521")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_TestMethod08()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "Ofelia", "Larsen", new DateTime(2022, 1, 2), webAPIHelper.chie.access_token);


            //Assert
            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(0, personData.data.Count);

        }

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by First Name, Last Name, Date of Birth and Post Code (No Match for the Post Code)")]
        [TestProperty("JiraIssueID", "CDV6-16522")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_TestMethod09()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "Ofelia", "Larsen", new DateTime(1995, 7, 18), "NOMATCH", webAPIHelper.chie.access_token);


            //Assert
            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(0, personData.data.Count);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-15789

        [TestMethod]
        [Description("Use ISO 8601 format for date and date/time values in the API")]
        [TestProperty("JiraIssueID", "CDV6-16552")]
        [TestCategory("Phoenix.APITest")]
        public void ValidateDateTimeField_ISO_8601_TestMethod01()
        {
            //Arrange 
            var ofeliaLarsen_PersonId = new Guid("5a913a53-f8c9-4c3a-b726-02eef9a896da"); //332345

            //Act
            var contactsInfo = webAPIHelper.chie.GetPersonContacts(ofeliaLarsen_PersonId, webAPIHelper.chie.access_token);

            //Assert
            Assert.AreEqual("2022-03-09T07:35:00Z", contactsInfo.data[0].contactreceiveddatetime);

        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-16555

        [TestMethod]
        [Description("Validate that it is possible to create a contact attachment using the api")]
        [TestProperty("JiraIssueID", "CDV6-16766")]
        [TestCategory("Phoenix.APITest")]
        [DeploymentItem("Files/FileToUpload.jpg")]
        public void Contact_Attachments_API_TestMethod01()
        {
            //Arrange 

            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            var _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            var _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("SmokeTest_Ethnicity").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "SmokeTest_Ethnicity", new DateTime(2020, 1, 1));
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("SmokeTest_Ethnicity")[0];

            #endregion

            #region ContactType

            var ContactTypeExist = dbHelper.contactType.GetByName("Adult Safeguarding").Any();
            if (!ContactTypeExist)
                dbHelper.contactType.CreateContactType(_careDirectorQA_TeamId, "Adult Safeguarding", DateTime.Now.Date.AddYears(-1), true);
            var _contactTypeId = dbHelper.contactType.GetByName("Adult Safeguarding")[0];

            #endregion

            #region Contact Reason

            var contactReasonExists = dbHelper.contactReason.GetByName("Adoption Enquiry").Any();
            if (!contactReasonExists)
                dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "Adoption Enquiry", new DateTime(2020, 1, 1), 110000000, false);
            var _contactReasonId = dbHelper.contactReason.GetByName("Adoption Enquiry")[0];

            #endregion

            #region ContactPresentingPriority

            var contactPresentingPriorityExists = dbHelper.contactPresentingPriority.GetByName("Routine").Any();
            if (!contactPresentingPriorityExists)
                dbHelper.contactPresentingPriority.CreateContactPresentingPriority(_careDirectorQA_TeamId, "Routine", "2", "2", new DateTime(2020, 1, 1));
            var _contactPresentingPriorityId = dbHelper.contactPresentingPriority.GetByName("Routine")[0];

            #endregion

            #region ContactStatus

            var ContactStatusExists = dbHelper.contactStatus.GetByName("New Contact").Any();
            if (!ContactStatusExists)
                dbHelper.contactStatus.CreateContactStatus(_careDirectorQA_TeamId, "New Contact", "2", new DateTime(2020, 1, 1), 1, true);
            var _contactStatusId = dbHelper.contactStatus.GetByName("New Contact")[0];

            #endregion

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            var _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language

            #region Authentication Provider

            var _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

            #endregion Language

            #region System User

            Guid _systemUserId = Guid.Empty;
            var systemUserExists = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_16555").Any();

            if (!systemUserExists)
                _systemUserId = dbHelper.systemUser.CreateSystemUser("Testing_CDV6_16555", "Testing", "CDV6_16555", "Testing CDV6_16555", "Passw0rd_!", "Testing_CDV6_16555@somemail.com", "Testing_CDV6_16555@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date);

            if (_systemUserId == Guid.Empty)
                _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_16555").FirstOrDefault();

            #endregion

            #region Person

            var firstName = "Testing_CDV6_16555";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");

            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion

            #region Contact

            var _contactid = dbHelper.contact.CreateContact(_careDirectorQA_TeamId, _personID, _contactTypeId, _contactReasonId, _contactPresentingPriorityId, _contactStatusId, _systemUserId, _personID, "person", firstName + " " + lastName, DateTime.Now.Date, "some value ...", 2, 2);

            #endregion

            #region Attach Document Type

            var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("All Attached Documents").Any();
            if (!attachDocumentTypeExists)
                dbHelper.attachDocumentType.CreateAttachDocumentType(_careDirectorQA_TeamId, "All Attached Documents", new DateTime(2020, 1, 1));
            var _attachDocumentTypeId = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("All Attached Documents")[0];

            #endregion

            #region Attach Document Sub Type

            var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Independent Living Grant").Any();
            if (!attachDocumentSubTypeExists)
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careDirectorQA_TeamId, "Independent Living Grant", new DateTime(2020, 1, 1), _attachDocumentTypeId);
            var _attachDocumentSubTypeId = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Independent Living Grant")[0];

            #endregion


            var _contactAttachmentFileUpload = new WebAPIHelper.WebAppAPI.Entities.CareDirector.ContactAttachmentFileUpload();
            var filestream = System.IO.File.OpenRead(TestContext.DeploymentDirectory + "\\FileToUpload.jpg");
            byte[] bytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                filestream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            string base64 = Convert.ToBase64String(bytes);

            _contactAttachmentFileUpload.filename = "FileToUpload.jpg";
            _contactAttachmentFileUpload.file = base64;

            _contactAttachmentFileUpload.record.title = "att 01";
            _contactAttachmentFileUpload.record.date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            _contactAttachmentFileUpload.record.contactid.id = _contactid;
            _contactAttachmentFileUpload.record.documenttypeid.id = _attachDocumentTypeId;
            _contactAttachmentFileUpload.record.documentsubtypeid.id = _attachDocumentSubTypeId;


            //Act
            var recordCreationResponse = webAPIHelper.chie.UploadContactAttachment(_contactAttachmentFileUpload, webAPIHelper.chie.access_token);

            //Assert
            var fields = dbHelper.contactAttachment.GetByID(recordCreationResponse.id, "title", "date", "documenttypeid", "documentsubtypeid", "fileid");
            Assert.AreEqual("att 01", fields["title"]);
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)fields["date"]).ToLocalTime());
            Assert.AreEqual(_attachDocumentTypeId, fields["documenttypeid"]);
            Assert.AreEqual(_attachDocumentSubTypeId, fields["documentsubtypeid"]);
            Assert.IsNotNull(fields["fileid"]);
            Assert.IsTrue((Guid)fields["fileid"] != Guid.Empty);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-17843

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by First Name and Last Name - Person Last Name contains a Single Quote")]
        [TestProperty("JiraIssueID", "CDV6-17985")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_SingleQuote_TestMethod01()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "Edward", "O''Brien", webAPIHelper.chie.access_token);

            //Assert
            var personId1 = new Guid("3b92beaa-0fb8-4bfa-ab27-97b92b85a539"); //332040
            var personId2 = new Guid("bc566dca-6b86-4bcb-ba35-74f1b4a0d04c"); //332041
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(2, personData.data.Count);

            int matchingPersonRecords = personData.data.Where(c => c.id.Equals(personId1)).Count();
            Assert.AreEqual(1, matchingPersonRecords);

            var personDetails = personData.data.Where(c => c.id.Equals(personId1)).First();
            Assert.AreEqual("171321", personDetails.addressline1);
            Assert.AreEqual("Gwalia, Caergwrle,'''", personDetails.addressline2);
            Assert.AreEqual("London!\"#$%&/()?*`´", personDetails.addressline3);
            Assert.AreEqual("London", personDetails.addressline4);
            Assert.AreEqual("Greater London", personDetails.addressline5);
            Assert.AreEqual("1965-01-17", personDetails.dateofbirth);
            Assert.AreEqual("Edward O'Brien", personDetails.fullname);
            Assert.AreEqual("1", personDetails.genderid.id);
            Assert.AreEqual("Male", personDetails.genderid.name);
            Assert.AreEqual("332040", personDetails.personnumber);
            Assert.AreEqual("BR1 4LT", personDetails.postcode);
            Assert.AreEqual("Oneal", personDetails.preferredname);


            matchingPersonRecords = personData.data.Where(c => c.id.Equals(personId2)).Count();
            Assert.AreEqual(1, matchingPersonRecords);
        }

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by First Name and Last Name - Person Last Name contains multiple single quote marks")]
        [TestProperty("JiraIssueID", "CDV6-17987")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_SingleQuote_TestMethod02()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "Justin", "O''Ros''s", webAPIHelper.chie.access_token);

            //Assert
            var personId1 = new Guid("f3e79e3b-26f7-4c5f-822e-52428baa345a"); //332042
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(1, personData.data.Count);

            var personDetails = personData.data.Where(c => c.id.Equals(personId1)).First();
            Assert.AreEqual("2013-11-19", personDetails.dateofbirth);
            Assert.AreEqual("Justin O'Ros's", personDetails.fullname);
            Assert.AreEqual("1", personDetails.genderid.id);
            Assert.AreEqual("Male", personDetails.genderid.name);
            Assert.AreEqual("332042", personDetails.personnumber);
            Assert.AreEqual("Ross", personDetails.preferredname);
        }

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by First Name and Last Name - Person Last Name contains multiple alpha numeric characters")]
        [TestProperty("JiraIssueID", "CDV6-17991")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_SingleQuote_TestMethod03()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "Lakisha", "O''B-i\"e?nãñçáàóòíì", webAPIHelper.chie.access_token);

            //Assert
            var personId1 = new Guid("fd29341a-3019-4820-b7bf-32eda80f420c"); //332043
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(1, personData.data.Count);

            var personDetails = personData.data.Where(c => c.id.Equals(personId1)).First();
            Assert.AreEqual("Lakisha O'B-i\"e?nãñçáàóòíì", personDetails.fullname);
            Assert.AreEqual("2", personDetails.genderid.id);
            Assert.AreEqual("Female", personDetails.genderid.name);
        }

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by First Name when person name contains 'and' e.g. Alejandra ")]
        [TestProperty("JiraIssueID", "CDV6-17992")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_SingleQuote_TestMethod04()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "Alejandra", webAPIHelper.chie.access_token);

            //Assert
            var personId1 = new Guid("b10d76c0-c516-45d1-b5ad-fb97c384466a"); //332044
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(1, personData.data.Count);

            var personDetails = personData.data.Where(c => c.id.Equals(personId1)).First();
            Assert.AreEqual("Alejandra Andre's", personDetails.fullname);
            Assert.AreEqual("2", personDetails.genderid.id);
            Assert.AreEqual("Female", personDetails.genderid.name);
        }

        [TestMethod]
        [Description("CHIE Person search - First 50 person records with CHIE search permission - Search by First Name and Last Name when person first and last names contains 'and' e.g. Alejandra Andre's")]
        [TestProperty("JiraIssueID", "CDV6-17993")]
        [TestCategory("Phoenix.APITest")]
        public void CHIEPersonSearch_SingleQuote_TestMethod05()
        {
            //Act
            var personData = webAPIHelper.chie.PersonSearch(50, "Alejandra", "Andre''s", webAPIHelper.chie.access_token);

            //Assert
            var personId1 = new Guid("b10d76c0-c516-45d1-b5ad-fb97c384466a"); //332044
            var teamid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA

            Assert.AreEqual(false, personData.hasMoreRecords);
            Assert.AreEqual(1, personData.data.Count);

            var personDetails = personData.data.Where(c => c.id.Equals(personId1)).First();
            Assert.AreEqual("Alejandra Andre's", personDetails.fullname);
            Assert.AreEqual("2", personDetails.genderid.id);
            Assert.AreEqual("Female", personDetails.genderid.name);
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-18421

        [TestMethod]
        [Description("")]
        [TestProperty("JiraIssueID", "CDV6-18743")]
        [TestCategory("Phoenix.APITest")]
        public void ExternalAPI_UpdatePerson_TestMethod01()
        {
            //Arrange 
            var _personId = new Guid("d66502a0-eb9e-ec11-a334-005056926fe4");

            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            var _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            var _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

            #endregion


            var person = new WebAPIHelper.WebAppAPI.Entities.CareDirector.Person
            {
                accommodationstatusid = new WebAPIHelper.WebAppAPI.Entities.CareDirector.Accommodationstatusid
                {
                    id = 99,
                },
                ethnicityid = new WebAPIHelper.WebAppAPI.Entities.CareDirector.Ethnicityid
                {
                    id = _ethnicityId,
                },
                AllergiesNotRecorded = null,
            };

            //Act

            try
            {
                webAPIHelper.chie.UpdatePersonRecord(_personId, person, webAPIHelper.chie.access_token);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsTrue(ex.Message.Contains("Please provide a valid picklist value for accommodationstatusid"));
            }



        }

        [TestMethod]
        [Description("")]
        [TestProperty("JiraIssueID", "CDV6-18744")]
        [TestCategory("Phoenix.APITest")]
        public void ExternalAPI_UpdatePerson_TestMethod02()
        {
            //Arrange 
            var _personId = new Guid("d66502a0-eb9e-ec11-a334-005056926fe4");

            var person = new WebAPIHelper.WebAppAPI.Entities.CareDirector.Person
            {
                accommodationstatusid = new WebAPIHelper.WebAppAPI.Entities.CareDirector.Accommodationstatusid
                {
                    id = 1,
                },
                ethnicityid = new WebAPIHelper.WebAppAPI.Entities.CareDirector.Ethnicityid
                {
                    id = Guid.NewGuid(),
                },
                AllergiesNotRecorded = null,
            };

            //Act

            try
            {
                webAPIHelper.chie.UpdatePersonRecord(_personId, person, webAPIHelper.chie.access_token);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsTrue(ex.Message.Contains("Please provide a valid reference value for ethnicityid"));
            }



        }

        [TestMethod]
        [Description("")]
        [TestProperty("JiraIssueID", "CDV6-18745")]
        [TestCategory("Phoenix.APITest")]
        public void ExternalAPI_UpdatePerson_TestMethod03()
        {
            //Arrange 

            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            var _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            var _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region Ethnicity

            var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any();
            if (!ethnicitiesExist)
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

            #endregion


            var person = new WebAPIHelper.WebAppAPI.Entities.CareDirector.Person
            {
                accommodationstatusid = new WebAPIHelper.WebAppAPI.Entities.CareDirector.Accommodationstatusid
                {
                    id = 1,
                },
                ethnicityid = new WebAPIHelper.WebAppAPI.Entities.CareDirector.Ethnicityid
                {
                    id = _ethnicityId,
                },
                AllergiesNotRecorded = false,
            };

            //Act

            try
            {
                webAPIHelper.chie.UpdatePersonRecord(new Guid("d66502a0-eb9e-ec11-a334-005056926fe4"), person, webAPIHelper.chie.access_token);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsTrue(ex.Message.Contains("allergiesnotrecorded is not valid for update operation."));
            }



        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-20809

        [TestMethod]
        [Description("Validate that it is possible to create a contact record using the api and set the Responsible Team id (ownerid) ")]
        [TestProperty("JiraIssueID", "")]
        [TestCategory("Phoenix.APITest")]
        public void Contact_API_TestMethod01()
        {
            //Arrange 

            #region Business Unit

            if (!dbHelper.businessUnit.GetByName("CareDirector QA").Any())
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            var _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            if (!dbHelper.team.GetTeamIdByName("Team CDV6_20809").Any())
                dbHelper.team.CreateTeam("Team CDV6_20809", null, _careDirectorQA_BusinessUnitId, "907678", "Team_CDV6_20809@careworkstempmail.com", "Team CDV6_20809", "020 123456");
            var _teamId = dbHelper.team.GetTeamIdByName("Team CDV6_20809")[0];

            #endregion

            #region Ethnicity

            if (!dbHelper.ethnicity.GetEthnicityIdByName("SmokeTest_Ethnicity").Any())
                dbHelper.ethnicity.CreateEthnicity(_teamId, "SmokeTest_Ethnicity", new DateTime(2020, 1, 1));
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("SmokeTest_Ethnicity")[0];

            #endregion

            #region ContactType

            if (!dbHelper.contactType.GetByName("Adult Safeguarding").Any())
                dbHelper.contactType.CreateContactType(_teamId, "Adult Safeguarding", DateTime.Now.Date.AddYears(-1), true);
            var _contactTypeId = dbHelper.contactType.GetByName("Adult Safeguarding")[0];

            #endregion

            #region Contact Reason

            if (!dbHelper.contactReason.GetByName("Adoption Enquiry").Any())
                dbHelper.contactReason.CreateContactReason(_teamId, "Adoption Enquiry", new DateTime(2020, 1, 1), 110000000, false);
            var _contactReasonId = dbHelper.contactReason.GetByName("Adoption Enquiry")[0];

            #endregion

            #region ContactPresentingPriority

            if (!dbHelper.contactPresentingPriority.GetByName("Routine").Any())
                dbHelper.contactPresentingPriority.CreateContactPresentingPriority(_teamId, "Routine", "2", "2", new DateTime(2020, 1, 1));
            var _contactPresentingPriorityId = dbHelper.contactPresentingPriority.GetByName("Routine")[0];

            #endregion

            #region ContactStatus

            if (!dbHelper.contactStatus.GetByName("New Contact").Any())
                dbHelper.contactStatus.CreateContactStatus(_teamId, "New Contact", "2", new DateTime(2020, 1, 1), 1, true);
            var _contactStatusId = dbHelper.contactStatus.GetByName("New Contact")[0];

            #endregion

            #region Person

            var firstName = "Testing_CDV6_20809";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullname = firstName + " " + lastName;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);

            #endregion

            var currentDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:00Z");

            var _contact = new ContactBO
            {
                regardingid = new Regardingid { id = _personID, name = fullname, type = "person" },

                consentdatetime = currentDate,
                contactaccepteddatetime = currentDate,
                contactassigneddatetime = currentDate,
                contactcloseddatetime = currentDate,
                contactreceiveddatetime = currentDate,
                contactrejecteddatetime = currentDate,

                ownerid = new Ownerid { id = _teamId },
                contacttypeid = new Contacttypeid { id = _contactTypeId },
                persongroupawareofcontactid = new Persongroupawareofcontactid { id = 2 },
                carerawareofcontactid = new Carerawareofcontactid { id = 2 },
                nextofkinawareofcontactid = new Nextofkinawareofcontactid { id = 2 },
                contactreasonid = new Contactreasonid { id = _contactReasonId },
                contactpresentingpriorityid = new Contactpresentingpriorityid { id = _contactPresentingPriorityId },
                presentingneed = "Testing CDV6-20809",
                contactstatusid = new Contactstatusid { id = _contactStatusId },
                lastname = DateTime.Now.ToString("yyyyMMddHHmmss"),
                isdatacollectionandusageconsent = "true"
            };



            //Act
            var recordCreationResponse = webAPIHelper.chie.CreateContact(_contact, webAPIHelper.chie.access_token);

            //Assert
            Assert.IsTrue(recordCreationResponse.id != Guid.Empty);
            var responsibleTeamId = dbHelper.contact.GetByID(recordCreationResponse.id, "ownerid")["ownerid"].ToString();
            Assert.AreEqual(_teamId.ToString(), responsibleTeamId);
        }

        #endregion

        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}
