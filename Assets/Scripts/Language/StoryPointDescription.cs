using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Language
{
    public class StoryPointDescription
    {
        public string title { get; set; }
        public string description { get; set; }
        public Language language { get; set; }

        public StoryPointDescription(string title, string descr, string lg)
        {
            this.title = title;
            description = descr;
            language = convertStringToLang(lg);
        }

        public Language convertStringToLang(string lg)
        {
            if (lg.Equals("EN"))
            {
                return Language.EN;
            }
            
            if (lg.Equals("FR"))
            {
                return Language.FR;
            }

            return Language.NONE;

        }
    }
}
