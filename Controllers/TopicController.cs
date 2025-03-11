using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Btl_web_nc.Controllers
{
    public class TopicController : Controller
    {
               private readonly NewsletterDBContext dBContext;

        public TopicController(NewsletterDBContext newsletterDB)
        {
           dBContext = newsletterDB; 
        }

        public IActionResult Index()
        {
            var topics = dBContext.Topics.ToList();
            return View(topics);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}