# escape=`

# Stage 1: Build environment
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2019 AS build

USER ContainerAdministrator

# Set shell to PowerShell
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Define Working Directory
WORKDIR "C:\\tools\\"

# Install Visual Studio Community 2022 and Dependencies
ENV VS_COMMUNITY_URL=https://aka.ms/vs/17/release/vs_community.exe

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

# Install Google Chrome and ChromeDriver
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

# Compress Chrome and other necessary directories
RUN Compress-Archive -Path 'C:\Program Files\Google\Chrome\Application' -DestinationPath 'C:\tools\chrome.zip' -Force ; `
    Compress-Archive -Path 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64' -DestinationPath 'C:\tools\msbuild.zip' -Force ; `
    Compress-Archive -Path 'C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow' -DestinationPath 'C:\tools\testwindow.zip' -Force

# Stage 2: Runtime environment
FROM mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2019

USER ContainerAdministrator

# Set shell to PowerShell
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Copy and extract necessary tools from the build stage
COPY --from=build C:\tools\chrome.zip C:\tools\chrome.zip
COPY --from=build C:\tools\msbuild.zip C:\tools\msbuild.zip
COPY --from=build C:\tools\testwindow.zip C:\tools\testwindow.zip
COPY --from=build C:\tools\chromedriver C:\tools\chromedriver
COPY --from=build C:\tools\selenium C:\tools\selenium
COPY --from=build C:\tools\nunit C:\tools\nunit

# Extract the compressed archives
RUN Expand-Archive -Path 'C:\tools\chrome.zip' -DestinationPath 'C:\Program Files\Google\Chrome\Application' -Force ; `
    Expand-Archive -Path 'C:\tools\msbuild.zip' -DestinationPath 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64' -Force ; `
    Expand-Archive -Path 'C:\tools\testwindow.zip' -DestinationPath 'C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow' -Force ; `
    Remove-Item 'C:\tools\chrome.zip' ; `
    Remove-Item 'C:\tools\msbuild.zip' ; `
    Remove-Item 'C:\tools\testwindow.zip'

# Add Visual Studio and related tools to the PATH
RUN $env:Path += ';C:\tools\chromedriver;' + `
    ';C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64;' + `
    ';C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow;' + `
    ';C:\Program Files\Google\Chrome\Application\;' ; `
    [System.Environment]::SetEnvironmentVariable('Path', $env:Path, [System.EnvironmentVariableTarget]::Machine)