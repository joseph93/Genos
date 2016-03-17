using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Exhibition_Content
{
    public class Video : ExhibitionContent
    {
        private string videoPath;
        public Video(string title, string contentID, int storylineID, string videoPath) : base(title, contentID, storylineID)
        {
            this.videoPath = videoPath;
        }
    }
}
