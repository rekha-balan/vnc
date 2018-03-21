namespace TeamAnalyzers.TestSource.Before.Design.LocalVariableCouldBeConstant.Triggering
{
   public class AssignNullToReferenceType
   {
      public void Foo()
      {
         const object c_E = null;
         const object c_F = null;
         const object c_G = null;
         const object c_H = null;

         object a = null;
         object b = null, c = null;
         object e = c_E;
         object f = c_F, g = c_G;
         var h = c_H;
         object i = default(object);
         object j = default(object), k = default(object);
         var m = default(object);

         object a1; a1 = a;
         object b1; b1 = b;
         object c1; c1 = c;
         object d1; d1 = e;
         object f1; f1 = f;
         object g1; g1 = g;
         object h1; h1 = h;
         object i1; i1 = i;
         object j1; j1 = j;
         object k1; k1 = k;
         object m1; m1 = m;
      }
   }
}
