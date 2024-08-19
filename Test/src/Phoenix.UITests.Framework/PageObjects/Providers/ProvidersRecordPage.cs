using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ProvidersRecordPage : CommonMethods
    {
        public ProvidersRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ProviderRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=provider&')]");
        readonly By ProviderAllowableBookingTypesIFrame = By.Id("CWIFrame_ProviderAllowableBookingTypes");

        By ChildProviderRecordIFrame(string ParentProviderID) => By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=provider&pid=" + ParentProviderID + "')]");


        readonly By pageHeader = By.XPath("//h1");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By recordHeaderTitle = By.XPath("//div[@id='CWToolbar']/div/h1");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By backButton = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']//*[@id='CWToolbarMenu']/button");
        readonly By timelineTab = By.Id("CWNavGroup_Timeline");
        readonly By summaryTab = By.Id("CWNavGroup_SummaryDashboard");
        readonly By detailsTab = By.Id("CWNavGroup_EditForm");
        readonly By contractServicesTab = By.Id("CWNavGroup_CareProviderContractServices");
        readonly By contractServicesWithRatesTab = By.Id("CWNavGroup_CPProviderContractServiceRates");
        readonly By financeTransactionsTab = By.Id("CWNavGroup_CareProviderFinanceTransactions");

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");


        #region General Section Fields

        readonly By General_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div/span[text()='General']");


        readonly By Id_Field = By.Id("CWField_providernumber");
        readonly By name_Field = By.Id("CWField_name");
        readonly By name_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");
        readonly By CommissionerOrganisation_LookupButton = By.Id("CWLookupBtn_commissionerorganisationid");
        readonly By CommissionerOrganisation_LinkField = By.Id("CWField_commissionerorganisationid_Link");
        readonly By RTTTreatmentFunction_LookupButton = By.Id("CWLookupBtn_rtttreatmentfunctionid");
        readonly By RTTTreatmentFunction_LinkField = By.Id("CWField_rtttreatmentfunctionid_Link");
        readonly By providertypeid_Field = By.Id("CWField_providertypeid");
        readonly By providerType_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_providertypeid']/label/span");
        readonly By accountnumber_Field = By.Id("CWField_accountnumber");
        readonly By parentproviderid_LookupButton = By.Id("CWLookupBtn_parentproviderid");
        readonly By primarycontactid_LookupButton = By.Id("CWLookupBtn_primarycontactid");
        readonly By cqclocationid_Field = By.Id("CWField_cqclocationid");
        readonly By odscode_Field = By.Id("CWField_odscode");
        readonly By description_Field = By.Id("CWField_description");

        readonly By ResponsibleTeam_LinkField = By.Id("CWField_ownerid_Link");
        readonly By mainphone_Field = By.Id("CWField_mainphone");
        readonly By otherphone_Field = By.Id("CWField_otherphone");
        readonly By website_Field = By.Id("CWField_website");
        readonly By email_Field = By.Id("CWField_email");
        readonly By startdate_Field = By.Id("CWField_startdate");
        readonly By enddate_Field = By.Id("CWField_enddate");
        readonly By EnableScheduling_YesRadioButton = By.Id("CWField_enablescheduling_1");
        readonly By EnableScheduling_NoRadioButton = By.Id("CWField_enablescheduling_0");
        readonly By website_LinkField = By.Id("CWField_website_Link");
        readonly By primaryContact_LinkField = By.Id("CWField_primarycontactid_Link");
        readonly By DefaultHolidayYear_LookupButton = By.Id("CWLookupBtn_defaultholidayyearid");

        readonly By EnableScheduling_LabelField = By.XPath("//*[@id='CWLabelHolder_enablescheduling']/label[text()='Enable Scheduling']");
        readonly By EnableScheduling_MandatoryField = By.XPath("//*[@id='CWLabelHolder_enablescheduling']/label/span[@class='mandatory']");

        #endregion

        #region Contact Section Fields

        readonly By Contact_SectionTitle = By.XPath("//*[@id='CWSection_Contact']/fieldset/div/span[text()='Correspondences']");

        #endregion

        #region Address Section Fields

        readonly By Address_SectionTitle = By.XPath("//*[@id='CWSection_Address']/fieldset/div/span[text()='Address']");

        static string propertyname_FieldName = "CWField_propertyname";
        static string propertyno_FieldName = "CWField_addressline1";
        static string street_FieldName = "CWField_addressline2";
        static string villageDistrict_FieldName = "CWField_addressline3";
        static string townCity_FieldName = "CWField_addressline4";
        static string postcode_FieldName = "CWField_postcode";

        static string county_FieldName = "CWField_addressline5";
        static string country_FieldName = "CWField_country";
        static string addressType_FieldName = "CWField_addresstypeid";
        static string startDate_FieldName = "CWField_startdate";
        static string endDate_FieldName = "CWField_enddate";

        readonly By addressstartdate_Field = By.Id("CWField_addressstartdate");
        readonly By addresstypeid_Field = By.Id("CWField_addresstypeid");
        readonly By propertyname_Field = By.Id("CWField_propertyname");
        readonly By propertyNo_Field = By.Id("CWField_addressline1");
        readonly By street_Field = By.Id("CWField_addressline2");
        readonly By vlgDistrict_Field = By.Id("CWField_addressline3");
        readonly By townCity_Field = By.Id("CWField_addressline4");
        readonly By county_Field = By.Id("CWField_addressline5");
        readonly By postcode_Field = By.Id("CWField_postcode");
        readonly By addresspropertytypeid_LookupButton = By.Id("CWLookupBtn_addresspropertytypeid");
        readonly By country_Field = By.Id("CWField_country");
        readonly By addressphone_Field = By.Id("CWField_addressphone");
        readonly By contactHours_Field = By.Id("CWField_contacthours");
        readonly By contactmethodid_LookupButton = By.Id("CWLookupBtn_contactmethodid");
        readonly By clearAddress_Button = By.Id("CWFieldButton_ClearAddress");
        readonly By addressSearch_Button = By.Id("CWFieldButton_AddressSearch");

        #endregion

        #region Finance Details Section Fields

        readonly By FinanceDetails_SectionTitle = By.XPath("//*[@id='CWSection_FinanceDetails']/fieldset/div/span[text()='Finance Details']");

        readonly By registeredno_Field = By.Id("CWField_registeredno");
        readonly By cpsuspenddebtorinvoices_YesRadioButton = By.Id("CWField_cpsuspenddebtorinvoices_1");
        readonly By cpsuspenddebtorinvoices_NoRadioButton = By.Id("CWField_cpsuspenddebtorinvoices_0");
        readonly By vatregistrationnumber_Field = By.Id("CWField_vatregistrationnumber");
        readonly By preferreddocumentsdeliverymethodid_Field = By.Id("CWField_preferreddocumentsdeliverymethodid");

        readonly By debtornumber1_Field = By.Id("CWField_debtornumber1");
        readonly By cpsuspenddebtorinvoicesreasonid_lookupButton = By.Id("CWLookupBtn_cpsuspenddebtorinvoicesreasonid");
        readonly By financereferencecode_Field = By.Id("CWField_financereferencecode");
        readonly By financecode_LookupButton = By.Id("CWLookupBtn_financecodeid");

        readonly By creditorNo_Field = By.Id("CWField_creditornumber");

        #endregion

        #region Bank Details Section Fields

        readonly By BankDetails_SectionTitle = By.XPath("//*[@id='CWSection_BankDetails']/fieldset/div/span[text()='Bank Details']");

        readonly By bankid_lookupButton = By.Id("CWLookupBtn_bankid");
        readonly By bankaccountnumber_Field = By.Id("CWField_bankaccountnumber");
        readonly By banksortcode_Field = By.Id("CWField_banksortcode");
        readonly By bankaccountname_LookupButton = By.Id("CWField_bankaccountname");

        #endregion

        #region Classification Section Fields

        readonly By Classification_SectionTitle = By.XPath("//*[@id='CWSection_Classification']/fieldset/div/span[text()='Classification']");

        readonly By providerhomelocationtypeid_lookupButton = By.Id("CWLookupBtn_providerhomelocationtypeid");
        readonly By providerhometypeid_lookupButton = By.Id("CWLookupBtn_providerhometypeid");
        readonly By nursingregistrationauthority_Field = By.Id("CWField_nursingregistrationauthority");
        readonly By residentialregistrationauthority_Field = By.Id("CWField_residentialregistrationauthority");

        #endregion

        #region Notes Section Fields

        readonly By Notes_SectionTitle = By.XPath("//*[@id='CWSection_Notes']/fieldset/div/span[text()='Notes']");

        #endregion

        #region Facilities Section Fields

        readonly By advocacypolicyid_Field = By.Id("CWField_advocacypolicyid");
        readonly By ensuiteroomsid_Field = By.Id("CWField_ensuiteroomsid");
        readonly By londonwidecontractid_Field = By.Id("CWField_londonwidecontractid");
        readonly By visitingchiropodistid_Field = By.Id("CWField_visitingchiropodistid");
        readonly By visitingdentistid_Field = By.Id("CWField_visitingdentistid");
        readonly By doublesRoom_Field = By.Id("CWField_doubleroomsid");
        readonly By gendersaccommodatedid_Field = By.Id("CWField_gendersaccommodatedid");
        readonly By singleRoom_Field = By.Id("CWField_singleroomsid");
        readonly By visitingopticianid_Field = By.Id("CWField_visitingopticianid");
        readonly By maximumnoofplaces_Field = By.Id("CWField_maximumnoofplaces");
        readonly By notesText_Field = By.Id("CWField_notes");

        #endregion

        #region Annual Leave Entitlement Section Fields

        readonly By calculateAnnualLeaveForEmployees_YesRadioButton = By.Id("CWField_calculateannualleaveforemployees_1");
        readonly By calculateAnnualLeaveForEmployees_NoRadioButton = By.Id("CWField_calculateannualleaveforemployees_0");

        #endregion

        #region Left Sub Menu

        readonly By activitiesDetailsElementExpanded = By.XPath("//span[text()='Activities']/parent::div/parent::summary/parent::details[@open]");
        readonly By activitiesLeftSubMenu = By.XPath("//span[text()='Activities']");

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//span[text()='Related Items']");

        readonly By otherInformationDetailsElementExpanded = By.XPath("//span[text()='Other Information']/parent::div/parent::summary/parent::details[@open]");
        readonly By otherInformationLeftSubMenu = By.XPath("//span[text()='Other Information']");

        readonly By qualityAssuranceDetailsElementExpanded = By.XPath("//span[text()='Quality Assurance']/parent::div/parent::summary/parent::details[@open]");
        readonly By qualityAssuranceLeftSubMenu = By.XPath("//span[text()='Quality Assurance']");

        readonly By childProviderLeftSubMenu = By.Id("CWNavItem_ChildProviders");

        #endregion

        By RecordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By RecordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']");


        public ProvidersRecordPage WaitForProvidersRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(ProviderRecordIFrame);
            SwitchToIframe(ProviderRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(pageHeader);
            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }

        public ProvidersRecordPage WaitForChildProvidersRecordPageToLoad(string RecordId)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(ChildProviderRecordIFrame(RecordId));
            SwitchToIframe(ChildProviderRecordIFrame(RecordId));

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);

            return this;
        }

        public ProvidersRecordPage WaitForProvidersRecordPageFromAdvancedSearchToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ProviderRecordIFrame);
            SwitchToIframe(ProviderRecordIFrame);

            WaitForElement(pageHeader);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }

        public ProvidersRecordPage WaitForProvidersSchedulingBookingTypesSectionToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(ProviderRecordIFrame);
            SwitchToIframe(ProviderRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(ProviderAllowableBookingTypesIFrame);
            SwitchToIframe(ProviderAllowableBookingTypesIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }


        public ProvidersRecordPage ClickSaveButton()
        {
            WaitForElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public ProvidersRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ProvidersRecordPage InsertName(string NameToInsert)
        {
            SendKeys(name_Field, NameToInsert);

            return this;
        }

        public ProvidersRecordPage SelectProviderType(String OptionToSelect)
        {
            WaitForElementVisible(providertypeid_Field);
            SelectPicklistElementByText(providertypeid_Field, OptionToSelect);

            return this;
        }

        public ProvidersRecordPage InsertStreetName(string streetnameToInsert)
        {
            ScrollToElement(street_Field);
            WaitForElementToBeClickable(street_Field);
            SendKeys(street_Field, streetnameToInsert);

            return this;
        }

        public ProvidersRecordPage InsertContactHours(string contacthoursToInsert)
        {
            ScrollToElement(contactHours_Field);
            WaitForElementToBeClickable(contactHours_Field);
            SendKeys(contactHours_Field, contacthoursToInsert);

            return this;
        }

        public ProvidersRecordPage InsertNotesText(string notestextToInsert)
        {
            SendKeys(notesText_Field, notestextToInsert);

            return this;
        }

        public ProvidersRecordPage InsertMaximumNoOfPlaces(string MaximumNoOfPlacesToInsert)
        {
            SendKeys(maximumnoofplaces_Field, MaximumNoOfPlacesToInsert);

            return this;
        }

        public ProvidersRecordPage SelectVisitingOpticianId(String OptionToSelect)
        {
            WaitForElementVisible(visitingopticianid_Field);
            SelectPicklistElementByText(visitingopticianid_Field, OptionToSelect);

            return this;
        }

        public ProvidersRecordPage SelectSingleRoomId(String OptionToSelect)
        {
            WaitForElementVisible(singleRoom_Field);
            SelectPicklistElementByText(singleRoom_Field, OptionToSelect);

            return this;
        }

        public ProvidersRecordPage SelectGendersAccommodatedId(String OptionToSelect)
        {
            WaitForElementVisible(gendersaccommodatedid_Field);
            SelectPicklistElementByText(gendersaccommodatedid_Field, OptionToSelect);

            return this;
        }

        public ProvidersRecordPage SelectDoublesRoom(String OptionToSelect)
        {
            WaitForElementVisible(doublesRoom_Field);
            SelectPicklistElementByText(doublesRoom_Field, OptionToSelect);

            return this;
        }

        public ProvidersRecordPage SelectVistingDentistId(String OptionToSelect)
        {
            WaitForElementVisible(visitingdentistid_Field);
            SelectPicklistElementByText(visitingdentistid_Field, OptionToSelect);

            return this;
        }

        public ProvidersRecordPage SelectVistingChiropodistId(String OptionToSelect)
        {
            WaitForElementVisible(visitingchiropodistid_Field);
            SelectPicklistElementByText(visitingchiropodistid_Field, OptionToSelect);

            return this;
        }

        public ProvidersRecordPage SelectLondonWideContractId(String OptionToSelect)
        {
            WaitForElementVisible(londonwidecontractid_Field);
            SelectPicklistElementByText(londonwidecontractid_Field, OptionToSelect);

            return this;
        }

        public ProvidersRecordPage SelectEnsuiteRoomsId(String OptionToSelect)
        {
            WaitForElementVisible(ensuiteroomsid_Field);
            SelectPicklistElementByText(ensuiteroomsid_Field, OptionToSelect);

            return this;
        }

        public ProvidersRecordPage SelectAdvocacyPolicyId(String OptionToSelect)
        {
            WaitForElementVisible(advocacypolicyid_Field);
            SelectPicklistElementByText(advocacypolicyid_Field, OptionToSelect);

            return this;
        }

        public ProvidersRecordPage ClickContactMethodeLookupButton()
        {
            ScrollToElement(contactmethodid_LookupButton);
            WaitForElementToBeClickable(contactmethodid_LookupButton);
            Click(contactmethodid_LookupButton);

            return this;
        }

        public ProvidersRecordPage ClickAddressPropertyTypeLookupButton()
        {
            ScrollToElement(addresspropertytypeid_LookupButton);
            WaitForElementToBeClickable(addresspropertytypeid_LookupButton);
            Click(addresspropertytypeid_LookupButton);

            return this;
        }

        public ProvidersRecordPage InsertPostCodeNo(string postcodenoToInsert)
        {
            ScrollToElement(postcode_Field);
            WaitForElementToBeClickable(postcode_Field);
            SendKeys(postcode_Field, postcodenoToInsert);

            return this;
        }

        public ProvidersRecordPage InsertCountryName(string countrynameToInsert)
        {
            ScrollToElement(country_Field);
            WaitForElementToBeClickable(country_Field);
            SendKeys(country_Field, countrynameToInsert);

            return this;
        }

        public ProvidersRecordPage InsertAddressPhoneNo(string addressphonenoToInsert)
        {
            ScrollToElement(addressphone_Field);
            WaitForElementToBeClickable(addressphone_Field);
            SendKeys(addressphone_Field, addressphonenoToInsert);

            return this;
        }

        public ProvidersRecordPage InsertCounty(string countyToInsert)
        {
            ScrollToElement(county_Field);
            WaitForElementToBeClickable(county_Field);
            SendKeys(county_Field, countyToInsert);

            return this;
        }

        public ProvidersRecordPage InsertTownCityName(string towncitynameToInsert)
        {
            ScrollToElement(townCity_Field);
            WaitForElementToBeClickable(townCity_Field);
            SendKeys(townCity_Field, towncitynameToInsert);

            return this;
        }

        public ProvidersRecordPage InsertVlgDistrictName(string vlgdistrictnameToInsert)
        {
            ScrollToElement(vlgDistrict_Field);
            WaitForElementToBeClickable(vlgDistrict_Field);
            SendKeys(vlgDistrict_Field, vlgdistrictnameToInsert);

            return this;
        }

        public ProvidersRecordPage InsertAccountNumber(string AccountNumberToInsert)
        {
            SendKeys(accountnumber_Field, AccountNumberToInsert);

            return this;
        }

        public ProvidersRecordPage ClickAddressSearchButton()
        {
            ScrollToElement(addressSearch_Button);
            WaitForElementToBeClickable(addressSearch_Button);
            Click(addressSearch_Button);

            return this;
        }

        public ProvidersRecordPage ClickClearAddressButton()
        {
            ScrollToElement(clearAddress_Button);
            WaitForElementToBeClickable(clearAddress_Button);
            Click(clearAddress_Button);

            return this;
        }

        public ProvidersRecordPage ClickCommissionerOrganisationLookupButton()
        {
            WaitForElementToBeClickable(CommissionerOrganisation_LookupButton);
            Click(CommissionerOrganisation_LookupButton);

            return this;
        }

        public ProvidersRecordPage ClickRTTTreatmentFunctionLookupButton()
        {
            WaitForElementToBeClickable(RTTTreatmentFunction_LookupButton);
            Click(RTTTreatmentFunction_LookupButton);

            return this;
        }

        public ProvidersRecordPage ClickParentProviderLookupButton()
        {
            WaitForElementToBeClickable(parentproviderid_LookupButton);
            Click(parentproviderid_LookupButton);

            return this;
        }

        public ProvidersRecordPage ClickPrimaryContactLookupButton()
        {
            ScrollToElement(primarycontactid_LookupButton);
            WaitForElementToBeClickable(primarycontactid_LookupButton);
            Click(primarycontactid_LookupButton);

            return this;
        }

        public ProvidersRecordPage InsertCreditorNoField(string creditorNoToInsert)
        {
            ScrollToElement(creditorNo_Field);
            WaitForElementToBeClickable(creditorNo_Field);
            SendKeys(creditorNo_Field, creditorNoToInsert);
            SendKeysWithoutClearing(creditorNo_Field, Keys.Tab);

            return this;
        }

        public ProvidersRecordPage InsertCQCLocation(string cqclocationToInsert)
        {
            SendKeys(cqclocationid_Field, cqclocationToInsert);

            return this;
        }

        public ProvidersRecordPage InsertODSCode(string odscodeToInsert)
        {
            SendKeys(odscode_Field, odscodeToInsert);

            return this;
        }

        public ProvidersRecordPage InsertDescription(string descriptionToInsert)
        {
            SendKeys(description_Field, descriptionToInsert);

            return this;
        }

        public ProvidersRecordPage InsertMainPhoneNo(string mainphoneToInsert)
        {
            SendKeys(mainphone_Field, mainphoneToInsert);

            return this;
        }

        public ProvidersRecordPage InsertOtherPhoneNo(string otherphonenoToInsert)
        {
            SendKeys(otherphone_Field, otherphonenoToInsert);

            return this;
        }

        public ProvidersRecordPage InsertEmailId(string emailToInsert)
        {
            SendKeys(email_Field, emailToInsert);

            return this;
        }

        public ProvidersRecordPage InsertStartDate(string startdateToInsert)
        {
            SendKeys(startdate_Field, startdateToInsert);

            return this;
        }

        public ProvidersRecordPage InsertEndDate(string enddateToInsert)
        {
            SendKeys(enddate_Field, enddateToInsert);

            return this;
        }

        public ProvidersRecordPage InsertWebsite(string websiteToInsert)
        {
            SendKeys(website_Field, websiteToInsert);

            return this;
        }

        public ProvidersRecordPage InsertStartDateOfAddress(string startdateofaddressToInsert)
        {
            ScrollToElement(addressstartdate_Field);
            WaitForElementToBeClickable(addressstartdate_Field);
            SendKeys(addressstartdate_Field, startdateofaddressToInsert);

            return this;
        }

        public ProvidersRecordPage SelectAddressTypeId(String OptionToSelect)
        {
            WaitForElementVisible(addresstypeid_Field);
            ScrollToElement(addresstypeid_Field);
            SelectPicklistElementByText(addresstypeid_Field, OptionToSelect);

            return this;
        }

        public ProvidersRecordPage InsertPropertyName(string propertynameToInsert)
        {
            ScrollToElement(propertyname_Field);
            WaitForElementToBeClickable(propertyname_Field);
            SendKeys(propertyname_Field, propertynameToInsert);

            return this;
        }

        public ProvidersRecordPage InsertPropertyNo(string propertynoToInsert)
        {
            ScrollToElement(propertyNo_Field);
            WaitForElementToBeClickable(propertyNo_Field);
            SendKeys(propertyNo_Field, propertynoToInsert);

            return this;
        }

        public ProvidersRecordPage ValidateNameErrorLabelText(string ExpectedText)
        {
            ValidateElementText(name_FieldErrorLabel, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ValidateProviderTypeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(providerType_FieldErrorLabel, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ValidateProviderTypeFieldValue(string ExpectedValue)
        {
            //ValidateElementValue(providertypeid_Field, ExpectedValue);
            //ValidatePicklistSelectedText(providertypeid_Field, ExpectedValue);
            ValidatePicklistContainsElementByText(providertypeid_Field, ExpectedValue);

            return this;
        }

        public ProvidersRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);
            System.Threading.Thread.Sleep(2000);

            return this;

        }

        public ProvidersRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public ProvidersRecordPage WaitForRecordToBeSaved(string providerName)
        {
            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementNotVisible(SaveButton, 20);
            WaitForElementNotVisible(SaveAndCloseButton, 10);
            WaitForElementVisible(additionalToolbarElementsButton);
            ValidateElementTextContainsText(recordHeaderTitle, providerName);

            return this;
        }


        #region General Section

        public ProvidersRecordPage ValidateGeneralSectionTitleVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(General_SectionTitle);
                WaitForElementVisible(General_SectionTitle);
            }
            else
            {
                WaitForElementNotVisible(General_SectionTitle, 3);
            }
            return this;
        }

        public ProvidersRecordPage ValidateGeneralSectionFieldsVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(Id_Field);
                WaitForElementVisible(Id_Field);
                ScrollToElement(name_Field);
                WaitForElementVisible(name_Field);
                ScrollToElement(providertypeid_Field);
                WaitForElementVisible(providertypeid_Field);
                ScrollToElement(parentproviderid_LookupButton);
                WaitForElementVisible(parentproviderid_LookupButton);
                ScrollToElement(cqclocationid_Field);
                WaitForElementVisible(cqclocationid_Field);
                ScrollToElement(description_Field);
                WaitForElementVisible(description_Field);

                ScrollToElement(ResponsibleTeam_LinkField);
                WaitForElementVisible(ResponsibleTeam_LinkField);
                ScrollToElement(startdate_Field);
                WaitForElementVisible(startdate_Field);
                ScrollToElement(enddate_Field);
                WaitForElementVisible(enddate_Field);
                ScrollToElement(EnableScheduling_YesRadioButton);
                WaitForElementVisible(EnableScheduling_YesRadioButton);
                ScrollToElement(EnableScheduling_NoRadioButton);
                WaitForElementVisible(EnableScheduling_NoRadioButton);
                
                //this field is being hidden because of this story ACC-6847
                //ScrollToElement(DefaultHolidayYear_LookupButton); 
                //WaitForElementVisible(DefaultHolidayYear_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(Id_Field, 3);
                WaitForElementNotVisible(name_Field, 3);
                WaitForElementNotVisible(providertypeid_Field, 3);
                WaitForElementNotVisible(parentproviderid_LookupButton, 3);
                WaitForElementNotVisible(cqclocationid_Field, 3);
                WaitForElementNotVisible(description_Field, 3);

                WaitForElementNotVisible(ResponsibleTeam_LinkField, 3);
                WaitForElementNotVisible(startdate_Field, 3);
                WaitForElementNotVisible(enddate_Field, 3);
                WaitForElementNotVisible(EnableScheduling_YesRadioButton, 3);
                WaitForElementNotVisible(EnableScheduling_NoRadioButton, 3);
                WaitForElementNotVisible(DefaultHolidayYear_LookupButton, 3);
            }
            return this;
        }

        #endregion

        #region Contact Section

        public ProvidersRecordPage ValidateContactSectionTitleVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(Contact_SectionTitle);
                WaitForElementVisible(Contact_SectionTitle);
            }
            else
            {
                WaitForElementNotVisible(Contact_SectionTitle, 3);
            }
            return this;
        }

        public ProvidersRecordPage ValidateContactSectionFieldsVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(primarycontactid_LookupButton);
                WaitForElementVisible(primarycontactid_LookupButton);
                ScrollToElement(mainphone_Field);
                WaitForElementVisible(mainphone_Field);
                ScrollToElement(website_Field);
                WaitForElementVisible(website_Field);

                ScrollToElement(otherphone_Field);
                WaitForElementVisible(otherphone_Field);
                ScrollToElement(email_Field);
                WaitForElementVisible(email_Field);
            }
            else
            {
                WaitForElementNotVisible(primarycontactid_LookupButton, 3);
                WaitForElementNotVisible(mainphone_Field, 3);
                WaitForElementNotVisible(website_Field, 3);
                WaitForElementNotVisible(otherphone_Field, 3);
                WaitForElementNotVisible(email_Field, 3);
            }
            return this;
        }

        #endregion

        #region Address Section

        public ProvidersRecordPage ValidateAddressSectionTitleVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(Address_SectionTitle);
                WaitForElementVisible(Address_SectionTitle);
            }
            else
            {
                WaitForElementNotVisible(Address_SectionTitle, 3);
            }
            return this;
        }


        public ProvidersRecordPage ValidateAddressSectionFieldsVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(addressstartdate_Field);
                WaitForElementVisible(addressstartdate_Field);
                ScrollToElement(addresstypeid_Field);
                WaitForElementVisible(addresstypeid_Field);
                ScrollToElement(propertyname_Field);
                WaitForElementVisible(propertyname_Field);
                ScrollToElement(propertyNo_Field);
                WaitForElementVisible(propertyNo_Field);
                ScrollToElement(street_Field);
                WaitForElementVisible(street_Field);
                ScrollToElement(vlgDistrict_Field);
                WaitForElementVisible(vlgDistrict_Field);
                ScrollToElement(townCity_Field);
                WaitForElementVisible(townCity_Field);
                ScrollToElement(county_Field);
                WaitForElementVisible(county_Field);
                ScrollToElement(postcode_Field);
                WaitForElementVisible(postcode_Field);

                ScrollToElement(addresspropertytypeid_LookupButton);
                WaitForElementVisible(addresspropertytypeid_LookupButton);
                ScrollToElement(country_Field);
                WaitForElementVisible(country_Field);
                ScrollToElement(addressphone_Field);
                WaitForElementVisible(addressphone_Field);
                ScrollToElement(contactHours_Field);
                WaitForElementVisible(contactHours_Field);
                ScrollToElement(contactmethodid_LookupButton);
                WaitForElementVisible(contactmethodid_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(addressstartdate_Field, 3);
                WaitForElementNotVisible(addresstypeid_Field, 3);
                WaitForElementNotVisible(propertyname_Field, 3);
                WaitForElementNotVisible(propertyNo_Field, 3);
                WaitForElementNotVisible(street_Field, 3);
                WaitForElementNotVisible(vlgDistrict_Field, 3);
                WaitForElementNotVisible(townCity_Field, 3);
                WaitForElementNotVisible(county_Field, 3);
                WaitForElementNotVisible(postcode_Field, 3);

                WaitForElementNotVisible(addresspropertytypeid_LookupButton, 3);
                WaitForElementNotVisible(country_Field, 3);
                WaitForElementNotVisible(addressphone_Field, 3);
                WaitForElementNotVisible(contactHours_Field, 3);
                WaitForElementNotVisible(contactmethodid_LookupButton, 3);
            }
            return this;
        }

        #endregion

        #region Finance Details Section

        public ProvidersRecordPage ValidateFinanceDetailsSectionTitleVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(FinanceDetails_SectionTitle);
                WaitForElementVisible(FinanceDetails_SectionTitle);
            }
            else
            {
                WaitForElementNotVisible(FinanceDetails_SectionTitle, 3);
            }
            return this;
        }

        public ProvidersRecordPage ValidateFinanceDetailsSectionFieldsVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(registeredno_Field);
                WaitForElementVisible(registeredno_Field);
                ScrollToElement(cpsuspenddebtorinvoices_YesRadioButton);
                WaitForElementVisible(cpsuspenddebtorinvoices_YesRadioButton);
                ScrollToElement(cpsuspenddebtorinvoices_NoRadioButton);
                WaitForElementVisible(cpsuspenddebtorinvoices_NoRadioButton);
                ScrollToElement(vatregistrationnumber_Field);
                WaitForElementVisible(vatregistrationnumber_Field);
                ScrollToElement(preferreddocumentsdeliverymethodid_Field);
                WaitForElementVisible(preferreddocumentsdeliverymethodid_Field);

                ScrollToElement(debtornumber1_Field);
                WaitForElementVisible(debtornumber1_Field);
                ScrollToElement(cpsuspenddebtorinvoicesreasonid_lookupButton);
                WaitForElementVisible(cpsuspenddebtorinvoicesreasonid_lookupButton);
                ScrollToElement(financereferencecode_Field);
                WaitForElementVisible(financereferencecode_Field);
                ScrollToElement(financecode_LookupButton);
                WaitForElementVisible(financecode_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(registeredno_Field, 3);
                WaitForElementNotVisible(cpsuspenddebtorinvoices_YesRadioButton, 3);
                WaitForElementNotVisible(cpsuspenddebtorinvoices_NoRadioButton, 3);
                WaitForElementNotVisible(vatregistrationnumber_Field, 3);
                WaitForElementNotVisible(preferreddocumentsdeliverymethodid_Field, 3);

                WaitForElementNotVisible(debtornumber1_Field, 3);
                WaitForElementNotVisible(cpsuspenddebtorinvoicesreasonid_lookupButton, 3);
                WaitForElementNotVisible(financereferencecode_Field, 3);
                WaitForElementNotVisible(financecode_LookupButton, 3);
            }
            return this;
        }

        public ProvidersRecordPage ClickSuspendDebtorInvoicesYesRadioButton()
        {
            ScrollToElement(cpsuspenddebtorinvoices_YesRadioButton);
            WaitForElementToBeClickable(cpsuspenddebtorinvoices_YesRadioButton);
            Click(cpsuspenddebtorinvoices_YesRadioButton);

            return this;
        }

        public ProvidersRecordPage ClickSuspendDebtorInvoicesNoRadioButton()
        {
            ScrollToElement(cpsuspenddebtorinvoices_NoRadioButton);
            WaitForElementToBeClickable(cpsuspenddebtorinvoices_NoRadioButton);
            Click(cpsuspenddebtorinvoices_NoRadioButton);

            return this;
        }

        public ProvidersRecordPage ClickSuspendDebtorInvoicesReasonLookupButton()
        {
            ScrollToElement(cpsuspenddebtorinvoicesreasonid_lookupButton);
            WaitForElementToBeClickable(cpsuspenddebtorinvoicesreasonid_lookupButton);
            Click(cpsuspenddebtorinvoicesreasonid_lookupButton);

            return this;
        }

        public ProvidersRecordPage ClickFinanceCodeLookupButton()
        {
            ScrollToElement(financecode_LookupButton);
            WaitForElementToBeClickable(financecode_LookupButton);
            Click(financecode_LookupButton);

            return this;
        }

        public ProvidersRecordPage ValidateSuspendDebtorInvoicesReasonLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ScrollToElement(cpsuspenddebtorinvoicesreasonid_lookupButton);
                ValidateElementDisabled(cpsuspenddebtorinvoicesreasonid_lookupButton);

            }
            else
            {
                ValidateElementNotDisabled(cpsuspenddebtorinvoicesreasonid_lookupButton);
            }
            return this;
        }

        #endregion

        #region Bank Details Section

        public ProvidersRecordPage ValidateBankDetailsSectionTitleVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(BankDetails_SectionTitle);
                WaitForElementVisible(BankDetails_SectionTitle);
            }
            else
            {
                WaitForElementNotVisible(BankDetails_SectionTitle, 3);
            }
            return this;
        }

        public ProvidersRecordPage ValidateBankDetailsSectionFieldsVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(bankid_lookupButton);
                WaitForElementVisible(bankid_lookupButton);
                ScrollToElement(bankaccountnumber_Field);
                WaitForElementVisible(bankaccountnumber_Field);
                ScrollToElement(banksortcode_Field);
                WaitForElementVisible(banksortcode_Field);
                ScrollToElement(bankaccountname_LookupButton);
                WaitForElementVisible(bankaccountname_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(bankid_lookupButton, 3);
                WaitForElementNotVisible(bankaccountnumber_Field, 3);
                WaitForElementNotVisible(banksortcode_Field, 3);
                WaitForElementNotVisible(bankaccountname_LookupButton, 3);
            }
            return this;
        }

        #endregion

        #region Classification Section

        public ProvidersRecordPage ValidateClassificationSectionTitleVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(Classification_SectionTitle);
                WaitForElementVisible(Classification_SectionTitle);
            }
            else
            {
                WaitForElementNotVisible(Classification_SectionTitle, 3);
            }
            return this;
        }

        public ProvidersRecordPage ValidateClassificationSectionFieldsVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(providerhomelocationtypeid_lookupButton);
                WaitForElementVisible(providerhomelocationtypeid_lookupButton);
                ScrollToElement(providerhometypeid_lookupButton);
                WaitForElementVisible(providerhometypeid_lookupButton);
                ScrollToElement(nursingregistrationauthority_Field);
                WaitForElementVisible(nursingregistrationauthority_Field);
                ScrollToElement(residentialregistrationauthority_Field);
                WaitForElementVisible(residentialregistrationauthority_Field);
            }
            else
            {
                WaitForElementNotVisible(providerhomelocationtypeid_lookupButton, 3);
                WaitForElementNotVisible(providerhometypeid_lookupButton, 3);
                WaitForElementNotVisible(nursingregistrationauthority_Field, 3);
                WaitForElementNotVisible(residentialregistrationauthority_Field, 3);
            }
            return this;
        }

        #endregion

        #region Notes Section

        public ProvidersRecordPage ValidateNotesSectionTitleVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(Notes_SectionTitle);
                WaitForElementVisible(Notes_SectionTitle);
            }
            else
            {
                WaitForElementNotVisible(Notes_SectionTitle, 3);
            }
            return this;
        }

        #endregion

        #region Record Details Section

        public ProvidersRecordPage ValidatePropertyNameFieldValue(string ExpectedText)
        {
            ScrollToElement(propertyname_Field);
            ValidateElementValueByJavascript(propertyname_FieldName, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ValidatePropertyNoFieldValue(string ExpectedText)
        {
            ScrollToElement(propertyNo_Field);
            ValidateElementValueByJavascript(propertyno_FieldName, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ValidateStreetFieldValue(string ExpectedText)
        {
            ScrollToElement(street_Field);
            ValidateElementValueByJavascript(street_FieldName, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ValidateVillageDistrictFieldValue(string ExpectedText)
        {
            ScrollToElement(vlgDistrict_Field);
            ValidateElementValueByJavascript(villageDistrict_FieldName, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ValidateTownCityFieldValue(string ExpectedText)
        {
            ScrollToElement(townCity_Field);
            ValidateElementValueByJavascript(townCity_FieldName, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ValidatePostcodeFieldValue(string ExpectedText)
        {
            ScrollToElement(postcode_Field);
            ValidateElementValueByJavascript(postcode_FieldName, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ValidateCountyFieldValue(string ExpectedText)
        {
            ScrollToElement(county_Field);
            ValidateElementValueByJavascript(county_FieldName, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ValidateCountryFieldValue(string ExpectedText)
        {
            ScrollToElement(country_Field);
            ValidateElementValueByJavascript(country_FieldName, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ClickDetailsTab()
        {
            WaitForElementToBeClickable(detailsTab);
            ScrollToElement(detailsTab);
            Click(detailsTab);
            return this;
        }

        public ProvidersRecordPage ClickContractServicesTab()
        {
            WaitForElementToBeClickable(contractServicesTab);
            ScrollToElement(contractServicesTab);
            Click(contractServicesTab);
            return this;
        }

        public ProvidersRecordPage ClickContractServicesWithRatesTab()
        {
            WaitForElementToBeClickable(contractServicesWithRatesTab);
            ScrollToElement(contractServicesWithRatesTab);
            Click(contractServicesWithRatesTab);
            return this;
        }

        public ProvidersRecordPage ClickFinanceTransactionsTab()
        {
            WaitForElementToBeClickable(financeTransactionsTab);
            ScrollToElement(financeTransactionsTab);
            Click(financeTransactionsTab);
            return this;
        }

        public ProvidersRecordPage ValidateTimelineTabIsDisplayed()
        {
            WaitForElementVisible(timelineTab);

            return this;
        }

        public ProvidersRecordPage ValidateSummaryTabIsDisplayed()
        {
            WaitForElementVisible(summaryTab);
            return this;
        }

        public ProvidersRecordPage ValidateDetailsTabIsDisplayed()
        {
            WaitForElementVisible(detailsTab);
            return this;
        }

        public ProvidersRecordPage ValidateNameFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(name_Field);
            ValidateElementValue(name_Field, ExpectedValue);
            return this;
        }

        public ProvidersRecordPage ValidateNameFieldText(string ExpectedText)
        {
            WaitForElement(name_Field);
            ValidateElementText(name_Field, ExpectedText);
            return this;
        }

        public ProvidersRecordPage ValidateSelectedProviderTypeValue(String expectedtext)
        {
            ValidatePicklistSelectedText(providertypeid_Field, expectedtext);
            return this;
        }

        public ProvidersRecordPage ValidateDescriptionFieldText(string ExpectedText)
        {
            WaitForElement(description_Field);
            ValidateElementText(description_Field, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ValidatePrimaryContactFieldText(string ExpectedText)
        {
            WaitForElement(primaryContact_LinkField);
            ValidateElementText(primaryContact_LinkField, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ValidateMainPhoneFieldValue(string ExpectedValue)
        {
            WaitForElement(mainphone_Field);
            ValidateElementValue(mainphone_Field, ExpectedValue);
            return this;
        }

        public ProvidersRecordPage ValidateMainPhoneFieldText(string ExpectedText)
        {
            WaitForElement(mainphone_Field);
            ValidateElementText(mainphone_Field, ExpectedText);
            return this;
        }

        public ProvidersRecordPage ValidateOtherPhoneFieldValue(string ExpectedValue)
        {
            WaitForElement(otherphone_Field);
            ValidateElementValue(otherphone_Field, ExpectedValue);

            return this;
        }

        public ProvidersRecordPage ValidateWebsiteFieldText(string ExpectedText)
        {
            WaitForElement(website_LinkField);
            ValidateElementTextContainsText(website_LinkField, ExpectedText);
            return this;
        }

        public ProvidersRecordPage ValidateEmailFieldValue(string ExpectedValue)
        {
            WaitForElement(email_Field);
            ValidateElementValue(email_Field, ExpectedValue);

            return this;
        }

        public ProvidersRecordPage ValidateNotesFieldText(string ExpectedText)
        {
            WaitForElement(notesText_Field);
            ValidateElementText(notesText_Field, ExpectedText);

            return this;
        }

        public ProvidersRecordPage ValidateStartDateFieldValue(string ExpectedValue)
        {
            WaitForElement(startdate_Field);
            ValidateElementValue(startdate_Field, ExpectedValue);

            return this;
        }

        public ProvidersRecordPage ValidateEndDateFieldValue(string ExpectedValue)
        {
            WaitForElement(enddate_Field);
            ValidateElementValue(enddate_Field, ExpectedValue);

            return this;
        }

        public ProvidersRecordPage ClickOnMenuAndValidateSubMenus()
        {
            Click(MenuButton);

            bool activitySubMenu = GetElementVisibility(activitiesLeftSubMenu);
            Assert.IsTrue(activitySubMenu);

            bool relatedItemsSubMenu = GetElementVisibility(relatedItemsLeftSubMenu);
            Assert.IsTrue(relatedItemsSubMenu);

            bool qualityAssuranceSubMenu = GetElementVisibility(qualityAssuranceLeftSubMenu);
            Assert.IsTrue(qualityAssuranceSubMenu);

            bool otherInformationSubMenu = GetElementVisibility(otherInformationLeftSubMenu);
            Assert.IsTrue(otherInformationSubMenu);

            return this;
        }

        public ProvidersRecordPage ValidateNameFieldVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(name_Field);
                Assert.IsTrue(GetElementVisibility(name_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(name_Field));
            }
            return this;
        }

        public ProvidersRecordPage ValidateProviderTypeFieldVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(providertypeid_Field);
                Assert.IsTrue(GetElementVisibility(providertypeid_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(providertypeid_Field));
            }
            return this;
        }

        public ProvidersRecordPage ValidateResponsibleTeamLinkFieldVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(ResponsibleTeam_LinkField);
                Assert.IsTrue(GetElementVisibility(ResponsibleTeam_LinkField));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(ResponsibleTeam_LinkField));
            }
            return this;
        }

        public ProvidersRecordPage ValidateAccountNumberFieldVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(accountnumber_Field);
                Assert.IsTrue(GetElementVisibility(accountnumber_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(accountnumber_Field));
            }
            return this;
        }

        public ProvidersRecordPage ValidateParentProviderLookupVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(parentproviderid_LookupButton);
                Assert.IsTrue(GetElementVisibility(parentproviderid_LookupButton));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(parentproviderid_LookupButton));
            }
            return this;
        }

        public ProvidersRecordPage ValidateCqcLocationIdFieldVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(cqclocationid_Field);
                Assert.IsTrue(GetElementVisibility(cqclocationid_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(cqclocationid_Field));
            }
            return this;
        }

        public ProvidersRecordPage ValidateOdsCodeFieldVisible(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(odscode_Field);
                Assert.IsTrue(GetElementVisibility(odscode_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(odscode_Field));
            }
            return this;
        }

        public ProvidersRecordPage ClickEnableSchedulingRadioButton(bool clickYesOption)
        {
            if (clickYesOption)
            {
                ScrollToElement(EnableScheduling_YesRadioButton);
                WaitForElementToBeClickable(EnableScheduling_YesRadioButton);
                Click(EnableScheduling_YesRadioButton);
            }
            else
            {
                ScrollToElement(EnableScheduling_NoRadioButton);
                WaitForElementToBeClickable(EnableScheduling_NoRadioButton);
                Click(EnableScheduling_NoRadioButton);
            }

            return this;
        }

        public ProvidersRecordPage ValidateEnableSchedulingFieldIsVisible()
        {
            WaitForElementVisible(EnableScheduling_LabelField);
            WaitForElementVisible(EnableScheduling_MandatoryField);
            WaitForElementVisible(EnableScheduling_YesRadioButton);
            ScrollToElement(EnableScheduling_NoRadioButton);

            return this;
        }

        public ProvidersRecordPage ValidateEnableScheduling_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(EnableScheduling_YesRadioButton);
            WaitForElementVisible(EnableScheduling_NoRadioButton);
            ScrollToElement(EnableScheduling_NoRadioButton);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(EnableScheduling_YesRadioButton);
                ValidateElementNotChecked(EnableScheduling_NoRadioButton);
            }
            else
            {
                ValidateElementChecked(EnableScheduling_NoRadioButton);
                ValidateElementNotChecked(EnableScheduling_YesRadioButton);
            }

            return this;
        }

        public ProvidersRecordPage NavigateToChildProvider()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }
            
            WaitForElementToBeClickable(childProviderLeftSubMenu);
            Click(childProviderLeftSubMenu);

            return this;
        }

        public ProvidersRecordPage SelectRecord(string RecordId)
        {
            WaitForElement(RecordRowCheckBox(RecordId));
            Click(RecordRowCheckBox(RecordId));

            return this;
        }

        public ProvidersRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            ScrollToElement(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public ProvidersRecordPage OpenRecord(string RecordId)
        {
            WaitForElementToBeClickable(RecordRow(RecordId));
            ScrollToElement(RecordRow(RecordId));
            Click(RecordRow(RecordId));

            return this;
        }

        public ProvidersRecordPage ValidateRecordIsPresent(string RecordId, bool IsPresent)
        {
            if (IsPresent)
                WaitForElementVisible(RecordRow(RecordId));
            else
                WaitForElementNotVisible(RecordRow(RecordId), 3);

            return this;
        }

        #endregion

        #region Annual Leave Entitlement Section

        public ProvidersRecordPage ClickCalculateAnnualLeaveForEmployeesYesRadioButton()
        {
            ScrollToElement(calculateAnnualLeaveForEmployees_YesRadioButton);
            WaitForElementToBeClickable(calculateAnnualLeaveForEmployees_YesRadioButton);
            Click(calculateAnnualLeaveForEmployees_YesRadioButton);

            return this;
        }

        public ProvidersRecordPage ClickCalculateAnnualLeaveForEmployeesNoRadioButton()
        {
            ScrollToElement(calculateAnnualLeaveForEmployees_NoRadioButton);
            WaitForElementToBeClickable(calculateAnnualLeaveForEmployees_NoRadioButton);
            Click(calculateAnnualLeaveForEmployees_NoRadioButton);

            return this;
        }

        #endregion
    }
}
