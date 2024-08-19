using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserEmploymentContractsRecordPage : CommonMethods
    {
        public SystemUserEmploymentContractsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuseremploymentcontract&')]");
        readonly By staffReviewRequirementsPopupIFrame = By.Id("iframe_StaffReviewRequirementsPopup");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By save_Button = By.Id("TI_SaveButton");
        readonly By saveAndClose_Button = By.Id("TI_SaveAndCloseButton");
        readonly By assign_Button = By.Id("TI_AssignRecordButton");
        readonly By savePopup_Button = By.Id("CWSave");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back'][@onclick='CW.DataForm.Close(); return false;']");
        readonly By delete_Button = By.Id("TI_DeleteRecordButton");
        readonly By suspend_Button = By.Id("TI_SuspendSingleContract");
        readonly By toolbarMenu_Button = By.XPath("//*[@id = 'CWToolbarMenu']/button");

        readonly By ok_Button = By.Id("CWOKButton");

        readonly By menu_Button = By.Id("CWNavGroup_Menu");
        readonly By audit_Button = By.Id("CWNavItem_AuditHistory");

        By RecordRowCheckBox(string FormName) => By.XPath("//table[@id='CWGrid']/tbody/tr/td[@title='" + FormName + "']");


        #region Fields

        readonly By systemUser_Field = By.Id("CWField_systemuserid");
        readonly By systemUser_LinkField = By.Id("CWField_systemuserid_Link");
        readonly By systemUser_Mandatoryfield = By.XPath("//*[@id='CWLabelHolder_systemuserid']/label/span[@class ='mandatory']");
        readonly By systemUser_LookUpButtonDisabled = By.XPath("//*[@id='CWLookupBtn_systemuserid'][@disabled='disabled']");
        readonly By systemUser_LookUpButton = By.Id("CWLookupBtn_systemuserid");

        readonly By name_Field = By.Id("CWField_name");
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By startTime_Field = By.Id("CWField_startdate_Time");
        readonly By endDate_Field = By.Id("CWField_enddate");
        readonly By endTime_Field = By.Id("CWField_enddate_Time");
        readonly By contracthoursperweek_Field = By.Id("CWField_contracthoursperweek");
        readonly By fixedworkingpatterncycle_Field = By.Id("CWField_fixedworkingpatterncycle");
        readonly By contractHoursPerWeek_MandatoryField = By.XPath("//*[@id='CWLabelHolder_contracthoursperweek']/label/span[@class ='mandatory']");
        readonly By signdate_Field = By.Id("CWField_signdate");
        readonly By maximumhoursperweek_Field = By.Id("CWField_maximumhoursperweek");
        readonly By status_Field = By.Id("CWField_statusid");
        readonly By ContractEndReason_LoopUpButton = By.Id("CWLookupBtn_contractendreasonid");
        readonly By ContractEndReason_LinkField = By.Id("CWField_contractendreasonid_Link");

        readonly By RecruitmentRoleApplication_LinkField = By.Id("CWField_recruitmentroleapplicantid_Link");
        readonly By RecruitmentRoleApplication_LookupButton = By.Id("CWLookupBtn_recruitmentroleapplicantid");

        readonly By roleid_LookupButton = By.Id("CWLookupBtn_careproviderstaffroletypeid");
        readonly By roleid_Field = By.Id("CWField_careproviderstaffroletypeid");
        readonly By roleid_LinkField = By.Id("CWField_careproviderstaffroletypeid_Link");
        readonly By responsibleTeam_LookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By responsibleTeam_LinkField = By.Id("CWField_ownerid_Link");
        readonly By workAtLookUpButton = By.Id("CWLookupBtn_workat");
        By workAt_RecordLinkField(string RecordId) => By.XPath("//*[@id='MS_workat_" + RecordId + "']");
        By workAt_RecordRemoveButton(string RecordId) => By.XPath("//*[@id='MS_workat_" + RecordId + "']/a[text()='Remove']");
        readonly By workAtNotAvailableAsteriskField = By.XPath("//*[@id='CWControlHolder_workat']/span[text()='****']");
        readonly By bookingTypesLookUpButton = By.Id("CWLookupBtn_bookingtypeid");
        readonly By payrollId = By.Id("CWField_payroll");

        readonly By typeid_Lookup = By.Id("CWLookupBtn_employmentcontracttypeid");
        readonly By typeid_Field = By.Id("CWField_employmentcontracttypeid");
        readonly By typeid_LinkField = By.Id("CWField_employmentcontracttypeid_Link");
        readonly By description_Field = By.Id("CWField_description");


        readonly By yearlyentitlement_Field = By.Id("CWField_yearlyentitlement");
        readonly By accruedannualleave_Field = By.Id("CWField_accruedannualleave");
        readonly By entitlementunitid_Field = By.Id("CWField_entitlementunitid");

        readonly By YearlyEntitlemenWeeks_Field_NottificationMessage = By.XPath("//*[@id='CWControlHolder_yearlyentitlement']/label/span");

        readonly By Suspension_StartDateTime = By.Id("CWField_ActiveSuspensionStartDateTime");
        readonly By Suspension_EndDateTime = By.Id("CWField_ActiveSuspensionEndDateTime");

        readonly By EntitledToAnnualLeaveAccrual_YesRadioButton = By.Id("CWField_isentitledtoannualleaveaccrual_1");
        readonly By EntitledToAnnualLeaveAccrual_NoRadioButton = By.Id("CWField_isentitledtoannualleaveaccrual_0");


        #region Labels

        By MandatoryField_Label(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']/span[@class = 'mandatory']");
        readonly By ContractHoursPerWeekErrorLabel = By.XPath("//*[@for = 'CWField_contracthoursperweek'][@class = 'formerror']/span");


        By AvailableBookingTypes_LinkRecordField(string linkRecordId) => By.Id("" + linkRecordId + "_Link");

        By AvailableBookingTypes_RemoveRecordIcon(string linkRecordId) => By.XPath("//*[@id='MS_bookingtypeid_" + linkRecordId + "']/a[text()='Remove']");

        #endregion

        #endregion

        public SystemUserEmploymentContractsRecordPage WaitForSystemUserEmploymentContractsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage WaitForGeneralSectionToLoad(bool ContractEndReasonVisible = false)
        {
            WaitForElementVisible(systemUser_LookUpButton);
            WaitForElementVisible(roleid_LookupButton);
            WaitForElementVisible(typeid_Lookup);
            WaitForElementVisible(startDate_Field);
            WaitForElementVisible(startTime_Field);
            WaitForElementVisible(description_Field);
            if (ContractEndReasonVisible)
                WaitForElementVisible(ContractEndReason_LoopUpButton);
            else
                WaitForElementNotVisible(ContractEndReason_LoopUpButton, 3);



            WaitForElementVisible(responsibleTeam_LookUpButton);
            WaitForElementVisible(workAtLookUpButton);
            WaitForElementVisible(status_Field);
            WaitForElementVisible(signdate_Field);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage WaitForSchedulingSectionToLoad()
        {
            WaitForElementVisible(contracthoursperweek_Field);
            WaitForElementVisible(bookingTypesLookUpButton);

            WaitForElementVisible(payrollId);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage WaitForAnnualLeaveSectionToLoad(bool EntitledToAnnualLeaveAccrual = true)
        {
            WaitForElementVisible(EntitledToAnnualLeaveAccrual_YesRadioButton);
            WaitForElementVisible(EntitledToAnnualLeaveAccrual_NoRadioButton);

            if (EntitledToAnnualLeaveAccrual)
            {
                WaitForElementVisible(yearlyentitlement_Field);
                WaitForElementVisible(accruedannualleave_Field);
                WaitForElementVisible(entitlementunitid_Field);
            }

            return this;
        }

        public SystemUserEmploymentContractsRecordPage WaitForStaffReviewRequrimentsLookUP()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(staffReviewRequirementsPopupIFrame);
            SwitchToIframe(staffReviewRequirementsPopupIFrame);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateSystemUserEmploymentContractsRecordPageTitle(string PageTitle)
        {
            ValidateElementText(pageHeader, PageTitle);

            return this;
        }


        public SystemUserEmploymentContractsRecordPage ValidateSystemUser_LinkField(string ExpectedText)
        {
            WaitForElement(systemUser_LinkField);
            ValidateElementTextContainsText(systemUser_LinkField, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateRole_LinkField(string ExpectedText)
        {
            WaitForElement(roleid_LinkField);
            ValidateElementTextContainsText(roleid_LinkField, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            if (ExpectedText != "")
                WaitForElementVisible(responsibleTeam_LinkField);

            ValidateElementTextContainsText(responsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateType_LinkField(string ExpectedText)
        {
            WaitForElement(typeid_LinkField);
            ScrollToElement(typeid_LinkField);
            ValidateElementTextContainsText(typeid_LinkField, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateContractEndReason_LinkField(string ExpectedText)
        {
            WaitForElement(ContractEndReason_LinkField);
            ValidateElementTextContainsText(ContractEndReason_LinkField, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickSystemUserLoopUpButton()
        {
            WaitForElement(systemUser_LookUpButton);
            Click(systemUser_LookUpButton);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickRoleLoopUpButton()
        {
            WaitForElement(roleid_LookupButton);
            Click(roleid_LookupButton);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateRoleLoopUpButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(roleid_LookupButton);
            else
                ValidateElementDisabled(roleid_LookupButton);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickContractEndReasonLoopUpButton()
        {
            WaitForElementToBeClickable(ContractEndReason_LoopUpButton);
            Click(ContractEndReason_LoopUpButton);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateContractEndReasonLoopUpButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(ContractEndReason_LoopUpButton);
            else
                ValidateElementDisabled(ContractEndReason_LoopUpButton);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickCanAlsoWorkAtLoopUpButton()
        {
            WaitForElement(workAtLookUpButton);
            Click(workAtLookUpButton);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateCanAlsoWorkAtLoopUpButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(workAtLookUpButton);
            else
                WaitForElementNotVisible(workAtLookUpButton, 3);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateCanAlsoWorkAtRecordVisible(Guid RecordID, bool ExpectVisible)
        {
            return ValidateCanAlsoWorkAtRecordVisible(RecordID.ToString(), ExpectVisible);
        }

        public SystemUserEmploymentContractsRecordPage ValidateCanAlsoWorkAtRecordVisible(string RecordID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(workAt_RecordLinkField(RecordID));
                WaitForElementVisible(workAt_RecordRemoveButton(RecordID));

            }
            else
            {
                WaitForElementNotVisible(workAt_RecordLinkField(RecordID), 3);
                WaitForElementNotVisible(workAt_RecordRemoveButton(RecordID), 3);
            }

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateCanAlsoWorkAtAsterisksFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(workAtNotAvailableAsteriskField);
            else
                WaitForElementNotVisible(workAtNotAvailableAsteriskField, 3);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickTypeLookUpButton()
        {
            WaitForElement(typeid_Lookup);
            Click(typeid_Lookup);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickCanAlsoWorkAtRecordRemoveButton(string RecordID)
        {
            WaitForElement(workAt_RecordRemoveButton(RecordID));
            Click(workAt_RecordRemoveButton(RecordID));

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickCanAlsoWorkAtRecordRemoveButton(Guid RecordID)
        {
            return ClickCanAlsoWorkAtRecordRemoveButton(RecordID.ToString());
        }

        public SystemUserEmploymentContractsRecordPage ClickBookingTypesLookUpButton()
        {
            WaitForElement(bookingTypesLookUpButton);
            Click(bookingTypesLookUpButton);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateName_FieldText(string ExpectedText)
        {
            WaitForElement(name_Field);
            ValidateElementValue(name_Field, ExpectedText);

            return this;
        }


        public SystemUserEmploymentContractsRecordPage ValidateStartDateFieldEnabled(bool ExpectedEnabled)
        {
            WaitForElement(status_Field);
            if (ExpectedEnabled)
            {
                ValidateElementEnabled(startDate_Field);
            }
            else
            {
                ValidateElementDisabled(startDate_Field);
            }

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateStartDate_FieldText(string ExpectedText)
        {
            WaitForElement(startDate_Field);
            ValidateElementValue(startDate_Field, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateStartTime_FieldText(string ExpectedText)
        {
            WaitForElement(startTime_Field);
            ScrollToElement(startTime_Field);
            ValidateElementValue(startTime_Field, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateEndDate_FieldText(string ExpectedText)
        {
            WaitForElement(endDate_Field);
            ValidateElementValue(endDate_Field, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateEndDateFieldEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                ValidateElementNotDisabled(endDate_Field);
                ValidateElementNotDisabled(endTime_Field);
            }
            else
            {
                ValidateElementDisabled(endDate_Field);
                ValidateElementDisabled(endTime_Field);
            }

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateEndTime_FieldText(string ExpectedText)
        {
            WaitForElement(endTime_Field);
            ValidateElementValue(endTime_Field, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateContractHoursPerWeek_FieldText(string ExpectedText)
        {
            WaitForElement(contracthoursperweek_Field);
            ScrollToElement(contracthoursperweek_Field);
            ValidateElementValueByJavascript("CWField_contracthoursperweek", ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateContactHoursPerWeek_IsMandatoryField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(contractHoursPerWeek_MandatoryField);
            else
                WaitForElementNotVisible(contractHoursPerWeek_MandatoryField, 3);

            return this;
        }


        public SystemUserEmploymentContractsRecordPage ValidateContractSignDate_FieldText(string ExpectedText)
        {
            WaitForElement(signdate_Field);
            ValidateElementValue(signdate_Field, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateMaximumHoursPerWeek_FieldText(string ExpectedText)
        {
            WaitForElement(maximumhoursperweek_Field);
            ValidateElementValue(maximumhoursperweek_Field, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateStatusFieldEnabled(bool ExpectedEnabled)
        {
            WaitForElement(status_Field);
            if (ExpectedEnabled)
            {
                ValidateElementEnabled(status_Field);
            }
            else
            {
                ValidateElementDisabled(status_Field);
            }

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateStatusFieldSelectedText(string ExpectedText)
        {
            WaitForElement(status_Field);
            ValidatePicklistSelectedText(status_Field, ExpectedText);

            return this;
        }


        public SystemUserEmploymentContractsRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(save_Button);
            Click(save_Button);
            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndClose_Button);
            Click(saveAndClose_Button);
            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assign_Button);
            Click(assign_Button);
            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickSavePopUpButton()
        {
            WaitForElementToBeClickable(savePopup_Button);
            Click(savePopup_Button);
            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(back_Button);
            Click(back_Button);
            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickDeleteButton()
        {
            ClickToolbarMenuButton();

            WaitForElementToBeClickable(delete_Button);
            Click(delete_Button);
            return this;
        }

        //method to click toolbar menu and then click delete button
        public SystemUserEmploymentContractsRecordPage ClickToolbarMenuButton()
        {
            WaitForElementToBeClickable(toolbarMenu_Button);
            ScrollToElement(toolbarMenu_Button);

            if (GetElementByAttributeValue(toolbarMenu_Button, "aria-expanded").Equals("false"))
                Click(toolbarMenu_Button);

            return this;
        }


        public SystemUserEmploymentContractsRecordPage ClickSuspendButton()
        {
            WaitForElementToBeClickable(suspend_Button);
            Click(suspend_Button);
            return this;
        }

        public SystemUserEmploymentContractsRecordPage InsertName(string TextToInsert)
        {
            WaitForElement(name_Field);
            SendKeys(name_Field, TextToInsert);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage InsertStartDate(string StartDate, string StartTime)
        {
            WaitForElement(startDate_Field);
            SendKeys(startDate_Field, StartDate + Keys.Tab);
            SendKeys(startTime_Field, StartTime + Keys.Tab);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElement(endDate_Field);
            SendKeys(endDate_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage InsertEndTime(string TextToInsert)
        {
            WaitForElement(endTime_Field);
            SendKeys(endTime_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage InsertContractSignedDate(string TextToInsert)
        {
            WaitForElement(signdate_Field);
            SendKeys(signdate_Field, TextToInsert);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage InsertContractHoursPerWeek(string TextToInsert)
        {
            WaitForElement(contracthoursperweek_Field);
            ScrollToElement(contracthoursperweek_Field);
            SendKeys(contracthoursperweek_Field, TextToInsert);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage InsertFixedWorkingPatternCycle(string TextToInsert)
        {
            WaitForElement(fixedworkingpatterncycle_Field);
            SendKeys(fixedworkingpatterncycle_Field, TextToInsert);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage InsertMaximumHoursPerWeek(string TextToInsert)
        {
            WaitForElement(maximumhoursperweek_Field);
            SendKeys(maximumhoursperweek_Field, TextToInsert);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage InsertPayrollId(string TextToInsert)
        {
            WaitForElement(payrollId);
            SendKeys(payrollId, TextToInsert);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage StaffReviewRequrimentSelection(string FormName)
        {
            //ScrollToElementViaJavascript(RecordRowCheckBox(FormName));
            WaitForElement(RecordRowCheckBox(FormName));
            this.Click(RecordRowCheckBox(FormName));

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickAuditButton()
        {
            WaitForElement(menu_Button);
            Click(menu_Button);
            WaitForElementToBeClickable(audit_Button);
            Click(audit_Button);

            return this;
        }


        public SystemUserEmploymentContractsRecordPage ClickResponsibleTeamLookUpButton()
        {
            WaitForElement(responsibleTeam_LookUpButton);
            Click(responsibleTeam_LookUpButton);



            return this;
        }

        public SystemUserEmploymentContractsRecordPage InsertDescription(string TextToInsert)
        {
            WaitForElement(description_Field);
            SendKeys(description_Field, TextToInsert);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateDescriptionText(string TextToInsert)
        {
            WaitForElement(description_Field);
            ScrollToElement(description_Field);
            ValidateElementText(description_Field, TextToInsert);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickOkButton()
        {
            WaitForElementToBeClickable(ok_Button);
            Click(ok_Button);
            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateRecruitmentRoleApplicationLinkFieldText(string ExpectedText)
        {
            WaitForElementVisible(RecruitmentRoleApplication_LinkField);
            ValidateElementTextContainsText(RecruitmentRoleApplication_LinkField, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickRecruitmentRoleApplicationLinkButton()
        {
            WaitForElementToBeClickable(RecruitmentRoleApplication_LinkField);
            ScrollToElement(RecruitmentRoleApplication_LinkField);
            Click(RecruitmentRoleApplication_LinkField);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateYearlyEntitlement_FieldText(string ExpectedText)
        {
            WaitForElementVisible(yearlyentitlement_Field);
            ScrollToElement(yearlyentitlement_Field);
            ValidateElementValue(yearlyentitlement_Field, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage InsertYearlyEntitlementWeek(string TextToInsert)
        {
            WaitForElementVisible(yearlyentitlement_Field);
            ScrollToElement(yearlyentitlement_Field);
            SendKeys(yearlyentitlement_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateEntitlementUnitFieldSelectedText(string ExpectedText)
        {
            WaitForElementVisible(entitlementunitid_Field);
            ScrollToElement(entitlementunitid_Field);
            ValidatePicklistSelectedText(entitlementunitid_Field, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateEntitlementUnitPicklist_FieldOptionIsPresent(string Text)
        {
            WaitForElementVisible(entitlementunitid_Field);
            ScrollToElement(entitlementunitid_Field);
            ValidatePicklistContainsElementByText(entitlementunitid_Field, Text);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateMaximumLimitOfDescriptionField(string expected)
        {
            WaitForElementVisible(description_Field);
            ScrollToElement(description_Field);
            ValidateElementMaxLength(description_Field, expected);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateMaximumLimitOfPayrollIDField(string expected)
        {
            WaitForElementVisible(payrollId);
            ScrollToElement(payrollId);
            ValidateElementMaxLength(payrollId, expected);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateYearlyEntitlementWeeksFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(YearlyEntitlemenWeeks_Field_NottificationMessage);
            ScrollToElement(YearlyEntitlemenWeeks_Field_NottificationMessage);
            ValidateElementText(YearlyEntitlemenWeeks_Field_NottificationMessage, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateMandatoryFieldIsDisplayed(string FieldName, bool ExpectedDisplayed = true)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 3);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateAvailableBookingTypesLinkFieldText(string linkRecordId, string ExpectedText)
        {
            WaitForElementVisible(AvailableBookingTypes_LinkRecordField(linkRecordId));
            ScrollToElement(AvailableBookingTypes_LinkRecordField(linkRecordId));
            ValidateElementTextContainsText(AvailableBookingTypes_LinkRecordField(linkRecordId), ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateContractHoursPerWeekErrorLabelVisible(string ExpectedString)
        {
            WaitForElementVisible(ContractHoursPerWeekErrorLabel);
            ValidateElementByTitle(ContractHoursPerWeekErrorLabel, ExpectedString);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickAvailableBookingTypes_RemoveRecordIcon(string RecordID)
        {
            WaitForElementToBeClickable(AvailableBookingTypes_RemoveRecordIcon(RecordID));
            Click(AvailableBookingTypes_RemoveRecordIcon(RecordID));

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateSuspensionStartDateText(string ExpectedValue)
        {
            WaitForElementVisible(Suspension_StartDateTime);
            ValidateElementValue(Suspension_StartDateTime, ExpectedValue);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ValidateSuspensionEndDateText(string ExpectedValue)
        {
            WaitForElementVisible(Suspension_EndDateTime);
            ValidateElementValue(Suspension_EndDateTime, ExpectedValue);

            return this;
        }



        public SystemUserEmploymentContractsRecordPage ClickEntitledToAnnualLeaveAccrual_YesRadioButton()
        {
            WaitForElement(EntitledToAnnualLeaveAccrual_YesRadioButton);
            Click(EntitledToAnnualLeaveAccrual_YesRadioButton);

            return this;
        }

        public SystemUserEmploymentContractsRecordPage ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
        {
            WaitForElement(EntitledToAnnualLeaveAccrual_NoRadioButton);
            Click(EntitledToAnnualLeaveAccrual_NoRadioButton);

            return this;
        }

    }
}

