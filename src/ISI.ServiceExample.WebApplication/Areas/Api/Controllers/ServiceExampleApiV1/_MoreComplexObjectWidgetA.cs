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
		public SERIALIZABLEMODELS.MoreComplexObjectWidgetA Convert(DOMAINENTITIES.MoreComplexObjectWidgetA source)
		{
			return new SERIALIZABLEMODELS.MoreComplexObjectWidgetA()
			{
				Name = source.Name,
				PropertyA = source.PropertyA,
			};
		}

		public DOMAINENTITIES.IMoreComplexObjectWidget Convert(SERIALIZABLEMODELS.MoreComplexObjectWidgetA source)
		{
			return new DOMAINENTITIES.MoreComplexObjectWidgetA()
			{
				Name = source.Name,
				PropertyA = source.PropertyA,
			};
		}
	}
}