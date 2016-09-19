@echo Off

echo BUILD.BAT - NuGet package restore started.
".\src\.nuget\NuGet.exe" restore ".\src\Colourful.sln" -OutputDirectory ".\src\packages"
echo BUILD.BAT - NuGet package restore finished.

echo BUILD.BAT - FAKE build started.
".\src\packages\FAKE.3.34.7\tools\Fake.exe" build.fsx encoding=utf-8
echo BUILD.BAT - FAKE build finished.

