using DataLayer.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class Tbl_Mobile:Tbl_BaseEntity
    {
        public int ID { get; set; }
        [Display(Name = "شماره همراه کاربر")]
        [Required(ErrorMessage = "شماره موبایل الزامی اسست")]
        [RegularExpression(@"09\d{9}$", ErrorMessage = "شماره موبایل معتبر نیست")]
        public string MobileNumber { get; set; }
        public string TcPersonal { get; set; }
       
    }
}
