    
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SummaryDashboardRecordPage : CommonMethods
    {
        public SummaryDashboardRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        #region IFrames

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By SummaryDashboardRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=businessobjectdashboard&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'


        #region Dashboards

        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        #endregion


        #endregion



        readonly By saveButton = By.Id("TI_SaveButton");

        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");

        //


        #region Navigation Area

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");

        readonly By DashboardSection = By.XPath("//li[@id='CWNavGroup_Dashboard']/a[@title='Dashboard']");
        readonly By DetailsSection = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");

        #endregion

        #region Dashboard

        readonly By addRecord_Button = By.Id("TI_AddWidgetButton");
        readonly By saveRecord_Button = By.Id("TI_SaveButton");
        readonly By editRecord_Button = By.Id("TI_EditRecordButton");

        By editWidget_Button(int WidgetPosition) => By.XPath("//*[@id='CWMain']/div/div[" + WidgetPosition + "]/div/div/div/div/button[@class='btnSettingsWidget btn']");

        

        #endregion

        #region Details Section

        readonly By AutoRefresh_label = By.XPath("//*[@id='CWLabelHolder_autorefresh']/label");
        readonly By AutoRefreshYes_RadioButton = By.Id("CWField_autorefresh_1");
        readonly By AutoRefreshNo_RadioButton = By.Id("CWField_autorefresh_0");

        readonly By refreshTime_label = By.XPath("//*[@id='CWLabelHolder_refreshtime']/label");
        readonly By refreshTime_Field = By.Id("CWField_refreshtime");
        readonly By refreshTime_NotificationArea = By.XPath("//*[@id='CWControlHolder_refreshtime']/label/span");

        #endregion



        public SummaryDashboardRecordPage WaitForSummaryDashboardRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(SummaryDashboardRecordIFrame);
            SwitchToIframe(SummaryDashboardRecordIFrame);

            WaitForElement(MenuButton);
            WaitForElement(DashboardSection);
            WaitForElement(DetailsSection);

            return this;
        }

        public SummaryDashboardRecordPage WaitForDashboardTabToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(SummaryDashboardRecordIFrame);
            SwitchToIframe(SummaryDashboardRecordIFrame);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElement(editRecord_Button);

            return this;
        }


        public SummaryDashboardRecordPage ValidateNotificationMessageArea(string ExpectedMessage)
        {
            ValidateElementText(notificationMessageArea, ExpectedMessage);

            return this;
        }

        public SummaryDashboardRecordPage ValidateRefreshTimeErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(refreshTime_NotificationArea, ExpectedMessage);

            return this;
        }

        public SummaryDashboardRecordPage ValidateAutoRefreshFieldVisibility(bool ExpectVisible)
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

        public SummaryDashboardRecordPage ValidaterefreshTimeFieldVisibility(bool ExpectVisible)
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


        public SummaryDashboardRecordPage TapDetailsTab()
        {
            Click(DetailsSection);

            return this;
        }

        public SummaryDashboardRecordPage TapAutoRefreshYesRadioButton()
        {
            WaitForElement(AutoRefreshYes_RadioButton);
            Click(AutoRefreshYes_RadioButton);

            return this;
        }

        public SummaryDashboardRecordPage TapAutoRefreshNoRadioButton()
        {
            WaitForElement(AutoRefreshNo_RadioButton);
            Click(AutoRefreshNo_RadioButton);

            return this;
        }

        public SummaryDashboardRecordPage InsertRefreshTime(string RefreshTimeSeconds)
        {
            WaitForElement(refreshTime_Field);
            SendKeys(refreshTime_Field, RefreshTimeSeconds);

            return this;
        }

        public SummaryDashboardRecordPage TapSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SummaryDashboardRecordPage ClickAddWidgetButton()
        {
            WaitForElementToBeClickable(addRecord_Button);
            Click(addRecord_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SummaryDashboardRecordPage ClickSaveWidgetButton()
        {
            WaitForElementToBeClickable(saveRecord_Button);
            Click(saveRecord_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SummaryDashboardRecordPage ClickToogleEditWidgetsButton()
        {
            WaitForElementToBeClickable(editRecord_Button);
            Click(editRecord_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SummaryDashboardRecordPage ClickEditWidgetButton(int WidgetPosition)
        {
            WaitForElementToBeClickable(editWidget_Button(WidgetPosition));
            Click(editWidget_Button(WidgetPosition));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

    }
}
