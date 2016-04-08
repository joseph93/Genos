using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Language;

namespace Assets.Scripts
{
    public class StoryPoint
    {
        public int storylineID { get; set; }
        private StoryPointDescription spd;
        private List<ExhibitionContent> contents;

        public StoryPoint(int sid, StoryPointDescription spd)
        {
            storylineID = sid;
            this.spd = spd;
            contents = new List<ExhibitionContent>();
        }

        public void setContentList(List<ExhibitionContent> c)
        {
            contents = c;
        }

        public void setTitleAndSummary(string title, string summary, string lg)
        {
            if (spd != null)
            {
                spd.title = title;
                spd.description = summary;
            }
            else
            {
                spd = new StoryPointDescription(title, summary, lg);
            }
        }

        public StoryPointDescription GetStorylineDescription()
        {
            return spd;
        }
    }
}
