using System.Reflection;
using System.Data.Entity;
// Used modules and interfaces in the project
using BusinessObjects;
using BusinessObjects.Orders;

namespace DataAccess
{
    public class DataBaseMigrator : DbContext
    {
        public DataBaseMigrator(): base("DOShop") { }

        // All the User types in the system
        public DbSet<Client> Client { get; set; }
        public DbSet<Admin> Admin { get; set; }

        // Everything associated with orders
        public DbSet<ClientOrders> Orders { get; set; }
        public DbSet<ItemsInCart> ItemsInCart { get; set; }

        // Everything associated with items in the shop
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Overrides for the convention-based mappings.
            // We're assuming that all our fluent mappings are declared in this assembly.
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(DataBaseMigrator)));

            modelBuilder.Entity<Item>().HasKey(q => q.Id);
            modelBuilder.Entity<Property>().HasKey(q => q.Id);
            modelBuilder.Entity<ItemProperty>().HasKey(q =>
                new {
                    q.ItemId,
                    q.PropertyId
                });

            modelBuilder.Entity<ItemProperty>()
                .HasRequired(t => t.Item)
                .WithMany(t => t.ItemProperties)
                .HasForeignKey(t => t.ItemId);

            modelBuilder.Entity<ItemProperty>()
                .HasRequired(t => t.Property)
                .WithMany(t => t.ItemProperties)
                .HasForeignKey(t => t.PropertyId);
        }
        
    }
}