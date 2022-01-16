using Data.Context;
using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class PageRepository : IPageRepository
    {
        private MyCmsContext _db;

        public PageRepository(MyCmsContext context)
        {
            _db = context;
        }


        public IEnumerable<Page> GetAllPage()
        {
            return _db.Pages;
        }

        public Page GetPageById(int pageId)
        {
            return _db.Pages.FirstOrDefault(p => p.PageId == pageId);
        }

        public bool InsertPage(Page page)
        {
            try
            {
                _db.Pages.Add(page);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool UpdatePage(Page page)
        {
            try
            {
                _db.Entry(page).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePage(Page page)
        {
            try
            {
                _db.Entry(page).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePage(int pageId)
        {
            try
            {
                DeletePage(GetPageById(pageId));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Page> TopNews(int take = 4)
        {
            return _db.Pages.OrderByDescending(p => p.Visit).Take(take);
        }

        public IEnumerable<Page> PagesInSlider()
        {
            return _db.Pages.Where(p => p.ShowInSlider);
        }

        public IEnumerable<Page> LastNews(int take = 4)
        {
            return _db.Pages.OrderByDescending(p => p.CreateDate).Take(take);
        }

        public IEnumerable<Page> ShowPageByGroupId(int groupId)
        {
            return _db.Pages.Where(p => p.GroupId == groupId);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IEnumerable<Page> SearchPage(string search)
        {
            // library loosing in .net for advanced search 
            return _db.Pages.Where(p => p.Title.Contains(search) || p.ShortDescription.Contains(search)
                    || p.Tags.Contains(search) || p.Text.Contains(search)).Distinct();
        }
    }
}
