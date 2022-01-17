using Microsoft.AspNetCore.Mvc;
using MvcCoreMultiplesBBDD.Models;
using MvcCoreMultiplesBBDD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreMultiplesBBDD.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;
        public EmpleadosController(RepositoryEmpleados repo) 
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }
        public IActionResult Details(int id) 
        {
            Empleado empleado = this.repo.FindEmpleado(id);
            return View(empleado);
        }

        public IActionResult Delete(int id) 
        {
            this.repo.DeleteEmpleado(id);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateSalario(int id) 
        {
            Empleado empleado = this.repo.FindEmpleado(id);
            return View(empleado);
        }
        [HttpPost]
        public IActionResult UpdateSalario(int idempleado, int incremento) 
        {

            return View();        
        }
    }
}
