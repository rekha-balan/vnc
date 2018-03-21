namespace TeamAnalyzers.TestSource.After.Design.LocalVariableCouldBeConstant.Triggering
{
   public class AssignChar
   {
      public void Foo()
      {
         const char c_E = 'E';
         const char c_F = 'F';
         const char c_G = 'G';
         const char c_H = 'H';

         const char a = 'A';
         const char b = 'B',
              c = 'C';
         const char d = 'D';
         const char e = c_E;
         const char f = c_F,
              g = c_G;
         const char h = c_H;
         const char i = default(char);
         const char j = default(char), k = default(char);
         const char m = default(char);

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
         char m1; m1 = m;

      }
   }
}
