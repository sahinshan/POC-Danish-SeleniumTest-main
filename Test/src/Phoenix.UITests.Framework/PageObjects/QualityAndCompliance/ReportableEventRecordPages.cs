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
    public class ReportableEventRecordPage : CommonMethods
    {
        public ReportableEventRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ReportableEventRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableevent&')]");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");

        readonly By pageHeader = By.XPath("//h1[@title='Reportable Event: New']");
        readonly By BackButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");

        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By eventId = By.Id("CWField_identifier");
        readonly By ImpactsTab = By.Id("CWNavGroup_CareProviderReportableEventImpacts");
        readonly By BehavioursTab = By.Id("CWNavGroup_CareProviderReportableEventBehaviour");


        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        readonly By attachment_linkFeild = By.Id("CWNavItem_Attachments");
        readonly By action_linkFeild = By.Id("CWNavItem_CareProviderReportableEventAction");
        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

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


        public ReportableEventRecordPage WaitForReportableEventRecordPagePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ReportableEventRecordIFrame);
            SwitchToIframe(ReportableEventRecordIFrame);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }



        public ReportableEventRecordPage WaitForReportableEventRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            this.WaitForElement(iframe_CWDataFormDialog);
            this.SwitchToIframe(iframe_CWDataFormDialog);

            this.WaitForElement(ReportableEventRecordIFrame);
            this.SwitchToIframe(ReportableEventRecordIFrame);


            return this;
        }

        public ReportableEventRecordPage WaitForReportableEventInactiveRecordageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ReportableEventRecordIFrame);
            SwitchToIframe(ReportableEventRecordIFrame);

            WaitForElement(deleteRecordButton);
            WaitForElement(assignRecordButton);

            return this;
        }

        public ReportableEventRecordPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }

        public ReportableEventRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }

        public ReportableEventRecordPage ClickSaveButton()
        {
            WaitForElement(SaveButton);
            Click(SaveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public ReportableEventRecordPage ClickSubMenu()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            return this;
        }
        public ReportableEventRecordPage ClickAttachmentLink()
        {
            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(attachment_linkFeild);
            Click(attachment_linkFeild);

            return this;
        }
        public ReportableEventRecordPage NavigateToReportableEventActions()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(action_linkFeild);
            Click(action_linkFeild);

            return this;
        }
        public ReportableEventRecordPage ClickBackButton()
        {
            WaitForElement(BackButton);
            Click(BackButton);

            return this;
        }
        public ReportableEventRecordPage ClickProviderLookUpButton()
        {
            WaitForElement(Provider_LookUpButton);
            Click(Provider_LookUpButton);

            return this;
        }
        public ReportableEventRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ReportableEventRecordPage TapGeneralSeverityLookupButton()
        {
            Click(GeneralSeverity_LookUpButton);

            return this;
        }

        public ReportableEventRecordPage TapEventTypeLookupButton()
        {
            Click(EventType_LookUpButton);

            return this;
        }
        public ReportableEventRecordPage TapResponsibleUserLookupButton()
        {
            Click(ResponsibleUser_LookupButton);

            return this;
        }
        public ReportableEventRecordPage TapEventTypeStatusLookupButton()
        {
            Click(EventStatus_LookUpButton);

            return this;
        }
        public ReportableEventRecordPage NavigateToDetailsTab()
        {
            WaitForElement(DetailsTab);
            Click(DetailsTab);

            return this;
        }

        public ReportableEventRecordPage NavigateToImpactsTab()
        {
            WaitForElement(ImpactsTab);
            Click(ImpactsTab);

            return this;
        }

        public ReportableEventRecordPage NavigateToBehavioursTab()
        {
            WaitForElement(BehavioursTab);
            Click(BehavioursTab);

            return this;
        }

        public ReportableEventRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

        public ReportableEventRecordPage ValidateResponsibleTeamMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_MandatoryField);
            else
                WaitForElementNotVisible(ResponsibleTeam_MandatoryField, 3);

            return this;
        }

        public ReportableEventRecordPage ValidateImpactFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ImpactsTab);
            else
                WaitForElementNotVisible(ImpactsTab, 3);

            return this;
        }
        public ReportableEventRecordPage InsertGeneralSeverity(string GeneralSeverity)
        {
            WaitForElement(GeneralSeverity_Field);
            SendKeys(GeneralSeverity_Field, GeneralSeverity);

            return this;
        }
        
        public ReportableEventRecordPage InsertEventType(string EventType)
        {
            WaitForElement(EventType_Field);
            SendKeys(EventType_Field, EventType);

            return this;
        }
        public ReportableEventRecordPage InsertStartDate(string DateToInsert)
        {
            WaitForElement(StartDate_Field, 5);
            SendKeys(StartDate_Field, DateToInsert);

            return this;
        }
        

         public ReportableEventRecordPage InsertEventStatus(string EventStatus)
        {
            WaitForElement(EventStatus_Field);
            SendKeys(EventStatus_Field, EventStatus);

            return this;
        }

        public ReportableEventRecordPage TapEventTypeCategoryLookupButton()
        {
            ScrollToElement(EndDate_Field);
            Click(Category_LookUpButton);

            return this;

            
        }

        public ReportableEventRecordPage GetReportableEventRecordId()
        {
            GetElementValue(eventId);

            return this;
        }

        public ReportableEventRecordPage ValidateStartDateNonEditable()
        {
            WaitForElement(StartDate_Field);
            ValidateElementDisabled(StartDate_Field);

            return this;
        }

        public ReportableEventRecordPage ValidateStartTimeNonEditable()
        {
            WaitForElement(StartDateTime_Field);
            ValidateElementDisabled(StartDateTime_Field);

            return this;
        }

        public ReportableEventRecordPage ValidateEndDateFieldText(string ExpectText)
        {
            ScrollToElement(EndDate_Field);
            string fieldText = GetElementValue(EndDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventRecordPage ValidateEventIdNonEditable()
        {
            ScrollToElement(EventId_Field);
            WaitForElement(EventId_Field);
           // ValidateElementDisabled(EventId_Field);
          

            return this;
        }

        public ReportableEventRecordPage ValidateReportableEventSummaryGeneralSeverityField(bool ExpectVisible,String ExpectText)
        {
            ScrollToElement(GeneralSeverity_Field);
            if (ExpectVisible)
                WaitForElementVisible(GeneralSeverity_Field);
            else
                WaitForElementNotVisible(GeneralSeverity_Field, 3);

            string fieldText = GetElementValue(GeneralSeverity_Field);
            Assert.AreEqual(ExpectText, fieldText);
           
            return this;
        }

        public ReportableEventRecordPage ValidateReportableEventSummaryEventTypeField(bool ExpectVisible, String ExpectText)
        {
            ScrollToElement(EventType_Field);
            if (ExpectVisible)
                WaitForElementVisible(EventType_Field);
            else
                WaitForElementNotVisible(EventType_Field, 3);

            string fieldText = GetElementValue(EventType_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }


        public ReportableEventRecordPage ValidateReportableEventSummaryCategoryField(bool ExpectVisible, String ExpectText)
        {
            ScrollToElement(Category_Field);
            if (ExpectVisible)
                WaitForElementVisible(Category_Field);
            else
                WaitForElementNotVisible(Category_Field, 3);

            string fieldText = GetElementValue(Category_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventRecordPage ValidateReportableEventSummarySubCategoryField(bool ExpectVisible, String ExpectText)
        {
            ScrollToElement(SubCategory_Field);
            if (ExpectVisible)
                WaitForElementVisible(SubCategory_Field);
            else
                WaitForElementNotVisible(SubCategory_Field, 3);

            string fieldText = GetElementValue(SubCategory_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventRecordPage ValidateReportableEventContextStartDate(bool ExpectVisible)
        {
            ScrollToElement(StartDate_Field);
            if (ExpectVisible)
                WaitForElementVisible(StartDate_Field);
            else
                WaitForElementNotVisible(StartDate_Field, 3);

            return this;
        }

        public ReportableEventRecordPage ValidateReportableEventContextEndDate(bool ExpectVisible)
        {
            ScrollToElement(EndDate_Field);
            if (ExpectVisible)
                WaitForElementVisible(EndDate_Field);
            else
                WaitForElementNotVisible(EndDate_Field, 3);

            return this;
        }
        public ReportableEventRecordPage ValidateResponsibleUserMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleUser_MandatoryField);
            else
                WaitForElementNotVisible(ResponsibleUser_MandatoryField, 3);

            return this;
        }

        public ReportableEventRecordPage ValidateReportableEventCategoryPrimaryCauseField(bool ExpectVisible, String ExpectText)
        {
            MoveToElementInPage(PrimaryCause_Field);
            if (ExpectVisible)
                WaitForElementVisible(PrimaryCause_Field);
            else
                WaitForElementNotVisible(PrimaryCause_Field, 3);

            string fieldText = GetElementValue(PrimaryCause_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventRecordPage ValidateReportableEventCategoryUnderlyingCauseField(bool ExpectVisible, String ExpectText)
        {
            MoveToElementInPage(UnderlyingCause_Field);
            if (ExpectVisible)
                WaitForElementVisible(UnderlyingCause_Field);
            else
                WaitForElementNotVisible(UnderlyingCause_Field, 3);

            string fieldText = GetElementValue(UnderlyingCause_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventRecordPage ValidateReportableEventManagingEventStatusField(bool ExpectVisible, String ExpectText)
        {
            ScrollToElement(EventStatus_Field);
            if (ExpectVisible)
                WaitForElementVisible(EventStatus_Field);
            else
                WaitForElementNotVisible(EventStatus_Field, 3);

            string fieldText = GetElementValue(EventStatus_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventRecordPage ValidateReportableEventManagingStatusChangedField(bool ExpectVisible, String ExpectText)
        {
            ScrollToElement(StatusChanged_DateField);
            if (ExpectVisible)
                WaitForElementVisible(StatusChanged_DateField);
            else
                WaitForElementNotVisible(StatusChanged_DateField, 3);

            string fieldText = GetElementValue(StatusChanged_DateField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventRecordPage ValidateReportableEventManagingStatusChangedByField(bool ExpectVisible)
        {
            ScrollToElement(StatusChangedByField);
            if (ExpectVisible)
                WaitForElementVisible(StatusChangedByField);
            else
                WaitForElementNotVisible(StatusChangedByField, 3);

           
            return this;
        }

       
        public ReportableEventRecordPage ValidateReportableentNotesText(bool ExpectVisible)
        {

            ScrollToElement(Notes_TextField);
            if (ExpectVisible)
                WaitForElementVisible(Notes_TextField);
            else
                WaitForElementNotVisible(Notes_TextField, 3);

            return this;
        }


    }
}
