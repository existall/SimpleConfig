#tool "nuget:?package=GitVersion.CommandLine&prerelease"
#addin "Cake.FileHelpers"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var apikey = FileReadText("./params.json");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var solutionFile = "./ExistAll.SimpleConfig.sln";
string semVersion = null;

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
	Information(apikey);
	DeleteDirectoryIfExists("./artifacts");
	DeleteFiles("./*/bin/*/*.nupkg");
});

Task("Version")
    .Does(() =>
{
    if (AppVeyor.IsRunningOnAppVeyor)
    {
		GitVersion(new GitVersionSettings 
		{
			OutputType = GitVersionOutput.BuildServer,
			UpdateAssemblyInfo = true
		});
	}

    var result = GitVersion(new GitVersionSettings 
	{
		OutputType = GitVersionOutput.Json
    });

	semVersion = result.NuGetVersionV2;
});

Task("NuGet-Restore")
    .Does(() =>
		{
			MSBuild(solutionFile, new MSBuildSettings()
			.SetConfiguration(configuration)
			.WithTarget("restore"));
		});

Task("Build")
    .IsDependentOn("NuGet-Restore")
    .Does(() =>
	{
		DotNetCoreBuild(solutionFile, new DotNetCoreBuildSettings
		{
			Configuration = configuration
		});
	});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
	foreach (var file in GetFiles("./Tests/*/*.csproj"))
	{
		DotNetCoreTest(file.FullPath, new DotNetCoreTestSettings
		{
			Configuration = configuration,
			NoBuild = true
		});
	}
});

Task("Package")
	.IsDependentOn("Clean")
	//.IsDependentOn("Version")
	.IsDependentOn("Test")
    .Does(() =>
{
	foreach (var file in GetFiles("./Core/*/*.csproj"))
	{
		MSBuild(file, new MSBuildSettings()
			.SetConfiguration(configuration)
			.WithProperty("IncludeSymbols", "true")
			.WithProperty("IncludeSource", "true")
			//.WithProperty("Version", semVersion)
			.WithTarget("pack")
		);
	}

	CreateDirectoryIfNotExists("./artifacts");
	MoveFiles("./Core/*/bin/*/*.nupkg", "./artifacts");
});

// Task("Upload-Artifacts")
//     .IsDependentOn("Package")
//     .Does(() =>
// {
// 	foreach (var file in GetFiles("./artifacts/*.nupkg"))
// 	{
// 		if (AppVeyor.IsRunningOnAppVeyor)
// 		{
// 			AppVeyor.UploadArtifact(file);
// 		}
// 	}
// });

// Task("NuGet-Push")
//     .IsDependentOn("Package")
//     .Does(() =>
// {
// 	if (AppVeyor.IsRunningOnAppVeyor && EnvironmentVariable("APPVEYOR_REPO_TAG") == "true")
// 	{
// 		foreach (var file in GetFiles("./artifacts/*.nupkg"))
// 		{
// 			if (file.ToString().Contains(".symbols.nupkg"))
// 			{
// 				NuGetPush(file, new NuGetPushSettings 
// 				{
// 					Source = "https://www.myget.org/F/baunegaard/symbols/api/v2/package",
// 					ApiKey = EnvironmentVariable("MYGET_API_KEY")
// 				});
// 			}
// 			else
// 			{
// 				NuGetPush(file, new NuGetPushSettings 
// 				{
// 					Source = "https://www.myget.org/F/baunegaard/api/v2/package",
// 					ApiKey = EnvironmentVariable("MYGET_API_KEY")
// 				});
// 			}
// 		}
// 	}
// });

//////////////////////////////////////////////////////////////////////
// HELPERS
//////////////////////////////////////////////////////////////////////

void CreateDirectoryIfNotExists(string path)
{
	var directory = Directory(path);
	if (!DirectoryExists(directory))
	{
		CreateDirectory(directory);
	}
}

void DeleteDirectoryIfExists(string path)
{
	var directory = Directory(path);
	if (DirectoryExists(directory))
	{
		DeleteDirectory(directory, true);
	}
}

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
	.IsDependentOn("Package");

    //.IsDependentOn("Upload-Artifacts")
	//.IsDependentOn("NuGet-Push");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);