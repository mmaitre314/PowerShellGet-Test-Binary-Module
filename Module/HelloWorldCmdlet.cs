// Docs:
//  - PowerShellGet:
//      - PowerShellGet Module: https://technet.microsoft.com/library/dn807169.aspx
//      - Unofficial example of PowerShellGet-friendly package: https://github.com/anpur/powershellget-module
//      - Create pure powershell Nuget module for PowerShellGet: http://stackoverflow.com/questions/33433824/create-pure-powershell-nuget-module-for-powershellget
//  - PowerShell Modules:
//      - How to Write a PowerShell Binary Module: https://msdn.microsoft.com/en-us/library/dd878342(v=vs.85).aspx
//      - Creating a Cmdlet without Parameters: https://msdn.microsoft.com/en-us/library/ms714622(v=vs.85).aspx

// Commands:
//  - Test the module from build output (no package used)
//      Import-Module .\bin\Debug\PowerShellGet-Test-Binary-Module.dll
//      Write-HelloWorld
//      Note: no good way to unload the DLL besides exiting PowerShell. This is required to be able to build again.
//  - Install the module:
//      - From the local test repo:
//          Install-Module -Name PowerShellGet-Test-Binary-Module -Repository LocalTest -Scope CurrentUser
//      - From the official PowerShell Gallery:
//          Install-Module -Name PowerShellGet-Test-Binary-Module -Repository PSGallery -Scope CurrentUser
//  - List installed modules
//      Get-InstalledModule
//  - Uninstall the module
//      Uninstall-Module PowerShellGet-Test-Binary-Module
//      Remove-Module PowerShellGet-Test-Binary-Module

using System.Management.Automation;

namespace Module
{
    [Cmdlet(VerbsCommunications.Write, "HelloWorld")]
    public class HelloWorldCmdlet : Cmdlet
    {
        protected override void BeginProcessing()
        {
            WriteObject("Hello, World!");
        }
    }
}
