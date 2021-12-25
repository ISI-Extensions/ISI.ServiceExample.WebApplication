using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISI.Extensions.AspNetCore;
using ISI.Extensions.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;

namespace ISI.ServiceExample.WebApplication
{
	public partial class Routes
	{
		public partial class Public : IHasUrlRoute
		{
			string IHasUrlRoute.UrlRoot => UrlRoot;

#pragma warning disable 649
			public class RouteNames : IRouteNames
			{
				[RouteName] public const string Index = "Index-c030633b-8cf5-4f51-a25a-71cb21ad29b2";
				//${RouteNames}
			}
#pragma warning restore 649

			internal static readonly string UrlRoot = Routes.UrlRoot;
		}
	}
}