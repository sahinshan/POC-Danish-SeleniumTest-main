using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.Portal
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Settings
    {
        public int DisplayMode { get; set; }
        public string CurrencySymbol { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
    }

    public class PicklistValue
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class Field
    {
        public string Name { get; set; }
        public bool ShowLabel { get; set; }
        public string Label { get; set; }
        public string Tooltip { get; set; }
        public string BusinessObjectFieldName { get; set; }
        public bool Visible { get; set; }
        public int FieldType { get; set; }
        public int MaxLength { get; set; }
        public bool? Required { get; set; }
        public List<PicklistValue> PicklistValues { get; set; }
        public int? LookupDisplay { get; set; }
        public string ReferenceBusinessObjectName { get; set; }
        public string DefaultViewId { get; set; }
        public bool? ShowTooltip { get; set; }
    }

    public class Column2
    {
        public int ColumnSize { get; set; }
        public List<Field> Fields { get; set; }
        public List<Section> Sections { get; set; }
    }

    public class Section
    {
        public string Label { get; set; }
        public bool ShowLabel { get; set; }
        public bool Visible { get; set; }
        public string Name { get; set; }
        public bool ShowInstructions { get; set; }
        public string Instructions { get; set; }
        public List<Column> Columns { get; set; }
    }

    public class Tab
    {
        public string Label { get; set; }
        public bool Visible { get; set; }
        public string Name { get; set; }
        public List<Column> Columns { get; set; }
    }

    public class DataForm
    {
        public string Label { get; set; }
        public string Instructions { get; set; }
        public string BusinessObjectName { get; set; }
        public Settings Settings { get; set; }
        public List<Tab> Tabs { get; set; }
    }




}
