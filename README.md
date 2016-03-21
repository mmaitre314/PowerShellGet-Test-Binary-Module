[![PowerShellGet package](http://mmaitre314.github.io/images/nuget.png)](https://www.powershellgallery.com/packages/PowerShellGet-Test-Binary-Module/)

A minimalist example of [PowerShell binary module](https://msdn.microsoft.com/en-us/library/dd878342(v=vs.85).aspx) (.NET DLL) that can be published to the [PowerShell gallery](https://www.powershellgallery.com/) as [PowerShellGet](https://technet.microsoft.com/library/dn807169.aspx) package (aka NuGet).

The module contains a single `Write-HelloWorld` cmdlet, whose name is self-explanatory:

```
using System.Management.Automation;

[Cmdlet(VerbsCommunications.Write, "HelloWorld")]
public class HelloWorldCmdlet : Cmdlet
{
    protected override void BeginProcessing()
    {
        WriteObject("Hello, World!");
    }
}
```

(the `System.Management.Automation` reference is found at `c:\Program Files (x86)\Reference Assemblies\Microsoft\WindowsPowerShell\3.0\System.Management.Automation.dll`)

The module manifest stores NuGet-style package info:

```
@{
    RootModule = 'PowerShellGet-Test-Binary-Module.dll'
    ModuleVersion = '1.0.0.3'
    CmdletsToExport = '*'
    GUID = '95ee4cf1-d508-45a8-9680-203b71453f98'
    DotNetFrameworkVersion = '4.5.1'
    Author = 'Matthieu Maitre'
    Description = 'Example of PowerShellGet Binary Module'
    CompanyName = 'None'
    Copyright = '(c) 2016 Matthieu Maitre. All rights reserved.'
    PrivateData = @{
        PSData = @{
            ProjectUri = 'https://github.com/mmaitre314/PowerShellGet-Test-Binary-Module'
            LicenseUri = 'https://github.com/mmaitre314/PowerShellGet-Test-Binary-Module/blob/master/LICENSE'
            ReleaseNotes = ''
        }
    }
}
```

And a couple of scripts (`Publish-ToTestRepo.ps1`, `Publish-ToPowerShellGallery.ps1`) automate publication.

Unit-testing cmdlets requires a couple of tricks:
- The PowerShell host needs to be mocked (`ICommandRuntime`)
- Protected methods need to be accessed using `PrivateObject`

```
[TestMethod]
public void Test()
{
    var mock = new Mock<ICommandRuntime>();

    var cmdlet = new HelloWorldCmdlet
    {
        CommandRuntime = mock.Object
    };
    new PrivateObject(cmdlet).Invoke("BeginProcessing");

    mock.Verify(runtime => runtime.WriteObject("Hello, World!"), Times.Once);
}
```

This code was heavily inspired from [AnPur's PowerShellGet-Module](https://github.com/anpur/powershellget-module).
