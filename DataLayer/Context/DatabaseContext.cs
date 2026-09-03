using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entity;
using DataLayer.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;


namespace DataLayer.Context
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {
                
        }
        public DbSet<Tbl_Mobile> Tbl_Mobile {get; set; }
        public DbSet<Tbl_Confirm> Tbl_Confirm { get; set; }
        public DbSet<Tbl_Customer> Tbl_Customer { get; set; }
        public DbSet<Tbl_Personal> Tbl_Personal { get; set; }
        public DbSet<Tbl_Social> Tbl_Social { get; set; }
        public DbSet<Tbl_NewsDay> Tbl_NewsDay { get; set; }
        public DbSet<Tbl_Salon> Tbl_Salon { get; set; }
        public DbSet<Tbl_Roles> Tbl_Roles { get; set; }
        public DbSet<Tbl_UserRole> Tbl_UserRole { get; set; }
        public DbSet<Tbl_Permission> Tbl_Permission { get; set; }
        public DbSet<Tbl_RolePermission> Tbl_RolePermission { get; set; }
        public DbSet<Tbl_Like> Tbl_Like { get; set; }
        public DbSet<Tbl_Portfoilo> Tbl_Portfoilo { get; set; }
        public DbSet<Tbl_SalonSerice> Tbl_SalonSerice { get; set; }
        public DbSet<Tbl_Category> Tbl_Category { get; set; }
        public DbSet<Tbl_CategoryPortfolio> Tbl_CategoryPortfolio { get; set; }
    }
}
