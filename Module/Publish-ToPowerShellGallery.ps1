$binPath = Join-Path $PSScriptRoot "bin\Release"
$modulePath = Join-Path $PSScriptRoot "bin\PowerShellGet-Test-Binary-Module"

Write-Host "Preparing module"

Remove-Item $modulePath -Force -Recurse
New-Item $modulePath -ItemType Directory -Force | Out-Null
Copy-Item (Join-Path $binPath "PowerShellGet-Test-Binary-Module.dll") (Join-Path $modulePath "PowerShellGet-Test-Binary-Module.dll")
Copy-Item (Join-Path $PSScriptRoot "PowerShellGet-Test-Binary-Module.psd1") (Join-Path $modulePath "PowerShellGet-Test-Binary-Module.psd1")

Write-Host "Publishing module"

Publish-Module -Path $modulePath -Repository PSGallery -NuGetApiKey (Read-Host "NuGetApiKey (from https://powershellgallery.com/account)")
