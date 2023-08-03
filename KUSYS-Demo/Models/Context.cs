using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Models
{
    public class Context : DbContext
    {

        public Context()
        {

        }

        public Context(DbContextOptions<Context> options) : base(options) { 
       
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentsCourse> StudentsCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // StudentsCourses tablosuna Students ve Courses tablolarına bağlı 2 adet primarykey eklemek için
            modelBuilder.Entity<StudentsCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));
            // Her kayıt işleminde eklenme veya güncelleme durumlarında CreateDate ve UpdateDate verilerin güncellenmesi
            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdateDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreateDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
