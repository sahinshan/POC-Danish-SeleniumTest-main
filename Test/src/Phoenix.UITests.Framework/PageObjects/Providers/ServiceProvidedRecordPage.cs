using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
    public class ServiceProvidedRecordPage : CommonMethods
    {

        public ServiceProvidedRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovided&')]");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By shareRecordButton = By.Id("TI_ShareRecordButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By activateButton = By.Id("TI_ActivateButton");
        readonly By restrictAccessButton = By.Id("TI_RestrictAccessButton");

        readonly By toolbarMenuButton = By.Id("CWToolbarMenu");
        readonly By toolbarMenuDeleteButton = By.XPath("//a[@title = 'Delete']");

        readonly By serviceProvidedPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[contains(@title, 'Service Provided: ')]");

        readonly By providerLookupButton = By.Id("CWLookupBtn_providerid");
        readonly By provider_LinkField = By.Id("CWField_providerid_Link");

        readonly By serviceElement1LookupButton = By.Id("CWLookupBtn_serviceelement1id");
        readonly By serviceElement1_LinkField = By.Id("CWField_serviceelement1id_Link");

        readonly By serviceElement2LookupButton = By.Id("CWLookupBtn_serviceelement2id");
        readonly By serviceElement2_LinkField = By.Id("CWField_serviceelement2id_Link");

        readonly By serviceElement3LookupButton = By.Id("CWLookupBtn_serviceelement3id");

        readonly By contractTypePicklist = By.Id("CWField_contracttypeid");

        readonly By approvalStatusPicklist = By.Id("CWField_approvalstatusid");

        readonly By clientCategoryLookupButton = By.Id("CWLookupBtn_financeclientcategoryid");

        readonly By serviceProvidedIdNumber = By.Id("CWControlHolder_serviceprovidednumber");
        readonly By serviceProvidedNumber_Field = By.Id("CWField_serviceprovidednumber");

        readonly By inactive_YesOption = By.Id("CWField_inactive_1");
        readonly By inactive_NoOption = By.Id("CWField_inactive_0");

        readonly By responsibleTeam_LinkField = By.Id("CWField_ownerid_Link");
        readonly By responsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

        readonly By responsibleUser_LinkField = By.Id("CWField_responsibleuserid_Link");
        readonly By responsibleUser_LookupButton = By.Id("CWLookupBtn_responsibleuserid");

        readonly By currentRanking_LookupButton = By.Id("CWLookupBtn_currentrankingid");
        readonly By glCode_LookupButton = By.Id("CWLookupBtn_glcodeid");
        readonly By notes_Field = By.Id("CWField_notes");

        readonly By negotiatedRatesApply_YesOption = By.Id("CWField_negotiatedratesapply_1");
        readonly By negotiatedRatesApply_NoOption = By.Id("CWField_negotiatedratesapply_0");

        readonly By usedInFinance_YesOption = By.Id("CWField_usedinfinance_1");
        readonly By usedInFinance_NoOption = By.Id("CWField_usedinfinance_0");

        readonly By serviceProvidedPage_DetailsTab = By.Id("CWNavGroup_EditForm");
        readonly By serviceFinanceSettings_Tab = By.Id("CWNavGroup_ServiceFinanceSettings");
        readonly By serviceProvidedRatePeriods_Tab = By.Id("CWNavGroup_ServiceProvidedRatePeriods");
        readonly By serviceDeliveryVariations_Tab = By.Id("CWNavGroup_ServiceDeliveryVariations");

        By approvalStatusOption(string optionText) => By.XPath("//*[@id='CWField_approvalstatusid']//option[text()='" + optionText + "']");


        public ServiceProvidedRecordPage WaitForServiceProvidedRecordPageToLoad(bool active)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            if (active)
            {
                WaitForElement(saveButton);
                WaitForElement(saveAndCloseButton);
            }
            WaitForElement(providerLookupButton);
            WaitForElement(serviceElement1LookupButton);
            WaitForElement(contractTypePicklist);
            WaitForElement(approvalStatusPicklist);
            WaitForElement(clientCategoryLookupButton);

            return this;
        }

        public ServiceProvidedRecordPage WaitForServiceProvidedRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(providerLookupButton);
            WaitForElement(serviceElement1LookupButton);
            WaitForElement(contractTypePicklist);
            WaitForElement(approvalStatusPicklist);
            WaitForElement(clientCategoryLookupButton);

            return this;
        }

        public ServiceProvidedRecordPage SelectServiceElement1LookupButton()
        {
            WaitForElementToBeClickable(serviceElement1LookupButton);
            MoveToElementInPage(serviceElement1LookupButton);
            Click(serviceElement1LookupButton);

            return this;
        }

        public ServiceProvidedRecordPage SelectServiceElement2LookupButton()
        {
            WaitForElementToBeClickable(serviceElement2LookupButton);
            MoveToElementInPage(serviceElement2LookupButton);
            Click(serviceElement2LookupButton);

            return this;
        }

        public ServiceProvidedRecordPage SelectServiceElement3LookupButton()
        {
            WaitForElement(serviceElement3LookupButton);
            MoveToElementInPage(serviceElement3LookupButton);
            Click(serviceElement3LookupButton);

            return this;
        }

        public ServiceProvidedRecordPage SelectClientCategoryLookupButton()
        {
            WaitForElementToBeClickable(clientCategoryLookupButton);
            MoveToElementInPage(clientCategoryLookupButton);
            Click(clientCategoryLookupButton);

            return this;
        }

        public ServiceProvidedRecordPage SelectCurrentRankingLookupButton()
        {
            WaitForElementToBeClickable(currentRanking_LookupButton);
            MoveToElementInPage(currentRanking_LookupButton);
            Click(currentRanking_LookupButton);

            return this;
        }

        public ServiceProvidedRecordPage SelectApprovalStatus(string optionToSelect)
        {
            WaitForElement(approvalStatusPicklist);
            MoveToElementInPage(approvalStatusPicklist);
            SelectPicklistElementByText(approvalStatusPicklist, optionToSelect);

            return this;
        }

        public ServiceProvidedRecordPage SelectContractType(string optionToSelect)
        {
            WaitForElement(contractTypePicklist);
            MoveToElementInPage(contractTypePicklist);
            SelectPicklistElementByText(contractTypePicklist, optionToSelect);

            return this;
        }

        public ServiceProvidedRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ServiceProvidedRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            WaitForElementNotVisible(By.Id("CWRefreshPanel"), 5);

            return this;
        }

        public ServiceProvidedRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            WaitForElementNotVisible(By.Id("CWRefreshPanel"), 5);

            return this;
        }

        public ServiceProvidedRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(toolbarMenuButton);
            Click(toolbarMenuButton);

            WaitForElementToBeClickable(toolbarMenuDeleteButton);
            Click(toolbarMenuDeleteButton);

            return this;
        }

        public ServiceProvidedRecordPage ClickActivateButton()
        {
            WaitForElement(activateButton);
            MoveToElementInPage(activateButton);
            Click(activateButton);
            return this;
        }


        public ServiceProvidedRecordPage InactiveStatus(string optionToSelect)
        {
            if (optionToSelect.Equals("Yes"))
            {
                WaitForElement(inactive_YesOption);
                Click(inactive_YesOption);
            }
            else
            {
                WaitForElement(inactive_NoOption);
                Click(inactive_NoOption);
            }

            return this;
        }

        public ServiceProvidedRecordPage NavigateToDetailsTab()
        {
            WaitForElementToBeClickable(serviceProvidedPage_DetailsTab);
            MoveToElementInPage(serviceProvidedPage_DetailsTab);
            Click(serviceProvidedPage_DetailsTab);

            return this;
        }

        public ServiceProvidedRecordPage NavigateToRatePeriodsTab()
        {
            WaitForElementToBeClickable(serviceProvidedRatePeriods_Tab);
            MoveToElementInPage(serviceProvidedRatePeriods_Tab);
            Click(serviceProvidedRatePeriods_Tab);

            return this;

        }

        public ServiceProvidedRecordPage NavigateToServiceFinanceSettingsTab()
        {
            WaitForElementToBeClickable(serviceFinanceSettings_Tab);
            MoveToElementInPage(serviceFinanceSettings_Tab);
            Click(serviceFinanceSettings_Tab);

            return this;

        }

        public ServiceProvidedRecordPage ValidateInactiveStatus(string optionToSelect)
        {
            WaitForElement(inactive_YesOption);
            WaitForElement(inactive_NoOption);

            if (optionToSelect.Equals("Yes"))
            {
                ValidateElementChecked(inactive_YesOption);
                ValidateElementNotChecked(inactive_NoOption);
                ValidateElementDisabled(inactive_YesOption);
                ValidateElementDisabled(inactive_NoOption);
            }
            else
            {
                ValidateElementChecked(inactive_NoOption);
                ValidateElementNotChecked(inactive_YesOption);
                ValidateElementNotDisabled(inactive_YesOption);
                ValidateElementNotDisabled(inactive_NoOption);
            }

            return this;
        }

        public ServiceProvidedRecordPage WaitForRecordPageTitleToLoad(string PageTitle)
        {
            WaitForServiceProvidedRecordPageToLoad();

            ValidateElementTextContainsText(serviceProvidedPageHeader, "Service Provided:\r\n" + PageTitle);

            return this;
        }

        public ServiceProvidedRecordPage ValidateRecordIsInactive()
        {
            WaitForElementVisible(activateButton);
            Assert.IsTrue(GetElementVisibility(activateButton));
            ValidateElementChecked(inactive_YesOption);
            ValidateElementNotChecked(inactive_NoOption);
            ValidateElementDisabled(inactive_YesOption);
            ValidateElementDisabled(inactive_NoOption);


            return this;
        }

        public ServiceProvidedRecordPage ValidateActiveButtonVisibility(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
                WaitForElementVisible(activateButton);

            Assert.AreEqual(ExpectedVisibility, GetElementVisibility(activateButton));

            return this;
        }

        public ServiceProvidedRecordPage ValidateProviderLinkFieldText(string ExpectedText)
        {
            WaitForElement(provider_LinkField);
            MoveToElementInPage(provider_LinkField);
            ValidateElementText(provider_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvidedRecordPage ValidateServiceElement1LinkFieldText(string ExpectedText)
        {
            WaitForElement(serviceElement1_LinkField);
            MoveToElementInPage(serviceElement1_LinkField);
            ValidateElementText(serviceElement1_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvidedRecordPage ValidateServiceElement2LinkFieldText(string ExpectedText)
        {
            WaitForElement(serviceElement2_LinkField);
            MoveToElementInPage(serviceElement2_LinkField);
            ValidateElementText(serviceElement2_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvidedRecordPage ValidateProviderLookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(providerLookupButton);
            else
                ValidateElementNotDisabled(providerLookupButton);

            return this;
        }

        public ServiceProvidedRecordPage ValidateServiceElement1LookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(serviceElement1LookupButton);
            else
                ValidateElementNotDisabled(serviceElement1LookupButton);

            return this;
        }

        public ServiceProvidedRecordPage ValidateServiceElement2LookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(serviceElement2LookupButton);
            else
                ValidateElementNotDisabled(serviceElement2LookupButton);

            return this;
        }

        public ServiceProvidedRecordPage ValidateServiceElement3LookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(serviceElement3LookupButton);
            else
                ValidateElementNotDisabled(serviceElement3LookupButton);

            return this;
        }

        public ServiceProvidedRecordPage ValidateContractTypePickListDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(contractTypePicklist);
            else
                ValidateElementNotDisabled(contractTypePicklist);

            return this;
        }

        public ServiceProvidedRecordPage ValidateResponsibleTeamLookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(responsibleTeam_LookupButton);
            else
                ValidateElementNotDisabled(responsibleTeam_LookupButton);

            return this;
        }

        public ServiceProvidedRecordPage ValidateResponsibleUserLookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(responsibleUser_LookupButton);
            else
                ValidateElementNotDisabled(responsibleUser_LookupButton);

            return this;
        }

        public ServiceProvidedRecordPage ValidateApprovalStatusPickListDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(approvalStatusPicklist);
            else
                ValidateElementNotDisabled(approvalStatusPicklist);

            return this;
        }

        public ServiceProvidedRecordPage ValidateServiceProvidedNumberFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(serviceProvidedNumber_Field);
            else
                ValidateElementNotDisabled(serviceProvidedNumber_Field);

            return this;
        }

        public ServiceProvidedRecordPage ValidateClientCategoryLookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(clientCategoryLookupButton);
            else
                ValidateElementNotDisabled(clientCategoryLookupButton);

            return this;
        }

        public ServiceProvidedRecordPage ValidateCurrentRankingLookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(currentRanking_LookupButton);
            else
                ValidateElementNotDisabled(currentRanking_LookupButton);

            return this;
        }

        public ServiceProvidedRecordPage ValidateGLCodeLookupButtondDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(glCode_LookupButton);
            else
                ValidateElementNotDisabled(glCode_LookupButton);

            return this;
        }

        public ServiceProvidedRecordPage ValidateNegotiatedRatesApplyOptionsDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(negotiatedRatesApply_YesOption);
                ValidateElementDisabled(negotiatedRatesApply_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(negotiatedRatesApply_YesOption);
                ValidateElementNotDisabled(negotiatedRatesApply_NoOption);
            }

            return this;
        }

        public ServiceProvidedRecordPage ValidateUsedInFinanceOptionsDisabled(bool ExpectDisabled)
        {
            WaitForElementVisible(usedInFinance_NoOption);
            ScrollToElement(usedInFinance_NoOption);

            if (ExpectDisabled)
            {
                ValidateElementDisabled(usedInFinance_YesOption);
                ValidateElementDisabled(usedInFinance_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(usedInFinance_YesOption);
                ValidateElementNotDisabled(usedInFinance_NoOption);
            }

            return this;
        }

        public ServiceProvidedRecordPage ValidateInactiveOptionsDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(inactive_YesOption);
                ValidateElementDisabled(inactive_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(inactive_YesOption);
                ValidateElementNotDisabled(inactive_NoOption);
            }

            return this;
        }

        public ServiceProvidedRecordPage ValidateNotesFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(notes_Field);
            else
                ValidateElementNotDisabled(notes_Field);

            return this;
        }

        public ServiceProvidedRecordPage InsertNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(notes_Field);
            SendKeys(notes_Field, TextToInsert);

            return this;
        }

        public ServiceProvidedRecordPage ValidateApprovalStatusOptionDisabled(string OptionText, bool ExpectDisabled)
        {
            WaitForElementToBeClickable(approvalStatusPicklist);
            MoveToElementInPage(approvalStatusPicklist);
            Click(approvalStatusPicklist);

            if (ExpectDisabled)
                ValidateElementDisabled(approvalStatusOption(OptionText));
            else
                ValidateElementNotDisabled(approvalStatusOption(OptionText));

            return this;
        }

        public ServiceProvidedRecordPage ValidateApprovalStatusSelectedText(string ExpectedSelectedText)
        {
            WaitForElementVisible(approvalStatusPicklist);
            MoveToElementInPage(approvalStatusPicklist);
            ValidatePicklistSelectedText(approvalStatusPicklist, ExpectedSelectedText);

            return this;
        }

        public ServiceProvidedRecordPage ValidateContractTypePickListSelectedText(string ExpectedSelectedText)
        {
            WaitForElementVisible(contractTypePicklist);
            MoveToElementInPage(contractTypePicklist);
            ValidatePicklistSelectedText(contractTypePicklist, ExpectedSelectedText);

            return this;
        }

        public ServiceProvidedRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(shareRecordButton);
            WaitForElement(assignRecordButton);
            WaitForElement(toolbarMenuButton);

            return this;
        }

        public ServiceProvidedRecordPage WaitForRecordToBeSavedAsInactive()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(shareRecordButton);
            WaitForElement(assignRecordButton);
            WaitForElement(activateButton);
            WaitForElement(restrictAccessButton);
            WaitForElement(toolbarMenuButton);

            return this;
        }

        public ServiceProvidedRecordPage ValidateServiceFinanceSettingsTabVisibility(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
                WaitForElementVisible(serviceFinanceSettings_Tab);

            Assert.AreEqual(ExpectedVisibility, GetElementVisibility(serviceFinanceSettings_Tab));

            return this;
        }

        public ServiceProvidedRecordPage ValidateRatePeriodsTabVisibility(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
                WaitForElement(serviceProvidedRatePeriods_Tab);

            Assert.AreEqual(ExpectedVisibility, GetElementVisibility(serviceProvidedRatePeriods_Tab));

            return this;
        }

        public ServiceProvidedRecordPage ValidateServiceDeliveryVariationsTabVisibility(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
                WaitForElement(serviceDeliveryVariations_Tab);

            Assert.AreEqual(ExpectedVisibility, GetElementVisibility(serviceDeliveryVariations_Tab));

            return this;
        }

        public ServiceProvidedRecordPage ValidateUsedInFinanceOptionChecked(bool YesOptionChecked = false)
        {
            WaitForElementVisible(usedInFinance_YesOption);
            WaitForElementVisible(usedInFinance_NoOption);
            ScrollToElement(usedInFinance_NoOption);

            if (YesOptionChecked)
            {
                ValidateElementChecked(usedInFinance_YesOption);
                ValidateElementNotChecked(usedInFinance_NoOption);
            }
            else
            {
                ValidateElementChecked(usedInFinance_NoOption);
                ValidateElementNotChecked(usedInFinance_YesOption);
            }

            return this;
        }

        public ServiceProvidedRecordPage SelectNegotiatedRatesApplyOption(string optionToSelect)
        {
            if (optionToSelect.Equals("Yes"))
            {
                WaitForElementToBeClickable(negotiatedRatesApply_YesOption);
                Click(negotiatedRatesApply_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(negotiatedRatesApply_NoOption);
                Click(negotiatedRatesApply_NoOption);
            }

            return this;
        }

        public ServiceProvidedRecordPage ValidateNegotiatedRatesApplyOptionChecked(bool YesOptionChecked = false)
        {
            WaitForElementVisible(negotiatedRatesApply_YesOption);
            WaitForElementVisible(negotiatedRatesApply_NoOption);
            ScrollToElement(negotiatedRatesApply_NoOption);

            if (YesOptionChecked)
            {
                ValidateElementChecked(negotiatedRatesApply_YesOption);
                ValidateElementNotChecked(negotiatedRatesApply_NoOption);
            }
            else
            {
                ValidateElementChecked(negotiatedRatesApply_NoOption);
                ValidateElementNotChecked(negotiatedRatesApply_YesOption);
            }

            return this;
        }

    }
}
