using Microsoft.EntityFrameworkCore;
using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region PROCEDIMIENTOS ALMACENADOS

//CREATE DEFINER =`root`@`localhost` PROCEDURE `SP_ALL_EMPLEADOS`()
//BEGIN
//	SELECT * FROM EMP;
//END
#endregion
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
            //var consulta = from datos in this.context.Empleados
            //               select datos;
            //return consulta.ToList();

            string sql = "call hospital.SP_ALL_EMPLEADOS();";
            var consulta = this.context.Empleados.FromSqlRaw(sql);
            return consulta.ToList();
        }
        public void DeleteEmpleado(int id)
        {
            string sql = "call hospital.SP_DELETE_EMPLEADO(@idempleado);";
            MySqlParameter pamid = new MySqlParameter("@idempleado", id);
            this.context.Database.ExecuteSqlRaw(sql, pamid);

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
