using System;
using System.Collections.Generic;
using System.Linq;
//using PunClient.Transports;
using PunClient.PunWebService;

namespace PunClient.DataServices
{
    class PunDataService
    {
        private PunServicev10SoapClient _client;

        public PunDataService()
        {
            _client = new PunServicev10SoapClient();
        }

        public PunWebService.Pun[] GetPuns()
        {
            return _client.GetPuns();
        }

        public Pun GetPunByID(int punID)
        {
            return _client.GetPunByID(punID);
        }

        public void CreatePun(PunWebService.Pun pun)
        {
            _client.CreatePun(pun);
        }

        public void UpdatePun(PunWebService.Pun pun)
        {
            _client.EditPun(pun);
        }

        public void DeletePun(int punID)
        {
            _client.DeletePun(punID);
        }

    }
}
