using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaDomain.Classes;
using NinjaDomain.DataModel;

namespace ConsoleExploreEF
{
    class Program
    {
        static void Main(string[] args)
        {
            // Have EF skip DB 
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            Console.WriteLine("InsertNinja");
            //InsertNinja();
            //InsertMultipleNinjas();
            //SimpleNinjaQueries();
            //QueryAndUpdateNinja();
            //DeleteNinja();
            //RetrieveDataWithFind();
            //RetrieveDataWithStoredProc();
            //DeleteNinjaWithKeyValue();
            //DeleteNinjaViaStoredProcedure();
            //QueryAndUpdateNinjaDisconnected();

            //InsertNinjaWithEquipment();
            //SimpleNinjaGraphQuery();
            //ProjectionQuery();
            //QueryAndUpdateNinjaDisconnected();

            //ReseedDatabase();

            DataHelpers.NewDbWithSeed();

            Console.ReadKey();
        }


        private static void InsertNinja()
        {
            // Create a Ninja

            var ninja = new Ninja
            {
                Name = "SampsonNew",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(2000, 11, 28),
                DateOfDeath = null,
                ClanId = 1

            };

            // And use EF to insert it.

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Add(ninja);

                //context.Database.Log = s =>
                //{
                //    Debug.Print(s);
                //};
                context.SaveChanges();
            }
        }

        private static void InsertMultipleNinjas()
        {
            var ninja1 = new Ninja
            {
                Name = "Leonardo",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 1, 1),
                ClanId = 1
            };
            var ninja2 = new Ninja
            {
                Name = "Raphael",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1985, 1, 1),
                ClanId = 1
            };
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.AddRange(new List<Ninja> { ninja1, ninja2 });
                context.SaveChanges();
            }
        }

        private static void SimpleNinjaQueries()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninjas = context.Ninjas
                    .Where(n => n.DateOfBirth >= new DateTime(1984, 1, 1))
                    .OrderBy(n => n.Name)
                    .Skip(1).Take(1);

                foreach (var ninja in ninjas)
                {
                    Console.WriteLine(ninja.Name);
                }

                //var query = context.Ninjas;

                //foreach (var ninja in query)
                //{
                //    Console.WriteLine(ninja.Name);
                //}

                //var ninjas = query.ToList();

                //foreach (var ninja in ninjas)
                //{
                //    Console.WriteLine(ninja.Name);
                //}

                //foreach (var ninja in context.Ninjas)
                //{
                //    Console.WriteLine(ninja.Name);
                //}
            }
        }

        private static void QueryAndUpdateNinja()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);
                context.SaveChanges();
            }
        }

        private static void QueryAndUpdateNinjaDisconnected()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            Ninja ninja;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
            }

            ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Attach(ninja);
                context.Entry(ninja).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static void RetrieveDataWithFind()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            var keyval = 4;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyval);
                Console.WriteLine("After Find#1:" + ninja.Name);

                var someNinja = context.Ninjas.Find(keyval);
                Console.WriteLine("After Find#2:" + someNinja.Name);
                ninja = null;
            }
        }

        private static void RetrieveDataWithStoredProc()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas.SqlQuery("exec GetOldNinjas").ToList();
                //foreach (var ninja in ninjas)
                //{
                //    Console.WriteLine(ninja.Name);
                //}
            }
        }

        private static void DeleteNinja()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            Ninja ninja;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
                //context.Ninjas.Remove(ninja);
                //context.SaveChanges();
            }
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                //context.Ninjas.Attach(ninja);
                //context.Ninjas.Remove(ninja);
                context.Entry(ninja).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        private static void DeleteNinjaWithKeyValue()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            var keyval = 1;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyval);
                context.Ninjas.Remove(ninja);
                context.SaveChanges();
            }
        }

        private static void DeleteNinjaViaStoredProcedure()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            var keyval = 3;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Database.ExecuteSqlCommand(
                    "exec DeleteNinjaViaId {0}", keyval);
            }
        }

        private static void InsertNinjaWithEquipment()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = new Ninja
                {
                    Name = "Kacy Catanzaro",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1990, 1, 14),
                    ClanId = 1
                };
                var muscles = new NinjaEquipment
                {
                    Name = "Muscles",
                    Type = EquipmentType.Tool,

                };
                var spunk = new NinjaEquipment
                {
                    Name = "Spunk",
                    Type = EquipmentType.Weapon
                };

                ninja.EquipmentOwned.Add(muscles);
                ninja.EquipmentOwned.Add(spunk);
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }

        }

        private static void SimpleNinjaGraphQuery()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                //var ninjas = context.Ninjas.Include(n => n.EquipmentOwned)
                //    .FirstOrDefault(n => n.Name.StartsWith("Kacy"));

                var ninja = context.Ninjas
                           .FirstOrDefault(n => n.Name.StartsWith("Kacy"));
                Console.WriteLine("Ninja Retrieved:" + ninja.Name);
                context.Entry(ninja).Collection(n => n.EquipmentOwned).Load();
            }
        }

        private static void ProjectionQuery()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas
                    .Select(n => new { n.Name, n.DateOfBirth, n.EquipmentOwned })
                    .ToList();

            }
        }

        private static void ReseedDatabase()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<NinjaContext>());
            using (var context = new NinjaContext())
            {
                context.Clans.Add(new Clan { ClanName = "Vermont Clan" });
                var j = new Ninja
                {
                    Name = "JulieSan",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1980, 1, 1),
                    ClanId = 1

                };
                var s = new Ninja
                {
                    Name = "SampsonSan",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(2008, 1, 28),
                    ClanId = 1

                };
                var l = new Ninja
                {
                    Name = "Leonardo",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1984, 1, 1),
                    ClanId = 1
                };
                var r = new Ninja
                {
                    Name = "Raphael",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1985, 1, 1),
                    ClanId = 1
                };
                context.Ninjas.AddRange(new List<Ninja> { j, s, l, r });
                context.SaveChanges();
                context.Database.ExecuteSqlCommand(
                  @"CREATE PROCEDURE GetOldNinjas
                    AS  SELECT * FROM Ninjas WHERE DateOfBirth<='1/1/1980'");

                context.Database.ExecuteSqlCommand(
                   @"CREATE PROCEDURE DeleteNinjaViaId
                     @Id int
                     AS
                     DELETE from Ninjas Where Id = @id
                     RETURN @@rowcount");
            }

        }
    }
}
