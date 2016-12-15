using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using PunData;

namespace PunService.AJAX
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PunService
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        //[OperationContract]
        //[WebGet]
        //public void DoWork()
        //{
        //    // Add your operation implementation here
        //    return;
        //}

        // Add more operations here and mark them with [OperationContract]

        private PunDataService _service;

        public PunService()
        {
            _service = new PunDataService();
        }

        [WebGet]
        public Pun[] GetPuns()
        {
            return _service.GetPuns();
        }

        [WebGet]
        public Pun GetPunByID(int punID)
        {
            return _service.GetPunById(punID);
        }

        [WebInvoke]
        public void CreatePun(Pun pun)
        {
            _service.AddPun(pun);
        }

        [WebInvoke]
        public void UpdatePun(Pun pun)
        {
            _service.UpdatePun(pun);
        }

        [WebInvoke]
        public void DeletePun(int id)
        {
            _service.DeletePun(id);
        }
    }
}
