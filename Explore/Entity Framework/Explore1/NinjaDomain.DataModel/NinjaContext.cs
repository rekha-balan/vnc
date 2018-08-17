using NinjaDomain.Classes;
using System.Data.Entity;

namespace NinjaDomain.DataModel
{
    public class NinjaContext : DbContext
    {
        // Can specify name of database this way

        public NinjaContext() : base("VNC")
        {
        }
        // Tell DbContext (NinjaContext
        // which collections of objects (DbSet) it contains (are in the model).
        // That allows queries and persistance directly

        #region Enums, Fields, Properties

        public DbSet<Clan> Clans { get; set; }

        public DbSet<NinjaEquipment> Equipment { get; set; }

        public DbSet<Ninja> Ninjas { get; set; }

        #endregion

    }
}
