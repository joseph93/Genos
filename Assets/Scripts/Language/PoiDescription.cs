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
        public Language language { get; set; }

        public PoiDescription(string title, string summary, string lg)
        {
            this.title = title;
            this.summary = summary;
            language = convertStringToLang(lg);
        }

        public void setTitle(string title)
        {
            this.title = title;
        }

        public void setDescription(string descr)
        {
            this.summary = descr;
        }

        public void setLanguage(string lg)
        {
            this.language = PoiDescription.convertStringToLang(lg);
        }

        public static Language convertStringToLang(string lg)
        {
            if (lg == null)
            {
                return Language.NONE;
            }
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
