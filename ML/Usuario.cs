using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; } //PK

        // [Required]
        public string? UserName { get; set; }

        // [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Solo se permite ingresar letras.")]
        public string Nombre { get; set; }

        //[Required(ErrorMessage = "campo obligatorio")]
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public byte[] Contraseña { get; set; }
    }
}
