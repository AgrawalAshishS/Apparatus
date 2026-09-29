@echo off
dotnet build Apparatus.sln -c Release --nologo -v q -clp:ErrorsOnly;Summary %*
