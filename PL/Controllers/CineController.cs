using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CineController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Cine cine = new ML.Cine();
            ML.Result result = BL.Cine.GetAll();

            if (result.Correct)
            {
                cine.Cines = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta de cine" + result.ErrorMessage;
            }

            return View(cine);
        }

        [HttpGet]
        public ActionResult Form(int? IdCine)
        {
            ML.Cine cine = new ML.Cine();
            ML.Zona zona = new ML.Zona();
            ML.Result resultZona = BL.Zona.GetAll();

            cine.Zona = new ML.Zona();
           

            if (resultZona.Correct)
            {
                cine.Zona.Zonas = resultZona.Objects;
            }

            return View(cine);

        }

        [HttpPost]
        public ActionResult Form(ML.Cine cine)        
        {
           

            if (cine.IdCine == 0)
            {
                ML.Result result = BL.Cine.Add(cine);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha insertado el cine";
                }
                else
                {
                    ViewBag.Message = "No se ha insertado el cine";
                }

                return View("Modal");
            }
            else
            {
                
                ML.Result result = BL.Cine.Update(cine);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha insertado el cine";
                }
                else
                {
                    ViewBag.Message = "No se ha insertado el cine";
                }

                return View("Modal");
            }
        }

        public ActionResult GetEstadisticas()
        {
            ML.Cine cine = new ML.Cine();
            ML.Result result = BL.Cine.GetAll();

            if (result.Correct)
            {
                cine.Cines = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta de cine" + result.ErrorMessage;
            }

            return View(cine);
        }
    }
}
