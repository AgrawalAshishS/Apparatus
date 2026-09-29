@echo off
rem Runs tests with coverage and writes an HTML + text summary to artifacts\coverage-report
where reportgenerator >nul 2>nul || dotnet tool install -g dotnet-reportgenerator-globaltool
if exist artifacts\coverage rmdir /s /q artifacts\coverage
dotnet test Apparatus.sln --nologo -v q --collect:"XPlat Code Coverage" --results-directory artifacts\coverage || exit /b 1
reportgenerator -reports:"artifacts\coverage\**\coverage.cobertura.xml" -targetdir:artifacts\coverage-report -reporttypes:Html;TextSummary -verbosity:Error
type artifacts\coverage-report\Summary.txt
