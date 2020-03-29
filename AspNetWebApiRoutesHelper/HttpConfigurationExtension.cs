using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using Microsoft.Web.Http.Versioning;
using Newtonsoft.Json;

namespace AspNetWebApiRoutesHelper
{
    public static class HttpConfigurationExtension
    {
        /// <summary>
        /// Gets Api routes from Http Configuration.
        /// </summary>
        /// <param name="config">
        /// A <see cref="System.Web.Http.HttpConfiguration"/> class.
        /// </param>
        /// <returns>
        /// A <see cref="IEnumerable{T}"/> of <see cref="ApiRouteInfo"/> collection.
        /// </returns>
        public static IEnumerable<ApiRouteInfo> GetApiRoutes(this HttpConfiguration config)
        {
            var apiRoutes = new List<ApiRouteInfo>();
            foreach (var route in config.Routes.ToList())
            {
                var apiRoute = MapToApiRoute(route);
                var apiSubRoutes = new List<ApiRouteInfo>();
                apiRoute.SubRoutes = apiSubRoutes;
                apiRoutes.Add(apiRoute);
                if (route is IEnumerable<IHttpRoute> subRoutes)
                {
                    foreach (var subRoute in subRoutes)
                    {
                        var apiSubRoute = MapToApiRoute(subRoute);
                        apiSubRoute.IsSubRoute = true;
                        apiSubRoutes.Add(apiSubRoute);
                    }
                }
            }

            return apiRoutes;
        }

        /// <summary>
        /// Maps to Api route.
        /// </summary>
        /// <param name="route">
        /// <see cref="System.Web.Http.Routing.IHttpRoute"/> for more information.
        /// </param>
        /// <returns></returns>
        private static ApiRouteInfo MapToApiRoute(IHttpRoute route)
        {
            var apiRoute = new ApiRouteInfo
            {
                Name = route.RouteTemplate
            };

            if (route.Defaults != null)
            {
                foreach (var defaultItem in route.Defaults)
                {
                    apiRoute.Defaults[defaultItem.Key] = defaultItem.Value?.ToString();
                }
            }

            if (route.Constraints != null)
            {
                foreach (var constraint in route.Constraints)
                {
                    apiRoute.Constraints.Add(constraint.Key, constraint.Value?.ToString());
                }
            }

            if (route.DataTokens != null)
            {
                foreach (var dataToken in route.DataTokens)
                {
                    var descriptors = dataToken.Value as HttpActionDescriptor[];
                    var tokenProperties = new List<DataTokenProperty>();
                    if (descriptors != null)
                    {
                        foreach (var descriptor in descriptors)
                        {
                            var tokenProperty = new DataTokenProperty
                            {
                                TypeName = descriptor.GetType().FullName,
                                Properties = {["actionName"] = descriptor.ActionName}
                            };
                            Trace.WriteLine($"{descriptor.GetType()}");
                            Trace.WriteLine($"{descriptor.ActionName}");
                            foreach (var prop in descriptor.Properties)
                            {
                                tokenProperty.Properties[prop.Key?.ToString()] = prop.Value;
                            }

                            tokenProperties.Add(tokenProperty);
                        }

                        apiRoute.DataTokens[dataToken.Key] = tokenProperties;
                    }
                    else
                    {
                        apiRoute.DataTokens[dataToken.Key] = new List<DataTokenProperty>
                        {
                           new DataTokenProperty
                           {
                               TypeName =  dataToken.Value?.GetType().FullName,
                               Properties = new Dictionary<string, object>
                               {
                                   {
                                       dataToken.Key, dataToken.Value
                                   }
                               }
                           }
                        };
                    }

                }
            }

            return apiRoute;
        }

        private static void DumpRoute(IHttpRoute route)
        {
            Trace.WriteLine($"[API-DEBUG] Route: Template{route.RouteTemplate}");
            Trace.WriteLine($"[API-DEBUG] _________ DEFAULTS ___________");

            if (route.Defaults != null)
            {
                foreach (var defaultItem in route.Defaults)
                {
                    Trace.WriteLine($"    [API-DEBUG] {defaultItem.Key} {defaultItem.Value}");
                }
            }

            Trace.WriteLine($"[API-DEBUG] _________ CONSTRAINTS ___________");
            if (route.Constraints != null)
            {
                foreach (var constraint in route.Constraints)
                {
                    Trace.WriteLine($"    [API-DEBUG] {constraint.Key} {constraint.Value}");
                }
            }

            Trace.WriteLine($"[API-DEBUG] _________ DATA TOKENS ___________");
            if (route.DataTokens != null)
            {
                foreach (var dataToken in route.DataTokens)
                {
                    Trace.WriteLine($"    [API-DEBUG] {dataToken.Key} {dataToken.Value}");
                    var descriptors = dataToken.Value as HttpActionDescriptor[];
                    if (descriptors != null)
                    {
                        foreach (var descriptor in descriptors)
                        {
                            Trace.WriteLine($"{descriptor.GetType()}");
                            Trace.WriteLine($"{descriptor.ActionName}");
                            foreach (var prop in descriptor.Properties)
                            {
                                var apiVerModel = prop.Value as ApiVersionModel;
                                if (apiVerModel != null)
                                {
                                    var info = JsonConvert.SerializeObject(apiVerModel);
                                    Trace.WriteLine($"[API-DEBUG] \t\t_________ {info}___________");
                                    Trace.WriteLine($"[API-DEBUG] \t\t_________ {apiVerModel.IsApiVersionNeutral}___________");
                                }
                                else
                                {
                                    Trace.WriteLine($"[API-DEBUG] \t\t_________ {prop.Key}:{prop.Value}___________");
                                }
                            }
                        }
                    }
                }
            }

            Trace.WriteLine($"[API-DEBUG] _________ HANDLER ___________");
            Trace.WriteLine($"    [API-DEBUG] {route.Handler} ");
        }
    }
}