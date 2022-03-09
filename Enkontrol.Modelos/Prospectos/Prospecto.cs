using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enkontrol.Modelos.Prospectos
{
    public class Prospecto
    {
        public int Id { get; set; }
        public string cNombre { get; set; }
        public string cApellidoPaterno { get; set; }
        public string cApellidoMaterno { get; set; }
        public DateTime dtFechaNacimiento { get; set; }
        public string cTelefonoMovil { get; set; }
        public string cEmail { get; set; }
        public string Genero { get; set; }
        public string EstadoCivil { get; set; }
    }
}
