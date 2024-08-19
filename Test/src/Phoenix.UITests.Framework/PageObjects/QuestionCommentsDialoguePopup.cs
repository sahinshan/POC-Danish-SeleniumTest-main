using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// This class represents the Section information popup.
    /// this popup is displayed when a user open an assessment in edit mode, taps on a section (or sub section) menu button and tap on the "Section Information" link
    /// </summary>
    public class QuestionCommentsDialoguePopup : CommonMethods
    {
        public QuestionCommentsDialoguePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By iframe_CWCommentsDialogWindow = By.Id("iframe_CWCommentsDialogWindow");

        readonly By popupTitle = By.XPath("//*[@id='CWHeader']/h1");

        By notificationMessage(string expectedText) => By.XPath("//*[@id='CWNotificationMessage_RenderComments'][text()='" + expectedText + "']");
        By CommentsMessage(string expectedText) => By.XPath("//*[@id='CWAssessmentCommentInputSection']/div/label/span[text()='" + expectedText + "']");

        By CommentContainer(string CommentID) => By.XPath("//div[@id='CWComment_" + CommentID + "']");

        

        #region Labels

        readonly By AddNewCommentLabel = By.XPath("//*[@id='CWCommentTextLabel'][text()='Add New Comment']");

        #endregion
        
        #region Fields

        readonly By newCommentTextBox = By.XPath("//*[@id='CWCommentText']");

        By CommentLogo(string CommentID) => By.XPath("//*[@id='CWComment_" + CommentID + "']/div/div/div/div/span[@class='attachmenttype icon-systemuser-large']");
        By CommentLine(string CommentID, string CommentText) => By.XPath("//*[@id='CWComment_" + CommentID + "']/div/div/div/div[1]/p[text()='" + CommentText + "']");
        By CommentCreatedOnLine(string CommentID, string CreatedOn, string CreatedBy) => By.XPath("//*[@id='CWComment_" + CommentID + "']/div/div/div/div[2]/p[text()='Created on " + CreatedOn + " by " + CreatedBy + "']");
        By CommentModifiedOnLine(string CommentID, string ModifiedOn, string ModifiedBy) => By.XPath("//*[@id='CWComment_" + CommentID + "']/div/div/div/div[3]/p[text()='Modified on " + ModifiedOn + " by " + ModifiedBy + "']");


        #endregion

        #region Buttons

        readonly By AddCommentButton = By.XPath("//*[@id='CWAssessmentCommentSave']");
        readonly By CancelButton = By.XPath("//*[@id='CWAssessmentCommentCancel']");
        readonly By CloseButton = By.XPath("//*[@id='CWAssessmentCommentClose']");

        By CommentDeleteButton(string CommentID) => By.XPath("//*[@id='CWComment_" + CommentID + "']/div/div/div/div/div/button[contains(@onclick, 'CW.AssessmentComments.DeleteComment')]");
        By CommentEditButton(string CommentID) => By.XPath("//*[@id='CWComment_" + CommentID + "']/div/div/div/div/div/button[contains(@onclick, 'CW.AssessmentComments.EditComment')]");

        

        #endregion




        public QuestionCommentsDialoguePopup WaitForQuestionCommentsDialoguePopupToLoad(string ExpectedPopupTitle)
        {
            WaitForElement(iframe_CWCommentsDialogWindow);
            SwitchToIframe(iframe_CWCommentsDialogWindow);

            WaitForElement(popupTitle);
            ValidateElementText(popupTitle, ExpectedPopupTitle);

            WaitForElement(AddNewCommentLabel);
            WaitForElement(newCommentTextBox);
            WaitForElement(AddCommentButton);
            WaitForElement(CancelButton);
            WaitForElement(CloseButton);
            

            return this;
        }

        public QuestionCommentsDialoguePopup ValidateNotificationMessageVisible(string ExpectedMessage)
        {
            WaitForElementVisible(notificationMessage(ExpectedMessage));
            
            return this;
        }

        public QuestionCommentsDialoguePopup ValidateNotificationMessageNotVisible(string ExpectedMessage)
        {
            WaitForElementNotVisible(notificationMessage(ExpectedMessage), 3);

            return this;
        }

        public QuestionCommentsDialoguePopup ValidateNewCommentMessageVisible(string ExpectedMessage)
        {
            WaitForElementVisible(CommentsMessage(ExpectedMessage));

            return this;
        }

        public QuestionCommentsDialoguePopup ValidateNewCommentMessageNotVisible(string ExpectedMessage)
        {
            WaitForElementNotVisible(CommentsMessage(ExpectedMessage), 3);

            return this;
        }

        public QuestionCommentsDialoguePopup ValidateCommentPresent(string CommentID, string CommentText, string CreatedOn, string CreatedBy, string ModifiedOn, string ModifiedBy)
        {
            WaitForElement(CommentLogo(CommentID));

            WaitForElement(CommentLine(CommentID, CommentText));
            WaitForElement(CommentCreatedOnLine(CommentID, CreatedOn, CreatedBy));
            WaitForElement(CommentModifiedOnLine(CommentID, ModifiedOn, ModifiedBy));

            WaitForElement(CommentDeleteButton(CommentID));
            WaitForElement(CommentEditButton(CommentID));
            
            return this;
        }

        public QuestionCommentsDialoguePopup ValidateCommentNotVisible(string CommentID)
        {
            WaitForElementNotVisible(CommentContainer(CommentID), 3);
            
            return this;
        }




        public QuestionCommentsDialoguePopup InsertComment(string Comment)
        {
            SendKeys(newCommentTextBox, Comment);

            return this;
        }

        public QuestionCommentsDialoguePopup TapAddCommentButton()
        {
            Click(AddCommentButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public QuestionCommentsDialoguePopup TapCancelButton()
        {
            Click(CancelButton);

            return this;
        }

        public QuestionCommentsDialoguePopup TapCloseButton()
        {
            Click(CloseButton);

            return this;
        }

        public AlertPopup TapCommentDeleteButton(string CommentID)
        {
            Click(CommentDeleteButton(CommentID));

            return new AlertPopup(driver, Wait, appURL);
        }

        public QuestionCommentsDialoguePopup TapCommentEditButton(string CommentID)
        {
            Click(CommentEditButton(CommentID));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


    }
}
