﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Szt2_projekt
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AdatbazisEntities : DbContext
    {
        public AdatbazisEntities()
            : base("name=AdatbazisEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ALAPLAP> ALAPLAP { get; set; }
        public virtual DbSet<CPU> CPU { get; set; }
        public virtual DbSet<FELHASZNALO> FELHASZNALO { get; set; }
        public virtual DbSet<GPU> GPU { get; set; }
        public virtual DbSet<HAZ> HAZ { get; set; }
        public virtual DbSet<HDD> HDD { get; set; }
        public virtual DbSet<MEMORIA> MEMORIA { get; set; }
        public virtual DbSet<RENDELESEK> RENDELESEK { get; set; }
        public virtual DbSet<SSD> SSD { get; set; }
        public virtual DbSet<TAP> TAP { get; set; }
        public virtual DbSet<UZENETEK> UZENETEK { get; set; }
        public virtual DbSet<SZEMELYES_ADATOK> SZEMELYES_ADATOK { get; set; }
        public virtual DbSet<KEDVENCEK> KEDVENCEK { get; set; }
    }
}
