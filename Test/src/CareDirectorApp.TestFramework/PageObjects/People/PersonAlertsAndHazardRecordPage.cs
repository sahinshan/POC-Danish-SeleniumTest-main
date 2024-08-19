using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CareDirectorApp.TestFramework.PageObjects
{
    public class PersonAlertsAndHazardRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("personalertandhazard_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("personalertandhazard_TextToSpeechStopButton");
        readonly Func<AppQuery, AppQuery> _deleteButton = e => e.Marked("personalertandhazard_DeleteRecordButton");

        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("personalertandhazard_SaveButton");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("personalertandhazard_SaveAndCloseButton");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");



        #region Fields titles

        readonly Func<AppQuery, AppQuery> _Person_FieldTitle = e => e.Marked("Person");
        readonly Func<AppQuery, AppQuery> _Role_FieldTitle = e => e.Marked("Role");
        readonly Func<AppQuery, AppQuery> _AlertHazardType_FieldTitle = e => e.Marked("Alert/Hazard Type");
        readonly Func<AppQuery, AppQuery> _StartDate_FieldTitle = e => e.Marked("Start Date");
        readonly Func<AppQuery, AppQuery> _EndDate_FieldTitle = e => e.Marked("End Date");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _ReviewFrequency_FieldTitle = e => e.Marked("Review Frequency");
        readonly Func<AppQuery, AppQuery> _AlertHazardEndReason_FieldTitle = e => e.Marked("Alert/Hazard End Reason");

        readonly Func<AppQuery, AppQuery> _Details_FieldTitle = e => e.Marked("Details");

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _Person_Field = e => e.Marked("Field_82d56e29650fea11a2c90050569231cf");

        readonly Func<AppQuery, AppQuery> _Role_Field = e => e.Marked("Field_be6787c8731ae91180dc0050560502cc");

        readonly Func<AppQuery, AppQuery> _AlertHazardType_Field = e => e.Marked("8b7b02c2731ae91180dc0050560502cc_LookupEntry");
        readonly Func<AppQuery, AppQuery> _AlertHazardType_RemoveButton = e => e.Marked("8b7b02c2731ae91180dc0050560502cc_RemoveValue");
        readonly Func<AppQuery, AppQuery> _AlertHazardType_LookupButton = e => e.Marked("8b7b02c2731ae91180dc0050560502cc_OpenLookup");

        readonly Func<AppQuery, AppQuery> _StartDate_DateField = e => e.Marked("Field_270fbdd4731ae91180dc0050560502cc_Date");
        readonly Func<AppQuery, AppQuery> _StartDate_DatePicker = e => e.Marked("Field_270fbdd4731ae91180dc0050560502cc_OpenPicker");

        readonly Func<AppQuery, AppQuery> _EndDate_DateField = e => e.All().Marked("Field_c9885b64650fea11a2c90050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _EndDate_DatePicker = e => e.All().Marked("Field_c9885b64650fea11a2c90050569231cf_OpenPicker");

        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_1889ebbb650fea11a2c90050569231cf");

        readonly Func<AppQuery, AppQuery> _ReviewFrequency_Field = e => e.Marked("Field_7a9674922f81e911a2c50050569231cf");

        readonly Func<AppQuery, AppQuery> _AlertHazardEndReason_Field = e => e.Marked("7da6dca9650fea11a2c90050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _AlertHazardEndReason_RemoveButton = e => e.Marked("7da6dca9650fea11a2c90050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _AlertHazardEndReason_LookupButton = e => e.Marked("7da6dca9650fea11a2c90050569231cf_OpenLookup");

        readonly Func<AppQuery, AppQuery> _Details_Field = e => e.Marked("Field_1550e3e96f87e911a2c50050569231cf");

        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion



        public PersonAlertsAndHazardRecordPage(IApp app)
        {
            _app = app;
        }


        public PersonAlertsAndHazardRecordPage WaitForPersonAlertsAndHazardRecordPageToLoad(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            WaitForElement(_pageTitle(PageTitleText));

            WaitForElement(_topBannerArea);
            
            return this;
        }



        public PersonAlertsAndHazardRecordPage ValidatePersonFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Person_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Person_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Person_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Person_FieldTitle));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateRoleFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Role_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Role_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Role_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Role_FieldTitle));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateAlertHazardTypeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_AlertHazardType_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_AlertHazardType_FieldTitle));
            }
            else
            {
                TryScrollToElement(_AlertHazardType_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_AlertHazardType_FieldTitle));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateStartDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_StartDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_StartDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_StartDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_StartDate_FieldTitle));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateEndDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_EndDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_EndDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_EndDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_EndDate_FieldTitle));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ResponsibleTeam_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ResponsibleTeam_FieldTitle));
            }
            else
            {
                TryScrollToElement(_ResponsibleTeam_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleTeam_FieldTitle));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateReviewFrequencyFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ReviewFrequency_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ReviewFrequency_FieldTitle));
            }
            else
            {
                TryScrollToElement(_ReviewFrequency_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ReviewFrequency_FieldTitle));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateAlertHazardEndReasonFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_AlertHazardEndReason_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_AlertHazardEndReason_FieldTitle));
            }
            else
            {
                TryScrollToElement(_AlertHazardEndReason_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_AlertHazardEndReason_FieldTitle));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateDetailsFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Details_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Details_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Details_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Details_FieldTitle));
            }

            return this;
        }

        


        public PersonAlertsAndHazardRecordPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_createdBy_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_createdBy_FieldTitle));
            }
            else
            {
                TryScrollToElement(_createdBy_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_createdBy_FieldTitle));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_createdOn_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_createdOn_FieldTitle));
            }
            else
            {
                TryScrollToElement(_createdOn_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_createdOn_FieldTitle));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_modifiedBy_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_modifiedBy_FieldTitle));
            }
            else
            {
                TryScrollToElement(_modifiedBy_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_modifiedBy_FieldTitle));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_modifiedOn_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_modifiedOn_FieldTitle));
            }
            else
            {
                TryScrollToElement(_modifiedOn_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_modifiedOn_FieldTitle));
            }

            return this;
        }






        public PersonAlertsAndHazardRecordPage ValidatePersonFieldText(string ExpectText)
        {
            ScrollToElement(_Person_Field);
            string fieldText = GetElementText(_Person_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateRoleFieldText(string ExpectText)
        {
            ScrollToElement(_Role_Field);
            string fieldText = GetElementText(_Role_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateAlertHazardTypeFieldText(string ExpectText)
        {
            ScrollToElement(_AlertHazardType_Field);
            string fieldText = GetElementText(_AlertHazardType_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateStartDateFieldText(string ExpectDateText)
        {
            ScrollToElement(_StartDate_DateField);

            string fieldText = GetElementText(_StartDate_DateField);
            Assert.AreEqual(ExpectDateText, fieldText);

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateEndDateFieldText(string ExpectText)
        {
            ScrollToElement(_EndDate_DateField);
            string fieldText = GetElementText(_EndDate_DateField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateResponsibleTeamFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            string fieldText = GetElementText(_ResponsibleTeam_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateReviewFrequencyFieldText(string ExpectText)
        {
            ScrollToElement(_ReviewFrequency_Field);
            string fieldText = GetElementText(_ReviewFrequency_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateAlertHazardEndDetailsFieldText(string ExpectText)
        {
            ScrollToElement(_AlertHazardEndReason_Field);
            string fieldText = GetElementText(_AlertHazardEndReason_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateDetailsFieldText(string ExpectText)
        {
            ScrollToElement(_Details_Field);
            string fieldText = GetElementText(_Details_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public PersonAlertsAndHazardRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }





        public PersonAlertsAndHazardRecordPage TapRoleField()
        {
            ScrollToElement(_Role_Field);
            this.Tap(_Role_Field);

            return this;
        }

        public PersonAlertsAndHazardRecordPage TapAlertHazardTypeRemoveButton()
        {
            ScrollToElement(_AlertHazardType_RemoveButton);
            Tap(_AlertHazardType_RemoveButton);

            return this;
        }

        public PersonAlertsAndHazardRecordPage TapAlertHazardTypeLookupButton()
        {
            ScrollToElement(_AlertHazardType_LookupButton);
            Tap(_AlertHazardType_LookupButton);

            return this;
        }

        public PersonAlertsAndHazardRecordPage InsertStartDate(string DateValue)
        {
            ScrollToElement(_StartDate_DateField);
            this.EnterText(_StartDate_DateField, DateValue);

            return this;
        }

        public PersonAlertsAndHazardRecordPage InsertEndDate(string DateValue)
        {
            ScrollToElement(_EndDate_DateField);
            this.EnterText(_EndDate_DateField, DateValue);

            return this;
        }

        public PersonAlertsAndHazardRecordPage TapReviewFrequencyField()
        {
            ScrollToElement(_ReviewFrequency_Field);
            Tap(_ReviewFrequency_Field);

            return this;
        }

        public PersonAlertsAndHazardRecordPage TapAlertHazardEndReasonRemoveButton()
        {
            ScrollToElement(_AlertHazardEndReason_RemoveButton);
            Tap(_AlertHazardEndReason_RemoveButton);

            return this;
        }

        public PersonAlertsAndHazardRecordPage TapAlertHazardEndReasonLookupButton()
        {
            ScrollToElement(_AlertHazardEndReason_LookupButton);
            Tap(_AlertHazardEndReason_LookupButton);

            return this;
        }

        public PersonAlertsAndHazardRecordPage InsertDatails(string ValueToInsert)
        {
            ScrollToElement(_Details_Field);
            this.EnterText(_Details_Field, ValueToInsert);

            return this;
        }






        public PersonAlertsAndHazardRecordPage ValidateAlertHazardTypeRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_AlertHazardType_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_AlertHazardType_RemoveButton));
            }
            else
            {
                TryScrollToElement(_AlertHazardType_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_AlertHazardType_RemoveButton));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateAlertHazardEndReasonRemoveButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_AlertHazardEndReason_RemoveButton);
                Assert.IsTrue(CheckIfElementVisible(_AlertHazardEndReason_RemoveButton));
            }
            else
            {
                TryScrollToElement(_AlertHazardEndReason_RemoveButton);
                Assert.IsFalse(CheckIfElementVisible(_AlertHazardEndReason_RemoveButton));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateAlertHazardTypeLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_AlertHazardType_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_AlertHazardType_LookupButton));
            }
            else
            {
                TryScrollToElement(_AlertHazardType_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_AlertHazardType_LookupButton));
            }

            return this;
        }

        public PersonAlertsAndHazardRecordPage ValidateAlertHazardEndReasonLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_AlertHazardEndReason_LookupButton);
                Assert.IsTrue(CheckIfElementVisible(_AlertHazardEndReason_LookupButton));
            }
            else
            {
                TryScrollToElement(_AlertHazardEndReason_LookupButton);
                Assert.IsFalse(CheckIfElementVisible(_AlertHazardEndReason_LookupButton));
            }

            return this;
        }


        





        public PersonAlertsAndHazardRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }

        public PersonAlertsAndHazardRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }

        public PersonAlertsAndHazardRecordPage TapOnDeleteButton()
        {
            Tap(_deleteButton);

            return this;
        }


        public PersonAlertsAndHazardRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }

        public PersonAlertsAndHazardRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }

        public PersonAlertsAndHazardRecordPage WaitForDeleteButtonNotVisible()
        {
            WaitForElementNotVisible(_deleteButton);

            return this;
        }
    }
}
