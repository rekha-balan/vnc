using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Data
{
    public class PunDataService
    {
        private List<Pun> Puns { get; set; }

        public PunDataService()
        {
            if (File.Exists("./puns.dat"))
            {
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream("./puns.dat", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    this.Puns = (List<Pun>)formatter.Deserialize(stream);
                }
            }
            else
            {
                this.Puns = new List<Pun>();
                SeedPuns();
            }
        }

        public void Save()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream("./puns.dat", FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, this.Puns);
            }
        }

        private void SeedPuns()
        {
            var pun = new Pun
            {
                PunID = 1,
                Title = "Lazy Bike",
                Joke = "Why can't a bike stand up on its own?  It's two tired!"
            };

            this.Puns.Add(pun);
            this.Puns.Add(new Pun
                {
                    PunID = 2,
                    Title = "Catch!",
                    Joke = "I wondered why the baseball was getting bigger.  Then it hit me."
                });

            this.Puns.Add(new Pun
                {
                    PunID = 3,
                    Title = "Right Handed",
                    Joke = "Did you hear about the guy who got his left side cut off?  He's all right now!"
                });

            this.Puns.Add(new Pun
                {
                    PunID = 4,
                    Title = "Best Seller",
                    Joke = "I'm reading a book about anti-gravity.  It's impossible to put down!"
                });
            Save();
        }

        public Pun[] GetPuns()
        {
            return this.Puns.ToArray();
        }

        public Pun GetPunById(int id)
        {
            return this.Puns.SingleOrDefault(p => p.PunID == id);
        }

        public void UpdatePun(Pun pun)
        {
            var found = this.Puns.SingleOrDefault(p => p.PunID == pun.PunID);
            if (found != null)
            {
                this.Puns.Remove(found);
                this.Puns.Add(pun);
                Save();
            }
        }

        public void AddPun(Pun pun)
        {
            var lastID = this.Puns.Max(p => p.PunID);
            pun.PunID = lastID + 1;
            this.Puns.Add(pun);
            Save();
        }

        public void DeletePun(int id)
        {
            var pun = this.Puns.SingleOrDefault(p => p.PunID == id);
            this.Puns.Remove(pun);
            Save();
        }
    }
}