@echo Off

set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

echo BUILD.BAT - Restoring nuget packages
nuget restore ".\src\Colourful.sln"
echo BUILD.BAT - Nuget package restore complete

echo BUILD.BAT - Building solution using FAKE
".\src\packages\FAKE.3.34.7\tools\Fake.exe" build.fsx
echo BUILD.BAT - FAKE build complete

