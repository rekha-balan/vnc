using System;

namespace TeamDiagnostics.TestSource.Before.Design.NonSealedAttribute
{
   public class IndirectAttributeBaseClass
      : InterveningAttributeClass
   {
   }

   public abstract class InterveningAttributeClass
      : Attribute
   {
   }
}
