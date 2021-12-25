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
		public SERIALIZABLEMODELS.SimpleObject Convert(DOMAINENTITIES.SimpleObject source)
		{
			return new SERIALIZABLEMODELS.SimpleObject()
			{
				SimpleObjectUuid = source.SimpleObjectUuid,
				Name = source.Name,
				IsActive = source.IsActive,
				CreatedOnUtc = source.CreatedOnUtc,
				CreatedBy = source.CreatedBy,
				ModifiedOnUtc = source.ModifiedOnUtc,
				ModifiedBy = source.ModifiedBy,
			};
		}
	}
}