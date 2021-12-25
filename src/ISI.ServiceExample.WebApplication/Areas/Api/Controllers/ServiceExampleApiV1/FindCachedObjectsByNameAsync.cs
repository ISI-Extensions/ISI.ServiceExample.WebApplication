using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using ISI.Extensions.Extensions;
using ServiceExampleApiDTOs = ISI.Services.ServiceExample.DataTransferObjects.ServiceExampleApi;
using DOMAINENTITIES = ISI.Services.ServiceExample;
using SERIALIZABLEMODELS = ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels;
using Microsoft.Extensions.Logging;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Controllers
{
	public partial class ServiceExampleApiV1Controller
	{
		[Microsoft.AspNetCore.Mvc.AcceptVerbs(nameof(Microsoft.AspNetCore.Http.HttpMethods.Post))]
		[Microsoft.AspNetCore.Authorization.Authorize(Policy = AuthorizationPolicy.PolicyName)]
		[ISI.Extensions.AspNetCore.NamedRoute(Routes.ServiceExampleApiV1.RouteNames.FindCachedObjectsByName, typeof(Routes.ServiceExampleApiV1), "find-cached-objects-by-name")]
		public async Task<Microsoft.AspNetCore.Mvc.ActionResult<SERIALIZABLEMODELS.FindCachedObjectsByNameResponse>> FindCachedObjectsByNameAsync([Microsoft.AspNetCore.Mvc.FromBody] SERIALIZABLEMODELS.FindCachedObjectsByNameRequest request)
		{
			var response = new SERIALIZABLEMODELS.FindCachedObjectsByNameResponse();
						
			var apiResponse = await ServiceExampleApi.FindCachedObjectsByNameAsync(new ServiceExampleApiDTOs.FindCachedObjectsByNameRequest()
			{
				Names = request.Names.ToNullCheckedArray(),
			});

			response.CachedObjects = apiResponse.CachedObjects.ToNullCheckedArray(Convert);

			return response;
		}
	}
}