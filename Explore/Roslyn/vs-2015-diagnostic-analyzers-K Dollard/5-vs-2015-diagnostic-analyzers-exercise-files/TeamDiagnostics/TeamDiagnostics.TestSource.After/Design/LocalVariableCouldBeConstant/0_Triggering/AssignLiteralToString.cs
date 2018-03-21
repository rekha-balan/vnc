namespace TeamAnalyzers.TestSource.After.Design.LocalVariableCouldBeConstant.Triggering
{
   public class AssignLiteralToString
   {
      public void Foo()
      {
         const string c_E = "E";
         const string c_F = "F";
         const string c_G = "G";
         const string c_H = "H";

         const string a = "A";
         const string b = "B", c = "C";
         const string d = "D";
         const string e = c_E;
         const string f = c_F, g = c_G;
         const string h = c_H;
         const string i = default(string);
         const string j = default(string), k = default(string);
         const string m = default(string);

         string a1; a1 = a;
         string b1; b1 = b;
         string c1; c1 = c;
         string d1; d1 = d;
         string e1; e1 = e;
         string f1; f1 = f;
         string g1; g1 = g;
         string h1; h1 = h;
         string i1; i1 = i;
         string j1; j1 = j;
         string k1; k1 = k;
         string m1; m1 = m;
      }
   }
}
