using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ImageViewerASP.Models
{
    public abstract class Card
    {
        public string RelativePath { get; set; }
        public string Name { get { return Path.GetFileName(Path.GetDirectoryName(RelativePath)); } }
        public abstract string FirstImageSrc();

    }
}
