@echo off
rem Usage: test.cmd [filter]   e.g. test.cmd StringExtensionTests
if "%~1"=="" (
  dotnet test Apparatus.sln --nologo -v q -clp:ErrorsOnly;Summary
) else (
  dotnet test Apparatus.sln --nologo -v q -clp:ErrorsOnly;Summary --filter "FullyQualifiedName~%~1"
)
