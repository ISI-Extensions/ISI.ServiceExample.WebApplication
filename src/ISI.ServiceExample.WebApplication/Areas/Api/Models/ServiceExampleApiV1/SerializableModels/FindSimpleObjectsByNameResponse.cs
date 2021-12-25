using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels
{
	[DataContract]
	public partial class FindSimpleObjectsByNameResponse
	{
		[DataMember(Name = "simpleObjects", EmitDefaultValue = false)]
		public SimpleObject[] SimpleObjects { get; set; }
	}
}