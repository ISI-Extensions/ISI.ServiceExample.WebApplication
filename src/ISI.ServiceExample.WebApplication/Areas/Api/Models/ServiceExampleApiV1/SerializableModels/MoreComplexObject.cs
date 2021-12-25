using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels
{
	[DataContract]
	public class MoreComplexObject
	{
		[DataMember(Name = "moreComplexObjectUuid", EmitDefaultValue = false)]
		public Guid MoreComplexObjectUuid { get; set; }

		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		[DataMember(Name = "widgets", EmitDefaultValue = false)]
		public IMoreComplexObjectWidget[] Widgets { get; set; }

		[DataMember(Name = "isActive", EmitDefaultValue = false)]
		public bool IsActive { get; set; }

		[DataMember(Name = "createdOnUtc", EmitDefaultValue = false)]
		public DateTime CreatedOnUtc { get; set; }

		[DataMember(Name = "createdBy", EmitDefaultValue = false)]
		public string CreatedBy { get; set; }

		[DataMember(Name = "modifiedOnUtc", EmitDefaultValue = false)]
		public DateTime ModifiedOnUtc { get; set; }

		[DataMember(Name = "modifiedBy", EmitDefaultValue = false)]
		public string ModifiedBy { get; set; }
	}
}
