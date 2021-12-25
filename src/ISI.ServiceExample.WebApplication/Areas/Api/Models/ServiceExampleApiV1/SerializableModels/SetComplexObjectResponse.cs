using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ISI.ServiceExample.WebApplication.Areas.Api.Models.ServiceExampleApiV1.SerializableModels
{
	[DataContract]
	public partial class SetComplexObjectResponse
	{
		[DataMember(Name = "complexObject", EmitDefaultValue = false)]
		public ComplexObject ComplexObject { get; set; }
	}
}