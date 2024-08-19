using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinance
{
	public class PaymentAllocatePage : CommonMethods
	{
        #region IFrame Locators

        readonly By paymentAllocateIFrame = By.Id("iframe_PaymentAllocate");
		readonly By cwContentIframe = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=careproviderfinanceinvoicepayment&')]");

        #endregion

        readonly By BackButton = By.XPath("//*[@id='CWClose']");
		readonly By CompletedOn = By.XPath("//*[@id='CWCompletedOn']");
		readonly By CompletedOnDatePicker = By.XPath("//*[@id='CWCompletedOn_DatePicker']");
		readonly By DirectDebit_YesOption = By.XPath("//*[@id='CWDirectDebit_1']");
		readonly By DirectDebit_NoOption = By.XPath("//*[@id='CWDirectDebit_0']");
		readonly By ClearFiltersButton = By.XPath("//*[@id='CWClearFiltersButton']");
		readonly By SearchButton = By.XPath("//*[@id='CWSearchButton']");
		readonly By PageTitle = By.XPath("//h1");
		readonly By ResultsGrid = By.Id("CWResultsGrid");
		readonly By SearchCriteriaPanel = By.Id("CWFilterPanel");

		By EstablishmentLinkField(string ProviderName) => By.XPath("//*[@id = 'CWEstablishmentId_Link'][@title = '" + ProviderName + "']");
		By PayerLinkField(string PayerName) => By.XPath("//*[@id = 'CWPayerProviderId_Link'][@title = '" + PayerName + "']");
		readonly By ContractSchemeLookupButton = By.XPath("//*[@id='CWLookupBtn_CWContractSchemeId']");
		readonly By ContractSchemeLinkField = By.XPath("//*[@id='CWContractSchemeId_Link']");
		readonly By PersonLookupButton = By.XPath("//*[@id='CWLookupBtn_CWPersonId']");
		readonly By PersonLinkField = By.XPath("//*[@id='CWPersonId_Link']");

		readonly By AutoFitColumnWidthButton = By.XPath("//*[@id = 'TI_AutoFitColumnWidthButton']");
		readonly By ResultsSectionTopArea = By.XPath("//*[@id = 'CWCalculated']");
		readonly By RunningTotalLabel = By.XPath("//*[@id='CWTotalLabel']");
		readonly By RunningTotalValue = By.XPath("//*[@id = 'CWTotal']");
		readonly By ValueAllocatedLabel = By.XPath("//*[@id='CWAllocationLabel']");
		readonly By AllocationValue = By.XPath("//*[@id = 'CWAllocation']");
		readonly By VariationLabel = By.XPath("//*[@id='CWVariationLabel']");
		readonly By VariationValue = By.XPath("//*[@id = 'CWVariation']");

		By FinanceInvoiceRecord(string RecordId) => By.XPath("//*[@id = 'CWResultsGrid']//tr[@id = '"+RecordId+"']");
		By FinanceInvoiceRecordCheckbox(string RecordId) => By.XPath("//*[@id = 'CWResultsGrid']//tr[@id = '" + RecordId + "']//input[@type = 'checkbox']");
		readonly By CancelButton = By.XPath("//*[@id='CWCancelButton']");
		readonly By AllocateButton = By.XPath("//*[@id='CWAllocateButton']");

		By ResultsHeaderCellContent(int CellPosition) => By.XPath("//*[@id = 'CWResultsGrid']//th["+CellPosition+"][@scope = 'col']/*");
		readonly By SelectAllHeaderCheckbox = By.XPath("//*[@id = 'cwgridheaderselector']");
		By FinanceInvoiceRecordCellContent(string RecordId, int CellPosition) => By.XPath("//*[@id = 'CWResultsGrid']//tr[@id = '" + RecordId + "']/td["+CellPosition+"]/*");
        By FinanceInvoiceRecordAllocationField(string RecordId) => By.XPath("//*[@id = 'CWResultsGrid']//tr[@id = '" + RecordId + "']/td[@lname = 'allocation']/input");
		By FinanceInvoiceRecordAllocationField_FormError(string RecordId) => By.XPath("//*[@id = 'CWResultsGrid']//tr[@id = '" + RecordId + "']/td[@lname = 'allocation']/label[@class = 'formerror']/span");
        By FinanceInvoiceRecordInputCell(string RecordId, int CellPosition) => By.XPath("//*[@id = 'CWResultsGrid']//tr[@id = '" + RecordId + "']/td[" + CellPosition + "]/input");

        public PaymentAllocatePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        public PaymentAllocatePage WaitForPageToLoad()
        {
			SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElement(cwContentIframe);
			SwitchToIframe(cwContentIframe);

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElement(cwDialogIFrame);
			SwitchToIframe(cwDialogIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElement(paymentAllocateIFrame);
            SwitchToIframe(paymentAllocateIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElement(PageTitle);


            return this;
        }

        //Verify ResultsGrid is visible or not visible
		public PaymentAllocatePage ValidateResultsGridVisible(bool IsVisible = true)
		{
			if(IsVisible)
            {
                WaitForElementVisible(ResultsGrid);
            }
            else
            {
                WaitForElementNotVisible(ResultsGrid, 3);
            }
            return this;
        }

		//Verify SearchCriteriaPanel is visible or not visible
		public PaymentAllocatePage ValidateSearchCriteriaPanelVisible(bool IsVisible = true)
		{
			if (IsVisible)
			{
				WaitForElementVisible(SearchCriteriaPanel);
			}
			else
			{
				WaitForElementNotVisible(SearchCriteriaPanel, 3);
			}
			return this;
		}


        public PaymentAllocatePage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

        //AutoFitColumnWidthButton is visible or not visible
		public PaymentAllocatePage ValidateAutoFitColumnWidthButtonVisible(bool IsVisible = true)
		{
			if(IsVisible)
			{
                WaitForElementVisible(AutoFitColumnWidthButton);
            }
            else
			{
                WaitForElementNotVisible(AutoFitColumnWidthButton, 3);
            }
            return this;
		}

        //Click AutoFitColumnWidthButton
		public PaymentAllocatePage ClickAutoFitColumnWidthButton()
		{
			WaitForElementToBeClickable(AutoFitColumnWidthButton);
			ScrollToElement(AutoFitColumnWidthButton);
            Click(AutoFitColumnWidthButton);

            return this;
		}

        public PaymentAllocatePage ValidateCompletedOnText(string ExpectedText)
		{
			ValidateElementValue(CompletedOn, ExpectedText);

			return this;
		}

        //CompletedOn is visible or not visible
		public PaymentAllocatePage ValidateCompletedOnVisible(bool IsVisible = true)
		{
			if(IsVisible)
            {
				ScrollToElement(CompletedOn);
                WaitForElementVisible(CompletedOn);
            }
            else
            {
                WaitForElementNotVisible(CompletedOn, 3);
            }
            return this;
        }

		public PaymentAllocatePage ValidateCompletedOnDatePickerVisible(bool IsVisible = true)
		{
			if (IsVisible)
			{
				ScrollToElement(CompletedOnDatePicker);
				WaitForElementVisible(CompletedOnDatePicker);
			}
			else
			{
				WaitForElementNotVisible(CompletedOnDatePicker, 3);
			}
			return this;
		}

        public PaymentAllocatePage InsertTextOnCompletedOn(string TextToInsert)
		{
			WaitForElement(CompletedOn);
			ScrollToElement(CompletedOn);
			SendKeys(CompletedOn, TextToInsert + Keys.Tab);
			
			return this;
		}

        //Click CompletedOnDatePicker
		public PaymentAllocatePage ClickCompletedOnDatePicker()
		{
            WaitForElement(CompletedOnDatePicker);
			ScrollToElement(CompletedOnDatePicker);
            Click(CompletedOnDatePicker);

            return this;
        }

        public PaymentAllocatePage ClickDirectDebit_YesOption()
		{
			WaitForElementToBeClickable(DirectDebit_YesOption);
			Click(DirectDebit_YesOption);

			return this;
		}

		public PaymentAllocatePage ValidateDirectDebit_YesOptionChecked()
		{
			WaitForElement(DirectDebit_YesOption);
			ValidateElementChecked(DirectDebit_YesOption);
			
			return this;
		}

		public PaymentAllocatePage ValidateDirectDebit_YesOptionNotChecked()
		{
			WaitForElement(DirectDebit_YesOption);
			ValidateElementNotChecked(DirectDebit_YesOption);
			
			return this;
		}

		public PaymentAllocatePage ClickDirectDebit_NoOption()
		{
			WaitForElementToBeClickable(DirectDebit_NoOption);
			Click(DirectDebit_NoOption);

			return this;
		}

		public PaymentAllocatePage ValidateDirectDebit_NoOptionChecked()
		{
			WaitForElement(DirectDebit_NoOption);
			ValidateElementChecked(DirectDebit_NoOption);
			
			return this;
		}

		public PaymentAllocatePage ValidateCWDirectDebit_NoOptionNotChecked()
		{
			WaitForElement(DirectDebit_NoOption);
			ValidateElementNotChecked(DirectDebit_NoOption);
			
			return this;
		}

		//DirectDebit Options are visible or not visible
		public PaymentAllocatePage ValidateDirectDebitOptionsVisible(bool IsVisible = true)
		{
            if(IsVisible)
			{
				ScrollToElement(DirectDebit_YesOption);
                WaitForElementVisible(DirectDebit_YesOption);
                WaitForElementVisible(DirectDebit_NoOption);
            }
            else
			{
                WaitForElementNotVisible(DirectDebit_YesOption, 3);
                WaitForElementNotVisible(DirectDebit_NoOption, 3);
            }
            return this;
        }

		public PaymentAllocatePage ClickClearFiltersButton()
		{
			ScrollToElement(ClearFiltersButton);
			WaitForElementToBeClickable(ClearFiltersButton);
			Click(ClearFiltersButton);

			return this;
		}

		public PaymentAllocatePage ClickSearchButton()
		{
			WaitForElement(SearchButton);
			ScrollToElement(SearchButton);
			Click(SearchButton);

			return this;
		}

        //EstablishmentLinkField is visible or not visible
		public PaymentAllocatePage ValidateEstablishmentLinkFieldVisible(string ProviderName, bool IsVisible = true)
		{
			if(IsVisible)
			{
                WaitForElementVisible(EstablishmentLinkField(ProviderName));
            }
            else
			{
                WaitForElementNotVisible(EstablishmentLinkField(ProviderName), 3);
            }
			return this;
		}

        //EstablishmentLinkField is disabled or not disabled
		public PaymentAllocatePage ValidateEstablishmentLinkFieldDisabled(string ProviderName, bool IsDisabled = true)
		{
			if(IsDisabled)
            {
                ValidateElementDisabled(EstablishmentLinkField(ProviderName));
            }
            else
            {
                ValidateElementNotDisabled(EstablishmentLinkField(ProviderName));
            }
            return this;
        }

		//PayerLinkField is disabled or not disabled
		public PaymentAllocatePage ValidatePayerLinkFieldDisabled(string PayerName, bool IsDisabled = true)
		{
			if (IsDisabled)
			{
				ValidateElementDisabled(PayerLinkField(PayerName));
			}
			else
			{
				ValidateElementNotDisabled(PayerLinkField(PayerName));
			}
			return this;
		}

        //PayerLinkField is visible or not visible
        public PaymentAllocatePage ValidatePayerLinkFieldVisible(string PayerName, bool IsVisible = true)
		{
			if(IsVisible)
			{
                WaitForElementVisible(PayerLinkField(PayerName));
            }
            else
			{
                WaitForElementNotVisible(PayerLinkField(PayerName), 3);
            }
            return this;
		}

        //ContractSchemeLookupButton is visible or not visible
		public PaymentAllocatePage ValidateContractSchemeLookupButtonVisible(bool IsVisible = true)
		{
			if(IsVisible)
			{
                WaitForElementVisible(ContractSchemeLookupButton);
            }
            else
			{
                WaitForElementNotVisible(ContractSchemeLookupButton, 3);
            }
            return this;
		}

        public PaymentAllocatePage ValidateContractSchemeLookupButtonDisabled(bool IsDisabled = true)
		{
            if (IsDisabled)
			{
                ValidateElementDisabled(ContractSchemeLookupButton);
            }
            else
			{
                ValidateElementNotDisabled(ContractSchemeLookupButton);
            }
            return this;
        }

		public PaymentAllocatePage ClickContractSchemeLookupButton()
		{
			WaitForElementToBeClickable(ContractSchemeLookupButton);
			//ScrollToElement(ContractSchemeLookupButton);
            Click(ContractSchemeLookupButton);

            return this;
		}

		public PaymentAllocatePage ValidateContractSchemeLinkField(string ExpectedText)
		{
			WaitForElement(ContractSchemeLinkField);
			ScrollToElement(ContractSchemeLinkField);
			ValidateElementByTitle(ContractSchemeLinkField, ExpectedText);

            return this;
		}

		public PaymentAllocatePage ClickPersonLookupButton()
		{
			WaitForElement(PersonLookupButton);
            ScrollToElement(PersonLookupButton);
            Click(PersonLookupButton);

            return this;
		}

		public PaymentAllocatePage ValidatePersonLinkField(string ExpectedText)
		{
			WaitForElement(PersonLinkField);
            ScrollToElement(PersonLinkField);
            ValidateElementByTitle(PersonLinkField, ExpectedText);

            return this;
		}

        //PersonLookupButton is disabled or not disabled
		public PaymentAllocatePage ValidatePersonLookupButtonDisabled(bool IsDisabled = true)
		{
			if(IsDisabled)
			{
                ValidateElementDisabled(PersonLookupButton);
            }
            else
			{
                ValidateElementNotDisabled(PersonLookupButton);
            }
            return this;
		}

		//PersonLookupButton is visible or not visible
		public PaymentAllocatePage ValidatePersonLookupButtonVisible(bool IsVisible = true)
		{
			if(IsVisible)
			{
				ScrollToElement(PersonLookupButton);
                WaitForElementVisible(PersonLookupButton);
            }
            else
			{
                WaitForElementNotVisible(PersonLookupButton, 3);
            }
            return this;
		}

        //verify ResultsSectionTopArea fields are visible or not visible
		public PaymentAllocatePage ValidateResultsSectionTopAreaVisible(bool IsVisible = true)
		{
			if(IsVisible)
			{
                ScrollToElement(ResultsSectionTopArea);
                WaitForElementVisible(ResultsSectionTopArea);
                WaitForElementVisible(RunningTotalLabel);
                WaitForElementVisible(RunningTotalValue);
                WaitForElementVisible(ValueAllocatedLabel);
                WaitForElementVisible(AllocationValue);
                WaitForElementVisible(VariationLabel);
                WaitForElementVisible(VariationValue);
            }
            else
			{
                WaitForElementNotVisible(ResultsSectionTopArea, 3);
                WaitForElementNotVisible(RunningTotalLabel, 3);
                WaitForElementNotVisible(RunningTotalValue, 3);
                WaitForElementNotVisible(ValueAllocatedLabel, 3);
                WaitForElementNotVisible(AllocationValue, 3);
                WaitForElementNotVisible(VariationLabel, 3);
                WaitForElementNotVisible(VariationValue, 3);
            }
            return this;
		}

		//verify running total value
		public PaymentAllocatePage ValidateRunningTotalValue(string ExpectedText)
		{
            WaitForElement(RunningTotalValue);
			ScrollToElement(RunningTotalValue);
            ValidateElementText(RunningTotalValue, ExpectedText);

            return this;
        }

		//verify allocation value
		public PaymentAllocatePage ValidateAllocationValue(string ExpectedText)
		{
            WaitForElement(AllocationValue);
			ScrollToElement(AllocationValue);
            ValidateElementText(AllocationValue, ExpectedText);

            return this;
        }

		//verify variation value
		public PaymentAllocatePage ValidateVariationValue(string ExpectedText)
		{
            WaitForElement(VariationValue);
			ScrollToElement(VariationValue);
            ValidateElementText(VariationValue, ExpectedText);

            return this;
        }

		//FinanceInvoiceRecord is visible or not visible
		public PaymentAllocatePage ValidateFinanceInvoiceRecordVisible(string RecordId, bool IsVisible = true)
		{
			if (IsVisible)
			{
				WaitForElementVisible(FinanceInvoiceRecord(RecordId));
			}
			else
			{
				WaitForElementNotVisible(FinanceInvoiceRecord(RecordId), 3);
			}
			return this;
		}

        //FinanceInvoiceRecordCheckbox is checked or not checked
		public PaymentAllocatePage ValidateFinanceInvoiceRecordCheckboxChecked(string RecordId, bool IsChecked = true)
		{
			if (IsChecked)
			{
                ValidateElementChecked(FinanceInvoiceRecordCheckbox(RecordId));
            }
            else
			{
                ValidateElementNotChecked(FinanceInvoiceRecordCheckbox(RecordId));
            }
            return this;
		}

        //Select FinanceInvoiceRecord
        public PaymentAllocatePage SelectFinanceInvoiceRecord(string RecordId)
		{
			WaitForElement(FinanceInvoiceRecordCheckbox(RecordId));
			ScrollToElement(FinanceInvoiceRecordCheckbox(RecordId));
            Click(FinanceInvoiceRecordCheckbox(RecordId));

            return this;
		}

		public PaymentAllocatePage ClickCancelButton()
		{
			WaitForElementToBeClickable(CancelButton);
			ScrollToElement(CancelButton);
            Click(CancelButton);

            return this;
		}

		public PaymentAllocatePage ClickAllocateButton()
		{
			WaitForElementToBeClickable(AllocateButton);
			ScrollToElement(AllocateButton);
            Click(AllocateButton);

            return this;
		}

		//Cancel button is visible or not visible
		public PaymentAllocatePage ValidateCancelButtonVisible(bool IsVisible = true)
		{
			if (IsVisible)
			{
                ScrollToElement(CancelButton);
                WaitForElementVisible(CancelButton);
            }
            else
			{
                WaitForElementNotVisible(CancelButton, 3);
            }
            return this;
		}

		//Allocate button is visible or not visible
		public PaymentAllocatePage ValidateAllocateButtonVisible(bool IsVisible = true)
		{
			if (IsVisible)
			{
                ScrollToElement(AllocateButton);
                WaitForElementVisible(AllocateButton);
            }
            else
			{
                WaitForElementNotVisible(AllocateButton, 3);
            }
            return this;
		}

		//Allocate button is disabled or not disabled
		public PaymentAllocatePage ValidateAllocateButtonDisabled(bool IsDisabled = true)
		{
			if (IsDisabled)
			{
                ValidateElementDisabled(AllocateButton);
            }
            else
			{
                ValidateElementNotDisabled(AllocateButton);
            }
            return this;
		}

		//Cancel button is disabled or not disabled
		public PaymentAllocatePage ValidateCancelButtonDisabled(bool IsDisabled = true)
		{
			if (IsDisabled)
			{
                ValidateElementDisabled(CancelButton);
            }
            else
			{
                ValidateElementNotDisabled(CancelButton);
            }
            return this;
		}

        //ResultsHeaderCellContent text
		public PaymentAllocatePage ValidateResultsHeaderCellContent(int CellPosition, string ExpectedText)
		{
			WaitForElement(ResultsHeaderCellContent(CellPosition));
            ScrollToElement(ResultsHeaderCellContent(CellPosition));
            ValidateElementTextContainsText(ResultsHeaderCellContent(CellPosition), ExpectedText);

            return this;
		}

        //SelectAllHeaderCheckbox is checked or not checked
		public PaymentAllocatePage ValidateSelectAllHeaderCheckboxChecked(bool IsChecked = true)
		{
			if(IsChecked)
			{
                ValidateElementChecked(SelectAllHeaderCheckbox);
            }
            else
			{
                ValidateElementNotChecked(SelectAllHeaderCheckbox);
            }
            return this;
		}

        //Verify FinanceInvoiceRecordCellContent text
        public PaymentAllocatePage ValidateFinanceInvoiceRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
		{
			WaitForElement(FinanceInvoiceRecordCellContent(RecordId, CellPosition));
            ScrollToElement(FinanceInvoiceRecordCellContent(RecordId, CellPosition));
            var elementValue = GetElementByAttributeValue(FinanceInvoiceRecordCellContent(RecordId, CellPosition), "value");
            Assert.AreEqual(ExpectedText, elementValue);

            return this;
		}

        //Verify FinanceInvoiceRecordInputCell field is visible or not visible
		public PaymentAllocatePage ValidateFinanceInvoiceRecordInputCellIsAvailable(string RecordId, int CellPosition, bool IsVisible = true)
		{
			if (IsVisible)
			{
				WaitForElementVisible(FinanceInvoiceRecordInputCell(RecordId, CellPosition));
            }
            else
            {
                WaitForElementNotVisible(FinanceInvoiceRecordInputCell(RecordId, CellPosition), 3);
            }
            return this;
		}


        //Insert text in FinanceInvoiceRecordAllocationField
        public PaymentAllocatePage InsertTextInFinanceInvoiceRecordAllocationField(string RecordId, string TextToInsert)
		{
			WaitForElement(FinanceInvoiceRecordAllocationField(RecordId));
            ScrollToElement(FinanceInvoiceRecordAllocationField(RecordId));
            SendKeys(FinanceInvoiceRecordAllocationField(RecordId), TextToInsert + Keys.Tab);

            return this;
		}

        //verify FinanceInvoiceRecordAllocationField value
		public PaymentAllocatePage ValidateFinanceInvoiceRecordAllocationFieldValue(string RecordId, string ExpectedText)
		{
			WaitForElement(FinanceInvoiceRecordAllocationField(RecordId));
            ScrollToElement(FinanceInvoiceRecordAllocationField(RecordId));
            var elementValue = GetElementByAttributeValue(FinanceInvoiceRecordAllocationField(RecordId), "value");
            Assert.AreEqual(ExpectedText, elementValue);

            return this;
		}

        //FinanceInvoiceRecordAllocationField is visible or not visible
		public PaymentAllocatePage ValidateFinanceInvoiceRecordAllocationFieldVisible(string RecordId, bool IsVisible = true)
		{
			if (IsVisible)
			{
                WaitForElementVisible(FinanceInvoiceRecordAllocationField(RecordId));
            }
            else
			{
                WaitForElementNotVisible(FinanceInvoiceRecordAllocationField(RecordId), 3);
            }
            return this;
		}

        //verify FinanceInvoiceRecordAllocationField_FormError text
		public PaymentAllocatePage ValidateFinanceInvoiceRecordAllocationField_FormError(string RecordId, string ExpectedText)
		{
			WaitForElement(FinanceInvoiceRecordAllocationField_FormError(RecordId));
            ScrollToElement(FinanceInvoiceRecordAllocationField_FormError(RecordId));
            ValidateElementByTitle(FinanceInvoiceRecordAllocationField_FormError(RecordId), ExpectedText);

            return this;
		}

        public PaymentAllocatePage ValidateFinanceInvoiceRecordAllocationField_FormError(Guid RecordId, string ExpectedText)
        {
			return ValidateFinanceInvoiceRecordAllocationField_FormError(RecordId.ToString(), ExpectedText);
        }

    }
}
