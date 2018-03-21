namespace TeamAnalyzers.TestSource.After.Design.LocalVariableCouldBeConstant.Triggering
{
   public class AssignNullToString
   {
      public void Foo()
      {
         const string c_E = null;
         const string c_F = null;
         const string c_G = null;
         const string c_H = null;

         const string a = null;
         const string b = null, c = null;
         const string e = c_E;
         const string f = c_F, g = c_G;
         const string h = c_H;

         string a1; a1 = a;
         string b1; b1 = b;
         string c1; c1 = c;
         string d1; d1 = e;
         string f1; f1 = f;
         string g1; g1 = g;
         string h1; h1 = h;
      }
   }
}
