namespace TeamAnalyzers.TestSource.After.Design.LocalVariableCouldBeConstant.Triggering
{
   class AssignInteger
   {
      public void Foo()
      {
         const int c_E = 42;
         const int c_F = 43;
         const int c_G = 44;
         const int c_H = 45;

         const int a = 46;
         const int b = 47, c = 48;
         const int d = 49;
         const int e = c_E;
         const int f = c_F, g = c_G;
         const int h = c_H;
         const int i = default(int);
         const int j = default(int), k = default(int);
         const int m = default(int);

         int a1; a1 = a;
         int b1; b1 = b;
         int c1; c1 = c;
         int d1; d1 = d;
         int e1; e1 = e;
         int f1; f1 = f;
         int g1; g1 = g;
         int h1; h1 = h;
         int i1; i1 = i;
         int j1; j1 = j;
         int k1; k1 = k;
         int m1; m1 = m;
      }
   }
}
