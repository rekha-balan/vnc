using System;

namespace TeamDiagnostics.TestSource.After.Design.NonSealedAttribute
{
   public sealed class IndirectAttributeBaseClass
      : InterveningAttributeClass
   {
   }

   public abstract class InterveningAttributeClass
      : Attribute
   {
   }
}
