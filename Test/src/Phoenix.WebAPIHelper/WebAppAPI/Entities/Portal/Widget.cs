using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.Portal
{

    public class WidgetList
    {
        public WidgetList()
        {
            Rows = new List<Row>();
        }

        public List<Row> Rows { get; set; }
    }

    public class Row
    {
        public Row()
        {
            Columns = new List<Column>();
        }

        public List<Column> Columns { get; set; }


    }
    public class Column
    {
        public Column()
        {

        }

        public int Type { get; set; }
        public string Title { get; set; }
        public int ColumnSize { get; set; }
        public int Height { get; set; }
        public Guid? BusinessObjectId { get; set; }
        public Guid? DataViewId { get; set; }
        public Guid? DataFormId { get; set; }
        public string HtmlFile { get; set; }
        public string WebsiteWidgetControl { get; set; }
        public bool Visible { get; set; }


    }

}
