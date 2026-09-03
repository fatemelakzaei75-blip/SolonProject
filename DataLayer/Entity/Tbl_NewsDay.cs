using DataLayer.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class Tbl_NewsDay:Tbl_BaseEntity
    {
     
        [Display(Name = "متن خبر")]
        [Required(ErrorMessage = "متن خبر الزامی است")]
        public  string TextNews { get; set; }

        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "تاریخ شروع الزامی است")]
        public DateTime DateStart { get; set; }

        [Display(Name = "تاریخ پایان")]
        [Required(ErrorMessage = "تاریخ پایان الزامی است")]
        public DateTime DateEnd { get; set; }

        [Display(Name = "زمان شروع")]
        [Required(ErrorMessage = "زمان شروع الزامی است")]
        public TimeSpan TimeStart { get; set; }

        [Display(Name = "زمان پایان")]
        [Required(ErrorMessage = "زمان پایان الزامی است")]
        public TimeSpan TimeEnd { get; set; }

        [Display(Name = "TC پرسنل")]
        [Required(ErrorMessage = "TC پرسنل الزامی است")]
        public Guid TC_Personal { get; set; }
    }
}
