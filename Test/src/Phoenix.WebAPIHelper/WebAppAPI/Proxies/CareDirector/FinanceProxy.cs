using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using Phoenix.WebAPIHelper.WebAppAPI.Classes;

namespace Phoenix.WebAPIHelper.WebAppAPI.Proxies
{
    public class FinanceProxy : IFinance
    {
        public FinanceProxy()
        {
            _finance = new Finance();
        }

        private IFinance _finance;

        public void ReadyToAuthoriseInvoice(Guid InvoiceID, string AuthenticationCookie)
        {
            _finance.ReadyToAuthoriseInvoice(InvoiceID, AuthenticationCookie);
        }

        public void AuthoriseInvoice(Guid InvoiceID, string AuthenticationCookie)
        {
            _finance.AuthoriseInvoice(InvoiceID, AuthenticationCookie);
        }
    }
}
