using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PunData;
namespace WebServices.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PunService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PunService.svc or PunService.svc.cs at the Solution Explorer and start debugging.
    public class PunService : IPunService
    {
        private PunDataService _service;
        public PunService()
        {
            _service = new PunDataService(@"C:\inetpub\temp\temp\wcf-puns.dat");
        }

        public Pun[] GetPuns()
        {
            return _service.GetPuns();
        }

        public Pun GetPunByID(int id)
        {
            return _service.GetPunById(id);
        }

        public void CreatePun(Pun pun)
        {
            _service.AddPun(pun);
        }

        public void UpdatePun(Pun pun)
        {
            _service.UpdatePun(pun);
        }

        public void DeletePun(int id)
        {
            _service.DeletePun(id);
        }
    }
}
