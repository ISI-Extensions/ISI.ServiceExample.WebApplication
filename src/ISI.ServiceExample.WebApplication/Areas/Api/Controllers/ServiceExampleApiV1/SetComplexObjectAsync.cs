using ISI.Extensions.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DOMAINENTITIES = ISI.Services.ServiceExample;
using SERIALIZABLEMODELS = ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels;
using ServiceExampleApiDTOs = ISI.Services.ServiceExample.DataTransferObjects.ServiceExampleApi;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Controllers
{
	public partial class ServiceExampleApiV1Controller
	{
		[Microsoft.AspNetCore.Mvc.AcceptVerbs(nameof(Microsoft.AspNetCore.Http.HttpMethods.Post))]
		[Microsoft.AspNetCore.Authorization.Authorize(Policy = AuthorizationPolicy.PolicyName)]
		[ISI.Extensions.AspNetCore.NamedRoute(Routes.ServiceExampleApiV1.RouteNames.SetComplexObject, typeof(Routes.ServiceExampleApiV1), "set-complex-object")]
		public async Task<Microsoft.AspNetCore.Mvc.ActionResult<SERIALIZABLEMODELS.SetComplexObjectResponse>> SetComplexObjectAsync([Microsoft.AspNetCore.Mvc.FromBody] SERIALIZABLEMODELS.SetComplexObjectRequest request)
		{
			var response = new SERIALIZABLEMODELS.SetComplexObjectResponse();

			var requestedOnUtc = DateTimeStamper.CurrentDateTimeUtc();
			var requestedBy = GetUserUuid().Formatted(GuidExtensions.GuidFormat.WithHyphens);

			DOMAINENTITIES.ComplexObject complexObject = null;

			if (request.ComplexObjectUuid != Guid.Empty)
			{
				var getComplexObjectsResponse = await ServiceExampleApi.GetComplexObjectsAsync(new ServiceExampleApiDTOs.GetComplexObjectsRequest()
				{
					ComplexObjectUuids = new[] { request.ComplexObjectUuid },
				});

				complexObject = getComplexObjectsResponse.ComplexObjects.NullCheckedFirstOrDefault();
			}
			else
			{
				request.ComplexObjectUuid = Guid.NewGuid();
			}

			complexObject ??= new DOMAINENTITIES.ComplexObject()
			{
				ComplexObjectUuid = request.ComplexObjectUuid,
				CreatedOnUtc = requestedOnUtc,
				CreatedBy = requestedBy,
			};


			complexObject.ComplexObjectUuid = request.ComplexObjectUuid;
			complexObject.Name = request.Name;
			complexObject.IsActive = request.IsActive;
			complexObject.ModifiedOnUtc = requestedOnUtc;
			complexObject.ModifiedBy = requestedBy;



			var apiResponse = await ServiceExampleApi.SetComplexObjectsAsync(new ServiceExampleApiDTOs.SetComplexObjectsRequest()
			{
				ComplexObjects = new[] { complexObject },
			});

			response.ComplexObject = apiResponse.ComplexObjects.NullCheckedSelect(Convert).NullCheckedFirstOrDefault();

			return response;
		}
	}
}