#define NewApi

using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UserAndAccounts
{
    public class TestUser
    {
        public async Task DoAsync()
        {
            string cloudUrl = "https://login.microsoftonline.com";
            string tenantDomain = "msidentitysamplestesting.onmicrosoft.com";
            string authority = $"{cloudUrl}/{tenantDomain}/";
            string clientId = "5d6f2191-d895-4d0f-81cd-986806b1db59"; // "145cec56-05b2-4764-a41c-b77466387462"; //
            string[] scopes = { "user.read" };

            PublicClientApplication app = new PublicClientApplication(clientId, authority);
            AuthenticationResult result;
            IEnumerable<IUser> users = await GetUsersAsync(app);

            try
            {
                result = await app.AcquireTokenSilentAsync(scopes, users.FirstOrDefault());
            }
            catch (MsalUiRequiredException)
            {
                result = await app.AcquireTokenAsync(scopes, users.FirstOrDefault());
            }

            users = await GetUsersAsync(app);
            IUser signedInUser = users.FirstOrDefault();

#if NewApi
            string displayableId = signedInUser.DisplayableId;
            string identifier = signedInUser.Identifier;
            string environement = signedInUser.Environment;
            string name = string.Empty; // the name did not make sense indeed
#else
            string displayableId = signedInUser.DisplayableId;
            string identifier = signedInUser.Identifier;
            string environement = signedInUser.IdentityProvider;
            string name = signedInUser.Name;
#endif

            try
            {
                result = await app.AcquireTokenSilentAsync(scopes, users.FirstOrDefault());
            }
            catch (MsalUiRequiredException)
            {
                result = await app.AcquireTokenAsync(scopes, users.FirstOrDefault(), UIBehavior.SelectAccount, string.Empty);
            }

        }

        private static async Task<IEnumerable<IUser>> GetUsersAsync(PublicClientApplication app)
        {
#if NewApi
            return await app.GetUsersAsync();
#else
            return app.Users;
#endif
        }
    }
}
