namespace TeamAnalyzers.TestSource.Before.Design.LocalVariableCouldBeConstant.NonTriggering
{
   public class AssignChar
   {
      public void Foo()
      {
         const char c_E = 'E';
         const char c_F = 'F';
         const char c_G = 'G';
         const char c_H = 'H';

         char a = 'A';
         char b = 'B', c = 'C';
         var d = 'D';
         char e = c_E;
         char f = c_F, g = c_G;
         var h = c_H;
         char i = default(char);
         char j = default(char), k = default(char);
         var m = default(char);

         char a1; a1 = a;
         char b1; b1 = b;
         char c1; c1 = c;
         char d1; d1 = d;
         char e1; e1 = e;
         char f1; f1 = f;
         char g1; g1 = g;
         char h1; h1 = h;
         char i1; i1 = i;
         char j1; j1 = j;
         char k1; k1 = k;

         a = b;
         b = c;
         c = d;
         d = e;
         e = f;
         f = g;
         g = h;
         h = i;
         i = j;
         j = k;
         k = m;
         m = a;
      }
   }
}
