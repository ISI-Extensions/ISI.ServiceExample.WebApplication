using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISI.ServiceExample.WebApplication
{
	[ISI.Extensions.ConfigurationHelper.Configuration(ConfigurationSectionName)]
	public partial class Configuration : ISI.Extensions.ConfigurationHelper.IConfiguration
	{
		public const string ConfigurationSectionName = "ISI.ServiceExample.WebApplication";

		public ElasticsearchLoggingConfiguration ElasticsearchLogging { get; set; } = new ElasticsearchLoggingConfiguration();
	}
}