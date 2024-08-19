using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    interface IFinance
    {
        void ReadyToAuthoriseInvoice(Guid InvoiceID, string AuthenticationCookie);

        void AuthoriseInvoice(Guid InvoiceID, string AuthenticationCookie);
    }
}
