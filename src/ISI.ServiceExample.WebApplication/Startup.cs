using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ISI.Extensions.Extensions;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using ISI.Extensions.AspNetCore.Swashbuckle.Extensions;

namespace ISI.ServiceExample.WebApplication
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddControllersWithViews()
				.AddNewtonsoftJson()
				;

			services
				.AddAuthentication(BearerAuthenticationHandler.AuthenticationHandlerName)
				.AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, BearerAuthenticationHandler>(BearerAuthenticationHandler.AuthenticationHandlerName, null)
				;

			services.AddAuthorization(options =>
			{
				options.AddPolicy(AuthorizationPolicy.PolicyName, policy => policy.Requirements.Add(new AuthorizationPolicy()));
			});

			services.AddSwaggerGen(swaggerGenOptions =>
			{
				swaggerGenOptions.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name.TrimEnd("Async") : null);

				swaggerGenOptions.AddBearerTokenAuthentication();

				swaggerGenOptions.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ISI.ServiceExample.WebApplication", Version = "v1" });
			});
			services.AddSwaggerGenNewtonsoftSupport();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostingEnvironment)
		{
			if (webHostingEnvironment.IsDevelopment())
			{
				applicationBuilder.UseDeveloperExceptionPage();
			}

			applicationBuilder.UseSerilogRequestLogging(options =>
			{
				// Customize the message template
				options.MessageTemplate = "Handled {RequestPath}";

				// Emit debug-level events instead of the defaults
				options.GetLevel = (httpContext, elapsed, ex) => Serilog.Events.LogEventLevel.Debug;

				// Attach additional properties to the request completion event
				options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
				{
					diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
					diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
				};
			});

			applicationBuilder.UseDefaultFiles();

			applicationBuilder.UseStaticFiles();

			applicationBuilder.UseRouting();

			//applicationBuilder.UseHttpsRedirection();

			applicationBuilder.UseAuthentication();
			applicationBuilder.UseAuthorization();

			applicationBuilder.UseSwagger();
			applicationBuilder.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ISI.ServiceExample.WebApplication v1"));

			applicationBuilder.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
