@echo off
cls
thirdparty\nant\nant.exe -buildfile:support\default.build -l:_build.log %*
