using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.Portal
{
    public class WidgetSettting
    {
        public WidgetSettting() 
        {

        }

        public string Type { get; set; }
        public Guid? HtmlFileId { get; set; }
        public Guid? ScriptFileId { get; set; }
        public Guid? StylesheetFileId { get; set; }


        public Guid? WebsiteWidgetId { get; set; }
        public Guid? BusinessObjectId { get; set; }
        public Guid? DataFormId { get; set; }
        public Guid? DataViewId { get; set; }

    }
}
