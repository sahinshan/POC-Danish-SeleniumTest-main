using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO.Compression;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace Phoenix.UITests.Framework.PDFTools
{
    public class PdfHelper
    {
        public bool FindText(string TextToFind, string FilePath)
        {
            using (PdfDocument document = PdfDocument.Open(FilePath))
            {
                foreach (Page page in document.GetPages())
                {
                    string pageText = page.Text;

                    if (pageText.Contains(TextToFind))
                        return true;
                }
            }

            return false;
        }

    }
}
