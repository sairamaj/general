using System.Collections.Generic;

namespace AspNetWebApiRoutesHelper
{
    /// <summary>
    /// Data token property.
    /// <see cref="System.Web.Http.Routing.IHttpRoute"/> for more information.
    /// </summary>
    public class DataTokenProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTokenProperty"/> class.
        /// </summary>
        public DataTokenProperty()
        {
            this.Properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or sets typename of the corresponding property in data token.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets data token properties.
        /// <see cref="System.Web.Http.Routing.IHttpRoute"/> for more information.
        /// </summary>
        public IDictionary<string, object> Properties { get; set; }
    }
}
