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
	public class MoreComplexObjectWidgetA : IMoreComplexObjectWidget
	{
		internal const string _SerializerContractUuid = "0ceb38a8-1aaa-486a-9ec8-0cf74badf134";

		[DataMember(Name = "_serializerContractUuid", EmitDefaultValue = false)]
		public string SerializerContractUuid { get; set; } = _SerializerContractUuid;

		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		[DataMember(Name = "propertyA", EmitDefaultValue = false)]
		public string PropertyA { get; set; }
	}
}
