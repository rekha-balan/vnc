using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AMLLinesEDMXCodeFirst;

namespace LineStatusViewer.Data
{
    public class LineStatusDataService : ILineStatusDataService
    {
        Func<AMLLinesCF> _contextCreator;

        // TODO(crhodes)
        // Not sure how Unity resolves this constructor parameter.  In the example
        // using AutoFac, had to register in bootstrapper.  Need to research this.
        // It magically is working, however.  :)

        public LineStatusDataService(Func<AMLLinesCF> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public IEnumerable<AML_LineStatus> GetAll()
        {
            using (var ctx = _contextCreator())
            {
                return ctx.AML_LineStatus.AsNoTracking().ToList();
            }
        }

        public async Task<IEnumerable<AML_LineStatus>> GetAllAsync()
        {
            using (var ctx = _contextCreator())
            {
                // Await result so ctx doesn't get disposed before ToListAsync returns

                return await ctx.AML_LineStatus.AsNoTracking().ToListAsync();

                //// Demonstrate UI remains responsive

                //var friends = await ctx.Friends.AsNoTracking().ToListAsync();

                //// See that can move window around
                //await Task.Delay(5000);

                //// And then friends show up.
                //return friends;
            }
        }

        public async Task<AML_LineStatus> GetByBuildNoAsync(string buildNo)
        {
            using (var ctx = _contextCreator())
            {
                // Await result so ctx doesn't get disposed before ToListAsync returns

                return await ctx.AML_LineStatus.AsNoTracking().SingleAsync(f => f.BuildNo == buildNo);

                //// Demonstrate UI remains responsive

                //var friends = await ctx.Friends.AsNoTracking().ToListAsync();

                //// See that can move window around
                //await Task.Delay(5000);

                //// And then friends show up.
                //return friends;
            }
        }

        //public IEnumerable<AML_LineStatus> GetAll()
        //{
        //    //// TODO(crhodes)
        //    //// Load data from real database.
        //    //// For now just return hard coded list.
        //    yield return new AML_LineStatus {
        //        LineID = 1,
        //        StationNO = "0012",
        //        BuildNo = "12345",
        //        EngineMoveDate = DateTime.Now,
        //        AndonCALL = 0, 
        //        IPCStatus = 0, 
        //        ReadStatus = 0 };
        //    yield return new AML_LineStatus {
        //        LineID = 1, 
        //        StationNO = "0014", 
        //        BuildNo = "23456", 
        //        EngineMoveDate = DateTime.Now, 
        //        AndonCALL = 0, 
        //        IPCStatus = 0, 
        //        ReadStatus = 1 };
        //    yield return new AML_LineStatus { LineID = 1, 
        //        StationNO = "0016", 
        //        BuildNo = "34567", 
        //        EngineMoveDate = DateTime.Now, 
        //        AndonCALL = 0, 
        //        IPCStatus = 0, 
        //        ReadStatus = 0 };
        //    yield return new AML_LineStatus {
        //        LineID = 2,
        //        StationNO = "0022",
        //        BuildNo = "45678",
        //        EngineMoveDate = DateTime.Now,
        //        AndonCALL = 1,
        //        IPCStatus = 0,
        //        ReadStatus = 0
        //    };
        //    yield return new AML_LineStatus {
        //        LineID = 2,
        //        StationNO = "0028",
        //        BuildNo = "56789",
        //        EngineMoveDate = DateTime.Now,
        //        AndonCALL = 0,
        //        IPCStatus = 0,
        //        ReadStatus = 0
        //    };
        //}
    }
}
