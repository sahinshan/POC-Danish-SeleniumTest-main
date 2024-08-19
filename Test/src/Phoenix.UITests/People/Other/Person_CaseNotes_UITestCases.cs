using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_CaseNotes_UITestCases : FunctionalTest
    {
        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private string _adminUserFullname;
        private Guid _systemUserId;
        private string _systemUsername;
        private string _systemUserFullName;
        private Guid _jobRoleTypeId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private string _activityReasonTypeName;
        private Guid _activityReasonTypeID;
        private Guid _activityPriorityID;
        private string _activityPriorityName;
        private Guid _activityCategoryID;
        private string _activityCategoryName;
        private Guid _activitySubCategoryID;
        private string _activitySubCategoryName;
        private Guid _activityOutcomeTypeID;
        private string _activityOutcomeTypeName;
        private Guid _significantEventCategoryId_1;
        private string _significantEventCategoryName_1;
        private Guid _significantEventSubCategoryId_1_1;
        private string _significantEventSubCategoryName_1_1;
        private Guid _significantEventSubCategoryId_1_2;
        private string _significantEventSubCategoryName_1_2;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Environment 

                _environmentName = ConfigurationManager.AppSettings["EnvironmentName"];

                #endregion

                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);
                _adminUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Default System User

                _systemUsername = "PersonCaseNoteUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "Person Case Note", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                _systemUserFullName = "Person Case Note User1";
                #endregion

                #region Job Role Type

                _jobRoleTypeId = commonMethodsDB.CreateJobRoleType(_teamId, "Default Consultant", new DateTime(2020, 1, 1), 1);

                #endregion

                #region Update System User Job Role Type

                dbHelper.systemUser.UpdateJobRoleType(_systemUserId, _jobRoleTypeId);

                #endregion

                #region Person

                var personFirstName = "Person_Case_Note";
                var personLastName = "LN_" + _currentDateString;
                _personFullName = personFirstName + " " + personLastName;
                _personID = commonMethodsDB.CreatePersonRecord(personFirstName, personLastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
                _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

                #endregion

                #region Activity Reason

                _activityReasonTypeName = "Assessment";
                _activityReasonTypeID = commonMethodsDB.CreateActivityReason(_activityReasonTypeName, new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Activity Priority

                _activityPriorityName = "Normal";
                _activityPriorityID = commonMethodsDB.CreateActivityPriority(_activityPriorityName, new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Activity Categories

                _activityCategoryName = "Advice";
                _activityCategoryID = commonMethodsDB.CreateActivityCategory(_activityCategoryName, new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Activity Sub Categories

                _activitySubCategoryName = "Home Support";
                _activitySubCategoryID = commonMethodsDB.CreateActivitySubCategory(_activitySubCategoryName, new DateTime(2020, 1, 1), _activityCategoryID, _teamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeTypeName = "More information needed";
                _activityOutcomeTypeID = commonMethodsDB.CreateActivityOutcome(_activityOutcomeTypeName, new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Significant Event Category

                _significantEventCategoryName_1 = "Category 1";
                _significantEventCategoryId_1 = commonMethodsDB.CreateSignificantEventCategory(_significantEventCategoryName_1, DateTime.Now.Date, _teamId, null, null, null);

                #endregion

                #region Significant Event Sub Category

                _significantEventSubCategoryName_1_1 = "Sub Cat 1_1";
                _significantEventSubCategoryId_1_1 = commonMethodsDB.CreateSignificantEventSubCategory(_teamId, _significantEventSubCategoryName_1_1, _significantEventCategoryId_1, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, null);

                _significantEventSubCategoryName_1_2 = "Sub Cat 1_2";
                _significantEventSubCategoryId_1_2 = commonMethodsDB.CreateSignificantEventSubCategory(_teamId, _significantEventSubCategoryName_1_2, _significantEventCategoryId_1, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, null);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region Spell Check https://advancedcsg.atlassian.net/browse/CDV6-5317


        public string NotesData1 { get { return "<div> <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:26pt\"><span style=\"font-family:Cambria,serif\"><span style=\"color:#17365d\">Testing title style</span></span></span></p> </div>  <h2 style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:13pt\"><span style=\"font-family:Cambria,serif\"><span style=\"color:#4f81bd\">Testing headings</span></span></span></h2>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Times New Roman&quot;,serif\">Testing New Roman Times Font Size 11</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Arial Font Size 11</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-size:20.0pt\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Arial Font Size 20</span></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">Testing Bold</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><em>Testing Italic</em></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><u>Testing Underline</u></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"background-color:lime\"><span style=\"color:black\">Testing highlighting</span></span></span></span></p>  <ul> 	<li><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">Testing bullet point</span></span></li> </ul>  <ol> 	<li><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">Testing bullet numbers</span></span></li> </ol>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:red\">Testing red font</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><span style=\"color:fuchsia\">Testing pink font from custom colours</span></span></span></span></p>  <table cellspacing=\"0\" class=\"Table\" style=\"border-collapse:collapse; border:undefined\"> 	<tbody> 		<tr> 			<td style=\"vertical-align:top; width:115.5pt\"> 			<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Table</span></span></span></p> 			</td> 			<td style=\"vertical-align:top; width:115.5pt\"> 			<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">one</span></span></span></p> 			</td> 			<td style=\"vertical-align:top; width:115.55pt\"> 			<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">two</span></span></span></p> 			</td> 			<td style=\"vertical-align:top; width:115.55pt\"> 			<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Three</span></span></span></p> 			</td> 		</tr> 		<tr> 			<td style=\"vertical-align:top; width:115.5pt\"> 			<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing </span></span></span></p> 			</td> 			<td style=\"vertical-align:top; width:115.5pt\"> 			<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">1</span></span></span></p> 			</td> 			<td style=\"vertical-align:top; width:115.55pt\"> 			<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">2</span></span></span></p> 			</td> 			<td style=\"vertical-align:top; width:115.55pt\"> 			<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">3</span></span></span></p> 			</td> 		</tr> 		<tr> 			<td style=\"vertical-align:top; width:115.5pt\"> 			<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Again </span></span></span></p> 			</td> 			<td style=\"vertical-align:top; width:115.5pt\"> 			<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">3</span></span></span></p> 			</td> 			<td style=\"vertical-align:top; width:115.55pt\"> 			<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">2</span></span></span></p> 			</td> 			<td style=\"vertical-align:top; width:115.55pt\"> 			<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">1</span></span></span></p> 			</td> 		</tr> 	</tbody> </table>  <p style=\"margin-left:0cm; margin-right:0cm\">&nbsp;</p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Shapes</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing spel cheker</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Tsting spll Checer agen</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing right alignment<s> </s></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm; text-align:center\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Centre alignment</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><sub><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Subscript</span></sub></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><sup><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Super Script</span></sup></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><s><span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Strikethrough</span></s></span></span></p>"; } }

        public string NotesData2 { get { return "<p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Thanks for that testing Matt. Unless I&rsquo;m misunderstanding this means we do still have an issue that would prevent us from taking 6.1.6.</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">&nbsp;</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Mark &ndash; out of interest can you copy and paste this email chain into a case note and then send us the screen shot.</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">&nbsp;</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Cheers</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Cath</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">&nbsp;</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">I&rsquo;ve deliberately included Maxine&rsquo;s signature here as I know this was a problem when we last tested;</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">&nbsp;</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><strong><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><span style=\"color:black\">Maxine Brook</span></span></strong></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:black\">Configuration Analyst </span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><strong><span style=\"color:black\">EPR Project </span></strong></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:black\">Leeds and York Partnership NHS Foundation Trust</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:black\">Health Informatics Service</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:black\">North Wing, St Mary&rsquo;s House, Leeds LS7 3LA</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:black\">Mob:</span><strong><span style=\"color:#0070c0\"> 07989414124</span></strong></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:black\">email:</span><span style=\"color:#1f497d\"> <a href=\"mailto:Maxine.Brook@nhs.net\" style=\"color:blue; text-decoration:underline\">Maxine.Brook@nhs.net</a></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">&nbsp;</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">&nbsp;</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><strong><span style=\"font-size:10.0pt\"><span style=\"font-family:&quot;Tahoma&quot;,sans-serif\">From:</span></span></strong><span style=\"font-size:10.0pt\"><span style=\"font-family:&quot;Tahoma&quot;,sans-serif\"> Matt Butler [<a href=\"mailto:mbutler@careworks.co.uk\" style=\"color:blue; text-decoration:underline\">mailto:mbutler@careworks.co.uk</a>]<br /> <strong>Sent:</strong> 21 October 2020 14:11<br /> <strong>To:</strong> <a href=\"mailto:mark.thompson@oneadvanced.com\" style=\"color:blue; text-decoration:underline\">mark.thompson@oneadvanced.com</a>; BORLAND, Iain (LEEDS AND YORK PARTNERSHIP NHS FOUNDATION TRUST)<br /> <strong>Cc:</strong> SWEENEY, Cath (LEEDS AND YORK PARTNERSHIP NHS FOUNDATION TRUST)<br /> <strong>Subject:</strong> RE: V6.1.6 Spell Check fix.</span></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">Hi Mark and Iain,</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">So a little bit of further testing in our QA&nbsp; using the original Word document Laura sent to us. I have found we still have an issue but it is rather a strange one when you go through all the steps that I have. I&rsquo;ll explain all the steps and then what you see in the attached will make more sense:</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <ol>  <li><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">I took the word document Laura sent to me which has a range of formats within the document;</span></span></li>  <li><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">I created a new case note for a patient in CareDirector QA;</span></span></li>  <li><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">I selected all the text in the word document (Ctrl c) and pasted these into the case note (Ctrl v). At this point if I do not hit the spell check button the pasted text is the same as the word document;</span></span></li>  <li><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">I hit the spell check button and you see additional text appear in the case for the Testing Arial Font Size 11 entry, which now shows Arial&rdquo;,sans-serif&rdquo;&gt;Testing Arial Font Size 11. Note the addition of <span style=\"color:#ed7d31\">Arial&rdquo;,sans-serif&rdquo;&gt; </span><span style=\"color:black\">(please see 6.1.6 test.jpg);</span></span></span></li>  <li><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:black\">I can save the case note and all text remains in the case note (on v6.1.3 it disappears);</span></span></span></li>  <li><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:black\">So here&rsquo;s the interesting thing&hellip;. If I delete the 2 entries for the Testing of Arial Fonts in the word document, type these same entries into the same word document at the end, copy and paste the word document text into the case note and hit spell check, I do not get the rogue entry before the Testing Arial Font Size 11 (see 6.1.6 test2.jpg).</span></span></span></li>  <li><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:black\">One final test I did was to create a new word document for myself and added 2 entries into it with and Arial Font Size 11 and Arial Font Size 20, copied these into a case note and it worked perfectly with no strange sans-serif text being added.</span></span></span></li> </ol>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">I can also confirm the issue we were also seeing when I copied details from an email and pasted those into a case, where it added extra formatting text similar to the above example no longer behaves that way and the copied text maintains the look and feel of the email. (specifically people&rsquo;s links to email addresses).</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">Hope this helps,</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">Thanks</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">Matt</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-size:14.0pt\"><span style=\"color:#7b7b7b\">Matt Butler</span></span><br /> <span style=\"font-size:10.0pt\">Project Manager<br /> <span style=\"color:#76923c\">Email</span>: <a href=\"mailto:mbutler@careworks.ie\" style=\"color:blue; text-decoration:underline\">mbutler@careworks.ie</a> or <a href=\"mailto:mbutler@careworks.co.uk\" style=\"color:blue; text-decoration:underline\">mbutler@careworks.co.uk</a></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-size:10.0pt\"><span style=\"font-family:&quot;Calibri\\,Times New Roman&quot;\"><span style=\"color:#7b7b7b\">Office UK</span></span></span><span style=\"font-size:10.0pt\"><span style=\"font-family:&quot;Calibri\\,Times New Roman&quot;\">: +44 (0) 2890 327 329 </span></span><br /> <span style=\"font-size:10.0pt\"><span style=\"font-family:&quot;Calibri\\,Times New Roman&quot;\"><span style=\"color:#7b7b7b\">Mobile: </span></span></span><span style=\"font-size:10.0pt\"><span style=\"font-family:&quot;Calibri\\,Times New Roman&quot;\">+44 (0) 7917 668734</span></span><br /> <span style=\"font-size:10.0pt\"><span style=\"font-family:&quot;Calibri\\,Times New Roman&quot;\"><span style=\"color:#7b7b7b\">Visit our website</span></span></span><span style=\"font-size:10.0pt\"><span style=\"font-family:&quot;Calibri\\,Times New Roman&quot;\">: </span></span><a href=\"http://www.careworks.co.uk/\" style=\"color:blue; text-decoration:underline\"><span style=\"font-size:10.0pt\"><span style=\"font-family:&quot;Calibri\\,Times New Roman&quot;\">www.careworks.co.uk</span></span></a> </span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-size:10.0pt\"><span style=\"color:black\">&nbsp;</span></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><strong><em><span style=\"font-size:10.5pt\">Solutions that enable Health and Social Care organisations to make a real difference to people&rsquo;s live</span></em></strong><strong><span style=\"font-size:10.5pt\">s</span></strong><br /> <img src=\"data:image/png;base64,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\" style=\"height:103px; width:314px\" /></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-size:9.0pt\"><span style=\"color:#a6a6a6\">This email is confidential and is meant for the intended addressee. If you are not the intended recipient, any use, printing, copying or disclosure is strictly prohibited: please notify the sender and delete the email. The views expressed are those of the sender and do not necessarily represent those of CareWorks. Responsibility for virus checking rests with the recipient. Please consider the environment before printing this email.</span></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><strong>From:</strong> Mark Thompson &lt;<a href=\"mailto:Mark.Thompson@oneadvanced.com\" style=\"color:blue; text-decoration:underline\">Mark.Thompson@oneadvanced.com</a>&gt;<br /> <strong>Sent:</strong> 21 October 2020 13:00<br /> <strong>To:</strong> BORLAND, Iain (LEEDS AND YORK PARTNERSHIP NHS FOUNDATION TRUST) &lt;<a href=\"mailto:iain.borland@nhs.net\" style=\"color:blue; text-decoration:underline\">iain.borland@nhs.net</a>&gt;; Matt Butler &lt;<a href=\"mailto:mbutler@careworks.co.uk\" style=\"color:blue; text-decoration:underline\">mbutler@careworks.co.uk</a>&gt;<br /> <strong>Cc:</strong> SWEENEY, Cath (LEEDS AND YORK PARTNERSHIP NHS FOUNDATION TRUST) &lt;<a href=\"mailto:cath.sweeney@nhs.net\" style=\"color:blue; text-decoration:underline\">cath.sweeney@nhs.net</a>&gt;<br /> <strong>Subject:</strong> RE: V6.1.6 Spell Check fix.</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Hi Iain, I was offering to check for you if you have any known examples that cause the issue. But is it the case that the only time you have seen the issue is when Matt was doing the demo of 6.1.5?</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">&nbsp;</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">&nbsp;</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-size:10.5pt\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><span style=\"color:black\">&nbsp;Thanks, mark</span></span></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-size:12.0pt\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><span style=\"color:black\">&nbsp;</span></span></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><a href=\"http://www.oneadvanced.com/\" style=\"color:blue; text-decoration:underline\"><span style=\"font-size:13.5pt\"><span style=\"font-family:&quot;Times&quot;,serif\"><img alt=\"cid:image012.png@01D17AF7.D972DF70\" src=\"data:image/png;base64,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\" style=\"height:108px; width:153px\" /></span></span></a></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><strong><span style=\"font-size:10.5pt\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><span style=\"color:#454545\">Mark Thompson</span></span></span></strong><br /> <span style=\"font-size:10.5pt\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><span style=\"color:#454545\">Principal Product Owner &gt; Health and Care &gt; Advanced</span></span></span><br /> <strong><span style=\"font-size:10.5pt\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><span style=\"color:#dc4405\">________________________</span></span></span></strong><br /> <br /> <span style=\"font-size:10.5pt\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><span style=\"color:#454545\"><strong>Advanced</strong><br /> Ditton Park, Riding Court Road, Datchet, Berkshire, SL3 9LL<br /> t: 0330 343 8000</span></span></span><br /> <br /> <span style=\"font-size:12.0pt\"><span style=\"font-family:&quot;Times New Roman&quot;,serif\"><span style=\"color:#1f497d\"><a href=\"https://www.oneadvanced.com/\" style=\"color:blue; text-decoration:underline\"><span style=\"font-size:10.5pt\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><span style=\"color:#dc4405\">www.oneadvanced.com</span></span></span></a></span></span></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-size:12.0pt\"><span style=\"font-family:&quot;Times New Roman&quot;,serif\"><span style=\"color:#1f497d\"><a href=\"http://www.linkedin.com/company/2426258\" style=\"color:blue; text-decoration:underline\"><span style=\"font-size:13.5pt\"><span style=\"font-family:&quot;Times&quot;,serif\"><img alt=\"cid:image018.png@01D17AF7.D972DF70\" src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABUAAAAWCAYAAAAvg9c4AAABGWlDQ1BJQ0MgUHJvZmlsZQAAKJFjYGBSSCwoyGESYGDIzSspCnJ3UoiIjFJgf8DAzMDFIMDAxsCRmFxc4BgQ4MMABDAaFXy7xsAIoi/rgszClMcLuFJSi5OB9B8gzk4uKCphYGDMALKVy0sKQOweIFskKRvMXgBiFwEdCGRvAbHTIewTYDUQ9h2wmpAgZyD7A5DNlwRmM4Hs4kuHsAVAbKi9ICDomJKflKoA8r2GoaWlhSaJfiAISlIrSkC0c35BZVFmekaJgiMwpFIVPPOS9XQUjAwMzRgYQOEOUf05EByejGJnEGIIgBCbI8HA4L+UgYHlD0LMpJeBYYEOAwP/VISYmiEDg4A+A8O+OcmlRWVQYxiZjBkYCPEBtH9KIvQ2P88AAARsSURBVDgRfVRLiFxFFD31ef3edM9MJ1kMiRjFEN0piIyIGVyYOG5EmE1EdCuIEQkRxY0grvyLuFLjwo2IiSsJRhEVt5KNbuIkE4wmmWBPZz7p33v1uZ4qdGkXXTT1Xr17zz3n3KtEGoEU8ArwP3yNxk5glIL2DYKdh5Ia01bUASKC+UNPAMojRgsVgsjm6Xcx+PwtuPEGdIwYWwPLRCo0/DfTYiJAQZkI3e5iz9EXsevJl6A2z34sG+8cQyBSKy1omSAUBUztmdkiaJkaVEcDpSMkIYZF9/jbMM/N/f2a7f0FSEBhDCZiocOEGTRaUSOqSCzyv1vbiBqedw2E2232oNYOF+K1g9Es+Y5FLDz2NMaDITZPv4/x9nVUDDdtiSK+0EEwDpHlVqHmmYhmJrMYFwPsO/ERqjvvwUx0UFsjbH/1Ojwpmb48KRpCg2IbBa9nWWRUGJVDVFKScCqeqNcFws4OmoTex+kxRdMlFiZ6zDSORJDj1WWIdfPMMEK1/wC6DxxFM+yh/92nMIE8kdNpi5QyOfln6U1FGj2BKOGhcGg3CqGzgBu/fU/CI+YW9mO4fo10LDLmgM4oMepfg0xGmN17N+qCdtvpw61fZmJP9SNatebdAKw+YmT1sJa1Rzsy+OlLkRBF+Nv45FW59HApox9PpSOfi1z94Bnxkz5zevHpgTjpf/GmXDwyJ78fsXJhuZW3LoiUWsG5Bppl5EVrxjBio9Vw9GlmgCbY9/yHMK099LQih+mupeFPoHPvEpkscmel7tJJXc8Lil2UaPaaNPB+tDMMbFCxtEazVZlovHYBvWfvx5VjDyJc/yNXGtggZvEhJqGfeSdt6swWYzA6nogCuQMFAgoq7xl8TEOzv8iZw82TL6N/+RfI6q/YOfsZajPgG6q+cBAqRWOstG3DgxJa3DGjKvNQ4E3yMUHHp3AtJkvtCgwHPZSh4tAI9O8IZZxFJD1CAGkG/LdsNDWhpy417PuUzeZ3DQPX5FQMW5YrASmMJVXkjH4uQzejT6ZPKHM7qyLf1ZWv8iCBbSGh5migpRiAFjKuRVQVqeGPnAlnQq3IvzLkecgAFIdfmNQgyVb/Tggd2Fojgi+IIAmq6QZFxC2enVX8rMkiidGZnlKN0bCqrAG5D8yY3lnyY2LDb8eM4cao9Dwc+71SvJW4Nh4Dls5RA5fq5sp/DMb0OXgk8uQ1S41rxQoMSzez2T3q4souUTtD9rugPHAfBShoCU6cm9twV8+jfXAJseDQ8PRs708UmzfgGMDuvR1u925UFFQTVHP+HBxRFvO3QV154ylpvj1FfWLuYTKeiTeOk8p0CXyczZ44SOhSo1hSRKeTweTrkqULGgquqUvn0Aqr2tqS9VeWUV86h8bRQAW5JBXJpw2tk4av13QEfZw9mOjhs0jB8rRPduOAV2WF9q134Zb3vqE92cMYbqM+cxIbP5+hyqNEIYWjRBRKYcLMtFtsI4rLKtdUumU5Ksmn5cjzRcTM0gq6j78AW7XxD7jvYc0HMlAVAAAAAElFTkSuQmCC\" style=\"height:22px; width:21px\" /></span></span>&nbsp;</a></span></span></span><a href=\"https://twitter.com/advanced\" style=\"color:blue; text-decoration:underline\"><span style=\"font-size:13.5pt\"><span style=\"font-family:&quot;Times&quot;,serif\"><img alt=\"cid:image019.png@01D17AF7.D972DF70\" src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABUAAAAWCAYAAAAvg9c4AAABGWlDQ1BJQ0MgUHJvZmlsZQAAKJFjYGBSSCwoyGESYGDIzSspCnJ3UoiIjFJgf8DAzMDFIMDAxiCcmFxc4BgQ4MMABDAaFXy7xsAIoi/rgszClMcLuFJSi5OB9B8gzk4uKCphYGDMALKVy0sKQOweIFskKRvMXgBiFwEdCGRvAbHTIewTYDUQ9h2wmpAgZyD7A5DNlwRmM4Hs4kuHsAVAbKi9ICDomJKflKoA8r2GoaWlhSaJfiAISlIrSkC0c35BZVFmekaJgiMwpFIVPPOS9XQUjAwMzRgYQOEOUf05EByejGJnEGIIgBCbI8HA4L+UgYHlD0LMpJeBYYEOAwP/VISYmiEDg4A+A8O+OcmlRWVQYxiZjBkYCPEByl5KLcNKz4kAAATsSURBVDgRLZVLiFxFGIVPVd1XT09PxsnLTBCMELJRTFzoIooSzIBZiEYNxI0vXCX4goAQFFeSRQKKIgEx4EYMuFF8IXFhwGyNWYQoMRKQ4CMxPZPu6e57b1X5VcfbND3TXff85z/n/P81MTQxhkzBtRr98KWCaSWXKW9atc7IhCgXrYJ1yr00clFFCPIZ34VWWXSKtlF3517JSEGZstpkqj87qn9Ovi0tj2RNobFZUcWJic0BAEi1lBlljZGv0k2NbCoEgEytOnOaces09/RrWth/SObq1yfi4OgBtXYkE3MpehVZrmU/UWU68poo5+bENPhGoKlsLedFR1LtM855eUN1CM2/fEzu4NyVt9rlP/kSQNjFPFNbT1RmPdkwBLALc/qmWOWsGuQQnyZ4pAoq0j0hyeT5OqrpX5H5/eEsBnSps1oZVa3vytBSi4A+Btm2VZVbMCMi5ABB1gWZ9iYzYybyPih3XQ3VqkOHGZIrUtzQauVbDYs+/1jldVKsVID5sDWqIlraVsCrHnuVeaXVMFF0GGgLigJIN3moUAgD6gy3awR3Xp0NW+UAbWHlTElBQ1u1PA7P73lRix9f0G3f9nXLwXc1u+1eLex5Di/QGttM0Wpc0q2ZIHRsAchQx2nDkVPqLb0gR5wmtBZbp26TK9/1uHqvHlG+4Q5VRVedR/dr8/tnVK3fJjO3ju5a+bYkGV62zhEa0Bp2U9XwZONLxzX/yjuqNm2hWFBtrXo7dlOyIrt4xstN44T7f19S6F9TgwxZMsxEmYtLlvA7uZArlhMt7H1Ts8++Tv6Tr9Lg7HeKF35S98F9spsWJY9+dJGA7fKKLj25FqBA8DrqMAzpykIoNNNONOg0tGpV7Ljvf8CgQMC723dJ25dSd4kicWoolqOjucnaGDXJUrodO4aEy5ZEZaUoVTQdWnJqLl7gEIQoYAwAjDAdTYMeiZoAJEHTsN84e5q/mToiGVIykvO8s+S8w5CWTJZtpWsfHtLGtlHx2POKZgEQAJgew7wn1UkX8w44HfpTJygxpmiplkoVrNOVhThUiaYkTE0xZvyiRsVIRXeGzNExDAwZNZhVk9u0C+QK3Tj7vfo/fqUGUhnmViyhFhmnoBlVIgLXgFWwzSelbnzyAW10MOcpFbcuEmYEYYwLfk+YdmWgfz96A+aNyhrj2CsNMqYBSpf1BDtYWpsy6SE2Og6vqzn/M4ZMpqYkQMhwkcHRsi4eXlLgd8srLRjL3OMbIzyevqkB7XQL9habblfvgSe05pFnpMWtaJcsZ5DJsK5c1vXTJzX49D351b8AIZM2U8Oy6YSS4mnLVSBB6vJDiqNyjsWxOq3oXK4Rh7tb7lReWPnGT9dc/OU8WrIycDrp38G4kNr3la7iQRe5cug2ZpW9u3a94vJ1jhGJvBDGawaX/W/ncJR2+K7AyLEZTXOaoZ1LzsCwZbQjLc+GeUxdZazZdL3Nsr27d6tkIgiURrQZikarKZ8skCbvIDohR4YidpW3s9OWvWd12xKgBMoeZt0Nc5ZOWkz37ESG/jD+cfh++V/PTbd4EclsDlecjnEMI4adkiYWMMUNpiZlOvKoSc+sAAE5nhCcW7N4lxaPfSMzZkWXg4H6XxzX4MzngiBpgAGpdGzbNKoZ6a9tyiAPOg+znNkj7GkJ5Xzy8FS2ex9r8AD4Xf0HAwdjh4ugOHkAAAAASUVORK5CYII=\" style=\"height:22px; width:21px\" /></span></span></a></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><strong><span style=\"font-size:10.5pt\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><span style=\"color:#dc4405\">&gt;</span></span></span></strong><strong><span style=\"font-size:10.5pt\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><span style=\"color:#454545\">&nbsp;Proud to be a Patron of The Prince&rsquo;s Trust</span></span></span></strong></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">&nbsp;</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">&nbsp;</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">&nbsp;</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><strong>From:</strong> BORLAND, Iain (LEEDS AND YORK PARTNERSHIP NHS FOUNDATION TRUST) &lt;<a href=\"mailto:iain.borland@nhs.net\" style=\"color:blue; text-decoration:underline\">iain.borland@nhs.net</a>&gt;<br /> <strong>Sent:</strong> 21 October 2020 12:35<br /> <strong>To:</strong> Mark Thompson &lt;<a href=\"mailto:Mark.Thompson@oneadvanced.com\" style=\"color:blue; text-decoration:underline\">Mark.Thompson@oneadvanced.com</a>&gt;; Matt Butler &lt;<a href=\"mailto:mbutler@careworks.co.uk\" style=\"color:blue; text-decoration:underline\">mbutler@careworks.co.uk</a>&gt;<br /> <strong>Cc:</strong> SWEENEY, Cath (LEEDS AND YORK PARTNERSHIP NHS FOUNDATION TRUST) &lt;<a href=\"mailto:cath.sweeney@nhs.net\" style=\"color:blue; text-decoration:underline\">cath.sweeney@nhs.net</a>&gt;<br /> <strong>Subject:</strong> RE: V6.1.6 Spell Check fix.</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"background-color:#ffeb9c\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-size:10.0pt\"><span style=\"color:#9c6500\">CAUTION:</span></span><span style=\"font-size:10.0pt\"><span style=\"color:black\"> This email originated from outside of the organisation. Do not click links or open attachments unless you recognise the sender and know the content is safe.</span></span></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-size:12.0pt\"><span style=\"font-family:&quot;Times New Roman&quot;,serif\">&nbsp;</span></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Thanks for getting back so quickly Mark,</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">That sounds hopeful, I would like to be able to check some more notes to be sure, but not sure how we do that before we get the release?</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Kind regards</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"font-family:&quot;Lucida Handwriting&quot;\"><span style=\"color:#1f497d\">Iain</span></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><a href=\"http://www.google.co.uk/url?url=http://hellomynameis.org.uk/&amp;rct=j&amp;frm=1&amp;q=&amp;esrc=s&amp;sa=U&amp;ei=gjWhU6aAD9TY7AbwxIDwDA&amp;ved=0CBYQ9QEwAA&amp;usg=AFQjCNH51Uy3T8b6K-oZ0ZVgLFltAwrORA\" style=\"color:blue; text-decoration:underline\"><span style=\"font-size:10.0pt\"><span style=\"font-family:&quot;Arial&quot;,sans-serif\"><span style=\"color:#1a0dab\"><img alt=\"https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcSGBZKGcl6nxKWmBe8lxd6ivILxbXLOgWWI_5A7S264QZg3vJCqdZw56E7_\" src=\"https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcSGBZKGcl6nxKWmBe8lxd6ivILxbXLOgWWI_5A7S264QZg3vJCqdZw56E7_\" style=\"height:43px; width:143px\" /></span></span></span></a></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Iain Borland</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">EPR Programme Manager</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Leeds and York Partnership NHS Foundation Trust</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">North Wing, St Mary&rsquo;s House</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">St Mary&rsquo;s Rd</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Leeds</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">LS7 3JX</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Tel: 07903 704197</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><strong><span style=\"font-size:10.0pt\"><span style=\"font-family:&quot;Tahoma&quot;,sans-serif\">From:</span></span></strong><span style=\"font-size:10.0pt\"><span style=\"font-family:&quot;Tahoma&quot;,sans-serif\"> Mark Thompson [<a href=\"mailto:Mark.Thompson@oneadvanced.com\" style=\"color:blue; text-decoration:underline\">mailto:Mark.Thompson@oneadvanced.com</a>]<br /> <strong>Sent:</strong> 21 October 2020 12:21<br /> <strong>To:</strong> BORLAND, Iain (LEEDS AND YORK PARTNERSHIP NHS FOUNDATION TRUST); Matt Butler<br /> <strong>Cc:</strong> SWEENEY, Cath (LEEDS AND YORK PARTNERSHIP NHS FOUNDATION TRUST)<br /> <strong>Subject:</strong> RE: V6.1.6 Spell Check fix.</span></span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Hi Iain, that all makes sense thanks.</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">As I understand Matt did this in version 6.1.6 with the same email and didn&rsquo;t get an error. &nbsp;So i haven&rsquo;t taken any further action on this.</span></span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\">&nbsp;</span></span></p>  <p style=\"margin-left:0cm; margin-right:0cm\"><span style=\"font-size:11pt\"><span style=\"font-family:Calibri,sans-serif\"><span style=\"color:#1f497d\">Matt &ndash; please confirm above? Also Matt if you would like the issue logged for version 6.1.5 please raise a call with the exact required replication"; } }


        #region View Record

        [TestProperty("JiraIssueID", "CDV6-24847")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-5317 - " +
            "User has access to a case note record with the problematic text in the Notes field" +
            "Login in the web app - navigate to the people page - open a person record - navigate to the person case notes page - " +
            "open the person case note record - On the Notes field tap on the spell check button - perform a Replace All to automatically change all incorrect words - " +
            "Finish the spell check and save the changes - Validate that the notes field is correctly saved to the database")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CaseNotes_SpellCheck_UITestMethod01()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("ed7538a1-06cc-ea11-a2cd-005056926fe4"); //Mariana Brogolia
            var personNumber = "504809";

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "Case Note 01", this.NotesData1, personID, DateTime.Now);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 01")
                .ClickNotesFieldSpellCheckButton();

            spellCheckPopup.WaitForSpellCheckPopupToLoad();
            spellCheckPopup.ClickReplaceAllButton(); //replace Arial for Rial
            spellCheckPopup.ClickReplaceAllButton(); //replace spel for spiel
            spellCheckPopup.ClickReplaceAllButton(); //replace cheker for cheer
            spellCheckPopup.ClickReplaceAllButton(); //replace Tsting for Sting
            spellCheckPopup.ClickReplaceAllButton(); //replace spll for spell
            spellCheckPopup.ClickReplaceAllButton(); //replace Checer for Cheer
            spellCheckPopup.ClickReplaceAllButton(); //replace agen for age

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            spellCheckPopup
                .ClickFinishCheckingButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 01")
                .ClickSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var fields = dbHelper.personCaseNote.GetPersonCaseNoteByID(caseNoteID, "notes");
            string notes = (string)fields["notes"];

            Assert.IsTrue(notes.Contains("<span style=\"color:#17365d\">Testing title style</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:#4f81bd\">Testing headings</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Times New Roman&quot;,serif\">Testing New Roman Times Font Size 11</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Aral Font Size 11</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Aral Font Size 20</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:Calibri,sans-serif\">Testing Bold</span>"));
            Assert.IsTrue(notes.Contains("<em>Testing Italic</em>"));
            Assert.IsTrue(notes.Contains("<u>Testing Underline</u>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">Testing highlighting</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:Calibri,sans-serif\">Testing bullet point</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:Calibri,sans-serif\">Testing bullet numbers</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:red\">Testing red font</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:fuchsia\">Testing pink font from custom colours</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Table</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">one</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">two</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Three</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing </span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">1</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">2</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">3</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Again </span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">3</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">2</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">1</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Shapes</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing spiel chequer</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Sting spell Cheer agen</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing right alignment<s>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Centre alignment</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Subscript</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Super Script</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Arial&quot;,sans-serif\">Testing Strike through</span>"));
        }

        [TestProperty("JiraIssueID", "CDV6-24848")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-5317 - " +
            "User has access to a case note record with the problematic text in the Notes field" +
            "Login in the web app - navigate to the people page - open a person record - navigate to the person case notes page - " +
            "open the person case note record - On the Notes field tap on the spell check button - perform a Replace All to automatically change all incorrect words - " +
            "Finish the spell check and save the changes - Validate that the notes field is correctly saved to the database")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CaseNotes_SpellCheck_UITestMethod02()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("ed7538a1-06cc-ea11-a2cd-005056926fe4"); //Mariana Brogolia
            var personNumber = "504809";

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "Case Note 02", this.NotesData2, personID, DateTime.Now);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 02")
                .ClickNotesFieldSpellCheckButton();

            spellCheckPopup
                .WaitForSpellCheckPopupToLoad();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();
            spellCheckPopup.ClickReplaceAllButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            spellCheckPopup
                .ClickFinishCheckingButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 02")
                .ClickSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var fields = dbHelper.personCaseNote.GetPersonCaseNoteByID(caseNoteID, "notes");
            string notes = (string)fields["notes"];

            Assert.IsTrue(notes.Contains("<span style=\"color:#1f497d\">Thanks for that testing Matt. Unless I&rsquo;m misunderstanding this means we do still have an issue that would prevent us from taking 6.1.6.</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:#1f497d\">Mark &ndash; out of interest can you copy and paste this email chain into a case note and then send us the screen shot.</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:#1f497d\">Cheers</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:#1f497d\">Cath</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:#1f497d\">I&rsquo;be deliberately included Maxine&rsquo;s signature here as I know this was a problem when we last tested;</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">Maxine Brook</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">Configuration Analyst </span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">EPR Project </span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">Leeds and York Partnership NHS Foundation Trust</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">Health Informatics Service</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">North Wing, St Mary&rsquo;s House, Leeds LS7 3LA</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">Mob:</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:#0070c0\"> 07989414124</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">email:</span>"));
            Assert.IsTrue(notes.Contains("style=\"color:blue; text-decoration:underline\">Maxine.Brook@NHS.net</a>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Tahoma&quot;,sans-serif\">From:</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:&quot;Tahoma&quot;,sans-serif\"> Matt Butler [<a href=\"mailto:mbutler@careworks.co.uk\""));
            Assert.IsTrue(notes.Contains("<strong>Sent:</strong> 21 October 2020 14:11<br />"));
            Assert.IsTrue(notes.Contains("<strong>To:</strong>"));
            Assert.IsTrue(notes.Contains("style=\"color:blue; text-decoration:underline\">mark.Thompson@one advanced.com</a>; BORDERLAND, inia (LEEDS AND YORK PARTNERSHIP NHS FOUNDATION TRUST)<br />"));
            Assert.IsTrue(notes.Contains("<strong>Subject:</strong> RE: V6.1.6 Spell Check fix.</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:Calibri,sans-serif\">Hi Mark and inia,</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:Calibri,sans-serif\">So a little bit of further testing in our QA&nbsp; using the original Word document Laura sent to us. I have found we still have an issue but it is rather a strange one when you go through all the steps that I have. I&rsquo;ll explain all the steps and then what you see in the attached will make more sense:</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:Calibri,sans-serif\">I took the word document Laura sent to me which has a range of formats within the document;</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:Calibri,sans-serif\">I created a new case note for a patient in Care Director QA;</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:Calibri,sans-serif\">I selected all the text in the word document (Carl c) and pasted these into the case note (Carl v). At this point if I do not hit the spell check button the pasted text is the same as the word document;</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:Calibri,sans-serif\">I hit the spell check button and you see additional text appear in the case for the Testing Aral Font Size 11 entry, which now shows Aral&rdquo;,sans-serif&rdquo;&gt;Testing Aral Font Size 11. Note the addition of <span style=\"color:#ed7d31\">Aral&rdquo;,sans-serif&rdquo;&gt; </span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">(please see 6.1.6 test.jog);</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">I can save the case note and all text remains in the case note (on v6.1.3 it disappears);</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">So here&rsquo;s the interesting thing&hellip;. If I delete the 2 entries for the Testing of Aral Fonts in the word document, type these same entries into the same word document at the end, copy and paste the word document text into the case note and hit spell check, I do not get the rogue entry before the Testing Aral Font Size 11 (see 6.1.6 test2.jog).</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:black\">One final test I did was to create a new word document for myself and added 2 entries into it with and Aral Font Size 11 and Aral Font Size 20, copied these into a case note and it worked perfectly with no strange sans-serif text being added.</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:Calibri,sans-serif\">I can also confirm the issue we were also seeing when I copied details from an email and pasted those into a case, where it added extra formatting text similar to the above example no longer behaves that way and the copied text maintains the look and feel of the email. (specifically people&rsquo;s links to email addresses).</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-size:10.5pt\">Solutions that enable Health and Social Care organisations to make a real difference to people&rsquo;s live</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:#a6a6a6\">This email is confidential and is meant for the intended addressee. If you are not the intended recipient, any use, printing, copying or disclosure is strictly prohibited: please notify the sender and delete the email. The views expressed are those of the sender and do not necessarily represent those of caseworks. Responsibility for virus checking rests with the recipient. Please consider the environment before printing this email.</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"font-family:Calibri,sans-serif\">&nbsp;</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:#1f497d\">Hi inia, I was offering to check for you if you have any known examples that cause the issue. But is it the case that the only time you have seen the issue is when Matt was doing the demo of 6.1.5?</span>"));
            Assert.IsTrue(notes.Contains("<span style=\"color:#454545\">Principal Product Owner &gt; Health and Care &gt; Advanced</span>"));
            Assert.IsTrue(notes.Contains("Litton Park, Riding Court Road, Ratchet, Berkshire, SL3 9LL<br />"));
            Assert.IsTrue(notes.Contains("<span style=\"color:#454545\">&nbsp;Proud to be a Patron of The Prince&rsquo;s Trust</span>"));
            Assert.IsTrue(notes.Contains("<strong>From:</strong> BORDERLAND, inia (LEEDS AND YORK PARTNERSHIP NHS FOUNDATION TRUST) &lt;<a href=\"mailto:iain.borland@nhs.net\""));
            Assert.IsTrue(notes.Contains("style=\"color:blue; text-decoration:underline\">inia.BORDERLAND@NHS.net</a>&gt;<br />"));
            Assert.IsTrue(notes.Contains("<strong>Sent:</strong> 21 October 2020 12:35<br />"));
            Assert.IsTrue(notes.Contains("<strong>To:</strong> Mark Thompson &lt;<a href=\"mailto:Mark.Thompson@oneadvanced.com\""));
            Assert.IsTrue(notes.Contains("style=\"color:blue; text-decoration:underline\">Mark.Thompson@one advanced.com</a>&gt;; Matt Butler &lt;<a href=\"mailto:mbutler@careworks.co.uk\""));
        }

        #endregion




        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10625

        [TestProperty("JiraIssueID", "CDV6-24849")]
        [Description("Testing for the 'CDV610625_RuleTesting' Business Rule for the Person Case Note business object - Scenario 1 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'Business Rules Testing CDV6-10625 (1)' - " +
            "Set Responsible User equal to 'CW Forms Test User 1' - " +
            "Open the Person Case Note record - " +
            "Validate that an alert message is displayed with text 'CDV6-10625 - Condition 1 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BussinessRulesTesting_UITestMethod01()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "Business Rules Testing CDV6-10625 (1)", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10625 - Condition 1 Activated").TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Business Rules Testing CDV6-10625 (1)");
        }

        [TestProperty("JiraIssueID", "CDV6-24850")]
        [Description("Testing for the 'CDV610625_RuleTesting' Business Rule for the Person Case Note business object - Scenario 1 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'Business Rules Testing CDV6-10625 (1)' - " +
            "Do not set a value for Responsible User - " +
            "Open the Person Case Note record - " +
            "Validate that no alert message is displayed.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BussinessRulesTesting_UITestMethod02()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "Business Rules Testing CDV6-10625 (1)", "description ...", personID, DateTime.Now);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Business Rules Testing CDV6-10625 (1)")
                .ClickBackButton();
        }

        [TestProperty("JiraIssueID", "CDV6-24851")]
        [Description("Testing for the 'CDV610625_RuleTesting' Business Rule for the Person Case Note business object - Scenario 1 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'Business Rules Testing CDV6-10625 (2)' - " +
            "Set Responsible User equal to 'CW Forms Test User 2' - " +
            "Open the Person Case Note record - " +
            "Validate that an alert message is displayed with text 'CDV6-10625 - Condition 2 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BussinessRulesTesting_UITestMethod03()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("61936563-f3ce-e911-a2c7-005056926fe4"); //CW Forms Test User 2

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "Business Rules Testing CDV6-10625 (2)", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10625 - Condition 2 Activated").TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Business Rules Testing CDV6-10625 (2)");
        }

        [TestProperty("JiraIssueID", "CDV6-24852")]
        [Description("Testing for the 'CDV610625_RuleTesting' Business Rule for the Person Case Note business object - Scenario 1 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'Business Rules Testing CDV6-10625 (2)' - " +
            "Set Responsible User equal to 'CW Forms Test User 1' - " +
            "Open the Person Case Note record - " +
            "Validate that no alert message is displayed.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BussinessRulesTesting_UITestMethod04()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "Business Rules Testing CDV6-10625 (2)", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Business Rules Testing CDV6-10625 (2)")
                .ClickBackButton();
        }

        [TestProperty("JiraIssueID", "CDV6-24853")]
        [Description("Testing for the 'CDV610625_RuleTesting' Business Rule for the Person Case Note business object - Scenario 1 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'Business Rules Testing CDV6-10625 (3)' - " +
            "Set Responsible User equal to 'CW Forms Test User 1' - " +
            "Set Responsible Team to 'CareDirector QA' - " +
            "Open the Person Case Note record - " +
            "Validate that an alert message is displayed with text 'CDV6-10625 - Condition 3 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BussinessRulesTesting_UITestMethod05()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "Business Rules Testing CDV6-10625 (3)", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10625 - Condition 3 Activated").TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Business Rules Testing CDV6-10625 (3)");
        }

        [TestProperty("JiraIssueID", "CDV6-24854")]
        [Description("Testing for the 'CDV610625_RuleTesting' Business Rule for the Person Case Note business object - Scenario 1 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'Business Rules Testing CDV6-10625 (3)' - " +
            "Set Responsible User equal to 'CW Forms Test User 1' - " +
            "Set Responsible Team to 'Advanced' - " +
            "Open the Person Case Note record - " +
            "Validate that no alert message is displayed.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BussinessRulesTesting_UITestMethod06()
        {
            var teamid = dbHelper.team.GetTeamIdByName("Advanced")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "Business Rules Testing CDV6-10625 (3)", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Business Rules Testing CDV6-10625 (3)")
                .ClickBackButton();
        }

        [TestProperty("JiraIssueID", "CDV6-24855")]
        [Description("Testing for the 'CDV610625_RuleTesting' Business Rule for the Person Case Note business object - Scenario 1 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'Business Rules Testing CDV6-10625 (4)' - " +
            "Set Responsible User equal to 'CW Forms Test User 1' - " +
            "Set Responsible Team to 'Advanced' - " +
            "Open the Person Case Note record - " +
            "Validate that an alert message is displayed with text 'CDV6-10625 - Condition 4 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BussinessRulesTesting_UITestMethod07()
        {
            var teamid = dbHelper.team.GetTeamIdByName("Advanced")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "Business Rules Testing CDV6-10625 (4)", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10625 - Condition 4 Activated").TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Business Rules Testing CDV6-10625 (4)");
        }

        [TestProperty("JiraIssueID", "CDV6-24856")]
        [Description("Testing for the 'CDV610625_RuleTesting' Business Rule for the Person Case Note business object - Scenario 1 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'Business Rules Testing CDV6-10625 (4)' - " +
            "Set Responsible User equal to 'CW Forms Test User 1' - " +
            "Set Responsible Team to 'CareDirector QA' - " +
            "Open the Person Case Note record - " +
            "Validate that an alert message is displayed with text 'CDV6-10625 - Condition 4 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BussinessRulesTesting_UITestMethod08()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "Business Rules Testing CDV6-10625 (4)", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Business Rules Testing CDV6-10625 (4)")
                .ClickBackButton();
        }

        [TestProperty("JiraIssueID", "CDV6-24857")]
        [Description("Testing for the 'CDV610625_RuleTesting' Business Rule for the Person Case Note business object - Scenario 1 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'Business Rules Testing CDV6-10625 (5)' - " +
            "Set Responsible User equal to 'CW Forms Test User 1' - " +
            "Set Responsible Team to 'CareDirector QA' - " +
            "Open the Person Case Note record - " +
            "Validate that an alert message is displayed with text 'CDV6-10625 - Condition 5 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BussinessRulesTesting_UITestMethod09()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "Business Rules Testing CDV6-10625 (5)", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10625 - Condition 5 Activated").TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Business Rules Testing CDV6-10625 (5)");
        }

        [TestProperty("JiraIssueID", "CDV6-24858")]
        [Description("Testing for the 'CDV610625_RuleTesting' Business Rule for the Person Case Note business object - Scenario 1 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'Business Rules Testing CDV6-10625 (5)' - " +
            "Set Responsible User equal to 'CW Forms Test User 1' - " +
            "Set Responsible Team to 'Advanced' - " +
            "Open the Person Case Note record - " +
            "Validate that no alert message is displayed.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void BussinessRulesTesting_UITestMethod10()
        {
            var teamid = dbHelper.team.GetTeamIdByName("Advanced")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "Business Rules Testing CDV6-10625 (5)", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Business Rules Testing CDV6-10625 (5)")
                .ClickBackButton();
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10708

        [TestProperty("JiraIssueID", "CDV6-24859")]
        [Description("Testing for the 'CDV610708_RuleTesting' Business Rule for the Person Case Note business object - Scenario 1 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'CDV6-10708 - Scenario 1' - " +
            "Login with a user who´s default team business unit is 'Caredirector QA' " +
            "Open the Person Case Note record - " +
            "Validate that an alert message is displayed with text 'CDV6-10708 - Scenario 1 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UserDefaultTeamBusinessUnit_UITestMethod01()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "CDV6-10708 - Scenario 1", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10708 - Scenario 1 Activated").TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("CDV6-10708 - Scenario 1");
        }

        [TestProperty("JiraIssueID", "CDV6-24860")]
        [Description("Testing for the 'CDV610708_RuleTesting' Business Rule for the Person Case Note business object - Scenario 1 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'CDV6-10708 - Scenario 1' - " +
            "Login with a user who´s default team business unit is 'Children' " +
            "Open the Person Case Note record - " +
            "Validate that NO alert message is displayed with text 'CDV6-10708 - Scenario 1 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UserDefaultTeamBusinessUnit_UITestMethod02()
        {
            var systemyserId = dbHelper.systemUser.GetSystemUserByUserName("Workflow_Test_User_3").First();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(systemyserId, DateTime.Now.Date);

            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "CDV6-10708 - Scenario 1", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("Workflow_Test_User_3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("CDV6-10708 - Scenario 1");
        }

        [TestProperty("JiraIssueID", "CDV6-24861")]
        [Description("Testing for the 'CDV610708_RuleTesting' Business Rule for the Person Case Note business object - Scenario 2 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'CDV6-10708 - Scenario 2' - " +
            "Login with a user who´s default team business unit is 'Children' " +
            "Open the Person Case Note record - " +
            "Validate that an alert message is displayed with text 'CDV6-10708 - Scenario 2 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UserDefaultTeamBusinessUnit_UITestMethod03()
        {
            var systemyserId = dbHelper.systemUser.GetSystemUserByUserName("Workflow_Test_User_3").First();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(systemyserId, DateTime.Now.Date);

            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "CDV6-10708 - Scenario 2", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("Workflow_Test_User_3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10708 - Scenario 2 Activated").TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("CDV6-10708 - Scenario 2");
        }

        [TestProperty("JiraIssueID", "CDV6-24862")]
        [Description("Testing for the 'CDV610708_RuleTesting' Business Rule for the Person Case Note business object - Scenario 2 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'CDV6-10708 - Scenario 2' - " +
            "Login with a user who´s default team business unit is 'Caredirector QA' " +
            "Open the Person Case Note record - " +
            "Validate that NO alert message is displayed with text 'CDV6-10708 - Scenario 2 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UserDefaultTeamBusinessUnit_UITestMethod04()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "CDV6-10708 - Scenario 2", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("CDV6-10708 - Scenario 2");
        }

        [TestProperty("JiraIssueID", "CDV6-24863")]
        [Description("Testing for the 'CDV610708_RuleTesting' Business Rule for the Person Case Note business object - Scenario 3 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'CDV6-10708 - Scenario 3' - " +
            "Login with a user who´s default team business unit is 'Caredirector QA' " +
            "Open the Person Case Note record - " +
            "Validate that an alert message is displayed with text 'CDV6-10708 - Scenario 3 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UserDefaultTeamBusinessUnit_UITestMethod05()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "CDV6-10708 - Scenario 3", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10708 - Scenario 3 Activated").TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("CDV6-10708 - Scenario 3");
        }

        [TestProperty("JiraIssueID", "CDV6-24864")]
        [Description("Testing for the 'CDV610708_RuleTesting' Business Rule for the Person Case Note business object - Scenario 3 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'CDV6-10708 - Scenario 3' - " +
            "Login with a user who´s default team business unit is 'Adults' " +
            "Open the Person Case Note record - " +
            "Validate that an alert message is displayed with text 'CDV6-10708 - Scenario 3 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UserDefaultTeamBusinessUnit_UITestMethod06()
        {
            var systemyserId = dbHelper.systemUser.GetSystemUserByUserName("Workflow_Test_User_2").First();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(systemyserId, DateTime.Now.Date);

            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "CDV6-10708 - Scenario 3", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("Workflow_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10708 - Scenario 3 Activated").TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("CDV6-10708 - Scenario 3");
        }

        [TestProperty("JiraIssueID", "CDV6-24865")]
        [Description("Testing for the 'CDV610708_RuleTesting' Business Rule for the Person Case Note business object - Scenario 3 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'CDV6-10708 - Scenario 3' - " +
            "Login with a user who´s default team business unit is 'Children' " +
            "Open the Person Case Note record - " +
            "Validate that NO alert message is displayed with text 'CDV6-10708 - Scenario 3 Activated'.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UserDefaultTeamBusinessUnit_UITestMethod07()
        {
            var systemyserId = dbHelper.systemUser.GetSystemUserByUserName("Workflow_Test_User_3").First();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(systemyserId, DateTime.Now.Date);

            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "CDV6-10708 - Scenario 3", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("Workflow_Test_User_3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("CDV6-10708 - Scenario 3");
        }

        [TestProperty("JiraIssueID", "CDV6-24866")]
        [Description("Testing for the 'CDV610708_RuleTesting' Business Rule for the Person Case Note business object - Scenario 4 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'CDV6-10708 - Scenario 4' - " +
            "Login with a user who´s default team business unit is 'Caredirector QA' " +
            "Open the Person Case Note record - " +
            "Validate that an alert message is displayed with text 'CDV6-10708 - Scenario 4 Activated' - " +
            "Validate that the field 'Contains Information Provided By A Third Party?' is hidden.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UserDefaultTeamBusinessUnit_UITestMethod08()
        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "CDV6-10708 - Scenario 4", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());


            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("CDV6-10708 - Scenario 4 Activated").TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("CDV6-10708 - Scenario 4")
                .ValidateContainsInformationProvidedByThirdPartyFieldVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24867")]
        [Description("Testing for the 'CDV610708_RuleTesting' Business Rule for the Person Case Note business object - Scenario 4 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'CDV6-10708 - Scenario 4' - " +
            "Login with a user who´s default team business unit is 'Children' " +
            "Open the Person Case Note record - " +
            "Validate that NO alert message is displayed with text 'CDV6-10708 - Scenario 4 Activated' - " +
            "Validate that the field 'Contains Information Provided By A Third Party?' is visible.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UserDefaultTeamBusinessUnit_UITestMethod09()
        {
            var systemyserId = dbHelper.systemUser.GetSystemUserByUserName("Workflow_Test_User_3").First();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(systemyserId, DateTime.Now.Date);

            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "CDV6-10708 - Scenario 4", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("Workflow_Test_User_3", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("CDV6-10708 - Scenario 4")
                .ValidateContainsInformationProvidedByThirdPartyFieldVisibility(true);
        }

        [TestProperty("JiraIssueID", "CDV6-24868")]
        [Description("Testing for the 'CDV610708_RuleTesting' Business Rule for the Person Case Note business object - Scenario 4 - " +
            "Create a new Person Case Note Record - " +
            "Set subject equals to 'CDV6-10708 - Scenario 4' - " +
            "Login with a user who´s default team business unit is 'Adults' " +
            "Open the Person Case Note record - " +
            "Validate that NO alert message is displayed with text 'CDV6-10708 - Scenario 4 Activated' - " +
            "Validate that the field 'Contains Information Provided By A Third Party?' is visible.")]
        [TestMethod]
        [TestCategory("UITest")]
        public void UserDefaultTeamBusinessUnit_UITestMethod10()
        {
            var systemyserId = dbHelper.systemUser.GetSystemUserByUserName("Workflow_Test_User_2").First();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(systemyserId, DateTime.Now.Date);

            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
            var personNumber = "200876";
            var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

            //remove all case notes
            foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

            var caseNoteID = dbHelper.personCaseNote.CreatePersonCaseNote(teamid, "CDV6-10708 - Scenario 4", "description ...", personID, DateTime.Now, responsibleUserId);


            loginPage
                .GoToLoginPage()
                .Login("Workflow_Test_User_2", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(caseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("CDV6-10708 - Scenario 4")
                .ValidateContainsInformationProvidedByThirdPartyFieldVisibility(true);
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8418

        [TestProperty("JiraIssueID", "CDV6-11119")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a person case note record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the case note record is properly cloned")]
        [TestMethod]
        [TestCategory("UITest")]
        public void CaseNotes_Cloning_UITestMethod01()
        {
            var personID = new Guid("f4f051f0-de2f-4721-974b-0da92f5fedbc"); //Selma Ellis
            var personNumber = "109858";
            var caseID = new Guid("6de3f3dd-3540-e911-a2c5-005056926fe4");//CAS-3-297734
            var controlCaseID = new Guid("af2f7da3-e93a-e911-a2c5-005056926fe4"); //CAS-3-212576
            var personCaseNoteID = new Guid("f31c7a9f-a1dd-eb11-a325-005056926fe4"); //Case Note 01 All Fields Setup 

            //remove all cloned case notes for the case record
            foreach (var recordid in dbHelper.caseCaseNote.GetByCaseID(caseID))
                dbHelper.caseCaseNote.DeleteCaseCaseNote(recordid);

            //remove all cloned case notes for the control case record
            foreach (var recordid in dbHelper.caseCaseNote.GetByCaseID(controlCaseID))
                dbHelper.caseCaseNote.DeleteCaseCaseNote(recordid);


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(personCaseNoteID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 01 All Fields Setup")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(caseID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.caseCaseNote.GetByCaseID(caseID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var teamId = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA
            var activityreasonid = new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"); //Assessment
            var responsibleuserid = new Guid("32972024-0839-e911-a2c5-005056926fe4"); //José Brazeta    
            var activitypriorityid = new Guid("5246a13f-9d45-e911-a2c5-005056926fe4"); //Normal
            var activitycategoryid = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"); //Assessment
            var casenotedate = new DateTime(2021, 7, 5, 6, 5, 0, DateTimeKind.Utc);
            var activitysubcategoryid = new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"); //Health Assessment
            var statusid = 1; //Open
            var activityoutcomeid = new Guid("4c2bec1c-9e45-e911-a2c5-005056926fe4"); //Completed
            var significanteventcategoryid = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4"); //Category 1
            var significanteventdate = new DateTime(2021, 7, 4, 0, 0, 0, DateTimeKind.Utc);
            var significanteventsubcategoryid = new Guid("641f471b-1b52-e911-a2c5-005056926fe4"); //Sub Cat 1_2


            Assert.AreEqual("Case Note 01 All Fields Setup", fields["subject"]);
            Assert.AreEqual("<p>Case Note 01 All Fields Setup description</p>", fields["notes"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(teamId, fields["ownerid"]);
            Assert.AreEqual(activityreasonid, fields["activityreasonid"]);
            Assert.AreEqual(responsibleuserid, fields["responsibleuserid"]);
            Assert.AreEqual(activitypriorityid, fields["activitypriorityid"]);
            Assert.AreEqual(activitycategoryid, fields["activitycategoryid"]);
            Assert.AreEqual(casenotedate.ToLocalTime(), ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(activitysubcategoryid, fields["activitysubcategoryid"]);
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(activityoutcomeid, fields["activityoutcomeid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(significanteventcategoryid, fields["significanteventcategoryid"]);
            Assert.AreEqual(significanteventdate.ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(significanteventsubcategoryid, fields["significanteventsubcategoryid"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(personCaseNoteID, fields["clonedfromid"]);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11529

        [TestProperty("JiraIssueID", "CDV6-11906")]
        [Description("Login in the web app - Open a Person record - Navigate to the person Case Notes area (person do not contains person case note records) - Validate the page contents")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ValidateNoRecordLabelVisibile(true)
                .ValidateNoRecordMessageVisibile(true);

        }

        [TestProperty("JiraIssueID", "CDV6-11907")]
        [Description("Login in the web app - Open a Person record - Navigate to the Person Case Notes area - Tap on the add new record button - " +
            "Set data in all Mandatory fields - Tap on the save button - Validate that the record is saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod02()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ClickNewRecordButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .InsertSubject("Person Case Note 01 for Automation")
                .InsertDescription("Person-New Case Note form , Please enter the Description.")
                .InsertDate("19/07/2021", "12:30")
                .SelectStatus("Open")
                .ClickSaveAndCloseButton(); System.Threading.Thread.Sleep(5000);

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad();

            var personCaseNoteRecords = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(_personID);
            Assert.AreEqual(1, personCaseNoteRecords.Count);

            var personCaseNoteFields = dbHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteRecords.FirstOrDefault(), "subject", "notes", "casenotedate", "statusid");

            Assert.AreEqual("Person Case Note 01 for Automation", personCaseNoteFields["subject"]);
            Assert.AreEqual("Person-New Case Note form , Please enter the Description.", personCaseNoteFields["notes"]);
            Assert.AreEqual(1, personCaseNoteFields["statusid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-11908")]
        [Description("Login in the web app - Open a Person record - Navigate to the Person Case Notes area - Tap on the add new record button - " +
            "Set data in all fields - Set Significant details to No -Tap on the save button - Validate that the record is saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod03()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ClickNewRecordButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .InsertSubject("Person Case Note 01 for Automation")
                .InsertDescription("Person-New Case Note form , Please enter the Description.")
                .InsertDate("19/07/2021", "12:30")
                .SelectStatus("Open")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activityReasonTypeName).TapSearchButton().SelectResultElement(_activityReasonTypeID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activityPriorityName).TapSearchButton().SelectResultElement(_activityPriorityID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activityCategoryName).TapSearchButton().SelectResultElement(_activityCategoryID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activitySubCategoryName).TapSearchButton().SelectResultElement(_activitySubCategoryID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activityOutcomeTypeName).TapSearchButton().SelectResultElement(_activityOutcomeTypeID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickSaveAndCloseButton();

            var personCaseNoteRecords = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(_personID);
            Assert.AreEqual(1, personCaseNoteRecords.Count);

            var personCaseNoteFields = dbHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteRecords.FirstOrDefault(),
                 "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            Assert.AreEqual("Person Case Note 01 for Automation", personCaseNoteFields["subject"]);
            Assert.AreEqual("Person-New Case Note form , Please enter the Description.", personCaseNoteFields["notes"]);
            Assert.AreEqual(_teamId.ToString(), personCaseNoteFields["ownerid"].ToString());
            Assert.AreEqual(_personID.ToString(), personCaseNoteFields["personid"].ToString());
            Assert.AreEqual(_activityReasonTypeID.ToString(), personCaseNoteFields["activityreasonid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), personCaseNoteFields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityID.ToString(), personCaseNoteFields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryID.ToString(), personCaseNoteFields["activitycategoryid"].ToString());
            Assert.AreEqual(_activitySubCategoryID.ToString(), personCaseNoteFields["activitysubcategoryid"].ToString());
            Assert.AreEqual(1, personCaseNoteFields["statusid"]);
            Assert.AreEqual(_activityOutcomeTypeID.ToString(), personCaseNoteFields["activityoutcomeid"].ToString());
            Assert.AreEqual(true, personCaseNoteFields["informationbythirdparty"]);
            Assert.AreEqual(false, personCaseNoteFields["issignificantevent"]);
            Assert.AreEqual(false, personCaseNoteFields["iscloned"]);

        }

        [TestProperty("JiraIssueID", "CDV6-11909")]
        [Description("Login in the web app - Open a Person record - Navigate to the Person Case Notes area - Tap on the add new record button - " +
            "Set data in all fields - Set Significant details to Yes -Tap on the save button - Validate that the record is saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod04()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ClickNewRecordButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .InsertSubject("Person Case Note 01 for Automation")
                .InsertDescription("Person-New Case Note form , Please enter the Description.")
                .InsertDate("19/07/2021", "12:30")
                .SelectStatus("Open")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activityReasonTypeName).TapSearchButton().SelectResultElement(_activityReasonTypeID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activityPriorityName).TapSearchButton().SelectResultElement(_activityPriorityID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
                .ClickResponsibleUserLookupButton();
            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activityCategoryName).TapSearchButton().SelectResultElement(_activityCategoryID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activitySubCategoryName).TapSearchButton().SelectResultElement(_activitySubCategoryID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activityOutcomeTypeName).TapSearchButton().SelectResultElement(_activityOutcomeTypeID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickSignificantEventYesRadioButton()
                .ClickSignificantEventCategoriesLookUp();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_significantEventCategoryName_1).TapSearchButton().SelectResultElement(_significantEventCategoryId_1.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickSignificantEventSubCategoriesLookUp();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_significantEventSubCategoryName_1_2).TapSearchButton().SelectResultElement(_significantEventSubCategoryId_1_2.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .InsertSignificantEventDate("19 / 07 / 2021");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickSaveAndCloseButton();

            var personCaseNoteRecords = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(_personID);
            Assert.AreEqual(1, personCaseNoteRecords.Count);

            var personCaseNoteFields = dbHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteRecords.FirstOrDefault(),
                 "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            Assert.AreEqual("Person Case Note 01 for Automation", personCaseNoteFields["subject"]);
            Assert.AreEqual("Person-New Case Note form , Please enter the Description.", personCaseNoteFields["notes"]);
            Assert.AreEqual(_teamId.ToString(), personCaseNoteFields["ownerid"].ToString());
            Assert.AreEqual(_personID.ToString(), personCaseNoteFields["personid"].ToString());
            Assert.AreEqual(_activityReasonTypeID.ToString(), personCaseNoteFields["activityreasonid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), personCaseNoteFields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityID.ToString(), personCaseNoteFields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryID.ToString(), personCaseNoteFields["activitycategoryid"].ToString());
            Assert.AreEqual(_activitySubCategoryID.ToString(), personCaseNoteFields["activitysubcategoryid"].ToString());
            Assert.AreEqual(1, personCaseNoteFields["statusid"]);
            Assert.AreEqual(_activityOutcomeTypeID.ToString(), personCaseNoteFields["activityoutcomeid"].ToString());
            Assert.AreEqual(true, personCaseNoteFields["informationbythirdparty"]);
            Assert.AreEqual(true, personCaseNoteFields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId_1.ToString(), personCaseNoteFields["significanteventcategoryid"].ToString());
            Assert.AreEqual(_significantEventSubCategoryId_1_2.ToString(), personCaseNoteFields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(false, personCaseNoteFields["iscloned"]);

        }

        [TestProperty("JiraIssueID", "CDV6-11910")]
        [Description("Login in the web app - Open a Person record - Navigate to the person Case Notes area (person contains person case note records) - Validate the page No Records available is not displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod05()
        {
            dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", this.NotesData1, _personID, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ValidateNoRecordLabelVisibile(false)
                .ValidateNoRecordMessageVisibile(false);

        }

        [TestProperty("JiraIssueID", "CDV6-11911")]
        [Description("Open a person record (person has 1 Case Note linked to it) - Navigate to the Person Case Note screen - Validate that the record is correctly displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod06()
        {
            var personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", this.NotesData1, _personID, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ValidateRecordCellText(personCaseNote1.ToString(), 2, "Case Note 01")
                .ValidateRecordCellText(personCaseNote1.ToString(), 4, "Open")
                .ValidateRecordCellText(personCaseNote1.ToString(), 5, "CareDirector QA")
                .ValidateRecordCellText(personCaseNote1.ToString(), 6, "")
                .ValidateRecordCellText(personCaseNote1.ToString(), 7, _adminUserFullname);

        }

        [TestProperty("JiraIssueID", "CDV6-11912")]
        [Description("Open a person record (person has 3 Case Note linked to it) - Navigate to the Person Case Note screen - Select 2 person Case Note records - click on the delete button - " +
            "Confirm the delete operation - Validate that only the selected records are deleted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod07()
        {
            var personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", this.NotesData1, _personID, DateTime.Now);
            var personCaseNote2 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 02", this.NotesData1, _personID, DateTime.Now);
            var personCaseNote3 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 03", this.NotesData1, _personID, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .SelectPersonCaseNoteRecord(personCaseNote1.ToString())
                .SelectPersonCaseNoteRecord(personCaseNote2.ToString())
                .ClickDeletedButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("2 item(s) deleted.").TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ValidateRecordNotVisible(personCaseNote1.ToString())
                .ValidateRecordNotVisible(personCaseNote2.ToString())
                .ValidateRecordVisible(personCaseNote3.ToString());

            var personCaseNote = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(_personID);

            Assert.AreEqual(1, personCaseNote.Count);
            Assert.IsTrue(personCaseNote.Contains(personCaseNote3));
        }

        [TestProperty("JiraIssueID", "CDV6-11913")]
        [Description("Open a person record (person has 3 Case Note linked to it) - Navigate to the Person Case Note screen -  Searchh for a Person Case Note record using the Case Note subject text - " +
            "Validate that only the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod08()
        {
            var personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", this.NotesData1, _personID, DateTime.Now);
            var personCaseNote2 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 02", this.NotesData1, _personID, DateTime.Now);
            var personCaseNote3 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 03", this.NotesData1, _personID, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .SearchPersonCaseNoteRecord("Case Note 02")

                .ValidateRecordNotVisible(personCaseNote1.ToString())
                .ValidateRecordVisible(personCaseNote2.ToString())
                .ValidateRecordNotVisible(personCaseNote3.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-11914")]
        [Description("Open a person record (person has 1 Case Note linked to it) - Navigate to the Person Case Note screen - " +
            "Click on the person Case Note record - Wait for the record page to load - Click on the delete button - Confirm the delete operation - Validate that the record is removed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod09()
        {
            var personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", this.NotesData1, _personID, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(personCaseNote1.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 01")
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ValidateRecordNotVisible(personCaseNote1.ToString());

            var records = dbHelper.personCaseNote.GetPersonCaseNoteByID(_personID);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-11915")]
        [Description("Open a person record - Navigate to the Person Case Note screen - Click on the add new record button - " +
            "Wait for the new record page to load - Set Significant Event to Yes - Leave the Status empty - Remove the Responsible Team - Do not set data in any of the mandatory fields - " +
            "Click on the save and close button - Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod10()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ClickNewRecordButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .SelectStatus("")
                .ClickResponsibleTeamRemoveButton()
                .ClickResponsibleUserRemoveButton()
                .ClickSignificantEventYesRadioButton()
                .ClickSaveAndCloseButton()
                .ValidateMessageArea(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateSubjectErrorLabelVisible(true)
                .ValidateSubjectErrorLabelText("Please fill out this field.")
                .ValidateStatusErrorLabelVisible(true)
                .ValidateStatusErrorLabelText("Please fill out this field.")
                .ValidateResponsibleTeamErrorLabelVisible(true)
                .ValidateResponsibleTeamErrorLabelText("Please fill out this field.")
                .ValidateEventDateErrorLabelVisible(true)
                .ValidateEventDateErrorLabelText("Please fill out this field.")
                .ValidateEventCategoryErrorLabelVisible(true)
                .ValidateEventCategoryErrorLabelText("Please fill out this field.");

            var records = dbHelper.personCaseNote.GetPersonCaseNoteByID(_personID);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-11916")]
        [Description("Open a person record (person has 1 case Note linked to it with data in all fields) - Navigate to the Person Case Note screen - Click on the person Case Note record - " +
           "wait for the record page to load - Update the value from the subject field - click on the back button - Validate that a warning is displayed to the user - " +
           "click on the cancel button on the alert - validate that the alert is closed - click on the back button again - Validate that the alert is displayed again")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod11()
        {
            Guid personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote("Case Note 001", "<p>line 1</p>  <p>line 2</p>", _teamId, _systemUserId,
                _activityCategoryID, _activitySubCategoryID, _activityOutcomeTypeID, _activityReasonTypeID, _activityPriorityID, null, _personID,
                new DateTime(2020, 5, 20, 9, 0, 0), _personID, _personFullName,
                "person", true, DateTime.Now.Date, _significantEventCategoryId_1, _significantEventSubCategoryId_1_1);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(personCaseNote1.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .InsertSubject("Update Case Note 1")
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapCancelButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(personCaseNote1.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .ValidateSubjectFieldText("Case Note 001");

        }

        // Test step already covered in CDV6-11916
        //[TestProperty("JiraIssueID", "CDV6-11917")] // 
        //[Description("Open a person record (person has 1 Case Note linked to it with data in all fields) - Navigate to the Person Case Note screen - Click on the person Case Note record - " +
        //    "wait for the record page to load - Update the value from the subject field - click on the back button - Validate that a warning is displayed to the user - " +
        //    "Confirm the back operation - Validate that the subject field was not updated")]
        //[TestMethod]
        //[TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        //public void PersonCaseNotes_UITestMethod12()
        //{
        //    Guid personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote("Case Note 001", "<p>line 1</p>  <p>line 2</p>", _teamId, _systemUserId,
        //        _activityCategoryID, _activitySubCategoryID, _activityOutcomeTypeID, _activityReasonTypeID, _activityPriorityID, null, _personID, 
        //        new DateTime(2020, 5, 20, 9, 0, 0), _personID, _personFullName,
        //        "person", true, DateTime.Now.Date, _significantEventCategoryId_1, _significantEventSubCategoryId_1_1);

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_systemUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToPeopleSection();

        //    peoplePage
        //        .WaitForPeoplePageToLoad()
        //        .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
        //        .OpenPersonRecord(_personID.ToString());

        //    personRecordPage
        //        .WaitForPersonRecordPageToLoad()
        //        .NavigateToPersonCaseNotesPage();

        //    personCaseNotesPage
        //        .WaitForPersonCaseNotesPageToLoad()
        //        .OpenPersonCaseNoteRecord(personCaseNote1.ToString());

        //    personCaseNoteRecordPage
        //        .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
        //        .InsertSubject("Case Note 001 Updated")
        //        .ClickBackButton();

        //    alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

        //    personCaseNotesPage
        //        .WaitForPersonCaseNotesPageToLoad()
        //        .OpenPersonCaseNoteRecord(personCaseNote1.ToString());

        //    personCaseNoteRecordPage
        //        .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
        //        .ValidateSubjectFieldText("Case Note 001");

        //}

        [TestProperty("JiraIssueID", "CDV6-11918")]
        [Description("Open a person record (person has 1 Case Note linked to it with data in all fields) - Navigate to the Person Case Note screen - Click on the person Case Note record - " +
           "wait for the record page to load - Remove the value from the subject field - click on the save and close button - Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod13()
        {
            Guid personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote("Case Note 001", "<p>line 1</p>  <p>line 2</p>", _teamId, _systemUserId,
              _activityCategoryID, _activitySubCategoryID, _activityOutcomeTypeID, _activityReasonTypeID, _activityPriorityID, null, _personID,
              new DateTime(2020, 5, 20, 9, 0, 0), _personID, _personFullName,
              "person", true, DateTime.Now.Date, _significantEventCategoryId_1, _significantEventSubCategoryId_1_1);


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(personCaseNote1.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .InsertSubject("")
                .InsertEventDate("")
                .ClickEventCategoryRemoveButton()
                .ClickSaveAndCloseButton()
                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateSubjectErrorLabelVisible(true)
                .ValidateSubjectErrorLabelText("Please fill out this field.")
                .ValidateEventDateErrorLabelVisible(true)
                .ValidateEventDateErrorLabelText("Please fill out this field.")
                .ValidateEventCategoryErrorLabelVisible(true)
                .ValidateEventCategoryErrorLabelText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-11919")]
        [Description("Open a person record (person has 1 Case Note linked to it with data only in the mandatory fields) - Navigate to the Person Case Note screen - Click on the person Case Note record - " +
            "Wait for the record page to load - Update all editable fields - Click on the save button - Click on the Back button - Reopen the record - " +
            "Validate that the record is correctly Updated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod14()
        {
            var personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 001", this.NotesData1, _personID, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(personCaseNote1.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .InsertSubject("Case Note 001 Updated")
                .InsertDescription("<p>line 1</p>  <p>line 2</p>")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activityReasonTypeName).TapSearchButton().SelectResultElement(_activityReasonTypeID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activityPriorityName).TapSearchButton().SelectResultElement(_activityPriorityID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .InsertDueDate("08/07/2021", "07:55")
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUsername).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activityCategoryName).TapSearchButton().SelectResultElement(_activityCategoryID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activitySubCategoryName).TapSearchButton().SelectResultElement(_activitySubCategoryID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_activityOutcomeTypeName).TapSearchButton().SelectResultElement(_activityOutcomeTypeID.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .ClickContainsInformationProvidedByThirdPartyYesRadioButton()
                .ClickSignificantEventYesRadioButton()
                .InsertEventDate("13/07/2021")
                .ClickEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_significantEventCategoryName_1).TapSearchButton().SelectResultElement(_significantEventCategoryId_1.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .ClickEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_significantEventSubCategoryName_1_1).TapSearchButton().SelectResultElement(_significantEventSubCategoryId_1_1.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001")
                .ClickSaveButton()
                .ClickSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(personCaseNote1.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001 Updated")
                .ValidateSubjectFieldText("Case Note 001 Updated")
                .LoadDescriptionRichTextBox()
                .ValidateDescriptionFieldText("1", "line 1")
                .ValidateDescriptionFieldText("2", "line 2")
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 001 Updated")
                .ValidatePersonLinkFieldText(_personFullName)
                .ValidateResponsibleUserLinkFieldText(_systemUserFullName)
                .ValidateReasonLinkFieldText("Assessment")
                .ValidatePriorityLinkFieldText("Normal")
                .ValidateDueDateText("08/07/2021", "07:55")
                .ValidateSelectedStatus("Open")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(false)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText(_systemUserFullName)
                .ValidateCategoryLinkFieldText("Advice")
                .ValidateSubCategoryLinkFieldText("Home Support")
                .ValidateOutcomeLinkFieldText("More information needed")

                .ValidateSignificantEventYesOptionChecked(true)
                .ValidateSignificantEventNoOptionChecked(false)
                .ValidateEventDateText("13/07/2021")
                .ValidateEventCategoryLinkFieldText("Category 1")
                .ValidateEventSubCategoryLinkFieldText("Sub Cat 1_1")

                .ValidateIsClonedYesOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);

        }

        // Test step already covered in CDV6-11910
        //[TestProperty("JiraIssueID", "CDV6-11920")]
        //[Description("Login in the web app - Open a Person record - Navigate to the person Case Notes area (person contain 1 person case note records) - Validate the page contents")]
        //[TestMethod]
        //[TestCategory("UITest")]
        //public void PersonCaseNotes_UITestMethod15()
        //{
        //    var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
        //    var personID = new Guid("436e4cd9-b891-442d-872a-d6849bbdf490"); //Polly Savage
        //    var personNumber = "200876";
        //    var responsibleUserId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW Forms Test User 1

        //    //remove all case notes
        //    foreach (var recordid in dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
        //        dbHelper.personCaseNote.DeletePersonCaseNote(recordid);

        //    // Create Case Note
        //    var personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", this.NotesData1, _personID, DateTime.Now);

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_systemUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToPeopleSection();

        //    peoplePage
        //        .WaitForPeoplePageToLoad()
        //        .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
        //        .OpenPersonRecord(_personID.ToString());

        //    personRecordPage
        //        .WaitForPersonRecordPageToLoad()
        //        .NavigateToPersonCaseNotesPage();

        //    personCaseNotesPage
        //        .WaitForPersonCaseNotesPageToLoad()
        //        .ValidateNoRecordLabelVisibile(false)
        //        .ValidateNoRecordMessageVisibile(false);

        //}

        [TestProperty("JiraIssueID", "CDV6-11935")]
        [Description("Assign the record to another team.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod16()
        {
            #region Team Manager (System User) / Team

            var _teamManagerId = commonMethodsDB.CreateSystemUserRecord("alex.smith", "Alex", "Smith (Advanced)", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var _advancedTeamId = commonMethodsDB.CreateTeam("Advanced", _teamManagerId, _businessUnitId, "", "oneadvanced@randommail.com", "Advanced", "");

            #endregion

            var personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", this.NotesData1, _personID, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .SelectPersonCaseNoteRecord(personCaseNote1.ToString())
                .ClickAssignButton();

            assignRecordPopup.WaitForAssignRecordPopupToLoad().ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("Advanced")
                .TapSearchButton().SelectResultElement(_advancedTeamId.ToString());

            assignRecordPopup.SelectResponsibleUserDecision("Do not change").TapOkButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ValidateRecordCellText(personCaseNote1.ToString(), 2, "Case Note 01")
                .ValidateRecordCellText(personCaseNote1.ToString(), 5, "Advanced");

        }

        [TestProperty("JiraIssueID", "CDV6-11936")]
        [Description("Export to Excel")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod17()
        {
            var personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", this.NotesData1, _personID, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .SelectPersonCaseNoteRecord(personCaseNote1.ToString())
                .ClickExportToExcelButton();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Selected Records")
                .SelectExportFormat("Csv (comma separated with quotes)")
                .ClickExportButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "PersonCaseNotes.csv");
            Assert.IsTrue(fileExists);

        }

        [TestProperty("JiraIssueID", "CDV6-11938")]
        [Description("Validate Audit")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod18()
        {
            var personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", this.NotesData1, _personID, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(personCaseNote1.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 01")
                .ClickCancelButton()
                .NavigateToAuditSubPage();



            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = personCaseNote1.ToString(),
                ParentTypeName = "casenote",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual(_systemUserFullName, auditResponseData.GridData[0].cols[1].Text);

        }

        [TestProperty("JiraIssueID", "CDV6-11939")]
        [Description("Validate user able to delete the case note created.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonCaseNotes_UITestMethod19()
        {
            var personCaseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", this.NotesData1, _personID, DateTime.Now);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .OpenPersonCaseNoteRecord(personCaseNote1.ToString());

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("Case Note 01")
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ValidateRecordNotVisible(personCaseNote1.ToString());

            var caseNote = dbHelper.personCaseNote.GetPersonCaseNoteByPersonID(_personID);
            Assert.AreEqual(0, caseNote.Count);

        }

        #endregion

        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}
