using Data.Context;
using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class PageCommentRepository : IPageCommentRepository
    {
        private MyCmsContext _db;
        public PageCommentRepository(MyCmsContext context)
        {
            _db = context;
        }
        public bool AddComment(PageComment comment)
        {
            try
            {
                _db.PageComments.Add(comment);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public IEnumerable<PageComment> GetCommentByNewsId(int pageId)
        {
            return _db.PageComments.Where(c => c.PageId == pageId);
        }
    }
}
