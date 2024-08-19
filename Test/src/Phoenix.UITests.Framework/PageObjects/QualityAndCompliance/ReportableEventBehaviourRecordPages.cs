using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.QualityAndCompliance
{
    public class ReportableEventBehaviourRecordPages : CommonMethods
    {
        public ReportableEventBehaviourRecordPages(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ReportableEventBehaviourRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableeventbehaviour&')]");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By ReportableEventIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableevent&')]");


        readonly By pageHeader = By.XPath("//h1[@title='Reportable Event: New']");
        readonly By BackButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By RelatedItemMenu = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']/a");

        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By eventId = By.Id("CWField_identifier");
        readonly By ImpactsTab = By.Id("CWNavGroup_CareProviderReportableEventImpacts");
        readonly By BehavioursTab = By.Id("CWNavGroup_CareProviderReportableEventBehaviour");


        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        
        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");

        readonly By attachment_linkFeild = By.Id("CWNavItem_Attachments");
        readonly By action_linkFeild = By.Id("CWNavItem_CareProviderReportableEventAction");
        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

        readonly By Sequence_Field = By.Id("CWField_sequence");
        readonly By Frequency_Field = By.Id("CWField_frequency");

        readonly By ReportableEventBehaviourActionType_Field = By.Id("CWField_cpreportableeventbehaviouractiontypeid_cwname");
        readonly By ReportableEventBehaviourActionType_Lookup = By.Id("CWLookupBtn_cpreportableeventbehaviouractiontypeid");
        readonly By ReportableEventBehaviourType_Lookup = By.Id("CWLookupBtn_careproviderreportableeventbehaviourtypeid");
        readonly By ReportableEventSeverity_Lookup = By.Id("CWLookupBtn_reportableeventseverityid");

        readonly By ReportableEventBehaviourType_Field = By.Id("CWField_careproviderreportableeventbehaviourtypeid_Link");
        readonly By ReportableEventSeverity_Field = By.Id("CWField_reportableeventseverityid_cwname");
        readonly By ReportableEventPeopleAffected_Field = By.Id("CWField_peopleaffectedid");
        readonly By ReportableEventComments_Field = By.Id("CWField_comments");


        #endregion

        #region Reportable General Fields

        readonly By EventId_Field = By.Id("CWControlHolder_identifier");
        readonly By ResponsibleTeam_Field = By.Id("CWField_ownerid_cwname");
        readonly By ResponsibleTeam_LookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span");
        readonly By ResponsibleUser_Field = By.Id("CWField_responsibleuserid_cwname");
        readonly By ResponsibleUser_LookupButton = By.Id("CWLookupBtn_responsibleuserid");
        readonly By ResponsibleUser_MandatoryField = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label/span");
        readonly By Provider_Field = By.Id("CWField_providerid_cwname");
        readonly By Provider_LookUpButton = By.Id("CWLookupBtn_providerid");


        #endregion

        #region Reportable Event Summary Fields
        readonly By GeneralSeverity_Field = By.Id("CWField_careproviderreportableeventseverityid_cwname");
        readonly By GeneralSeverity_LookUpButton = By.Id("CWLookupBtn_careproviderreportableeventseverityid");
        readonly By EventType_Field = By.Id("CWField_careproviderreportableeventtypeid_cwname");
        readonly By EventType_LookUpButton = By.Id("CWLookupBtn_careproviderreportableeventtypeid");
        readonly By EventType_MandatoryField = By.Id("//*[@id='CWLabelHolder_careproviderreportableeventtypeid']/label/span");
        readonly By Category_Field = By.Id("CWField_careproviderreportableeventcategoryid_cwname");
        readonly By Category_LookUpButton = By.Id("CWLookupBtn_careproviderreportableeventcategoryid");
        readonly By SubCategory_Field = By.Id("CWControlHolder_careproviderreportableeventsubcategoryid");
        readonly By SubCategory_LookUpButton = By.Id("CWLookupBtn_careproviderreportableeventsubcategoryid");
        #endregion
       // CWField_careproviderreportableeventsubcategoryid_cwname
        
        #region Reportable Event Contexts Fields
        readonly By StartDate_Field = By.Id("CWField_startdate");
        readonly By StartDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_startdate'']/label/span");
        readonly By StartDateTime_Field = By.Id("CWField_startdate_Time");
        readonly By EndDate_Field = By.Id("CWField_enddate");
        readonly By EndDateTime_Field = By.Id("CWField_enddate_Time");
        readonly By PrimaryCause_Field = By.Id("CWControlHolder_cpreportableeventprimarycauseid");
        readonly By PrimaryCause_LookUpButton = By.Id("CWLookupBtn_primarycauseid");
        readonly By UnderlyingCause_Field = By.Id("CWControlHolder_cpreportableeventunderlyingcauseid");
        readonly By UnderlyingCause_LookUpButton = By.Id("CWLookupBtn_underlyingcauseid");

        #endregion

        #region Reportable Event Managing
        readonly By EventStatus_Field = By.Id("CWControlHolder_careproviderreportableeventstatusid");
        readonly By EventStatus_LookUpButton = By.Id("CWLookupBtn_careproviderreportableeventstatusid");
        readonly By EventStatus_MandatoryField = By.Id("//*[@id='CWLabelHolder_careproviderreportableeventstatusid']/label/span");
        readonly By StatusChanged_DateField = By.Id("CWField_statuschangedon");
        readonly By StatusChanged_MandatoryField = By.Id("//*[@id='CWLabelHolder_statuschangedon']/label/span");
        readonly By StatusChangedBy_Field = By.Id("CWField_statuschangedbyid_cwname");
        readonly By StatusChangedBy_LookUpButton = By.Id("CWLookupBtn_statuschangedbyid");
        readonly By StatusChangedBy_MandatoryField = By.Id("//*[@id='CWLabelHolder_statuschangedbyid']/label/span");
        readonly By StatusChangedByField = By.Id("CWField_statuschangedbyid_Link");

        // CWField_careproviderreportableeventstatusid_cwname
        readonly By Notes_TextField = By.XPath("//div[@id='cke_1_contents']");
        #endregion
        readonly By ReportableEventBehaviourIFrame = By.Id("CWRelatedRecordPanel_IFrame");

        public ReportableEventBehaviourRecordPages WaitForReportableEventBehaviourRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

           
            WaitForElement(ReportableEventBehaviourRecordIFrame);
            SwitchToIframe(ReportableEventBehaviourRecordIFrame);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }



      

        public ReportableEventBehaviourRecordPages ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }

        public ReportableEventBehaviourRecordPages ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }

        public ReportableEventBehaviourRecordPages ClickSaveButton()
        {
            WaitForElement(SaveButton);
            Click(SaveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public ReportableEventBehaviourRecordPages ClickSubMenu()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            return this;
        }
        
        public ReportableEventBehaviourRecordPages ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ReportableEventBehaviourRecordPages VerifySequence_Field(String value)
        {
            ScrollToElement(Sequence_Field);
            ValidateElementAttribute(Sequence_Field,"range",value);

            return this;
        }

        public ReportableEventBehaviourRecordPages InsertSequence_Field(String value)
        {
            ScrollToElement(Sequence_Field);
            SendKeys(Sequence_Field, value);

            return this;
        }


        public ReportableEventBehaviourRecordPages ValidateBehaviourActionTypeField(Boolean ExpectVisible)

             {
            if (ExpectVisible)
            {
                WaitForElementVisible(ReportableEventBehaviourActionType_Field);
            }
            else
            {
                WaitForElementNotVisible(ReportableEventBehaviourActionType_Field, 3);
            }
            return this;
        }
        public ReportableEventBehaviourRecordPages TapBehaviourActionTypeLookup()

        {
            Click(ReportableEventBehaviourActionType_Lookup);
            return this;
        }

        public ReportableEventBehaviourRecordPages TapSeverityLookup()

        {
            Click(ReportableEventSeverity_Lookup);
            return this;
        }

        public ReportableEventBehaviourRecordPages TapBehaviourTypeLookup()

        {
            Click(ReportableEventBehaviourType_Lookup);
            return this;
        }
        public ReportableEventBehaviourRecordPages ValidateBehaviourTypeField(Boolean ExpectVisible)

        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ReportableEventBehaviourType_Field);
            }
            else
            {
                WaitForElementNotVisible(ReportableEventBehaviourType_Field, 3);
            }
            return this;
        }

        public ReportableEventBehaviourRecordPages VerifyFrequency_Field(String value)
        {
            ScrollToElement(Frequency_Field);
            ValidateElementAttribute(Frequency_Field, "range", value);
            return this;
        }

        public ReportableEventBehaviourRecordPages InsertFrequency_Field(String value)
        {
            ScrollToElement(Frequency_Field);
            SendKeys(Frequency_Field,value);
            return this;
        }

        public ReportableEventBehaviourRecordPages ValidateReportableEventSeverityField(Boolean ExpectVisible)

             {
            if (ExpectVisible)
            {
                WaitForElementVisible(ReportableEventSeverity_Field);
            }
            else
            {
                WaitForElementNotVisible(ReportableEventSeverity_Field, 3);
            }
            return this;
        }

        public ReportableEventBehaviourRecordPages ValidateReportableEventPeopleAffectedField(Boolean ExpectVisible)

        {
            ScrollToElement(ReportableEventPeopleAffected_Field);

            if (ExpectVisible)
            {
                WaitForElementVisible(ReportableEventPeopleAffected_Field);
            }
            else
            {
                WaitForElementNotVisible(ReportableEventPeopleAffected_Field, 3);
            }
            return this;
        }

        public ReportableEventBehaviourRecordPages ValidateReportableEventCommentsField(Boolean ExpectVisible)

        {
            ScrollToElement(ReportableEventComments_Field);
            
            if (ExpectVisible)
            {
                WaitForElementVisible(ReportableEventComments_Field);
            }
            else
            {
                WaitForElementNotVisible(ReportableEventComments_Field, 3);
            }
            return this;
        }
    }
}
