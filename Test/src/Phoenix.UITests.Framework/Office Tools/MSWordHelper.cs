using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO.Compression;
using Microsoft.Office.Interop.Word;

namespace Phoenix.UITests.Framework.OfficeTools
{
    public class MSWordHelper
    {

        public int CountWord(string WordToFind, string FilePath)
        {
            var wordCounter = 0;

            Application wordApp = new Application { Visible = false };

            Document aDoc = wordApp.Documents.Open(FilePath, ReadOnly: true, Visible: false);
            try
            {
                aDoc.Activate();

                // Loop through all words in the document.
                for (var i = 1; i <= aDoc.Words.Count; i++)
                    if (aDoc.Words[i].Text.TrimEnd() == WordToFind)
                        wordCounter++;
            }
            catch
            {
                aDoc.Close();
            }


            aDoc.Close();

            return wordCounter;
        }


        public bool FindWord(string TextToFind, string FilePath)
        {
            object findText = TextToFind;
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object wrap = 1;
            object format = false;
            object replaceWithText = "";
            object replace = 0;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;

            Application wordApp = new Application { Visible = false };
            Document aDoc = wordApp.Documents.Open(FilePath, ReadOnly: true, Visible: false);
            bool resultFound = false;
            try
            {
                aDoc.Activate();

                //execute find
                resultFound = wordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards,
                    ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                    ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
            }
            catch
            {
                aDoc.Close();
            }

            aDoc.Close();

            return resultFound;
        }

        public int CountWordsInDocument(string FilePath, string wordToFind)
        {
            var wordCounter = 0;
            Application wordApp = new Application { Visible = false };
            Document aDoc = wordApp.Documents.Open(FilePath, ReadOnly: true, Visible: false);

            try
            {
                aDoc.Activate();

                // Loop through all words in the document.
                for (var i = 1; i <= aDoc.Words.Count; i++)
                    if (aDoc.Words[i].Text.TrimEnd() == wordToFind)
                        wordCounter++;
            }
            catch
            {
                aDoc.Close();
                throw;
            }

            aDoc.Close();

            return wordCounter;
        }

        public void ValidateWordsPresent(string FilePath, params string[] WordsToFind)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object wrap = 1;
            object format = false;
            object replaceWithText = "";
            object replace = 0;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;

            Application wordApp = new Application { Visible = false };
            Document aDoc = wordApp.Documents.OpenNoRepairDialog(FilePath, ReadOnly: true, Visible: false);
            aDoc.Activate();

            foreach (string wordToFind in WordsToFind)
            {
                object findText = wordToFind;
                bool resultFound = false;

                try
                {
                    //execute find
                    resultFound = wordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards,
                        ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                        ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);

                    if (!resultFound)
                    {
                        var headerAndFooterText = GetAllHeaderAndFooterTextFromDocument(aDoc);
                        resultFound = FindWordInListOfText(headerAndFooterText, wordToFind);
                    }
                }
                catch
                {
                    aDoc.Close();
                }

                if (!resultFound)
                {
                    aDoc.Close();
                    throw new Exception("The Word " + findText + " was not found in the document");
                }
            }

            aDoc.Close();

        }

        internal bool FindWordInListOfText(List<string> listOfText, string WordToFind)
        {
            foreach (var text in listOfText)   
                if (text.Contains(WordToFind))
                    return true;
            
            return false;
        }

        internal List<string> GetAllHeaderAndFooterTextFromDocument(Document aDoc)
        {
            List<string> headersAndFooters = new List<string>();

            foreach (Section aSection in aDoc.Sections)
            {
                foreach (HeaderFooter aHeader in aSection.Headers)
                    headersAndFooters.Add(aHeader.Range.Text);

                foreach (HeaderFooter aFooter in aSection.Footers)
                    headersAndFooters.Add(aFooter.Range.Text);
            }

            return headersAndFooters;
        }

        public void ValidateWordsNotPresent(string FilePath, params string[] WordsToSearch)
        {

            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object wrap = 1;
            object format = false;
            object replaceWithText = "";
            object replace = 0;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;

            Application wordApp = new Application { Visible = false };
            Document aDoc = wordApp.Documents.Open(FilePath, ReadOnly: true, Visible: false);
            aDoc.Activate();

            foreach (string wordToSearch in WordsToSearch)
            {
                object findText = wordToSearch;
                bool resultFound = false;

                try
                {
                    resultFound = wordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace, ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
                }
                catch
                {
                    aDoc.Close();
                }

                if (resultFound)
                {
                    aDoc.Close();
                    throw new Exception("The Word " + findText + " should not be present in the document but it was found.");
                }
            }

            aDoc.Close();

        }
    }
}
