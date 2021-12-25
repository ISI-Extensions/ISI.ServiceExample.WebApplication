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
		[ISI.Extensions.AspNetCore.NamedRoute(Routes.ServiceExampleApiV1.RouteNames.SetCachedObject, typeof(Routes.ServiceExampleApiV1), "set-cached-object")]
		public async Task<Microsoft.AspNetCore.Mvc.ActionResult<SERIALIZABLEMODELS.SetCachedObjectResponse>> SetCachedObjectAsync([Microsoft.AspNetCore.Mvc.FromBody] SERIALIZABLEMODELS.SetCachedObjectRequest request)
		{
			var response = new SERIALIZABLEMODELS.SetCachedObjectResponse();

			var requestedOnUtc = DateTimeStamper.CurrentDateTimeUtc();
			var requestedBy = GetUserUuid().Formatted(GuidExtensions.GuidFormat.WithHyphens);

			DOMAINENTITIES.CachedObject cachedObject = null;

			if (request.CachedObjectUuid != Guid.Empty)
			{
				var getCachedObjectsResponse = await ServiceExampleApi.GetCachedObjectsAsync(new ServiceExampleApiDTOs.GetCachedObjectsRequest()
				{
					CachedObjectUuids = new[] { request.CachedObjectUuid },
				});

				cachedObject = getCachedObjectsResponse.CachedObjects.NullCheckedFirstOrDefault();
			}
			else
			{
				request.CachedObjectUuid = Guid.NewGuid();
			}

			cachedObject ??= new DOMAINENTITIES.CachedObject()
			{
				CachedObjectUuid = request.CachedObjectUuid,
				CreatedOnUtc = requestedOnUtc,
				CreatedBy = requestedBy,
			};


			cachedObject.CachedObjectUuid = request.CachedObjectUuid;
			cachedObject.Name = request.Name;
			cachedObject.IsActive = request.IsActive;
			cachedObject.ModifiedOnUtc = requestedOnUtc;
			cachedObject.ModifiedBy = requestedBy;



			var apiResponse = await ServiceExampleApi.SetCachedObjectsAsync(new ServiceExampleApiDTOs.SetCachedObjectsRequest()
			{
				CachedObjects = new[] { cachedObject },
			});

			response.CachedObject = apiResponse.CachedObjects.NullCheckedSelect(Convert).NullCheckedFirstOrDefault();

			return response;
		}
	}
}