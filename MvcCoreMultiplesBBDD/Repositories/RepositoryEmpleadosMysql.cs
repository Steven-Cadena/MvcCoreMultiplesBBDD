using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosMysql : IRepositoryEmpleados
    {
        private HospitalContext context;
        public RepositoryEmpleadosMysql(HospitalContext context) 
        {
            this.context = context;
        }
        public List<Empleado> GetEmpleados()
        {
            var consulta = from datos in this.context.Empleados
                           select datos;
            return consulta.ToList();

        }
        public void DeleteEmpleado(int id)
        {
            throw new NotImplementedException();
        }

        public Empleado FindEmpleado(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateSalarioEmpleado(int id, int incremento)
        {
            throw new NotImplementedException();
        }
    }
}
