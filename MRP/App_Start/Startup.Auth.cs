using System;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using MRP.Provider;

namespace MRP
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new OAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(2),
                AllowInsecureHttp = true
            };
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
