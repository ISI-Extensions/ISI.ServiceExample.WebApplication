using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels
{
	[DataContract]
	public partial class GetMoreComplexObjectsRequest
	{
		[DataMember(Name = "moreComplexObjectUuids", EmitDefaultValue = false)]
		public Guid[] MoreComplexObjectUuids { get; set; }
	}
}