using Data.Context;
using Data.Repositories;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyCms.Controllers
{
    public class SearchController : Controller
    {
        private IPageRepository _pageRepository;
        MyCmsContext _db = new MyCmsContext();

        public SearchController()
        {
            _pageRepository = new PageRepository(_db);
        }
        public ActionResult Index(string q)
        {
            ViewBag.Name = q;
            return View(_pageRepository.SearchPage(q));
        }
    }
}
