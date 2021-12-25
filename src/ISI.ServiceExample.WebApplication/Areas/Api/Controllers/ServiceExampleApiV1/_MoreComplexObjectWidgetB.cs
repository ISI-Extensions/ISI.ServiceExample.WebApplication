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
		public SERIALIZABLEMODELS.IMoreComplexObjectWidget Convert(DOMAINENTITIES.MoreComplexObjectWidgetB source)
		{
			return new SERIALIZABLEMODELS.MoreComplexObjectWidgetB()
			{
				Name = source.Name,
				PropertyB = source.PropertyB,
			};
		}

		public DOMAINENTITIES.IMoreComplexObjectWidget Convert(SERIALIZABLEMODELS.MoreComplexObjectWidgetB source)
		{
			return new DOMAINENTITIES.MoreComplexObjectWidgetB()
			{
				Name = source.Name,
				PropertyB = source.PropertyB,
			};
		}
	}
}