﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientPrototype
{
    public class AuthenticationHandler : HttpClientHandler
    {
        private string token;

        public AuthenticationHandler(string username, string password)
        {
            this.token = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", "Basic " + token);
            return base.SendAsync(request, cancellationToken);
        }
    }
}