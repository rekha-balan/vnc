using NinjaDomain.Classes;
using NinjaDomain.Classes.Interfaces;
using System;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;

namespace NinjaDomain.DataModel
{
    public class NinjaContext : DbContext
    {
        // Can specify name of database this way

        public NinjaContext() : base("VNCNinja")
        {
        }

        // Tell DbContext (NinjaContext
        // which collections of objects (DbSet) it contains (are in the model).
        // That allows queries and persistence directly

        #region Enums, Fields, Properties

        public DbSet<Clan> Clans { get; set; }

        public DbSet<NinjaEquipment> Equipment { get; set; }

        public DbSet<Ninja> Ninjas { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Ignore any IsDirty property on any types pulled into model.

            modelBuilder.Types().
                Configure(c => c.Ignore("IsDirty"));
            base.OnModelCreating(modelBuilder);
        }

        // Override SaveChanges so EF will enforce use of IModificationHistory changes.

        public override int SaveChanges()
        {
            foreach (var history in this.ChangeTracker.Entries()
              .Where(e => e.Entity is IModificationHistory 
                && (e.State == EntityState.Added || e.State == EntityState.Modified))
               .Select(e => e.Entity as IModificationHistory)
              )
            {
                history.DateModified = DateTime.Now;

                // Use DateCreated DateTime.MinValue as new flag

                var dt = DateTime.MinValue;
                var dt2 = SqlDateTime.MinValue;
                var hdc = history.DateCreated;

                //if (history.DateCreated == DateTime.MinValue)
                //{      
                //    history.DateCreated = DateTime.Now;
                //}

                if (history.DateCreated == null)
                {
                    history.DateCreated = DateTime.Now;
                }
            }

            // Save changes to database.

            int result = base.SaveChanges();

            foreach (var history in this.ChangeTracker.Entries()
                                          .Where(e => e.Entity is IModificationHistory)
                                          .Select(e => e.Entity as IModificationHistory)
              )
            {
                history.IsDirty = false;
            }

            return result;
        }
    }
}
