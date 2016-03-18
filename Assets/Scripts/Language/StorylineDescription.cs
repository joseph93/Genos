using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Language
{
    public class StorylineDescription
    {
        public string title { get; set; }
        public string description { get; set; }

        public StorylineDescription(string title, string descr)
        {
            this.title = title;
            description = descr;
        }
    }
}
