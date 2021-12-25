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
		[ISI.Extensions.AspNetCore.NamedRoute(Routes.ServiceExampleApiV1.RouteNames.GetMoreComplexObjects, typeof(Routes.ServiceExampleApiV1), "get-more-complex-objects")]
		public async Task<Microsoft.AspNetCore.Mvc.ActionResult<SERIALIZABLEMODELS.GetMoreComplexObjectsResponse>> GetMoreComplexObjectsAsync([Microsoft.AspNetCore.Mvc.FromBody] SERIALIZABLEMODELS.GetMoreComplexObjectsRequest request)
		{
			var response = new SERIALIZABLEMODELS.GetMoreComplexObjectsResponse();
						
			var apiResponse = await ServiceExampleApi.GetMoreComplexObjectsAsync(new ServiceExampleApiDTOs.GetMoreComplexObjectsRequest()
			{
				MoreComplexObjectUuids = request.MoreComplexObjectUuids.ToNullCheckedArray(),
			});

			response.MoreComplexObjects = apiResponse.MoreComplexObjects.ToNullCheckedArray(Convert);

			return response;
		}
	}
}