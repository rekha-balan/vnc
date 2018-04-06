using System;
using System.ComponentModel;
using System.Management.Automation;

/*
To build and install:

1) 
Set-Alias csc $env:WINDIR\Microsoft.NET\Framework\v2.0.50727\csc.exe

2) 
Set-Alias installutil `
   $env:WINDIR\Microsoft.NET\Framework\v2.0.50727\installutil.exe
3)
$ref = [PsObject].Assembly.Location
csc /out:TemplateSnapin.dll /t:library InvokeTemplateCmdletCommand.cs /r:$ref

4)
installutil TemplateSnapin.dll

5)
Add-PSSnapin TemplateSnapin

To run:

PS >Invoke-TemplateCmdlet

To uninstall:

installutil /u TemplateSnapin.dll
 
*/

namespace Template.Commands
{
    [Cmdlet("Invoke", "TemplateCmdlet")]
    public class InvokeTemplateCmdletCommand : Cmdlet
    {
        [Parameter(Mandatory=true, Position=0, ValueFromPipeline=true)]
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }
        private string text;

        protected override void BeginProcessing()
        {
            WriteObject("Processing Started");
        }

        protected override void ProcessRecord()
        {
            WriteObject("Processing " + text);
        }

        protected override void EndProcessing()
        {
            WriteObject("Processing Complete.");
        }
    }

    [RunInstaller(true)]
    public class TemplateSnapin : PSSnapIn
    {
        public TemplateSnapin()
            : base()
        {
        }

        ///<summary>The snapin name which is used for registration</summary>
        public override string Name
        {
            get
            {
                return "TemplateSnapin";
            }
        }
        /// <summary>Gets vendor of the snapin.</summary>
        public override string Vendor
        {
            get
            {
                return "Template Vendor";
            }
        }
        /// <summary>Gets description of the snapin. </summary>
        public override string Description
        {
            get
            {
                return "This is a snapin that provides a template cmdlet.";
            }
        }
    }
}
