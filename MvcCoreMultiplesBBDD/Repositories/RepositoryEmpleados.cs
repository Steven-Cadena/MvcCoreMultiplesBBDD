using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreMultiplesBBDD.Repository
{
    public class RepositoryEmpleados
    {
        private HospitalContext context;
        public RepositoryEmpleados(HospitalContext context) 
        {
            this.context = context;
        }
        public List<Empleado> GetEmpleados() 
        {
            var consulta = from datos in this.context.Empleados
                           select datos;
            return consulta.ToList();
        }

        public Empleado FindEmpleado(int id) 
        {
            var consulta = from datos in this.context.Empleados
                           where datos.IdEmpleado == id
                           select datos;
            //APLICAR EL FILTRO NO SOBRE LA BBDD CON LINQ
            //APLICAR EL FILTRO SOBRE LA COLECCION
            return consulta.ToList().FirstOrDefault();
        }

        public void DeleteEmpleado(int id) 
        {
            Empleado empleado = this.FindEmpleado(id);
            this.context.Empleados.Remove(empleado);
            this.context.SaveChanges();
        }

        public void UpdateSalarioempleado(int id, int incremento) 
        {
            //cambios que hacemos en la bbdd
            Empleado empleado = this.FindEmpleado(id);
            empleado.Salario += incremento;
            //para guardar los cambios realizados
            this.context.SaveChanges();
        }
    }
}
