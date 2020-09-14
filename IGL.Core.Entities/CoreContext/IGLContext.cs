using IGL.Core.Entities.Master;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.Entities.CoreContext
{
   public  class IGLContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server= DESKTOP-SF1G3N8\\VIPRAIT; Database= IGLDevelopment; User Id=sa;Password = vi@pra91");
        }

        public DbSet<UnitMaster> UnitMasters { get; set; }
        public DbSet<CustomerMaster> CustomerMasters { get; set; }
        public DbSet<MaterialMaster> MaterialMasters { get; set; }
    }
}
