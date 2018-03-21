namespace TeamAnalyzers.TestSource.After.Design.LocalVariableCouldBeConstant.Triggering
{
   public class AssignDouble
   {
      public void Foo()
      {
         const double c_E = 4.2;
         const double c_F = 4.3;
         const double c_G = 4.4;
         const double c_H = 4.5;

         const double a = 4.6;
         const double b = 4.7, c = 4.8;
         const double d = 4.9;
         const double e = c_E;
         const double f = c_F, g = c_G;
         const double h = c_H;
         const double i = default(double);
         const double j = default(double), k = default(double);
         const double m = default(double);

         double a1; a1 = a;
         double b1; b1 = b;
         double c1; c1 = c;
         double d1; d1 = d;
         double e1; e1 = e;
         double f1; f1 = f;
         double g1; g1 = g;
         double h1; h1 = h;
         double i1; i1 = i;
         double j1; j1 = j;
         double k1; k1 = k;
         double m1; m1 = m;
      }
   }
}
