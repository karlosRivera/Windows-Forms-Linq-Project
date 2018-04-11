using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCRUD.DataAccessLayer
{
    class EmpleadoDataAccess
    {

        private DataClassesDataContext db = new DataClassesDataContext();

        /// <summary>
        ///  Retorna una lista de empleados por departamento.
        /// </summary>
        /// <param name="departamento"></param>
        /// <returns></returns>
        public List<empleado> GetEmployeesByDepartment(string departamento)
        {
            var empleados = (from e in db.empleados
                            select e).Where(e => e.departamento == departamento).ToList();

            return empleados;

        }

        /// <summary>
        /// Retorna una lista de empleados por su nombre.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<empleado> GetEmployeesByName(string nombre)
        {
            var empleados = (from e in db.empleados
                             select e).Where(g => g.nombre.Contains(nombre)).ToList();

            return empleados;

        }

        /// <summary>
        /// Crea un nuevo objeto de tipo empleado.
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public bool InsertEmployee(empleado empleado)
        {
            try
            {
                db.empleados.InsertOnSubmit(empleado);
                db.SubmitChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Crea un nuevo objeto de tipo empleado.
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public bool UpdateEmployee(empleado emp)
        {
            try
            {
                var seleccion = (from e in db.empleados
                                where e.idempleado == emp.idempleado select e).SingleOrDefault();

                seleccion.nombre = emp.nombre;
                seleccion.apellido = emp.apellido;
                seleccion.tipo_documento = emp.tipo_documento;
                seleccion.sexo = emp.sexo;
                seleccion.direccion = emp.direccion;
                seleccion.telefono = emp.telefono;
                seleccion.numero_documento = emp.numero_documento;
                seleccion.email = emp.email;
                seleccion.departamento = emp.departamento;
                seleccion.fecha_nacimiento = emp.fecha_nacimiento;
               
                db.SubmitChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Elimina un empleado específico.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteEmployee(int id)
        {
            try
            {
                var seleccion = (from e in db.empleados
                                 where e.idempleado == id
                                 select e).SingleOrDefault();

                db.empleados.DeleteOnSubmit(seleccion);
                db.SubmitChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
