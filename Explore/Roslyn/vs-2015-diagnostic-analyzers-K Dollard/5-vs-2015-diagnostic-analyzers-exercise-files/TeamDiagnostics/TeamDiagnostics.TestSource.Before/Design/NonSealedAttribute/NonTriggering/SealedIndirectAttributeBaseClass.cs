using System;

namespace TeamDiagnostics.TestSource.Before.Design.NonSealedAttribute
{
   public sealed class SealedIndirectAttributeBaseClass
      : InterveningAttributeClass
   {
   }

   public abstract class SealedInterveningAttributeClass
      : Attribute 
   {
   }
}
