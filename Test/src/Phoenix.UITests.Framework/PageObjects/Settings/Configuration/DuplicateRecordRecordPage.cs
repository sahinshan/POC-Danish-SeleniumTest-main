using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DuplicateRecordRecordPage : CommonMethods
    {

        public DuplicateRecordRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=duplicaterecord&')]");
        readonly By CWIFrame_SubordinateDuplicates = By.Id("CWIFrame_SubordinateDuplicates");

        

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backbutton = By.XPath("//button[@title='Back']");

        readonly By recordLinkField = By.Id("CWField_recordid_Link");
        readonly By duplicateDetectionRuleLinkField = By.Id("CWField_duplicatedetectionruleid_Link");
        readonly By numberOfDuplicatesField = By.Id("CWField_numberofduplicates");


        #region Subordinate Duplicates Area

        readonly By SubordinateDuplicates_MergeDuplicateButton = By.XPath("//*[@id='TI_MergeDuplicate']");

        By SubordinateDuplicates_RecordCheckBox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");
        By SubordinateDuplicates_RecordIDCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");


        #endregion


        public DuplicateRecordRecordPage WaitForDuplicateRecordRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(pageHeader);

            WaitForElement(recordLinkField);
            WaitForElement(duplicateDetectionRuleLinkField);
            WaitForElement(numberOfDuplicatesField);

            return this;
        }

        public DuplicateRecordRecordPage WaitForSubordinateDuplicatesAreaToLoad()
        {
            WaitForElement(CWIFrame_SubordinateDuplicates);
            SwitchToIframe(CWIFrame_SubordinateDuplicates);

            return this;
        }


        public DuplicateRecordRecordPage ClickBackButton()
        {
            this.Click(backbutton);


            return this;
        }



        public DuplicateRecordRecordPage ClickSubordinateDuplicates_RecordCheckBox(string recordID)
        {
            this.Click(SubordinateDuplicates_RecordCheckBox(recordID));


            return this;
        }

        public DuplicateRecordRecordPage ClickSubordinateDuplicates_MergeDuplicateButton()
        {
            this.Click(SubordinateDuplicates_MergeDuplicateButton);


            return this;
        }

    }
}
