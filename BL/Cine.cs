using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cine
    {
        public static ML.Result Add(ML.Cine cine)

        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {
                    int queryEF = context.Database.ExecuteSqlRaw($"CineAdd  '{cine.Nombre}', '{cine.Direccion}', {cine.Zona.IdZona},{cine.Ventas},{cine.Latitud},{cine.Latitud}");

                    if (queryEF > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al insertar el cine" + ex;
            }
            return result;
        }


        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {

                    int queryEF = context.Database.ExecuteSqlRaw($"CineUpdate {cine.IdCine}, '{cine.Nombre}', '{cine.Direccion}',{cine.Zona.IdZona},{cine.Ventas},{cine.Latitud},{cine.Latitud}");
                    if (queryEF > 0)
                    {
                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al actualizar el cine" + ex;
            }
            return result;
        }


        public static ML.Result Delete(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {

                    int queryEF = context.Database.ExecuteSqlRaw($"CineDelete  {cine.IdCine}");

                    if (queryEF > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al eliminar el cine" + ex;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            ML.Cine cine = new ML.Cine();

            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {

                    var queryEF = context.Cines.FromSqlRaw($"CineGetAll ").ToList();

                    result.Objects = new List<object>();

                    foreach (var row in queryEF)
                    {
                       
                        cine = new ML.Cine();

                        cine.IdCine = row.IdCine;
                        cine.Nombre = row.Nombre;
                        cine.Direccion = row.Direccion;

                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = (int)row.IdZona;
                        cine.Zona.Nombre = row.ZonaNombre;

                        cine.Ventas = (int)row.Ventas;
                        cine.Latitud = (double)row.Latitud;
                        cine.Longitud = (double)row.Longitud;

                        result.Objects.Add(cine); //boxing
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error al mostrar la tabla de cine" + ex;
            }
            return result;
        }


        public static ML.Result GetById(int IdCine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {

                    var objCine = context.Cines.FromSqlRaw($"CineGetById  {IdCine}").AsEnumerable().FirstOrDefault();

                    if (objCine != null)
                    {
                        ML.Cine cine = new ML.Cine();

                        cine.IdCine = objCine.IdCine;
                        cine.Nombre = objCine.Nombre;
                        cine.Direccion = objCine.Direccion;

                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = objCine.IdCine;
                        cine.Zona.Nombre = objCine.Nombre;

                        cine.Ventas = (int)objCine.Ventas;
                        cine.Latitud = (double)objCine.Latitud;
                        cine.Longitud = (double)objCine.Longitud;

                        result.Object = cine;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Cine";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Estadisticas()
        {
            ML.Result result = new();
            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();
            cine.Venta = new ML.Venta();

            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {
                    var queryEF = context.Cines.FromSqlRaw("CineGetAll").ToList();

                    result.Objects = new List<object>();


                    float totalVentas = 0;

                    foreach (var row in queryEF)
                    {
                        cine.IdCine = row.IdCine;
                        cine.Ventas = (int)row.Ventas;
                        cine.Zona.IdZona = (int)row.IdZona;

                        if (cine.Zona.IdZona == 1)
                        {
                            cine.Venta.TotalNorte = cine.Venta.TotalNorte + cine.Ventas;
                            cine.Venta.Sumatotal = cine.Venta.Sumatotal + cine.Ventas;
                        }
                        if (cine.Zona.IdZona == 2)
                        {
                            cine.Venta.TotalSur = cine.Venta.TotalSur + cine.Ventas;
                            cine.Venta.Sumatotal = cine.Venta.Sumatotal + cine.Ventas;
                        }
                        if (cine.Zona.IdZona == 3)
                        {
                            cine.Venta.TotalEste = cine.Venta.TotalEste + cine.Ventas;
                            cine.Venta.Sumatotal = cine.Venta.Sumatotal + cine.Ventas;
                        }
                        if (cine.Zona.IdZona == 4)
                        {
                            cine.Venta.TotalOeste = cine.Venta.TotalOeste + cine.Ventas;
                            cine.Venta.Sumatotal = cine.Venta.Sumatotal + cine.Ventas;
                        }
                        result.Object = cine;
                    }
                }
                cine.Venta.PromedioNorte = (cine.Venta.TotalNorte / cine.Venta.Sumatotal) * 100;
                cine.Venta.PromedioSur = (cine.Venta.TotalSur / cine.Venta.Sumatotal) * 100;
                cine.Venta.PromedioEste = (cine.Venta.TotalEste / cine.Venta.Sumatotal) * 100;
                cine.Venta.PromedioOeste = (cine.Venta.TotalOeste / cine.Venta.Sumatotal) * 100;
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrió un error al mostrar el porcentaje de ventas por cine: " + ex.Message;
            }

            return result;
        }

        
    }
}
