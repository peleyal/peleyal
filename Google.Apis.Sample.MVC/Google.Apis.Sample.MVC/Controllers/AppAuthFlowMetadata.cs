using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using Google.Apis.Util.Store;

namespace Google.Apis.Sample.MVC.Controllers
{
    public class AppAuthFlowMetadata : FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = "762781009756-m0ed1ekb562tibge75157hnmct5hep8j.apps.googleusercontent.com",
                        ClientSecret = "Osqjq87ROjK5y6tBw9rJFPIe"
                    },
                    Scopes = new[] { DriveService.Scope.Drive },
                    // TODO: maybe in a future post I'll demonstrate a new EFDataStore usage.
                    DataStore = new FileDataStore("Google.Apis.Sample.MVC")
                });

        public override string GetUserId(Controller controller)
        {
            return controller.User.Identity.GetUserName();
        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }
}