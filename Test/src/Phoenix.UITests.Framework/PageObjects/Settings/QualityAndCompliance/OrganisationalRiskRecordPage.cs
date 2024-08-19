
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class OrganisationalRiskRecordPage : CommonMethods
    {
        public OrganisationalRiskRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=organisationalrisk&')]");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Organisational Risks']");
        readonly By save_Button = By.Id("TI_SaveButton");
        readonly By saveAndClose_Button = By.Id("TI_SaveAndCloseButton");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back'][@onclick='CW.DataForm.Close(); return false;']");
      

     


        #region Fields 
        readonly By riskType_LookupButton = By.Id("CWLookupBtn_organisationalrisktypeid");
        readonly By responsibleUser_LookupButton = By.Id("CWLookupBtn_responsibleuserid");
        readonly By riskDescription_Field = By.Id("CWField_riskdescription");
        readonly By status_Field = By.Id("CWField_riskstatusid"); 
        readonly By nextReviewDate_Field = By.Id("CWField_nextreviewdate"); 
        readonly By corporateRiskRegister_Field = By.Id("CWField_corporateriskregisterid");
        readonly By  consequences_Field=By.Id("CWField_consequences"); 
        readonly By likelihood_Field = By.Id("CWField_likelihood");
        readonly By residualConsequence_Field = By.Id("CWField_residualconsequences"); 
        readonly By residualLikelihood_Field = By.Id("CWField_residuallikelihood");
        #endregion

       


        public OrganisationalRiskRecordPage WaitForOrganisationalRiskRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);
           
            return this;
        }        

        public OrganisationalRiskRecordPage ClickRiskTypeLooupButton()
        {
            WaitForElement(riskType_LookupButton);
            Click(riskType_LookupButton);

        }
        public OrganisationalRiskRecordPage ClickResponsibleUserLookupButton()
        {
            WaitForElement(responsibleUser_LookupButton);
            Click(responsibleUser_LookupButton);

        }

        public OrganisationalRiskRecordPage InsertRiskDescription(string Text)
        {
            WaitForElement(riskDescription_Field);
            SendKeys(riskDescription_Field, Text);

            return this;
        }
      
        public OrganisationalRiskRecordPage InsertNextReviewDate(string TextToInsert)
        {
            WaitForElement(nextReviewDate_Field);

            SendKeys(nextReviewDate_Field, TextToInsert);

            return this;
        }
        public OrganisationalRiskRecordPage SelectStatus(string TextToSelect)
        {
            WaitForElement(status_Field);
            SelectPicklistElementByText(status_Field, TextToSelect);

            return this;
        }


        public OrganisationalRiskRecordPage SelectCorporateRiskRegister(string TextToSelect)
        {
            WaitForElement(corporateRiskRegister_Field);
            SelectPicklistElementByText(corporateRiskRegister_Field, TextToSelect);

            return this;
        }

        public OrganisationalRiskRecordPage InsertConsequence(string Text)
        {
            WaitForElement(consequences_Field);
            SendKeys(consequences_Field, Text);

            return this;
        }

        public OrganisationalRiskRecordPage InsertLikeliood(string Text)
        {
            WaitForElement(likelihood_Field);
            SendKeys(likelihood_Field, Text);

            return this;
        }

        public OrganisationalRiskRecordPage InsertResidualConsequence(string Text)
        {
            WaitForElement(residualConsequence_Field);
            SendKeys(residualConsequence_Field, Text);

            return this;
        }

        public OrganisationalRiskRecordPage InsertResidualLikeliood(string Text)
        {
            WaitForElement(residualLikelihood_Field);
            SendKeys(residualLikelihood_Field, Text);

            return this;
        }


        public OrganisationalRiskRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(save_Button);
            Click(save_Button);
            return this;
        }

        public OrganisationalRiskRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndClose_Button);
            Click(saveAndClose_Button);
            return this;
        }

     

        public OrganisationalRiskRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(back_Button);
            Click(back_Button);
            return this;
        }


       

    }
}
