using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.Portal
{

    public class RetrievePortalDataViewLookupResponse
    {
        public bool HasMoreRecords { get; set; }
        public List<LookupViewRecord> Records { get; set; }
    }


    public class LookupViewRecord
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }

    

}
