using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCRUD.DataAccessLayer.Models
{
    class Empleado
    {
        public int EmpleadoID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string NumeroDocumento { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
        public string FechaNacimiento { get; set; }

    }
}
