using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class PageGroup
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage =" لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string GroupTitle { get; set; }

        public virtual ICollection<Page> Pages { get; set; }
    }
}
