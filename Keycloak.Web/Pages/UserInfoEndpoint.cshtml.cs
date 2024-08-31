using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Keycloak.Web.Pages
{
    public class UserInfoEndpointModel : PageModel
    {
        [BindProperty] public UserInfoResponse? UserInfo { get; set; }

        public async Task OnGet()
        {
            HttpClient httpClient = new HttpClient();
            var disco = await httpClient.GetDiscoveryDocumentAsync(
                "http://localhost:8080/realms/techtenant/.well-known/openid-configuration");

            if (disco.IsError)
            {
                //loglama yap
            }


            UserInfo = await httpClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = disco.UserInfoEndpoint,
                Token = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken)
            });
        }
    }
}