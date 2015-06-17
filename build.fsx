#r "src/packages/FAKE.3.34.7/tools/FakeLib.dll"

open Fake
open Fake.AssemblyInfoFile
open System.IO;

RestorePackages()
 
// Properties
let buildDir = "./build/"
let deployDir = "./deploy/"
 
// Version info
let version = environVarOrDefault "PackageVersion" (environVarOrDefault "APPVEYOR_BUILD_VERSION" "1.1.0.0")  // or retrieve from CI server
let project = "Colourful"
let authors = [ "Tomáš Pažourek" ]
let summary = "Open source .NET library for working with color spaces."
let copyright = "Tomáš Pažourek, 2015"
let tags = "color space sRGB Adobe RGB delta-e lab luv xyz cielab cieluv ciexyz cct chromatic adaptation conversion difference convert"
let description = "Open source .NET library for working with color spaces. 

Supports these color spaces:

- RGB (various working spaces)
- linear RGB
- CIE XYZ
- CIE xyY
- CIE Lab
- CIE Luv
- CIE LCh (uv)
- CIE LCh (ab)
- Hunter Lab
- LMS (cone response)

Conversions are done correctly using chromatic adaptation and white points.

Other features include: Delta-E color difference (many formulas), Correlated color temperature (CCT) approximation, Conversion between RGB working spaces, Chromatic adaptation (many white points supported).

For more info, visit the project page."


let solution = "./src/Colourful.sln"
 
// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; deployDir]
)

Target "AssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo "./src/Colourful/Properties/AssemblyInfo.cs"
        [
            Attribute.CLSCompliant true
            Attribute.Title project
            Attribute.Description summary
            Attribute.Guid "D11F6BE9-3DCB-45B7-A076-4D476236C3CB"
            Attribute.Product project
            Attribute.Version version
            Attribute.FileVersion version
            Attribute.InformationalVersion version
            Attribute.Copyright copyright
        ]
)

Target "Build" (fun _ -> ())

let buildOutputPCL = buildDir + "pcl"
Target "BuildPCL" (fun _ ->
   !! solution
     |> MSBuild buildOutputPCL "Build" [ "Configuration", "Release (PCL)" ]
     |> Log "AppBuild-Output: "
)

let buildOutputNET35 = buildDir + "net35"
Target "BuildNET35" (fun _ ->
   !! solution
     |> MSBuild buildOutputNET35 "Build" [ "Configuration", "Release (.NET 3.5)" ]
     |> Log "AppBuild-Output: "
)

let buildOutputNET40 = buildDir + "net40"
Target "BuildNET40" (fun _ ->
   !! solution
     |> MSBuild buildOutputNET40 "Build" [ "Configuration", "Release (.NET 4.0)" ]
     |> Log "AppBuild-Output: "
)

let buildOutputNET45 = buildDir
Target "BuildNET45" (fun _ ->
   !! solution
     |> MSBuild buildOutputNET45 "Build" [ "Configuration", "Release" ]
     |> Log "AppBuild-Output: "
)
 
Target "Test" (fun _ ->
    !! (buildDir + "*.Test.dll") 
    ++ (buildDir + "*.Tests.dll")
      |> NUnit (fun p ->
          {p with
             DisableShadowCopy = true;
             OutputFile = buildDir + "TestResults.xml" })
)

Target "Package" (fun _ ->

    let files = [ "Colourful.dll"; "Colourful.pdb"; "Colourful.xml" ]
    let libDir = Path.Combine(deployDir, "lib")

    let deployPCL = Path.Combine(libDir, @"portable-net45+netcore45+wp8+wpa81+win8")
    files
    |> Seq.map (fun file -> Path.Combine(buildOutputPCL, file))
    |> CopyFiles deployPCL

    let deployNET35 =Path.Combine(libDir, "net35")
    files
    |> Seq.map (fun file -> Path.Combine(buildOutputNET35, file))
    |> CopyFiles deployNET35

    let deployNET40 = Path.Combine(libDir, "net40")
    files
    |> Seq.map (fun file -> Path.Combine(buildOutputNET40, file))
    |> CopyFiles deployNET40

    let deployNET45 = Path.Combine(libDir, "net45")
    files
    |> Seq.map (fun file -> Path.Combine(buildOutputNET45, file))
    |> CopyFiles deployNET45

    NuGet (fun p -> 
        {p with
            Authors = authors
            Project = project
            Description = description
            Summary = summary
            Copyright = copyright
            Tags = tags
            OutputPath = deployDir
            WorkingDir = deployDir
            FrameworkAssemblies = [ { FrameworkVersions = ["net35"; "net40"; "net45"]; AssemblyName = "System" };
                                    { FrameworkVersions = ["net35"; "net40"; "net45"]; AssemblyName = "System.Core" };
                                    { FrameworkVersions = ["net35"; "net40"; "net45"]; AssemblyName = "System.Drawing" };
                                    { FrameworkVersions = ["net35"]; AssemblyName = "Microsoft.CSharp" }]
            Version = version
            Publish = false }) 
            "./src/Colourful.nuspec"
)

Target "Run" (fun _ -> 
    trace "FAKE build complete"
)

// Dependencies

"Clean"
  ==> "BuildNET45"

"Clean"
  ==> "BuildNET40"

"Clean"
  ==> "BuildNET35"

"Clean"
  ==> "BuildPCL"

"AssemblyInfo"
  ==> "BuildNET45"

"AssemblyInfo"
  ==> "BuildNET40"

"AssemblyInfo"
  ==> "BuildNET35"

"AssemblyInfo"
  ==> "BuildPCL"

"BuildNET45"
  ==> "Build"

"BuildNET40"
  ==> "Build"

"BuildNET35"
  ==> "Build"

"BuildPCL"
  ==> "Build"

"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Package"
  ==> "Run"

// Start build
RunTargetOrDefault "Run"