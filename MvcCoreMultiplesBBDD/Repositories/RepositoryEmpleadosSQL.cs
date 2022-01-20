using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;
using MvcCoreMultiplesBBDD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


#region PROCEDIMIENTOS ALMACENADOS 
//PARA CONSULTA CON BBDD SQLSERVER
//create procedure SP_DELETE_EMPLEADO
//(@IDEMPLEADO INT)
//AS
//    DELETE FROM EMP
//	WHERE EMP_NO=@IDEMPLEADO
//GO
//CREATE PROCEDURE SP_ALL_EMPLEADOS
//AS 
//	SELECT * FROM EMP
//GO
#endregion
namespace MvcCoreMultiplesBBDD.Repository
{
    public class RepositoryEmpleadosSQL: IRepositoryEmpleados
    {
        private HospitalContext context;
        public RepositoryEmpleadosSQL(HospitalContext context) 
        {
            this.context = context;
        }
        public List<Empleado> GetEmpleados() 
        {
            //para hacer consulta con procedimientos 
            string sql = "SP_ALL_EMPLEADOS";
            var consulta = this.context.Empleados.FromSqlRaw(sql);
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
            //LINQ sin procedure
            //Empleado empleado = this.FindEmpleado(id);
            //this.context.Empleados.Remove(empleado);
            //this.context.SaveChanges();

            //procedure esto funciona con SqlServer
            string sql = "SP_DELETE_EMPLEADO @IDEMPLEADO";
            SqlParameter pamid = new SqlParameter("@IDEMPLEADO", id);
            //ExecuteSqlRaw ejecuta la consulta del procedure
            this.context.Database.ExecuteSqlRaw(sql, pamid);
        }

        public void UpdateSalarioEmpleado(int id, int incremento) 
        {
            //cambios que hacemos en la bbdd
            Empleado empleado = this.FindEmpleado(id);
            empleado.Salario += incremento;
            //para guardar los cambios realizados
            this.context.SaveChanges();
        }
    }
}
