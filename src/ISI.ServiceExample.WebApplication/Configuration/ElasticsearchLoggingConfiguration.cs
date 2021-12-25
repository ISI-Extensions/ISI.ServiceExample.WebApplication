using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISI.ServiceExample.WebApplication
{
	public partial class Configuration
	{
		public class ElasticsearchLoggingConfiguration
		{
			public string NodeUrl { get; set; }
			public string UserName { get; set; }
			public string Password { get; set; }
			public string IndexFormat { get; set; } = "custom-index-{0:yyyy.MM}";
		}
	}
}