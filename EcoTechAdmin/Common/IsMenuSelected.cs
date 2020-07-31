#region [ Namespace ]
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace EcoTechAdmin
{
    public static class IsMenuSelected
    {
        #region [ Make the current Page's Menu Active (Bold) based on the Controller and Action Names ]
        /// <summary>
        /// Make the current Page's Menu Active (Bold) based on the Controller and Action Names
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="controllers"></param>
        /// <param name="actions"></param>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        public static string IsSelected(this IHtmlHelper htmlHelper, string controllers, string actions, string cssClass = "active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"].ToString().ToLower();
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString().ToLower();

            IEnumerable<string> acceptedActions = (actions.ToLower() ?? currentAction).Split(',');
            IEnumerable<string> acceptedControllers = (controllers.ToLower() ?? currentController).Split(',');

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }
        #endregion
    }
}