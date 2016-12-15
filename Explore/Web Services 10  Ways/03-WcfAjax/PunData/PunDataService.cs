using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace PunData
{
    public class PunDataService
    {
        private string _punDataFile;

        private List<Pun> Puns { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PunDataService"/> class.
        /// </summary>
        /// <param name="punDataFile"></param>
        public PunDataService(string punDataFile)
        {
            _punDataFile = punDataFile;

            if (File.Exists(_punDataFile))
            {
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(_punDataFile, FileMode.Open, FileAccess.Read, FileShare.Read))
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

        public PunDataService() : this(@"C:\inetpub\temp\temp\puns.dat")
        {
            //if (File.Exists(_punDataFile))
            //{
            //    var formatter = new BinaryFormatter();
            //    using (var stream = new FileStream(_punDataFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            //    {
            //        this.Puns = (List<Pun>)formatter.Deserialize(stream);
            //    }
            //}
            //else
            //{
            //    this.Puns = new List<Pun>();
            //    SeedPuns();
            //}
        }

        private void Save()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(_punDataFile, FileMode.Create, FileAccess.Write))
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