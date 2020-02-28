using StageManager.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace StageManager.DAL
{
    public class StageManagerContext : DbContext
    {
        public StageManagerContext() : base("StageManagerContext")
        {
        }

        public DbSet<Stagiaire> Stagiaires { get; set; }
        public DbSet<NoteStagiaire> NotesStagiaires { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}