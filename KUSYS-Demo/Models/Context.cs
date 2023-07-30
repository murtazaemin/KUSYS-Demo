using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Models
{
    public class Context : DbContext
    {


        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).updated_at = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).created_at = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
