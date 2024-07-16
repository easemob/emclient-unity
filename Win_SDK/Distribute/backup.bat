@echo off
setlocal

rem 定义版本号变量
set "version=1.3.1"
set "cid1=emclient-linux-commit-id=1081245a22"
set "cid2=emclient-unity-commit-id=d4d6cb6347"

rem 打开命令回显，以显示每个命令
@echo on

rem 设置备份目录
set "dest_dir=D:\workspace\UnitySDKWin\backup\win_proj_backup\%version%"

rem 判断备份目录是否存在，如果存在则移除它
if exist "%dest_dir%" (
    echo Removing existing dest directory: %dest_dir%
    rmdir /s /q "%dest_dir%"
)

rem 创建备份目录
mkdir "%dest_dir%"

rem 设置commit-id输出文件
set "output_file=%dest_dir%\commit-ids.txt"

rem 写入commit-id
echo %cid1% >> "%output_file%"
echo %cid2% >> "%output_file%"


rem 设置proj源目录和proj备份目录的路径
set "source_dir_proj=D:\workspace\UnitySDKWin\emclient\emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\build_win\proj"
set "dest_dir_proj=D:\workspace\UnitySDKWin\backup\win_proj_backup\%version%\proj"

rem 创建proj备份目录
mkdir "%dest_dir_proj%"

rem 拷贝 .vcproj 文件
for %%f in ("%source_dir_proj%\*.vcproj") do (
    echo Copying file: %%~nxf
    copy "%%f" "%dest_dir_proj%"
)

rem 拷贝 .vcxproj 文件
for %%f in ("%source_dir_proj%\*.vcxproj") do (
    echo Copying file: %%~nxf
    copy "%%f" "%dest_dir_proj%"
)

rem 拷贝 .vcxproj.filters 文件
for %%f in ("%source_dir_proj%\*.vcxproj.filters") do (
    echo Copying file: %%~nxf
    copy "%%f" "%dest_dir_proj%"
)

rem 拷贝 .vcxproj.user 文件
for %%f in ("%source_dir_proj%\*.vcxproj.user") do (
    echo Copying file: %%~nxf
    copy "%%f" "%dest_dir_proj%"
)

rem 拷贝 .sln 文件
for %%f in ("%source_dir_proj%\*.sln") do (
    echo Copying file: %%~nxf
    copy "%%f" "%dest_dir_proj%"
)

rem 设置dll源目录和dll备份目录的路径
set "source_dir_dll=D:\workspace\UnitySDKWin\emclient\emclient-unity\AgoraChatSDK\CWrapper\Wrapper\Wrapper_Common\build_win\proj\Release"
set "dest_dir_dll=D:\workspace\UnitySDKWin\backup\win_proj_backup\%version%\dll-pdb"

rem 创建proj备份目录
mkdir "%dest_dir_dll%"

rem 拷贝dll和pdb文件
xcopy /Y "%source_dir_dll%\SdkWrapper.dll" "%dest_dir_dll%"
xcopy /Y "%source_dir_dll%\SdkWrapper.pdb" "%dest_dir_dll%"

xcopy /Y "%source_dir_dll%\CommonWrapper.dll" "%dest_dir_dll%"
xcopy /Y "%source_dir_dll%\CommonWrapper.pdb" "%dest_dir_dll%"

xcopy /Y "%source_dir_dll%\ChatCWrapper.dll" "%dest_dir_dll%"
xcopy /Y "%source_dir_dll%\ChatCWrapper.pdb" "%dest_dir_dll%"

xcopy /Y "%source_dir_dll%\easemob.pdb" "%dest_dir_dll%"
xcopy /Y "%source_dir_dll%\easemob.lib" "%dest_dir_dll%"

endlocal