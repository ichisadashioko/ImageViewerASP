using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageViewerASP.Models
{
    public class ChapterDirectory : Card
    {
        public virtual IEnumerable<string> RequestImages { get; set; }
    }
}
