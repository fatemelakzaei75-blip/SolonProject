using DataLayer.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class Tbl_Category:Tbl_BaseEntity
    {
        [Display(Name = "TC سالن")]
        [Required(ErrorMessage = "TC سالن الزامی است")]
        public Guid SalonTC { get; set; }
        [Display(Name = "نام دسته‌بندی")]
        [Required(ErrorMessage = "نام دسته‌بندی الزامی است")]
        [MaxLength(100, ErrorMessage = "نام دسته‌بندی نمی‌تواند بیشتر از 100 کاراکتر باشد")]
        public  string CategoryName { get; set; }
    }
}
