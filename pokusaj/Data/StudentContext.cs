using pokusaj.Models;
using Microsoft.EntityFrameworkCore;
using pokusaj.ViewModels;

namespace pokusaj.Data
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<ProfWithSubject> ProfsWithSub { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<InstructionsDate> Instructions { get; set; }
        public StudentContext(DbContextOptions options ) : base(options) 
        { 

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=baza.db");
        }
    }
}
