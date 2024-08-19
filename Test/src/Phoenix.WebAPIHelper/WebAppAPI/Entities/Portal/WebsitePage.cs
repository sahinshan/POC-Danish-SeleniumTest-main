using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.Portal
{
    public class WebsitePage
    {
        public WebsitePage() 
        {

        }

        public string HeaderTitle { get; set; }
        public bool IsSecure { get; set; }
        public bool DisplayHeaderTitle { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public Guid? ParentPageId { get; set; }
        public Guid Id { get; set; }

        public WidgetList Layout { get; set; }


        public List<string> ScriptFiles { get; set; }
        public List<string> StyleSheets { get; set; }
        public List<string> WidgetScriptFiles { get; set; }
        public List<string> WidgetStyleSheets { get; set; }
    }
}
