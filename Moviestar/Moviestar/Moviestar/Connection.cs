using Moviestar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Moviestar
{
    public class Connection
    {
        public Connection()
        {

        }

        static HttpClient client = new HttpClient();
         
        public string GetAPI()
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("http://app.wouterdolk.nl/select_movielist").Result;
                var responsecontent = response.Content;
                string content = responsecontent.ReadAsStringAsync().Result;
                return content;
            }
            catch(Exception e)
            {
                return  e.Message;
            }
        }
    }
}
