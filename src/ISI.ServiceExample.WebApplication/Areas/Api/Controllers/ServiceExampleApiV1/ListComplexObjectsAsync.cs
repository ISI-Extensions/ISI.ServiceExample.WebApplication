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
		[ISI.Extensions.AspNetCore.NamedRoute(Routes.ServiceExampleApiV1.RouteNames.ListComplexObjects, typeof(Routes.ServiceExampleApiV1), "list-complex-objects")]
		public async Task<Microsoft.AspNetCore.Mvc.ActionResult<SERIALIZABLEMODELS.ListComplexObjectsResponse>> ListComplexObjectsAsync([Microsoft.AspNetCore.Mvc.FromBody] SERIALIZABLEMODELS.ListComplexObjectsRequest request)
		{
			var response = new SERIALIZABLEMODELS.ListComplexObjectsResponse();
						
			var apiResponse = await ServiceExampleApi.ListComplexObjectsAsync(new ServiceExampleApiDTOs.ListComplexObjectsRequest());

			response.ComplexObjects = apiResponse.ComplexObjects.ToNullCheckedArray(Convert);

			return response;
		}
	}
}