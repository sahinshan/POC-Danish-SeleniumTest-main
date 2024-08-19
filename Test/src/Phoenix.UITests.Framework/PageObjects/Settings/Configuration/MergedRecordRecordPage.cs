using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class MergedRecordRecordPage : CommonMethods
    {

        public MergedRecordRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By incomesupportsetupIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=mergedrecord&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        
        readonly By backbutton = By.XPath("//button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By UnmergeButton = By.Id("TI_Unmerge");


        readonly By TitleField = By.Id("CWField_title");
        readonly By MasterRecord_LookupButton = By.Id("CWLookupBtn_masterrecordid");
        readonly By SubordinateRecord_LookupButton = By.Id("CWLookupBtn_subordinaterecordid");
        readonly By RecordType_LookupButton = By.Id("CWLookupBtn_businessobjectid");


        public MergedRecordRecordPage WaitForMergedRecordRecordPageToLoad(string MergedRecordName)
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(incomesupportsetupIFrame);
            this.SwitchToIframe(incomesupportsetupIFrame);

            this.WaitForElement(pageHeader);

            this.WaitForElement(TitleField);
            this.WaitForElement(MasterRecord_LookupButton);
            this.WaitForElement(SubordinateRecord_LookupButton);
            this.WaitForElement(RecordType_LookupButton);

            if (driver.FindElement(pageHeader).Text != "Merged Record:\r\n" + MergedRecordName)
                throw new Exception("Page title do not equals: Merged Record: " + MergedRecordName);

            return this;
        }

        public MergedRecordRecordPage WaitForUnmergedRecordRecordPageToLoad(string MergedRecordName)
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(incomesupportsetupIFrame);
            this.SwitchToIframe(incomesupportsetupIFrame);

            this.WaitForElement(pageHeader);

            this.WaitForElement(TitleField);

            return this;
        }



        public MergedRecordRecordPage InsertTitle(string Title)
        {
            this.SendKeys(TitleField, Title);

            return this;
        }

        public MergedRecordRecordPage ClickMasterRecordLookupButton()
        {
            this.Click(MasterRecord_LookupButton);

            return this;
        }

        public MergedRecordRecordPage ClickSubordinateRecordLookupButton()
        {
            this.Click(SubordinateRecord_LookupButton);

            return this;
        }

        public MergedRecordRecordPage ClickRecordTypeLookupButton()
        {
            this.Click(RecordType_LookupButton);

            return this;
        }





        public MergedRecordRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public MergedRecordRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public MergedRecordRecordPage ClickUnmergeButton()
        {
            this.Click(UnmergeButton);

            return this;
        }

        public MergedRecordRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public MergedRecordRecordPage ClickBackButton()
        {
            System.Threading.Thread.Sleep(2000);
            this.WaitForElement(backbutton);
            this.Click(backbutton);

            return this;
        }

    }
}
