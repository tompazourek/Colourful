///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// PREPARATION
///////////////////////////////////////////////////////////////////////////////

// basic information
var productName = "Colourful";
var companyName = "Tomáš Pažourek";
var copyright = string.Format("Copyright \u00A9 {0}, {1}", companyName, DateTime.Now.Year);
var summary = "Open source .NET library for working with color spaces.";

// Get whether or not this is a local build
var local = BuildSystem.IsLocalBuild;
var versionInformational = "1.1.2"; // used for NuGet package version as well
var buildNumber = AppVeyor.IsRunningOnAppVeyor ? AppVeyor.Environment.Build.Number.ToString() : "0";
var versionBuild = versionInformational + "." + buildNumber;

// Define directories and files
var sourceDirectory = Directory("./src");
var outputDirectory = Directory("./output");
var artifactsDirectory = outputDirectory + Directory("artifacts");
var testResultsDirectory = outputDirectory + Directory("testresults");
var solutionDirectory = Directory(".");
var solutionFile = GetFiles("./*.sln").First();
var projectDirectories = GetFiles(solutionDirectory.ToString() + "/**/*.xproj").Select(x => x.GetDirectory());
var baseBuildDirectory = sourceDirectory + Directory("Colourful/bin/");

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
Task("CleanProjectOutputs")
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
Task("CleanOutputDirectory")
    .Does(() => 
{
    if (!DirectoryExists(outputDirectory))
        return;

    DeleteDirectory(outputDirectory, recursive: true);
});

// Clean all relevant directories
Task("Clean")
    .IsDependentOn("UpdateAppVeyorBuildNumber")
    .IsDependentOn("CleanProjectOutputs")
    .IsDependentOn("CleanOutputDirectory");

// Creates all necessary directories
Task("CreateDirectories")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var directories = new [] { outputDirectory, artifactsDirectory, testResultsDirectory };
    foreach (var directory in directories)
    {
        if (DirectoryExists(directory))
            continue;

        CreateDirectory(directory);
    };
});

// Restores NuGet packages for the whole solution
Task("RestoreNuGetPackages")
    .IsDependentOn("CreateDirectories")
    .Does(() =>
{
    DotNetCoreRestore();
});

// Patches the common assembly info files in solution
Task("PatchAssemblyInfo")
    .IsDependentOn("RestoreNuGetPackages")
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
Task("UpdateAppVeyorBuildNumber")
    .WithCriteria(() => AppVeyor.IsRunningOnAppVeyor)
    .Does(() =>
{
    AppVeyor.UpdateBuildVersion(versionBuild);
});

// Builds solution
Task("Build")
    .IsDependentOn("PreBuild")
    .Does(() => 
{
    DotNetCoreBuild(MakeAbsolute(File("./src/Colourful/project.json")).ToString(), new DotNetCoreBuildSettings {
        Configuration = configuration
    });
});

Task("PreBuild")
    .IsDependentOn("Clean")
    .IsDependentOn("PatchAssemblyInfo");

Task("RunUnitTests")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest(MakeAbsolute(File("./test/Colourful.Tests/project.json")).ToString(), new DotNetCoreTestSettings
    {
        Configuration = configuration,
        WorkingDirectory = testResultsDirectory.ToString()
    });

    var testResults = testResultsDirectory.Path + "/TestResult.xml";

    if (AppVeyor.IsRunningOnAppVeyor)
            AppVeyor.UploadTestResults(testResults, AppVeyorTestResultsType.NUnit3);
});

Task("Package")
    .IsDependentOn("RunUnitTests")
    .Does(() =>
{
    DotNetCorePack(MakeAbsolute(File("./src/Colourful/project.json")).ToString(), new DotNetCorePackSettings
    {
        Configuration = configuration,
        OutputDirectory = artifactsDirectory.ToString()
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