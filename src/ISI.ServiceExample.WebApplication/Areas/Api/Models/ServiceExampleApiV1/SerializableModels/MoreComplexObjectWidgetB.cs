using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels
{
	[ISI.Extensions.Serialization.SerializerContractUuid(_SerializerContractUuid)]
	[DataContract]
	public class MoreComplexObjectWidgetB : IMoreComplexObjectWidget
	{
		internal const string _SerializerContractUuid = "3323bb9d-0008-46cf-95e0-a326f5bbd77e";

		[DataMember(Name = "_serializerContractUuid", EmitDefaultValue = false)]
		public string SerializerContractUuid { get; set; } = _SerializerContractUuid;

		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		[DataMember(Name = "propertyB", EmitDefaultValue = false)]
		public string PropertyB { get; set; }
	}
}
