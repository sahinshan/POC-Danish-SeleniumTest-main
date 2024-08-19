using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class PersonalisedCareAndSupportPlanRecordPage : CommonMethods
	{
        public PersonalisedCareAndSupportPlanRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By personcareplanIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personcareplan&')]");
        readonly By copyCarePlanIFrame = By.Id("iframe_CopyCarePlan");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        #endregion

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By ShareRecordButton = By.XPath("//*[@id='TI_ShareRecordButton']");
		readonly By AuthoriseButton = By.XPath("//*[@id='TI_AuthoriseButton']");
		readonly By Careplannumber = By.XPath("//*[@id='CWField_careplannumber']");
		readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
		readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
		readonly By CareplanagreedbyidLookupButton = By.XPath("//*[@id='CWLookupBtn_careplanagreedbyid']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By Dateofagreement = By.XPath("//*[@id='CWField_dateofagreement']");
		readonly By DateofagreementDatePicker = By.XPath("//*[@id='CWField_dateofagreement_DatePicker']");
		readonly By CareplanneeddomainidLink = By.XPath("//*[@id='CWField_careplanneeddomainid_Link']");
		readonly By CareplanneeddomainidClearButton = By.XPath("//*[@id='CWClearLookup_careplanneeddomainid']");
		readonly By CareplanneeddomainidLookupButton = By.XPath("//*[@id='CWLookupBtn_careplanneeddomainid']");
		readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By Setregularreviewcycle_1 = By.XPath("//*[@id='CWField_setregularreviewcycle_1']");
		readonly By Setregularreviewcycle_0 = By.XPath("//*[@id='CWField_setregularreviewcycle_0']");
		readonly By Reviewdate = By.XPath("//*[@id='CWField_reviewdate']");
		readonly By ReviewdateDatePicker = By.XPath("//*[@id='CWField_reviewdate_DatePicker']");
		readonly By Statusid = By.XPath("//*[@id='CWField_statusid']");
		readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EnddateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
		readonly By LastreviewedbyidLink = By.XPath("//*[@id='CWField_lastreviewedbyid_Link']");
		readonly By LastreviewedbyidLookupButton = By.XPath("//*[@id='CWLookupBtn_lastreviewedbyid']");
		readonly By Lastrevieweddate = By.XPath("//*[@id='CWField_lastrevieweddate']");
		readonly By LastrevieweddateDatePicker = By.XPath("//*[@id='CWField_lastrevieweddate_DatePicker']");
		readonly By Careneedtypeid = By.XPath("//*[@id='CWField_careneedtypeid']");
		readonly By Milestone = By.XPath("//*[@id='CWField_milestone']");
		readonly By Currentsituation = By.XPath("//*[@id='CWField_currentsituation']");
		readonly By Expectedoutcome = By.XPath("//*[@id='CWField_expectedoutcome']");
		readonly By Actions = By.XPath("//*[@id='CWField_actions']");
		readonly By AuthorisedbyidLink = By.XPath("//*[@id='CWField_authorisedbyid_Link']");
		readonly By AuthorisedbyidLookupButton = By.XPath("//*[@id='CWLookupBtn_authorisedbyid']");
		readonly By Authorisationdatetime = By.XPath("//*[@id='CWField_authorisationdatetime']");
		readonly By AuthorisationdatetimeDatePicker = By.XPath("//*[@id='CWField_authorisationdatetime_DatePicker']");
		readonly By Authorisationdatetime_Time = By.XPath("//*[@id='CWField_authorisationdatetime_Time']");
		readonly By Authorisationdatetime_Time_TimePicker = By.XPath("//*[@id='CWField_authorisationdatetime_Time_TimePicker']");
		readonly By ToolbarMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By EndCarePlanButton = By.XPath("//*[@id = 'TI_EndButton']");
		readonly By EndReasonField_LinkText = By.XPath("//*[@id = 'CWField_careplanendreasonid_Link']");


        public PersonalisedCareAndSupportPlanRecordPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(personcareplanIFrame);
            SwitchToIframe(personcareplanIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(pageHeader);

            return this;
        }

        public PersonalisedCareAndSupportPlanRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickShareRecordButton()
		{
			WaitForElementToBeClickable(ShareRecordButton);
			Click(ShareRecordButton);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickAuthoriseButton()
		{
			WaitForElementToBeClickable(AuthoriseButton);
			ScrollToElement(AuthoriseButton);
			Click(AuthoriseButton);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickToolbarMenuButton()
		{
			WaitForElement(ToolbarMenuButton);
			ScrollToElement(ToolbarMenuButton);
            var IsToolbarDropdownExpanded = GetElementByAttributeValue(ToolbarMenuButton, "aria-expanded");
			if (IsToolbarDropdownExpanded.Equals("false"))
				Click(ToolbarMenuButton);

			return this;

		}

		public PersonalisedCareAndSupportPlanRecordPage ClickEndCarePlanButton()
		{
			ClickToolbarMenuButton();

			WaitForElement(EndCarePlanButton);
			ScrollToElement(EndCarePlanButton);
			Click(EndCarePlanButton);

			return this;
		}

        //verify EndReasonField_LinkText text
		public PersonalisedCareAndSupportPlanRecordPage ValidateEndReasonField_LinkText(string ExpectedText)
		{
			WaitForElement(EndReasonField_LinkText);
			ScrollToElement(EndReasonField_LinkText);
            ValidateElementText(EndReasonField_LinkText, ExpectedText);

            return this;
        }


        public PersonalisedCareAndSupportPlanRecordPage ValidateCareplannumberText(string ExpectedText)
		{
			ValidateElementValue(Careplannumber, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage InsertTextOnCareplannumber(string TextToInsert)
		{
			WaitForElementToBeClickable(Careplannumber);
			SendKeys(Careplannumber, TextToInsert);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickPersonidLink()
		{
			WaitForElementToBeClickable(PersonidLink);
			Click(PersonidLink);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidatePersonidLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PersonidLink);
			ValidateElementText(PersonidLink, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickPersonidLookupButton()
		{
			WaitForElementToBeClickable(PersonidLookupButton);
			Click(PersonidLookupButton);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickCareplanagreedbyidLookupButton()
		{
			WaitForElementToBeClickable(CareplanagreedbyidLookupButton);
			Click(CareplanagreedbyidLookupButton);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateDateofagreementText(string ExpectedText)
		{
			ValidateElementValue(Dateofagreement, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage InsertTextOnDateofagreement(string TextToInsert)
		{
			WaitForElementToBeClickable(Dateofagreement);
			SendKeys(Dateofagreement, TextToInsert);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickDateofagreementDatePicker()
		{
			WaitForElementToBeClickable(DateofagreementDatePicker);
			Click(DateofagreementDatePicker);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickCareplanneeddomainidLink()
		{
			WaitForElementToBeClickable(CareplanneeddomainidLink);
			Click(CareplanneeddomainidLink);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateCareplanneeddomainidLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CareplanneeddomainidLink);
			ValidateElementText(CareplanneeddomainidLink, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickCareplanneeddomainidClearButton()
		{
			WaitForElementToBeClickable(CareplanneeddomainidClearButton);
			Click(CareplanneeddomainidClearButton);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickCareplanneeddomainidLookupButton()
		{
			WaitForElementToBeClickable(CareplanneeddomainidLookupButton);
			Click(CareplanneeddomainidLookupButton);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateStartdateText(string ExpectedText)
		{
			ValidateElementValue(Startdate, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage InsertTextOnStartdate(string TextToInsert)
		{
			WaitForElementToBeClickable(Startdate);
			SendKeys(Startdate, TextToInsert);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickStartdateDatePicker()
		{
			WaitForElementToBeClickable(StartdateDatePicker);
			Click(StartdateDatePicker);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickSetregularreviewcycle_1()
		{
			WaitForElementToBeClickable(Setregularreviewcycle_1);
			Click(Setregularreviewcycle_1);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateSetregularreviewcycle_1Checked()
		{
			WaitForElement(Setregularreviewcycle_1);
			ValidateElementChecked(Setregularreviewcycle_1);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateSetregularreviewcycle_1NotChecked()
		{
			WaitForElement(Setregularreviewcycle_1);
			ValidateElementNotChecked(Setregularreviewcycle_1);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickSetregularreviewcycle_0()
		{
			WaitForElementToBeClickable(Setregularreviewcycle_0);
			Click(Setregularreviewcycle_0);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateSetregularreviewcycle_0Checked()
		{
			WaitForElement(Setregularreviewcycle_0);
			ValidateElementChecked(Setregularreviewcycle_0);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateSetregularreviewcycle_0NotChecked()
		{
			WaitForElement(Setregularreviewcycle_0);
			ValidateElementNotChecked(Setregularreviewcycle_0);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateReviewdateText(string ExpectedText)
		{
			ValidateElementValue(Reviewdate, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage InsertTextOnReviewdate(string TextToInsert)
		{
			WaitForElementToBeClickable(Reviewdate);
			SendKeys(Reviewdate, TextToInsert);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickReviewdateDatePicker()
		{
			WaitForElementToBeClickable(ReviewdateDatePicker);
			Click(ReviewdateDatePicker);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage SelectStatusid(string TextToSelect)
		{
			WaitForElementToBeClickable(Statusid);
			SelectPicklistElementByText(Statusid, TextToSelect);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateStatusSelectedText(string ExpectedText)
		{
			ValidatePicklistSelectedText(Statusid, ExpectedText);

			return this;
		}

		//Care Need Ended date field
		public PersonalisedCareAndSupportPlanRecordPage ValidateEndDateText(string ExpectedText)
		{
			WaitForElement(Enddate);
			ScrollToElement(Enddate);
			ValidateElementValue(Enddate, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage InsertTextOnEnddate(string TextToInsert)
		{
			WaitForElementToBeClickable(Enddate);
			SendKeys(Enddate, TextToInsert);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickEnddateDatePicker()
		{
			WaitForElementToBeClickable(EnddateDatePicker);
			Click(EnddateDatePicker);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickLastreviewedbyidLink()
		{
			WaitForElementToBeClickable(LastreviewedbyidLink);
			Click(LastreviewedbyidLink);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateLastreviewedbyidLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(LastreviewedbyidLink);
			ValidateElementText(LastreviewedbyidLink, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickLastreviewedbyidLookupButton()
		{
			WaitForElementToBeClickable(LastreviewedbyidLookupButton);
			Click(LastreviewedbyidLookupButton);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateLastrevieweddateText(string ExpectedText)
		{
			ValidateElementValue(Lastrevieweddate, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage InsertTextOnLastrevieweddate(string TextToInsert)
		{
			WaitForElementToBeClickable(Lastrevieweddate);
			SendKeys(Lastrevieweddate, TextToInsert);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickLastrevieweddateDatePicker()
		{
			WaitForElementToBeClickable(LastrevieweddateDatePicker);
			Click(LastrevieweddateDatePicker);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage SelectCareneedtypeid(string TextToSelect)
		{
			WaitForElementToBeClickable(Careneedtypeid);
			SelectPicklistElementByText(Careneedtypeid, TextToSelect);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateCareneedtypeidSelectedText(string ExpectedText)
		{
			ValidateElementText(Careneedtypeid, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateMilestoneText(string ExpectedText)
		{
			ValidateElementValue(Milestone, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage InsertTextOnMilestone(string TextToInsert)
		{
			WaitForElementToBeClickable(Milestone);
			SendKeys(Milestone, TextToInsert);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateCurrentsituationText(string ExpectedText)
		{
			ValidateElementText(Currentsituation, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage InsertTextOnCurrentsituation(string TextToInsert)
		{
			WaitForElementToBeClickable(Currentsituation);
			SendKeys(Currentsituation, TextToInsert);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateExpectedoutcomeText(string ExpectedText)
		{
			ValidateElementText(Expectedoutcome, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage InsertTextOnExpectedoutcome(string TextToInsert)
		{
			WaitForElementToBeClickable(Expectedoutcome);
			SendKeys(Expectedoutcome, TextToInsert);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateActionsText(string ExpectedText)
		{
			ValidateElementText(Actions, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage InsertTextOnActions(string TextToInsert)
		{
			WaitForElementToBeClickable(Actions);
			SendKeys(Actions, TextToInsert);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickAuthorisedbyidLink()
		{
			WaitForElementToBeClickable(AuthorisedbyidLink);
			Click(AuthorisedbyidLink);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateAuthorisedbyidLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(AuthorisedbyidLink);
			ValidateElementText(AuthorisedbyidLink, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickAuthorisedbyidLookupButton()
		{
			WaitForElementToBeClickable(AuthorisedbyidLookupButton);
			Click(AuthorisedbyidLookupButton);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateAuthorisationdatetimeText(string ExpectedText)
		{
			ValidateElementValue(Authorisationdatetime, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage InsertTextOnAuthorisationdatetime(string TextToInsert)
		{
			WaitForElementToBeClickable(Authorisationdatetime);
			SendKeys(Authorisationdatetime, TextToInsert);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickAuthorisationdatetimeDatePicker()
		{
			WaitForElementToBeClickable(AuthorisationdatetimeDatePicker);
			Click(AuthorisationdatetimeDatePicker);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ValidateAuthorisationdatetime_TimeText(string ExpectedText)
		{
			ValidateElementValue(Authorisationdatetime_Time, ExpectedText);

			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage InsertTextOnAuthorisationdatetime_Time(string TextToInsert)
		{
			WaitForElementToBeClickable(Authorisationdatetime_Time);
			SendKeys(Authorisationdatetime_Time, TextToInsert);
			
			return this;
		}

		public PersonalisedCareAndSupportPlanRecordPage ClickAuthorisationdatetime_Time_TimePicker()
		{
			WaitForElementToBeClickable(Authorisationdatetime_Time_TimePicker);
			Click(Authorisationdatetime_Time_TimePicker);

			return this;
		}

	}
}
