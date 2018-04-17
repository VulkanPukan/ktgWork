using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class Column
    {
        public string field { get; set; }
        public string title { get; set; }
        public string width { get; set; }
    }

    public class TemplateColumn :
        Column
    {
        public string template { get; set; }
    }

    public class ColumnTree :
        Column
    {
        public IList<Column> columns { get; set; }
    }
}