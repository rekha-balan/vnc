using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDiagnostics
{
   public static class DiagnosticIdExtensions
   {
      public static string ToDiagnosticId(this KadGenDiagnosticId diagnosticId)
                  => $"KADGEN{(int)diagnosticId:D3}";
   }

   public enum KadGenDiagnosticId
   {
      NotYetSet = 0,
      EmptyCatchClause = 1,
      NonSealedAttribute = 2,
      LocalVariableCouldBeConstant = 3
   }
}
