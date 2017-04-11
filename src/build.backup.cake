// Install addins.

// Install tools.

// Load other scripts.

//////////////////////////////////////////////////////////////////////
// PARAMETERS
//////////////////////////////////////////////////////////////////////



///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectories("./.artifacts");
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    
    DotNetCoreRestore("./Core/ExistAll.Settings");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
        DotNetCoreBuild("./Core/ExistAll.Settings", new DotNetCoreBuildSettings {
            Configuration = "Release"
        });
    
});

Task("Create-NuGet-Packages")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetCorePack("./Core/ExistAll.Settings", new DotNetCorePackSettings 
        {
            OutputDirectory = "./.artifacts",
            Verbose = false,
            Configuration = "Release"
        });
    });

// Task("Publish-NuGet")
//     .IsDependentOn("Create-NuGet-Packages")
//     .WithCriteria(() => parameters.ShouldPublish)
//     .Does(() =>
// {
//     // Resolve the API key.
//     var apiKey = EnvironmentVariable("NUGET_API_KEY");
//     if(string.IsNullOrEmpty(apiKey)) {
//         throw new InvalidOperationException("Could not resolve NuGet API key.");
//     }

//     // Resolve the API url.
//     var apiUrl = EnvironmentVariable("NUGET_API_URL");
//     if(string.IsNullOrEmpty(apiUrl)) {
//         throw new InvalidOperationException("Could not resolve NuGet API url.");
//     }

//     foreach(var package in parameters.Packages.Nuget)
//     {
//         // Push the package.
//         NuGetPush(package.PackagePath, new NuGetPushSettings {
//           ApiKey = apiKey,
//           Source = apiUrl
//         });
//     }
// })
// .OnError(exception =>
// {
//     Information("Publish-NuGet Task failed, but continuing with next Task...");
//     publishingError = true;
// });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

// Task("Package")
//   .IsDependentOn("Zip-Files")
//   .IsDependentOn("Create-NuGet-Packages");

Task("Default")
  .IsDependentOn("Create-NuGet-Packages");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget("Default");