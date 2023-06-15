using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)

        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {
                    int queryEF = context.Database.ExecuteSqlRaw($"UsuarioAdd  '{usuario.UserName}', '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno},'{usuario.Correo}',@Contraseña",new SqlParameter("@Contraseña", usuario.Contraseña));

                    if (queryEF > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al insertar al usuario" + ex;
            }
            return result;
        }

        public static ML.Result GetById(int idUsuario)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {
                    var obj = context.Usuarios.FromSqlRaw($"UsuarioGetById {idUsuario}").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = obj.IdUsuario;
                        usuario.UserName = obj.UserName;
                        usuario.Nombre = obj.Nombre;
                        usuario.ApellidoPaterno = obj.ApellidoPaterno;
                        usuario.ApellidoMaterno = obj.ApellidoMaterno;
                        usuario.Correo = obj.Correo;
                        usuario.Contraseña = obj.Contraseña;

                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo realizar la consulta";
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

        public static ML.Result GetByUserName(string userName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {
                    var objUsuario = context.Usuarios.FromSqlRaw($"UsuarioGetByIdUserName {userName}").AsEnumerable().FirstOrDefault();

                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.UserName = usuario.UserName;
                        usuario.Nombre = objUsuario.Nombre;
                        usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                        usuario.Correo = objUsuario.Correo;
                        usuario.Contraseña = objUsuario.Contraseña;                       

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Alumno";
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

        public static ML.Result GetByEmail(string correo)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {
                    var obj = context.Usuarios.FromSqlRaw($"GetByEmail '{correo}'").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = obj.IdUsuario;
                        usuario.Nombre = obj.Nombre;
                        usuario.UserName = obj.UserName;

                        usuario.Correo = obj.Correo;
                        usuario.Contraseña = obj.Contraseña;

                        result.Object = usuario; //boxing

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo realizar la consulta";
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

        public static ML.Result UpdatePassword(int IdUsuario, string contraseña)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {

                    int queryEF = context.Database.ExecuteSqlRaw($"UsuarioUpdatePassword {IdUsuario}, '{contraseña}'");
                    if (queryEF > 0)
                    {
                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al actualizar la contraseña" + ex;
            }
            return result;
        }

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.PpriantiCineContext context = new DL.PpriantiCineContext())
                {

                    int queryEF = context.Database.ExecuteSqlRaw($"EmailUpdatePassword  '{usuario.Correo}',  @Contraseña",new SqlParameter("@Contraseña", usuario.Contraseña));
                    if (queryEF > 0)
                    {
                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al actualizar la contraseña" + ex;
            }
            return result;
        }


    }
}
