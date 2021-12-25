using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels
{
	[DataContract]
	public partial class FindComplexObjectsByNameResponse
	{
		[DataMember(Name = "complexObjects", EmitDefaultValue = false)]
		public ComplexObject[] ComplexObjects { get; set; }
	}
}