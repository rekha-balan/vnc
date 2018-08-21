using System;
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
            yield return new AML_LineStatus {LineID = 1,
                StationNO = "0012",
                BuildNo = "12345",
                EngineMoveDate = DateTime.Now,
                AndonCALL = 0, 
                IPCStatus = 0, 
                ReadStatus = 0 };
            yield return new AML_LineStatus { LineID = 1, 
                StationNO = "0014", 
                BuildNo = "23456", 
                EngineMoveDate = DateTime.Now, 
                AndonCALL = 0, 
                IPCStatus = 0, 
                ReadStatus = 1 };
            yield return new AML_LineStatus { LineID = 1, 
                StationNO = "0016", 
                BuildNo = "34567", 
                EngineMoveDate = DateTime.Now, 
                AndonCALL = 0, 
                IPCStatus = 0, 
                ReadStatus = 0 };
            yield return new AML_LineStatus {
                LineID = 2,
                StationNO = "0022",
                BuildNo = "45678",
                EngineMoveDate = DateTime.Now,
                AndonCALL = 1,
                IPCStatus = 0,
                ReadStatus = 0
            };
            yield return new AML_LineStatus {
                LineID = 2,
                StationNO = "0028",
                BuildNo = "56789",
                EngineMoveDate = DateTime.Now,
                AndonCALL = 0,
                IPCStatus = 0,
                ReadStatus = 0
            };
        }
    }
}
