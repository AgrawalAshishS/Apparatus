@echo off
dotnet pack src\Apparatus\Apparatus.csproj -c Release --nologo -o artifacts\nupkg
