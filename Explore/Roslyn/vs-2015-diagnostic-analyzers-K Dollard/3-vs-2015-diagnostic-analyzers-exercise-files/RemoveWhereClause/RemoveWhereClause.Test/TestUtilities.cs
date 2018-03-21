using System.IO;

namespace RemoveWhereClause.Test
{
   public static class TestUtilities
   {
      private static string beforeProjectLocationSimple = "{0}.TestSource.Before";
      private static string beforeProjectLocationWithModifier = "{0}.TestSource.{1}.Before";
      private static string afterProjectLocationSimple = "{0}.TestSource.After";
      private static string afterProjectLocationWithModifier = "{0}.TestSource.{1}.After";

      public static string GetBeforeSource(string fileName, string projectModifier)
      {
         return Path.Combine(GetBeforeSourceRoot(projectModifier), fileName);
      }

      public static string GetBeforeSource(string fileName)
      {
         return GetBeforeSource(fileName, null);
      }

      public static string GetSolutionRoot()
      {
         var currentDirectory = Directory.GetCurrentDirectory();
         var solutionDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
         return solutionDirectory;
      }

      internal static string GetAfterSource(string fileName)
      {
         return GetAfterSource(fileName, null);
      }

      public static string GetAfterSource(string fileName, string projectModifier)
      {
         return Path.Combine(GetAfterSourceRoot(projectModifier), fileName);
      }

      public static string GetBeforeSourceRoot(string projectModifier)
      {
         var solutionRoot = GetSolutionRoot();
         var simpleRoot = Path.GetFileName(solutionRoot);
         var format = string.IsNullOrWhiteSpace(projectModifier)
                        ? beforeProjectLocationSimple
                        : beforeProjectLocationSimple;

         return Path.Combine(solutionRoot, string.Format(format, simpleRoot));
      }

      public static string GetBeforeSourceRoot()
      {
         return GetBeforeSourceRoot(null);
      }

      public static string GetAfterSourceRoot(string projectModifier)
      {
         var solutionRoot = GetSolutionRoot();
         var simpleRoot = Path.GetFileName(solutionRoot);
         var format = string.IsNullOrWhiteSpace(projectModifier)
                        ? afterProjectLocationSimple
                        : afterProjectLocationWithModifier;

         return Path.Combine(solutionRoot, string.Format(format, simpleRoot));
      }

      public static string GetAfterSourceRoot()
      {
         return GetAfterSourceRoot(null);
      }

   }
}
