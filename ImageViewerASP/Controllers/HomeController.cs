using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageViewerASP.Models;
using ImageViewerASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ImageViewerASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<AppConfig> _config;
        private readonly ImageUtils _imageUtils;
        public HomeController(IOptions<AppConfig> config)
        {
            _config = config;
            _imageUtils = new ImageUtils(config);
        }
        public IActionResult Index()
        {
            string renderPath = _config.Value.ImagePath;
            IEnumerable<Card> cards = _imageUtils.GetCards(renderPath);
            ViewBag.Title = "Index";
            return View(cards);
        }

        public IActionResult Manga(string requestId)
        {
            Debug.WriteLine($"requestId: {requestId}");
            string relativeRequestPath = Uri.UnescapeDataString(requestId);
            string requestPath = Regex.Replace($"{_config.Value.RequestPath}/{relativeRequestPath}", @"/+", "/");
            string localPath = _imageUtils.MapRequestToLocal(requestPath);
            var dirType = ImageUtils.GetDirectoryType(localPath);

            if (dirType == DirectoryType.AllFile)
            {
                // Get next and previous chapters if exist
                string parentDir = Directory.GetParent(localPath).FullName;
                string prevId = "";
                string nextId = "";

                var siblingDirs = Directory.GetDirectories(parentDir);
                int i;
                for (i = 0; i < siblingDirs.Length; i++)
                {
                    if (String.Compare(siblingDirs[i], localPath) == 0)
                    {
                        break;
                    }
                }
                if (i > 0)
                {
                    var prevChapter = new ChapterDirectory
                    {
                        BaseImagePath = _config.Value.ImagePath,
                        RequestPath = _config.Value.RequestPath,
                        LocalPath = siblingDirs[i - 1],
                    };
                    prevId = prevChapter.RequestId;
                }
                if (i < (siblingDirs.Length - 1))
                {
                    var nextChapter = new ChapterDirectory
                    {
                        BaseImagePath = _config.Value.ImagePath,
                        RequestPath = _config.Value.RequestPath,
                        LocalPath = siblingDirs[i + 1],
                    };
                    nextId = nextChapter.RequestId;
                }

                // get all images
                var images = Directory.EnumerateFiles(localPath).Select(_imageUtils.MapLocalToRequest);

                var chapter = new ChapterDirectory
                {
                    BaseImagePath = _config.Value.ImagePath,
                    RequestPath = _config.Value.RequestPath,
                    LocalPath = localPath,
                    RequestImages = images,
                    PreviousChapterRequestId = prevId,
                    NextChapterRequestId = nextId,
                };
                ViewBag.Title = chapter.Name;
                return View("Chapter", chapter);
            }
            IEnumerable<Card> cards = _imageUtils.GetCards(localPath);
            if (cards.Count() == 1)
            {
                localPath = cards.First().LocalPath;
                var images = Directory.EnumerateFiles(localPath).Select(_imageUtils.MapLocalToRequest);
                var chapter = new ChapterDirectory
                {
                    BaseImagePath = _config.Value.ImagePath,
                    RequestPath = _config.Value.RequestPath,
                    LocalPath = localPath,
                    RequestImages = images,
                };
                ViewBag.Title = chapter.Name;
                return View("Chapter", chapter);
            }
            ViewBag.Title = Path.GetFileName(localPath);
            return View("Index", cards);
        }
    }
}