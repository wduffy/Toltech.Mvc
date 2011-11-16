using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Reflection;

namespace System.Web.Mvc
{
    public class IsRootActionConstraint : IRouteConstraint
    {
        private Dictionary<string, Type> _controllers;

        public IsRootActionConstraint()
        {
            _controllers = Assembly
                                .GetCallingAssembly()
                                .GetTypes()
                                .Where(type => type.IsSubclassOf(typeof(Controller)))
                                .ToDictionary(key => key.Name.Replace("Controller", ""));
        }

        #region IRouteConstraint Members

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return !_controllers.Keys.Contains(values["action"] as string);
        }

        #endregion
    }
}
