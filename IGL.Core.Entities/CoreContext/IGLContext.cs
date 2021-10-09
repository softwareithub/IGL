using IGL.Core.Entities.Inventory;
using IGL.Core.Entities.Master;
using IGL.Core.Entities.Organization;
using IGL.Core.Entities.ProductTransaction;
using IGL.Core.Entities.SIV;
using IGL.Core.Entities.StoreMaster;
using IGL.Core.Entities.Transaction;
using IGL.Core.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;


namespace IGL.Core.Entities.CoreContext
{
    public class IGLContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server= 89.163.218.70\\MSSQLSERVER2017; Database= IGL_Development; User Id=igl;Password = Manoj@12345");
        }

        public DbSet<UnitMaster> UnitMasters { get; set; }
        public DbSet<CustomerMaster> CustomerMasters { get; set; }
        public DbSet<MaterialMaster> MaterialMasters { get; set; }
        public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public DbSet<MaterialTransction> MaterialTransctions { get; set; }
        public DbSet<TransactionItems> TransactionItems { get; set; }
        public DbSet<VendorMaster> VendorMasters { get; set; }
        public DbSet<RiserMaster> RiserMasters { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<POItem> POItems { get; set; }
        public DbSet<StoreDetail> StoreDetails { get; set; }
        public DbSet<SIVDetail> SIVDetails { get; set; }
        public DbSet<SIVMaterialTransaction> SIVMaterialTransactions { get; set; }
        public DbSet<ProductTransactionDetail> ProductTransactionDetails { get; set; }
        public DbSet<RateMaster> RateMasters { get; set; }
        public DbSet<MenuSubMenuModel> MenuSubMenuModels { get; set; }
        public DbSet<RoleAccess> RoleAccess { get; set; }
    }
}
