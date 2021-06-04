using System.Data.Entity;

namespace Diary
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }
    }
}
