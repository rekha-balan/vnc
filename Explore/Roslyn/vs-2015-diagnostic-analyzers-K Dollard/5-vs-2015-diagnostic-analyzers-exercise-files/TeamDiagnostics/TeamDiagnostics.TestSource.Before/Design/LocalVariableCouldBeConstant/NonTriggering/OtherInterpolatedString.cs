namespace TeamAnalyzers.TestSource.Before.Design.LocalVariableCouldBeConstant.NonTriggering
{
   public class OtherInterpolatedString
   {
      public void Foo()
      {
         const string c_E = "E";
         const string c_F = "F";
         const string c_G = "G";

         string a = $"A{c_E}";
         string b = $"B", c = "C";
         var d = $"D";
         string e = $"{c_E}";
         string f = c_F, g = $"{c_G}";

      }
   }
}
