using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ISI.ServiceExample.WebApplication.Controllers
{
	public partial class PublicController : Controller
	{
		protected IServiceProvider ServiceProvider { get; }

		public PublicController(
			IServiceProvider serviceProvider,
			Microsoft.Extensions.Logging.ILogger logger)
			: base(logger)
		{
			ServiceProvider = serviceProvider;
		}
	}
}