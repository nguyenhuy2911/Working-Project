﻿using System.Web.Mvc;

namespace Ecommerce.Web.Areas.BackEnd
{
    public class BackEndAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BackEnd";
            }
        }


        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "DashBoard",
               "admin",
               new { controller= "DasBoard", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "BackEnd_default",
                "BackEnd/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}