using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IPageGroupRepository : IDisposable
    {
        IEnumerable<PageGroup> GetAllGroups();
        PageGroup GetGroupById(int groupId);
        bool InsertGroup(PageGroup pageGroup);
        bool UpdateGroup(PageGroup pageGroup);
        bool DeleteGroup(PageGroup pageGroup);
        bool DeleteGroup(int groupId);
        IEnumerable<ShowGroupViewModel> GetGroupsForView();
        void Save();
    }
}
