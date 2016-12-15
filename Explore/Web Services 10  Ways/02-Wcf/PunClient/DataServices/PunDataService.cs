using System;
using System.Collections.Generic;
using System.Linq;
//using PunClient.Transports;
using PunClient.WCFPunService;

namespace PunClient.DataServices
{
    class PunDataService
    {
        private PunServiceClient _client;
        public PunDataService()
        {
            _client = new WCFPunService.PunServiceClient();
        }

        public Pun[] GetPuns()
        {
            return _client.GetPuns();
        }

        public Pun GetPunByID(int punID)
        {
            return _client.GetPunByID(punID);
        }

        public void CreatePun(Pun pun)
        {
            _client.CreatePun(pun);
        }

        public void UpdatePun(Pun pun)
        {
            _client.UpdatePun(pun);
        }

        public void DeletePun(int punID)
        {
            _client.DeletePun(punID);
        }
    }
}
