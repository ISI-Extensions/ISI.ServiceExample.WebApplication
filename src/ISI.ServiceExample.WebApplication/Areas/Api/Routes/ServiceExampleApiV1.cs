#region Copyright & License
/*
Copyright (c) 2026, Integrated Solutions, Inc.
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

		* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
		* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
		* Neither the name of the Integrated Solutions, Inc. nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion
 
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