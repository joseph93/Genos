using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Language
{
    public class PoiDescription
    {
        public string title { get; set; }
        public string summary { get; set; }

        public PoiDescription(string title, string summary)
        {
            this.title = title;
            this.summary = summary;
        }
    }
}
