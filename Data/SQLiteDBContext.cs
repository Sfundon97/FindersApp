// Programmer name : S Nondwatyu
// Student nr : 220036624
// Assignment nr : GA1
// Purpose : The purpose of this SQLiteDBContext class is to serve as the database context
// for interacting with SQLite database in an ASP.NET Core application.
// It inherits from the DbContext class provided by Entity Framework Core,
// enabling the application to perform database operations such as creating,
// updating, and deleting data from the SQLite database.

using Finders.Models;
using Microsoft.EntityFrameworkCore;

namespace Finders.Data
{
    public class SQLiteDBContext : DbContext
    {
        public SQLiteDBContext(DbContextOptions <SQLiteDBContext> options) : base(options)
        {
        }

        public DbSet<Finder> Finders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Finder>().ToTable("CIPC");
           

        }
    }
}
