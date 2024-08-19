using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class CaseInvolvementRecordPage : CommonMethods
	{
        public CaseInvolvementRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=caseinvolvement')]");


        readonly By PageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By ShareRecordButton = By.XPath("//*[@id='TI_ShareRecordButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");

		readonly By CaseidLink = By.XPath("//*[@id='CWField_caseid_Link']");
		readonly By CaseidLookupButton = By.XPath("//*[@id='CWLookupBtn_caseid']");
		readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
		readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
		readonly By InvolvementmemberidLink = By.XPath("//*[@id='CWField_involvementmemberid_Link']");
		readonly By InvolvementmemberidLookupButton = By.XPath("//*[@id='CWLookupBtn_involvementmemberid']");
		readonly By InvolvementroleidLink = By.XPath("//*[@id='CWField_involvementroleid_Link']");
		readonly By InvolvementroleidLookupButton = By.XPath("//*[@id='CWLookupBtn_involvementroleid']");
		readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By InvolvementreasonidLookupButton = By.XPath("//*[@id='CWLookupBtn_involvementreasonid']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By InvolvementpriorityidLookupButton = By.XPath("//*[@id='CWLookupBtn_involvementpriorityid']");
		readonly By InvolvementstatusidLookupButton = By.XPath("//*[@id='CWLookupBtn_involvementstatusid']");
		readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EnddateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
		readonly By InvolvementendreasonidLookupButton = By.XPath("//*[@id='CWLookupBtn_involvementendreasonid']");
		readonly By SocialworkerchangereasonidLink = By.XPath("//*[@id='CWField_socialworkerchangereasonid_Link']");
		readonly By SocialworkerchangereasonidLookupButton = By.XPath("//*[@id='CWLookupBtn_socialworkerchangereasonid']");
		readonly By Description = By.XPath("//*[@id='CWField_description']");


        public CaseInvolvementRecordPage WaitForCaseInvolvementRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(PageHeader);

            return this;
        }



        public CaseInvolvementRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public CaseInvolvementRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public CaseInvolvementRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public CaseInvolvementRecordPage ClickShareRecordButton()
		{
			WaitForElementToBeClickable(ShareRecordButton);
			Click(ShareRecordButton);

			return this;
		}

		public CaseInvolvementRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}



		public CaseInvolvementRecordPage ClickCaseLink()
		{
			WaitForElementToBeClickable(CaseidLink);
			Click(CaseidLink);

			return this;
		}

		public CaseInvolvementRecordPage ValidateCaseLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CaseidLink);
			ValidateElementText(CaseidLink, ExpectedText);

			return this;
		}

		public CaseInvolvementRecordPage ClickCaseLookupButton()
		{
			WaitForElementToBeClickable(CaseidLookupButton);
			Click(CaseidLookupButton);

			return this;
		}

		public CaseInvolvementRecordPage ClickPersonLink()
		{
			WaitForElementToBeClickable(PersonidLink);
			Click(PersonidLink);

			return this;
		}

		public CaseInvolvementRecordPage ValidatePersonLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PersonidLink);
			ValidateElementText(PersonidLink, ExpectedText);

			return this;
		}

		public CaseInvolvementRecordPage ClickPersonLookupButton()
		{
			WaitForElementToBeClickable(PersonidLookupButton);
			Click(PersonidLookupButton);

			return this;
		}

		public CaseInvolvementRecordPage ClickInvolveMentmemberLink()
		{
			WaitForElementToBeClickable(InvolvementmemberidLink);
			Click(InvolvementmemberidLink);

			return this;
		}

		public CaseInvolvementRecordPage ValidateInvolveMentmemberLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(InvolvementmemberidLink);
			ValidateElementText(InvolvementmemberidLink, ExpectedText);

			return this;
		}

		public CaseInvolvementRecordPage ClickInvolveMentmemberLookupButton()
		{
			WaitForElementToBeClickable(InvolvementmemberidLookupButton);
			Click(InvolvementmemberidLookupButton);

			return this;
		}

		public CaseInvolvementRecordPage ClickInvolvementRoleLink()
		{
			WaitForElementToBeClickable(InvolvementroleidLink);
			Click(InvolvementroleidLink);

			return this;
		}

		public CaseInvolvementRecordPage ValidateInvolvementRoleLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(InvolvementroleidLink);
			ValidateElementText(InvolvementroleidLink, ExpectedText);

			return this;
		}

		public CaseInvolvementRecordPage ClickInvolvementRoleLookupButton()
		{
			WaitForElementToBeClickable(InvolvementroleidLookupButton);
			Click(InvolvementroleidLookupButton);

			return this;
		}

		public CaseInvolvementRecordPage ValidateStartDateText(string ExpectedText)
		{
			WaitForElement(Startdate);
            ValidateElementValue(Startdate, ExpectedText);

			return this;
		}

		public CaseInvolvementRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Startdate);
			SendKeys(Startdate, TextToInsert);
			
			return this;
		}

		public CaseInvolvementRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartdateDatePicker);
			Click(StartdateDatePicker);

			return this;
		}

		public CaseInvolvementRecordPage ClickInvolvementReasonLookupButton()
		{
			WaitForElementToBeClickable(InvolvementreasonidLookupButton);
			Click(InvolvementreasonidLookupButton);

			return this;
		}

		public CaseInvolvementRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public CaseInvolvementRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public CaseInvolvementRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public CaseInvolvementRecordPage ClickInvolvementPriorityLookupButton()
		{
			WaitForElementToBeClickable(InvolvementpriorityidLookupButton);
			Click(InvolvementpriorityidLookupButton);

			return this;
		}

		public CaseInvolvementRecordPage ClickInvolvementStatusLookupButton()
		{
			WaitForElementToBeClickable(InvolvementstatusidLookupButton);
			Click(InvolvementstatusidLookupButton);

			return this;
		}

		public CaseInvolvementRecordPage ValidateEndDateText(string ExpectedText)
		{
			WaitForElement(Enddate);
            ValidateElementValue(Enddate, ExpectedText);

			return this;
		}

		public CaseInvolvementRecordPage InsertTextOnEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Enddate);
			SendKeys(Enddate, TextToInsert);
			
			return this;
		}

		public CaseInvolvementRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EnddateDatePicker);
			Click(EnddateDatePicker);

			return this;
		}

		public CaseInvolvementRecordPage ClickInvolvementEndReasonLookupButton()
		{
			WaitForElementToBeClickable(InvolvementendreasonidLookupButton);
			Click(InvolvementendreasonidLookupButton);

			return this;
		}

		public CaseInvolvementRecordPage ClickSocialWorkerChangeReasonLink()
		{
			WaitForElementToBeClickable(SocialworkerchangereasonidLink);
			Click(SocialworkerchangereasonidLink);

			return this;
		}

		public CaseInvolvementRecordPage ValidateSocialWorkerChangeReasonLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(SocialworkerchangereasonidLink);
			ValidateElementText(SocialworkerchangereasonidLink, ExpectedText);

			return this;
		}

		public CaseInvolvementRecordPage ClickSocialWorkerChangeReasonLookupButton()
		{
			WaitForElementToBeClickable(SocialworkerchangereasonidLookupButton);
			Click(SocialworkerchangereasonidLookupButton);

			return this;
		}

        public CaseInvolvementRecordPage ValidateSocialWorkerChangeReasonLookupButtonDisabled(bool ExpectDisabled)
        {
			if(ExpectDisabled)
				ValidateElementDisabled(SocialworkerchangereasonidLookupButton);
            else
                ValidateElementNotDisabled(SocialworkerchangereasonidLookupButton);

            return this;
        }

        public CaseInvolvementRecordPage ValidateDescriptionText(string ExpectedText)
		{
			WaitForElement(Description);
			ValidateElementText(Description, ExpectedText);

            return this;
		}

		public CaseInvolvementRecordPage InsertTextOnDescription(string TextToInsert)
		{
			WaitForElementToBeClickable(Description);
			SendKeys(Description, TextToInsert);
			
			return this;
		}

	}
}
