
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonRecordNewPage : CommonMethods
    {
        public PersonRecordNewPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame"); 

        readonly By iframe_CWNewPerson = By.Id("iframe_CWNewPerson");

        By personRecordIFrame(string PersonID) => By.Id("iframe_CWDialog_" + PersonID + "_Edit"); 

        
        #region Top Menu

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By printButton = By.Id("TI_PrintDetailsButton");
        readonly By shareButton = By.Id("TI_ShareRecordButton");
        readonly By additionalIItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By runOnDemandWorkflowButton = By.Id("TI_RunOnDemandWorkflow");
        readonly By copyRecordLinkButton = By.Id("TI_CopyRecordLink");

        #endregion

        readonly By PageTitle = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Person: ']/span");
        
        readonly By NotificationMessage = By.XPath("//*[@id='CWNotificationMessage_DataForm']");


        #region Fields

        readonly By personType_Field = By.Id("CWField_persontypeid");
        readonly By title_Field = By.Id("CWField_title");
        readonly By firstName_Field = By.Id("CWField_firstname");
        readonly By middleName_Field = By.Id("CWField_middlename");
        readonly By lastName_Field = By.Id("CWField_lastname");
        readonly By lastName_ErrorLabel = By.XPath("//*[@id='CWControlHolder_lastname']/label");
        readonly By statedGender_Field = By.Id("CWField_genderid");
        readonly By statedGender_ErrorLabel = By.XPath("//*[@id='CWControlHolder_genderid']/label/span");
        readonly By sameAsBirthGender_Field = By.Id("CWField_sameasbirthgenderid_cwname");
        readonly By sameAsBirthGender_LookUpButton = By.Id("CWLookupBtn_sameasbirthgenderid");
        readonly By DOBAndAge_Field = By.Id("CWField_dobandagetypeid");
        readonly By dateOfBirth_Field = By.Id("CWField_dateofbirth");
        readonly By DOB_ErrorLabel = By.XPath("//*[@id='CWControlHolder_dateofbirth']/label/span");
        readonly By ethnicity_Field = By.Id("CWLookupBtn_ethnicityid");
        readonly By ethnicity_ErrorLabel = By.XPath("//*[@id='CWControlHolder_ethnicityid']/label/span");
        readonly By personPhoto_Field = By.Id("CWField_personphotoid");
        readonly By nHSNo_Field = By.Id("CWField_nhsnumber");
        readonly By nHSNoReason_Field = By.Id("CWField_nonhsnumberreasonid_cwname");
        readonly By nHSNoReason_LookUpButton = By.Id("CWLookupBtn_nonhsnumberreasonid");
        readonly By maritalStatus_Field = By.Id("CWField_maritalstatusid_cwname");
        readonly By maritalStatus_LookUpButton = By.Id("CWLookupBtn_maritalstatusid");


        readonly By deceased_FieldLabel = By.XPath("//*[@id='CWLabelHolder_deceased']/label");
        readonly By deceased_YesOption = By.Id("CWField_deceased_1");
        readonly By deceased_NoOption = By.Id("CWField_deceased_0");
        readonly By causeOfDeath_Field = By.Id("CWField_causeofdeathid_cwname");
        readonly By dateOfDeath_Field = By.Id("CWField_dateofdeath");
        readonly By placeOfDeath_Field = By.Id("CWField_placeofdeath");


        readonly By addressType_Field = By.Id("CWField_addresstypeid");
        readonly By addressType_ErrorLabel = By.XPath("//*[@id='CWControlHolder_addresstypeid']/label/span");
        readonly By addressStartDate_Field = By.Id("CWField_addressstartdate");
        readonly By propertyType_Field = By.Id("CWField_addresspropertytypeid_cwname");
        readonly By propertyType_LookUpButton = By.Id("CWLookupBtn_addresspropertytypeid");
        readonly By propertyName_Field = By.Id("CWField_propertyname");
        readonly By propertyNo_Field = By.Id("CWField_addressline1");
        readonly By street_Field = By.Id("CWField_addressline2");
        readonly By village_Field = By.Id("CWField_addressline3");
        readonly By townCity_Field = By.Id("CWField_addressline4");
        readonly By county_Field = By.Id("CWField_addressline5");
        readonly By postCode_Field = By.Id("CWField_postcode");
        readonly By UPRN_Field = By.Id("CWField_uprn");
        readonly By borough_Field = By.Id("CWField_addressboroughid_cwname");
        readonly By borough_LookUpButton = By.Id("CWLookupBtn_addressboroughid");
        readonly By ward_Field = By.Id("CWField_addresswardid_cwname");
        readonly By ward_LookUpButton = By.Id("CWLookupBtn_addresswardid");
        readonly By country_Field = By.Id("CWField_country");
        readonly By accomodationStatus_Field = By.Id("CWField_accommodationstatusid");
        readonly By accomodationType_Field = By.Id("CWField_accommodationtypeid_cwname");
        readonly By accomodationType_LookUpButton = By.Id("CWLookupBtn_accommodationtypeid");
        readonly By livesAlone_Field = By.Id("CWField_livesalonetypeid");
        readonly By CCG_Boundary_Field = By.Id("CWField_ccgboundaryid_cwname");
        readonly By CCG_Boundary_LookUpButton = By.Id("CWLookupBtn_ccgboundaryid");
        readonly By lowerSuperOutputArea_Field = By.Id("CWField_lowersuperoutputareaid_cwname");
        readonly By lowerSuperOutputArea_LookUpButton = By.Id("CWLookupBtn_lowersuperoutputareaid");
        readonly By clearAddress_Button = By.Id("CWFieldButton_ClearAddress");
        readonly By addressSearch_Button = By.Id("CWFieldButton_AddressSearch");


        readonly By businessPhone = By.Id("CWField_businessphone");
        readonly By homePhone_Field = By.Id("CWField_homephone");
        readonly By mobilePhone_Field = By.Id("CWField_mobilephone");
        readonly By primaryEmail_Field = By.Id("CWField_primaryemail");
        readonly By secondaryEmail_Field = By.Id("CWField_secondaryemail");
        readonly By primaryEmail_ErrorLabel = By.XPath("//*[@id='CWControlHolder_primaryemail']/label/span");
        readonly By billingEmail_Field = By.Id("CWField_billingemail");
        readonly By telephone1_Field = By.Id("CWField_telephone1");
        readonly By telephone2_Field = By.Id("CWField_telephone2");
        readonly By telephone3_Field = By.Id("CWField_telephone3");

        readonly By preferredLanguage_Field = By.Id("CWField_languageid_cwname");
        readonly By preferredLanguage_LookUpButton = By.Id("CWLookupBtn_languageid");
        readonly By preferredName_Field = By.Id("CWField_preferredname");
        readonly By preferredContactMethod_Field = By.Id("CWField_contactmethodid_cwname");
        readonly By preferredContactMethod_LookUpButton = By.Id("CWLookupBtn_contactmethodid");
        readonly By modeOfCommunication_Field = By.Id("CWField_modeofcommunicationid_cwname");
        readonly By modeOfCommunication_LookUpButton = By.Id("CWLookupBtn_modeofcommunicationid");
        readonly By preferredDay_Field = By.Id("CWField_preferredcontactdayid");
        readonly By preferredTime_Field = By.Id("CWField_preferredcontacttimeid");
        readonly By interpreterRequired_YesOption = By.Id("CWField_interpreterrequired_1");
        readonly By interpreterRequired_NoOption = By.Id("CWField_interpreterrequired_0");
        readonly By documentFormat_Field = By.Id("CWField_documentformatid_cwname");
        readonly By documentFormat_LookUpButton = By.Id("CWLookupBtn_documentformatid");
        readonly By retainConcernInformation_YesOption = By.Id("CWField_retaininformationconcern_1");
        readonly By retainConcernInformation_NoOption = By.Id("CWField_retaininformationconcern_0");
        readonly By allowMail_YesOption = By.Id("CWField_allowmail_1");
        readonly By allowMail_NoOption = By.Id("CWField_allowmail_0");
        readonly By allowEmail_YesOption = By.Id("CWField_allowemail_1");
        readonly By allowEmail_NoOption = By.Id("CWField_allowemail_0");
        readonly By allowPhone_YesOption = By.Id("CWField_allowphone_1");
        readonly By allowPhone_NoOption = By.Id("CWField_allowphone_0");
        readonly By allowSMS_YesOption = By.Id("CWField_allowsms_1");
        readonly By allowSMS_NoOption = By.Id("CWField_allowsms_0");


        readonly By responsibleTeam_Field = By.Id("CWField_ownerid_cwname");
        readonly By responsibleTeam_LookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By targetGroup_Field = By.Id("CWField_persontargetgroupid_cwname");
        readonly By targetGroup_LookUpButton = By.Id("CWLookupBtn_persontargetgroupid");
        readonly By maidenName_Field = By.Id("CWField_maidenname");
        readonly By religion_Field = By.Id("CWField_religionid_cwname");
        readonly By religion_LookUpButton = By.Id("CWLookupBtn_religionid");
        readonly By nationality_Field = By.Id("CWField_nationalityid_cwname");
        readonly By nationality_LookUpButton = By.Id("CWLookupBtn_nationalityid");
        readonly By countryOfOrgin_Field = By.Id("CWField_countryoforiginid_cwname");
        readonly By countryOfOrgin_LookUpButton = By.Id("CWLookupBtn_countryoforiginid");
        readonly By leavingCareEligibility_Field = By.Id("CWField_leavingcareeligibilityid_cwname");
        readonly By leavingCareEligibility_LookUpButton = By.Id("CWLookupBtn_leavingcareeligibilityid");
        readonly By NHSCardLocation_Field = By.Id("CWField_nhscardlocation");
        readonly By immigrationStatus_Field = By.Id("CWField_immigrationstatusid_cwname");
        readonly By immigrationStatus_LookUpButton = By.Id("CWLookupBtn_immigrationstatusid");
        readonly By placeOfBirth_Field = By.Id("CWField_placeofbirth");
        readonly By ageGroup_Field = By.Id("CWField_agegroupid");
        readonly By age_Field = By.Id("CWField_age");
        readonly By expectedDateOfBirth_Field = By.Id("CWField_expecteddateofbirth");
        readonly By sexualOrientation_Field = By.Id("CWField_sexualorientationid_cwname");
        readonly By sexualOrientation_LookUpButton = By.Id("CWLookupBtn_sexualorientationid");
        readonly By exBritishForces_Field = By.Id("CWField_exbritishforcesid");
        readonly By excludeFromDBS_YesOption = By.Id("CWField_excludefromdbs_1");
        readonly By excludeFromDBS_NoOption = By.Id("CWField_excludefromdbs_0");
        readonly By pronounsField_LookupButton = By.Id("CWLookupBtn_pronounsid");
        readonly By pronouns_LinkField = By.Id("CWField_pronounsid_Link");
        readonly By livesInSmokinHousehold_Picklist = By.Id("CWField_smokinghouseholdid");
        readonly By pets_Picklist = By.Id("CWField_petstatusid");


        readonly By creditorNo_Field = By.Id("CWField_creditornumber");
        readonly By debtorNo1_Field = By.Id("CWField_debtornumber1");
        readonly By debtorNo2_Field = By.Id("CWField_debtornumber2");
        readonly By debtorNo3_Field = By.Id("CWField_debtornumber3");
        readonly By debtorNo4_Field = By.Id("CWField_debtornumber4");
        readonly By referanceCode = By.Id("CWField_referencecode");
        readonly By payer_Field = By.Id("CWField_payerid_Link");
        readonly By payer_LookUpButton = By.Id("CWLookupBtn_payerid");
        readonly By payer_EndDate_Field = By.Id("CWField_payerenddate");
        readonly By supressStatementInvoices_YesOption = By.Id("CWField_suppressstatementinvoices_1");
        readonly By supressStatementInvoices_NoOption = By.Id("CWField_suppressstatementinvoices_0");




        readonly By SSD_Number_Field = By.Id("CWField_ssdnumber");
        readonly By nationalInsuranceNumber_Field = By.Id("CWField_nationalinsurancenumber");
        readonly By NHSNO_Pre_Field = By.Id("CWField_nhsnumberpre1995");
        readonly By uniquePupilNo_Field = By.Id("CWField_uniquepupilnumber");
        readonly By formerPupilNo_Field = By.Id("CWField_formeruniquepupilnumber");
        readonly By courtCaseNumber_Field = By.Id("CWField_courtcasenumber");
        readonly By birthCertificateNumber = By.Id("CWField_birthcertificatenumber");
        readonly By isExternalPerson_YesOption = By.Id("CWField_isexternalperson_1");
        readonly By isExternalPerson_NoOption = By.Id("CWField_isexternalperson_0");
        readonly By homeOfficeRegistrationNumber_Field = By.Id("CWField_homeofficeregistrationnumber");
        readonly By UPNUnknownReason_Field = By.Id("CWField_upnunknownreasonid_cwname");
        readonly By UPNUnknownReason_LookUpButton = By.Id("CWLookupBtn_upnunknownreasonid");


        #endregion

        #region Mandatory Labels
        By MandatoryField_Label(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']/span[@class = 'mandatory']");

        #endregion


        public PersonRecordNewPage SelectPersonType(string TextToSelect)
        {
            try
            {
                SelectPicklistElementByText(personType_Field, TextToSelect);
            }
            catch
            {
            }

            return this;
        }

        public PersonRecordNewPage WaitForPersonRecordNewPageToLoad(string PersonID, string PersonName)
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame(PersonID));
            SwitchToIframe(personRecordIFrame(PersonID));

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(printButton);
            WaitForElement(shareButton);
            
            WaitForElement(PageTitle);
            ValidateElementText(PageTitle, PersonName);

            return this;
        }

        public PersonRecordNewPage WaitForNewPersonRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWNewPerson);
            SwitchToIframe(iframe_CWNewPerson);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(PageTitle);

            return this;
        }

        public PersonRecordNewPage ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NotificationMessage);
            else
                WaitForElementNotVisible(NotificationMessage, 3);

            return this;
        }

        public PersonRecordNewPage ValidateNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(NotificationMessage, ExpectedText);

            return this;
        }

        public PersonRecordNewPage InsertFirstName(string TextToInsert)
        {
            SendKeys(firstName_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertMiddleName(string TextToInsert)
        {
            SendKeys(middleName_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertLastName(string TextToInsert)
        {
            SendKeys(lastName_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage SelectStatedGender(string TextToSelect)
        {
            SelectPicklistElementByText(statedGender_Field, TextToSelect);

            return this;
        }

        public PersonRecordNewPage SelectDOBAndAge(string TextToSelect)
        {
            SelectPicklistElementByText(DOBAndAge_Field, TextToSelect);

            return this;
        }

        public PersonRecordNewPage InsertDOB(string TextToInsert)
        {
            SendKeys(dateOfBirth_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage SelectAddressType(string TextToSelect)
        {
            SelectPicklistElementByText(addressType_Field, TextToSelect);

            return this;
        }

        public PersonRecordNewPage InsertPostCode(string TextToInsert)
        {
            SendKeys(postCode_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertHomePhone(string TextToInsert)
        {
            SendKeys(homePhone_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertBusinessPhone(string TextToInsert)
        {
            SendKeys(businessPhone, TextToInsert);

            return this;
        }
        public PersonRecordNewPage InsertMobilePhone(string TextToInsert)
        {
            SendKeys(mobilePhone_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertPrimaryEmail(string TextToInsert)
        {
            SendKeys(primaryEmail_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertSecondaryEmail(string TextToInsert)
        {
            SendKeys(secondaryEmail_Field, TextToInsert);

            return this;
        }


        public PersonRecordNewPage ClickEthnicityLookupButton()
        {
            Click(ethnicity_Field);

            return this;
        }




        public PersonRecordNewPage InsertBillingEmail(string TextToInsert)
        {
            SendKeys(billingEmail_Field, TextToInsert);

            return this;
        }
        public PersonRecordNewPage InsertTelephone1(string TextToInsert)
        {
            SendKeys(telephone1_Field, TextToInsert);

            return this;
        }
        public PersonRecordNewPage InsertTelephone2(string TextToInsert)
        {
            SendKeys(telephone2_Field, TextToInsert);

            return this;
        }
        public PersonRecordNewPage InsertTelephone3(string TextToInsert)
        {
            SendKeys(telephone3_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage SelectLivesInSmokinHouseholdFieldValue(string TextToSelect)
        {
            ScrollToElement(livesInSmokinHousehold_Picklist);
            WaitForElementVisible(livesInSmokinHousehold_Picklist);
            SelectPicklistElementByText(livesInSmokinHousehold_Picklist, TextToSelect);
            return this;
        }

        public PersonRecordNewPage SelectPetsFieldValue(string TextToSelect)
        {
            ScrollToElement(pets_Picklist);
            WaitForElementVisible(pets_Picklist);
            SelectPicklistElementByText(pets_Picklist, TextToSelect);
            return this;
        }


        public PersonRecordNewPage ValidatePrimaryEmailErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(primaryEmail_ErrorLabel);
            else
                WaitForElementNotVisible(primaryEmail_ErrorLabel, 3);

            return this;
        }

        public PersonRecordNewPage ValidatePrimaryEmailErrorLabelText(string ExpectedText)
        {
            ValidateElementText(primaryEmail_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonRecordNewPage TapSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            return this;
        }

        public PersonRecordNewPage ClickRunOnDemandWorkflowButton()
        {
            WaitForElement(additionalIItemsButton);
            Click(additionalIItemsButton);

            WaitForElement(runOnDemandWorkflowButton);
            Click(runOnDemandWorkflowButton);

            return this;
        }

        public PersonRecordNewPage ClickCopyRecordLinkButton()
        {
            WaitForElement(additionalIItemsButton);
            Click(additionalIItemsButton);

            WaitForElement(copyRecordLinkButton);
            Click(copyRecordLinkButton);

            return this;
        }

        public PersonRecordNewPage TapSaveButton()
        {
            Click(saveButton);

            return this;
        }

        public PersonRecordNewPage ValidateLastNameErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(lastName_ErrorLabel);
            else
                WaitForElementNotVisible(lastName_ErrorLabel, 3);

            return this;
        }

        public PersonRecordNewPage ValidateLastNameErrorLabelText(string ExpectedText)
        {
            ValidateElementText(lastName_ErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecordNewPage ValidateStatedGenderNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(statedGender_ErrorLabel);
            else
                WaitForElementNotVisible(statedGender_ErrorLabel, 3);

            return this;
        }

        public PersonRecordNewPage ValidateStatedGenderNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(statedGender_ErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecordNewPage ValidateEthnicityNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ethnicity_ErrorLabel);
            else
                WaitForElementNotVisible(ethnicity_ErrorLabel, 3);

            return this;
        }

        public PersonRecordNewPage ValidateEthnicityNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(ethnicity_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonRecordNewPage ValidateDOBNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DOB_ErrorLabel);
            else
                WaitForElementNotVisible(DOB_ErrorLabel, 3);

            return this;
        }

        public PersonRecordNewPage ValidateDOBNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(DOB_ErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecordNewPage ValidateAddressTypeNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(addressType_ErrorLabel);
            else
                WaitForElementNotVisible(addressType_ErrorLabel, 3);

            return this;
        }

        public PersonRecordNewPage ValidateAddressTypeNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(addressType_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonRecordNewPage InsertTitle(string TextToInsert)
        {
            SendKeys(title_Field, TextToInsert);

            return this;
        }
        public PersonRecordNewPage InsertNHSNo(string TextToInsert)
        {
            Click(nHSNo_Field);
            ClearText(nHSNo_Field);

            Click(nHSNoReason_Field);
            SendKeys(nHSNoReason_Field, Keys.LeftShift + Keys.Tab);

            SendKeysWithoutClearing(nHSNo_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage ClickBrowseButton()
        {
            WaitForElement(personPhoto_Field);
            Click(personPhoto_Field);

            return this;
        }

        public PersonRecordNewPage ClickImageUploadDocument(string FilePath)
        {
            WaitForElement(personPhoto_Field);
            SendKeys(personPhoto_Field, FilePath);
            

            return this;
        }
        public PersonRecordNewPage ClickMaritalStatusLookupButton()
        {
            Click(maritalStatus_LookUpButton);

            return this;
        }

        public PersonRecordNewPage InsertStartDateOfAddress(string TextToInsert)
        {
            SendKeys(addressStartDate_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertPropertyName(string TextToInsert)
        {
            SendKeys(propertyName_Field, TextToInsert);

            return this;
        }
        public PersonRecordNewPage InsertPropertyNo(string TextToInsert)
        {
            SendKeys(propertyNo_Field, TextToInsert);

            return this;
        }
        public PersonRecordNewPage InsertStreet(string TextToInsert)
        {
            SendKeys(street_Field, TextToInsert);

            return this;
        }
        public PersonRecordNewPage InsertVillageDistrict(string TextToInsert)
        {
            SendKeys(village_Field, TextToInsert);

            return this;
        }
        public PersonRecordNewPage InsertTownCity(string TextToInsert)
        {
            SendKeys(townCity_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertCounty(string TextToInsert)
        {
            SendKeys(county_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertCountry(string TextToInsert)
        {
            SendKeys(country_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertUPRN(string TextToInsert)
        {
            SendKeys(UPRN_Field, TextToInsert);

            return this;
        }
        public PersonRecordNewPage ClickPropertyTypeLookupButton()
        {
            Click(propertyType_LookUpButton);

            return this;
        }
        public PersonRecordNewPage ClickBoroughLookupButton()
        {
            Click(borough_LookUpButton);

            return this;
        }

        public PersonRecordNewPage ClickWardLookupButton()
        {
            Click(ward_LookUpButton);

            return this;
        }

        public PersonRecordNewPage SelectAccomodationStatus(string TextToSelect)
        {
            SelectPicklistElementByText(accomodationStatus_Field, TextToSelect);

            return this;
        }
        public PersonRecordNewPage ClickAccomodationTypeLookupButton()
        {
            Click(accomodationType_LookUpButton);

            return this;
        }
        public PersonRecordNewPage SelectLivesAlone(string TextToSelect)
        {
            SelectPicklistElementByText(livesAlone_Field, TextToSelect);

            return this;
        }

        public PersonRecordNewPage ClickCCGBoundaryLookupButton()
        {
            Click(CCG_Boundary_LookUpButton);

            return this;
        }

        public PersonRecordNewPage ClickLowerSuperOutputAreaLookupButton()
        {
            Click(lowerSuperOutputArea_LookUpButton);

            return this;
        }

        public PersonRecordNewPage ClickPreferredLanguageLookupButton()
        {
            Click(preferredLanguage_LookUpButton);

            return this;
        }
        public PersonRecordNewPage InsertPreferredName(string TextToInsert)
        {
            SendKeys(preferredName_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage ClickPreferredContactMethodLookupButton()
        {
            Click(preferredContactMethod_LookUpButton);

            return this;
        }

        public PersonRecordNewPage ClickPreferredModeOfCommunicationLookupButton()
        {
            Click(modeOfCommunication_LookUpButton);

            return this;
        }

        public PersonRecordNewPage SelectPreferredDay(string TextToSelect)
        {
            SelectPicklistElementByText(preferredDay_Field, TextToSelect);

            return this;
        }

        public PersonRecordNewPage SelectPreferredTime(string TextToSelect)
        {
            SelectPicklistElementByText(preferredTime_Field, TextToSelect);

            return this;
        }

        public PersonRecordNewPage ClickDocumentFormatLookupButton()
        {
            Click(documentFormat_LookUpButton);

            return this;
        }

        public PersonRecordNewPage ClickResponsibleTeamLookupButton()
        {
            ScrollToElement(responsibleTeam_LookUpButton);
            WaitForElementToBeClickable(responsibleTeam_LookUpButton);
            Click(responsibleTeam_LookUpButton);

            return this;
        }

        public PersonRecordNewPage ClickTargetGroupLookupButton()
        {
            Click(targetGroup_LookUpButton);

            return this;
        }

        public PersonRecordNewPage InsertMaidenName(string TextToInsert)
        {
            SendKeys(maidenName_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertNHSCardLocation(string TextToInsert)
        {
            SendKeys(NHSCardLocation_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage ClickImmigrationStatusLookupButton()
        {
           Click(immigrationStatus_LookUpButton);

            return this;
        }

        public PersonRecordNewPage InsertPlaceOfBirth(string TextToInsert)
        {
            SendKeys(placeOfBirth_Field, TextToInsert);

            return this;
        }
        public PersonRecordNewPage ClickReligionLookupButton()
        {
            Click(religion_LookUpButton);

            return this;
        }

        public PersonRecordNewPage SelectAgeGroup(string TextToSelect)
        {
            SelectPicklistElementByText(ageGroup_Field, TextToSelect);

            return this;
        }

        public PersonRecordNewPage ClickNationalityLookupButton()
        {
            Click(nationality_LookUpButton);

            return this;
        }

        public PersonRecordNewPage ClickSexualOrientationLookupButton()
        {
            Click(sexualOrientation_LookUpButton);

            return this;
        }

        public PersonRecordNewPage ClickCountryOfOriginLookupButton()
        {
            Click(countryOfOrgin_LookUpButton);

            return this;
        }

        public PersonRecordNewPage SelectExBritishForces(string TextToSelect)
        {
            SelectPicklistElementByText(exBritishForces_Field, TextToSelect);

            return this;
        }

        public PersonRecordNewPage ClickLeavingCareEligibilityLookupButton()
        {
            Click(leavingCareEligibility_LookUpButton);

            return this;
        }

        public PersonRecordNewPage InsertCreditorNo(string TextToInsert)
        {
            SendKeys(creditorNo_Field, TextToInsert);

            return this;
        }


        public PersonRecordNewPage InsertReferenceCode(string TextToInsert)
        {
            SendKeys(referanceCode, TextToInsert);

            return this;
        }


        public PersonRecordNewPage InsertDebtor1(string TextToInsert)
        {
            SendKeys(debtorNo1_Field, TextToInsert);

            return this;
        }


        public PersonRecordNewPage InsertDebtor2(string TextToInsert)
        {
            SendKeys(debtorNo2_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage ClickPayerLookupButton()
        {
            Click(payer_LookUpButton);

            return this;
        }


        public PersonRecordNewPage InsertSSDNumber(string TextToInsert)
        {
            SendKeys(SSD_Number_Field , TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertNationalInsuranceNumber(string TextToInsert)
        {
            SendKeys(nationalInsuranceNumber_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertNHSNoPre(string TextToInsert)
        {
            SendKeys(NHSNO_Pre_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertUniquePupilNo(string TextToInsert)
        {
            SendKeys(uniquePupilNo_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertFormerUniquePupilNo(string TextToInsert)
        {
            SendKeys(formerPupilNo_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertCourtCaseNo(string TextToInsert)
        {
            SendKeys(courtCaseNumber_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertBirthCertificateNo(string TextToInsert)
        {
            SendKeys(birthCertificateNumber, TextToInsert);

            return this;
        }

        public PersonRecordNewPage InsertHomeOfficeRegistrationNumber(string TextToInsert)
        {
            SendKeys(homeOfficeRegistrationNumber_Field, TextToInsert);

            return this;
        }

        public PersonRecordNewPage ClickUPNUnknownReasonLookupButton()
        {
            Click(UPNUnknownReason_LookUpButton);

            return this;
        }

        public PersonRecordNewPage ValidateAgeFieldInactiveStatus(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
               
                ValidateElementDisabled(age_Field);
            }

            else
            {
                WaitForElementNotVisible(age_Field, 3);
            }
               


            return this;
        }

        public PersonRecordNewPage ValidateDateOfBirthFieldActiveStatus(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElement(dateOfBirth_Field);
                ValidateElementNotDisabled(dateOfBirth_Field);
            }

            else
                WaitForElementNotVisible(dateOfBirth_Field, 3);

            return this;
        }

        public PersonRecordNewPage ValidateDateOfBirthFieldInActiveStatus(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
               
                ValidateElementDisabled(dateOfBirth_Field);
            }

            else
                WaitForElementNotVisible(dateOfBirth_Field, 3);

            return this;
        }

        public PersonRecordNewPage ValidateAgeFieldActiveStatus(bool ExpectVisible)
        {
            if (ExpectVisible)
            {

                ValidateElementNotDisabled(age_Field);
            }

            else
            {
                WaitForElementNotVisible(age_Field, 3);
            }



            return this;
        }
        public PersonRecordNewPage ValidateExpectedDOBActiveStatus(bool ExpectVisible)
        {
            if (ExpectVisible)
            {

                ValidateElementNotDisabled(expectedDateOfBirth_Field);
            }

            else
            {
                WaitForElementNotVisible(expectedDateOfBirth_Field, 3);
            }



            return this;
        }

        public PersonRecordNewPage ClickDeceasedYesRadioButton()
        {
            Click(deceased_YesOption);

            return this;
        }

        public PersonRecordNewPage ValidateCauseOfDeathFieldActiveStatus(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElement(causeOfDeath_Field);
                ValidateElementNotDisabled(causeOfDeath_Field);
            }

            else
                WaitForElementNotVisible(causeOfDeath_Field, 3);

            return this;
        }

        public PersonRecordNewPage ValidateDateOfDeathFieldActiveStatus(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElement(dateOfDeath_Field);
                ValidateElementNotDisabled(dateOfDeath_Field);
            }

            else
                WaitForElementNotVisible(dateOfDeath_Field, 3);

            return this;
        }

        public PersonRecordNewPage ValidatePlaceOfDeathFieldActiveStatus(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElement(placeOfDeath_Field);
                ValidateElementNotDisabled(placeOfDeath_Field);
            }

            else
                WaitForElementNotVisible(placeOfDeath_Field, 3);

            return this;
        }

        public PersonRecordNewPage ClickPronounsLookupButton()
        {
            ScrollToElement(pronounsField_LookupButton);
            WaitForElementToBeClickable(pronounsField_LookupButton);
            Click(pronounsField_LookupButton);

            return this;
        }

        public PersonRecordNewPage ValidatePronounsLinkFieldText(string ExpectedValue)
        {
            ScrollToElement(pronouns_LinkField);
            WaitForElementVisible(pronouns_LinkField);
            ValidateElementByTitle(pronouns_LinkField, ExpectedValue);
            return this;
        }

        public PersonRecordNewPage ValidatePronounsLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(pronounsField_LookupButton);
            else
                WaitForElementNotVisible(pronounsField_LookupButton, 3);

            return this;
        }

        public PersonRecordNewPage ValidateMandatoryFieldIsDisplayed(string FieldName, bool ExpectedMandatory = true)
        {
            if (ExpectedMandatory)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 2);
            bool ActualDisplayed = GetElementVisibility(MandatoryField_Label(FieldName));
            Assert.AreEqual(ExpectedMandatory, ActualDisplayed);

            return this;
        }

    }
}
