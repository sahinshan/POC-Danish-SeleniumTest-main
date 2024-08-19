using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using System.Security.Policy;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class PersonalMoneyAccountRecordPage : CommonMethods
	{
        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersonalmoneyaccount')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By RunOnDemandWorkflow = By.XPath("//*[@id='TI_RunOnDemandWorkflow']");

        readonly By menuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By attachmentsSubMenuButton = By.XPath("//*[@id='CWNavItem_Attachments']");

        readonly By PersonalMoneyAccountDetailsTab = By.XPath("//*[@id='CWNavGroup_accountdetails']");
        readonly By DetailsTab = By.XPath("//*[@id='CWNavGroup_EditForm']");

        readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
		readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
		readonly By AccounttypeidLink = By.XPath("//*[@id='CWField_accounttypeid_Link']");
		readonly By AccounttypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_accounttypeid']");
		readonly By Inactive_1 = By.XPath("//*[@id='CWField_inactive_1']");
		readonly By Inactive_0 = By.XPath("//*[@id='CWField_inactive_0']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By Accountname = By.XPath("//*[@id='CWField_accountname']");

        public PersonalMoneyAccountRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        public PersonalMoneyAccountRecordPage WaitForPersonalMoneyAccountRecordPageToLoad(bool SaveButtonVisible = true)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);
			
			if(SaveButtonVisible)
				WaitForElementVisible(SaveButton);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(DetailsTab);

            return this;
        }

        public PersonalMoneyAccountRecordPage WaitForInactivePersonalMoneyAccountRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(PersonidLookupButton);

            return this;
        }

        public PersonalMoneyAccountRecordPage ValidatePageHeaderText(string ExpectedText)
        {
            ValidateElementText(pageHeader, ExpectedText);

            return this;
        }

        public PersonalMoneyAccountRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public PersonalMoneyAccountRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public PersonalMoneyAccountRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public PersonalMoneyAccountRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public PersonalMoneyAccountRecordPage ClickRunOnDemandWorkflow()
		{
			WaitForElementToBeClickable(RunOnDemandWorkflow);
			Click(RunOnDemandWorkflow);

			return this;
		}

        public PersonalMoneyAccountRecordPage ClickPersonalMoneyAccountDetailsTab()
        {
            WaitForElementToBeClickable(PersonalMoneyAccountDetailsTab);
            Click(PersonalMoneyAccountDetailsTab);

            return this;
        }

        public PersonalMoneyAccountRecordPage ValidatePersonalMoneyAccountDetailsTabVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PersonalMoneyAccountDetailsTab);
            else
                WaitForElementNotVisible(PersonalMoneyAccountDetailsTab, 3);

            return this;
        }


        public PersonalMoneyAccountRecordPage NavigateToAttachmentsPage()
        {
            WaitForElementToBeClickable(menuButton);
            Click(menuButton);

            WaitForElementToBeClickable(attachmentsSubMenuButton);
            Click(attachmentsSubMenuButton);

            return this;
        }


        public PersonalMoneyAccountRecordPage ClickPersonLink()
		{
			WaitForElementToBeClickable(PersonidLink);
			Click(PersonidLink);

			return this;
		}

		public PersonalMoneyAccountRecordPage ValidatePersonLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PersonidLink);
			ValidateElementText(PersonidLink, ExpectedText);

			return this;
		}

		public PersonalMoneyAccountRecordPage ClickPersonLookupButton()
		{
			WaitForElementToBeClickable(PersonidLookupButton);
			Click(PersonidLookupButton);

			return this;
		}

        public PersonalMoneyAccountRecordPage ValidatePersonLookupButtonDisabled(bool ExpectDisabled)
        {
			if(ExpectDisabled)
				ValidateElementDisabled(PersonidLookupButton);
			else
                ValidateElementNotDisabled(PersonidLookupButton);

            return this;
        }

        public PersonalMoneyAccountRecordPage ClickAccountTypeLink()
		{
			WaitForElementToBeClickable(AccounttypeidLink);
			Click(AccounttypeidLink);

			return this;
		}

		public PersonalMoneyAccountRecordPage ValidateAccountTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(AccounttypeidLink);
			ValidateElementText(AccounttypeidLink, ExpectedText);

			return this;
		}

		public PersonalMoneyAccountRecordPage ClickAccountTypeLookupButton()
		{
			WaitForElementToBeClickable(AccounttypeidLookupButton);
			Click(AccounttypeidLookupButton);

			return this;
		}

        public PersonalMoneyAccountRecordPage ValidateAccountTypeLookupButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(AccounttypeidLookupButton);
            else
                ValidateElementNotDisabled(AccounttypeidLookupButton);

            return this;
        }

        public PersonalMoneyAccountRecordPage ClickInactive_YesRadioButton()
		{
			WaitForElementToBeClickable(Inactive_1);
			Click(Inactive_1);

			return this;
		}

		public PersonalMoneyAccountRecordPage ValidateInactive_YesRadioButtonChecked()
		{
			WaitForElement(Inactive_1);
			ValidateElementChecked(Inactive_1);
			
			return this;
		}

		public PersonalMoneyAccountRecordPage ValidateInactive_YesRadioButtonNotChecked()
		{
			WaitForElement(Inactive_1);
			ValidateElementNotChecked(Inactive_1);
			
			return this;
		}

        public PersonalMoneyAccountRecordPage ValidateInactive_YesRadioButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(Inactive_1);
            else
                ValidateElementNotDisabled(Inactive_1);

            return this;
        }

        public PersonalMoneyAccountRecordPage ClickInactive_NoRadioButton()
		{
			WaitForElementToBeClickable(Inactive_0);
			Click(Inactive_0);

			return this;
		}

		public PersonalMoneyAccountRecordPage ValidateInactive_NoRadioButtonChecked()
		{
			WaitForElement(Inactive_0);
			ValidateElementChecked(Inactive_0);
			
			return this;
		}

		public PersonalMoneyAccountRecordPage ValidateInactive_NoRadioButtonNotChecked()
		{
			WaitForElement(Inactive_0);
			ValidateElementNotChecked(Inactive_0);
			
			return this;
		}

        public PersonalMoneyAccountRecordPage ValidateInactive_NoRadioButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(Inactive_0);
            else
                ValidateElementNotDisabled(Inactive_0);

            return this;
        }

        public PersonalMoneyAccountRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public PersonalMoneyAccountRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public PersonalMoneyAccountRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

        public PersonalMoneyAccountRecordPage ValidateResponsibleTeamLookupButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(ResponsibleTeamLookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeamLookupButton);

            return this;
        }

        public PersonalMoneyAccountRecordPage ValidateAccountNameText(string ExpectedText)
		{
			ValidateElementValue(Accountname, ExpectedText);

			return this;
		}

		public PersonalMoneyAccountRecordPage InsertTextOnAccountName(string TextToInsert)
		{
			WaitForElementToBeClickable(Accountname);
			SendKeys(Accountname, TextToInsert);
			
			return this;
		}

        public PersonalMoneyAccountRecordPage ValidateAccountNameFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(Accountname);
            else
                ValidateElementNotDisabled(Accountname);

            return this;
        }

    }
}
