using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ranaitfleur.Infrastructure
{
    public static class UrlExtensions
    {

        public static string PathAndQuery(this HttpRequest request) =>
            request.QueryString.HasValue
                ? $"{request.Path}{request.QueryString}"
                : request.Path.ToString();

        /// <summary>
        /// Generates a fully qualified URL to an action method by using the specified action name, controller name and
        /// route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteAction(
            this IUrlHelper url,
            string actionName,
            string controllerName,
            object routeValues = null)
        {
            var scheme = url.ActionContext.HttpContext.Request.Scheme;

            return url.Action(actionName, controllerName, routeValues, scheme);
        }
    }
}
