﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Recursos_Humanos.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Recursos_HumanoEntities : DbContext
    {
        public Recursos_HumanoEntities()
            : base("name=Recursos_HumanoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cargo> cargo { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<Licencias> Licencias { get; set; }
        public virtual DbSet<Nomina> Nomina { get; set; }
        public virtual DbSet<Permiso> Permiso { get; set; }
        public virtual DbSet<Salida> Salida { get; set; }
        public virtual DbSet<Vacaciones> Vacaciones { get; set; }
    }
}
