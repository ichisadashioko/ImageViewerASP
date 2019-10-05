using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ImageViewerASP.Services;
using System.Web;

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
                return HttpUtility.UrlEncode(RelativeRequestPath);
            }
        }
        public string PreviewImage
        {
            get
            {
                string imageLocalPath = ImageUtils.GetFirstImageForPreview(LocalPath);
                string imageRemotePath = ImageUtils.MapLocalToRemote(imageLocalPath, BaseImagePath, RequestPath);
                return imageRemotePath;
            }
        }
    }
}
