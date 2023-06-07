using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Pelicula
    {
        public String backdrop_path { get; set; }
        public List<object> genre_ids { get; set; }
        public int id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }

        public string path = "https://www.themoviedb.org/t/p/w300_and_h450_bestv2";
        public string release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public string media_type { get; set; }
        public bool favorite { get; set; }
        public int vote_count { get; set; }
        public int page { get; set; }
        public List<object> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
        public List<object> Movies { get; set; }
        public List<object> Objects { get; set; }

    }
}
