#tool "nuget:?package=NUnit.ConsoleRunner&version=3.4.1"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");

///////////////////////////////////////////////////////////////////////////////
// PREPARATION
///////////////////////////////////////////////////////////////////////////////

// basic information
var productName = "Colourful";
var companyName = "Tomáš Pažourek";
var authors = new [] { companyName };
var copyright = string.Format("Copyright \u00A9 {0}, {1}", companyName, DateTime.Now.Year);
var summary = "Open source .NET library for working with color spaces.";
var tags = new [] { "color", "space", "sRGB", "Adobe RGB", "delta-e", "lab", "luv", "xyz", "cielab", "cieluv", "ciexyz", "cct", "chromatic adaptation", "conversion", "difference", "convert" };
var description = @"
Open source .NET library for working with color spaces.
Supports these color spaces: RGB (various working spaces), linear RGB, CIE XYZ, CIE xyY, CIE Lab, CIE Luv, CIE LCh (uv), CIE LCh (ab), Hunter Lab, LMS (cone response).
Conversions are done correctly using chromatic adaptation and white points.
Other features include: Delta-E color difference (many formulas), Correlated color temperature (CCT) approximation, Conversion between RGB working spaces, Chromatic adaptation (many white points supported).
For more info, visit the project page.";

// Get whether or not this is a local build
var local = BuildSystem.IsLocalBuild;
var versionInformational = "1.1.1"; // used for NuGet package version as well
var buildNumber = AppVeyor.IsRunningOnAppVeyor ? AppVeyor.Environment.Build.Number.ToString() : "0";
var versionBuild = versionInformational + "." + buildNumber;

// Define directories and files
var sourceDirectory = Directory("./src");
var outputDirectory = Directory("./output");
var artifactsDirectory = outputDirectory + Directory("artifacts");
var packageDirectory = outputDirectory + Directory("package");
var testResultsDirectory = outputDirectory + Directory("testresults");
var solutionDirectory = sourceDirectory;
var solutionFile = GetFiles(sourceDirectory.ToString() + "/*.sln").First();
var projectDirectories = GetFiles(solutionDirectory.ToString() + "/*/*.csproj").Select(x => x.GetDirectory());
var baseBuildDirectory = sourceDirectory + Directory("Colourful/bin/Release");

// Define files
var nugetExecutable = "./tools/nuget.exe"; 

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(context =>
{
    // Executed BEFORE the first task.
    Information("Target: " + target);
    Information("Is running on AppVeyor: " + AppVeyor.IsRunningOnAppVeyor.ToString());
});

Teardown(context =>
{
    // Executed AFTER the last task.
    Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

// Cleans /bin and /obj directories of every project in the solution
Task("Clean-Project-Outputs")
    .Does(() => 
{
    var subDirectories = new [] { Directory("/bin/"), Directory("/obj/") };
    foreach (var removedDirectory in projectDirectories.SelectMany(x => subDirectories, (x, y) => x + y))
    {
        if (!DirectoryExists(removedDirectory))
            continue;
        
        DeleteDirectory(removedDirectory, recursive: true);
    }
});

// Cleans output directory of the whole solution
Task("Clean-Output-Directory")
    .Does(() => 
{
    if (!DirectoryExists(outputDirectory))
        return;

    DeleteDirectory(outputDirectory, recursive: true);
});

// Clean all relevant directories
Task("Clean")
    .IsDependentOn("Update-AppVeyor-Build-Number")
    .IsDependentOn("Clean-Project-Outputs")
    .IsDependentOn("Clean-Output-Directory");

// Creates all necessary directories
Task("Create-Directories")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var directories = new [] { outputDirectory, artifactsDirectory, packageDirectory, testResultsDirectory };
    foreach (var directory in directories)
    {
        if (DirectoryExists(directory))
            continue;

        CreateDirectory(directory);
    };
});

// Restores NuGet packages for the whole solution
Task("Restore-NuGet-Packages")
    .IsDependentOn("Create-Directories")
    .Does(() =>
{
    NuGetRestore(solutionFile);
});

// Patches the common assembly info files in solution
Task("Patch-Assembly-Info")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    var assemblyInfoFile = sourceDirectory + File("Colourful/Properties/AssemblyInfo.cs");
    var assemblyInfo = ParseAssemblyInfo(assemblyInfoFile);
    CreateAssemblyInfo(assemblyInfoFile, new AssemblyInfoSettings {
        CLSCompliant = true,
        Title = productName,
        Description = summary,
        Guid = "D11F6BE9-3DCB-45B7-A076-4D476236C3CB",
        Product = productName,
        Version = "0.0.0.0",
        FileVersion = "0.0.0.0",
        InformationalVersion = buildNumber,
        Copyright = copyright
    });
});

// updates AppVeyor build number to informational version
Task("Update-AppVeyor-Build-Number")
    .WithCriteria(() => AppVeyor.IsRunningOnAppVeyor)
    .Does(() =>
{
    AppVeyor.UpdateBuildVersion(versionBuild);
});

// Builds solution
Task("Build")
    .IsDependentOn("BuildNET45")
    .IsDependentOn("BuildNET40")
    .IsDependentOn("BuildNET35")
    .IsDependentOn("BuildPCL");

Task("Pre-Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Patch-Assembly-Info");

Task("BuildPCL")
    .IsDependentOn("Pre-Build")
    .Does(() =>
{
    MSBuild(solutionFile, settings => settings.SetConfiguration("Release (PCL)"));
});

Task("BuildNET35")
    .IsDependentOn("Pre-Build")
    .Does(() =>
{
    MSBuild(solutionFile, settings => settings.SetConfiguration("Release (.NET 3.5)"));
});

Task("BuildNET40")
    .IsDependentOn("Pre-Build")
    .Does(() =>
{
    MSBuild(solutionFile, settings => settings.SetConfiguration("Release (.NET 4.0)"));
});

Task("BuildNET45")
    .IsDependentOn("Pre-Build")
    .Does(() =>
{
    MSBuild(solutionFile, settings => settings.SetConfiguration("Release"));
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    foreach (var projectDirectory in projectDirectories)
    {
        var binariesDirectory = projectDirectory + Directory("/bin/");
        Information(binariesDirectory.ToString());

        var projectFile = GetFiles(projectDirectory.ToString() + "/*.csproj").First();
        var project = ParseProject(projectFile);
        var projectAssemblyName = project.AssemblyName;

        var testAssembliesMask = binariesDirectory.ToString() + "/**/*Tests.dll";
        var testAssemblies = GetFiles(testAssembliesMask);
        if (!testAssemblies.Any())
            continue;

        var testResults = testResultsDirectory.Path + "/" + projectAssemblyName + ".xml";
        NUnit3(testAssembliesMask, new NUnit3Settings 
        {
            Results = testResults
        });

        if (AppVeyor.IsRunningOnAppVeyor)
            AppVeyor.UploadTestResults(testResults, AppVeyorTestResultsType.NUnit3);
    }
});

Task("Package")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{
    var files = new [] { "Colourful.dll", "Colourful.pdb", "Colourful.xml" };
    var libDir = packageDirectory + Directory("lib");

    var buildPCL = baseBuildDirectory + Directory("pcl");
    var deployPCL = Directory(libDir) + Directory(@"portable-net45+netcore45+wp8+wpa81+win8");
    CreateDirectory(deployPCL);

    var buildNET35 = baseBuildDirectory + Directory("net35");
    var deployNET35 = Directory(libDir) + Directory("net35");
    CreateDirectory(deployNET35);
    
    var buildNET40 = baseBuildDirectory + Directory("net40");
    var deployNET40 = Directory(libDir) + Directory("net40");
    CreateDirectory(deployNET40);
    
    var buildNET45 = baseBuildDirectory + Directory("net45");
    var deployNET45 = Directory(libDir) + Directory("net45");
    CreateDirectory(deployNET45);

    var nuSpecFiles = new List<NuSpecContent>();

    foreach(var file in files)
    {
        CopyFile(buildPCL + File(file), deployPCL + File(file));
        CopyFile(buildNET35 + File(file), deployNET35 + File(file));
        CopyFile(buildNET40 + File(file), deployNET40 + File(file));
        CopyFile(buildNET45 + File(file), deployNET45 + File(file));
    };

    NuGetPack("./src/Colourful.nuspec", new NuGetPackSettings
    {
        Authors = authors,
        Title = productName,
        Description = description,
        Summary = summary,
        Copyright = copyright,
        Tags = tags,
        OutputDirectory = artifactsDirectory,
        WorkingDirectory = packageDirectory,
        BasePath = packageDirectory,
        Version = versionInformational
    });

    foreach (var artifactFile in GetFiles(artifactsDirectory.ToString() + "/*.nupkg"))
    {
        if (AppVeyor.IsRunningOnAppVeyor)
            AppVeyor.UploadArtifact(artifactFile);
    }
});

///////////////////////////////////////////////////////////////////////////////
// TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Package");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);