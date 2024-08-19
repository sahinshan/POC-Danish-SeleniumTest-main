using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class BedRecordPage : CommonMethods
    {
        public BedRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=inpatientbed&')]");

        #region Navigation Area
        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/a[@title='Menu']");

        #endregion

        #region Left Sub Menu        
        readonly By relatedItemsLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_RelatedItems']/a");
        readonly By BedSubMenu = By.Id("CWNavItem_InpatientBed");
        #endregion


        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Bed: New']");

        readonly By Bednumber_Field = By.Id("CWField_bednumber");
        readonly By inpatientbayid_LookupButton = By.Id("CWLookupBtn_inpatientbayid");
        readonly By Serialnumber_Field = By.Id("CWField_serialnumber");
        readonly By Statusid_Field = By.Id("CWField_statusid");
        readonly By inpatientbedtypeid_LookupButton = By.Id("CWLookupBtn_inpatientbedtypeid");
        readonly By description_Field = By.Id("CWField_description");
        readonly By rowpositionid = By.Id("CWField_rowpositionid");
        readonly By position_Field = By.Id("CWField_position");
        readonly By commissioneddate_Field = By.Id("CWField_commissioneddate");
        readonly By decommissioneddate_Field = By.Id("CWField_decommissioneddate");



        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        


        readonly By ClickHereToHide = By.XPath("//*[@id='CWNotificationHolder_PersonChronology']/a");
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
            
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        




        public BedRecordPage WaitForBedRecordPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

           WaitForElement(personRecordIFrame);
           SwitchToIframe(personRecordIFrame);


            return this;
        }

        public BedRecordPage WaitForBayRecordPageToLoad(string TitleName)
        {
            SwitchToDefaultFrame();


            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);



            WaitForElement(MenuButton);


            return this;
        }

        public BedRecordPage NavigateToBedPage()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(relatedItemsLeftSubMenu);
            Click(relatedItemsLeftSubMenu);

            WaitForElementToBeClickable(BedSubMenu);
            Click(BedSubMenu);

            return this;
        }



        public BedRecordPage OpenBayOrRoomsRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public BedRecordPage SelectBayOrRoomsRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public BedRecordPage ClickBayLookupButton()
        {
            Click(inpatientbayid_LookupButton);

            return this;
        }

        public BedRecordPage ClickBedTypeLookupButton()
        {
            Click(inpatientbedtypeid_LookupButton);

            return this;
        }


        public BedRecordPage ClickSaveButton()
        {
            WaitForElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public BedRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);
            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }
       



       
        public BedRecordPage Insertdescription(string descriptionToInsert)
        {
            SendKeys(description_Field, descriptionToInsert);

            return this;
        }

       

        public BedRecordPage InsertBedNo(string BedNoToInsert)
        {
            SendKeys(Bednumber_Field, BedNoToInsert);

            return this;
        }

        public BedRecordPage InsertSerialNo(string SerialNoToInsert)
        {
            SendKeys(Serialnumber_Field, SerialNoToInsert);

            return this;
        }

        public BedRecordPage InsertPositionNo(string OrderNoToInsert)
        {
            SendKeys(position_Field, OrderNoToInsert);

            return this;
        }

        public BedRecordPage InsertCommissionDate(string maxbedsinleftrowToInsert)
        {
            SendKeys(commissioneddate_Field, maxbedsinleftrowToInsert);

            return this;
        }

        public BedRecordPage InsertDeCommissionDate(string maxbedsinrightrowToInsert)
        {
            SendKeys(decommissioneddate_Field, maxbedsinrightrowToInsert);

            return this;
        }
        public BedRecordPage SelectStatusType(String OptionToSelect)
        {
            WaitForElementVisible(Statusid_Field);
            SelectPicklistElementByText(Statusid_Field, OptionToSelect);

            return this;
        }
        public BedRecordPage SelectRowPositionByText(String OptionToSelect)
        {
            WaitForElementVisible(rowpositionid);
            SelectPicklistElementByText(rowpositionid, OptionToSelect);

            return this;
        }

      





    }
}
