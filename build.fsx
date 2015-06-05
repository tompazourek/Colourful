#r "src/packages/FAKE.3.34.7/tools/FakeLib.dll"

open Fake
RestorePackages()
 
// Properties
let buildDir = "./build/"
let deployDir = "./deploy/"
 
// version info
let version = "1.0.2.0"  // or retrieve from CI server
 
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

Target "Run" (fun _ -> ())
  
// Dependencies
"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Run"
 
// start build
RunTargetOrDefault "Run"