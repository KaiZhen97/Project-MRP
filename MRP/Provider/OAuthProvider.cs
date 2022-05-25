using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebFrameWorkLib.Dal;

namespace MRP.Provider
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        WebFrameWorkLib.BusinessLogic.AuditBL auditBL = new WebFrameWorkLib.BusinessLogic.AuditBL();

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var username = context.UserName;
                var password = context.Password;
                var roleDescStr = "";
                UserDal userDal = new UserDal();

                WebFrameWorkLib.Database.UAMUser user = new WebFrameWorkLib.Database.UAMUser();

                var data = context.Request.ReadFormAsync();
                var formResult = data.Result;
                var customParameter = formResult.Where(c => c.Key == "custom").FirstOrDefault().Value;
                var customValue = customParameter[0];

                user = userDal.getUserByIDAndPwd(username, password, customValue);

                if (user != null)
                {
                    var roleDesc = userDal.getUserRoleDesc(user.RoleID);
                    roleDescStr = roleDesc;
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.LoginID),
                        new Claim("UserID", user.AccessID.ToString()),
                        new Claim("RoleDesc", roleDesc),
                        new Claim(ClaimTypes.Role, roleDesc)
                    };

                    ClaimsIdentity oAutIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                    var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {
                            "role", roleDescStr
                        }
                    });
                    context.Validated(new AuthenticationTicket(oAutIdentity, props));
                    auditBL.accessAudit(user.AccessID, "Login");
                }
                else
                {
                    context.SetError("invalid_grant", "Error");
                }
            });
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}