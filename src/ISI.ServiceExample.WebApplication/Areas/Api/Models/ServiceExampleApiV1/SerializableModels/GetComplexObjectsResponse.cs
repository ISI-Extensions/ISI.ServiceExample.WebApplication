using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels
{
	[DataContract]
	public partial class GetComplexObjectsResponse
	{
		[DataMember(Name = "complexObjects", EmitDefaultValue = false)]
		public ComplexObject[] ComplexObjects { get; set; }
	}
}