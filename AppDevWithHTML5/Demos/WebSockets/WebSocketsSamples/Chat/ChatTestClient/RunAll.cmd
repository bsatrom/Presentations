@echo off

setlocal

set FQDN=localhost
set bindir=%~dp0..\..\..\..\build\Debug\WebSockets\bin

REM Start selfhost service
start CMD /C "%bindir%\ChatService.exe" 
if ERRORLEVEL 1 goto Error

REM Start client hitting selfhosted service
start CMD /C "%bindir%\ChatTestClient.exe"
if ERRORLEVEL 1 goto Error

goto OuttaHere

:Error
echo %~0 failed: %ERRORLEVEL%
exit /b 999

:OuttaHere
echo %~0 succeeded
exit /b 0