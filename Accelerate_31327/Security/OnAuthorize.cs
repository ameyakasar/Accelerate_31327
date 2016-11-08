using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Web.Security;
using System.Web.Mvc;
using System.Configuration;
using Accelerate_31327.Models;


namespace Accelerate_31327.Security
{
    public class OnAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            // Get Action Name
            //var rd = httpContext.Request.RequestContext.RouteData;
            //string currentAction = rd.GetRequiredString("action");
            //string currentController = rd.GetRequiredString("controller");

            // Check Authorization from Database
            if (Roles.Contains(GetAuthorizedRoles(httpContext.User.Identity.Name)))
            {
                return true;
            }

            else
            {
                return false;
            }
            // return base.AuthorizeCore(httpContext);
        }

        /// <summary>
        /// Redirects to System Unauthorized page
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectResult("~/Home/Unauthorized");
        }

        /// <summary>
        /// Function to get Roles of the user from the database
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetAuthorizedRoles(string userName)
        {
            AccelerateEntities ac = new AccelerateEntities();

            //var az = from s in ac.Action_Manager
            //         join r in ac.Page_Manager on s.Page_ID equals r.Page_ID
            //         where r.Controller == controller && r.Action == action
            //         select s;



            string role = "";
            var row = ac.AppUsers.Where(p => p.UserName == userName).FirstOrDefault();


            if (row != null)
            {
                role = row.UserRole;
            }

            return role;

        }
    }
}