using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ISI.Extensions.Extensions;

namespace ISI.ServiceExample.WebApplication
{
	public class AuthorizationPolicy : Microsoft.AspNetCore.Authorization.AuthorizationHandler<AuthorizationPolicy>, Microsoft.AspNetCore.Authorization.IAuthorizationRequirement
	{
		public const string PolicyName = nameof(AuthorizationPolicy);

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationPolicy requirement)
		{
			if (!context.User.HasClaim(claim => string.Equals(claim.Type, BearerAuthenticationHandler.Keys.UserUuid, StringComparison.Ordinal)))
			{
				context.Fail();

				return Task.CompletedTask;
			}

			context.Succeed(requirement);

			return Task.CompletedTask;
		}
	}
}
