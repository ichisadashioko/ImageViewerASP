using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsolePlayground
{
    class Program
    {
        public enum ChildrenFileType
        {
            Empty,
            Directory,
            File,
            Mix,
        }
        public static ChildrenFileType GetChildrenFileType(string root)
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

        public static void RemoveEmptyDirectory(string root)
        {
            var attr = File.GetAttributes(root);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                var directoryType = GetChildrenFileType(root);
                if (directoryType == ChildrenFileType.Empty)
                {
                    Console.WriteLine($"{directoryType.ToString()}: {root}");
                    // careful to check your code before comment out the following line
                    //Directory.Delete(root);
                }
                if (directoryType == ChildrenFileType.Directory || directoryType == ChildrenFileType.Mix)
                {
                    var children = Directory.EnumerateDirectories(root);
                    foreach (var child in children)
                    {
                        RemoveEmptyDirectory(child);
                    }
                }
            }
        }

        public static void GetFirstImageForPreview(string root)
        {
            var attr = File.GetAttributes(root);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                var directoryType = GetChildrenFileType(root);
                if (directoryType == ChildrenFileType.Mix)
                {
                    Console.WriteLine($"{directoryType.ToString()}: {root}");
                }
                if (directoryType == ChildrenFileType.Directory || directoryType == ChildrenFileType.Mix)
                {
                    var children = Directory.EnumerateDirectories(root);
                    foreach (var child in children)
                    {
                        GetFirstImageForPreview(child);
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            string imagePath = @"D:\fmd_0.9.158.0_Win64\downloads";
            string requestPath = "/images";
            //IEnumerable<string> dirList = Directory.EnumerateDirectories(imagePath);
            //foreach (var dirPath in dirList)
            //{
            //    Console.WriteLine(dirPath.Replace(imagePath, requestPath).Replace('\\', '/'));
            //}
            //GetFirstImageForPreview(imagePath);
            RemoveEmptyDirectory(imagePath);

            Console.ReadLine();
        }
    }
}
