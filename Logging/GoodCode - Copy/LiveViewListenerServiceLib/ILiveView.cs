using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace LiveViewListenerServiceLib
{
  [ServiceContract(Namespace = "http://MyCompany.com")]
  public interface ILiveView
  {
    [OperationContract]
    void DisplayLogEntry(string message);
  }
}
