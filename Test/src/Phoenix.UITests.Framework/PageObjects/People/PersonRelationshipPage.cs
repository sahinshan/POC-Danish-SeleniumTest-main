using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonRelationshipPage : CommonMethods
    {
        public PersonRelationshipPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        #region Locators

        readonly string iframe_CWRecordDialog_Name = "iframe_CWRecordDialog";
        readonly By iframe_CWRecordDialog = By.Id("iframe_CWRecordDialog");

        //Person Relationship Iframes
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_PersonRelationshipsFrame = By.Id("CWUrlPanel_IFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");



        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Relationship: ']");
        readonly By pageHeaderPersonRelationship = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Person Relationship']");

        #region Top Menu

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By shareButton = By.Id("TI_ShareRecordButton");
        readonly By assignutton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarItemsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By restrictAccessButton = By.Id("TI_RestrictAccessButton");
        readonly By assignButton = By.Id("TI_DeleteRecordButton");



        #endregion

        #region Person Banner

        readonly By personLinkPersonBanner = By.XPath("//div[@id='CWInfoLeft']/span/a/strong");
        readonly By bornLabelPersonBanner = By.XPath("//div[@id='CWInfoRight']/span/strong[text()='Born: ']");
        readonly By genderLabelPersonBanner = By.XPath("//div[@id='CWInfoRight']/span/strong[text()='Gender: ']");
        readonly By nhsNoLabelPersonBanner = By.XPath("//div[@id='CWInfoRight']/span/strong[text()='NHS No: ']");
        readonly By preferredNameLabelPersonBanner = By.XPath("//div[@id='CWBannerHolder']/ul/li/div/span/strong[text()='Preferred Name: ']");

        #endregion

        #region Main Menu

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");
        readonly By healthLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_Health']/a");
        readonly By PersonGestationPeriodPageLeftSubMenuItem = By.Id("CWNavItem_GestationPeriod");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        #endregion

        #region Relationship to Other Person section

        readonly By RelationshipToOtherPersonAreaTitle = By.XPath("//div[@id='CWSection_RelationshipToOtherPerson']/fieldset/div/span[text()='Relationship to Other Person']");


        #region Field Labels

        readonly By primaryPersonFieldLabel = By.XPath("//*[@id='CWLabelHolder_personid']/label[text()='Primary Person']");
        readonly By relationshipTypeFieldLabel = By.XPath("//*[@id='CWLabelHolder_personrelationshiptypeid']/label[text()='is a']");
        readonly By relatedPersonFieldLabel = By.XPath("//*[@id='CWLabelHolder_relatedpersonid']/label");


        #endregion

        #region Fields

        readonly By primaryPersonField = By.XPath("//a[@id='CWField_personid_Link']");
        readonly By primaryPersonFieldLookupButton = By.Id("CWLookupBtn_personid");

        readonly By relationshipTypeField = By.XPath("//a[@id='CWField_personrelationshiptypeid_Link']");
        readonly By relationshipTypeFieldLookupButton = By.Id("CWLookupBtn_personrelationshiptypeid");

        readonly By relatedPersonField = By.XPath("//a[@id='CWField_relatedpersonid_Link']");
        readonly By relatedPersonFieldLookupButton = By.Id("CWLookupBtn_relatedpersonid");

        #endregion

        #endregion

        #region Reciprocal Relationship section

        readonly By ReciprocalRelationshipAreaTitle = By.XPath("//*[@id='CWSection_ReciprocalRelationship']/fieldset/div/span[text()='Reciprocal Relationship']");


        #region Field Labels

        readonly By inversePersonFieldLabel = By.XPath("//*[@id='CWLabelHolder_InversePerson']/label[text()='Person']");
        readonly By relatedPersonRelationshipTypeFieldLabel = By.XPath("//*[@id='CWLabelHolder_relatedpersonrelationshiptypeid']/label[text()='is a']");
        readonly By toFieldLabel = By.XPath("//*[@id='CWLabelHolder_RoleTo']/label[text()='To']");


        #endregion
        
        #region Fields

        readonly By inversePersonField = By.XPath("//*[@id='CWField_InversePerson']");

        readonly By relatedPersonRelationshipTypeField = By.XPath("//*[@id='CWField_relatedpersonrelationshiptypeid_Link']");
        readonly By relatedPersonRelationshipTypeFieldLookupButton = By.Id("CWLookupBtn_relatedpersonrelationshiptypeid");

        readonly By toField = By.XPath("//*[@id='CWField_RoleTo']");

        #endregion

        #endregion

        #region Relationship Details section

        readonly By RelationshipDetailsAreaTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div/span[text()='Relationship Details']");


        #region Field Labels

        readonly By startDateFieldLabel = By.XPath("//*[@id='CWLabelHolder_startdate']/label[text()='Start Date']");
        readonly By endDateFieldLabel = By.XPath("//*[@id='CWLabelHolder_enddate']/label[text()='End Date']");
        readonly By descriptionFieldLabel = By.XPath("//*[@id='CWLabelHolder_description']/label[text()='Description']");
        readonly By responsibleTeamFieldLabel = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");


        #endregion

        #region Fields

        readonly By startDateField = By.XPath("//*[@id='CWField_startdate']");
        readonly By endDateField = By.XPath("//*[@id='CWField_enddate']");
        readonly By descriptionField = By.XPath("//*[@id='CWField_description']");
        readonly By responsibleTeamField = By.XPath("//*[@id='CWField_ownerid_Link']");

        #endregion

        #endregion

        #region Nature of Relationship to Primary Person section

        readonly By NatureOfRelationshipToPrimaryPersonAreaTitle = By.XPath("//*[@id='CWSection_NatureRelationshipPrimaryPerson']/fieldset/div[1]/span[text()='Nature of Relationship to Primary Person']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        #region Field Labels

        readonly By InsideHouseholdFieldLabel = By.XPath("//*[@id='CWLabelHolder_insidehouseholdid']/label[text()='Inside Household']");
        readonly By FamilyMemberFieldLabel = By.XPath("//*[@id='CWLabelHolder_familymemberid']/label[text()='Family Member']");
        readonly By NextofKinFieldLabel = By.XPath("//*[@id='CWLabelHolder_nextofkinid']/label[text()='Next of Kin']");
        readonly By EmergencyContactFieldLabel = By.XPath("//*[@id='CWLabelHolder_emergencycontactid']/label[text()='Emergency Contact']");
        readonly By KeyHolderFieldLabel = By.XPath("//*[@id='CWLabelHolder_keyholderid']/label[text()='Key Holder']");
        readonly By AdvocateFieldLabel = By.XPath("//*[@id='CWLabelHolder_advocateid']/label[text()='Advocate']");
        readonly By MHANearestRelativeFieldLabel = By.XPath("//*[@id='CWLabelHolder_mhanearestrelativeid']/label[text()='MHA Nearest Relative']");
        readonly By IsBirthParentFieldLabel = By.XPath("//*[@id='CWLabelHolder_isbirthparentid']/label[text()='Is Birth Parent']");
        readonly By PrimaryCarerFieldLabel = By.XPath("//*[@id='CWLabelHolder_primarycarerid']/label[text()='Primary Carer']");
        readonly By PowersOfAttorneyFieldLabel = By.XPath("//*[@id='CWLabelHolder_powerofattorneyid']/label[text()='Powers of Attorney']");
        readonly By LegalGuardianFieldLabel = By.XPath("//*[@id='CWLabelHolder_legalguardianid']/label[text()='Legal Guardian']");
        readonly By secondaryCaregiverFieldLabel = By.XPath("//*[@id='CWLabelHolder_secondarycaregiverid']/label[text()='Secondary Caregiver']");
        readonly By FinancialRepresentativeFieldLabel = By.XPath("//*[@id='CWLabelHolder_financialrepresentativeid']/label[text()='Financial Representative']");
        readonly By HasParentalResponsibilityFieldLabel = By.XPath("//*[@id='CWLabelHolder_hasparentalresponsibilityid']/label[text()='Has Parental Responsibility']");
        readonly By FieldLabel = By.XPath("//*[@id='CWLabelHolder_externalprimarycaseworkerid']/label[text()='Primary Case Worker (External Contact)']");
        readonly By PCHRFieldLabel = By.XPath("//*[@id='CWLabelHolder_pchrid']/label[text()='PCHR']");


        #endregion

        #region Fields

        readonly By InsideHouseholdField = By.XPath("//*[@id='CWField_insidehouseholdid']");
        readonly By FamilyMemberField = By.XPath("//*[@id='CWField_familymemberid']");
        readonly By NextofKinField = By.XPath("//*[@id='CWField_nextofkinid']");
        readonly By EmergencyContactField = By.XPath("//*[@id='CWField_emergencycontactid']");
        readonly By KeyHolderField = By.XPath("//*[@id='CWField_keyholderid']");
        readonly By AdvocateField = By.XPath("//*[@id='CWField_advocateid']");
        readonly By MHANearestRelativeField = By.XPath("//*[@id='CWField_mhanearestrelativeid']");
        readonly By IsBirthParentField = By.XPath("//*[@id='CWField_isbirthparentid']");
        readonly By PrimaryCarerField = By.XPath("//*[@id='CWField_primarycarerid']");
        readonly By PowersOfAttorneyField = By.XPath("//*[@id='CWField_powerofattorneyid']");
        readonly By LegalGuardianField = By.XPath("//*[@id='CWField_legalguardianid']");
        readonly By secondaryCaregiverField = By.XPath("//*[@id='CWField_secondarycaregiverid']");
        readonly By FinancialRepresentativeField = By.XPath("//*[@id='CWField_financialrepresentativeid']");
        readonly By HasParentalResponsibilityField = By.XPath("//*[@id='CWField_hasparentalresponsibilityid']");
        readonly By Field = By.XPath("//*[@id='CWField_externalprimarycaseworkerid']");
        readonly By PCHRField = By.XPath("//*[@id='CWField_pchrid']");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordsAreaHeaderCell(int CellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + CellPosition + "]/a");
        By RelatedPersonCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");

        #endregion

        #endregion

        #region Footer

        readonly By createdByFooterLabel = By.XPath("//*[@id='CWInputFormFooter']/div/div/label[text()='Created By']");
        readonly By createdOnFooterLabel = By.XPath("//*[@id='CWInputFormFooter']/div/div/label[text()='Created On']");
        readonly By modifiedByFooterLabel = By.XPath("//*[@id='CWInputFormFooter']/div/div/label[text()='Modified By']");
        readonly By modifiedOnFooterLabel = By.XPath("//*[@id='CWInputFormFooter']/div/div/label[text()='Modified On']");
        

        #endregion

        #endregion




        /// <summary>
        /// Use this method when the Person Relationship page is loaded from a Case From record.
        /// e.g. A user tap on a CMS Editable Field in a case form that redirects the user to the Person Relationship Page
        /// </summary>
        /// <returns></returns>
        public PersonRelationshipPage WaitForPageToLoadAfterNavigatingFromCaseForm()
        {
            WaitForElement(iframe_CWRecordDialog);
            SwitchToIframe(iframe_CWRecordDialog);

            WaitForElement(pageHeader);

            WaitForElement(backButton);
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(shareButton);
            WaitForElement(assignutton);
            WaitForElement(additionalToolbarItemsButton);

            WaitForElement(personLinkPersonBanner);
            WaitForElement(bornLabelPersonBanner);
            WaitForElement(genderLabelPersonBanner);
            WaitForElement(nhsNoLabelPersonBanner);
            WaitForElement(preferredNameLabelPersonBanner);

            WaitForElement(RelationshipToOtherPersonAreaTitle);
            WaitForElement(primaryPersonFieldLabel);
            WaitForElement(relationshipTypeFieldLabel);
            WaitForElement(relatedPersonFieldLabel);

            WaitForElement(ReciprocalRelationshipAreaTitle);
            WaitForElement(inversePersonFieldLabel);
            WaitForElement(relatedPersonRelationshipTypeFieldLabel);
            WaitForElement(toFieldLabel);

            WaitForElement(RelationshipDetailsAreaTitle);
            WaitForElement(startDateFieldLabel);
            WaitForElement(endDateFieldLabel);
            WaitForElement(descriptionFieldLabel);
            WaitForElement(responsibleTeamFieldLabel);

            WaitForElement(NatureOfRelationshipToPrimaryPersonAreaTitle);
            WaitForElement(InsideHouseholdFieldLabel);
            WaitForElement(FamilyMemberFieldLabel);
            WaitForElement(NextofKinFieldLabel);
            WaitForElement(EmergencyContactFieldLabel);
            WaitForElement(KeyHolderFieldLabel);
            WaitForElement(AdvocateFieldLabel);
            WaitForElement(MHANearestRelativeFieldLabel);
            WaitForElement(IsBirthParentFieldLabel);
            WaitForElement(PrimaryCarerFieldLabel);
            WaitForElement(PowersOfAttorneyFieldLabel);
            WaitForElement(LegalGuardianFieldLabel);
            WaitForElement(secondaryCaregiverFieldLabel);
            WaitForElement(FinancialRepresentativeFieldLabel);
            WaitForElement(HasParentalResponsibilityFieldLabel);
            WaitForElement(FieldLabel);
            WaitForElement(PCHRFieldLabel);

            WaitForElement(createdByFooterLabel);
            WaitForElement(createdOnFooterLabel);
            WaitForElement(modifiedByFooterLabel);
            WaitForElement(modifiedOnFooterLabel);

            return this;
        }

        /// <summary>
        /// Use this method when the Person Relationship page is loaded from a Case From record.
        /// e.g. A user tap on a CMS Editable Field "New" button in a case form that redirects the user to the Person Relationship Page to create a new relationship record
        /// </summary>
        /// <returns></returns>
        public PersonRelationshipPage WaitForPageToLoadAfterTapNewButtonOnFormCMSField()
        {
            if (iframe_CWRecordDialog_Name != GetCurrentFrameName())
            {
                WaitForElement(iframe_CWRecordDialog);
                SwitchToIframe(iframe_CWRecordDialog);
            }
            

            WaitForElement(pageHeader);

            WaitForElement(backButton);
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(RelationshipToOtherPersonAreaTitle);
            WaitForElement(primaryPersonFieldLabel);
            WaitForElement(relationshipTypeFieldLabel);
            WaitForElement(relatedPersonFieldLabel);

            WaitForElement(ReciprocalRelationshipAreaTitle);
            WaitForElement(inversePersonFieldLabel);
            WaitForElement(relatedPersonRelationshipTypeFieldLabel);
            WaitForElement(toFieldLabel);

            WaitForElement(RelationshipDetailsAreaTitle);
            WaitForElement(startDateFieldLabel);
            WaitForElement(endDateFieldLabel);
            WaitForElement(descriptionFieldLabel);
            WaitForElement(responsibleTeamFieldLabel);

            WaitForElement(NatureOfRelationshipToPrimaryPersonAreaTitle);
            WaitForElement(InsideHouseholdFieldLabel);
            WaitForElement(FamilyMemberFieldLabel);
            WaitForElement(NextofKinFieldLabel);
            WaitForElement(EmergencyContactFieldLabel);
            WaitForElement(KeyHolderFieldLabel);
            WaitForElement(AdvocateFieldLabel);
            WaitForElement(MHANearestRelativeFieldLabel);
            WaitForElement(IsBirthParentFieldLabel);
            WaitForElement(PrimaryCarerFieldLabel);
            WaitForElement(PowersOfAttorneyFieldLabel);
            WaitForElement(LegalGuardianFieldLabel);
            WaitForElement(secondaryCaregiverFieldLabel);
            WaitForElement(FinancialRepresentativeFieldLabel);
            WaitForElement(HasParentalResponsibilityFieldLabel);
            WaitForElement(FieldLabel);
            WaitForElement(PCHRFieldLabel);

            return this;
        }

        public PersonRelationshipPage TapBackButton()
        {
            Click(backButton);

            return this;
        }

        public PersonRelationshipPage TapSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            return this;
        }

        public LookupPopup TapPrimaryPersonFieldLookupButton()
        {
            Click(primaryPersonFieldLookupButton);

            return new LookupPopup(driver, Wait, appURL);
        }

        public LookupPopup TapRelationshipTypeFieldLookupButton()
        {
            Click(relationshipTypeFieldLookupButton);

            return new LookupPopup(driver, Wait, appURL);
        }

        public LookupPopup TapRelatedPersonFieldLookupButton()
        {
            Click(relatedPersonFieldLookupButton);

            return new LookupPopup(driver, Wait, appURL);
        }

        public LookupPopup TapRelatedPersonRelationshipTypeFieldLookupButton()
        {
            Click(relatedPersonRelationshipTypeFieldLookupButton);

            return new LookupPopup(driver, Wait, appURL);
        }

        public PersonRelationshipPage SelectInsideHouseholdValue(string TextToSelect)
        {
            SelectPicklistElementByText(InsideHouseholdField, TextToSelect);

            return this;
        }

        public PersonRelationshipPage ValidateRelatedPersonText(string ExpectedText)
        {
            ValidateElementText(relatedPersonField, ExpectedText);

            return this;
        }

        public PersonRelationshipPage ValidateInsideHouseholdSelectedValue(string ExpectedText)
        {
            ValidatePicklistSelectedText(InsideHouseholdField, ExpectedText);

            return this;
        }

        public PersonRelationshipPage ValidatePersonRelationshipType(string ExpectedValue)
        {
            ValidateElementText(relatedPersonRelationshipTypeField, ExpectedValue);

            return this;
        }

        public PersonRelationshipPage ValidateStartDate(string ExpectedValue)
        {
            ValidateElementValue(startDateField, ExpectedValue);

            return this;
        }

        public PersonRelationshipPage ValidateEndDate(string ExpectedValue)
        {
            ValidateElementValue(endDateField, ExpectedValue);

            return this;
        }

        public PersonRelationshipPage WaitForPersonRelationshipPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(iframe_PersonRelationshipsFrame);
            SwitchToIframe(iframe_PersonRelationshipsFrame);

            WaitForElement(recordsAreaHeaderCell(2));
            WaitForElement(recordsAreaHeaderCell(3));
            WaitForElement(recordsAreaHeaderCell(4));
            WaitForElement(recordsAreaHeaderCell(5));
            WaitForElement(recordsAreaHeaderCell(6));
            WaitForElement(recordsAreaHeaderCell(7));
            WaitForElement(recordsAreaHeaderCell(8));
           

            ScrollToElement(recordsAreaHeaderCell(2));
            ValidateElementText(recordsAreaHeaderCell(2), "Related Person");
            ScrollToElement(recordsAreaHeaderCell(3));
            ValidateElementText(recordsAreaHeaderCell(3), "Related Relationship");
            ScrollToElement(recordsAreaHeaderCell(4));
            ValidateElementText(recordsAreaHeaderCell(4), "Inside Household");
            ScrollToElement(recordsAreaHeaderCell(5));
            ValidateElementText(recordsAreaHeaderCell(5), "Family Member");
            ScrollToElement(recordsAreaHeaderCell(6));
            ValidateElementText(recordsAreaHeaderCell(6), "Primary Carer");
            ScrollToElement(recordsAreaHeaderCell(7));
            ValidateElementText(recordsAreaHeaderCell(7), "Main Visitor");
            ScrollToElement(recordsAreaHeaderCell(8));
            ValidateElementText(recordsAreaHeaderCell(8), "Start Date");
            ScrollToElement(recordsAreaHeaderCell(9));
            ValidateElementText(recordsAreaHeaderCell(9), "End Date");

            return this;
        }

        public PersonRelationshipPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            WaitForElementNotVisible("CWRefreshPanel", 10);
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonRelationshipPage OpenPersonRelationshipRecord(string RecordId)
        {
            WaitForElement(RelatedPersonCell(RecordId));
            Click(RelatedPersonCell(RecordId));

            return this;
        }

        public PersonRelationshipPage NavigateToGestationPeriodPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(healthLeftSubMenu);
            Click(healthLeftSubMenu);

            WaitForElementToBeClickable(PersonGestationPeriodPageLeftSubMenuItem);
            Click(PersonGestationPeriodPageLeftSubMenuItem);


            return this;
        }

        public PersonRelationshipPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {

            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(NoRecordMessage, 5);
            }
            return this;
        }



        public PersonRelationshipPage InsertStartDate(string ValueToInsert)
        {
            SendKeys(startDateField, ValueToInsert);
            SendKeysWithoutClearing(startDateField, Keys.Tab);

            return this;
        }

    }
}
