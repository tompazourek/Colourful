#r "src/packages/FAKE.3.34.7/tools/FakeLib.dll"

open Fake
open System.IO;

RestorePackages()
 
// Properties
let buildDir = "./build/"
let deployDir = "./deploy/"
 
// version info
let version = environVarOrDefault "PackageVersion" "1.0.1.0"  // or retrieve from CI server
let summary = "Open source .NET library for working with color spaces."
let copyright = "Tomáš Pažourek, 2014"
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

let portableAssemblies = [ "Colourful.dll"; "Colourful.pdb" ]
let net45Assemblies = [ "Colourful.Net45.dll"; "Colourful.Net45.pdb" ]
let allAssemblies = portableAssemblies @ net45Assemblies
let portableTarget = @"portable-net45+netcore45+wp8+wpa81+win8"
let net45Target = "net45"
let libDir = "lib"
 
// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; deployDir]
)
 
Target "Build" (fun _ ->
   !! "./src/**/*.csproj"
     |> MSBuildRelease buildDir "Build"
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

    // Copy all the package files into a package folder
    let allAssemblyPaths =
      allAssemblies
      |> Seq.map (fun assembly -> buildDir + assembly)
      
    // Copy all the package files into a package folder
    allAssemblyPaths |> CopyFiles deployDir

    let portableAssemblyFiles =
      portableAssemblies
      |> List.map(fun a -> (a, Some(Path.Combine(libDir, portableTarget)), None))

    let net45AssemblyFiles = 
      net45Assemblies @ portableAssemblies
      |> List.map(fun a -> (a, Some(Path.Combine(libDir, net45Target)), None))

    NuGet (fun p -> 
        {p with
            Authors = [ "Tom� Pa�ourek" ]
            Project = "Colourful"
            Description = description
            Summary = summary
            Copyright = copyright
            Tags = tags
            OutputPath = deployDir
            WorkingDir = deployDir
            Version = version
            Files = portableAssemblyFiles @ net45AssemblyFiles
            Publish = false }) 
            "./src/Colourful.nuspec"
)

Target "Run" (fun _ -> 
    trace "FAKE build complete"
)
  
// Dependencies
"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Package"
  ==> "Run"
 
// start build
RunTargetOrDefault "Run"