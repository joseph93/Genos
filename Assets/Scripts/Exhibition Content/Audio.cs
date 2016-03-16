using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Exhibition_Content
{
    public class Audio : ExhibitionContent
    {
        private string filePath;
        public Audio(string title, string contentID, int storylineID, string filePath) : base(title, contentID, storylineID)
        {
            this.filePath = filePath;
        }
    }
}
