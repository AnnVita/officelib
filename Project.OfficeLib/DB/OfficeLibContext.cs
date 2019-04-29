using Microsoft.EntityFrameworkCore;

namespace OfficeLib.DB
{
    public class OfficeLibContext : DbContext
    {
        public OfficeLibContext(DbContextOptions<OfficeLibContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
    }
}