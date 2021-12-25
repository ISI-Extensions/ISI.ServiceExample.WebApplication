using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ISI.Extensions.Extensions;

namespace ISI.ServiceExample.WebApplication
{
	public class BearerAuthenticationHandler : Microsoft.AspNetCore.Authentication.AuthenticationHandler<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions>
	{
		public const string AuthenticationHandlerName = nameof(BearerAuthenticationHandler);

		public class Keys
		{
			public const string Authorization = nameof(Authorization);
			public const string Bearer = nameof(Bearer);
			public const string UserUuid = nameof(UserUuid);
		}

		public BearerAuthenticationHandler(
			Microsoft.Extensions.Options.IOptionsMonitor<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions> options,
			Microsoft.Extensions.Logging.ILoggerFactory logger,
			System.Text.Encodings.Web.UrlEncoder encoder,
			Microsoft.AspNetCore.Authentication.ISystemClock clock)
			: base(options, logger, encoder, clock)
		{
		}

		protected override async Task<Microsoft.AspNetCore.Authentication.AuthenticateResult> HandleAuthenticateAsync()
		{
			// skip authentication if endpoint has [AllowAnonymous] attribute
			var endpoint = Context.GetEndpoint();
			if (endpoint?.Metadata?.GetMetadata<Microsoft.AspNetCore.Authorization.IAllowAnonymous>() != null)
			{
				return Microsoft.AspNetCore.Authentication.AuthenticateResult.NoResult();
			}

			if (!Request.Headers.ContainsKey(Keys.Authorization))
			{
				return Microsoft.AspNetCore.Authentication.AuthenticateResult.Fail("Missing Authorization Header");
			}

			try
			{
				var authHeader = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(Request.Headers[Keys.Authorization]);

				//obviously this should be coming from an api call
				Guid? userUuid = Guid.Parse("3798bc4f-c00d-438a-9437-b55c4e272732");

				if (userUuid.HasValue)
				{
					Request.HttpContext.Items[Keys.UserUuid] = userUuid.Formatted(GuidExtensions.GuidFormat.WithHyphens);

					var principal = new System.Security.Claims.ClaimsPrincipal(new[]
					{
						new System.Security.Claims.ClaimsIdentity(new[]
						{
							new System.Security.Claims.Claim(Keys.UserUuid, userUuid.Formatted(GuidExtensions.GuidFormat.WithHyphens)),
						}),
					});

					var ticket = new Microsoft.AspNetCore.Authentication.AuthenticationTicket(principal, Scheme.Name);

					return Microsoft.AspNetCore.Authentication.AuthenticateResult.Success(ticket);
				}

				return Microsoft.AspNetCore.Authentication.AuthenticateResult.Fail("Invalid Token");
			}
			catch
			{
				return Microsoft.AspNetCore.Authentication.AuthenticateResult.Fail("Invalid Authorization Header");
			}
		}
	}
}