#addin nuget:?package=Cake.FileHelpers
#addin nuget:?package=ISI.Cake.AddIn&loaddependencies=true
#addin nuget:?package=Cake.Docker&version=1.0.0

//mklink /D Secrets S:\
var settingsFullName = System.IO.Path.Combine(System.Environment.GetEnvironmentVariable("LocalAppData"), "Secrets", "ISI.keyValue");
var settings = GetSettings(settingsFullName);

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var solutionPath = File("./ISI.ServiceExample.WebApplication.sln");
var solution = ParseSolution(solutionPath);

var assemblyVersionFile = File("./ISI.ServiceExample.Version.cs");

var buildDateTime = DateTime.UtcNow;
var buildDateTimeStamp = GetDateTimeStamp(buildDateTime);
var buildRevision = GetBuildRevision(buildDateTime);
var assemblyVersion = GetAssemblyVersion(ParseAssemblyInfo(assemblyVersionFile).AssemblyVersion, buildRevision);
Information("AssemblyVersion: {0}", assemblyVersion);

Task("Clean")
	.Does(() =>
	{
		foreach(var projectPath in solution.Projects.Select(p => p.Path.GetDirectory()))
		{
			Information("Cleaning {0}", projectPath);
			CleanDirectories(projectPath + "/**/bin/" + configuration);
			CleanDirectories(projectPath + "/**/obj/" + configuration);
		}

		Information("Cleaning Projects ...");
	});

Task("NugetPackageRestore")
	.IsDependentOn("Clean")
	.Does(() =>
	{
		Information("Restoring Nuget Packages ...");
		NuGetRestore(solutionPath);
	});

Task("Build")
	.IsDependentOn("NugetPackageRestore")
	.Does(() => 
	{
		CreateAssemblyInfo(assemblyVersionFile, new AssemblyInfoSettings()
		{
			Version = assemblyVersion,
		});

		MSBuild("ISI.ServiceExample.WebApplication\\ISI.ServiceExample.WebApplication.csproj", configurator => configurator
			.SetVerbosity(Verbosity.Verbose)
			.SetConfiguration(configuration)
			.WithProperty("DeployOnBuild", "true")
			.WithProperty("PublishProfile", "docker.pubxml")
			.SetMaxCpuCount(0)
			.SetNodeReuse(false)
			.WithTarget("Rebuild"));

		CreateAssemblyInfo(assemblyVersionFile, new AssemblyInfoSettings()
		{
			Version = GetAssemblyVersion(assemblyVersion, "*"),
		});
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