using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class AdvanceFinancialsProxy : IAdvanceFinancials
    {
        public AdvanceFinancialsProxy()
        {
            _advanceFinancials = new AdvanceFinancials();
        }


        private IAdvanceFinancials _advanceFinancials;


        public string access_token { get; set; }



        public string Authenticate()
        {
            this.access_token = _advanceFinancials.Authenticate();

            return this.access_token;
        }

        public void HealthCheck(string Access_token)
        {
            _advanceFinancials.HealthCheck(access_token);
        }

        public void HealthCheck()
        {
            _advanceFinancials.HealthCheck(access_token);
        }

        public Entities.CareDirector.AdvanceFinancialsExtractBatchData PendingFinanceExtracts(string Access_token)
        {
            return _advanceFinancials.PendingFinanceExtracts(Access_token);
        }

        public Entities.CareDirector.AdvanceFinancialsExtractBatchData PendingFinanceExtracts()
        {
            return _advanceFinancials.PendingFinanceExtracts(access_token);
        }

        public string GetSignedURL(string Access_token, string FinanceExtractBatchContentID)
        {
            return _advanceFinancials.GetSignedURL(Access_token, FinanceExtractBatchContentID);
        }

        public string GetSignedURL(string FinanceExtractBatchContentID)
        {
            return _advanceFinancials.GetSignedURL(access_token, FinanceExtractBatchContentID);
        }

        public string DownloadFileFromS3(string Access_token, string DownloadURL)
        {
            return _advanceFinancials.DownloadFileFromS3(Access_token, DownloadURL);
        }

        public string DownloadFileFromS3(string DownloadURL)
        {
            return _advanceFinancials.DownloadFileFromS3(access_token, DownloadURL);
        }

        public void SetExtractAsDownloaded(string Access_token, string FinanceExtractBatchID)
        {
            _advanceFinancials.SetExtractAsDownloaded(Access_token, FinanceExtractBatchID);
        }

        public void SetExtractAsDownloaded(string FinanceExtractBatchID)
        {
            _advanceFinancials.SetExtractAsDownloaded(access_token, FinanceExtractBatchID);
        }
    }
}
