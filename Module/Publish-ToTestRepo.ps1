$binPath = Join-Path $PSScriptRoot "bin\Release"
$modulePath = Join-Path $PSScriptRoot "bin\PowerShellGet-Test-Binary-Module"
$repoPath = "C:\Temp\PSRepo"

Write-Host "Preparing module"

Remove-Item $modulePath -Force -Recurse
New-Item $modulePath -ItemType Directory -Force | Out-Null
Copy-Item (Join-Path $binPath "PowerShellGet-Test-Binary-Module.dll") (Join-Path $modulePath "PowerShellGet-Test-Binary-Module.dll")
Copy-Item (Join-Path $PSScriptRoot "PowerShellGet-Test-Binary-Module.psd1") (Join-Path $modulePath "PowerShellGet-Test-Binary-Module.psd1")

if ((Get-PSRepository | ? { $_.name -eq "LocalTest" } | Measure-Object).Count -eq 0)
{
    Write-Host "Creating local test repo"

    New-Item $repoPath -ItemType Directory -Force | Out-Null
    Register-PSRepository -Name LocalTest -SourceLocation $repoPath -PublishLocation $repoPath -InstallationPolicy Trusted
}

Write-Host "Publishing module"

Publish-Module -Path $modulePath -Repository LocalTest -NuGetApiKey 'none'
