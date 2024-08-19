using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Finance
{
	public class ServiceUprateRecordPage : CommonMethods
	{
		readonly By CWContentIFrame = By.Id("CWContentIFrame");
		readonly By ServiceUprateIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceuprate&')]");
		readonly By pageHeader = By.XPath("//h1");
		readonly By DeleteButton = By.XPath("//button[@title = 'Delete']");
		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By GenerateButton = By.XPath("//*[@id='TI_GenerateButton']");
		readonly By optionsToolbarMenu = By.Id("CWToolbarMenu");
		readonly By ProcessButton = By.XPath("//*[@id='TI_ProcessButton']");
		readonly By Historicratechange_1 = By.XPath("//*[@id='CWField_historicratechange_1']");
		readonly By Historicratechange_0 = By.XPath("//*[@id='CWField_historicratechange_0']");
		readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By Uprateratetypeid = By.XPath("//*[@id='CWField_uprateratetypeid']");
		readonly By Upratebankholidaytypeid = By.XPath("//*[@id='CWField_upratebankholidaytypeid']");
		readonly By Uprateperunittypeid = By.XPath("//*[@id='CWField_uprateperunittypeid']");
		readonly By Roundingoptionid = By.XPath("//*[@id='CWField_roundingoptionid']");
		readonly By Suspendrate_1 = By.XPath("//*[@id='CWField_suspendrate_1']");
		readonly By Suspendrate_0 = By.XPath("//*[@id='CWField_suspendrate_0']");
		readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EnddateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
		readonly By Uprateratevalue = By.XPath("//*[@id='CWField_uprateratevalue']");
		readonly By Upratebankholidayvalue = By.XPath("//*[@id='CWField_upratebankholidayvalue']");
		readonly By Uprateperunitvalue = By.XPath("//*[@id='CWField_uprateperunitvalue']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By Serviceupratenumber = By.XPath("//*[@id='CWField_serviceupratenumber']");
		readonly By Serviceratetypeid = By.XPath("//*[@id='CWField_serviceratetypeid']");
		readonly By Contracttypeid = By.XPath("//*[@id='CWField_contracttypeid']");
		readonly By Serviceelement1idLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceelement1id']");
		readonly By Serviceelement3idLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceelement3id']");
		readonly By ProvideridLookupButton = By.XPath("//*[@id='CWLookupBtn_providerid']");
		readonly By Statusid = By.XPath("//*[@id='CWField_statusid']");
		readonly By RateunitidLookupButton = By.XPath("//*[@id='CWLookupBtn_rateunitid']");
		readonly By Serviceelement2idLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceelement2id']");
		readonly By ClientcategoryidLookupButton = By.XPath("//*[@id='CWLookupBtn_clientcategoryid']");
		readonly By ServiceprovisionidLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceprovisionid']");
		readonly By Notes = By.XPath("//*[@id='CWField_notes']");

		public ServiceUprateRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		public ServiceUprateRecordPage WaitForServiceUprateRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElement(CWContentIFrame);
			SwitchToIframe(CWContentIFrame);

			WaitForElement(ServiceUprateIFrame);
			SwitchToIframe(ServiceUprateIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 60);

			WaitForElement(pageHeader);

			WaitForElement(SaveButton);
			WaitForElement(SaveAndCloseButton);

			return this;
		}

		public ServiceUprateRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public ServiceUprateRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public ServiceUprateRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public ServiceUprateRecordPage ClickGenerateButton()
		{
			WaitForElementToBeClickable(GenerateButton);
			Click(GenerateButton);

			return this;
		}

		public ServiceUprateRecordPage ClickProcessButton()
		{
			WaitForElementToBeClickable(optionsToolbarMenu);
			MoveToElementInPage(optionsToolbarMenu);
			Click(optionsToolbarMenu);

			WaitForElementToBeClickable(ProcessButton);
			MoveToElementInPage(ProcessButton);
			Click(ProcessButton);

			return this;
		}

		public ServiceUprateRecordPage ClickHistoricratechange_YesButton()
		{
			WaitForElementToBeClickable(Historicratechange_1);
			Click(Historicratechange_1);

			return this;
		}

		public ServiceUprateRecordPage ValidateHistoricratechange_YesChecked()
		{
			WaitForElement(Historicratechange_1);
			ValidateElementChecked(Historicratechange_1);
			
			return this;
		}

		public ServiceUprateRecordPage ValidateHistoricratechange_YesNotChecked()
		{
			WaitForElement(Historicratechange_1);
			ValidateElementNotChecked(Historicratechange_1);
			
			return this;
		}

		public ServiceUprateRecordPage ClickHistoricratechange_No()
		{
			WaitForElementToBeClickable(Historicratechange_0);
			Click(Historicratechange_0);

			return this;
		}

		public ServiceUprateRecordPage ValidateHistoricratechange_NoChecked()
		{
			WaitForElementVisible(Historicratechange_0);
			ValidateElementChecked(Historicratechange_0);
			
			return this;
		}

		public ServiceUprateRecordPage ValidateHistoricratechange_NoNotChecked()
		{
			WaitForElementVisible(Historicratechange_0);
			ValidateElementNotChecked(Historicratechange_0);
			
			return this;
		}

		public ServiceUprateRecordPage ValidateStartDateText(string ExpectedText)
		{
			WaitForElementVisible(Startdate);
			ValidateElementValue(Startdate, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Startdate);
			SendKeys(Startdate, TextToInsert);
			
			return this;
		}

		public ServiceUprateRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartdateDatePicker);
			Click(StartdateDatePicker);

			return this;
		}

		public ServiceUprateRecordPage SelectUprateRatetype(string TextToSelect)
		{
			WaitForElementToBeClickable(Uprateratetypeid);
			SelectPicklistElementByText(Uprateratetypeid, TextToSelect);

			return this;
		}

		public ServiceUprateRecordPage ValidateUprateratetypeSelectedText(string ExpectedText)
		{
			WaitForElementVisible(Uprateratetypeid);
			ValidateElementText(Uprateratetypeid, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage SelectUpratebankholidaytype(string TextToSelect)
		{
			WaitForElementToBeClickable(Upratebankholidaytypeid);
			SelectPicklistElementByText(Upratebankholidaytypeid, TextToSelect);

			return this;
		}

		public ServiceUprateRecordPage ValidateUpratebankholidaytypeSelectedText(string ExpectedText)
		{
			WaitForElementVisible(Upratebankholidaytypeid);
			ValidateElementText(Upratebankholidaytypeid, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage SelectUprateperunittypeid(string TextToSelect)
		{
			WaitForElementToBeClickable(Uprateperunittypeid);
			SelectPicklistElementByText(Uprateperunittypeid, TextToSelect);

			return this;
		}

		public ServiceUprateRecordPage ValidateUprateperunittypeidSelectedText(string ExpectedText)
		{
			WaitForElementVisible(Uprateperunittypeid);
			ValidateElementText(Uprateperunittypeid, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage SelectRoundingoptionid(string TextToSelect)
		{
			WaitForElementToBeClickable(Roundingoptionid);
			SelectPicklistElementByText(Roundingoptionid, TextToSelect);

			return this;
		}

		public ServiceUprateRecordPage ValidateRoundingoptionidSelectedText(string ExpectedText)
		{
			WaitForElementVisible(Roundingoptionid);
			ValidateElementText(Roundingoptionid, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage ClickSuspendrate_YesButton()
		{
			WaitForElementToBeClickable(Suspendrate_1);
			Click(Suspendrate_1);

			return this;
		}

		public ServiceUprateRecordPage ValidateSuspendrate_YesChecked()
		{
			WaitForElementVisible(Suspendrate_1);
			ValidateElementChecked(Suspendrate_1);
			
			return this;
		}

		public ServiceUprateRecordPage ValidateSuspendrate_YesNotChecked()
		{
			WaitForElementVisible(Suspendrate_1);
			ValidateElementNotChecked(Suspendrate_1);
			
			return this;
		}

		public ServiceUprateRecordPage ClickSuspendrate_No()
		{
			WaitForElementToBeClickable(Suspendrate_0);
			Click(Suspendrate_0);

			return this;
		}

		public ServiceUprateRecordPage ValidateSuspendrate_NoChecked()
		{
			WaitForElementVisible(Suspendrate_0);
			ValidateElementChecked(Suspendrate_0);
			
			return this;
		}

		public ServiceUprateRecordPage ValidateSuspendRate_NoNotChecked()
		{
			WaitForElementVisible(Suspendrate_0);
			ValidateElementNotChecked(Suspendrate_0);
			
			return this;
		}

		public ServiceUprateRecordPage ValidateEndDateText(string ExpectedText)
		{
			WaitForElementVisible(Enddate);
			ValidateElementValue(Enddate, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage InsertTextOnEnddate(string TextToInsert)
		{
			WaitForElementToBeClickable(Enddate);
			SendKeys(Enddate, TextToInsert);
			
			return this;
		}

		public ServiceUprateRecordPage ClickEnddateDatePicker()
		{
			WaitForElementToBeClickable(EnddateDatePicker);
			Click(EnddateDatePicker);

			return this;
		}

		public ServiceUprateRecordPage ValidateUprateratevalueText(string ExpectedText)
		{
			WaitForElementVisible(Uprateperunitvalue);
			ValidateElementValue(Uprateratevalue, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage InsertTextOnUprateratevalue(string TextToInsert)
		{
			WaitForElementToBeClickable(Uprateratevalue);
			SendKeys(Uprateratevalue, TextToInsert);
			
			return this;
		}

		public ServiceUprateRecordPage ValidateUpratebankholidayvalueText(string ExpectedText)
		{
			WaitForElementVisible(Upratebankholidayvalue);
			ValidateElementValue(Upratebankholidayvalue, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage InsertTextOnUpratebankholidayvalue(string TextToInsert)
		{
			WaitForElementToBeClickable(Upratebankholidayvalue);
			SendKeys(Upratebankholidayvalue, TextToInsert);
			
			return this;
		}

		public ServiceUprateRecordPage ValidateUprateperunitvalueText(string ExpectedText)
		{
			WaitForElementVisible(Uprateperunitvalue);
			ValidateElementValue(Uprateperunitvalue, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage InsertTextOnUprateperunitvalue(string TextToInsert)
		{
			WaitForElementToBeClickable(Uprateperunitvalue);
			SendKeys(Uprateperunitvalue, TextToInsert);
			
			return this;
		}

		public ServiceUprateRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public ServiceUprateRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage ClickResponsibleTeamClearButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamClearButton);
			Click(ResponsibleTeamClearButton);

			return this;
		}

		public ServiceUprateRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public ServiceUprateRecordPage ValidateServiceupratenumberText(string ExpectedText)
		{
			WaitForElementToBeClickable(Serviceupratenumber);
			ValidateElementValue(Serviceupratenumber, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage InsertTextOnServiceupratenumber(string TextToInsert)
		{
			WaitForElementToBeClickable(Serviceupratenumber);
			SendKeys(Serviceupratenumber, TextToInsert);
			
			return this;
		}

		public ServiceUprateRecordPage SelectServiceratetypeid(string TextToSelect)
		{
			WaitForElementToBeClickable(Serviceratetypeid);
			SelectPicklistElementByText(Serviceratetypeid, TextToSelect);

			return this;
		}

		public ServiceUprateRecordPage ValidateServiceratetypeidSelectedText(string ExpectedText)
		{
			WaitForElementToBeClickable(Serviceratetypeid);
			ValidateElementText(Serviceratetypeid, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage SelectContracttypeid(string TextToSelect)
		{
			WaitForElementToBeClickable(Contracttypeid);
			SelectPicklistElementByText(Contracttypeid, TextToSelect);

			return this;
		}

		public ServiceUprateRecordPage ValidateContracttypeidSelectedText(string ExpectedText)
		{
			WaitForElementToBeClickable(Contracttypeid);
			ValidateElementText(Contracttypeid, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage ClickServiceelement1idLookupButton()
		{
			WaitForElementToBeClickable(Serviceelement1idLookupButton);
			Click(Serviceelement1idLookupButton);

			return this;
		}

		public ServiceUprateRecordPage ClickServiceelement3idLookupButton()
		{
			WaitForElementToBeClickable(Serviceelement3idLookupButton);
			Click(Serviceelement3idLookupButton);

			return this;
		}

		public ServiceUprateRecordPage ClickProvideridLookupButton()
		{
			WaitForElementToBeClickable(ProvideridLookupButton);
			Click(ProvideridLookupButton);

			return this;
		}

		public ServiceUprateRecordPage SelectStatusid(string TextToSelect)
		{
			WaitForElementToBeClickable(Statusid);
			SelectPicklistElementByText(Statusid, TextToSelect);

			return this;
		}

		public ServiceUprateRecordPage ValidateStatusSelectedText(string ExpectedText)
		{
			WaitForElementVisible(Statusid);
			ValidatePicklistSelectedText(Statusid, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage ClickRateunitidLookupButton()
		{
			WaitForElementToBeClickable(RateunitidLookupButton);
			Click(RateunitidLookupButton);

			return this;
		}

		public ServiceUprateRecordPage ClickServiceelement2idLookupButton()
		{
			WaitForElementToBeClickable(Serviceelement2idLookupButton);
			Click(Serviceelement2idLookupButton);

			return this;
		}

		public ServiceUprateRecordPage ClickClientcategoryidLookupButton()
		{
			WaitForElementToBeClickable(ClientcategoryidLookupButton);
			Click(ClientcategoryidLookupButton);

			return this;
		}

		public ServiceUprateRecordPage ClickServiceprovisionidLookupButton()
		{
			WaitForElementToBeClickable(ServiceprovisionidLookupButton);
			Click(ServiceprovisionidLookupButton);

			return this;
		}

		public ServiceUprateRecordPage ValidateNotesText(string ExpectedText)
		{
			WaitForElementToBeClickable(Notes);
			ValidateElementText(Notes, ExpectedText);

			return this;
		}

		public ServiceUprateRecordPage InsertTextOnNotes(string TextToInsert)
		{
			WaitForElementToBeClickable(Notes);
			SendKeys(Notes, TextToInsert);
			
			return this;
		}

	}
}
