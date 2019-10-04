using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ImageViewerASP.Models
{
    public class FolderDirectory : Card
    {
        public IEnumerable<Card> Chapters { get; set; }

        public override string FirstImageSrc()
        {

        }
    }
}
