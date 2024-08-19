using Phoenix.UITests.Framework.WebAppAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    public interface IAdvanceFinancials
    {
        string Authenticate();

        void HealthCheck(string Access_token);

        Entities.CareDirector.AdvanceFinancialsExtractBatchData PendingFinanceExtracts(string Access_token);

        string GetSignedURL(string Access_token, string FinanceExtractBatchContentID);

        string DownloadFileFromS3(string Access_token, string DownloadURL);

        void SetExtractAsDownloaded(string Access_token, string FinanceExtractBatchID);

    }
}
