using Microsoft.EntityFrameworkCore;
using MvcCoreMultiplesBBDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreMultiplesBBDD.Data
{
    public class HospitalContext: DbContext 
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }
        public DbSet<Empleado> Empleados { get; set; }
    }
}
