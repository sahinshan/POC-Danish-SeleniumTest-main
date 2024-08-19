using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DuplicateRecordMergeDialog : CommonMethods
    {
        public DuplicateRecordMergeDialog(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=duplicaterecord&')]");
        readonly By iframe_CWMergeDialog = By.Id("iframe_CWMergeDialog");

        readonly By popupHeader = By.XPath("//*[@id='CWHeaderText']");

        By recordCheckbox(string recordID) => By.XPath("//*[@id='" + recordID + "']");
        By addressType_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_addresstypeid']");
        By adultsafeguarding_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_adultsafeguardingflag']");
        By age_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_age']");
        By agegroup_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_agegroupid']");
        By allergiesnotrecorded_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_allergiesnotrecorded']");
        By allowemail_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_allowemail']");
        By allowphone_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_allowphone']");
        By allowsms_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_allowsms']");
        By retaininformationconcern_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_retaininformationconcern']");
        By deceased_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_deceased']");
        By dateofbirth_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_dateofbirth']");
        By dobandagetype_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_dobandagetypeid']");
        By ethnicity_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_ethnicityid']");
        By firstname_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_firstname']");
        By interpreterrequired_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_interpreterrequired']");
        By isexternalperson_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_isexternalperson']");
        By islookedafterchild_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_islookedafterchild']");
        By knownallergies_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_knownallergies']");
        By lastname_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_lastname']");
        By nhsnumber_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_nhsnumber']");
        By nhsnumberstatusid_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_nhsnumberstatusid']");
        By noknownallergies_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_noknownallergies']");
        By owningbusinessunitid_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_owningbusinessunitid']");
        By pdsisdeferred_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_pdsisdeferred']");
        By recordedinerror_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_recordedinerror']");
        By relatedadultsafeguardingflag_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_relatedadultsafeguardingflag']");
        By relatedchildprotectionflag_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_relatedchildprotectionflag']");
        By representalertorhazard_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_representalertorhazard']");
        By genderid_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_genderid']");
        By childprotectionflag_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_childprotectionflag']");
        By suppressstatementinvoices_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_suppressstatementinvoices']");
        By updatepersonaddress_Checkbox(string recordID) => By.XPath("//*[@id='" + recordID + "_updatepersonaddress']");


        readonly By MergeButton = By.Id("CWMergeButton");
        readonly By CloseButton = By.Id("CloseButton");


        

        public DuplicateRecordMergeDialog WaitForDuplicateRecordMergeDialogToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            this.WaitForElement(iframe_CWMergeDialog);
            SwitchToIframe(iframe_CWMergeDialog);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            ValidateElementText(popupHeader, "Merge Record Please select the master record");

            return this;
        }


        
        public DuplicateRecordMergeDialog SelectMainRecord(string RecordID)
        {
            this.Click(recordCheckbox(RecordID));

            return this;
        }

        public DuplicateRecordMergeDialog ClickaddressType_Checkbox(string RecordID)
        {
            this.Click(addressType_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickadultsafeguarding_Checkbox(string RecordID)
        {
            this.Click(adultsafeguarding_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickage_Checkbox(string RecordID)
        {
            this.Click(age_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickagegroup_Checkbox(string RecordID)
        {
            this.Click(agegroup_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickallergiesnotrecorded_Checkbox(string RecordID)
        {
            this.Click(allergiesnotrecorded_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickallowemail_Checkbox(string RecordID)
        {
            this.Click(allowemail_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickallowphone_Checkbox(string RecordID)
        {
            this.Click(allowphone_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickallowsms_Checkbox(string RecordID)
        {
            this.Click(allowsms_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickretaininformationconcern_Checkbox(string RecordID)
        {
            this.Click(retaininformationconcern_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickdeceased_Checkbox(string RecordID)
        {
            this.Click(deceased_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickdateofbirth_Checkbox(string RecordID)
        {
            this.Click(dateofbirth_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickdobandagetype_Checkbox(string RecordID)
        {
            this.Click(dobandagetype_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickethnicity_Checkbox(string RecordID)
        {
            this.Click(ethnicity_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickfirstname_Checkbox(string RecordID)
        {
            this.Click(firstname_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickinterpreterrequired_Checkbox(string RecordID)
        {
            this.Click(interpreterrequired_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickisexternalperson_Checkbox(string RecordID)
        {
            this.Click(isexternalperson_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickislookedafterchild_Checkbox(string RecordID)
        {
            this.Click(islookedafterchild_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickknownallergies_Checkbox(string RecordID)
        {
            this.Click(knownallergies_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clicklastname_Checkbox(string RecordID)
        {
            this.Click(lastname_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clicknhsnumber_Checkbox(string RecordID)
        {
            this.Click(nhsnumber_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clicknhsnumberstatusid_Checkbox(string RecordID)
        {
            this.Click(nhsnumberstatusid_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clicknoknownallergies_Checkbox(string RecordID)
        {
            this.Click(noknownallergies_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickowningbusinessunitid_Checkbox(string RecordID)
        {
            this.Click(owningbusinessunitid_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickpdsisdeferred_Checkbox(string RecordID)
        {
            this.Click(pdsisdeferred_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickrecordedinerror_Checkbox(string RecordID)
        {
            this.Click(recordedinerror_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickrelatedadultsafeguardingflag_Checkbox(string RecordID)
        {
            this.Click(relatedadultsafeguardingflag_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickrelatedchildprotectionflag_Checkbox(string RecordID)
        {
            this.Click(relatedchildprotectionflag_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickrepresentalertorhazard_Checkbox(string RecordID)
        {
            this.Click(representalertorhazard_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickgenderid_Checkbox(string RecordID)
        {
            this.Click(genderid_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickchildprotectionflag_Checkbox(string RecordID)
        {
            this.Click(childprotectionflag_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clicksuppressstatementinvoices_Checkbox(string RecordID)
        {
            this.Click(suppressstatementinvoices_Checkbox(RecordID));

            return this;
        }
        public DuplicateRecordMergeDialog Clickupdatepersonaddress_Checkbox(string RecordID)
        {
            this.Click(updatepersonaddress_Checkbox(RecordID));

            return this;
        }


        public DuplicateRecordMergeDialog ClickMergeButton()
        {
            this.Click(MergeButton);

            return this;
        }

        public DuplicateRecordMergeDialog ClickCloseButton()
        {
            this.Click(CloseButton);

            return this;
        }
    }
}
