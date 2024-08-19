using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using Phoenix.UITests.Framework;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using System.Configuration;
using Phoenix.UITests.Framework.PageObjects;

namespace Phoenix.UITests.People.Related_Items
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    /// 
    [TestClass]
    public class Person_AllegedAbuser_UITestCases : FunctionalTest
    {
        #region https://advancedcsg.atlassian.net/browse/CDV6-12505

        [TestProperty("JiraIssueID", "CDV6-12727")]
        [Description("Pre-Requisite -Person should have atleast 1 case and Adult Safeguarding Record."+"Open existing person Allegation Abuser record"+
            "Navigate to Allegation Investigators and Enter the Date started greater than the Allegation Date- Validate the pop up message")]
        [TestMethod]
        public void Person_AllegedAbuser_UITestMethod01()

        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("782d59e4-1315-ec11-a32d-f90a4322a942"); //Marie John
            var personNumber = "508246";
            Guid allegedVictimid = new Guid("4c5eff49-58e4-eb11-a325-005056926fe4");//John In
            var allegationDate = DateTime.Now.AddDays(-5);
            Guid allegationCategoryId = new Guid("743c8513-fb59-e911-a2c5-005056926fe4");//Physical Abuse
            Guid adultSafeGuardingId = new Guid("d8839395-5f15-ec11-a32d-f90a4322a942");//Emotional- John In
            var dateStarted = DateTime.Now.AddDays(-10);



            //remove Alleged Victim related to the person
            foreach (var recordid in dbHelper.allegation.GetAllegationIdByVictim(personID))
            {
                foreach (var reviewRecordId in dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(recordid))
                {
                    dbHelper.allegationInvestigator.DeleteAllegationInvestigator(reviewRecordId);
                }
                dbHelper.allegation.DeleteAllegation(recordid);
            }

            //remove Alleged Abuser related to the person
            foreach (var recordid in dbHelper.allegation.GetAllegationIdByAbuser(personID))
            {
                foreach (var reviewRecordId in dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(recordid))
                {
                    dbHelper.allegationInvestigator.DeleteAllegationInvestigator(reviewRecordId);
                }
                dbHelper.allegation.DeleteAllegation(recordid);
            }



            Guid AllegationAbsuerRecord = dbHelper.allegation.CreateAllegation(adultSafeGuardingId, teamid, allegedVictimid, "", "John In", personID, "", "Marie John", personID, allegationCategoryId, allegationDate);
            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAllegedAbuserPage();

            allegedAbuserPage
                .WaitForAllegedAbuserPageToLoad()
                .OpenAllegationAbuserRecord(AllegationAbsuerRecord.ToString());

            allegedAbsuserRecordPage
                .WaitForAllegedAbsuserRecordPageToLoad("Allegation for Marie John created by Security Test User Admin on ")
                .NavigateAllegationInvestigatorsSubpage();

            allegationInvestigatorPage
                .WaitForAllegationInvestigatorPageToLoad()
                .ClickNewRecordButton();

            allegationInvestigatorRecordPage
                .WaitForAllegationInvestigatorRecordPageToLoad("New")
                .InsertDateStarted(dateStarted.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Date Started must be after Allegation Date on the linked Allegation");
                
              


        }
        [TestProperty("JiraIssueID", "CDV6-12728")]
        [Description("Pre-Requisite -Person should have atleast 1 case and Adult Safeguarding Record." + "Open existing person Allegation Abuser record" +
            "Navigate to Allegation Investigators and Enter the Date ended greater than the Date started- Validate the pop up message")]
        [TestMethod]
        public void Person_AllegedAbuser_UITestMethod02()

        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("782d59e4-1315-ec11-a32d-f90a4322a942"); //Marie John
            var personNumber = "508246";
            Guid allegedVictimid = new Guid("4c5eff49-58e4-eb11-a325-005056926fe4");//John In
            var allegationDate = DateTime.Now.AddDays(-15);
            Guid allegationCategoryId = new Guid("743c8513-fb59-e911-a2c5-005056926fe4");//Physical Abuse
            Guid adultSafeGuardingId = new Guid("d8839395-5f15-ec11-a32d-f90a4322a942");//Emotional- John In
            var dateStarted = DateTime.Now.AddDays(-5);
            var dateEnded = DateTime.Now.AddDays(-10);



            //remove Alleged Victim related to the person
            foreach (var recordid in dbHelper.allegation.GetAllegationIdByVictim(personID))
            {
                foreach (var reviewRecordId in dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(recordid))
                {
                    dbHelper.allegationInvestigator.DeleteAllegationInvestigator(reviewRecordId);
                }
                dbHelper.allegation.DeleteAllegation(recordid);
            }

            //remove Alleged Abuser related to the person
            foreach (var recordid in dbHelper.allegation.GetAllegationIdByAbuser(personID))
            {
                foreach (var reviewRecordId in dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(recordid))
                {
                    dbHelper.allegationInvestigator.DeleteAllegationInvestigator(reviewRecordId);
                }
                dbHelper.allegation.DeleteAllegation(recordid);
            }



            Guid AllegationAbsuerRecord = dbHelper.allegation.CreateAllegation(adultSafeGuardingId, teamid, allegedVictimid, "", "John In", personID, "", "Marie John", personID, allegationCategoryId, allegationDate);
            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAllegedAbuserPage();

            allegedAbuserPage
                .WaitForAllegedAbuserPageToLoad()
                .OpenAllegationAbuserRecord(AllegationAbsuerRecord.ToString());

            allegedAbsuserRecordPage
                .WaitForAllegedAbsuserRecordPageToLoad("Allegation for Marie John created by Security Test User Admin on ")
                .NavigateAllegationInvestigatorsSubpage();

            allegationInvestigatorPage
                .WaitForAllegationInvestigatorPageToLoad()
                .ClickNewRecordButton();

            allegationInvestigatorRecordPage
                .WaitForAllegationInvestigatorRecordPageToLoad("New")
                .InsertDateStarted(dateStarted.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertDateEnded(dateEnded.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Date Ended must be after Date Started");




        }


        [TestProperty("JiraIssueID", "CDV6-12729")]
        [Description("Pre-Requisite -Person should have atleast 1 case and Adult Safeguarding Record." + "Open existing person Allegation Abuser record" +
            "Navigate to Allegation Investigators and Enter all the fields and save the record.")]
        [TestMethod]
        public void Person_AllegedAbuser_UITestMethod03()

        {
            var teamid = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("782d59e4-1315-ec11-a32d-f90a4322a942"); //Marie John
            var personNumber = "508246";
            Guid allegedVictimid = new Guid("4c5eff49-58e4-eb11-a325-005056926fe4");//John In
            var allegationDate = DateTime.Now.AddDays(-15);
            Guid allegationCategoryId = new Guid("743c8513-fb59-e911-a2c5-005056926fe4");//Physical Abuse
            Guid adultSafeGuardingId = new Guid("d8839395-5f15-ec11-a32d-f90a4322a942");//Emotional- John In
            var dateStarted = DateTime.Now.AddDays(-5);
            var dateEnded = DateTime.Now.Date;
            Guid userId = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4");



            //remove Alleged Victim related to the person
            foreach (var recordid in dbHelper.allegation.GetAllegationIdByVictim(personID))
            {
                foreach (var reviewRecordId in dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(recordid))
                {
                    dbHelper.allegationInvestigator.DeleteAllegationInvestigator(reviewRecordId);
                }
                dbHelper.allegation.DeleteAllegation(recordid);
            }

            //remove Alleged Abuser related to the person
            foreach (var recordid in dbHelper.allegation.GetAllegationIdByAbuser(personID))
            {
                foreach (var reviewRecordId in dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(recordid))
                {
                    dbHelper.allegationInvestigator.DeleteAllegationInvestigator(reviewRecordId);
                }
                dbHelper.allegation.DeleteAllegation(recordid);
            }



            Guid AllegationAbsuerRecord = dbHelper.allegation.CreateAllegation(adultSafeGuardingId, teamid, allegedVictimid, "", "John In", personID, "", "Marie John", personID, allegationCategoryId, allegationDate);
            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecord(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAllegedAbuserPage();

            allegedAbuserPage
                .WaitForAllegedAbuserPageToLoad()
                .OpenAllegationAbuserRecord(AllegationAbsuerRecord.ToString());

            allegedAbsuserRecordPage
                .WaitForAllegedAbsuserRecordPageToLoad("Allegation for Marie John created by Security Test User Admin on ")
                .NavigateAllegationInvestigatorsSubpage();

            allegationInvestigatorPage
                .WaitForAllegationInvestigatorPageToLoad()
                .ClickNewRecordButton();

            allegationInvestigatorRecordPage
                .WaitForAllegationInvestigatorRecordPageToLoad("New")
                .InsertDateStarted(dateStarted.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertDateEnded(dateEnded.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectInvestigator("2")
                .ClickSaveAndCloseButton();


            allegationInvestigatorPage
                .WaitForAllegationInvestigatorPageToLoad();

            var investigators = dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(AllegationAbsuerRecord);
            Assert.AreEqual(1, investigators.Count);
            var allegationInvestigators = investigators.FirstOrDefault();


            var allegationInvestigatorsFields = dbHelper.allegationInvestigator.GetAllegationInvestigatorByID(allegationInvestigators, "startdate","enddate", "investigatorid","ownerid","responsibleuserid");

            Assert.AreEqual(dateStarted.Date, allegationInvestigatorsFields["startdate"]);
            Assert.AreEqual(dateEnded.Date, allegationInvestigatorsFields["enddate"]);
            Assert.AreEqual(dateEnded.Date, allegationInvestigatorsFields["enddate"]);
            Assert.AreEqual(2, allegationInvestigatorsFields["investigatorid"]);
            Assert.AreEqual(teamid, allegationInvestigatorsFields["ownerid"]);
            Assert.AreEqual(userId, allegationInvestigatorsFields["responsibleuserid"]);

        }
        #endregion
    }
}
