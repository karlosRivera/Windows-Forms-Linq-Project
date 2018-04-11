using LinqCRUD.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCRUD.BusinessLayer
{
    class EmpleadoBL
    {
        EmpleadoDataAccess da = new EmpleadoDataAccess();

        public List<empleado> GetByDepartment(string dept)
        {
            List<empleado> empleados;

            if (!String.IsNullOrEmpty(dept))
            {
                empleados = da.GetEmployeesByDepartment(dept);
            }
            else
            {
                empleados = null;
            }

            return empleados;
        }

        public List<empleado> GetByName(string name)
        {
            List<empleado> empleados;


            if (!String.IsNullOrEmpty(name))
            {
                empleados = da.GetEmployeesByName(name);
            }
            else
            {
                empleados = null;
            }
           
            return empleados;
        }

        public bool Create(string nombre, string apellido, DateTime fecha_nac, string sexo, string direccion, string telefono, string tipo_documento, string num_documento, string email, string departamento)
        {
            var emp = new empleado
            {
                nombre = nombre,
                apellido = apellido,
                fecha_nacimiento = fecha_nac,
                sexo = sexo,
                direccion = direccion,
                telefono = telefono,
                tipo_documento = tipo_documento,
                numero_documento = num_documento,
                email = email,
                departamento = departamento
            };

            var result = da.InsertEmployee(emp);

            return result;

        }

        public bool Update(int id,string nombre, string apellido, DateTime fecha_nac, string sexo, string direccion, string telefono, string tipo_documento, string num_documento, string email, string departamento)
        {
            var emp = new empleado
            {
                idempleado = id,
                nombre = nombre,
                apellido = apellido,
                fecha_nacimiento = fecha_nac,
                sexo = sexo,
                direccion = direccion,
                telefono = telefono,
                tipo_documento = tipo_documento,
                numero_documento = num_documento,
                email = email,
                departamento = departamento
            };

            var result = da.UpdateEmployee(emp);

            return result;

        }

        public bool Delete(string id)
        {
            var result = false;
            if (!string.IsNullOrEmpty(id))
            {
                int idemp = Convert.ToInt32(id);
                result = da.DeleteEmployee(idemp);
            }

            return result;
        }

    }
}
