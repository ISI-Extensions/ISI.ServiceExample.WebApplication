#region Copyright & License
/*
Copyright (c) 2023, Integrated Solutions, Inc.
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

		* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
		* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
		* Neither the name of the Integrated Solutions, Inc. nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion
 
using ISI.Extensions.ConfigurationHelper.Extensions;
using ISI.Extensions.DependencyInjection.Extensions;
using ISI.Extensions.Extensions;
using ISI.Extensions.MessageBus.Extensions;
using ISI.Extensions.Ngrok.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISI.ServiceExample.WebApplication
{
	public class Program
	{
		public static int Main(string[] args)
		{
			var configurationBuilder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();

			var configurationsPath = string.Format("Configuration{0}", System.IO.Path.DirectorySeparatorChar);

			var activeEnvironment = configurationBuilder.GetActiveEnvironmentConfig($"{configurationsPath}isi.extensions.environmentsConfig.json");

			configurationBuilder.SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
			configurationBuilder.AddJsonFile("appsettings.json", optional: false);
			configurationBuilder.AddJsonFiles(activeEnvironment.ActiveEnvironments, environment => $"appsettings.{environment}.json");

			var configuration = configurationBuilder.Build();

			Serilog.Log.Logger = UpdateLoggerConfiguration(null, null, configuration, activeEnvironment.ActiveEnvironment).CreateLogger();

			Serilog.Log.Information($"Starting {typeof(Program).Namespace}");
			Serilog.Log.Information($"Version: {ISI.Extensions.SystemInformation.GetAssemblyVersion(typeof(Program).Assembly)}");

			foreach (System.Collections.DictionaryEntry environmentVariable in Environment.GetEnvironmentVariables())
			{
				Serilog.Log.Information($"  EV \"{environmentVariable.Key}\" => \"{environmentVariable.Value}\"");
			}

			Serilog.Log.Information($"ActiveEnvironment: {activeEnvironment.ActiveEnvironment}");
			Serilog.Log.Information($"ActiveEnvironments: {string.Join(", ", activeEnvironment.ActiveEnvironments.Select(e => string.Format("\"{0}\"", e)))}");

			try
			{
				var hostBuilder = CreateHostBuilder(configuration, activeEnvironment.ActiveEnvironment, args);

				var host = hostBuilder.Build();

				host.Services.SetServiceLocator();

				var messageBus = host.Services.GetService<ISI.Extensions.MessageBus.IMessageBus>();

				messageBus.Build(host.Services, new ISI.Extensions.MessageBus.MessageBusBuildRequestCollection()
				{
					ISI.Extensions.Caching.MessageBus.Subscriptions.GetAddSubscriptions,
				});

				messageBus.StartAsync().Wait();

				host.Run();

				return 0;
			}
			catch (Exception exception)
			{
				Serilog.Log.Fatal(exception, "Host terminated unexpectedly");
				return 1;
			}
			finally
			{
				Serilog.Log.CloseAndFlush();
			}
		}

		public static Serilog.LoggerConfiguration UpdateLoggerConfiguration(Serilog.LoggerConfiguration loggerConfiguration, IServiceProvider serviceProvider, IConfigurationRoot configuration, string environment)
		{
			loggerConfiguration ??= new LoggerConfiguration();

			loggerConfiguration
				.MinimumLevel.Verbose()
				.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
				.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
				.MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
				.MinimumLevel.Override("Microsoft.AspNetCore.Authentication", Serilog.Events.LogEventLevel.Information)
				.Enrich.FromLogContext()
				.Enrich.WithEnvironmentUserName()
				.Enrich.WithMachineName()
				.Enrich.WithProcessId()
				.Enrich.WithThreadId()
				.Enrich.WithExceptionDetails()
				.Enrich.WithProperty("Environment", environment)
				.ReadFrom.Configuration(configuration)
				;

			if (serviceProvider != null)
			{
				loggerConfiguration.ReadFrom.Services(serviceProvider);
			}

			loggerConfiguration.WriteTo.Console();

			var webApplicationConfiguration = new ISI.ServiceExample.WebApplication.Configuration();
			configuration.Bind(ISI.ServiceExample.WebApplication.Configuration.ConfigurationSectionName, webApplicationConfiguration);

			if (!string.IsNullOrWhiteSpace(webApplicationConfiguration?.ElasticsearchLogging?.NodeUrl))
			{
				if (!string.IsNullOrWhiteSpace(webApplicationConfiguration?.ElasticsearchLogging?.UserName))
				{
					loggerConfiguration.WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(webApplicationConfiguration.ElasticsearchLogging.NodeUrl))
					{
						AutoRegisterTemplate = true,
						AutoRegisterTemplateVersion = Serilog.Sinks.Elasticsearch.AutoRegisterTemplateVersion.ESv6,
						IndexFormat = webApplicationConfiguration.ElasticsearchLogging.IndexFormat,
						ModifyConnectionSettings = connectionConfiguration => connectionConfiguration.BasicAuthentication(webApplicationConfiguration.ElasticsearchLogging.UserName, webApplicationConfiguration.ElasticsearchLogging.Password),
					});
				}
				else
				{
					loggerConfiguration.WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(webApplicationConfiguration.ElasticsearchLogging.NodeUrl))
					{
						AutoRegisterTemplate = true,
						AutoRegisterTemplateVersion = Serilog.Sinks.Elasticsearch.AutoRegisterTemplateVersion.ESv6,
						IndexFormat = webApplicationConfiguration.ElasticsearchLogging.IndexFormat,
					});
				}
			}

			return loggerConfiguration;
		}

		public static IHostBuilder CreateHostBuilder(IConfigurationRoot configuration, string environment, string[] args)
		{
			return Host
				.CreateDefaultBuilder(args)
				.UseSerilog((context, services, loggerConfiguration) => UpdateLoggerConfiguration(loggerConfiguration, services, configuration, environment))
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.ConfigureServices(services =>
						{
							services
								.AddOptions()
								.AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(configuration)
								.AddAllConfigurations(configuration)
								.AddConfiguration<Microsoft.Extensions.Hosting.ConsoleLifetimeOptions>(configuration)
								.AddConfiguration<Microsoft.Extensions.Hosting.HostOptions>(configuration)

								.AddConfigurationRegistrations(configuration)
								.ProcessServiceRegistrars()

								.AddSingleton<ISI.Extensions.JsonSerialization.Newtonsoft.NewtonsoftJsonSerializer>()
								;

							services
								//.AddSingleton<Microsoft.Extensions.Logging.ILoggerFactory, Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory>()
								//.AddSingleton<Microsoft.Extensions.Logging.ILoggerFactory, Microsoft.Extensions.Logging.LoggerFactory>()
								//.AddLogging(builder => builder
								//	.AddConsole()
								////.AddFilter(level => level >= Microsoft.Extensions.Logging.LogLevel.Information)
								//)
								.AddTransient<Microsoft.Extensions.Logging.ILogger>(serviceProvider => serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>())
								.AddSingleton<Microsoft.Extensions.FileProviders.IFileProvider>(provider => provider.GetService<Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Builder.StaticFileOptions>>().Value.FileProvider)

								.AddSingleton<Microsoft.Extensions.Caching.Memory.IMemoryCache>(provider => new Microsoft.Extensions.Caching.Memory.MemoryCache(new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions()))
								.AddSingleton<ISI.Extensions.Caching.ICacheManager, ISI.Extensions.Caching.CacheManager<Microsoft.Extensions.Caching.Memory.IMemoryCache>>()

								.AddMessageBus(configuration);
							;
						});

					webBuilder.UseContentRoot(System.IO.Directory.GetCurrentDirectory());

					webBuilder.UseStartup<Startup>();

#if DEBUG
					webBuilder.UseIISIntegration();
					//webBuilder.UseKestrel(kestrelServerOptions =>
					//{
					//	kestrelServerOptions.Listen(System.Net.IPAddress.Loopback, 5000);
					//	kestrelServerOptions.Listen(System.Net.IPAddress.Loopback, 5001, listenOptions =>
					//	{
					//		listenOptions.UseHttps(@"S:\spconnect_development.pfx", "spconnect", configureOptions => configureOptions.SslProtocols = System.Security.Authentication.SslProtocols.Tls12);
					//	});
					//});
#else
					webBuilder.UseKestrel(kestrelServerOptions =>
						{
							kestrelServerOptions.ListenAnyIP(Environment.GetEnvironmentVariable("PORT").ToInt());
							//kestrelServerOptions.ListenAnyIP(80);
							//kestrelServerOptions.ListenAnyIP(443, listenOptions => listenOptions.UseHttps());
						})
#endif
					;
				})
				//.UseServiceProviderFactory(new ISI.Extensions.DependencyInjection.ServiceProviderFactory(configuration))
#if DEBUG
				.UseNGrok()
#endif
				;
		}
	}
}