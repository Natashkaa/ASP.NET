namespace ShopAdoWeb
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShopAdo_Context : DbContext
    {
        public ShopAdo_Context()
            : base("name=ShopAdo_Context")
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Good> Good { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Good>()
                .Property(e => e.GoodName)
                .IsUnicode(false);

            modelBuilder.Entity<Good>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Good>()
                .Property(e => e.GoodCount)
                .HasPrecision(18, 3);
        }
    }
}
