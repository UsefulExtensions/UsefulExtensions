using System;
using System.Data.Entity;
using System.Linq;
using UsefulExtensions.EntityFramework.Models;

namespace UsefulExtensions.EntityFramework
{
    public abstract class BaseDbContext : DbContext
    {
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var deletedEntities = ChangeTracker.Entries()
                                               .Where(t => t.State == EntityState.Deleted);

            foreach (var deletedEntity in deletedEntities)
            {
                if (!(deletedEntity.Entity is ISoftDeletable item)) continue;
                item.IsDeleted = true;
                deletedEntity.State = EntityState.Modified;
            }

            var addedEntities = ChangeTracker.Entries()
                                             .Where(t => t.State == EntityState.Added)
                                             .Select(t => t.Entity);
            var modifiedEntities = ChangeTracker.Entries()
                                                .Where(t => t.State == EntityState.Modified)
                                                .Select(t => t.Entity);

            foreach (var addedEntity in addedEntities)
            {
                if (!(addedEntity is IAudit item)) continue;
                item.CreateDate = DateTime.UtcNow;
                item.LastModifiedDate = DateTime.UtcNow;
            }

            foreach (var modifiedEntity in modifiedEntities)
            {
                if (!(modifiedEntity is IAudit item)) continue;
                item.LastModifiedDate = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }
    }
}