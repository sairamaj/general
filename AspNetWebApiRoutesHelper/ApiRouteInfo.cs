using System.Collections.Generic;

namespace AspNetWebApiRoutesHelper
{
    /// <summary>
    /// IHttpRoute extracted in to Api route information.
    /// </summary>
    public class ApiRouteInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiRouteInfo"/> class.
        /// </summary>
        public ApiRouteInfo()
        {
            this.Defaults = new Dictionary<string, string>();
            this.Constraints = new Dictionary<string, string>();
            this.DataTokens = new Dictionary<string, IEnumerable<DataTokenProperty>>();
        }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Defaults.
        /// <see cref="System.Web.Http.Routing.IHttpRoute"/> for more information.
        /// </summary>
        public IDictionary<string, string> Defaults { get; set; }

        /// <summary>
        /// Gets or sets Constraints.
        /// <see cref="System.Web.Http.Routing.IHttpRoute"/> for more information.
        /// </summary>
        public IDictionary<string, string> Constraints { get; set; }

        /// <summary>
        /// Converts route DataToken in to <see cref="DataTokenProperty"/> class.
        /// <see cref="System.Web.Http.Routing.IHttpRoute"/> for more information.
        /// </summary>
        public IDictionary<string, IEnumerable<DataTokenProperty>> DataTokens { get; set; }

        /// <summary>
        /// Gets or sets indicating whether a route is sub route or not.
        /// </summary>
        public bool IsSubRoute { get; set; }

        /// <summary>
        /// Gets or sets sub routes.
        /// </summary>
        public IEnumerable<ApiRouteInfo> SubRoutes { get; set; }
    }
}