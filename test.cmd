@echo off
rem Usage: test.cmd [filter]   e.g. test.cmd StringExtensionTests
if "%~1"=="" (
  dotnet test Apparatus.sln --nologo -v q
) else (
  dotnet test Apparatus.sln --nologo -v q --filter "FullyQualifiedName~%~1"
)
