@echo off
set /p TMLServerPath=<tmlpath.user

call "%TMLServerPath%" -build % -unsafe true*
