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
                var images = Directory.EnumerateFiles(localPath).Select(_imageUtils.MapLocalToRequest);
                var chapter = new ChapterDirectory
                {
                    BaseImagePath = _config.Value.ImagePath,
                    RequestPath = _config.Value.RequestPath,
                    LocalPath = localPath,
                    RequestImages = images,
                };
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
                return View("Chapter", chapter);
            }
            return View("Index", cards);
        }
    }
}