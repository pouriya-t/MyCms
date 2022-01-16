using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class PageComment
    {
        [Key]
        public int CommentId { get; set; }


        public virtual Page Page { get; set; }
        [Display(Name = "خبر")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        public int PageId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
         [MaxLength(150)]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "سایت")]
        public string WebSite { get; set; }

        [Display(Name = "نظر")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        public string Comment { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; }
    }
}
