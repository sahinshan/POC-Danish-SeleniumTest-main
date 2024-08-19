using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class AllegedVictimRecordPage  : CommonMethods
    {
        public AllegedVictimRecordPage (IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=allegation&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By leftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By allegationInvestigators = By.Id("CWNavItem_AllegationInvestigator");

        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By general_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div[1]/span");
        readonly By allegedVictim_FieldTitle = By.XPath("//*[@id='CWLabelHolder_allegedvictimid']/label[text()='Alleged Victim']");
        readonly By allegedVictim_Field = By.Id("CWField_allegedvictimid_cwname");
        readonly By allegedVictim_LookUpButton = By.Id("CWLookupBtn_allegedvictimid");
        readonly By allegedAbuser_FieldTitle = By.XPath("//*[@id='CWLabelHolder_allegedabuserid']/label[text()='Alleged Abuser']");
        readonly By allegedAbuser_Field = By.Id("CWField_allegedabuserid_Link");
        readonly By allegedAbuser_LookUpButton = By.Id("CWLookupBtn_allegedabuserid");

        readonly By allegationDetails_SectionTitle = By.XPath("//*[@id='CWSection_AllegationDetailsSection']/fieldset/div[1]/span");
        readonly By allegationDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_allegationdate']/label[text()='Allegation Date']");
        readonly By allegationDate_Field = By.Id("CWField_allegationdate");
        readonly By association_FieldTitle = By.XPath("//*[@id='CWLabelHolder_allegedabuserroleid']/label");
        readonly By association_Field = By.Id("CWField_allegedabuserroleid_cwname");
        readonly By association_LookUpButton = By.Id("CWLookupBtn_allegedabuserroleid");
        readonly By partOfALargerInvestigation_Title = By.XPath("//*[@id='CWLabelHolder_partoflargerinvestigation']/label");
        readonly By partOfALargerInvestigation_YesRadioButton = By.XPath("//*[@id='CWControlHolder_partoflargerinvestigation']/div[1]/label");
        readonly By partOfALargerInvestigation_NoRadioButton = By.XPath("//*[@id='CWControlHolder_partoflargerinvestigation']/div[2]/label");
        readonly By responsibleTeam_FieldTitle = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By responsibleTeam_Field = By.Id("CWLabelHolder_ownerid");


        readonly By absueDetails_SectionTitle = By.XPath("//*[@id='CWSection_AbuseDetailsSection']/fieldset/div[1]/span");
        readonly By primaryCategoryOfAbuse_FieldTitle = By.XPath("//*[@id='CWLabelHolder_allegationcategoryid']/label[text()='Primary Category of Abuse']");
        readonly By primaryCategoryOfAbuse_Field = By.Id("CWField_allegationcategoryid_cwname");
        readonly By primaryCategoryOfAbuse_LookUpButton = By.Id("CWLookupBtn_allegationcategoryid");
        readonly By primaryPlaceofAllegedAbuse_FieldTitle = By.XPath("//*[@id='CWLabelHolder_primaryallegedabuseplaceid']/label");
        readonly By primaryPlaceofAllegedAbuse_Field = By.Id("CWField_primaryallegedabuseplaceid_cwname");
        readonly By primaryPlaceofAllegedAbuse_LookUpButton = By.Id("CWLookupBtn_primaryallegedabuseplaceid");
        readonly By secondaryCategoriesofAbuse_FieldTitle = By.XPath("//*[@id='CWLabelHolder_secondarycategories']/label");
        readonly By secondaryCategoriesofAbuse_Field = By.Id("CWField_secondarycategories_List");
        readonly By secondaryCategoriesofAbuse_LookUpButton = By.Id("CWLookupBtn_secondarycategories");
        readonly By otherPlaceofAllegedAbuse_FieldTitle = By.XPath("//*[@id='CWLabelHolder_otherallegedabuseplaceid']/label");
        readonly By otherPlaceofAllegedAbuse_Field = By.Id("CWField_otherallegedabuseplaceid_List");
        readonly By otherPlaceofAllegedAbuse_LookUpButton = By.Id("CWLookupBtn_otherallegedabuseplaceid");
        readonly By abuseDateFrom_FieldTitle = By.XPath("//*[@id='CWLabelHolder_abusestartdate']/label");
        readonly By abuseDateFrom_Field = By.Id("CWField_abusestartdate");
        readonly By abuseDateTo_FieldTitle = By.XPath("//*[@id='CWLabelHolder_abuseenddate']/label");
        readonly By abuseDateTo_Field = By.Id("CWField_abuseenddate");
        readonly By normalPlaceOfResidence_FieldTitle = By.XPath("//*[@id='CWLabelHolder_normalresidenceplaceid']/label");
        readonly By normalPlaceOfResidence_Field = By.Id("CWField_normalresidenceplaceid_cwname");
        readonly By normalPlaceOfResidence_LookUpButton = By.Id("CWLookupBtn_normalresidenceplaceid");
        readonly By abuseDetails_FieldTitle = By.XPath("//*[@id='CWLabelHolder_abusedetails']/label");
        readonly By abuseDetails_Field = By.Id("CWField_abusedetails");

        readonly By allegationOutcome_SectionTitle = By.XPath("//*[@id='CWSection_AllegationOutcomeSection']/fieldset/legend");
        readonly By relatedSafeguardingRecord_FieldTitle = By.XPath("//*[@id='CWLabelHolder_adultsafeguardingid']/label");
        readonly By relatedSafeguardingRecord_Field = By.Id("CWField_adultsafeguardingid_cwname");
        readonly By relatedSafeguardingRecord_LookUpButton = By.Id("CWLookupBtn_adultsafeguardingid");
        readonly By outcome_FieldTitle = By.XPath("//*[@id='CWLabelHolder_allegationoutcomeid']/label");
        readonly By outcome_Field = By.Id("CWField_allegationoutcomeid_cwname");
        readonly By outcome_LookUpButton = By.Id("CWLookupBtn_allegationoutcomeid");
        readonly By outcomeDetails_FieldTitle = By.XPath("//*[@id='CWLabelHolder_outcomedetails']/label");
        readonly By outcomeDetails_Field = By.Id("CWField_outcomedetails");


        readonly By policeDetails_SectionTitle = By.XPath("//*[@id='CWTab_PoliceDetailsTab']/div[1]/span");
        readonly By DecisionToNotifyPolice_Title = By.XPath("//*[@id='CWSection_DecisionToNotifyPoliceSection']/fieldset/div[1]/span");
        readonly By shouldThePoliceBeNotified_Title = By.XPath("//*[@id='CWLabelHolder_shouldpolicenotified']/label");
        readonly By shouldThePoliceBeNotified_YesRadioButton = By.Id("CWField_shouldpolicenotified_1");
        readonly By shouldThePoliceBeNotified_NoRadioButton = By.Id("CWField_shouldpolicenotified_0");
        readonly By decisionDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_decisiondate']/label");
        readonly By decisionDate = By.Id("CWField_decisiondate");
        readonly By reasonForDecision_FieldTitle = By.XPath("//*[@id='CWLabelHolder_reasonfordecision']/label");
        readonly By reasonForDecision_Field = By.Id("CWField_reasonfordecision");

        readonly By policeNotificationDetails_SectionTitle = By.XPath("//*[@id='CWSection_PoliceNotificationDetailsSection']/fieldset/div[1]/span");
        readonly By policehaveBeenNotified_Title = By.XPath("//*[@id='CWLabelHolder_policenotified']/label");
        readonly By policehaveBeenNotified_YesRadioButton = By.Id("CWField_policenotified_1");
        readonly By policehaveBeenNotified_NoRadioButton = By.Id("CWField_policenotified_0");


        public AllegedVictimRecordPage WaitForAllegedVictimRecordPageToLoad(string TaskTitle)
        {
            driver.SwitchTo().DefaultContent();


            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(general_SectionTitle);
            WaitForElement(allegedVictim_FieldTitle);
            WaitForElement(responsibleTeam_FieldTitle);
            WaitForElement(allegedAbuser_FieldTitle);
            WaitForElement(allegationDetails_SectionTitle);
            WaitForElement(allegationDate_FieldTitle);
            WaitForElement(association_FieldTitle);
            WaitForElement(partOfALargerInvestigation_Title);
            WaitForElement(absueDetails_SectionTitle);
            WaitForElement(primaryCategoryOfAbuse_FieldTitle);
            WaitForElement(primaryPlaceofAllegedAbuse_FieldTitle);
            WaitForElement(secondaryCategoriesofAbuse_FieldTitle);
            WaitForElement(otherPlaceofAllegedAbuse_FieldTitle);
            WaitForElement(abuseDateFrom_FieldTitle);
            WaitForElement(abuseDateTo_FieldTitle);
            WaitForElement(normalPlaceOfResidence_FieldTitle);
            WaitForElement(abuseDetails_FieldTitle);
            WaitForElement(allegationOutcome_SectionTitle);
            WaitForElement(relatedSafeguardingRecord_FieldTitle);
            WaitForElement(outcome_FieldTitle);
            WaitForElement(outcomeDetails_FieldTitle);
            WaitForElement(policeDetails_SectionTitle);
            WaitForElement(DecisionToNotifyPolice_Title);
            WaitForElement(shouldThePoliceBeNotified_Title);
            WaitForElement(decisionDate_FieldTitle);
            WaitForElement(reasonForDecision_FieldTitle);
            WaitForElement(policeNotificationDetails_SectionTitle);
            WaitForElement(policehaveBeenNotified_Title);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            ValidateElementTextContainsText(pageHeader, "Allegation:\r\n" + TaskTitle);

            return this;
        }

        public AllegedVictimRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            return this;
        }

        public AllegedVictimRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public AllegedVictimRecordPage NavigateAllegationInvestigatorsSubpage()
        {
            WaitForElementToBeClickable(leftMenuButton);
            Click(leftMenuButton);

            WaitForElementToBeClickable(allegationInvestigators);
            Click(allegationInvestigators);

            return this;
        }

    }
}
