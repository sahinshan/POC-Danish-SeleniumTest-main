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

    public class RetrievePortalDataViewResponse
    {
        public string Title { get; set; }
        public bool HasMoreRecords { get; set; }
        public bool HasCreatePrivilege { get; set; }
        public string BusinessObjectName { get; set; }
        public bool IsSecure { get; set; }
        public List<Record> Records { get; set; }
    }

    public class DataViewField
    {
        public string Label { get; set; }
        public string Text { get; set; }
    }

    public class Record
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<DataViewField> Fields { get; set; }
    }

    

}
