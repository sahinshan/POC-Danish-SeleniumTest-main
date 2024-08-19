using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.CareDirector
{
    public class FinanceExtractBatches
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public int batchid { get; set; }
        public ExtractContent extractcontent { get; set; }
    }

    public class ExtractContent
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string size { get; set; }
        public string uri { get; set; }
    }

    public class AdvanceFinancialsExtractBatchData
    {
        public string hasMoreRecords { get; set; }

        public List<FinanceExtractBatches> data { get; set; }
    }
}
