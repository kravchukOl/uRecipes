using Microsoft.EntityFrameworkCore;


namespace uRecipes.Services.SqLiteRepository
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet <Recipe> Recipes { get; set; }
        public DbSet <Ingredients> Ingredients { get; set; }
        public DbSet <Instructions> Instructions { get; set; }
        public DbSet <NutritionInfo> NutritionInfos { get; set; }
        public DbSet <Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Finename=EfRecipes.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasKey(e => e.Id);
            modelBuilder.Entity<Ingredients>().HasKey(e => e.Id);
            modelBuilder.Entity<Instructions>().HasKey(e => e.Id);
            modelBuilder.Entity<NutritionInfo>().HasKey(e => e.Id);
            modelBuilder.Entity<Category>().HasKey(e => e.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
