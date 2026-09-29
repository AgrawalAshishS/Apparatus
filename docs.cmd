@echo off
rem Rebuilds the API reference (docs\api) and example pages (docs\examples) from the source code.
dotnet build src\Apparatus\Apparatus.csproj -c Release --nologo -v q -clp:ErrorsOnly || exit /b 1
dotnet run --project tools\ApiDocGenerator -c Release -- src\Apparatus\bin\Release\net8.0\Apparatus.xml docs\api examples\Apparatus.Examples docs\examples
