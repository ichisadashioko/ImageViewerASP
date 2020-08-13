using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Options;
using ImageViewerASP.Models;
using System.Diagnostics;
using System.Reflection;

namespace ImageViewerASP.Services
{
    public enum DirectoryType
    {
        Empty,
        AllDirectory,
        AllFile,
        Mix,
    }

    public class ImageUtils
    {
        private readonly IOptions<AppConfig> _config;
        public ImageUtils(IOptions<AppConfig> config)
        {
            _config = config;
        }

        public static bool DoesDirectoryContainFiles(string path)
        {
            var numFiles = Directory.GetFiles(path).Length;

            if (numFiles > 0)
            {
                return true;
            }

            var numDirs = Directory.GetDirectories(path).Length;

            if (numDirs > 0)
            {
                foreach (var childDir in Directory.EnumerateDirectories(path))
                {
                    if (DoesDirectoryContainFiles(childDir))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static DirectoryType GetDirectoryType(string root)
        {
            if (!DoesDirectoryContainFiles(root))
            {
                return DirectoryType.Empty;
            }

            var numDirs = Directory.GetDirectories(root).Length;
            var numFiles = Directory.GetFiles(root).Length;

            if (numDirs == 0 && numFiles != 0)
            {
                return DirectoryType.AllFile;
            }
            else if (numDirs != 0 && numFiles == 0)
            {
                return DirectoryType.AllDirectory;
            }
            else
            {
                return DirectoryType.Mix;
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

        public static string MapLocalToRequest(string path, string imagePath, string requestPath)
        {
            return path.Replace(imagePath, requestPath)
                .Replace('\\', '/');
        }

        public virtual string MapLocalToRequest(string path)
        {
            string imagePath = _config.Value.ImagePath;
            string requestPath = _config.Value.RequestPath;

            return path.Replace(imagePath, requestPath)
                .Replace('\\', '/');
        }

        public static string MapRequestToLocal(string path, string imagePath, string requestPath)
        {
            return path.Replace(requestPath, imagePath)
                .Replace('/', '\\');
        }
        public virtual string MapRequestToLocal(string path)
        {
            string imagePath = _config.Value.ImagePath;
            string requestPath = _config.Value.RequestPath;

            return path.Replace(requestPath, imagePath)
                .Replace('/', '\\');
        }

        /// <summary>
        /// Find the first image in the `root` directory.
        /// Recursive walk in to the first directory to find the image.
        /// Return an empty string if there is no image in directory.
        /// </summary>
        /// <param name="root">The directory path in local drive.</param>
        /// <returns></returns>
        public static string GetFirstImageForPreview(string root)
        {
            var dirType = GetDirectoryType(root);

            if (dirType == DirectoryType.AllFile || dirType == DirectoryType.Mix)
            {
                return Directory.EnumerateFiles(root).OrderBy(x => x).First();
            }
            else if (dirType == DirectoryType.AllDirectory)
            {
                // loop through all the children until find an image
                // as we are not sure the children directory is not empty
                var children = Directory.EnumerateDirectories(root).OrderBy(x => x);
                foreach (var child in children)
                {
                    var childRetval = GetFirstImageForPreview(child);
                    if (!String.IsNullOrEmpty(childRetval))
                    {
                        return childRetval;
                    }
                }
            }

            return "";
        }

        public virtual IEnumerable<Card> GetCards(string root)
        {
            // for logging
            var methodInfo = MethodBase.GetCurrentMethod();
            var methodFullName = $"{methodInfo.DeclaringType.FullName}.{methodInfo.Name}";

            List<Card> cards = new List<Card>();
            IEnumerable<string> dirList = Directory.EnumerateDirectories(root);
            foreach (var dir in dirList)
            {
                var dirType = GetDirectoryType(dir);
                if (dirType == DirectoryType.Empty)
                {
                    Debug.WriteLine($"{methodFullName}: {dir} is empty.");
                    continue;
                }
                else
                {
                    var card = new Card
                    {
                        BaseImagePath = _config.Value.ImagePath,
                        RequestPath = _config.Value.RequestPath,
                        LocalPath = dir
                    };

                    cards.Add(card);
                }
            }

            return cards;
        }
    }
}
