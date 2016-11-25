using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using PunData;

namespace WebService.ASMX
{
    /// <summary>
    /// Summary description for PunService
    /// </summary>
    [WebService(Namespace = "http://puns.org/", Name ="Pun Service v1.0", Description ="This web service provides CRUD services over a collecton of puns")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PunService : System.Web.Services.WebService
    {
        private PunData.PunDataService _service;

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="PunService"/> class.
        /// </summary>
        public PunService()
        {
            _service = new PunData.PunDataService();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PunService"/> class.
        /// </summary>
        /// <param name="service"></param>
        public PunService(PunData.PunDataService service)
        {
            _service = service;
        }

        [WebMethod]
        public Pun[] GetPuns()
        {
            //var service = new PunDataService();
            return _service.GetPuns();
        }

        [WebMethod]
        public Pun GetPunByID(int PunID)
        {
            //var service = new PunDataService();
            return _service.GetPunById(PunID);
        }

        [WebMethod]
        public void CreatePun(Pun pun)
        {
            _service.AddPun(pun);
        }

        [WebMethod]
        public void EditPun(Pun pun)
        {
            _service.AddPun(pun);
        }

        [WebMethod]
        public void DeletePun(int punID)
        {
            _service.DeletePun(punID);
        }
    }
}
