using DataLayer.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class Tbl_Personal:Tbl_BaseEntity
    {
        public Guid salon_Tc { get; set; }
        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "تاریخ تولد الزامی است")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "TC سرویس")]
        [Required(ErrorMessage = "TC سرویس الزامی است")]
        public Guid TC_Service { get; set; }
    }
}
