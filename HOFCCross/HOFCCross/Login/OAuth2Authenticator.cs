using System;
using System.Collections.Generic;

namespace HOFCCross.Login
{
    public class OAuth2Authenticator: Xamarin.Auth.OAuth2Authenticator
    {
        public OAuth2Authenticator(string clientId, string clientSecret, string scope, Uri authorizeUrl, Uri redirectUrl, Uri accessTokenUrl): base(clientId, clientSecret, scope, authorizeUrl, redirectUrl, accessTokenUrl)
        {

        }
        
        protected override void OnPageEncountered(Uri url, IDictionary<string, string> query, IDictionary<string, string> fragment)
        {
            // FIXME sans cette ligne, une erreur apparait indiquant une possible tentative de hack
            // Il faut donc cliquer 2 fois pour que cela fonctionne
            query.Remove("state");
            base.OnPageEncountered(url, query, fragment);
        }
    }
}