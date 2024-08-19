
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonAllActivitiesSubPage : CommonMethods
    {
        public PersonAllActivitiesSubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWHTMLResourcePanel_IFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageTitle = By.XPath("//h1[text()='All Activities']");


        #region Top Menu

        readonly By printAllButton = By.Id("CWPrintAll");
        readonly By printSelectedButton = By.Id("CWPrintSelected");

        #endregion

        #region Search Area

        readonly By keyword_Field = By.Id("CWField_Keyword");
        readonly By ActivityType_Picklist = By.Id("CWField_ActivityTypeId");
        readonly By FromDateType_Picklist = By.Id("CWField_FromDateTypeId");
        readonly By FromDate_Field = By.Id("CWField_FromDate");
        readonly By ToDate_Field = By.Id("CWField_ToDate");
        readonly By ActualEndFromDate_Field = By.Id("CWField_ActualEndFromDate");
        readonly By ActualEndToDate_Field = By.Id("CWField_ActualEndToDate");
        readonly By activitycategory_LookupButton = By.Id("CWLookupBtn_activitycategory");
        readonly By activitysubcategory_LookupButton = By.Id("CWLookupBtn_activitysubcategory");
        readonly By systemuser_LookupButton = By.Id("CWLookupBtn_systemuser");
        readonly By team_LookupButton = By.Id("CWLookupBtn_team");
        readonly By StatusId_Picklist = By.Id("CWField_StatusId");
        readonly By CaseNotesOnly_CheckBox = By.Id("CWField_CaseNotesOnly");
        readonly By person_LookupButton = By.Id("CWLookupBtn_person");
        readonly By activityreason_LookupButton = By.Id("CWLookupBtn_activityreason");

        readonly By searchButton = By.Id("CWSearchButton");
        readonly By clearFiltersButton = By.Id("CWClearFiltersButton");

        #endregion

        #region Results area

        

        readonly By NoResultsMessage = By.XPath("//*[@id='CWResultsContainer']/div/h2[text()='NO RECORDS']");
        readonly By NoSearchPerformedMessage = By.XPath("//*[@id='CWResultsContainer']/div/span[text()='No search performed.']");

        readonly By selectAllActivitiesCheckBox = By.XPath("//*[@id='cwgridheaderselector']");

        readonly By ChangeViewButton = By.Id("CWChangeView");
        readonly By ExpandCollapseAllButton = By.Id("CWExpandCollapseAll");

        By activityRecordCheckBox(string RecordID) => By.Id("CHK_" + RecordID);

        By activityRecordSubjectCell(string RecordID, string subject) => By.XPath("//*[@id='" + RecordID + "']/td[3][text()='" + subject + "']");




        readonly By regardingHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[2]/a/*");
        readonly By subjectHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[3]/a/*");
        readonly By activityHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[4]/a/*");
        readonly By statusHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[5]/a/*");
        readonly By startDueDateHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[6]/a/*");
        readonly By actualEndHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[7]/a/*");
        readonly By caseNoteHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[8]/a/*");
        readonly By regardingTypeHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[9]/a/*");
        readonly By responsibleUserHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[10]/a/*");
        readonly By responsibleTeamHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[11]/a/*");
        readonly By modifiedByHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[12]/a/*");
        readonly By modifiedOnHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[13]/a/*");
        readonly By createdByHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[14]/a/*");
        readonly By createdOnHeader = By.XPath("//div[@id='CWResultsContainer']//*[@id='CWGridHeaderRow']/th[15]/a/*");




        By regardingCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[2]");
        By subjectCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[3]");
        By activityCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[4]");
        By statusCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[5]");
        By startDueDateCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[6]");
        By actualEndCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[7]");
        By caseNoteCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[8]");
        By regardingTypeCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[9]");
        By responsibleUserCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[10]");
        By responsibleTeamCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[11]");
        By modifiedByCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[12]");
        By modifiedOnCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[13]");
        By createdByCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[14]");
        By createdOnCell(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[15]");




        #region List View Mode

        By EditRecordButton_ListViewMode(string RecordID) => By.XPath("//button[@title='View/Edit Record'][contains(@onclick, '" + RecordID + "')]");
        By ExpandRecordButton_ListViewMode(string RecordID) => By.XPath("//button[@title='Show/Hide Info'][contains(@onclick, '" + RecordID + "')]");
        By RecordLineMainCard_ListViewMode(string RecordID, int LineNumber) => By.XPath("//button[@title='View/Edit Record'][contains(@onclick, '" + RecordID + "')]/parent::div/parent::div/ul[1]/li[" + LineNumber + "]");
        By RecordLineSubCard_ListViewMode(string RecordID, int LineNumber) => By.XPath("//button[@title='View/Edit Record'][contains(@onclick, '" + RecordID + "')]/parent::div/parent::div/ul[2]/li[" + LineNumber + "]");

        #endregion



        #endregion







        public PersonAllActivitiesSubPage WaitForPersonAllActivitiesSubPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWHTMLResourcePanel_IFrame);
            SwitchToIframe(CWHTMLResourcePanel_IFrame);

            WaitForElement(pageTitle);
            WaitForElement(printAllButton);
            WaitForElement(printSelectedButton);


            return this;
        }


        public PersonAllActivitiesSubPage SelectActivityRecord(string RecordID)
        {
            WaitForElement(activityRecordCheckBox(RecordID));
            Click(activityRecordCheckBox(RecordID));

            return this;
        }




        public PersonAllActivitiesSubPage InsertKeyword(string TextToInsert)
        {
            SendKeys(keyword_Field, TextToInsert);

            return this;
        }
        public PersonAllActivitiesSubPage SelectActivityType(string TextToSelect)
        {
            SelectPicklistElementByText(ActivityType_Picklist, TextToSelect);

            return this;
        }
        public PersonAllActivitiesSubPage SelectDateType(string TextToSelect)
        {
            SelectPicklistElementByText(FromDateType_Picklist, TextToSelect);

            return this;
        }
        public PersonAllActivitiesSubPage InsertFromDate(string TextToInsert)
        {
            SendKeys(FromDate_Field, TextToInsert);

            return this;
        }
        public PersonAllActivitiesSubPage InsertToDate(string TextToInsert)
        {
            SendKeys(ToDate_Field, TextToInsert);

            return this;
        }
        public PersonAllActivitiesSubPage InsertActualEndDateFrom(string TextToInsert)
        {
            SendKeys(ActualEndFromDate_Field, TextToInsert);

            return this;
        }
        public PersonAllActivitiesSubPage InsertActualEndDateTo(string TextToInsert)
        {
            SendKeys(ActualEndToDate_Field, TextToInsert);

            return this;
        }
        public PersonAllActivitiesSubPage TapCategoryLookupButton()
        {
            Click(activitycategory_LookupButton);

            return this;
        }
        public PersonAllActivitiesSubPage TapSubCategoryLookupButton()
        {
            Click(activitysubcategory_LookupButton);

            return this;
        }
        public PersonAllActivitiesSubPage TapResponsibleUserLookupButton()
        {
            Click(systemuser_LookupButton);

            return this;
        }
        public PersonAllActivitiesSubPage TapResponsibleTeamLookupButton()
        {
            Click(team_LookupButton);

            return this;
        }
        public PersonAllActivitiesSubPage SelectStatusByText(string TextToSelect)
        {
            SelectPicklistElementByText(StatusId_Picklist, TextToSelect);

            return this;
        }
        public PersonAllActivitiesSubPage TapCaseNoteOnlyRadioButton()
        {
            Click(CaseNotesOnly_CheckBox);

            return this;
        }
        public PersonAllActivitiesSubPage TapRelatedPersonLookupbutton()
        {
            Click(person_LookupButton);

            return this;
        }
        public PersonAllActivitiesSubPage TapReasonLookupbutton()
        {
            Click(activityreason_LookupButton);

            return this;
        }





        public PersonAllActivitiesSubPage TapSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }
        public PersonAllActivitiesSubPage TapClearFiltersButton()
        {
            Click(clearFiltersButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }
        public PersonAllActivitiesSubPage TapSelectAllActivitiesCheckBox()
        {
            WaitForElement(selectAllActivitiesCheckBox);
            Click(selectAllActivitiesCheckBox);

            return this;
        }
        public PersonAllActivitiesSubPage TapPrintAllButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(printAllButton);
            Click(printAllButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }
        public PersonAllActivitiesSubPage TapPrintSelectedButton()
        {
            Click(printSelectedButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }
        public PersonAllActivitiesSubPage TapChangeViewButton()
        {
            WaitForElementToBeClickable(ChangeViewButton);
            Click(ChangeViewButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public PersonAllActivitiesSubPage TapExpandCollapseAllButton_ListViewMode()
        {
            Click(ExpandCollapseAllButton);

            return this;
        }
        public PersonAllActivitiesSubPage TapEditRecordButton_ListViewMode(string RecordId)
        {
            Click(EditRecordButton_ListViewMode(RecordId));

            return this;
        }
        public PersonAllActivitiesSubPage TapExpandRecordButton_ListViewMode(string RecordId)
        {
            Click(ExpandRecordButton_ListViewMode(RecordId));

            return this;
        }




        public PersonAllActivitiesSubPage ValidateExpandCollapseAllButtonVisibility_ListViewMode(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(ExpandCollapseAllButton);
            else
                WaitForElementNotVisible(ExpandCollapseAllButton, 7);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateEditRecordButtonVisibility_ListViewMode(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EditRecordButton_ListViewMode(RecordId));
            else
                WaitForElementNotVisible(EditRecordButton_ListViewMode(RecordId), 7);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateExpandRecordButtonVisibility_ListViewMode(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ExpandRecordButton_ListViewMode(RecordId));
            else
                WaitForElementNotVisible(ExpandRecordButton_ListViewMode(RecordId), 7);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateRecordLineMainCardVisibility_ListViewMode(string RecordId, int LineNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(RecordLineMainCard_ListViewMode(RecordId, LineNumber));
            else
                WaitForElementNotVisible(RecordLineMainCard_ListViewMode(RecordId, LineNumber), 7);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateRecordLineSubCardVisibility_ListViewMode(string RecordId, int LineNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(RecordLineSubCard_ListViewMode(RecordId, LineNumber));
            else
                WaitForElementNotVisible(RecordLineSubCard_ListViewMode(RecordId, LineNumber), 7);

            return this;
        }





        public PersonAllActivitiesSubPage OpenActivityRecord(string recordID)
        {
            WaitForElement(subjectCell(recordID));
            Click(subjectCell(recordID));

            return this;
        }




        public PersonAllActivitiesSubPage ValidateRecordPresent(string recordID, string recordSubject)
        {
            WaitForElement(activityRecordSubjectCell(recordID, recordSubject));

            return this;
        }
        public PersonAllActivitiesSubPage ValidateRecordPresent(string recordID)
        {
            WaitForElement(activityRecordCheckBox(recordID));

            return this;
        }
        public PersonAllActivitiesSubPage ValidateRecordNotPresent(string recordID)
        {
            WaitForElementNotVisible(activityRecordCheckBox(recordID), 3);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateNoRecordsMessageVisible()
        {
            WaitForElementVisible(NoResultsMessage);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateNoSearchPerformedMessageVisible()
        {
            WaitForElementVisible(NoSearchPerformedMessage);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateNoRecordsMessageNotVisible()
        {
            WaitForElementNotVisible(NoResultsMessage, 4);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateNoSearchPerformedMessageNotVisible()
        {
            WaitForElementNotVisible(NoSearchPerformedMessage, 4);

            return this;
        }





        public PersonAllActivitiesSubPage ValidateRegardingHeaderText(string ExpectedText)
        {
            WaitForElement(regardingTypeHeader);
            MoveToElementInPage(regardingHeader);
            ValidateElementText(regardingHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateSubjectHeaderText(string ExpectedText)
        {
            WaitForElement(subjectHeader);
            MoveToElementInPage(subjectHeader);
            ValidateElementText(subjectHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateActivityHeaderText(string ExpectedText)
        {
            WaitForElement(activityHeader);
            MoveToElementInPage(activityHeader);
            ValidateElementText(activityHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateStatusHeaderText(string ExpectedText)
        {
            WaitForElement(statusHeader);
            MoveToElementInPage(statusHeader);
            ValidateElementText(statusHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateDueDateHeaderText(string ExpectedText)
        {
            WaitForElement(startDueDateHeader);
            MoveToElementInPage(startDueDateHeader);
            ValidateElementText(startDueDateHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateActualEndHeaderText(string ExpectedText)
        {
            WaitForElement(actualEndHeader);
            MoveToElementInPage(actualEndHeader);
            ValidateElementText(actualEndHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateCaseNoteHeaderText(string ExpectedText)
        {
            WaitForElement(caseNoteHeader);
            MoveToElementInPage(caseNoteHeader);
            ValidateElementText(caseNoteHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateRegardingTypeHeaderText(string ExpectedText)
        {
            WaitForElement(regardingTypeHeader);
            MoveToElementInPage(regardingTypeHeader);
            ValidateElementText(regardingTypeHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateResponsibleUserHeaderText(string ExpectedText)
        {
            WaitForElement(responsibleUserHeader);
            MoveToElementInPage(responsibleUserHeader);
            ValidateElementText(responsibleUserHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateResponsibleTeamHeaderText(string ExpectedText)
        {
            ValidateElementText(responsibleTeamHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateModifiedByHeaderText(string ExpectedText)
        {
            ValidateElementText(modifiedByHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateModifiedOnHeaderText(string ExpectedText)
        {
            ValidateElementText(modifiedOnHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateCreatedByHeaderText(string ExpectedText)
        {
            ValidateElementText(createdByHeader, ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateCreatedOnHeaderText(string ExpectedText)
        {
            ValidateElementText(createdOnHeader, ExpectedText);

            return this;
        }



        public PersonAllActivitiesSubPage ValidateRegardingCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(regardingCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateSubjectCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(subjectCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateActivityCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(activityCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateStatusCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(statusCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateDueDateCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(startDueDateCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateActualEndCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(actualEndCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateCaseNoteCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(caseNoteCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateRegardingTypeCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(regardingTypeCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateResponsibleUserCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(responsibleUserCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateResponsibleTeamCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(responsibleTeamCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateModifiedByCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(modifiedByCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateModifiedOnCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(modifiedOnCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateCreatedByCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(createdByCell(recordID), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateCreatedOnCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(createdOnCell(recordID), ExpectedText);

            return this;
        }



        public PersonAllActivitiesSubPage ValidateRecordLineMainCardText_ListViewMode(string RecordId, int LineNumber, string ExpectedText)
        {
            MoveToElementInPage(RecordLineMainCard_ListViewMode(RecordId, LineNumber));
            ValidateElementText(RecordLineMainCard_ListViewMode(RecordId, LineNumber), ExpectedText);

            return this;
        }
        public PersonAllActivitiesSubPage ValidateRecordLineSubCardText_ListViewMode(string RecordId, int LineNumber, string ExpectedText)
        {
            MoveToElementInPage(RecordLineSubCard_ListViewMode(RecordId, LineNumber));
            ValidateElementText(RecordLineSubCard_ListViewMode(RecordId, LineNumber), ExpectedText);

            return this;
        }


    }
}
