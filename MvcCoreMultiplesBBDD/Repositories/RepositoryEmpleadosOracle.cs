using Microsoft.EntityFrameworkCore;
using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region PROCEDIMIENTOS ALMACENADOS ORACLE
//create or replace procedure SP_DELETE_EMPLEADO
//(P_IDEMPLEADO INT)
//AS
//BEGIN
//   DELETE FROM EMP
//   WHERE EMP_NO=P_IDEMPLEADO;
//COMMIT;
//END;

//create or replace procedure SP_UPDATE_SALARIO_EMP
//(P_IDEMPLEADO INT, P_INCREMENTO INT)
//AS
//BEGIN
//  UPDATE EMP SET SALARIO = SALARIO + P_INCREMENTO
//  WHERE EMP_NO = P_IDEMPLEADO;
//COMMIT;
//END;

//--EN ORACLE NO PODEMOS INCLUIR SELECT DENTRO DE LOS PROCEDIMIENTOS ALMACENADOS
//--PARA PODER UTILIZAR SELECT Y DEVOLVER
//--CONSULTAS DEBEMOS UTILIZAR PARAMETROS
//--DE SALIDA QUE CONTENGAN EN CURSOR

//create or replace procedure SP_ALL_EMPLEADOS
//(P_EMPLEADOS OUT SYS_REFCURSOR)
//AS
//BEGIN
//  OPEN P_EMPLEADOS FOR
//  SELECT * FROM EMP;
//END;
#endregion
namespace MvcCoreMultiplesBBDD.Repositories
{
    //IMPLEMENTAMOS LOS METODOS DE LA INTERFAZ
    public class RepositoryEmpleadosOracle : IRepositoryEmpleados
    {
        private HospitalContext context;
        public RepositoryEmpleadosOracle(HospitalContext context) 
        {
            this.context = context;
        }
        public List<Empleado> GetEmpleados()
        {
            //var consulta = from datos in this.context.Empleados
            //               select datos;
            //return consulta.ToList();

            string sql = "BEGIN " +
                "SP_ALL_EMPLEADOS(:P_EMPLEADOS);" +
                "END;";
            OracleParameter pamempleados = new OracleParameter();
            //para parametros de salida
            pamempleados.ParameterName = "P_EMPLEADOS";
            pamempleados.Value = null;//es null por que es un objeto
            pamempleados.Direction = System.Data.ParameterDirection.Output;//poner el tipo de salida
            pamempleados.OracleDbType = OracleDbType.RefCursor;// indicar el tipo de dato que es
            var consulta = this.context.Empleados.FromSqlRaw(sql, pamempleados);
            //AL SER UN CURSO DE SALIDA, DENTRO DE LA CONSULTA 
            //ESTAN LOS DATOS DIRECTAMENTE
            return consulta.ToList();

        }
        public void DeleteEmpleado(int id)
        {
            //EN ORACLE LOS PARAMETROS EN LA LLAMADA
            //SE DENOMINAN CON : PARAMETRO
            string sql =
                "BEGIN " +
                "SP_DELETE_EMPLEADO(:P_IDEMPLEADO);" +
                " END;";
            //igualar el parametro al id que recibimnos
            OracleParameter pamid = new OracleParameter("P_IDEMPLEADO", id);
            this.context.Database.ExecuteSqlRaw(sql, pamid);
                
        }

        public Empleado FindEmpleado(int id)
        {
            return this.context.Empleados.SingleOrDefault(x => x.IdEmpleado == id);
        }

        public void UpdateSalarioEmpleado(int id, int incremento)
        {
            string sql =
                "BEGIN " +
                "SP_UPDATE_SALARIO_EMP(:P_IDEMPLEADO, :P_INCREMENTO);" +
                "END;";
            OracleParameter pamid = new OracleParameter("P_IDEMPLEADO", id);
            OracleParameter paminc= new OracleParameter("P_INCREMENTO", incremento);
            this.context.Database.ExecuteSqlRaw(sql, pamid, paminc);
        }
    }
}
