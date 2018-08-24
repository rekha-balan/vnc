using System.Collections.Generic;
using System.Threading.Tasks;
using AMLLinesEDMXCodeFirst;

namespace LineStatusViewer.Data
{
    public interface ILineStatusDataService
    {
        IEnumerable<AML_LineStatus> GetAll();

        Task<IEnumerable<AML_LineStatus>> GetAllAsync();

        Task<AML_LineStatus> GetByBuildNoAsync(string buildNo);
    }
}
