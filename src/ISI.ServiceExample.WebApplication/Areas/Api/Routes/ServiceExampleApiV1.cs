using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISI.Extensions.Extensions;
using ISI.Extensions.AspNetCore;
using ISI.Extensions.AspNetCore.Extensions;

namespace ISI.ServiceExample.WebApplication.Areas.Api
{
	public partial class Routes
	{
		public partial class ServiceExampleApiV1 : IHasUrlRoute
		{
			string IHasUrlRoute.UrlRoot => UrlRoot;

#pragma warning disable 649
			public class RouteNames : IRouteNames
			{
				[RouteName] public const string SetSimpleObject = "SetSimpleObject-572b8e49-1e32-492b-be60-aae2459fe027";
				[RouteName] public const string GetSimpleObjects = "GetSimpleObjects-d238402b-055c-4e59-b5e9-639e23162e86";
				[RouteName] public const string ListSimpleObjects = "ListSimpleObjects-b5cf3706-91c8-4d98-909d-ede2cede570b";
				[RouteName] public const string FindSimpleObjectsByName = "FindSimpleObjectsByName-d0fb008f-b578-4276-8cbe-08526d9786d1";

				[RouteName] public const string SetComplexObject = "SetComplexObject-3335ac9c-d7c2-4746-854d-73f6922f72d6";
				[RouteName] public const string GetComplexObjects = "GetComplexObjects-0e0e73fd-0abb-4680-97d9-4395728ce38a";
				[RouteName] public const string ListComplexObjects = "ListComplexObjects-6c5e0a37-2400-40c8-9c61-d51f5f0db2f7";
				[RouteName] public const string FindComplexObjectsByName = "FindComplexObjectsByName-e362540e-f8bc-4f21-b599-bceeda5c190f";
				
				[RouteName] public const string SetMoreComplexObject = "SetMoreComplexObject-36257fbc-bb16-4097-b3d5-2ee98c0c50e6";
				[RouteName] public const string GetMoreComplexObjects = "GetMoreComplexObjects-7e9f0ae5-056b-4be9-8e66-90beb81cbedb";
				[RouteName] public const string ListMoreComplexObjects = "ListMoreComplexObjects-a2cd2d16-1180-481c-9511-66715c2678d8";
				[RouteName] public const string FindMoreComplexObjectsByName = "FindMoreComplexObjectsByName-1c4ac41f-5def-4177-bcf5-6f64bd048898";

				[RouteName] public const string SetCachedObject = "SetCachedObject-482b8b55-51dc-4f43-be38-cf405f71e80b";
				[RouteName] public const string GetCachedObjects = "GetCachedObjects-f7c8c370-ecbd-42d0-ab77-8d96d2e54efc";
				[RouteName] public const string ListCachedObjects = "ListCachedObjects-eb94c490-cc16-42c5-8010-eabad7c31236";
				[RouteName] public const string FindCachedObjectsByName = "FindCachedObjectsByName-32ab70e4-b7f2-4ec9-bf69-b29d13ad789c";
				//${RouteNames}
			}
#pragma warning restore 649

			internal static readonly string UrlRoot = Routes.UrlRoot + "service-example-api/v1/";
		}
	}
}