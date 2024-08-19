using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
	public class CommunityClinicTeamRecordPage : CommonMethods
	{
        public CommunityClinicTeamRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=communityandclinicteam&')]");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");

		readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
		readonly By DiaryViewSetupLinkButton = By.XPath("//*[@id='CWNavItem_CommunityClinicDiaryViewSetup']");

        readonly By Title = By.XPath("//*[@id='CWField_title']");
		readonly By ProvideridLookupButton = By.XPath("//*[@id='CWLookupBtn_providerid']");
		readonly By TeamidLookupButton = By.XPath("//*[@id='CWLookupBtn_teamid']");
		readonly By Comments = By.XPath("//*[@id='CWField_comments']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By CommunityandclinicteamspecialistcodeidLookupButton = By.XPath("//*[@id='CWLookupBtn_communityandclinicteamspecialistcodeid']");
		readonly By Supportspathwayrttrules_1 = By.XPath("//*[@id='CWField_supportspathwayrttrules_1']");
		readonly By Supportspathwayrttrules_0 = By.XPath("//*[@id='CWField_supportspathwayrttrules_0']");
		readonly By Inactive_1 = By.XPath("//*[@id='CWField_inactive_1']");
		readonly By Inactive_0 = By.XPath("//*[@id='CWField_inactive_0']");


        public CommunityClinicTeamRecordPage WaitForCommunityClinicTeamRecordPage()
        {
            SwitchToDefaultFrame();

			WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementVisible(pageHeader);

            return this;
        }

        public CommunityClinicTeamRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public CommunityClinicTeamRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public CommunityClinicTeamRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}


		public CommunityClinicTeamRecordPage ValidateCommunityClinicTeamText(string ExpectedText)
		{
			WaitForElementVisible(Title);
			ValidateElementValue(Title, ExpectedText);

            return this;
		}

		public CommunityClinicTeamRecordPage InsertTextOnCommunityClinicTeam(string TextToInsert)
		{
			WaitForElementToBeClickable(Title);
			SendKeys(Title, TextToInsert);
			
			return this;
		}

		public CommunityClinicTeamRecordPage ClickHealthProviderLookupButton()
		{
			WaitForElementToBeClickable(ProvideridLookupButton);
			Click(ProvideridLookupButton);

			return this;
		}

		public CommunityClinicTeamRecordPage ClickTeamLookupButton()
		{
			WaitForElementToBeClickable(TeamidLookupButton);
			Click(TeamidLookupButton);

			return this;
		}

		public CommunityClinicTeamRecordPage ValidateCommentsText(string ExpectedText)
		{
			WaitForElementVisible(Comments);
			ValidateElementText(Comments, ExpectedText);

            return this;
		}

		public CommunityClinicTeamRecordPage InsertTextOnComments(string TextToInsert)
		{
			WaitForElementToBeClickable(Comments);
			SendKeys(Comments, TextToInsert);
			
			return this;
		}

		public CommunityClinicTeamRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public CommunityClinicTeamRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public CommunityClinicTeamRecordPage ClickResponsibleTeamClearButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamClearButton);
			Click(ResponsibleTeamClearButton);

			return this;
		}

		public CommunityClinicTeamRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public CommunityClinicTeamRecordPage ClickMainSpecialityCodeLookupButton()
		{
			WaitForElementToBeClickable(CommunityandclinicteamspecialistcodeidLookupButton);
			Click(CommunityandclinicteamspecialistcodeidLookupButton);

			return this;
		}

		public CommunityClinicTeamRecordPage ClickSupportsPathwayRTTRules_YesRadioButton()
		{
			WaitForElementToBeClickable(Supportspathwayrttrules_1);
			Click(Supportspathwayrttrules_1);

			return this;
		}

		public CommunityClinicTeamRecordPage ValidateSupportsPathwayRTTRules_YesRadioButtonChecked()
		{
			WaitForElement(Supportspathwayrttrules_1);
			ValidateElementChecked(Supportspathwayrttrules_1);
			
			return this;
		}

		public CommunityClinicTeamRecordPage ValidateSupportsPathwayRTTRules_YesRadioButtonNotChecked()
		{
			WaitForElement(Supportspathwayrttrules_1);
			ValidateElementNotChecked(Supportspathwayrttrules_1);
			
			return this;
		}

		public CommunityClinicTeamRecordPage ClickSupportsPathwayRTTRules_NoRadioButton()
		{
			WaitForElementToBeClickable(Supportspathwayrttrules_0);
			Click(Supportspathwayrttrules_0);

			return this;
		}

		public CommunityClinicTeamRecordPage ValidateSupportsPathwayRTTRules_NoRadioButtonChecked()
		{
			WaitForElement(Supportspathwayrttrules_0);
			ValidateElementChecked(Supportspathwayrttrules_0);
			
			return this;
		}

		public CommunityClinicTeamRecordPage ValidateSupportsPathwayRTTRules_NoRadioButtonNotChecked()
		{
			WaitForElement(Supportspathwayrttrules_0);
			ValidateElementNotChecked(Supportspathwayrttrules_0);
			
			return this;
		}

		public CommunityClinicTeamRecordPage ClickInactive_YesRadioButton()
		{
			WaitForElementToBeClickable(Inactive_1);
			Click(Inactive_1);

			return this;
		}

		public CommunityClinicTeamRecordPage ValidateInactive_YesRadioButtonChecked()
		{
			WaitForElement(Inactive_1);
			ValidateElementChecked(Inactive_1);
			
			return this;
		}

		public CommunityClinicTeamRecordPage ValidateInactive_YesRadioButtonNotChecked()
		{
			WaitForElement(Inactive_1);
			ValidateElementNotChecked(Inactive_1);
			
			return this;
		}

		public CommunityClinicTeamRecordPage ClickInactive_NoRadioButton()
		{
			WaitForElementToBeClickable(Inactive_0);
			Click(Inactive_0);

			return this;
		}

		public CommunityClinicTeamRecordPage ValidateInactive_NoRadioButtonChecked()
		{
			WaitForElement(Inactive_0);
			ValidateElementChecked(Inactive_0);
			
			return this;
		}

		public CommunityClinicTeamRecordPage ValidateInactive_NoRadioButtonNotChecked()
		{
			WaitForElement(Inactive_0);
			ValidateElementNotChecked(Inactive_0);
			
			return this;
		}

        public CommunityClinicTeamRecordPage NavigateToDiaryViewSetup()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(DiaryViewSetupLinkButton);
            Click(DiaryViewSetupLinkButton);

            return this;
        }

    }
}
