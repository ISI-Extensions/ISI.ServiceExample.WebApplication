using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISI.Extensions.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ISI.ServiceExample.WebApplication.Controllers
{
	public abstract partial class Controller : Microsoft.AspNetCore.Mvc.Controller
	{
		protected Microsoft.Extensions.Logging.ILogger Logger { get; }

		protected Controller(
			Microsoft.Extensions.Logging.ILogger logger)
		{
			Logger = logger;
		}

		protected Guid GetUserUuid()
		{
			if(HttpContext.Items.TryGetValue(BearerAuthenticationHandler.Keys.UserUuid, out var value))
			{
				return string.Format("{0}", value).ToGuid();
			}

			return Guid.Empty;
		}
	}
}
