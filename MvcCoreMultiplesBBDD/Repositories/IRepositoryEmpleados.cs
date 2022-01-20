using MvcCoreMultiplesBBDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreMultiplesBBDD.Repositories
{
    /*IMPORTANTE EL PUBLIC*/
    public interface IRepositoryEmpleados
    {
        //creamos los metodos que usaremos en las clases
        List<Empleado> GetEmpleados();
        Empleado FindEmpleado(int id);
        void DeleteEmpleado(int id);

        void UpdateSalarioEmpleado(int id, int incremento);
    }
}
