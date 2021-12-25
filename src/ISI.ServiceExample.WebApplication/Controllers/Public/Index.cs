using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ISI.ServiceExample.WebApplication.Controllers
{
	public partial class PublicController 
	{
		[Microsoft.AspNetCore.Mvc.AcceptVerbs(nameof(Microsoft.AspNetCore.Http.HttpMethods.Get))]
		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		[ISI.Extensions.AspNetCore.NamedRoute(Routes.Public.RouteNames.Index, typeof(Routes.Public), "")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public virtual async Task<Microsoft.AspNetCore.Mvc.IActionResult> IndexAsync()
		{
			var viewModel = new ISI.ServiceExample.WebApplication.Models.Public.IndexModel();

			viewModel.AssemblyVersion = ISI.Extensions.SystemInformation.GetAssemblyVersion(typeof(Program).Assembly);

			return View(viewModel);
		}
	}
}