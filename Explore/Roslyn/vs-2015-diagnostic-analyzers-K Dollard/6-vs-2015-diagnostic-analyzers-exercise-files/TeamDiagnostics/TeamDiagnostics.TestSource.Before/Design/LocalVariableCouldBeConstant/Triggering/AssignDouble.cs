namespace TeamAnalyzers.TestSource.Before.Design.LocalVariableCouldBeConstant.Triggering
{
   public class AssignDouble
   {
      public void Foo()
      {
         const double c_E = 4.2;
         const double c_F = 4.3;
         const double c_G = 4.4;
         const double c_H = 4.5;

         double a = 4.6;
         double b = 4.7, c = 4.8;
         var d = 4.9;
         double e = c_E;
         double f = c_F, g = c_G;
         var h = c_H;
         double i = default(double);
         double j = default(double), k = default(double);
         var m = default(double);

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
