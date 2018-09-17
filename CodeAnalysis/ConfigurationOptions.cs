using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

namespace VNC.CodeAnalysis
{
    public enum InfoType : Int16
    {
        Names = 0,
        DataType = 1
    }

    public class ConfigurationOptions
    {
        public SyntaxWalkerDepth SyntaxWalkerDepth { get; set; } = SyntaxWalkerDepth.StructuredTrivia;
        #region Output Options

        public SyntaxNode.AdditionalNodes AdditionalNodeAnalysis { get; set; } = SyntaxNode.AdditionalNodes.None;

        public bool DisplayNodeKind { get; set; } = false;

        public bool DisplayNodeValue { get; set; } = false;

        public bool DisplayFormattedOutput { get; set; } = false;

        public bool DisplayNodeParent { get; set; } = false;

        public bool DisplayStatementBlock { get; set; } = false;

        public bool IncludeStatementBlockInCRC { get; set; } = false;

        public bool SourceLocation { get; set; } = false;

        public bool CRC32 { get; set; } = false;

        public bool ShowAnalysisCRC { get; set; } = false;

        public bool ReplaceCRLF { get; set; } = false;

        public bool ClassOrModuleName { get; set; } = false;

        public bool MethodName { get; set; } = false;

         public bool ContainingMethodBlock { get; set; }    
        
        public bool ContainingBlock { get; set; }

        public bool InTryBlock { get; set; } = false;

        public bool InWhileBlock { get; set; } = false;

        public bool InForBlock { get; set; } = false;

        public bool InIfBlock { get; set; } = false;


        public bool AllTypes { get; set; } = false;


        public bool Boolean { get; set; } = false;

        public bool Byte { get; set; } = false;

        public bool DataTable { get; set; } = false;

        public bool Date { get; set; } = false;

        public bool DateTime { get; set; } = false;

        public bool Int16 { get; set; } = false;

        public bool Int32 { get; set; } = false;

        public bool Int64 { get; set; } = false;

        public bool Integer { get; set; } = false;

        public bool Long { get; set; } = false;

        public bool Single { get; set; } = false;

        public bool String { get; set; } = false;

        public bool OtherTypes { get; set; } = false;

        #endregion

        #region Rewriter Options

        public bool AddFileSuffix { get; set; } = false;

        public string FileSuffix { get; set; } = "xxx";

        #endregion

    }
}
