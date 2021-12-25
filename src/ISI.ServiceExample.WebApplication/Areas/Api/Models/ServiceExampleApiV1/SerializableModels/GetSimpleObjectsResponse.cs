using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels
{
	[DataContract]
	public partial class GetSimpleObjectsResponse
	{
		[DataMember(Name = "simpleObjects", EmitDefaultValue = false)]
		public SimpleObject[] SimpleObjects { get; set; }
	}
}