#region Copyright & License
/*
Copyright (c) 2023, Integrated Solutions, Inc.
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

		* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
		* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
		* Neither the name of the Integrated Solutions, Inc. nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion
 
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