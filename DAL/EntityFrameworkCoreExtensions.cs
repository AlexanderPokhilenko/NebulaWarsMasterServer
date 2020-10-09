using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public static class EntityFrameworkCoreExtensions
    {
        //From: https://entityframework.net/knowledge-base/5466677/undo-changes-in-entity-framework-entities
        public static void Rollback(this DbContext context)
        {
            var changedEntries = context.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
    }
}