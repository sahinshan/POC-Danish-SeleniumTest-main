using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PublicHolidayRecordPage : CommonMethods
    {
        public PublicHolidayRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CWDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=bankholiday&')]"); 


        
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        
        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");

        


        #region Navigation Area

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");


        #endregion

        #region FieldNames

        readonly By name_FieldName = By.XPath("//*[@id='CWLabelHolder_name']/label");
        readonly By description_FieldName = By.XPath("//*[@id='CWLabelHolder_description']/label");
        readonly By date_FieldName = By.XPath("//*[@id='CWLabelHolder_holidaydate']/label");

        #endregion

        #region Fields

        readonly By name_Field = By.Id("CWField_name");
        readonly By description_Field = By.Id("CWField_description");
        readonly By date_Field = By.Id("CWField_holidaydate");
        readonly By name_ErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");
        #endregion

        public PublicHolidayRecordPage WaitForPublicHolidayRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWDialogIFrame);
            SwitchToIframe(CWDialogIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(name_FieldName);
            WaitForElement(description_FieldName);
            WaitForElement(date_FieldName);
            WaitForElement(name_Field);
            WaitForElement(description_Field);
            WaitForElement(date_Field);

            return this;
        }




        public PublicHolidayRecordPage InsertName(string ValueToInsert)
        {
            WaitForElementToBeClickable(name_Field);
            MoveToElementInPage(name_Field);
            Click(name_Field);
            SendKeys(name_Field, ValueToInsert);

            return this;
        }
        
        public PublicHolidayRecordPage InsertDescription(string ValueToInsert)
        {
            WaitForElementToBeClickable(description_Field);
            MoveToElementInPage(description_Field);
            Click(description_Field);
            SendKeys(description_Field, ValueToInsert);

            return this;
        }

        public PublicHolidayRecordPage InsertDate(string ValueToInsert)
        {
            WaitForElementToBeClickable(date_Field);
            MoveToElementInPage(date_Field);
            Click(date_Field);
            SendKeys(date_Field, ValueToInsert);

            return this;
        }

        public PublicHolidayRecordPage ValidateNotificationMessageAreaVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(notificationMessageArea);
            }
            else
            {
                WaitForElementNotVisible(notificationMessageArea, 7);
            }

            return this;
        }

        public PublicHolidayRecordPage ValidateDescriptionFieldVisibility(bool ExpectedVisible)
        {
            if(ExpectedVisible)
            {
                WaitForElementVisible(description_FieldName);                
            }
            else
            {
                WaitForElementNotVisible(description_FieldName, 7);
            }

            return this;
        }

        public PublicHolidayRecordPage ValidateDateFieldVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(date_Field);
            }
            else
            {
                WaitForElementNotVisible(date_Field, 7);
            }

            return this;
        }

        public PublicHolidayRecordPage ValidateNameFieldVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(name_Field);
            }
            else
            {
                WaitForElementNotVisible(name_Field, 7);
            }

            return this;
        }

        public PublicHolidayRecordPage ValidateNameErrorLabelVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(name_ErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(name_ErrorLabel, 7);
            }

            return this;
        }



        public PublicHolidayRecordPage ValidateNotificationMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessageArea, ExpectedText);

            return this;
        }
        public PublicHolidayRecordPage ValidateName_ErrorLabelLabelText(string ExpectedText)
        {
            ValidateElementText(name_ErrorLabel, ExpectedText);

            return this;
        }

        public PublicHolidayRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            return this;
        }

        public PublicHolidayRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public PublicHolidayRecordPage TapDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            MoveToElementInPage(deleteButton);
            Click(deleteButton);

            return this;
        }
    }
}
