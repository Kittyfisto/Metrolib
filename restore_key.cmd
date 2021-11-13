@setlocal
@echo off

sig\crypt.exe decrypt sig\sns key.snk
sig\crypt.exe enablesigning src\Metrolib\Metrolib.csproj ..\..\key.snk
sig\crypt.exe enablesigning src\Metrolib.Test\Metrolib.Test.csproj ..\..\key.snk
