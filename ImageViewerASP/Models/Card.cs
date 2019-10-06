using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ImageViewerASP.Services;
using System.Web;
using System.Text.RegularExpressions;

namespace ImageViewerASP.Models
{
    public class Card
    {
        public string LocalPath { get; set; }
        public string BaseImagePath { get; set; }
        public string RequestPath { get; set; }

        public virtual string Name
        {
            get
            {
                return Path.GetFileName(LocalPath);
            }
        }
        public virtual string RelativeLocalPath
        {
            get
            {
                return LocalPath.Replace(BaseImagePath, "");
            }
        }
        public virtual string RelativeRequestPath
        {
            get
            {
                return RelativeLocalPath.Replace('\\', '/');
            }
        }
        public virtual string RequestId
        {
            get
            {
                //return HttpUtility.UrlEncode(Regex.Replace(RelativeRequestPath, @"^/", ""));
                return Uri.EscapeDataString(Regex.Replace(RelativeRequestPath, @"^/", ""));
            }
        }
        public string PreviewImage
        {
            get
            {
                string imageLocalPath = ImageUtils.GetFirstImageForPreview(LocalPath);
                string imageRequestPath = ImageUtils.MapLocalToRequest(imageLocalPath, BaseImagePath, RequestPath);
                return imageRequestPath;
            }
        }
    }
}
