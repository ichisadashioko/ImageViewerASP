using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ImageViewerASP.Models
{
    public class MangaDirectory : Card
    {
        public IEnumerable<Card> ChildrenCard { get; set; }
    }
}
