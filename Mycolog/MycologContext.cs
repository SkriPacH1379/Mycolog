using System.Data.Entity;

namespace Mycolog
{
    public class MycologContext : DbContext
    {
        public MycologContext() : base("MycologDbConnection") { }

        public DbSet<MushroomCulture> Cultures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MushroomCulture>().ToTable("Cultures");
            base.OnModelCreating(modelBuilder);
        }
    }
}