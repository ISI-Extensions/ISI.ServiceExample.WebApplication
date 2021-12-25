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
		[ISI.Extensions.AspNetCore.NamedRoute(Routes.ServiceExampleApiV1.RouteNames.SetSimpleObject, typeof(Routes.ServiceExampleApiV1), "set-simple-object")]
		public async Task<Microsoft.AspNetCore.Mvc.ActionResult<SERIALIZABLEMODELS.SetSimpleObjectResponse>> SetSimpleObjectAsync([Microsoft.AspNetCore.Mvc.FromBody] SERIALIZABLEMODELS.SetSimpleObjectRequest request)
		{
			var response = new SERIALIZABLEMODELS.SetSimpleObjectResponse();

			var requestedOnUtc = DateTimeStamper.CurrentDateTimeUtc();
			var requestedBy = GetUserUuid().Formatted(GuidExtensions.GuidFormat.WithHyphens);

			DOMAINENTITIES.SimpleObject simpleObject = null;

			if (request.SimpleObjectUuid != Guid.Empty)
			{
				var getSimpleObjectsResponse = await ServiceExampleApi.GetSimpleObjectsAsync(new ServiceExampleApiDTOs.GetSimpleObjectsRequest()
				{
					SimpleObjectUuids = new[] { request.SimpleObjectUuid },
				});

				simpleObject = getSimpleObjectsResponse.SimpleObjects.NullCheckedFirstOrDefault();
			}
			else
			{
				request.SimpleObjectUuid = Guid.NewGuid();
			}

			simpleObject ??= new DOMAINENTITIES.SimpleObject()
			{
				SimpleObjectUuid = request.SimpleObjectUuid,
				CreatedOnUtc = requestedOnUtc,
				CreatedBy = requestedBy,
			};


			simpleObject.Name = request.Name;
			simpleObject.IsActive = request.IsActive;
			simpleObject.ModifiedOnUtc = requestedOnUtc;
			simpleObject.ModifiedBy = requestedBy;



			var apiResponse = await ServiceExampleApi.SetSimpleObjectsAsync(new ServiceExampleApiDTOs.SetSimpleObjectsRequest()
			{
				SimpleObjects = new[] { simpleObject },
			});

			response.SimpleObject = apiResponse.SimpleObjects.NullCheckedSelect(Convert).NullCheckedFirstOrDefault();

			return response;
		}
	}
}