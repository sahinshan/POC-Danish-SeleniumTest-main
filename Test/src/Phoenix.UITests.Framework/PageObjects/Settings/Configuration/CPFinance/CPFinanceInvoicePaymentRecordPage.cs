using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.Finance;
using Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinance;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class CPFinanceInvoicePaymentRecordPage : CommonMethods
	{

        #region IFrame Locators

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=careproviderfinanceinvoicepayment&')]");
		By cwDiaglogIframe_Invoice(string RecordId) => By.XPath("//iframe[contains(@id, 'iframe_CWDialog_"+RecordId+"')]");

        #endregion


        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By AllocateButton = By.XPath("//*[@id='TI_Allocate']");
		readonly By CareproviderFinanceInvoiceLink = By.XPath("//*[@id='CWField_careproviderfinanceinvoiceid_Link']");
		readonly By CareproviderFinanceInvoiceLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderfinanceinvoiceid']");
		readonly By EstablishmentLink = By.XPath("//*[@id='CWField_establishmentid_Link']");
		readonly By EstablishmentLookupButton = By.XPath("//*[@id='CWLookupBtn_establishmentid']");
		readonly By InvoiceNumber = By.XPath("//*[@id='CWField_invoicenumber']");
		readonly By PaymentDate = By.XPath("//*[@id='CWField_paymentdate']");
		readonly By PaymentDate_DatePicker = By.XPath("//*[@id='CWField_paymentdate_DatePicker']");
		readonly By PaymentAmount = By.XPath("//*[@id='CWField_paymentamount']");
		readonly By PaymentMethodLink = By.XPath("//*[@id='CWField_paymentmethodid_Link']");
		readonly By PaymentMethodClearButton = By.XPath("//*[@id='CWClearLookup_paymentmethodid']");
		readonly By PaymentMethodLookupButton = By.XPath("//*[@id='CWLookupBtn_paymentmethodid']");
		readonly By Totalrecord_YesOption = By.XPath("//*[@id='CWField_totalrecord_1']");
		readonly By Totalrecord_NoOption = By.XPath("//*[@id='CWField_totalrecord_0']");
		readonly By CareProviderFinanceInvoicePaymentReportTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderfinanceinvoicepaymentreporttypeid']");
		readonly By Reference = By.XPath("//*[@id='CWField_reference']");
		readonly By ReportType_Link = By.XPath("//*[@id = 'CWField_careproviderfinanceinvoicepaymentreporttypeid_Link']");
		readonly By FinanceInvoicePayment_AllocatedTo_Link = By.XPath("//*[@id = 'CWField_allocatedfinanceinvoicepaymentid_Link']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By ProviderOrPersonLink = By.XPath("//*[@id='CWField_providerorpersonid_Link']");
		readonly By ProviderOrPersonLookupButton = By.XPath("//*[@id='CWLookupBtn_providerorpersonid']");
		readonly By AlternativeInvoiceNumber = By.XPath("//*[@id='CWField_alternativeinvoicenumber']");
		readonly By PostedDate = By.XPath("//*[@id='CWField_posteddate']");
		readonly By PostedDate_DatePicker = By.XPath("//*[@id='CWField_posteddate_DatePicker']");
		readonly By DebtWrittenOff = By.XPath("//*[@id='CWField_debtwrittenoff']");
		readonly By PaidBy = By.XPath("//*[@id='CWField_paidby']");
		readonly By Allocated_YesOption = By.XPath("//*[@id='CWField_allocated_1']");
		readonly By Allocated_NoOption = By.XPath("//*[@id='CWField_allocated_0']");
		readonly By AllocatedFinanceInvoicePaymentLink = By.XPath("//*[@id='CWField_allocatedfinanceinvoicepaymentid_Link']");
		readonly By AllocatedFinanceInvoicePaymentLookupButton = By.XPath("//*[@id='CWLookupBtn_allocatedfinanceinvoicepaymentid']");
        By MandatoryField_Label(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']/span[@class = 'mandatory']");

		readonly By MenuButton = By.XPath("//*[@id ='CWNavGroup_Menu']/button");
		readonly By LeftNavigationMenu = By.XPath("//*[@id = 'left-nav']");

        readonly By activitiesDetailsElementExpanded = By.XPath("//span[text()='Activities']/parent::div/parent::summary/parent::details[@open]");
        readonly By activitiesLeftSubMenu = By.XPath("//details/summary/div/span[text()='Activities']");

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");

        readonly By TasksSubMenuItemButton = By.Id("CWNavItem_Tasks");
		readonly By AttachmentsSubMenuItemButton = By.Id("CWNavItem_Attachments");

		readonly By AllocationsTab = By.XPath("//*[@id = 'CWNavGroup_Allocations']");

        public CPFinanceInvoicePaymentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        public CPFinanceInvoicePaymentRecordPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);


            return this;
        }

        public CPFinanceInvoicePaymentRecordPage WaitForPageToLoad(string RecordId)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(cwDiaglogIframe_Invoice(RecordId));
            SwitchToIframe(cwDiaglogIframe_Invoice(RecordId));

            WaitForElementNotVisible("CWRefreshPanel", 20);


            return this;
        }

        public CPFinanceInvoicePaymentRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);

            return this;
        }

        public CPFinanceInvoicePaymentRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickAllocateButton()
		{
            WaitForElement(AllocateButton);
			ScrollToElement(AllocateButton);
            Click(AllocateButton);

            return this;
        }

		//AllocateButton is visible or not visible
		public CPFinanceInvoicePaymentRecordPage ValidateAllocateButtonIsDisplayed(bool IsDisplayed = true)
		{
            if (IsDisplayed)
                WaitForElementVisible(AllocateButton);
            else
                WaitForElementNotVisible(AllocateButton, 3);

            return this;
        }

		public CPFinanceInvoicePaymentRecordPage ClickCareproviderFinanceInvoiceLink()
		{
			WaitForElementToBeClickable(CareproviderFinanceInvoiceLink);
			Click(CareproviderFinanceInvoiceLink);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateCareproviderFinanceInvoiceLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CareproviderFinanceInvoiceLink);
			ValidateElementText(CareproviderFinanceInvoiceLink, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickCareproviderFinanceInvoiceLookupButton()
		{
			WaitForElementToBeClickable(CareproviderFinanceInvoiceLookupButton);
			Click(CareproviderFinanceInvoiceLookupButton);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickEstablishmentLink()
		{
			WaitForElementToBeClickable(EstablishmentLink);
			Click(EstablishmentLink);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateEstablishmentLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(EstablishmentLink);
			ValidateElementText(EstablishmentLink, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickEstablishmentLookupButton()
		{
			WaitForElementToBeClickable(EstablishmentLookupButton);
			Click(EstablishmentLookupButton);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateInvoiceNumberText(string ExpectedText)
		{
			ValidateElementValue(InvoiceNumber, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage InsertTextOnInvoiceNumber(string TextToInsert)
		{
			WaitForElementToBeClickable(InvoiceNumber);
			SendKeys(InvoiceNumber, TextToInsert);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidatePaymentDateText(string ExpectedText)
		{
			ValidateElementValue(PaymentDate, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage InsertTextOnPaymentDate(string TextToInsert)
		{
			WaitForElementToBeClickable(PaymentDate);
			SendKeys(PaymentDate, TextToInsert);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickPaymentDate_DatePicker()
		{
			WaitForElementToBeClickable(PaymentDate_DatePicker);
			Click(PaymentDate_DatePicker);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidatePaymentAmountText(string ExpectedText)
		{
			WaitForElementVisible(PaymentAmount);
			ScrollToElement(PaymentAmount);
			string value = GetElementByAttributeValue(PaymentAmount, "value");
			Assert.AreEqual(ExpectedText, value);

			return this;
		}

		//Validate PaymentAmount attribute value
		public CPFinanceInvoicePaymentRecordPage ValidatePaymentAmountAttributeValue(string AttributeName, string ExpectedValue)
		{
            WaitForElementVisible(PaymentAmount);
			ValidateElementAttribute(PaymentAmount, AttributeName, ExpectedValue);

            return this;
        }


		public CPFinanceInvoicePaymentRecordPage InsertTextOnPaymentAmount(string TextToInsert)
		{
			WaitForElementToBeClickable(PaymentAmount);
			ScrollToElement(PaymentAmount);
			SendKeys(PaymentAmount, TextToInsert);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickPaymentMethodLink()
		{
			WaitForElementToBeClickable(PaymentMethodLink);
			Click(PaymentMethodLink);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidatePaymentMethodLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PaymentMethodLink);
			ValidateElementText(PaymentMethodLink, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickPaymentMethodClearButton()
		{
			WaitForElementToBeClickable(PaymentMethodClearButton);
			Click(PaymentMethodClearButton);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickPaymentMethodLookupButton()
		{
			WaitForElementToBeClickable(PaymentMethodLookupButton);
			Click(PaymentMethodLookupButton);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickTotalrecord_YesOption()
		{
			WaitForElementToBeClickable(Totalrecord_YesOption);
			Click(Totalrecord_YesOption);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateTotalrecord_YesOptionChecked()
		{
			WaitForElement(Totalrecord_YesOption);
			ValidateElementChecked(Totalrecord_YesOption);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateTotalrecord_YesOptionNotChecked()
		{
			WaitForElement(Totalrecord_YesOption);
			ValidateElementNotChecked(Totalrecord_YesOption);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickTotalrecord_NoOption()
		{
			WaitForElementToBeClickable(Totalrecord_NoOption);
			Click(Totalrecord_NoOption);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateTotalrecord_NoOptionChecked()
		{
			WaitForElement(Totalrecord_NoOption);
			ValidateElementChecked(Totalrecord_NoOption);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateTotalrecord_NoOptionNotChecked()
		{
			WaitForElement(Totalrecord_NoOption);
			ValidateElementNotChecked(Totalrecord_NoOption);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickCareProviderFinanceInvoicePaymentReportTypeLookupButton()
		{
			WaitForElementToBeClickable(CareProviderFinanceInvoicePaymentReportTypeLookupButton);
			Click(CareProviderFinanceInvoicePaymentReportTypeLookupButton);

			return this;
		}

        //verify ReportType_Link text
		public CPFinanceInvoicePaymentRecordPage ValidateReportTypeLinkText(string ExpectedText)
		{
			WaitForElement(ReportType_Link);
			if(GetElementVisibility(ReportType_Link))
				ScrollToElement(ReportType_Link);
            ValidateElementText(ReportType_Link, ExpectedText);

            return this;
		}

        public CPFinanceInvoicePaymentRecordPage ValidateReferenceText(string ExpectedText)
		{
			ValidateElementValue(Reference, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage InsertTextOnReference(string TextToInsert)
		{
			WaitForElementToBeClickable(Reference);
			SendKeys(Reference, TextToInsert);
			
			return this;
		}

        //verify FinanceInvoicePayment_AllocatedTo_Link text
		public CPFinanceInvoicePaymentRecordPage ValidateFinanceInvoicePayment_AllocatedTo_LinkText(string ExpectedText)
		{
			WaitForElement(FinanceInvoicePayment_AllocatedTo_Link);
			ScrollToElement(FinanceInvoicePayment_AllocatedTo_Link);
            ValidateElementByTitle(FinanceInvoicePayment_AllocatedTo_Link, ExpectedText);

            return this;
		}

        public CPFinanceInvoicePaymentRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickProviderOrPersonidLink()
		{
			WaitForElementToBeClickable(ProviderOrPersonLink);
			Click(ProviderOrPersonLink);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidatePayerLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ProviderOrPersonLink);
			ValidateElementText(ProviderOrPersonLink, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickProviderOrPersonLookupButton()
		{
			WaitForElementToBeClickable(ProviderOrPersonLookupButton);
			Click(ProviderOrPersonLookupButton);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateAlternativeInvoiceNumberText(string ExpectedText)
		{
			ValidateElementValue(AlternativeInvoiceNumber, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage InsertTextOnAlternativeInvoiceNumber(string TextToInsert)
		{
			WaitForElementToBeClickable(AlternativeInvoiceNumber);
			SendKeys(AlternativeInvoiceNumber, TextToInsert);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidatePostedDateText(string ExpectedText)
		{
			ValidateElementValue(PostedDate, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage InsertTextOnPostedDate(string TextToInsert)
		{
			WaitForElementToBeClickable(PostedDate);
			SendKeys(PostedDate, TextToInsert);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickPostedDate_DatePicker()
		{
			WaitForElementToBeClickable(PostedDate_DatePicker);
			Click(PostedDate_DatePicker);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateDebtWrittenOffText(string ExpectedText)
		{
			WaitForElementVisible(DebtWrittenOff);
			ScrollToElement(DebtWrittenOff);
			ValidateElementValue(DebtWrittenOff, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage InsertTextOnDebtWrittenOff(string TextToInsert)
		{
			WaitForElementToBeClickable(DebtWrittenOff);
			SendKeys(DebtWrittenOff, TextToInsert);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidatePaidByText(string ExpectedText)
		{
			ValidateElementValue(PaidBy, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage InsertTextOnPaidBy(string TextToInsert)
		{
			WaitForElementToBeClickable(PaidBy);
			SendKeys(PaidBy, TextToInsert);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickAllocated_Yes()
		{
			WaitForElementToBeClickable(Allocated_YesOption);
			Click(Allocated_YesOption);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateAllocated_YesOptionChecked()
		{
			WaitForElement(Allocated_YesOption);
			ValidateElementChecked(Allocated_YesOption);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateAllocated_YesOptionNotChecked()
		{
			WaitForElement(Allocated_YesOption);
			ValidateElementNotChecked(Allocated_YesOption);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickAllocated_No()
		{
			WaitForElementToBeClickable(Allocated_NoOption);
			Click(Allocated_NoOption);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateAllocated_NoOptionChecked()
		{
			WaitForElement(Allocated_NoOption);
			ValidateElementChecked(Allocated_NoOption);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateAllocated_NoOptionNotChecked()
		{
			WaitForElement(Allocated_NoOption);
			ValidateElementNotChecked(Allocated_NoOption);
			
			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickAllocatedFinanceInvoicePaymentLink()
		{
			WaitForElementToBeClickable(AllocatedFinanceInvoicePaymentLink);
			Click(AllocatedFinanceInvoicePaymentLink);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ValidateAllocatedFinanceInvoicePaymentLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(AllocatedFinanceInvoicePaymentLink);
			ValidateElementText(AllocatedFinanceInvoicePaymentLink, ExpectedText);

			return this;
		}

		public CPFinanceInvoicePaymentRecordPage ClickAllocatedFinanceInvoicePaymentLookupButton()
		{
			WaitForElementToBeClickable(AllocatedFinanceInvoicePaymentLookupButton);
			Click(AllocatedFinanceInvoicePaymentLookupButton);

			return this;
		}

        //Validate CareproviderFinanceInvoiceLookupButton is displayed or not displayed by accepting a boolean flag for IsDisplayed
		public CPFinanceInvoicePaymentRecordPage ValidateCareproviderFinanceInvoiceLookupButtonDisplayed(bool IsDisplayed = true)
		{
            if (IsDisplayed)
                WaitForElementVisible(CareproviderFinanceInvoiceLookupButton);
            else
                WaitForElementNotVisible(CareproviderFinanceInvoiceLookupButton, 3);

            Assert.AreEqual(IsDisplayed, GetElementVisibility(CareproviderFinanceInvoiceLookupButton));

            return this;
        }

		//Validate EstablishmentLookupButton is displayed or not displayed by accepting a boolean flag for IsDisplayed
		public CPFinanceInvoicePaymentRecordPage ValidateEstablishmentLookupButtonDisplayed(bool IsDisplayed = true)
		{
            if (IsDisplayed)
                WaitForElementVisible(EstablishmentLookupButton);
            else
                WaitForElementNotVisible(EstablishmentLookupButton, 3);

            return this;
		}

		//Validate PaymentMethodLookupButton is displayed or not displayed by accepting a boolean flag for IsDisplayed
		public CPFinanceInvoicePaymentRecordPage ValidatePaymentMethodLookupButtonDisplayed(bool IsDisplayed = true)
		{
			if (IsDisplayed)
                WaitForElementVisible(PaymentMethodLookupButton);
            else
                WaitForElementNotVisible(PaymentMethodLookupButton, 3);

			return this;
		}

		//Validate InvoiceNumber field is displayed or not displayed by accepting a boolean flag for IsDisplayed
		public CPFinanceInvoicePaymentRecordPage ValidateInvoiceNumberDisplayed(bool IsDisplayed = true)
		{
			if(IsDisplayed)
                WaitForElementVisible(InvoiceNumber);
            else
                WaitForElementNotVisible(InvoiceNumber, 3);

			return this;
		}

		//Validate PaymentDate field is displayed or not displayed by accepting a boolean flag for IsDisplayed
		public CPFinanceInvoicePaymentRecordPage ValidatePaymentDateDisplayed(bool IsDisplayed = true)
		{
			if(IsDisplayed)
                WaitForElementVisible(PaymentDate);
            else
                WaitForElementNotVisible(PaymentDate, 3);

			return this;
		}

		//Method Validate PaymentAmount field is displayed or not displayed by accepting a boolean flag for IsDisplayed, return this object
		public CPFinanceInvoicePaymentRecordPage ValidatePaymentAmountDisplayed(bool IsDisplayed = true)
		{
            if (IsDisplayed)
                WaitForElementVisible(PaymentAmount);
            else
                WaitForElementNotVisible(PaymentAmount, 3);

            return this;
        }

		//Method to Validate Totalrecord_YesOption and Totalrecord_NoOption is displayed or not displayed by accepting a boolean flag for IsDisplayed
		//Return this object at the end
		public CPFinanceInvoicePaymentRecordPage ValidateTotalRecordOptionsDisplayed(bool IsDisplayed = true)
		{
			ScrollToElement(Totalrecord_YesOption);
			if (IsDisplayed)
			{
                WaitForElementVisible(Totalrecord_YesOption);
                WaitForElementVisible(Totalrecord_NoOption);
            }
            else
			{
                WaitForElementNotVisible(Totalrecord_YesOption, 3);
                WaitForElementNotVisible(Totalrecord_NoOption, 3);
            }

            return this;
		}

		//Method to validate if ResponsibleTeam Lookup is displayed or not
		public CPFinanceInvoicePaymentRecordPage ValidateResponsibleTeamLookupButtonDisplayed(bool IsDisplayed = true)
		{
			if(IsDisplayed)
				WaitForElementVisible(ResponsibleTeamLookupButton);
            else
                WaitForElementNotVisible(ResponsibleTeamLookupButton, 3);

			return this;
		}

		//Method to validate if Payer ProviderOrPerson Lookup is displayed or not
		public CPFinanceInvoicePaymentRecordPage ValidateProviderOrPersonLookupButtonDisplayed(bool IsDisplayed = true)
		{
			if(IsDisplayed)
				WaitForElementVisible(ProviderOrPersonLookupButton);
            else
                WaitForElementNotVisible(ProviderOrPersonLookupButton, 3);

			return this;
		}

		//Method to validate if AlternativeInvoiceNumber field is displayed or not displayed
		public CPFinanceInvoicePaymentRecordPage ValidateAlternativeInvoiceNumberDisplayed(bool IsDisplayed = true)
		{
            if(IsDisplayed)
                WaitForElementVisible(AlternativeInvoiceNumber);
            else
                WaitForElementNotVisible(AlternativeInvoiceNumber, 3);

            return this;
        }

		//Method to validate if PostedDate field is displayed or not displayed
		public CPFinanceInvoicePaymentRecordPage ValidatePostedDateDisplayed(bool IsDisplayed = true)
		{
			if(IsDisplayed)
                WaitForElementVisible(PostedDate);
            else
                WaitForElementNotVisible(PostedDate, 3);

			return this;
		}

		//Method to validate if DebtWrittenOff field is displayed or not displayed
		public CPFinanceInvoicePaymentRecordPage ValidateDebtWrittenOffDisplayed(bool IsDisplayed = true)
		{
			if(IsDisplayed)
                WaitForElementVisible(DebtWrittenOff);
            else
                WaitForElementNotVisible(DebtWrittenOff, 3);
			
			return this;
		}

		//Method to validate if PaidBy field is displayed or not displayed
		public CPFinanceInvoicePaymentRecordPage ValidatePaidByDisplayed(bool IsDisplayed = true)
		{
			if(IsDisplayed)
                WaitForElementVisible(PaidBy);
            else
                WaitForElementNotVisible(PaidBy, 3);

            return this;
		}

		//Method to validate if Allocated_YesOption and Allocated_NoOption is displayed or not displayed
		public CPFinanceInvoicePaymentRecordPage ValidateAllocatedOptionsDisplayed(bool IsDisplayed = true)
		{
			ScrollToElement(Allocated_YesOption);
            if (IsDisplayed)
			{
                WaitForElementVisible(Allocated_YesOption);
                WaitForElementVisible(Allocated_NoOption);
            }
            else
			{
                WaitForElementNotVisible(Allocated_YesOption, 3);
                WaitForElementNotVisible(Allocated_NoOption, 3);
            }

            return this;
		}

		//Method to validate if Reference field is displayed or not displayed
		public CPFinanceInvoicePaymentRecordPage ValidateReferenceFieldIsDisplayed(bool IsDisplayed = true)
		{
			if(IsDisplayed)
                WaitForElementVisible(Reference);
            else
                WaitForElementNotVisible(Reference, 3);

            return this;
		}

        public CPFinanceInvoicePaymentRecordPage ValidateMandatoryFieldIsDisplayed(string FieldName, bool ExpectedMandatory = true)
        {
            if (ExpectedMandatory)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 2);

            return this;
        }

        //Method to validate if Finance Invoice Lookup Button is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidateCareProviderFinanceInvoiceLookupButtonDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
                ValidateElementDisabled(CareproviderFinanceInvoiceLookupButton);
            else
                ValidateElementNotDisabled(CareproviderFinanceInvoiceLookupButton);

            return this;
        }

		//Method to validate if Establishment Lookup Button is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidateEstablishmentLookupButtonDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
                ValidateElementDisabled(EstablishmentLookupButton);
            else
                ValidateElementNotDisabled(EstablishmentLookupButton);

            return this;
        }

		//Method to validate if Invoice Number is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidateInvoiceNumberDisabled(bool IsDisabled = true)
		{
			if (IsDisabled)
                ValidateElementDisabled(InvoiceNumber);
            else
                ValidateElementNotDisabled(InvoiceNumber);

            return this;
		}

		//Method to validate if Payment Date is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidatePaymentDateDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
                ValidateElementDisabled(PaymentDate);
            else
                ValidateElementNotDisabled(PaymentDate);

            return this;
        }

		//Method to validate if Payment Amount is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidatePaymentAmountDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
                ValidateElementDisabled(PaymentAmount);
            else
                ValidateElementNotDisabled(PaymentAmount);

            return this;
        }

		//Method to validate if Payment Method Lookup Button is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidatePaymentMethodLookupButtonDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
                ValidateElementDisabled(PaymentMethodLookupButton);
            else
                ValidateElementNotDisabled(PaymentMethodLookupButton);

            return this;
        }

		//Method to validate if Total Record fields are disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidateTotalRecordFieldsDisabled(bool IsDisabled = true)
		{
			if (IsDisabled)
			{
                ValidateElementDisabled(Totalrecord_YesOption);
                ValidateElementDisabled(Totalrecord_NoOption);
            }
            else
			{
                ValidateElementNotDisabled(Totalrecord_YesOption);
                ValidateElementNotDisabled(Totalrecord_NoOption);
            }

            return this;
		}

		//Method to validate if Reference field is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidateReferenceFieldDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
                ValidateElementDisabled(Reference);
            else
                ValidateElementNotDisabled(Reference);

            return this;
        }

		//Method to validate if Allocated fields are disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidateAllocatedFieldsDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
			{
                ValidateElementDisabled(Allocated_YesOption);
                ValidateElementDisabled(Allocated_NoOption);
            }
            else
			{
                ValidateElementNotDisabled(Allocated_YesOption);
                ValidateElementNotDisabled(Allocated_NoOption);
            }

            return this;
        }

		//Method to validate if Responsible Team Lookup Button is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidateResponsibleTeamLookupButtonDisabled(bool IsDisabled = true)
		{
			if (IsDisabled)
                ValidateElementDisabled(ResponsibleTeamLookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeamLookupButton);

            return this;
		}

		//Method to validate if Payer ProviderOrPerson Lookup Button is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidatePayerLookupButtonDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
                ValidateElementDisabled(ProviderOrPersonLookupButton);
            else
                ValidateElementNotDisabled(ProviderOrPersonLookupButton);

            return this;
        }

		//Method to validate if Alternative Invoice Number is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidateAlternativeInvoiceNumberDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
                ValidateElementDisabled(AlternativeInvoiceNumber);
            else
                ValidateElementNotDisabled(AlternativeInvoiceNumber);

            return this;
        }

		//Method to validate if Posted Date is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidatePostedDateDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
                ValidateElementDisabled(PostedDate);
            else
                ValidateElementNotDisabled(PostedDate);

            return this;
        }

		//Method to validate if Debt Written Off is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidateDebtWrittenOffDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
                ValidateElementDisabled(DebtWrittenOff);
            else
                ValidateElementNotDisabled(DebtWrittenOff);

            return this;
        }

		//Method to validate if Paid By is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidatePaidByDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
                ValidateElementDisabled(PaidBy);
            else
                ValidateElementNotDisabled(PaidBy);

            return this;
        }

        //Method to validate if AllocatedFinanceInvoicePaymentLookupButton is disabled or not disabled
		public CPFinanceInvoicePaymentRecordPage ValidateAllocatedFinanceInvoicePaymentLookupButtonDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
                ValidateElementDisabled(AllocatedFinanceInvoicePaymentLookupButton);
            else
                ValidateElementNotDisabled(AllocatedFinanceInvoicePaymentLookupButton);

            return this;
        }


        //method to click Menu Button
        public CPFinanceInvoicePaymentRecordPage ClickMenuButton()
		{
            WaitForElement(MenuButton);
			ScrollToElement(MenuButton);

			var IsMenuDisplayed = GetElementVisibility(LeftNavigationMenu);
			if (!IsMenuDisplayed)
				Click(MenuButton);

            return this;
        }

		//method to click Activities menu
		public CPFinanceInvoicePaymentRecordPage ClickActivitiesMenuButton()
		{
            if (!CheckIfElementExists(activitiesDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(activitiesLeftSubMenu);
                Click(activitiesLeftSubMenu);
            }

            return this;
		}

		//method to click Related Items menu
		public CPFinanceInvoicePaymentRecordPage ClickRelatedItemsMenuButton()
		{
            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            return this;
		}

		public CPFinanceInvoicePaymentRecordPage VerifyTasksSubMenuItemIsDisplayed(bool IsDisplayed = true)
		{
			ClickMenuButton();

			ClickActivitiesMenuButton();

			if (IsDisplayed)
				WaitForElementVisible(TasksSubMenuItemButton);
			else
				WaitForElementNotVisible(TasksSubMenuItemButton, 3);

			return this;

		}

        //Verify Attachments Sub Menu Item is displayed or not displayed
		public CPFinanceInvoicePaymentRecordPage VerifyAttachmentsSubMenuItemIsDisplayed(bool IsDisplayed = true)
		{
			ClickMenuButton();

            ClickRelatedItemsMenuButton();

            if (IsDisplayed)
                WaitForElementVisible(AttachmentsSubMenuItemButton);
            else
                WaitForElementNotVisible(AttachmentsSubMenuItemButton, 3);

            return this;
		}

        //Method to click Tasks sub menu button
        public CPFinanceInvoicePaymentRecordPage ClickTasks()
		{
            ClickMenuButton();

            ClickActivitiesMenuButton();

            WaitForElementToBeClickable(TasksSubMenuItemButton);
            Click(TasksSubMenuItemButton);

            return this;
        }

		//Method to click Attachments sub menu button
		public CPFinanceInvoicePaymentRecordPage ClickAttachments()
		{
            ClickMenuButton();

            ClickRelatedItemsMenuButton();

            WaitForElementToBeClickable(AttachmentsSubMenuItemButton);
            Click(AttachmentsSubMenuItemButton);

            return this;
        }

		//Method to click Allocations tab
		public CPFinanceInvoicePaymentRecordPage ClickAllocationsTab()
		{

			if(!GetElementByAttributeValue(AllocationsTab, "class").Contains("current")){
                WaitForElementToBeClickable(AllocationsTab);
                ScrollToElement(AllocationsTab);
                Click(AllocationsTab);
            }
            return this;
        }

		//Method to validate if Allocations tab is displayed or not displayed
		public CPFinanceInvoicePaymentRecordPage ValidateAllocationsTabIsVisible(bool IsVisible = true)
		{
            if (IsVisible)
                WaitForElementVisible(AllocationsTab);
            else
                WaitForElementNotVisible(AllocationsTab, 3);

            return this;
        }


	}
}
