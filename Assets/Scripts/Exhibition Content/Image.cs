using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Exhibition_Content
{
    public class Image : ExhibitionContent
    {
        private string filePath;
        private int width;
        private int height;
        public Image(string title, string contentID, int storylineID, string filePath, int width, int height) : base(title, contentID, storylineID)
        {
            this.filePath = filePath;
            this.width = width;
            this.height = height;
        }
    }
}
