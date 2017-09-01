//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Symbols;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
//using Microsoft.CodeAnalysis.Text;


//namespace TransformationCS
//{
//    public class TypeInferenceRewriter : CSharpSyntaxRewriter
//    {
//        private readonly SemanticModel SemanticModel;

//        public TypeInferenceRewriter(SemanticModel semanticModel)
//        {
//            this.SemanticModel = semanticModel;
//        }

//        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
//        {
//            // Don't support this
//            // Type variable1 = expression1, variable2 = expression2

//            if (node.Declaration.Variables.Count > 1)
//            {
//                return node;
//            }

//            // No initializer
//            // Type variable;

//            if (node.Declaration.Variables[0].Initializer == null)
//            {
//                return node;
//            }

//            var declarator = node.Declaration.Variables.First();
//            var variableTypeName = node.Declaration.Type;

//            var variableType = (ITypeSymbol)SemanticModel.GetSymbolInfo(variableTypeName).Symbol;

//            var initializerInfo = SemanticModel.GetTypeInfo(declarator.Initializer.Value);

//            if (variableType == initializerInfo.Type)
//            {
//                TypeSyntax varTypeName =
//                    IdentifierName("var")
//                        .WithLeadingTrivia(variableTypeName.GetLeadingTrivia())
//                        .WithTrailingTrivia(variableTypeName.GetTrailingTrivia());

//                return node.ReplaceNode(variableTypeName, varTypeName);
//            }
//            else
//            {
//                return node;
//            }
//        }
//    }
//}
