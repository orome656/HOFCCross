using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace HOFCCross.Auth
{
    public class OAuth2Request : Xamarin.Auth.OAuth2Request
    {
        public OAuth2Request(string method, Uri url, IDictionary<string, string> parameters, Account account) : base(method, url, parameters, account)
        {
        }

        public override Task<Response> GetResponseAsync(CancellationToken cancellationToken)
        {
            // Make sure we have an account
            if (Account == null)
            {
                throw new InvalidOperationException("You must specify an Account for this request to proceed");
            }

            // Sign the request before getting the response
            var req = GetPreparedWebRequest();

            // Authorize it
            var authorization = GetAuthorizationHeader(Account);
            
            req.Headers.Authorization = AuthenticationHeaderValue.Parse(authorization);

            return base.GetResponseAsync(cancellationToken);
        }
    }
}
