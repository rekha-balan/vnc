using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationWithRoslyn
{
    public class EnumHelper
    {
        public static IList<EnumTypeItem> GetEnumData(Database database)
        {
            if (database == null)
            {
                throw new Exception("VNCDB database not found");
            }

            var sql = @"
SELECT
    LoanCodeTypeId,
    ShortDescription,
    LongDescription,
    IsRange
FROM dbo.LoanCodeType";

            var data = database.ExecuteWithResults(sql);
            var table = data.Tables[0];
            var enumTypes = new List<EnumTypeItem>();

            foreach (DataRow record in table.Rows)
            {
                var item = (new EnumTypeItem
                {
                    ID = (int)record[0],
                    ShortDescription = (string)record[1],
                    LongDescription = (string)record[2],
                    IsRange = (bool)record[3],
                    Details = new List<EnumTypeDetailItem>()
                });


            sql = @"
SELECT
    LoanCodeID,
    LoanCodeTypeID,
    ShortDescription,
    ISNULL(LongDescription, '')
FROM dbo.LoadCode
WHERE LoanCodeTypeID = " + record[0].ToString();

                var details = database.ExecuteWithResults(sql);

                foreach (DataRow row in details.Tables[0].Rows)
                {
                    item.Details.Add(new EnumTypeDetailItem
                    {
                        LoanCodeID = (int)row[0],
                        LoanCodeTypeID = item.ID,
                        LongDescription = (string)row[3],
                        ShortDescription = (string)row[2]
                    });
                }

                enumTypes.Add(item);
            }

            return enumTypes;
        }
    }
}
