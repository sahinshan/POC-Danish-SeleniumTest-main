using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CopyCarePlanPopupPage : CommonMethods
    {
        public CopyCarePlanPopupPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By personcareplanIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personcareplan&')]");
        readonly By copyCarePlanIFrame = By.Id("iframe_CopyCarePlan");

        #endregion

        readonly By carePlanTypeId_LookupButton = By.Id("CWLookupBtn_CWCarePlanTypeId");
        readonly By careCoordinatorId_LookupButton = By.Id("CWLookupBtn_CWCareCoordinatorId");
        readonly By responsibleTeamId_LookupButton = By.Id("CWLookupBtn_CWResponsibleTeamId");
        readonly By startDate_Field = By.Id("CWStartDate");
        readonly By ReviewDate_Field = By.Id("CWReviewDate");
        readonly By ReviewFrequency_Field = By.Id("CWReviewFrequencyId");
        readonly By case_LookupButton = By.Id("CWLookupBtn_CWCaseId");
        readonly By familyInvolvedInCarePlan_Picklist = By.Id("CWFamilyInvolvedInCarePlanId");
        readonly By agreedWithPersonOrLegalRepresentative_YesRadioButton = By.Id("CWPlanAgreed_1");
        readonly By agreedWithPersonOrLegalRepresentative_NoRadioButton = By.Id("CWPlanAgreed_0");

        readonly By copy_Button = By.Id("CWCopy");
        readonly By cancel_Button = By.Id("CWCancel");


        public CopyCarePlanPopupPage WaitForCopyCarePlanPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personcareplanIFrame);
            SwitchToIframe(personcareplanIFrame);

            WaitForElement(copyCarePlanIFrame);
            SwitchToIframe(copyCarePlanIFrame);
            
            WaitForElement(carePlanTypeId_LookupButton);
            WaitForElement(careCoordinatorId_LookupButton);
            WaitForElement(responsibleTeamId_LookupButton);
            WaitForElement(startDate_Field);
            WaitForElement(case_LookupButton);
            WaitForElement(familyInvolvedInCarePlan_Picklist);
            WaitForElement(agreedWithPersonOrLegalRepresentative_YesRadioButton);
            WaitForElement(agreedWithPersonOrLegalRepresentative_NoRadioButton);
            WaitForElement(copy_Button);
            WaitForElement(cancel_Button);

            return this;
        }

        public CopyCarePlanPopupPage WaitForCopyCarePlanPageToLoadWhenNocaseNCareCoordinator()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personcareplanIFrame);
            SwitchToIframe(personcareplanIFrame);

            WaitForElement(copyCarePlanIFrame);
            SwitchToIframe(copyCarePlanIFrame);

          //  WaitForElement(carePlanTypeId_LookupButton);
          
            WaitForElement(responsibleTeamId_LookupButton);
            WaitForElement(startDate_Field);
            WaitForElement(ReviewDate_Field);
            WaitForElement(familyInvolvedInCarePlan_Picklist);
            WaitForElement(agreedWithPersonOrLegalRepresentative_YesRadioButton);
            WaitForElement(agreedWithPersonOrLegalRepresentative_NoRadioButton);
            WaitForElement(copy_Button);
            WaitForElement(cancel_Button);

            return this;
        }

        public CopyCarePlanPopupPage ClickCarePlanTypeIdLookupButton()
        {
            this.Click(carePlanTypeId_LookupButton);

            return this;
        }

        public CopyCarePlanPopupPage ClickCareCoordinatorLookupButton()
        {
            this.ScrollToElementViaJavascript(careCoordinatorId_LookupButton);
            this.Click(careCoordinatorId_LookupButton);

            return this;
        }

        public CopyCarePlanPopupPage ClickResponsibleTeamLookupButton()
        {
            this.ScrollToElementViaJavascript(responsibleTeamId_LookupButton);
            this.Click(responsibleTeamId_LookupButton);

            return this;
        }

        public CopyCarePlanPopupPage InsertStartDate(string TextToInsert)
        {
            this.ScrollToElementViaJavascript(startDate_Field);
            this.SendKeys(startDate_Field, TextToInsert);

            return this;
        }

        public CopyCarePlanPopupPage InsertReviewDate(string TextToInsert)
        {
            this.ScrollToElementViaJavascript(ReviewDate_Field);
            this.SendKeys(ReviewDate_Field, TextToInsert);

            return this;
        }

        public CopyCarePlanPopupPage ValidateReviewDateField_Visible()
        {
            WaitForElementVisible(ReviewDate_Field);

            return this;
        }

        public CopyCarePlanPopupPage ValidateReviewFrequencyield_Visible()
        {
            WaitForElementVisible(ReviewFrequency_Field);

            return this;
        }

        public CopyCarePlanPopupPage SelectReviewFrequencyield(String ReviewFrequency)
        {
            this.ScrollToElementViaJavascript(ReviewFrequency_Field);
            this.SelectPicklistElementByText(ReviewFrequency_Field, ReviewFrequency);

           
            return this;
        }

        public CopyCarePlanPopupPage ClickCaseLookupButton()
        {
            this.ScrollToElementViaJavascript(case_LookupButton);
            this.Click(case_LookupButton);

            return this;
        }

        public CopyCarePlanPopupPage SelectFamilyInvolvedInCarePlan(string TextToSelect)
        {
            this.ScrollToElementViaJavascript(familyInvolvedInCarePlan_Picklist);
            this.SelectPicklistElementByText(familyInvolvedInCarePlan_Picklist, TextToSelect);

            return this;
        }

        public CopyCarePlanPopupPage ClickAgreedWithPersonOrLegalRepresentativeYesRadioButton()
        {
            this.ScrollToElementViaJavascript(agreedWithPersonOrLegalRepresentative_YesRadioButton);
            this.Click(agreedWithPersonOrLegalRepresentative_YesRadioButton);

            return this;
        }

        public CopyCarePlanPopupPage ClickAgreedWithPersonOrLegalRepresentativeNoRadioButton()
        {
            this.ScrollToElementViaJavascript(agreedWithPersonOrLegalRepresentative_NoRadioButton);
            this.Click(agreedWithPersonOrLegalRepresentative_NoRadioButton);

            return this;
        }

        public CopyCarePlanPopupPage ClickCopyButton()
        {
            this.Click(copy_Button);

            return this;
        }

        public CopyCarePlanPopupPage ClickCancelButton()
        {
            this.Click(cancel_Button);

            return this;
        }

    }
}
