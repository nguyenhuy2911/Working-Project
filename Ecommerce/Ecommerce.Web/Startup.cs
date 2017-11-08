﻿using Ecommerce.Web.common.Helper.ckfinder;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartupAttribute(typeof(Ecommerce.Web.Startup))]
namespace Ecommerce.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConnectorConfig.RegisterFileSystems();
            app.Map("/ckfinder/connector", ConnectorConfig.SetupConnector);
        }
    }


}
