using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ImageViewerASP.Models
{
    public class ChapterDirectory : Card
    {
        public virtual string NextChapterRequestId { get; set; }
        public virtual string PreviousChapterRequestId { get; set; }
        public virtual IEnumerable<string> RequestImages { get; set; }
    }
}
