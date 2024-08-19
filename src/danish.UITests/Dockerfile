# escape=`
# Use the official Visual Studio 2022 image from Microsoft
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2019

USER ContainerAdministrator

# Set shell to PowerShell
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Define Working Directory
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
# Install Google Chrome and ChromeDriver
# =============================================================
RUN Write-Output 'Downloading and installing Google Chrome...' ; `
    Invoke-WebRequest -Uri "https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7B01234567-89AB-CDEF-0123-456789ABCDEF%7D%26lang%3Den%26browser%3D3%26usagestats%3D0%26appname%3DGoogle%20Chrome%26needsadmin%3Dtrue%26brand%3DGCEB/dl/chrome/install/googlechromestandaloneenterprise64.msi" -OutFile "googlechromestandaloneenterprise64.msi" ; `
    Start-Process -FilePath msiexec.exe -ArgumentList '/i', 'googlechromestandaloneenterprise64.msi', '/qn', '/norestart' -NoNewWindow -Wait ; `
    Remove-Item "googlechromestandaloneenterprise64.msi" ; `
    Write-Output 'Google Chrome installation completed.' ; `
    Write-Output 'Downloading and installing ChromeDriver...' ; `
    Invoke-WebRequest -Uri "https://chromedriver.storage.googleapis.com/114.0.5735.90/chromedriver_win32.zip" -OutFile "chromedriver_win32.zip" ; `
    Expand-Archive -Path "chromedriver_win32.zip" -DestinationPath "C:/tools/chromedriver" -Force ; `
    Remove-Item "chromedriver_win32.zip" ; `
    $env:Path += ';C:\tools\chromedriver;' + ';C:\Program Files\Google\Chrome\Application\;' ; `
    [System.Environment]::SetEnvironmentVariable('Path', $env:Path, [EnvironmentVariableTarget]::Machine) ; `
    Write-Output 'ChromeDriver installation completed.'

# =============================================================
# Install .NET Selenium WebDriver NuGet Package
# =============================================================
RUN Write-Output 'Installing Selenium WebDriver NuGet package...' ; `
    Invoke-WebRequest -Uri "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile "C:/tools/nuget.exe" ; `
    .\nuget.exe install Selenium.WebDriver -Version 3.141.0 -OutputDirectory C:\tools\selenium ; `
    Write-Output 'Selenium WebDriver NuGet package installed.'

# =============================================================
# Install NUnit Console Runner for Test Execution
# =============================================================
RUN Write-Output 'Installing NUnit Console Runner...' ; `
    .\nuget.exe install NUnit.Console -Version 3.11.1 -OutputDirectory C:\tools\nunit ; `
    Write-Output 'NUnit Console Runner installed.'

# =============================================================
# Install Git
# =============================================================
RUN Write-Output 'Downloading and installing Git...' ; `
    Invoke-WebRequest -Uri "https://github.com/git-for-windows/git/releases/download/v2.42.0.windows.1/Git-2.42.0-64-bit.exe" -OutFile "C:/tools/git-installer.exe" ; `
    Start-Process -FilePath "C:/tools/git-installer.exe" -ArgumentList '/SILENT' -NoNewWindow -Wait ; `
    Remove-Item "C:/tools/git-installer.exe" ; `
    Write-Output 'Git installation completed.'

# =============================================================
# Clone the GitHub repository using the PAT
# =============================================================
ARG GITHUB_PAT
ENV GITHUB_PAT2=${GITHUB_PAT}
RUN Write-Output 'Cloning the GitHub repository...' ; `
    git clone https://$env:GITHUB_PAT2@github.com/advancedcsg/ahc-CareDirector-V6-QA.git repo ; `
    Write-Output 'GitHub repository cloned.'

# =============================================================
# Build the Project using the 64-bit MSBuild
# =============================================================
WORKDIR "C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\MSBuild\\Current\\Bin\\amd64\\"
RUN Write-Output 'Building the project using MSBuild...' ; `
    ./MSBuild.exe "C:\\tools\\repo\\src\\Phoenix.UITests\\" ; `
    Write-Output 'Project build completed.'

# =============================================================
# Verify Chrome Starts Correctly
# =============================================================
WORKDIR "C:\\Program Files\\Google\\Chrome\\Application\\"
RUN Write-Output 'Verifying Chrome starts correctly...' ; `
    .\chrome.exe --headless --disable-gpu --no-sandbox --remote-debugging-port=9222 --disable-dev-shm-usage --disable-software-rasterizer --version ; `
    Write-Output 'Chrome verified successfully.'

# Create a Temp folder under C:/
RUN Write-Output 'Creating Temp directory...' ; `
    New-Item -Path "C:\Temp" -ItemType "Directory" ; `
    Write-Output 'Temp directory created.'

# =============================================================
# Test the Project with Additional Logging
# =============================================================
WORKDIR "C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow\\"
RUN Write-Output 'Running tests with vstest.console.exe...' ; `
    ./vstest.console.exe "C:\\tools\\repo\\src\\Phoenix.UITests\\bin\\Debug\\Phoenix.UITests.dll" `
    --TestCaseFilter:"TestCategory=Daily_Runs" `
    --Logger:trx `
    --Logger:html `
    --ResultsDirectory:"C:\\Temp\\TestResults" ; `
    Write-Output 'Tests execution completed.'
