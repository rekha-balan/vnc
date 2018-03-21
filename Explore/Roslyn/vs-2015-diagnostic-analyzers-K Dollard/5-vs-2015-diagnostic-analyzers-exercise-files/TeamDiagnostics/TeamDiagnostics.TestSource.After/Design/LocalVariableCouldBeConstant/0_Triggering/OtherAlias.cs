﻿using System;

namespace TeamAnalyzers.TestSource.After.Design.LocalVariableCouldBeConstant.Triggering
{

   namespace AliasObject
   {
      using myAlias = System.Object;

      public class OtherTriggering
      {

         public void Foo()
         {
            const myAlias b = null;

            myAlias b1; b1 = b;

         }
      }

      namespace AliasInt
      {
         using myAlias = Int32;

         public class OtherTriggering
         {

            public void Foo()
            {
               const myAlias b = default(myAlias);

               myAlias b1; b1 = b;

            }
         }
      }

      namespace GlobalNamespace
      {
         public class OtherTriggering
         {

            public void Foo()
            {
               const global::System.Object b = null;
               const global::System.Int32 c = default(int);

               object b1; b1 = b;
               int c1; c1 = c;
            }
         }
      }
   }
}
