using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.CareDirector
{
    public class AuditResponse
    {
        public string SortOptions { get; set; }
        public string DataQuery { get; set; }
        public string Columns { get; set; }
        public int ResultCount { get; set; }
        public bool HasMoreRecords { get; set; }
        public List<GridDatum> GridData { get; set; }
        public List<DataGridHeader> DataGridHeader { get; set; }
        public bool Success { get; set; }
        public bool UnauthorisedAccess { get; set; }
    }

    
    public class CD
    {
        public string Text { get; set; }
        public bool IPF { get; set; }
    }

    public class DataGridHeader
    {
        public int FT { get; set; }
        public bool IS { get; set; }
        public string BOFN { get; set; }
        public string FLDN { get; set; }
        public string HT { get; set; }
        public int SO { get; set; }
        public string SC { get; set; }
        public string TA { get; set; }
        public int Width { get; set; }
    }

    public class GridDatum
    {
        public bool RIE { get; set; }
        public bool IRR { get; set; }
        public string Id { get; set; }
        public List<CD> cols { get; set; }
    }

    


}
