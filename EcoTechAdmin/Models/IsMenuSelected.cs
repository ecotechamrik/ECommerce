using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoTechAdmin
{
    public static class IsMenuSelected
    {
        public static string IsSelected(this IHtmlHelper htmlHelper, string controllers, string actions, string cssClass = "active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"].ToString().ToLower();
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString().ToLower();

            IEnumerable<string> acceptedActions = (actions.ToLower() ?? currentAction).Split(',');
            IEnumerable<string> acceptedControllers = (controllers.ToLower() ?? currentController).Split(',');

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }
    }
}