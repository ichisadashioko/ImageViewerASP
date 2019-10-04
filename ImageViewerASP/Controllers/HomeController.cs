using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageViewerASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
            return View();
        }
    }
}