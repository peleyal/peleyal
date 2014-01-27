using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Google.Apis.Sample.MVC.Controllers
{
    public class AuthCallbackController :
        Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override Google.Apis.Auth.OAuth2.Mvc.FlowMetadata FlowData
        {
            get { return new AppAuthFlowMetadata(); }

        }
    }
}