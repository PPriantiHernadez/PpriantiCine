using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;

namespace PL.Controllers
{
    public class UserController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UserController(IHostingEnvironment _environment, IConfiguration _configuration, IWebHostEnvironment hostingEnvironment)
        {
            environment = _environment;
            configuration = _configuration;
            _hostingEnvironment = hostingEnvironment;
        }

       
        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Login(ML.Usuario usuario, string password)
        {
            // Crear una instancia del algoritmo de hash bcrypt
            var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
            // Obtener el hash resultante para la contraseña ingresada 
            var passwordHash = bcrypt.GetBytes(20);

            if (usuario.UserName != null)
            {
                // Insertar usuario en la base de datos
                usuario.Contraseña = passwordHash;
                ML.Result result = BL.Usuario.Add(usuario);
                return View();
            }
            else
            {
                // Proceso de login
                ML.Result result = BL.Usuario.GetByEmail(usuario.Correo);
                usuario = (ML.Usuario)result.Object;

                if (usuario.Contraseña.SequenceEqual(passwordHash))
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult CambiarPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmailContraseña(string email)
        {

            //validar que exista el email en la bd

            string emailOrigen = configuration["EmailOrigen"];

            MailMessage mailMessage = new MailMessage(emailOrigen, email, "Recuperar Contraseña", "<p>Correo para recuperar contraseña</p>");
            mailMessage.IsBodyHtml = true;

            //string contenidoHTML = System.IO.File.ReadAllText(@configuration["Email"]);

            string contenidoHTML = System.IO.File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Template", "Email.html"));

            mailMessage.Body = contenidoHTML;

            //string url = configuration["NewPassword"] + HttpUtility.UrlEncode(email); //crea la variable de la direccion del metodo

            //string url = "http://192.168.0.111/User/NewPassword" + HttpUtility.UrlEncode(email);
            string url = configuration["NewPassword2"] + HttpUtility.UrlEncode(email);


            mailMessage.Body = mailMessage.Body.Replace("{Url}",url); //remplaza la Url
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, configuration["Contrasenia"]);

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();

            ViewBag.Modal = "show";
            ViewBag.Mensaje = "Se ha enviado un correo de confirmación a tu correo electronico";
            return View("ModalLogin");
        }


        [HttpGet]
        public ActionResult NewPassword(string email)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Correo = email;

            return View(usuario);
        }

        [HttpPost]
        public ActionResult NewPassword(ML.Usuario usuario, string password)
        {

            var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);

            var passwordHash = bcrypt.GetBytes(20);
            usuario.Contraseña = passwordHash;

            ML.Result result = BL.Usuario.Update(usuario);

            if (result.Correct)
            {
                ViewBag.Modal = "show";
                ViewBag.Message = "Se ha actualizado correctamente";
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                ViewBag.Modal = "show";
                ViewBag.Mensaje = "Error al actualizar la contraseña";
                return View();
            }
        }

    }
}
