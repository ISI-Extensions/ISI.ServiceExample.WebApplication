using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISI.Extensions.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Controllers
{
	public abstract partial class Controller : WebApplication.Controllers.Controller
	{
		protected Controller(Microsoft.Extensions.Logging.ILogger logger)
			: base(logger)
		{

		}
	}
}