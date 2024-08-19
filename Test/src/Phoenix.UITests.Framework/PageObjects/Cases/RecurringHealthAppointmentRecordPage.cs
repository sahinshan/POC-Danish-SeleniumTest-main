using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Cases
{
    public class RecurringHealthAppointmentRecordPage : CommonMethods
    {
        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=recurringhealthappointment')]");		
		readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
		readonly By AdditionalOptionToolbarButton = By.Id("CWToolbarMenu");
		readonly By RestrictAccessButton = By.Id("TI_RestrictAccessButton");
		readonly By HealthappointmentreasonidLookupButton = By.XPath("//*[@id='CWLookupBtn_healthappointmentreasonid']");
		readonly By HealthAppointmentReasonLink = By.Id("CWField_healthappointmentreasonid_Link");
		readonly By ContactTypeLink = By.Id("CWField_healthappointmentcontacttypeid_Link");
		readonly By CommunityandclinicteamidLookupButton = By.XPath("//*[@id='CWLookupBtn_communityandclinicteamid']");
		readonly By Homevisit_1 = By.XPath("//*[@id='CWField_homevisit_1']");
		readonly By Homevisit_0 = By.XPath("//*[@id='CWField_homevisit_0']");
		readonly By CaseidLink = By.XPath("//*[@id='CWField_caseid_Link']");
		readonly By CaseidClearButton = By.XPath("//*[@id='CWClearLookup_caseid']");
		readonly By CaseidLookupButton = By.XPath("//*[@id='CWLookupBtn_caseid']");
		readonly By TranslatorRequired = By.XPath("//*[@id='CWField_TranslatorRequired']");
		readonly By HealthappointmentcontacttypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_healthappointmentcontacttypeid']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By ResponsibleuseridLink = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
		readonly By ResponsibleuseridClearButton = By.XPath("//*[@id='CWClearLookup_responsibleuserid']");
		readonly By ResponsibleuseridLookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
		readonly By Groupbooking_1 = By.XPath("//*[@id='CWField_groupbooking_1']");
		readonly By Groupbooking_0 = By.XPath("//*[@id='CWField_groupbooking_0']");
		readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
		readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
		readonly By PreferredLanguage = By.XPath("//*[@id='CWField_PreferredLanguage']");
		readonly By Starttime = By.XPath("//*[@id='CWField_starttime']");
		readonly By Starttime_TimePicker = By.XPath("//*[@id='CWField_starttime_TimePicker']");
		readonly By Endtime = By.XPath("//*[@id='CWField_endtime']");
		readonly By Endtime_TimePicker = By.XPath("//*[@id='CWField_endtime_TimePicker']");
		readonly By RecurrencepatternidLookupButton = By.XPath("//*[@id='CWLookupBtn_recurrencepatternid']");
		readonly By RecurrencePatternLink = By.Id("CWField_recurrencepatternid_Link");
		readonly By StartRange = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartRangeDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By Firstappointmentdate = By.XPath("//*[@id='CWField_firstappointmentdate']");
		readonly By FirstappointmentdateDatePicker = By.XPath("//*[@id='CWField_firstappointmentdate_DatePicker']");
		readonly By Occurrences = By.XPath("//*[@id='CWField_occurrences']");
		readonly By Endrangeid = By.XPath("//*[@id='CWField_endrangeid']");
		readonly By Lastappointmentdate = By.XPath("//*[@id='CWField_lastappointmentdate']");
		readonly By LastappointmentdateDatePicker = By.XPath("//*[@id='CWField_lastappointmentdate_DatePicker']");
		readonly By CWFieldButton_SelectAvailableSlots = By.XPath("//*[@id='CWFieldButton_SelectAvailableSlots']");
		readonly By LocationLookupButton = By.XPath("//*[@id='CWLookupBtn_providerid']");
		readonly By LocationLink = By.Id("CWField_providerid_Link");
		readonly By RoomLink = By.Id("CWField_providerroomid_Link");
		readonly By Ambulancerequired_1 = By.XPath("//*[@id='CWField_ambulancerequired_1']");
		readonly By Ambulancerequired_0 = By.XPath("//*[@id='CWField_ambulancerequired_0']");
		readonly By ProviderroomidLookupButton = By.XPath("//*[@id='CWLookupBtn_providerroomid']");
		readonly By Notes = By.XPath("//*[@id='CWField_notes']");
		readonly By AppointmentReason_FieldTitle = By.XPath("//*[@id='CWLabelHolder_healthappointmentreasonid']/label");

		public RecurringHealthAppointmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		public RecurringHealthAppointmentRecordPage WaitForRecurringHealthAppointmentRecordPageToLoad()
        {
			SwitchToDefaultFrame();


			WaitForElement(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElement(iframe_CWDialog);
			SwitchToIframe(iframe_CWDialog);

			WaitForElement(pageHeader);

			WaitForElement(SaveButton);
			WaitForElement(SaveAndCloseButton);

			WaitForElement(AppointmentReason_FieldTitle);

			return this;
        }

		public RecurringHealthAppointmentRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage WaitForRecordToBeSaved()
		{
			WaitForElement(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(AdditionalOptionToolbarButton);
			WaitForElement(RestrictAccessButton);
			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickHealthAppointmentReasonLookupButton()
		{
			WaitForElementToBeClickable(HealthappointmentreasonidLookupButton);
			Click(HealthappointmentreasonidLookupButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickCommunityAndClinicTeamLookupButton()
		{
			WaitForElementToBeClickable(CommunityandclinicteamidLookupButton);
			Click(CommunityandclinicteamidLookupButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickHomevisit_YesOption()
		{
			WaitForElementToBeClickable(Homevisit_1);
			Click(Homevisit_1);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateHomevisit_YesOptionChecked()
		{
			WaitForElement(Homevisit_1);
			ValidateElementChecked(Homevisit_1);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateHomevisit_YesOptionNotChecked()
		{
			WaitForElement(Homevisit_1);
			ValidateElementNotChecked(Homevisit_1);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickHomevisit_NoOption()
		{
			WaitForElementToBeClickable(Homevisit_0);
			Click(Homevisit_0);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateHomevisit_NoOptionChecked()
		{
			WaitForElement(Homevisit_0);
			ValidateElementChecked(Homevisit_0);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateHomevisit_NoOptionNotChecked()
		{
			WaitForElement(Homevisit_0);
			ValidateElementNotChecked(Homevisit_0);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickCaseLink()
		{
			WaitForElementToBeClickable(CaseidLink);
			Click(CaseidLink);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateCaseLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CaseidLink);
			ValidateElementText(CaseidLink, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickCaseClearButton()
		{
			WaitForElementToBeClickable(CaseidClearButton);
			Click(CaseidClearButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickCaseLookupButton()
		{
			WaitForElementToBeClickable(CaseidLookupButton);
			Click(CaseidLookupButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateTranslatorRequiredText(string ExpectedText)
		{
			ValidateElementValue(TranslatorRequired, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage InsertTextOnTranslatorRequired(string TextToInsert)
		{
			WaitForElementToBeClickable(TranslatorRequired);
			SendKeys(TranslatorRequired, TextToInsert);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickHealthAppointmentContactTypeLookupButton()
		{
			WaitForElementToBeClickable(HealthappointmentcontacttypeidLookupButton);
			Click(HealthappointmentcontacttypeidLookupButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickResponsibleTeamClearButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamClearButton);
			Click(ResponsibleTeamClearButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickResponsibleUserLink()
		{
			WaitForElementToBeClickable(ResponsibleuseridLink);
			Click(ResponsibleuseridLink);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateHealthAppointmentReasonLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(HealthAppointmentReasonLink);
			MoveToElementInPage(HealthAppointmentReasonLink);
			ValidateElementText(HealthAppointmentReasonLink, ExpectedText);

			return this;
		}
		
		public RecurringHealthAppointmentRecordPage ValidateContactTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ContactTypeLink);
			MoveToElementInPage(ContactTypeLink);
			ValidateElementText(ContactTypeLink, ExpectedText);

			return this;
		}


		public RecurringHealthAppointmentRecordPage ValidateResponsibleUserLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleuseridLink);
			ValidateElementText(ResponsibleuseridLink, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickResponsibleUserClearButton()
		{
			WaitForElementToBeClickable(ResponsibleuseridClearButton);
			Click(ResponsibleuseridClearButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickResponsibleUserLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleuseridLookupButton);
			Click(ResponsibleuseridLookupButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickGroupbooking_YesOption()
		{
			WaitForElementToBeClickable(Groupbooking_1);
			Click(Groupbooking_1);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateGroupbooking_YesOptionChecked()
		{
			WaitForElement(Groupbooking_1);
			ValidateElementChecked(Groupbooking_1);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateGroupbooking_YesOptionNotChecked()
		{
			WaitForElement(Groupbooking_1);
			ValidateElementNotChecked(Groupbooking_1);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickGroupbooking_NoOption()
		{
			WaitForElementToBeClickable(Groupbooking_0);
			Click(Groupbooking_0);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateGroupbooking_NoOptionChecked()
		{
			WaitForElement(Groupbooking_0);
			ValidateElementChecked(Groupbooking_0);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateGroupbooking_NoOptionNotChecked()
		{
			WaitForElement(Groupbooking_0);
			ValidateElementNotChecked(Groupbooking_0);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickPersonLink()
		{
			WaitForElementToBeClickable(PersonidLink);
			Click(PersonidLink);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidatePersonLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PersonidLink);
			ValidateElementText(PersonidLink, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickPersonLookupButton()
		{
			WaitForElementToBeClickable(PersonidLookupButton);
			Click(PersonidLookupButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidatePreferredLanguageText(string ExpectedText)
		{
			ValidateElementValue(PreferredLanguage, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage InsertTextOnPreferredLanguage(string TextToInsert)
		{
			WaitForElementToBeClickable(PreferredLanguage);
			SendKeys(PreferredLanguage, TextToInsert);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateStartTime(string ExpectedText)
		{
			ValidateElementValue(Starttime, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage InsertStartTime(string TextToInsert)
		{
			WaitForElementToBeClickable(Starttime);
			SendKeys(Starttime, TextToInsert);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickStartTime_TimePicker()
		{
			WaitForElementToBeClickable(Starttime_TimePicker);
			Click(Starttime_TimePicker);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateEndTimeText(string ExpectedText)
		{
			ValidateElementValue(Endtime, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage InsertEndTime(string TextToInsert)
		{
			WaitForElementToBeClickable(Endtime);
			SendKeys(Endtime, TextToInsert);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickEndTime_TimePicker()
		{
			WaitForElementToBeClickable(Endtime_TimePicker);
			Click(Endtime_TimePicker);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickRecurrencePatternLookupButton()
		{
			WaitForElementToBeClickable(RecurrencepatternidLookupButton);
			Click(RecurrencepatternidLookupButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateRecurrencePatternLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RecurrencePatternLink);
			MoveToElementInPage(RecurrencePatternLink);
			ValidateElementByTitle(RecurrencePatternLink, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateStartDateText(string ExpectedText)
		{
			WaitForElement(StartRange);
			MoveToElementInPage(StartRange);
			ValidateElementValue(StartRange, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage InsertStartRange(string TextToInsert)
		{
			WaitForElementToBeClickable(StartRange);
			SendKeys(StartRange, TextToInsert);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickStartDate_DatePicker()
		{
			WaitForElementToBeClickable(StartRangeDatePicker);
			Click(StartRangeDatePicker);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateFirstAppointmentDateText(string ExpectedText)
		{
			WaitForElement(Firstappointmentdate);
			MoveToElementInPage(Firstappointmentdate);
			ValidateElementValue(Firstappointmentdate, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage InsertFirstAppointmentDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Firstappointmentdate);
			SendKeys(Firstappointmentdate, TextToInsert);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickFirstAppointmentDate_DatePicker()
		{
			WaitForElementToBeClickable(FirstappointmentdateDatePicker);
			Click(FirstappointmentdateDatePicker);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateOccurrencesText(string ExpectedText)
		{
			WaitForElement(Occurrences);
			MoveToElementInPage(Occurrences);
			ValidateElementValue(Occurrences, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage InsertNumberOfOccurrences(string TextToInsert)
		{
			WaitForElementToBeClickable(Occurrences);
			SendKeys(Occurrences, TextToInsert);

			return this;
		}

		public RecurringHealthAppointmentRecordPage SelectEndRange(string TextToSelect)
		{
			WaitForElementToBeClickable(Endrangeid);
			SelectPicklistElementByText(Endrangeid, TextToSelect);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateEndRangeSelectedText(string ExpectedText)
		{
			WaitForElement(Endrangeid);
			MoveToElementInPage(Endrangeid);
			ValidatePicklistSelectedText(Endrangeid, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateLastAppointmentDateText(string ExpectedText)
		{
			WaitForElement(Lastappointmentdate);
			MoveToElementInPage(Lastappointmentdate);
			ValidateElementValue(Lastappointmentdate, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage InsertLastAppointmentDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Lastappointmentdate);
			SendKeys(Lastappointmentdate, TextToInsert);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickLastAppointmentDate_DatePicker()
		{
			WaitForElementToBeClickable(LastappointmentdateDatePicker);
			Click(LastappointmentdateDatePicker);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickSelectAvailableSlots()
		{
			WaitForElementToBeClickable(CWFieldButton_SelectAvailableSlots);
			Click(CWFieldButton_SelectAvailableSlots);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickLocationLookupButton()
		{
			WaitForElementToBeClickable(LocationLookupButton);
			Click(LocationLookupButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateLocationLinkText(string ExpectedText)
        {
			MoveToElementInPage(LocationLink);
			WaitForElementToBeClickable(LocationLink);
			ValidateElementByTitle(LocationLink, ExpectedText);

			return this;
        }

		public RecurringHealthAppointmentRecordPage ClickAmbulancerequired_YesOption()
		{
			WaitForElementToBeClickable(Ambulancerequired_1);
			Click(Ambulancerequired_1);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateAmbulancerequired_YesOptionChecked()
		{
			WaitForElement(Ambulancerequired_1);
			MoveToElementInPage(Ambulancerequired_1);
			ValidateElementChecked(Ambulancerequired_1);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateAmbulancerequired_YesOptionNotChecked()
		{
			WaitForElement(Ambulancerequired_1);
			MoveToElementInPage(Ambulancerequired_1);
			ValidateElementNotChecked(Ambulancerequired_1);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickAmbulancerequired_NoOption()
		{
			WaitForElementToBeClickable(Ambulancerequired_0);
			Click(Ambulancerequired_0);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateAmbulancerequired_NoOptionChecked()
		{
			WaitForElement(Ambulancerequired_0);
			MoveToElementInPage(Ambulancerequired_0);
			ValidateElementChecked(Ambulancerequired_0);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateAmbulancerequired_NoOptionNotChecked()
		{
			WaitForElement(Ambulancerequired_0);
			MoveToElementInPage(Ambulancerequired_0);
			ValidateElementNotChecked(Ambulancerequired_0);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ClickProviderRoomLookupButton()
		{
			WaitForElementToBeClickable(ProviderroomidLookupButton);
			Click(ProviderroomidLookupButton);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateRoomLinkText(string ExpectedText)
		{
			MoveToElementInPage(RoomLink);
			WaitForElementToBeClickable(RoomLink);
			ValidateElementByTitle(RoomLink, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage ValidateNotesText(string ExpectedText)
		{
			WaitForElement(Notes);
			MoveToElementInPage(Notes);
			ValidateElementText(Notes, ExpectedText);

			return this;
		}

		public RecurringHealthAppointmentRecordPage InsertNotes(string TextToInsert)
		{
			WaitForElementToBeClickable(Notes);
			SendKeys(Notes, TextToInsert);

			return this;
		}

	}
}
