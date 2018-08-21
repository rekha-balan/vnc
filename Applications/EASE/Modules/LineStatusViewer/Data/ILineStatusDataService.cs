using System.Collections.Generic;
using AMLLinesEDMXCodeFirst;

namespace LineStatusViewer.Data
{
    public interface ILineStatusDataService
    {
        IEnumerable<AML_LineStatus> GetAll();
    }
}
