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
        static HttpClient client = new HttpClient();
         
        /*
        public string GetMovies()
        {
            try
            {
                
                client.BaseAddress = new Uri("");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string weee = client.ToString();
                JsonConvert.DeserializeObject(weee);

                return weee;
                

            }
            catch (Exception ex)
            {
                var t = ex.Message;
                return t;
            }
        }
        */

        public string GetProductAsync()
        {
            client.BaseAddress = new Uri("http://app.wouterdolk.nl/select_movelist");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Movie movie = null;
            List<Movie> movies = new List<Movie>();

            HttpResponseMessage response = client.GetAsync("http://app.wouterdolk.nl/select_movelist").Result;
            if (response.IsSuccessStatusCode)
            {
                //movie = response.Content.ReadAsAsync<Movie>().Result;
                //movies.Add(movie);
                string content = response.Content.ReadAsStringAsync().Result;
                return content;
            }
            else
            {
                return null;
            }
        }


    }
}
