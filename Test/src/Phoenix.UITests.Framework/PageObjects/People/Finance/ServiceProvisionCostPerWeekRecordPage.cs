using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Finance
{
	public class ServiceProvisionCostPerWeekRecordPage : CommonMethods
	{

		public ServiceProvisionCostPerWeekRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By CWContentIFrame = By.Id("CWContentIFrame");
		readonly By serviceProvisionCostPerWeekIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovisioncostperweek&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'

		readonly By MenuButton = By.Id("CWNavGroup_Menu");
		readonly By DetailsTab = By.Id("CWNavGroup_EditForm");
		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By RestrictAccessButton = By.XPath("//*[@id='TI_RestrictAccessButton']");
		readonly By ServiceProvisionLink = By.XPath("//*[@id='CWField_serviceprovisionid_Link']");
		readonly By ServiceProvisionLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceprovisionid']");
		readonly By StartDate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartDateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By CostPerWeek = By.XPath("//*[@id='CWField_costperweek']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By EndDate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EndDateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");

		public ServiceProvisionCostPerWeekRecordPage WaitForServiceProvisionCostPerWeekRecordPageToLoad()
		{
			driver.SwitchTo().DefaultContent();

			WaitForElement(CWContentIFrame);
			SwitchToIframe(CWContentIFrame);

			WaitForElement(serviceProvisionCostPerWeekIFrame);
			SwitchToIframe(serviceProvisionCostPerWeekIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 50);

            WaitForElement(BackButton);

            WaitForElement(MenuButton);            
            WaitForElement(DetailsTab);

            return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			MoveToElementInPage(BackButton);
			Click(BackButton);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			MoveToElementInPage(SaveButton);
			Click(SaveButton);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			MoveToElementInPage(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			MoveToElementInPage(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ClickRestrictAccessButton()
		{
			WaitForElementToBeClickable(RestrictAccessButton);
			MoveToElementInPage(RestrictAccessButton);
			Click(RestrictAccessButton);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ClickServiceProvisionLink()
		{
			WaitForElementToBeClickable(ServiceProvisionLink);
			MoveToElementInPage(ServiceProvisionLink);
			Click(ServiceProvisionLink);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ValidateServiceProvisionLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ServiceProvisionLink);
			MoveToElementInPage(ServiceProvisionLink);
			ValidateElementText(ServiceProvisionLink, ExpectedText);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ClickServiceProvisionLookupButton()
		{
			WaitForElementToBeClickable(ServiceProvisionLookupButton);
			MoveToElementInPage(ServiceProvisionLookupButton);
			Click(ServiceProvisionLookupButton);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ValidateStartDateText(string ExpectedText)
		{
			WaitForElementVisible(StartDate);
			MoveToElementInPage(StartDate);
			ValidateElementValue(StartDate, ExpectedText);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate);
			MoveToElementInPage(StartDate);
			SendKeys(StartDate, TextToInsert);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartDateDatePicker);
			MoveToElementInPage(StartDateDatePicker);
			Click(StartDateDatePicker);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ValidateCostPerWeekText(string ExpectedText)
		{
			MoveToElementInPage(CostPerWeek);
			ValidateElementValue(CostPerWeek, ExpectedText);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage InsertTextOnCostPerWeek(string TextToInsert)
		{
			WaitForElementToBeClickable(CostPerWeek);
			MoveToElementInPage(CostPerWeek);
			SendKeys(CostPerWeek, TextToInsert);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ValidateEndDateText(string ExpectedText)
		{
			WaitForElementVisible(EndDate);
			MoveToElementInPage(EndDate);
			ValidateElementValue(EndDate, ExpectedText);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage InsertTextOnEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate);
			SendKeys(EndDate, TextToInsert);

			return this;
		}

		public ServiceProvisionCostPerWeekRecordPage ClickEnddateDatePicker()
		{
			WaitForElementToBeClickable(EndDateDatePicker);
			MoveToElementInPage(EndDateDatePicker);
			Click(EndDateDatePicker);

			return this;
		}

	}
}
