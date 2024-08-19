using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class HospitalWardsRecordPage : CommonMethods
    {

        public HospitalWardsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=inpatientward')]");

        readonly By HospitalWardsRecordPageHeader = By.XPath("//h1[@title='Hospital Ward: New']");
        readonly By HospitalWardsRecordPageHeaders = By.XPath("//h1");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");            
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");

        #region Navigation Area
        
        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/a[@title='Menu']");

        #endregion

        #region Left Sub Menu 
        
        readonly By relatedItemsLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_RelatedItems']/a");
        readonly By BayRoomSubMenu = By.Id("CWNavItem_BayRoom");
        
        #endregion

        #region Fields

        readonly By name_Field = By.XPath("//input[@name='CWField_title']");
        readonly By hospital_LookupButton = By.Id("CWLookupBtn_providerid");
        readonly By wardsManager_LookupButton = By.Id("CWLookupBtn_wardmanagerid");
        readonly By wardSpecialty_LookupButton = By.Id("CWLookupBtn_wardspecialtyid");
        readonly By responsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By EndDate_Field = By.Id("CWField_enddate");

        #endregion


        public HospitalWardsRecordPage WaitForHospitalWardsRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(HospitalWardsRecordPageHeader);

            ValidateElementText(HospitalWardsRecordPageHeader, "Hospital Ward:\r\nNew");

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            return this;
        }

        public HospitalWardsRecordPage WaitForHospitalWardsRecordPageToLoad(string TitleName)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(HospitalWardsRecordPageHeaders);

            ValidateElementText(HospitalWardsRecordPageHeaders, "Hospital Ward:\r\n" + TitleName);

            WaitForElement(MenuButton);
            

            return this;
        }
        
        public HospitalWardsRecordPage NavigateToBayRoomPage()
           {
               Click(MenuButton);

               WaitForElementToBeClickable(relatedItemsLeftSubMenu);
               Click(relatedItemsLeftSubMenu);

               WaitForElementToBeClickable(BayRoomSubMenu);
               Click(BayRoomSubMenu);

               return this;
           }

        public HospitalWardsRecordPage InsertName(string NameToInsert)
        {
            SendKeys(name_Field, NameToInsert);

            return this;
        }

        public HospitalWardsRecordPage InsertStartDate(string DateToInsert)
        {
            SendKeys(startDate_Field, DateToInsert);

            return this;
        }

        public HospitalWardsRecordPage InsertEndDate(string DateToInsert)
        {
            SendKeys(EndDate_Field, DateToInsert);

            return this;
        }

        public HospitalWardsRecordPage ClickHospitalLookupButton()
        {
            WaitForElementToBeClickable(hospital_LookupButton);
            Click(hospital_LookupButton);

            return this;
        }
        
        public HospitalWardsRecordPage ClickWardsMangerLookupButton()
        {
            WaitForElementToBeClickable(wardsManager_LookupButton);
            Click(wardsManager_LookupButton);

            return this;
        }

        public HospitalWardsRecordPage ClickWardSpecialtyLookupButton()
        {
            WaitForElementToBeClickable(wardSpecialty_LookupButton);
            Click(wardSpecialty_LookupButton);

            return this;
        }
        
        public HospitalWardsRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton); ;
            Click(saveButton);

            return this;
        }
        
        public HospitalWardsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
       
        public HospitalWardsRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

    }
}
