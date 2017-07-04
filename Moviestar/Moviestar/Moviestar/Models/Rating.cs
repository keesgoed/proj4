using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moviestar.Models
{
    public class Rating
    {
        public string movie_id { get; set; }
        public string user_id { get; set; }
        public int rating { get; set; }
    }
}
