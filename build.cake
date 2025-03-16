#addin nuget:?package=Cake.FileHelpers
#addin nuget:?package=ISI.Cake.AddIn&loaddependencies=true
#addin nuget:?package=Cake.Docker&version=1.0.0

//mklink /D Secrets S:\
var settingsFullName = System.IO.Path.Combine(System.Environment.GetEnvironmentVariable("LocalAppData"), "Secrets", "ISI.keyValue");
var settings = GetSettings(settingsFullName);

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var solutionFile = File("./src/ISI.ServiceExample.WebApplication.slnx");
var solution = ParseSolution(solutionFile);
var rootProjectFile = File("./src/ISI.ServiceExample.WebApplication/ISI.ServiceExample.WebApplication.csproj");
var rootAssemblyVersionKey = "ISI.ServiceExample";
var artifactName = "ISI.ServiceExample.WindowsService";

var buildDateTime = DateTime.UtcNow;
var buildDateTimeStamp = GetDateTimeStamp(buildDateTime);
var buildRevision = GetBuildRevision(buildDateTime);

var assemblyVersions = GetAssemblyVersionFiles(rootAssemblyVersionKey, buildRevision);
var assemblyVersion = assemblyVersions[rootAssemblyVersionKey].AssemblyVersion;

var buildDateTimeStampVersion = new ISI.Extensions.Scm.DateTimeStampVersion(buildDateTimeStamp, assemblyVersions[rootAssemblyVersionKey].AssemblyVersion);

Information("BuildDateTimeStampVersion: {0}", buildDateTimeStampVersion);

Task("Clean")
	.Does(() =>
	{
		Information("Cleaning Projects ...");

		foreach(var projectPath in new HashSet<string>(solution.Projects.Select(p => p.Path.GetDirectory().ToString())))
		{
			Information("Cleaning {0}", projectPath);
			CleanDirectories(projectPath + "/**/bin/" + configuration);
			CleanDirectories(projectPath + "/**/obj/" + configuration);
		}
	});

Task("NugetPackageRestore")
	.IsDependentOn("Clean")
	.Does(() =>
	{
		Information("Restoring Nuget Packages ...");
		using(GetNugetLock())
		{
			RestoreNugetPackages(solutionFile);
		}
	});

Task("Build")
	.IsDependentOn("NugetPackageRestore")
	.Does(() => 
	{
		using(SetAssemblyVersionFiles(assemblyVersions))
		{
			DotNetBuild(solutionFile, new DotNetBuildSettings()
			{
				Configuration = configuration,
			});
		}
	});

Task("Publish")
	.IsDependentOn("Build")
	.Does(() =>
	{
		//DockerTag("isiserviceexamplewebapplication", "repo/isiserviceexamplewebapplication:latest");
		//DockerPush("repo/isiserviceexamplewebapplication:latest");
	});

Task("Production-Deploy")
	.Does(() => 
	{
		//DockerTag("isiapiwebapplication", "repo/isiserviceexamplewebapplication:production");
		//DockerTag("repo/isiserviceexamplewebapplication:latest", "repo/isiserviceexamplewebapplication:production");
		//DockerPush("repo/isiserviceexamplewebapplication:production");
	});

Task("Default")
	.IsDependentOn("Publish")
	.Does(() => 
	{
		Information("No target provided. Starting default task");
	});

using(GetSolutionLock())
{
	RunTarget(target);
}