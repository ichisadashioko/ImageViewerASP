using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Options;

namespace ImageViewerASP.Services
{
    public enum ChildrenFileType
    {
        Empty,
        Directory,
        File,
        Mix,
    }
    public class ImageUtils
    {
        private readonly IOptions<AppConfig> _config;
        public ImageUtils(IOptions<AppConfig> config)
        {
            _config = config;
        }
        protected virtual ChildrenFileType GetChildrenFileType(string root)
        {
            int numDirs = Directory.GetDirectories(root).Length;
            int numFiles = Directory.GetFiles(root).Length;
            if (numDirs == 0 && numFiles == 0)
            {
                return ChildrenFileType.Empty;
            }
            else if (numDirs == 0 && numFiles != 0)
            {
                return ChildrenFileType.File;
            }
            else if (numDirs != 0 && numFiles == 0)
            {
                return ChildrenFileType.Directory;
            }
            else
            {
                return ChildrenFileType.Mix;
            }
        }
        public virtual IEnumerable<string> GetFolders(string root)
        {
            return Directory.EnumerateDirectories(root).OrderBy(x => x);
        }

        public virtual IEnumerable<string> GetImageFiles(string root)
        {
            return Directory.EnumerateFiles(root).OrderBy(x => x);
        }

        public virtual string MapLocalToWeb(string path)
        {
            string imagePath = _config.Value.ImagePath;
            string requestPath = _config.Value.RequestPath;

            return path.Replace(imagePath, requestPath)
                .Replace('\\', '/');
        }

        /// <summary>
        /// Find the first image in the `root` directory.
        /// Recursive walk in to the first directory to find the image.
        /// </summary>
        /// <param name="root">The directory path in local drive.</param>
        /// <returns></returns>
        public virtual string GetFirstImageForPreview(string root)
        {
            var attr = File.GetAttributes(root);
            if((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {

            }
            else
            {
                return root;
            }
        }
    }
}
