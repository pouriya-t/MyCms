﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.Context;
using Data.Models;
using Data.Repositories;
using Data.Services;

namespace MyCms.Areas.Admin.Controllers
{
    [Authorize]
    public class PagesController : Controller
    {
        private IPageRepository _pageRepository;
        private IPageGroupRepository _pageGroupRepository;
        private MyCmsContext _db = new MyCmsContext();

        public PagesController()
        {
            _pageRepository = new PageRepository(_db);
            _pageGroupRepository = new PageGroupRepository(_db);
        }


        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View(_pageRepository.GetAllPage());
        }

        // GET: Admin/Pages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = _db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // GET: Admin/Pages/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(_pageGroupRepository.GetAllGroups(), "GroupId", "GroupTitle");
            return View();
        }

        // POST: Admin/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageId,GroupId,Title,ShortDescription,Text,Visit,ImageName," +
                        "ShowInSlider,CreateDate,Tags")] Page page,
                                    HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)
            {
                page.Visit = 0;
                page.CreateDate = DateTime.Now;

                if (imgUpload != null)
                {
                    page.ImageName = Guid.NewGuid() + Path.GetExtension(imgUpload.FileName);
                    imgUpload.SaveAs(Server.MapPath("/PageImages/" + page.ImageName));
                }

                _pageRepository.InsertPage(page);
                _pageRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(_db.PageGroups, "GroupId", "GroupTitle", page.GroupId);
            return View(page);
        }

        // GET: Admin/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = _pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(_pageGroupRepository.GetAllGroups(), "GroupId", "GroupTitle", page.GroupId);
            return View(page);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageId,GroupId,Title,ShortDescription,Text,Visit" +
            "                   ,ImageName,ShowInSlider,CreateDate,Tags")] Page page,
                                    HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)
            {
                if (imgUpload != null)
                {
                    if(page.ImageName != null)
                    {
                        System.IO.File.Delete(Server.MapPath("/PageImages/" + page.ImageName));
                    }
                    page.ImageName = Guid.NewGuid() + Path.GetExtension(imgUpload.FileName);
                    imgUpload.SaveAs(Server.MapPath("/PageImages/" + page.ImageName));
                }

                _pageRepository.UpdatePage(page);
                _pageRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(_db.PageGroups, "GroupId", "GroupTitle", page.GroupId);
            return View(page);
        }

        // GET: Admin/Pages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = _pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var page = _pageRepository.GetPageById(id);
            if (page.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/PageImages/" + page.ImageName));
            }
            _pageRepository.DeletePage(page);
            _pageRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
