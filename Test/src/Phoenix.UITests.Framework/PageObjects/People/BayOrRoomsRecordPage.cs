using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class BayOrRoomsRecordpage : CommonMethods
    {
        public BayOrRoomsRecordpage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=inpatientbay&')]");

        #region Navigation Area
        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/a[@title='Menu']");

        #endregion

        #region Left Sub Menu        
        readonly By relatedItemsLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_RelatedItems']/a");
        readonly By BedSubMenu = By.Id("CWNavItem_InpatientBed");
        #endregion


        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Bay/Room: New']");

        
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By Name_Field = By.Id("CWField_name");
        readonly By EntryPoint_Field = By.Id("CWField_entrypointid");
        readonly By description_Field = By.Id("CWField_description");
        readonly By roomTypeId_Field = By.Id("CWField_roomtypeid");
        readonly By Row_Field = By.Id("CWField_row");
        readonly By Order_Field = By.Id("CWField_order");
        readonly By maxbedsinleftrow_Field = By.Id("CWField_maxbedsinleftrow");
        readonly By maxbedsinrightrow_Field = By.Id("CWField_maxbedsinrightrow");
        readonly By applicablegendertypeid_Field = By.Id("CWField_applicablegendertypeid");
        readonly By inpatientwardid_LookupButton = By.Id("CWLookupBtn_inpatientwardid");      



        readonly By ClickHereToHide = By.XPath("//*[@id='CWNotificationHolder_PersonChronology']/a");
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
            
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        




        public BayOrRoomsRecordpage WaitForBayOrRoomsRecordPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

           WaitForElement(personRecordIFrame);
           SwitchToIframe(personRecordIFrame);


            return this;
        }

        public BayOrRoomsRecordpage WaitForBayRecordPageToLoad(string TitleName)
        {
            SwitchToDefaultFrame();


            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);



            WaitForElement(MenuButton);


            return this;
        }

        public BayOrRoomsRecordpage NavigateToBedPage()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(relatedItemsLeftSubMenu);
            Click(relatedItemsLeftSubMenu);

            WaitForElementToBeClickable(BedSubMenu);
            Click(BedSubMenu);

            return this;
        }

        public BayOrRoomsRecordpage NavigateToRelatedItemsPage()
        {
            Click(MenuButton);

            WaitForElementToBeClickable(relatedItemsLeftSubMenu);
            Click(relatedItemsLeftSubMenu);

          

            return this;
        }

        public BayOrRoomsRecordpage ValidateBed_IconVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(BedSubMenu);
                
            }
            else
            {
                WaitForElementNotVisible(BedSubMenu, 5);
            }
            return this;
        }



        public BayOrRoomsRecordpage OpenBayOrRoomsRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public BayOrRoomsRecordpage SelectBayOrRoomsRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public BayOrRoomsRecordpage ClickWardLookupButton()
        {
            Click(inpatientwardid_LookupButton);

            return this;
        }


        public BayOrRoomsRecordpage ClickSaveButton()
        {
            WaitForElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public BayOrRoomsRecordpage ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }
       



       
        public BayOrRoomsRecordpage Insertdescription(string descriptionToInsert)
        {
            SendKeys(description_Field, descriptionToInsert);

            return this;
        }

       

        public BayOrRoomsRecordpage InsertName(string NameToInsert)
        {
            SendKeys(Name_Field, NameToInsert);

            return this;
        }

        public BayOrRoomsRecordpage InsertRowNo(string RowNoToInsert)
        {
            SendKeys(Row_Field, RowNoToInsert);

            return this;
        }

        public BayOrRoomsRecordpage InsertOrderNo(string OrderNoToInsert)
        {
            SendKeys(Order_Field, OrderNoToInsert);

            return this;
        }

        public BayOrRoomsRecordpage Insertmaxbedsinleftrow(string maxbedsinleftrowToInsert)
        {
            SendKeys(maxbedsinleftrow_Field, maxbedsinleftrowToInsert);

            return this;
        }

        public BayOrRoomsRecordpage Insertmaxbedsinrighttrow(string maxbedsinrightrowToInsert)
        {
            SendKeys(maxbedsinrightrow_Field, maxbedsinrightrowToInsert);

            return this;
        }
        public BayOrRoomsRecordpage SelectGenderType(String OptionToSelect)
        {
            WaitForElementVisible(applicablegendertypeid_Field);
            SelectPicklistElementByText(applicablegendertypeid_Field, OptionToSelect);

            return this;
        }
        public BayOrRoomsRecordpage SelectEntryPoint(String OptionToSelect)
        {
            WaitForElementVisible(EntryPoint_Field);
            SelectPicklistElementByText(EntryPoint_Field, OptionToSelect);

            return this;
        }

        public BayOrRoomsRecordpage SelectRoomTypeByText(String OptionToSelect)
        {
            WaitForElementVisible(roomTypeId_Field);
            SelectPicklistElementByText(roomTypeId_Field, OptionToSelect);

            return this;
        }





    }
}
