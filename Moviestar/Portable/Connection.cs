using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net;

namespace Moviestar
{
    class Connection
    {
        private WebRequest request;
        private Uri mUrl;

        public void Connect()
        {
            mUrl = new Uri("http://app.wouterdolk.nl/index.php");
            

        }
    }
}
