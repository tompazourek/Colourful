@echo Off

echo BUILD.BAT - Restoring nuget packages
".\.nuget\NuGet.exe" restore ".\src\Colourful.sln"
echo BUILD.BAT - Nuget package restore complete

echo BUILD.BAT - Building solution using FAKE
".\src\packages\FAKE.3.34.7\tools\Fake.exe" build.fsx
echo BUILD.BAT - FAKE build complete

