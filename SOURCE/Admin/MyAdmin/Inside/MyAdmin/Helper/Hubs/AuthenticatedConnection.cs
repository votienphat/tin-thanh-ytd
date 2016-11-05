﻿using Microsoft.AspNet.SignalR;

namespace MyAdmin.Helper.Hubs
{
    public class AuthenticatedConnection : PersistentConnection
    {
        protected override bool AuthorizeRequest(IRequest request)
        {
            return request.User.Identity.IsAuthenticated;
        }
    } 
}