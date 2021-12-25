using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels
{
	[DataContract]
	public partial class FindMoreComplexObjectsByNameResponse
	{
		[DataMember(Name = "moreComplexObjects", EmitDefaultValue = false)]
		public MoreComplexObject[] MoreComplexObjects { get; set; }
	}
}