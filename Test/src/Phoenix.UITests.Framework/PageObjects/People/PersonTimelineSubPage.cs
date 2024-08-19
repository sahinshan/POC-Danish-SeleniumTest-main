
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonTimelineSubPage : CommonMethods
    {
        public PersonTimelineSubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWTimelinePanel_IFrame = By.Id("CWUrlPanel_IFrame");


        #region Search Area

        readonly By LeftSidePanel = By.XPath("//*[@id='CWFilterPanel']");

        readonly By filterBy_FieldLabel = By.XPath("//label[@for='CWBOList']");
        readonly By From_FieldLabel = By.XPath("//label[@for='CWFromDate']");
        readonly By To_FieldLabel = By.XPath("//label[@for='CWToDate']");
        readonly By Team_FieldLabel = By.XPath("//label[@for='CWTeamId']");
        readonly By ProfessionType_FieldLabel = By.XPath("//label[@for='CWProfessionTypeId']");

        readonly By filterBy_Picklist = By.Id("CWBOList");
        readonly By From_Field = By.Id("CWFromDate");
        readonly By To_Field = By.Id("CWToDate");
        readonly By Team_LookupButton = By.Id("CWLookupBtn_CWTeamId");
        readonly By ProfessionaType_LookupButton = By.Id("CWLookupBtn_CWProfessionTypeId");

        readonly By Reset_Button = By.Id("CWReset");
        readonly By Refresh_Button = By.Id("CWRefresh");
        readonly By Apply_Button = By.Id("CWFilter");

        readonly By splitterbutton = By.Id("CWSplitter_Link");

        #endregion

        #region Results area

        readonly By RightSidePanel = By.XPath("//*[@id='CWResultsPanel']");

        By cardTitleLink(string recordID) => By.XPath("//div[@class='card-title row']/div/a[contains(@onclick,'" + recordID + "')]");
        By cardText(string Part1Text, string Part2Text) => By.XPath("//div[@class='card-text mb-2 row']/div/span[text()='" + Part1Text + "']/strong[text()='" + Part2Text + "']");


        By restrictedRecordCard(string position) => By.XPath("//*[@id='CWPastTimelinePanel']/li[@class='w-100 cardwrap'][" + position + "]/div/div/div/div/div");

        By personAlertRecord = By.XPath("//*[@id='CWPastTimelinePanel']/li[2]/div[2]/div/div[1]/div[1]/a/h3[text() = 'Person Alert And Hazard Created']");
        By personAlertReviewRecord = By.XPath("//*[@id='CWPastTimelinePanel']/li[2]/div[1]/div[1]");

        #endregion






        public PersonTimelineSubPage WaitForPersonTimelineSubPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWTimelinePanel_IFrame);
            SwitchToIframe(CWTimelinePanel_IFrame);

            WaitForElement(filterBy_FieldLabel);
            WaitForElement(From_FieldLabel);
            WaitForElement(To_FieldLabel);
            WaitForElement(Team_FieldLabel);
            WaitForElement(ProfessionType_FieldLabel);

            WaitForElement(filterBy_Picklist);
            WaitForElement(From_Field);
            WaitForElement(To_Field);
            WaitForElement(Team_LookupButton);
            WaitForElement(ProfessionaType_LookupButton);
            
            WaitForElement(Reset_Button);
            WaitForElement(Refresh_Button);
            WaitForElement(Apply_Button);

            WaitForElement(splitterbutton);


            return this;
        }

        public PersonTimelineSubPage ValidateSearchAreaVisibleInLeftSide()
        {
            WaitForElement(LeftSidePanel);

            return this;
        }

        public PersonTimelineSubPage ValidateSearchAreaVisibleInRightSide()
        {
            WaitForElement(RightSidePanel);

            return this;
        }

        public PersonTimelineSubPage ClickTeamLookupButton()
        {
            Click(Team_LookupButton);

            return this;
        }

        public PersonTimelineSubPage ClickProfessionTypeLookupButton()
        {
            Click(ProfessionaType_LookupButton);

            return this;
        }

        public PersonTimelineSubPage ClickResetButton()
        {
            Click(Reset_Button);

            return this;
        }

        public PersonTimelineSubPage ClickRefreshButton()
        {
            Click(Refresh_Button);

            return this;
        }

        public PersonTimelineSubPage ClickApplyButton()
        {
            Click(Apply_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonTimelineSubPage SelectFilterBy(string TextToSelect)
        {
            SelectPicklistElementByText(filterBy_Picklist, TextToSelect);

            return this;
        }

        public PersonTimelineSubPage ValidateRecordPresent(string RecordID)
        {
            WaitForElement(cardTitleLink(RecordID));

            return this;
        }

        public PersonTimelineSubPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(cardTitleLink(RecordID), 7);

            return this;
        }

        public PersonTimelineSubPage ValidateRestrictedRecordCardVisibility(string position, bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(restrictedRecordCard(position));
            else
                WaitForElementNotVisible(restrictedRecordCard(position), 3);


            return this;
        }

        public PersonTimelineSubPage ValidateRestrictedRecordCardVisibility(string position, string ExpectedText)
        {
            ValidateElementText(restrictedRecordCard(position), ExpectedText);
            
            return this;
        }

        public PersonTimelineSubPage ValidatePersonAlertRecord(bool ExpectedText)
        {

            if (ExpectedText)
            {
                WaitForElementVisible(personAlertRecord);
            }
            else
            {
                WaitForElementNotVisible(personAlertRecord, 5);
            }
            return this;
        }

        public PersonTimelineSubPage ValidatePersonAlertReviewRecord(bool ExpectedText)
        {

            if (ExpectedText)
            {
                WaitForElementVisible(personAlertReviewRecord);
            }
            else
            {
                WaitForElementNotVisible(personAlertReviewRecord, 5);
            }
            return this;
        }



    }
}
