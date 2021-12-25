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
		[ISI.Extensions.AspNetCore.NamedRoute(Routes.ServiceExampleApiV1.RouteNames.SetMoreComplexObject, typeof(Routes.ServiceExampleApiV1), "set-more-complex-object")]
		public async Task<Microsoft.AspNetCore.Mvc.ActionResult<SERIALIZABLEMODELS.SetMoreComplexObjectResponse>> SetMoreComplexObjectAsync([Microsoft.AspNetCore.Mvc.FromBody] SERIALIZABLEMODELS.SetMoreComplexObjectRequest request)
		{
			var response = new SERIALIZABLEMODELS.SetMoreComplexObjectResponse();

			var requestedOnUtc = DateTimeStamper.CurrentDateTimeUtc();
			var requestedBy = GetUserUuid().Formatted(GuidExtensions.GuidFormat.WithHyphens);

			DOMAINENTITIES.MoreComplexObject moreComplexObject = null;

			if (request.MoreComplexObjectUuid != Guid.Empty)
			{
				var getMoreComplexObjectsResponse = await ServiceExampleApi.GetMoreComplexObjectsAsync(new ServiceExampleApiDTOs.GetMoreComplexObjectsRequest()
				{
					MoreComplexObjectUuids = new[] { request.MoreComplexObjectUuid },
				});

				moreComplexObject = getMoreComplexObjectsResponse.MoreComplexObjects.NullCheckedFirstOrDefault();
			}
			else
			{
				request.MoreComplexObjectUuid = Guid.NewGuid();
			}

			moreComplexObject ??= new DOMAINENTITIES.MoreComplexObject()
			{
				MoreComplexObjectUuid = request.MoreComplexObjectUuid,
				CreatedOnUtc = requestedOnUtc,
				CreatedBy = requestedBy,
			};


			moreComplexObject.MoreComplexObjectUuid = request.MoreComplexObjectUuid;
			moreComplexObject.Name = request.Name;
			moreComplexObject.Widgets = request.Widgets.ToNullCheckedArray(widget =>
			{
				switch (widget)
				{
					case SERIALIZABLEMODELS.MoreComplexObjectWidgetA moreComplexObjectWidgetA:
						return Convert(moreComplexObjectWidgetA);
					case SERIALIZABLEMODELS.MoreComplexObjectWidgetB moreComplexObjectWidgetB:
						return Convert(moreComplexObjectWidgetB);
					default:
						throw new ArgumentOutOfRangeException(nameof(widget));
				}
			});
			moreComplexObject.IsActive = request.IsActive;
			moreComplexObject.ModifiedOnUtc = requestedOnUtc;
			moreComplexObject.ModifiedBy = requestedBy;



			var apiResponse = await ServiceExampleApi.SetMoreComplexObjectsAsync(new ServiceExampleApiDTOs.SetMoreComplexObjectsRequest()
			{
				MoreComplexObjects = new[] { moreComplexObject },
			});

			response.MoreComplexObject = apiResponse.MoreComplexObjects.NullCheckedSelect(Convert).NullCheckedFirstOrDefault();

			return response;
		}
	}
}