using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AdvanceSearchPage : CommonMethods
    {
        public AdvanceSearchPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Advanced Search']");

        readonly By searchButton = By.Id("TI_ShowQueryResultsButton");
        readonly By chooseColumnsButton = By.Id("TI_ChooseColumnsButton");

        readonly By recordTypePicklist = By.Id("initialBOSelector");
        readonly By savedViewsPicklist = By.Id("initialSavedQueriesSelector");
        readonly By deleteButton = By.XPath("//*[@class='rule-container']/div[1]/div/button");
        readonly By advancedSearchButton = By.XPath("//*[@id='navbar']/ul[2]/li[1]/a");
        readonly By requestReceivedDateSortButton = By.XPath("//*[@id='CWGridHeaderRow']/th[7]/a/span[2]");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        readonly By auditHistory = By.Id("CWNavItem_AuditHistory");

        By conditionBuilderTitle(string CardText) => By.XPath("//*[@class='card']/div[@class='card-header']/span[@class='conditionBuilderTitle'][text()='" + CardText + "']");

        By filterPicklist(string SelectPosition) => By.XPath("//*[@class='rule-container'][" + SelectPosition + "]/div[@class='rule-filter-container']/select");
        By filterOption(string SelectPosition, string OptionText) => By.XPath("//*/div/div[" + SelectPosition + "]/div/select/optgroup[@label='Fields']/option[text()='" + OptionText + "']");
        By operatorPicklist(string SelectPosition) => By.XPath("//*[@class='rule-container'][" + SelectPosition + "]/div[@class='rule-operator-container']/select");
        By ruleValueLookupButton(string SelectPosition) => By.XPath("//div[@class='rule-container'][" + SelectPosition + "]/div[@class='rule-value-container']/div/div/button");
        By ruleValueTextField(string SelectPosition) => By.XPath("//div[@class='rule-container'][" + SelectPosition + "]/div[@class='rule-value-container']/div/input");
        By ruleValuePicklistField(string SelectPosition) => By.XPath("//div[@class='rule-container'][" + SelectPosition + "]/div[@class='rule-value-container']/div/select");

        By operatorYesNoPicklist(string SelectPosition) => By.XPath("//*[@class='rule-value-container'][" + SelectPosition + "]/div[@class='input-group']/select");
        By ruleValueTypePicklist(string SelectPosition) => By.XPath("//*[@class='rule-value-type-container'][" + SelectPosition + "]/select");
        readonly By RelatedRecordsPicklist = By.XPath("//select[@class='relatedBOSelector form-control']");
        readonly By RelatedRecordsAddButton = By.XPath("//select[@class='relatedBOSelector form-control']/parent::div/div/button");


        By AddRuleButton(int SelectPosition) => By.XPath("//div[" + SelectPosition + "]/div[@class='rules-group-header']/div[@class='btn-group float-right group-actions']/button[@data-add='rule']");

        readonly By newRecordButton_ResultsPage = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton_ResultsPage = By.Id("TI_ExportToExcelButton");
        readonly By backButton_ResultsPage = By.Id("BackButton");
        readonly By ToolbarMenu_ResultsPage = By.Id("CWToolbarMenu");
        readonly By DeleteRecordButton_ResultsPage = By.Id("TI_DeleteRecordButton");
        readonly By EditButton_ResultsPage = By.Id("TI_EditButton");

        By resultsPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//span[@class='th-text']");
        By resultsPageHeaderElementSortOrded(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2]");

        By recordCheckbox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");
        By recordCell(string recordID, int CellPosition) => By.XPath("//*[@id='" + recordID + "']/td[" + CellPosition + "]");

        By recordRow(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");

        readonly By selectfield = By.XPath("//div[@class='rules-group-body']//div[@class='rule-filter-container']/select");

        By selectfieldlocation(string postion) => By.XPath("//select[contains(@name,'_rule_" + postion + "_filter')]");

        readonly By SelectFieldValue = By.XPath("//div[@class='rules-group-body']//div[@class='rule-value-container']/div[@class='input-group']/input");

        #region Choose Columns Popup

        readonly By ChooseColumnsPopup_Title = By.XPath("//*[@id='chooseColumns']/div/div/div/h5");
        readonly By ChooseColumnsPopup_ParentRecordTypePicklist = By.XPath("//*[@id='CWParentRelationships']");
        readonly By ChooseColumnsPopup_FieldPicklist = By.XPath("//*[@id='columnsParentBO']");
        readonly By ChooseColumnsPopup_AddButton = By.XPath("//*[@id='btnAddParentColumn']");
        readonly By ChooseColumnsPopup_OKButton = By.XPath("//*[@id='btnChooseColumnsOK']");

        By ChooseColumnsPopup_FieldWithInput(string FieldID) => By.XPath("//*[@id='cw" + FieldID + "']");
        By ChooseColumnsPopup_FieldWithInput(string RelationshipID, string FieldID) => By.XPath("//*[@id='cw" + RelationshipID + "_" + FieldID + "']");

        By ChooseColumnsPopup_Field(string FieldID) => By.XPath("//*[@id='" + FieldID + "']/following-sibling::label");

        #endregion


        public AdvanceSearchPage WaitForAdvanceSearchPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 40);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 40);

            WaitForElement(pageHeader);
            WaitForElement(searchButton);
            WaitForElement(chooseColumnsButton);
            WaitForElement(recordTypePicklist);
            WaitForElement(savedViewsPicklist);
            WaitForElement(deleteButton);

            return this;
        }

        public AdvanceSearchPage WaitForResultsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDataFormDialog);
            SwitchToIframe(iframe_CWDataFormDialog);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(exportToExcelButton_ResultsPage);

            return this;
        }

        public AdvanceSearchPage WaitForResultsPageToLoadwithNoExportToExcel()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDataFormDialog);
            SwitchToIframe(iframe_CWDataFormDialog);

            WaitForElementNotVisible("CWRefreshPanel", 100);


            return this;
        }

        public AdvanceSearchPage ValidateConditionBuilderCardTitle(string ExpectedText)
        {
            WaitForElementVisible(conditionBuilderTitle(ExpectedText));

            return this;
        }

        public AdvanceSearchPage ClickSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            Click(searchButton);

            return this;
        }

        public AdvanceSearchPage ClickAdvancedSearchButton()
        {
            Click(advancedSearchButton);

            return this;
        }

        public AdvanceSearchPage ClickRequestReceivedDateSortButton()
        {
            Click(requestReceivedDateSortButton);

            return this;
        }

        public AdvanceSearchPage ClickChooseColumnsButton()
        {
            WaitForElementToBeClickable(chooseColumnsButton);
            Click(chooseColumnsButton);

            return this;
        }

        public AdvanceSearchPage ClickDeleteButton()
        {
            Click(deleteButton);

            return this;
        }

        public AdvanceSearchPage SelectRecordType(string TextToSelect)
        {
            System.Threading.Thread.Sleep(3000);

            SelectPicklistElementByText(recordTypePicklist, TextToSelect);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(filterPicklist("1"));

            return this;
        }

        public AdvanceSearchPage SelectSavedView(string TextToSelect)
        {
            System.Threading.Thread.Sleep(3000);

            SelectPicklistElementByText(savedViewsPicklist, TextToSelect);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public AdvanceSearchPage SelectSavedViewWithoutWaitingForRefreshPanel(string TextToSelect)
        {
            SelectPicklistElementByText(savedViewsPicklist, TextToSelect);
            return this;
        }

        public AdvanceSearchPage SelectFilter(string SelectPosition, string TextToSelect)
        {
            WaitForElementVisible(filterPicklist(SelectPosition));
            WaitForElementToBeClickable(filterPicklist(SelectPosition));

            SelectPicklistElementByText(filterPicklist(SelectPosition), TextToSelect);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(operatorPicklist(SelectPosition));

            return this;
        }

        public AdvanceSearchPage ValidateFilterNotAvailable(string SelectPosition, string TextToFind)
        {
            WaitForElementVisible(filterPicklist(SelectPosition));
            WaitForElementToBeClickable(filterPicklist(SelectPosition));

            ValidatePicklistDoesNotContainsElementByText(filterPicklist(SelectPosition), TextToFind);

            return this;
        }

        public AdvanceSearchPage ValidateSelectFilterFieldOptionIsPresent(string SelectPosition, string text)
        {
            WaitForElement(filterPicklist(SelectPosition));
            ValidatePicklistContainsElementByText(filterPicklist(SelectPosition), text);

            return this;
        }

        public AdvanceSearchPage ValidateSelectFilterFieldOptionIsNotPresent(string SelectPosition, string text)
        {
            ValidatePicklistDoesNotContainsElementByText(filterPicklist(SelectPosition), text);

            return this;
        }

        public AdvanceSearchPage SelectFilterInsideOptGroup(string SelectPosition, string TextToSelect)
        {
            WaitForElementVisible(filterPicklist(SelectPosition));
            WaitForElementToBeClickable(filterPicklist(SelectPosition));

            Click(filterPicklist(SelectPosition));
            Click(filterOption(SelectPosition, TextToSelect));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(operatorPicklist(SelectPosition));

            return this;
        }

        public AdvanceSearchPage SelectOperator(string SelectPosition, string TextToSelect)
        {
            SelectPicklistElementByText(operatorPicklist(SelectPosition), TextToSelect);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public AdvanceSearchPage ClickRuleValueLookupButton(string SelectPosition)
        {

            WaitForElementVisible(ruleValueLookupButton(SelectPosition));
            Click(ruleValueLookupButton(SelectPosition));

            return this;
        }

        public AdvanceSearchPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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

        public AdvanceSearchPage InsertRuleValueText(string SelectPosition, string TextToInsert)
        {
            WaitForElementVisible(ruleValueTextField(SelectPosition));
            SendKeys(ruleValueTextField(SelectPosition), TextToInsert);

            return this;
        }

        public AdvanceSearchPage SelectPicklistRuleValue(string SelectPosition, string ValueToSelect)
        {
            WaitForElementVisible(ruleValuePicklistField(SelectPosition));
            SelectPicklistElementByText(ruleValuePicklistField(SelectPosition), ValueToSelect);

            return this;
        }

        public AdvanceSearchPage ClickAddRuleButton(int ContainerPosition)
        {
            WaitForElementToBeClickable(AddRuleButton(ContainerPosition));
            Click(AddRuleButton(ContainerPosition));

            return this;
        }

        public AdvanceSearchPage SelectRelatedRecords(string TextToSelect)
        {
            WaitForElementVisible(RelatedRecordsPicklist);
            WaitForElementToBeClickable(RelatedRecordsPicklist);

            SelectPicklistElementByText(RelatedRecordsPicklist, TextToSelect);

            return this;
        }

        public AdvanceSearchPage ClickRelatedRecordsAddButton()
        {
            WaitForElementToBeClickable(RelatedRecordsAddButton);

            Click(RelatedRecordsAddButton);

            return this;
        }

        public AdvanceSearchPage ClickColumnHeader(int CellPosition)
        {
            WaitForElement(resultsPageHeaderElement(CellPosition));
            ScrollToElement(resultsPageHeaderElement(CellPosition));
            WaitForElementToBeClickable(resultsPageHeaderElement(CellPosition));
            Click(resultsPageHeaderElement(CellPosition));

            return this;
        }

        public AdvanceSearchPage ResultsPageValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(resultsPageHeaderElement(CellPosition));
            WaitForElementVisible(resultsPageHeaderElement(CellPosition));
            ValidateElementText(resultsPageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public AdvanceSearchPage ResultsPageValidateHeaderCellSortOrdedAscending(int CellPosition)
        {
            WaitForElement(resultsPageHeaderElementSortOrded(CellPosition));
            ScrollToElement(resultsPageHeaderElementSortOrded(CellPosition));
            WaitForElementVisible(resultsPageHeaderElementSortOrded(CellPosition));
            ValidateElementAttribute(resultsPageHeaderElementSortOrded(CellPosition), "class", "sortasc");

            return this;
        }

        public AdvanceSearchPage ResultsPageValidateHeaderCellSortOrdedDescending(int CellPosition)
        {
            WaitForElement(resultsPageHeaderElementSortOrded(CellPosition));
            ScrollToElement(resultsPageHeaderElementSortOrded(CellPosition));
            WaitForElementVisible(resultsPageHeaderElementSortOrded(CellPosition));
            ValidateElementAttribute(resultsPageHeaderElementSortOrded(CellPosition), "class", "sortdesc");

            return this;
        }

        public AdvanceSearchPage ValidateSearchResultRecordPresent(string recordID)
        {
            ScrollToElement(recordCheckbox(recordID));
            WaitForElementVisible(recordCheckbox(recordID));

            return this;
        }

        public AdvanceSearchPage ValidateSearchResultRecordCellContent(string recordID, int CellPosition, string ExpectedText)
        {
            ValidateElementText(recordCell(recordID, CellPosition), ExpectedText);

            return this;
        }

        public AdvanceSearchPage ValidateSearchResultRecordNotPresent(string recordID)
        {
            WaitForElementNotVisible(recordCheckbox(recordID), 3);

            return this;
        }

        public AdvanceSearchPage ClickBackButton_ResultsPage()
        {
            WaitForElementVisible(backButton_ResultsPage);
            WaitForElementToBeClickable(backButton_ResultsPage);
            Click(backButton_ResultsPage);

            return this;
        }

        public AdvanceSearchPage ClickNewRecordButton_ResultsPage()
        {
            WaitForElementVisible(newRecordButton_ResultsPage);
            WaitForElementToBeClickable(newRecordButton_ResultsPage);
            Click(newRecordButton_ResultsPage);

            return this;
        }

        public AdvanceSearchPage ClickExportToExcelButton_ResultsPage()
        {
            WaitForElementVisible(exportToExcelButton_ResultsPage);
            WaitForElementToBeClickable(exportToExcelButton_ResultsPage);
            Click(exportToExcelButton_ResultsPage);

            return this;
        }

        public AdvanceSearchPage ClickEditButton_ResultsPage()
        {
            WaitForElementVisible(EditButton_ResultsPage);
            WaitForElementToBeClickable(EditButton_ResultsPage);
            Click(EditButton_ResultsPage);

            return this;
        }

        public AdvanceSearchPage SelectSearchResultRecord(string recordID)
        {
            Click(recordCheckbox(recordID));

            return this;
        }

        public AdvanceSearchPage SelectYesNoOperator(string SelectPosition, string TextToSelect)
        {
            SelectPicklistElementByText(operatorYesNoPicklist(SelectPosition), TextToSelect);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public AdvanceSearchPage SelectValueType(string SelectPosition, string TextToSelect)
        {
            SelectPicklistElementByText(ruleValueTypePicklist(SelectPosition), TextToSelect);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        #region Choose Columns Popup

        public AdvanceSearchPage WaitForChooseColumnsPopupToLoad()
        {
            WaitForElementVisible(ChooseColumnsPopup_Title);
            WaitForElementVisible(ChooseColumnsPopup_ParentRecordTypePicklist);
            WaitForElementVisible(ChooseColumnsPopup_FieldPicklist);
            WaitForElementVisible(ChooseColumnsPopup_AddButton);
            WaitForElementVisible(ChooseColumnsPopup_OKButton);

            return this;
        }

        public AdvanceSearchPage ChooseColumnsPopup_SelectParentRecordType(string TextToSelect)
        {
            SelectPicklistElementByText(ChooseColumnsPopup_ParentRecordTypePicklist, TextToSelect);

            return this;
        }

        public AdvanceSearchPage ChooseColumnsPopup_SelectField(string TextToSelect)
        {
            SelectPicklistElementByText(ChooseColumnsPopup_FieldPicklist, TextToSelect);

            return this;
        }

        public AdvanceSearchPage ChooseColumnsPopup_InsertColumnWith(string FieldID, string TextToInsert)
        {
            SendKeys(ChooseColumnsPopup_FieldWithInput(FieldID), TextToInsert);

            return this;
        }

        public AdvanceSearchPage ChooseColumnsPopup_InsertColumnWith(string RelationshipID, string FieldID, string TextToInsert)
        {
            SendKeys(ChooseColumnsPopup_FieldWithInput(RelationshipID, FieldID), TextToInsert);

            return this;
        }

        public AdvanceSearchPage ChooseColumnsPopup_ValidateColumnWith(string FieldID, string ExpectedValue)
        {
            ValidateElementValue(ChooseColumnsPopup_FieldWithInput(FieldID), ExpectedValue);

            return this;
        }

        public AdvanceSearchPage ChooseColumnsPopup_ValidateColumnWith(string RelationshipID, string FieldID, string ExpectedValue)
        {
            ValidateElementValue(ChooseColumnsPopup_FieldWithInput(RelationshipID, FieldID), ExpectedValue);

            return this;
        }

        public AdvanceSearchPage ChooseColumnsPopup_ValidateColumn(string FieldID, string ExpectedValue)
        {
            MoveToElementInPage(ChooseColumnsPopup_Field(FieldID));
            ValidateElementText(ChooseColumnsPopup_Field(FieldID), ExpectedValue);

            return this;
        }

        public AdvanceSearchPage ChooseColumnsPopup_ClickAddButton()
        {
            WaitForElementToBeClickable(ChooseColumnsPopup_AddButton);
            Click(ChooseColumnsPopup_AddButton);

            return this;
        }

        public AdvanceSearchPage ChooseColumnsPopup_ClickOKButton()
        {
            MoveToElementInPage(ChooseColumnsPopup_OKButton);
            Click(ChooseColumnsPopup_OKButton);

            return this;
        }

        public AdvanceSearchPage SelectFieldOption(string postion, string text)
        {
            WaitForElement(selectfieldlocation(postion));
            SelectPicklistElementByText(selectfieldlocation(postion), text);

            return this;
        }

        public AdvanceSearchPage ValidateSelectFieldOption(string text)
        {
            WaitForElement(selectfield);
            ValidatePicklistContainsElementByText(selectfield, text);

            return this;
        }

        public AdvanceSearchPage InsertFieldOptionValue(string text)
        {
            WaitForElement(selectfield);
            SendKeys(SelectFieldValue, text);

            return this;
        }

        public AdvanceSearchPage ValidateSelectFieldOptionNotPresent(string text)
        {
            WaitForElement(selectfield);
            ValidatePicklistDoesNotContainsElementByText(selectfield, text);

            return this;
        }

        public AdvanceSearchPage OpenRecord(string RecordID)
        {
            System.Threading.Thread.Sleep(500);

            this.WaitForElementToBeClickable(recordRow(RecordID));
            MoveToElementInPage(recordRow(RecordID));
            this.Click(recordRow(RecordID));

            System.Threading.Thread.Sleep(500);

            return this;
        }

        public AdvanceSearchPage ClickSelectFilterFieldOption(string SelectPosition)
        {
            WaitForElementToBeClickable(filterPicklist(SelectPosition));
            Click(filterPicklist(SelectPosition));

            return this;
        }

        public AdvanceSearchPage ClickDeleteRecordButtonFromResultPage()
        {
            WaitForElementVisible(ToolbarMenu_ResultsPage);
            MoveToElementInPage(ToolbarMenu_ResultsPage);
            Click(ToolbarMenu_ResultsPage);

            WaitForElementVisible(DeleteRecordButton_ResultsPage);
            MoveToElementInPage(DeleteRecordButton_ResultsPage);
            Click(DeleteRecordButton_ResultsPage);

            return this;
        }

        public AdvanceSearchPage ValidateNewRecordButton_ResultsPageIsNotPresent(bool ExpectedNotVisible)
        {
            if (ExpectedNotVisible)
            {
                WaitForElementNotVisible(newRecordButton_ResultsPage, 5);
            }
            else
            {
                WaitForElementVisible(newRecordButton_ResultsPage);
            }

            return this;
        }

        #endregion

    }
}
