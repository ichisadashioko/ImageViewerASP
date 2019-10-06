using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsolePlayground
{
    class Program
    {
        static void RemoveHashFromPath(string root)
        {
            string fileName = Path.GetFileName(root);
            if (fileName.Contains("#"))
            {
                string newFileName = fileName.Replace("#", "");
                string newPath = root.Replace(fileName, newFileName);
                Console.WriteLine(root);
                Console.WriteLine(newPath);

                //Directory.Move(root, newPath);
                //root = newPath;
            }
            IEnumerable<string> dirList = Directory.EnumerateDirectories(root);
            foreach(var dir in dirList)
            {
                RemoveHashFromPath(dir);
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
            //    Console.WriteLine(Uri.EscapeUriString(dirPath));
            //}

            RemoveHashFromPath(imagePath);

            Console.ReadLine();
        }
    }
}
