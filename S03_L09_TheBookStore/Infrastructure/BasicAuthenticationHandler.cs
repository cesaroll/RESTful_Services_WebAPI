﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheBookStore.Contracts;
using TheBookStore.Models;

namespace TheBookStore.Infrastructure
{
    public class BasicAuthenticationHandler : DelegatingHandler
    {
        private const string basicAuthResponseHeader = "WWW-Authenticate";
        private const string basicAuthResponseHeaderValue = "Basic";

        private readonly IPrincipalProvider principalProvider;

        public BasicAuthenticationHandler(IPrincipalProvider principalProvider)
        {
            this.principalProvider = principalProvider;
        }

        private Credentials ParseAuthorizationHeader(string authHeader)
        {
            var credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authHeader)).Split(':');

            if (credentials.Length != 2 ||
                string.IsNullOrEmpty(credentials[0]) ||
                string.IsNullOrEmpty(credentials[1]) )
                return null;

            return new Credentials()
            {
                Username = credentials[0],
                Password = credentials[1]
            };

        }

        protected  async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authValue = request.Headers.Authorization;

            if (authValue != null && !String.IsNullOrWhiteSpace(authValue.Parameter))
            {
                var parsedCredentials = ParseAuthorizationHeader(authValue.Parameter);

                if (parsedCredentials != null)
                    request.GetRequestContext().Principal = principalProvider.CreatePrincipal(
                        parsedCredentials.Username, parsedCredentials.Password);

            }

            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;

                    if(response.StatusCode == HttpStatusCode.Unauthorized &&
                        !response.Headers.Contains(basicAuthResponseHeader))
                        response.Headers.Add(basicAuthResponseHeader, basicAuthResponseHeaderValue);

                    return response;

                });
        }
    }
}