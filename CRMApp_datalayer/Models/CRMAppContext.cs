using CRMApp_datalayer.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.Models
{
    public class CRMAppContext : DbContext
    {
        public CRMAppContext() : base("CRMAppConstr")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CRMAppContext, Configuration>());
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Customer_CustomerType> Customer_CustomerTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
