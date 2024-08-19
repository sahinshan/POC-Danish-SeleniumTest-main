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
    public class CaseInvolvementRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("caseinvolvement_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("caseinvolvement_TextToSpeechStopButton");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");



        #region Fields titles

        readonly Func<AppQuery, AppQuery> _Case_FieldTitle = e => e.Marked("Case");
        readonly Func<AppQuery, AppQuery> _InvolvementMember_FieldTitle = e => e.Marked("Involvement Member");
        readonly Func<AppQuery, AppQuery> _Role_FieldTitle = e => e.Marked("Role");
        readonly Func<AppQuery, AppQuery> _StartDate_FieldTitle = e => e.Marked("Start Date");
        readonly Func<AppQuery, AppQuery> _InvolvementReason_FieldTitle = e => e.Marked("Involvement Reason");

        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _InvolvementPriority_FieldTitle = e => e.Marked("Involvement Priority");
        readonly Func<AppQuery, AppQuery> _InvolvementStatus_FieldTitle = e => e.Marked("Involvement Status");
        readonly Func<AppQuery, AppQuery> _EndDate_FieldTitle = e => e.Marked("End Date");
        readonly Func<AppQuery, AppQuery> _InvolvementEndReason_FieldTitle = e => e.Marked("Involvement End Reason");
        
        readonly Func<AppQuery, AppQuery> _Description_FieldTitle = e => e.Marked("Description");

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _Case_Field = e => e.Marked("Field_a4a18049a019e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _InvolvementMember_Field = e => e.Marked("Field_0340e214a119e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _Role_Field = e => e.Marked("Field_ac4cf475a019e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _StartDate_Field = e => e.Marked("Field_f57f068fa019e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _InvolvementReason_Field = e => e.Marked("Field_cdad839ba019e91180dc0050560502cc");

        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_179f46caa019e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _InvolvementPriority_Field = e => e.Marked("Field_8da88383a019e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _InvolvementStatus_Field = e => e.Marked("Field_c87ac3b9a019e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _EndDate_Field = e => e.Marked("Field_1bac67a5a019e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _InvolvementEndReason_Field = e => e.Marked("Field_2eba42b1a019e91180dc0050560502cc");
        
        readonly Func<AppQuery, AppQuery> _Description_Field = e => e.Marked("Field_a09d2ad4a019e91180dc0050560502cc");


        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion



        public CaseInvolvementRecordPage(IApp app)
        {
            _app = app;
        }


        public CaseInvolvementRecordPage WaitForCaseInvolvementRecordPageToLoad(string PageTitleText)
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



        public CaseInvolvementRecordPage ValidateCaseFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Case_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Case_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Case_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Case_FieldTitle));
            }

            return this;
        }

        public CaseInvolvementRecordPage ValidateInvolvementMemberFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_InvolvementMember_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_InvolvementMember_FieldTitle));
            }
            else
            {
                TryScrollToElement(_InvolvementMember_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_InvolvementMember_FieldTitle));
            }

            return this;
        }

        public CaseInvolvementRecordPage ValidateRoleFieldTitleVisible(bool ExpectFieldVisible)
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

        public CaseInvolvementRecordPage ValidateStartDateFieldTitleVisible(bool ExpectFieldVisible)
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

        public CaseInvolvementRecordPage ValidateInvolvementReasonFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_InvolvementReason_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_InvolvementReason_FieldTitle));
            }
            else
            {
                TryScrollToElement(_InvolvementReason_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_InvolvementReason_FieldTitle));
            }

            return this;
        }




        public CaseInvolvementRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectFieldVisible)
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

        public CaseInvolvementRecordPage ValidateInvolvementPriorityFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_InvolvementPriority_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_InvolvementPriority_FieldTitle));
            }
            else
            {
                TryScrollToElement(_InvolvementPriority_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_InvolvementPriority_FieldTitle));
            }

            return this;
        }

        public CaseInvolvementRecordPage ValidateInvolvementStatusFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_InvolvementStatus_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_InvolvementStatus_FieldTitle));
            }
            else
            {
                TryScrollToElement(_InvolvementStatus_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_InvolvementStatus_FieldTitle));
            }

            return this;
        }

        public CaseInvolvementRecordPage ValidateEndDateFieldTitleVisible(bool ExpectFieldVisible)
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

        public CaseInvolvementRecordPage ValidateInvolvementEndReasonFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_InvolvementEndReason_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_InvolvementEndReason_FieldTitle));
            }
            else
            {
                TryScrollToElement(_InvolvementEndReason_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_InvolvementEndReason_FieldTitle));
            }

            return this;
        }

        
        
        public CaseInvolvementRecordPage ValidateDescriptionFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Description_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Description_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Description_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Description_FieldTitle));
            }

            return this;
        }




        public CaseInvolvementRecordPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
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

        public CaseInvolvementRecordPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
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

        public CaseInvolvementRecordPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
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

        public CaseInvolvementRecordPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
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






        public CaseInvolvementRecordPage ValidateCaseFieldText(string ExpectText)
        {
            ScrollToElement(_Case_Field);
            string fieldText = GetElementText(_Case_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseInvolvementRecordPage ValidateInvolvementMemberFieldText(string ExpectText)
        {
            ScrollToElement(_InvolvementMember_Field);
            string fieldText = GetElementText(_InvolvementMember_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseInvolvementRecordPage ValidateRoleFieldText(string ExpectText)
        {
            ScrollToElement(_Role_Field);
            string fieldText = GetElementText(_Role_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseInvolvementRecordPage ValidateStartDateFieldText(string ExpectText)
        {
            ScrollToElement(_StartDate_Field);
            string fieldText = GetElementText(_StartDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseInvolvementRecordPage ValidateinvolvementReasonFieldText(string ExpectText)
        {
            ScrollToElement(_InvolvementReason_Field);
            string fieldText = GetElementText(_InvolvementReason_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        
        public CaseInvolvementRecordPage ValidateResponsibleTeamFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            string fieldText = GetElementText(_ResponsibleTeam_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseInvolvementRecordPage ValidateInvolvementPriorityFieldText(string ExpectText)
        {
            ScrollToElement(_InvolvementPriority_Field);
            string fieldText = GetElementText(_InvolvementPriority_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseInvolvementRecordPage ValidateInvolvementStatusFieldText(string ExpectText)
        {
            ScrollToElement(_InvolvementStatus_Field);
            string fieldText = GetElementText(_InvolvementStatus_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseInvolvementRecordPage ValidateEndDateFieldText(string ExpectText)
        {
            ScrollToElement(_EndDate_Field);
            string fieldText = GetElementText(_EndDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseInvolvementRecordPage ValidateInvolvementEndReasonFieldText(string ExpectText)
        {
            ScrollToElement(_InvolvementEndReason_Field);
            string fieldText = GetElementText(_InvolvementEndReason_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public CaseInvolvementRecordPage ValidateDescriptionFieldText(string ExpectText)
        {
            ScrollToElement(_Description_Field);
            string fieldText = GetElementText(_Description_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }




        public CaseInvolvementRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseInvolvementRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseInvolvementRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public CaseInvolvementRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }














    }
}
