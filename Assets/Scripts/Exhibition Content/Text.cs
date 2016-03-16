using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Exhibition_Content
{
    public class Text : ExhibitionContent
    {
        private string htmlContent;
        public Text(string title, string contentID, int storylineID, string htmlContent) : base(title, contentID, storylineID)
        {
            this.htmlContent = htmlContent;
        }
    }
}
