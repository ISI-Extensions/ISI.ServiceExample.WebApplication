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
		public SERIALIZABLEMODELS.MoreComplexObject Convert(DOMAINENTITIES.MoreComplexObject source)
		{
			return new SERIALIZABLEMODELS.MoreComplexObject()
			{
				MoreComplexObjectUuid = source.MoreComplexObjectUuid,
				Name = source.Name,
				Widgets = source.Widgets.ToNullCheckedArray(widget =>
				{
					switch (widget)
					{
						case DOMAINENTITIES.MoreComplexObjectWidgetA moreComplexObjectWidgetA:
							return Convert(moreComplexObjectWidgetA);
						case DOMAINENTITIES.MoreComplexObjectWidgetB moreComplexObjectWidgetB:
							return Convert(moreComplexObjectWidgetB);
						default:
							throw new ArgumentOutOfRangeException(nameof(widget));
					}
				}),
				IsActive = source.IsActive,
				CreatedOnUtc = source.CreatedOnUtc,
				CreatedBy = source.CreatedBy,
				ModifiedOnUtc = source.ModifiedOnUtc,
				ModifiedBy = source.ModifiedBy,
			};
		}
	}
}