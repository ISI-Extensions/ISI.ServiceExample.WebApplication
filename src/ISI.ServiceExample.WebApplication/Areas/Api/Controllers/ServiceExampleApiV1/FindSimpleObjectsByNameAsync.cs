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
		[ISI.Extensions.AspNetCore.NamedRoute(Routes.ServiceExampleApiV1.RouteNames.FindSimpleObjectsByName, typeof(Routes.ServiceExampleApiV1), "find-simple-objects-by-name")]
		public async Task<Microsoft.AspNetCore.Mvc.ActionResult<SERIALIZABLEMODELS.FindSimpleObjectsByNameResponse>> FindSimpleObjectsByNameAsync([Microsoft.AspNetCore.Mvc.FromBody] SERIALIZABLEMODELS.FindSimpleObjectsByNameRequest request)
		{
			var response = new SERIALIZABLEMODELS.FindSimpleObjectsByNameResponse();
						
			var apiResponse = await ServiceExampleApi.FindSimpleObjectsByNameAsync(new ServiceExampleApiDTOs.FindSimpleObjectsByNameRequest()
			{
				Names = request.Names.ToNullCheckedArray(),
			});

			response.SimpleObjects = apiResponse.SimpleObjects.ToNullCheckedArray(Convert);

			return response;
		}
	}
}