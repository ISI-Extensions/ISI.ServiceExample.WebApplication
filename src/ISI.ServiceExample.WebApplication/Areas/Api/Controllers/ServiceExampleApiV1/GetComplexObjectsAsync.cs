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
		[ISI.Extensions.AspNetCore.NamedRoute(Routes.ServiceExampleApiV1.RouteNames.GetComplexObjects, typeof(Routes.ServiceExampleApiV1), "get-complex-objects")]
		public async Task<Microsoft.AspNetCore.Mvc.ActionResult<SERIALIZABLEMODELS.GetComplexObjectsResponse>> GetComplexObjectsAsync([Microsoft.AspNetCore.Mvc.FromBody] SERIALIZABLEMODELS.GetComplexObjectsRequest request)
		{
			var response = new SERIALIZABLEMODELS.GetComplexObjectsResponse();

			var apiResponse = await ServiceExampleApi.GetComplexObjectsAsync(new ServiceExampleApiDTOs.GetComplexObjectsRequest()
			{
				ComplexObjectUuids = request.ComplexObjectUuids.ToNullCheckedArray(),
			});

			response.ComplexObjects = apiResponse.ComplexObjects.ToNullCheckedArray(Convert);

			return response;
		}
	}
}