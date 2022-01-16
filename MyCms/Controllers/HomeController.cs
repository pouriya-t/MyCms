using Data.Context;
using Data.Repositories;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCms.Controllers
{
    public class HomeController : Controller
    {
        MyCmsContext _db = new MyCmsContext();
        private IPageRepository _pageRepository;

        public HomeController()
        {
            _pageRepository = new PageRepository(_db);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Slider()
        {
            return PartialView(_pageRepository.PagesInSlider());
        }


    }
}