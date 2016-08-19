using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace Qubiz.QuizEngine.Areas.M.Controllers
{
    public class NamespaceHttpControllerSelector : IHttpControllerSelector
    {
        private const string ControllerKey = "controller";

        private readonly HttpConfiguration configuration;
        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> controllers;
        private readonly HashSet<string> duplicates;

        public NamespaceHttpControllerSelector(HttpConfiguration config)
        {
            configuration = config;
            duplicates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(InitializeControllerDictionary);
        }

        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var controllerList = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

            // Create a lookup table where key is "namespace.controller". The value of "namespace" is the last
            // segment of the full namespace. For example:
            // MyApplication.Controllers.V1.ProductsController => "V1.Products"
            IAssembliesResolver assembliesResolver = configuration.Services.GetAssembliesResolver();
            IHttpControllerTypeResolver controllersResolver = configuration.Services.GetHttpControllerTypeResolver();
            
            ICollection<Type> controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

            foreach (Type controllerType in controllerTypes)
            {
                var segments = controllerType.Namespace.Split(Type.Delimiter);

                // For the dictionary key, strip "Controller" from the end of the type name.
                // This matches the behavior of DefaultHttpControllerSelector.
                var controllerName = controllerType.Name.Remove(controllerType.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);

                var key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", segments[segments.Length-2], controllerName);

                if (segments.Contains("M"))
                {
                    key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", segments[segments.Length - 3], controllerName);
                }

                // Check for duplicate keys.
                if (controllerList.Keys.Contains(key))
                {
                    duplicates.Add(key);
                }
                else
                {
                    controllerList[key] = new HttpControllerDescriptor(configuration, controllerType.Name, controllerType);
                }
            }

            // Remove any duplicates from the dictionary, because these create ambiguous matches. 
            // For example, "Foo.V1.ProductsController" and "Bar.V1.ProductsController" both map to "v1.products".
            foreach (string s in duplicates)
            {
                controllerList.Remove(s);
            }
            return controllerList;
        }

        // Get a value from the route data, if present.
        private static T GetRouteVariable<T>(IHttpRouteData routeData, string name)
        {
            object result = null;

            if (routeData.Values.TryGetValue(name, out result))
            {
                return (T)result;
            }
            return default(T);
        }

        private string GetAppVersion(HttpRequestMessage request)
        {
            if (request.Headers.Contains("Version"))
            {
                IEnumerable<string> headerValues = request.Headers.GetValues("Version");
                var version = headerValues.FirstOrDefault();
                return version;
            }
            else return null;
        }

        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {  
            IHttpRouteData routeData = request.GetRouteData();

            if (routeData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Get the namespace and controller variables from the route data.
            string namespaceName = GetAppVersion(request);

            if (string.IsNullOrEmpty(namespaceName))
            {
                namespaceName = "QuizEngine";
            }

            string controllerName = GetRouteVariable<string>(routeData, ControllerKey);
            if (controllerName == null)
            {
                IEnumerable<IHttpRouteData> subroutes = routeData.GetSubRoutes();
                var route = subroutes.First().Route;
                if(route != null)
                {
                    HttpActionDescriptor[] actionDescriptors = (HttpActionDescriptor[])route.DataTokens["actions"];
                    return actionDescriptors.First().ControllerDescriptor;
                }
            }
            else
            {
                // Find a matching controller.
                string key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, controllerName);

                HttpControllerDescriptor controllerDescriptor;
                
                if (controllers.Value.TryGetValue(key, out controllerDescriptor))
                {
                    return controllerDescriptor;
                }
                else if (duplicates.Contains(key))
                {
                    throw new HttpResponseException(
                        request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        "Multiple controllers were found that match this request."));
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
            return null;
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return controllers.Value;
        }
    }
}