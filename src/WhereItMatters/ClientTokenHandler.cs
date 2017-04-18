using Braintree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WhereItMatters
{
    public class ClientTokenHandler : IHttpHandler
    {
        private readonly BraintreeGateway _gateway;
        public ClientTokenHandler(BraintreeGateway gateway)
        {
            _gateway = gateway;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            var clientToken = _gateway.ClientToken.generate();
            context.Response.Write(clientToken);
        }
    }
}
