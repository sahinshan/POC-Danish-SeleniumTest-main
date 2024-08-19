using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonChronologiesPage : CommonMethods
    {
        public PersonChronologiesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); 
        readonly By CWNavItem_PersonChronologyFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Saved Chronologies']");
        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.XPath("//button[@title='Create New Chronology']");
        readonly By IncludeEvent = By.Id("CWIncludedEvents");
        readonly By DeleteRecordButton = By.XPath("//*[@id='CWToolbarButtons']/button[3]");

        
        public PersonChronologiesPage WaitForPersonChronologiesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonChronologyFrame);
            SwitchToIframe(CWNavItem_PersonChronologyFrame);

            return this;
        }

        public PersonChronologiesPage WaitForPersonChronologyDetailsPageToLoda()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonChronologyFrame);
            SwitchToIframe(CWNavItem_PersonChronologyFrame);

            WaitForElement(IncludeEvent);
            SwitchToIframe(IncludeEvent);

            return this;
        }

        public PersonChronologiesPage OpenPersonChronologyRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public PersonChronologiesPage SelectPersonChronologyRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonChronologiesPage SelectNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public PersonChronologiesPage SelectDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public PersonChronologiesPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordLabel);

            }
            else
            {
                WaitForElementNotVisible(NoRecordLabel, 5);
            }
            return this;
        }

    }
}
