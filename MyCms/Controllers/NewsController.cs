using Data.Context;
using Data.Models;
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
    public class NewsController : Controller
    {
        MyCmsContext _db = new MyCmsContext();
        private IPageGroupRepository _pageGroupRepository;
        private IPageRepository _pageRepository;
        private IPageCommentRepository _pageCommentRepository;

        public NewsController()
        {
            _pageGroupRepository = new PageGroupRepository(_db);
            _pageRepository = new PageRepository(_db);
            _pageCommentRepository = new PageCommentRepository(_db);
        }

        public ActionResult ShowGroups()
        {
            return PartialView(_pageGroupRepository.GetGroupsForView());
        }

        public ActionResult ShowGroupsInMenu()
        {
            return PartialView(_pageGroupRepository.GetAllGroups());
        }

        public ActionResult TopNews()
        {
            return PartialView(_pageRepository.TopNews());
        }

        public ActionResult LatesNews()
        {
            return PartialView(_pageRepository.LastNews());
        }

        [Route("Archive")]
        public ActionResult ArchiveNews()
        {
            return View(_pageRepository.GetAllPage());
        }

        [Route("Group/{id}/{title}")]
        public ActionResult ShowNewsByGroupId(int id, string title)
        {
            ViewBag.name = title;
            return View(_pageRepository.ShowPageByGroupId(id));
        }

        [Route("News/{id}")]
        public ActionResult ShowNews(int id)
        {
            var news = _pageRepository.GetPageById(id);

            if (news == null) return HttpNotFound();

            news.Visit += 1;
            _pageRepository.UpdatePage(news);
            _pageRepository.Save();

            return View(news);
        }

        public ActionResult AddComment(int id, string name, string email, string comment)
        {
            PageComment addComment = new PageComment
            {
                CreateDate = DateTime.Now,
                PageId = id,
                Comment = comment,
                Email = email,
                Name = name
            };
            _pageCommentRepository.AddComment(addComment);
            return PartialView("ShowComments", _pageCommentRepository.GetCommentByNewsId(id));
        }

        public ActionResult ShowComments(int id)
        {
            return PartialView(_pageCommentRepository.GetCommentByNewsId(id));
        }
    }
}
