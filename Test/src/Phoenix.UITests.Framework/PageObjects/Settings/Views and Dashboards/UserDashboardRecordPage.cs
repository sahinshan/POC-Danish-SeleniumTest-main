    
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class UserDashboardRecordPage : CommonMethods
    {
        public UserDashboardRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By userDashboardRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=userdashboard&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'


        
        readonly By saveButton = By.Id("TI_SaveButton");

        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");

        //


        #region Navigation Area

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");

        readonly By DetailsSection = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");

        #endregion

        readonly By AutoRefresh_label = By.XPath("//*[@id='CWLabelHolder_autorefresh']/label");
        readonly By AutoRefreshYes_RadioButton = By.Id("CWField_autorefresh_1");
        readonly By AutoRefreshNo_RadioButton = By.Id("CWField_autorefresh_0");
        
        readonly By refreshTime_label = By.XPath("//*[@id='CWLabelHolder_refreshtime']/label");
        readonly By refreshTime_Field = By.Id("CWField_refreshtime");
        readonly By refreshTime_NotificationArea = By.XPath("//*[@id='CWControlHolder_refreshtime']/label/span");




        public UserDashboardRecordPage WaitForUserDashboardRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(userDashboardRecordIFrame);
            SwitchToIframe(userDashboardRecordIFrame);

            WaitForElement(MenuButton);
            WaitForElement(DetailsSection);

            return this;
        }


        public UserDashboardRecordPage ValidateNotificationMessageArea(string ExpectedMessage)
        {
            ValidateElementText(notificationMessageArea, ExpectedMessage);

            return this;
        }

        public UserDashboardRecordPage ValidateRefreshTimeErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(refreshTime_NotificationArea, ExpectedMessage);

            return this;
        }

        public UserDashboardRecordPage ValidateAutoRefreshFieldVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
            {
                WaitForElementVisible(AutoRefresh_label);
                WaitForElementVisible(AutoRefreshYes_RadioButton);
                WaitForElementVisible(AutoRefreshNo_RadioButton);
            }
            else
            {
                WaitForElementNotVisible(AutoRefresh_label, 7);
                WaitForElementNotVisible(AutoRefreshYes_RadioButton, 7);
                WaitForElementNotVisible(AutoRefreshNo_RadioButton, 7);
            }

            return this;
        }

        public UserDashboardRecordPage ValidaterefreshTimeFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(refreshTime_label);
                WaitForElementVisible(refreshTime_Field);
            }
            else
            {
                WaitForElementVisible(refreshTime_label);
                WaitForElementVisible(refreshTime_Field);
            }

            return this;
        }


        public UserDashboardRecordPage TapDetailsTab()
        {
            Click(DetailsSection);

            return this;
        }

        public UserDashboardRecordPage TapAutoRefreshYesRadioButton()
        {
            WaitForElement(AutoRefreshYes_RadioButton);
            Click(AutoRefreshYes_RadioButton);

            return this;
        }

        public UserDashboardRecordPage TapAutoRefreshNoRadioButton()
        {
            WaitForElement(AutoRefreshNo_RadioButton);
            Click(AutoRefreshNo_RadioButton);

            return this;
        }

        public UserDashboardRecordPage InsertRefreshTime(string RefreshTimeSeconds)
        {
            WaitForElement(refreshTime_Field);
            SendKeys(refreshTime_Field, RefreshTimeSeconds);

            return this;
        }

        public UserDashboardRecordPage TapSaveButton()
        {
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}
