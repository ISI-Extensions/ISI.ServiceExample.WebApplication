using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels
{
	[DataContract]
	public partial class SetMoreComplexObjectRequest
	{
		[DataMember(Name = "moreComplexObjectUuid", EmitDefaultValue = false)]
		public Guid MoreComplexObjectUuid { get; set; }

		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		[DataMember(Name = "widgets", EmitDefaultValue = false)]
		public IMoreComplexObjectWidget[] Widgets { get; set; }

		[DataMember(Name = "isActive", EmitDefaultValue = false)]
		public bool IsActive { get; set; }
	}
}