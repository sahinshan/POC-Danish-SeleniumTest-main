using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Portal.UITestsFramework.FileSystem
{
    public class PdfHelper
    {
        public string GetAllTextFromPDF(string DirectoryPath, string FileName)
        {
            var document = new Spire.Pdf.PdfDocument();
            document.LoadFromFile(DirectoryPath + "\\" + FileName);
            var content = new StringBuilder();
            for (int i = 0; i < document.Pages.Count; i++)
                content.Append(document.Pages[i].ExtractText());

            return content.ToString();
        }
    }
}
