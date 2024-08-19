    
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemDashboardRecordPage : CommonMethods
    {
        public SystemDashboardRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By SystemDashboardRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemdashboard&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");


        
        readonly By saveButton = By.Id("TI_SaveButton");

        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");

        //


        #region Navigation Area

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");

        readonly By DashboardSection = By.XPath("//li[@id='CWNavGroup_Dashboard']/a[@title='Dashboard']");
        readonly By DetailsSection = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");

        #endregion

        #region Dashboard Tab

        readonly By EditButton_DashboardTab = By.XPath("//*[@id='TI_EditRecordButton']");
        readonly By SaveButton_DashboardTab = By.XPath("//*[@id='TI_SaveButton']");
        By WidgetSettingsButton_DashboardTab(int WidgetPosition) => By.XPath("//*[@id='CWMain']/div/div[" + WidgetPosition + "]/div/div/div/div/button[@title='Settings']");
        By WidgetHeader_DashboardTab(int WidgetPosition, string BackgroudColor) => By.XPath("//*[@id='CWMain']/div/div[" + WidgetPosition + "]/div/div/div[@class='card-header pr-3 bg-" + BackgroudColor + " non-draggable-widget']");


        #endregion

        readonly By AutoRefresh_label = By.XPath("//*[@id='CWLabelHolder_autorefresh']/label");
        readonly By AutoRefreshYes_RadioButton = By.Id("CWField_autorefresh_1");
        readonly By AutoRefreshNo_RadioButton = By.Id("CWField_autorefresh_0");
        
        readonly By refreshTime_label = By.XPath("//*[@id='CWLabelHolder_refreshtime']/label");
        readonly By refreshTime_Field = By.Id("CWField_refreshtime");
        readonly By refreshTime_NotificationArea = By.XPath("//*[@id='CWControlHolder_refreshtime']/label/span");




        public SystemDashboardRecordPage WaitForSystemDashboardRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(SystemDashboardRecordIFrame);
            SwitchToIframe(SystemDashboardRecordIFrame);

            WaitForElement(MenuButton);
            WaitForElement(DashboardSection);
            WaitForElement(DetailsSection);

            return this;
        }

        public SystemDashboardRecordPage WaitForDashboardTabToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(SystemDashboardRecordIFrame);
            SwitchToIframe(SystemDashboardRecordIFrame);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElement(EditButton_DashboardTab);

            return this;
        }

        public SystemDashboardRecordPage ValidateNotificationMessageArea(string ExpectedMessage)
        {
            ValidateElementText(notificationMessageArea, ExpectedMessage);

            return this;
        }

        public SystemDashboardRecordPage ValidateRefreshTimeErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(refreshTime_NotificationArea, ExpectedMessage);

            return this;
        }

        public SystemDashboardRecordPage ValidateAutoRefreshFieldVisibility(bool ExpectVisible)
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

        public SystemDashboardRecordPage ValidaterefreshTimeFieldVisibility(bool ExpectVisible)
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


        public SystemDashboardRecordPage TapDetailsTab()
        {
            Click(DetailsSection);

            return this;
        }

        public SystemDashboardRecordPage TapAutoRefreshYesRadioButton()
        {
            WaitForElement(AutoRefreshYes_RadioButton);
            Click(AutoRefreshYes_RadioButton);

            return this;
        }

        public SystemDashboardRecordPage TapAutoRefreshNoRadioButton()
        {
            WaitForElement(AutoRefreshNo_RadioButton);
            Click(AutoRefreshNo_RadioButton);

            return this;
        }

        public SystemDashboardRecordPage InsertRefreshTime(string RefreshTimeSeconds)
        {
            WaitForElement(refreshTime_Field);
            SendKeys(refreshTime_Field, RefreshTimeSeconds);

            return this;
        }

        public SystemDashboardRecordPage TapSaveButton()
        {
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        #region Dashboard Tab

        public SystemDashboardRecordPage ClickEditButton_DashboardTab()
        {
            WaitForElementToBeClickable(EditButton_DashboardTab);
            Click(EditButton_DashboardTab);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SystemDashboardRecordPage ClickSaveButton_DashboardTab()
        {
            WaitForElementToBeClickable(SaveButton_DashboardTab);
            Click(SaveButton_DashboardTab);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SystemDashboardRecordPage ClickWidgetSettingsButton_DashboardTab(int WidgetPosition)
        {
            WaitForElementToBeClickable(WidgetSettingsButton_DashboardTab(WidgetPosition));
            Click(WidgetSettingsButton_DashboardTab(WidgetPosition));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SystemDashboardRecordPage ValidateWidgetHeaderBackgroudColor_DashboardTab(int WidgetPosition, string ExpectedHeaderBackgroudColor)
        {
            WaitForElementVisible(WidgetHeader_DashboardTab(WidgetPosition, ExpectedHeaderBackgroudColor));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        #endregion

    }
}
