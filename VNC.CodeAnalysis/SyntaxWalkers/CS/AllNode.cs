﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace VNC.CodeAnalysis.SyntaxWalkers.CS
{
    public class AllNode : VNCCSSyntaxWalkerBase
    {
        public AllNode() : base(SyntaxWalkerDepth.Node)
        {
            
        }
    }
}
