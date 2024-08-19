# escape=`
# Use the official Visual Studio 2022 image from Microsoft
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2019

USER ContainerAdministrator

# Set shell to PowerShell
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Set the working directory to C:\Program Files\

WORKDIR "C:\\tools\\"

# =============================================================
# Install Visual Studio Community 2022 and Dependencies
# =============================================================
ENV VS_COMMUNITY_URL=https://aka.ms/vs/17/release/vs_community.exe

# Download and install Visual Studio Community 2022 with necessary components
RUN Write-Output 'Downloading Visual Studio...' ; `
    Invoke-WebRequest -Uri $env:VS_COMMUNITY_URL -OutFile C:\tools\vs_community.exe ; `
    Write-Output 'Installing Visual Studio...' ; `
    Start-Process -FilePath C:\tools\vs_community.exe -ArgumentList '--quiet', '--wait', '--norestart', `
    '--add', 'Microsoft.VisualStudio.Workload.CoreEditor', `
    '--add', 'Microsoft.VisualStudio.Workload.ManagedDesktop', `
    '--add', 'Microsoft.VisualStudio.Workload.NetCoreBuildTools', `
    '--add', 'Microsoft.VisualStudio.Component.TestTools.BuildTools', `
    '--add', 'Microsoft.VisualStudio.Component.VSTest.TestAdapter', `
    '--add', 'Microsoft.VisualStudio.Component.TestTools.Core', `
    '--add', 'Microsoft.VisualStudio.ComponentGroup.TestTools.Native', `
    '--add', 'Microsoft.Net.Component.4.7.2.SDK', `
    '--add', 'Microsoft.Net.Component.4.7.2.TargetingPack', `
    '--add', 'Microsoft.VisualStudio.Workload.VisualStudioExtensionBuildTools', `
    '--add', 'Microsoft.VisualStudio.Component.VC.ATL', `
    '--add', 'Microsoft.VisualStudio.Component.VC.CoreBuildTools', `
    '--add', 'Microsoft.VisualStudio.Component.VC.Tools.x86.x64', `
    '--add', 'Microsoft.VisualStudio.Component.VC.Tools.ARM64', `
    '--add', 'Microsoft.VisualStudio.Component.Windows10SDK.18362' `
    -NoNewWindow -Wait ; `
    Write-Output 'Visual Studio installation completed.' ; `
    Remove-Item C:\tools\vs_community.exe ; `
    $env:Path += ';C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64;' + `
    ';C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow;' ; `
    [System.Environment]::SetEnvironmentVariable('Path', $env:Path, [System.EnvironmentVariableTarget]::Machine)



# =============================================================
# Install .NET Selenium WebDriver NuGet Package
# =============================================================
RUN Write-Output 'Installing Selenium WebDriver NuGet package...' ; `
    New-Item -Path 'C:\Program Files\selenium' -ItemType Directory -Force ; `
    Invoke-WebRequest -Uri "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile "C:/tools/nuget.exe" ; `
    .\nuget.exe install Selenium.WebDriver -Version 4.0.0 -OutputDirectory 'C:\Program Files\selenium' -Force ; `
    Write-Output 'Selenium WebDriver NuGet package installed.'
    
# =============================================================
# Install Git
# =============================================================
RUN Write-Output 'Downloading and installing Git...' ; `
    Invoke-WebRequest -Uri "https://github.com/git-for-windows/git/releases/download/v2.42.0.windows.1/Git-2.42.0-64-bit.exe" -OutFile "C:/tools/git-installer.exe" ; `
    Start-Process -FilePath "C:/tools/git-installer.exe" -ArgumentList '/SILENT' -NoNewWindow -Wait ; `
    Remove-Item "C:/tools/git-installer.exe" ; `
    Write-Output 'Git installation completed.'

WORKDIR "C:\\Program Files\\"
# =============================================================
# Install Google Chrome and ChromeDriver
# =============================================================

RUN Write-Output 'Downloading and installing Google Chrome...' ; `
    Invoke-WebRequest -Uri "https://storage.googleapis.com/chrome-for-testing-public/127.0.6533.119/win64/chrome-win64.zip" -OutFile "chrome-win64.zip" ; `
    New-Item -Path 'C:\Program Files\Google\Chrome\Application' -ItemType Directory -Force ; `
    Expand-Archive -Path "chrome-win64.zip" -DestinationPath 'C:\Program Files\Google\Chrome\Application' -Force ; `
    Remove-Item "chrome-win64.zip" ; `
    Write-Output 'Google Chrome installation completed.'

RUN Write-Output 'Downloading and installing ChromeDriver...' ; `
    Invoke-WebRequest -Uri "https://storage.googleapis.com/chrome-for-testing-public/127.0.6533.119/win64/chromedriver-win64.zip" -OutFile "chromedriver-win64.zip" ; `
    New-Item -Path 'C:\Program Files\Google\Chrome\Driver' -ItemType Directory -Force ; `
    Expand-Archive -Path "chromedriver-win64.zip" -DestinationPath 'C:\Program Files\Google\Chrome\Driver' -Force ; `
    Remove-Item "chromedriver-win64.zip" ; `
    Write-Output 'ChromeDriver installation completed.'



# Add Chrome and ChromeDriver to the system PATH
RUN $env:Path += ';C:\Program Files\Google\Chrome\Application\chrome-win64\;' + `
                 ';C:\Program Files\Google\Chrome\Driver\;' ; `
    [System.Environment]::SetEnvironmentVariable('Path', $env:Path, [System.EnvironmentVariableTarget]::Machine)

# =============================================================
# Clone the GitHub repository using the PAT
# =============================================================
ARG GITHUB_PAT
ENV GITHUB_PAT2=${GITHUB_PAT}
RUN Write-Output 'Cloning the GitHub repository...' ; `
    git clone https://$env:GITHUB_PAT2@github.com/advancedcsg/POC-Danish-SeleniumTest.git repo ; `
    Write-Output 'GitHub repository cloned.'
    
# Create a Temp folder under C:/
RUN Write-Output 'Creating Temp directory...' ; `
    New-Item -Path "C:\Temp" -ItemType "Directory" ; `
    Write-Output 'Temp directory created.'

# =============================================================
# Test the Project with Additional Logging
# =============================================================
WORKDIR "C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow\\"
RUN Write-Output 'Running tests with vstest.console.exe...' ; `
    ./vstest.console.exe "C:\\tools\\repo\\Test\\src\\Phoenix.UITests\\bin\\Debug\\Phoenix.UITests.dll" `
    --TestCaseFilter:"TestCategory=Daily_Runs" `
    --Logger:trx `
    --Logger:html `
    --ResultsDirectory:"C:\\Temp\\TestResults" ; `
    Write-Output 'Tests execution completed.'


