using Data.Context;
using Data.Models;
using Data.Repositories;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class PageGroupRepository : IPageGroupRepository
    {
        private MyCmsContext _db;

        public PageGroupRepository(MyCmsContext context)
        {
            _db = context;
        }


        public IEnumerable<PageGroup> GetAllGroups()
        {
            return  _db.PageGroups.ToList();
        }

        public PageGroup GetGroupById(int groupId)
        {
            return _db.PageGroups.Find(groupId);
        }

        public bool InsertGroup(PageGroup pageGroup)
        {
            try
            {
                _db.PageGroups.Add(pageGroup);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool UpdateGroup(PageGroup pageGroup)
        {
            try
            {
                _db.Entry(pageGroup).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteGroup(PageGroup pageGroup)
        {
            try
            {
                _db.Entry(pageGroup).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteGroup(int groupId)
        {

            try
            {
                DeleteGroup(GetGroupById(groupId));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
             _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IEnumerable<ShowGroupViewModel> GetGroupsForView()
        {
            return _db.PageGroups.Select(g => new ShowGroupViewModel
            {
                GroupId = g.GroupId,
                GroupTitle = g.GroupTitle,
                PageCount = g.Pages.Count
            });
        }
    }
}
