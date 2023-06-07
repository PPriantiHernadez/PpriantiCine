using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PeliculaController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Pelicula pelicula = new ML.Pelicula();
            pelicula.Movies = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");

                var responseTask = client.GetAsync("movie/popular?api_key=73862df679591e2645d7021f7168faa4&language=en-US&page=1");
                responseTask.Wait(); //esperar a que se resuelva la llamada al servicio

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<dynamic>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.results)
                    {
                        ML.Pelicula resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Pelicula>(resultItem.ToString());

                        pelicula.Movies.Add(resultItemList);
                    }
                }
            }
            return View(pelicula);
        }

        [HttpGet]
        public IActionResult Favoritas()
        {
            ML.Pelicula pelicula = new ML.Pelicula();
            pelicula.Movies = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");

                var responseTask = client.GetAsync("account/19728416/favorite/movies?api_key=73862df679591e2645d7021f7168faa4&language=en-US&page=1&session_id=02470cc3a9e0666029e4d478b5f75d23787ef3b3&sort_by=created_at.asc");
                responseTask.Wait(); //esperar a que se resuelva la llamada al servicio

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<dynamic>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.results)
                    {
                        ML.Pelicula resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Pelicula>(resultItem.ToString());

                        pelicula.Movies.Add(resultItemList);
                    }
                }
            }
            return View(pelicula);
        }


        [HttpGet]
        public IActionResult Add(int id)
        {
            using (var client = new HttpClient())
            {
                if (id != null)
                {
                    ML.Favorito favorite = new ML.Favorito();
                    favorite.media_id = id;
                    favorite.media_type = "movie";
                    favorite.favorite = true;

                    client.BaseAddress = new Uri("https://api.themoviedb.org/3/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Favorito>("account/19728416/favorite?session_id=02470cc3a9e0666029e4d478b5f75d23787ef3b3&api_key=73862df679591e2645d7021f7168faa4", favorite);

                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se agrego corectamente la Pelicula a favoritos  ";
                    }
                    else
                    {
                        ViewBag.Message = "Error no se pudo agregar ";

                    }
                }
            }


            return View("Modal");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                if (id != null)
                {
                    ML.Favorito favorite = new ML.Favorito();
                    favorite.media_id = id;
                    favorite.media_type = "movie";
                    favorite.favorite = false;

                    client.BaseAddress = new Uri("https://api.themoviedb.org/3/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Favorito>("account/19728416/favorite?session_id=02470cc3a9e0666029e4d478b5f75d23787ef3b3&api_key=73862df679591e2645d7021f7168faa4", favorite);

                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = " Se elimnino correctamente ";
                    }
                    else
                    {
                        ViewBag.Message = "No se elimnino correctamente  ";

                    }
                }
            }
            return View("Modal");
        }


    }
}
