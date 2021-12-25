using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels
{
	[DataContract]
	public partial class SetSimpleObjectRequest
	{
		[DataMember(Name = "simpleObjectUuid", EmitDefaultValue = false)]
		public Guid SimpleObjectUuid { get; set; }

		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		[DataMember(Name = "isActive", EmitDefaultValue = false)]
		public bool IsActive { get; set; }
	}
}