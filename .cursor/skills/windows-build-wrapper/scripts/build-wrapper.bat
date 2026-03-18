@echo off
setlocal EnableExtensions EnableDelayedExpansion

set "TARGET_MODE=%~1"
if /i "%TARGET_MODE%"=="" set "TARGET_MODE=unity"
set "COMPILE_TYPE=%~2"
if /i "%COMPILE_TYPE%"=="" set "COMPILE_TYPE=default"

if /i not "%TARGET_MODE%"=="unity" if /i not "%TARGET_MODE%"=="windows-test" (
  echo [ERROR] Invalid mode: %TARGET_MODE%
  echo Usage: %~nx0 [unity^|windows-test] [default^|shengwang]
  exit /b 1
)

if /i not "%COMPILE_TYPE%"=="default" if /i not "%COMPILE_TYPE%"=="shengwang" (
  echo [ERROR] Invalid compile type: %COMPILE_TYPE%
  echo Usage: %~nx0 [unity^|windows-test] [default^|shengwang]
  exit /b 1
)

set "SCRIPT_DIR=%~dp0"
for %%I in ("%SCRIPT_DIR%..\..\..\..") do set "REPO_ROOT=%%~fI"
set "PROJ_DIR=%REPO_ROOT%\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\wrapper_build\proj"
set "CMAKE_LOG=%TEMP%\wrapper_cmake_%RANDOM%%RANDOM%.log"

if not exist "%PROJ_DIR%" (
  echo [ERROR] Build directory not found: %PROJ_DIR%
  exit /b 1
)

echo [INFO] Target mode: %TARGET_MODE%
echo [INFO] Compile type: %COMPILE_TYPE%
echo [INFO] Project dir: %PROJ_DIR%

pushd "%PROJ_DIR%" || (
  echo [ERROR] Failed to enter %PROJ_DIR%
  exit /b 1
)

echo [INFO] Cleaning existing content in proj directory...
for /d %%D in (*) do rd /s /q "%%D"
del /f /q * >nul 2>&1

echo [INFO] Running CMake...
cmake -G "Visual Studio 16 2019" .. > "%CMAKE_LOG%" 2>&1
set "CMAKE_EXIT=%ERRORLEVEL%"
type "%CMAKE_LOG%"
if not "%CMAKE_EXIT%"=="0" (
  echo [ERROR] CMake failed with exit code %CMAKE_EXIT%
  popd
  exit /b %CMAKE_EXIT%
)

for /f "usebackq delims=" %%I in (`powershell -NoProfile -Command "(Get-Content -Path '%CMAKE_LOG%' ^| Where-Object { $_.Trim() -ne '' } ^| Select-Object -Last 1)"`) do set "LAST_CMAKE_LINE=%%I"

echo [INFO] Last CMake line: !LAST_CMAKE_LINE!
echo !LAST_CMAKE_LINE! | findstr /c:"Build files have been written to" >nul
if errorlevel 1 (
  echo [ERROR] CMake did not finish with expected message.
  popd
  exit /b 1
)

for /f "tokens=*" %%i in ('powershell -command "[math]::truncate((Get-Date).ToUniversalTime().Subtract((Get-Date ''1970-01-01'')).TotalSeconds)"') do set BUILD_NO=%%i
if /i "%COMPILE_TYPE%"=="shengwang" (
  set "EASEMOB_MACROS=/DBUILD_NO=%BUILD_NO% /DUSE_SHENGWANG_DOMAIN"
) else (
  set "EASEMOB_MACROS=/DBUILD_NO=%BUILD_NO%"
)
set "CL=%EASEMOB_MACROS%"

echo [INFO] BUILD_NO=%BUILD_NO%
echo [INFO] Building easemob.dll with CL=%CL%
msbuild easemob.vcxproj /p:Configuration=Release /p:Platform="x64"
if errorlevel 1 (
  echo [ERROR] Failed to build easemob.dll
  popd
  exit /b 1
)

if /i "%TARGET_MODE%"=="windows-test" (
  set "SDKWRAPPER_MACROS=/D_PURE_WIN32"
) else (
  set "SDKWRAPPER_MACROS="
)
set "CL=%SDKWRAPPER_MACROS%"

echo [INFO] Building SdkWrapper.dll with CL=%CL%
msbuild SdkWrapper.vcxproj /p:Configuration=Release /p:Platform="x64"
if errorlevel 1 (
  echo [ERROR] Failed to build SdkWrapper.dll
  popd
  exit /b 1
)

echo [INFO] Building CommonWrapper.dll
msbuild CommonWrapper.vcxproj /p:Configuration=Release /p:Platform="x64"
if errorlevel 1 (
  echo [ERROR] Failed to build CommonWrapper.dll
  popd
  exit /b 1
)

echo [INFO] Building ChatCWrapper.dll
msbuild ChatCWrapper.vcxproj /p:Configuration=Release /p:Platform="x64"
if errorlevel 1 (
  echo [ERROR] Failed to build ChatCWrapper.dll
  popd
  exit /b 1
)

echo [INFO] Wrapper build completed successfully.
popd
exit /b 0
