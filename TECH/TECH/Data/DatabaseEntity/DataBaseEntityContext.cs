using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TECH.Data.DatabaseEntity
{
    public class DataBaseEntityContext : DbContext
    {
        public DataBaseEntityContext(DbContextOptions<DataBaseEntityContext> options) : base(options) { }

        public DbSet<Users> users { set; get; }
        public DbSet<Contracts> contacts { set; get; }
        public DbSet<Category> categories { set; get; }
        public DbSet<Products> products { set; get; }
        public DbSet<ProductQuantity> product_quantity { set; get; }
        public DbSet<ProductImages> products_images { set; get; }
        public DbSet<Images> images { set; get; }
        public DbSet<Colors> colors { set; get; }
        public DbSet<Size> sizes { set; get; }
        public DbSet<Posts> posts { set; get; }
        public DbSet<Orders> orders { set; get; }
        public DbSet<Reviews> reviews { set; get; }
        public DbSet<Fees> fees { set; get; }
        public DbSet<City> cities { set; get; }
        public DbSet<OrdersDetails> order_details { set; get; }

        public DbSet<Districts> districts { set; get; }
        public DbSet<Wards> wards { set; get; }
        public DbSet<Carts> carts { set; get; }
        public DbSet<Siders> siders { set; get; }
        public DbSet<Advertisement> advertisement { set; get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);            
        }
    }
}
