using System.Collections.Generic;
using AMLLinesEDMXCodeFirst;

namespace LineStatusViewer.Data
{
    public class LineStatusDataService : ILineStatusDataService
    {
        public IEnumerable<AML_LineStatus> GetAll()
        {
            //// TODO(crhodes)
            //// Load data from real database.
            //// For now just return hard coded list.
            yield return new AML_LineStatus { LineID = 1, StationNO = "0012", BuildNo = "12345" };
            yield return new AML_LineStatus { LineID = 1, StationNO = "0014", BuildNo = "23456" };
            yield return new AML_LineStatus { LineID = 1, StationNO = "0016", BuildNo = "34567" };
            yield return new AML_LineStatus { LineID = 2, StationNO = "0022", BuildNo = "45678" };
            yield return new AML_LineStatus { LineID = 2, StationNO = "0028", BuildNo = "56789" };
        }
    }
}
