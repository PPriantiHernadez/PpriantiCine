using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cine
    {
        public int IdCine { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public ML.Zona Zona { get; set; }
        public int Ventas { get; set; }
        public List<object> Cines { get; set; }

        public ML.Venta Venta { get; set; }
        public ML.Estadistica Estadistica { get; set; }

        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
