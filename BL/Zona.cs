using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {

                    var queryEF = context.Zonas.FromSqlRaw($"ZonaGetAll ").ToList();

                    result.Objects = new List<object>();

                    foreach (var row in queryEF)
                    {
                        ML.Zona zona = new ML.Zona();
                        zona = new ML.Zona();

                        zona.IdZona = zona.IdZona;
                        zona.Nombre = row.Nombre;                     

                        result.Objects.Add(zona); 
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error al mostrar la tabla de zona" + ex;
            }
            return result;
        }

    }
}
