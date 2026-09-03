using DataLayer.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class Tbl_Permission:Tbl_BaseEntity
    {
        [Display(Name = "نام دسترسی")]
        [Required(ErrorMessage = "نام دسترسی الزامی است")]
        [MaxLength(100, ErrorMessage = "نام دسترسی نمی‌تواند بیشتر از 100 کاراکتر باشد")]
        public  string PermissionName { get; set; }
    }
}
