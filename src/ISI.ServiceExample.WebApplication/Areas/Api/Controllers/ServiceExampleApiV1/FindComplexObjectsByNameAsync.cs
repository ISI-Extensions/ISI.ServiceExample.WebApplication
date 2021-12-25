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
		[ISI.Extensions.AspNetCore.NamedRoute(Routes.ServiceExampleApiV1.RouteNames.FindComplexObjectsByName, typeof(Routes.ServiceExampleApiV1), "find-complex-objects-by-name")]
		public async Task<Microsoft.AspNetCore.Mvc.ActionResult<SERIALIZABLEMODELS.FindComplexObjectsByNameResponse>> FindComplexObjectsByNameAsync([Microsoft.AspNetCore.Mvc.FromBody] SERIALIZABLEMODELS.FindComplexObjectsByNameRequest request)
		{
			var response = new SERIALIZABLEMODELS.FindComplexObjectsByNameResponse();
						
			var apiResponse = await ServiceExampleApi.FindComplexObjectsByNameAsync(new ServiceExampleApiDTOs.FindComplexObjectsByNameRequest()
			{
				Names = request.Names.ToNullCheckedArray(),
			});

			response.ComplexObjects = apiResponse.ComplexObjects.ToNullCheckedArray(Convert);

			return response;
		}
	}
}