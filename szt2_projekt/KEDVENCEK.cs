//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class KEDVENCEK
    {
        public decimal KEDVENCEK_ID { get; set; }
        public decimal FELHASZNALO_ID { get; set; }
        public decimal ALAPLAP_ID { get; set; }
        public decimal CPU_ID { get; set; }
        public decimal MEMORIA_ID { get; set; }
        public decimal GPU_ID { get; set; }
        public decimal TAP_ID { get; set; }
        public decimal HDD_ID { get; set; }
        public Nullable<decimal> SSD_ID { get; set; }
        public decimal HAZ_ID { get; set; }
        public string ALLAPOT { get; set; }
    
        public virtual ALAPLAP ALAPLAP { get; set; }
        public virtual CPU CPU { get; set; }
        public virtual FELHASZNALO FELHASZNALO { get; set; }
        public virtual GPU GPU { get; set; }
        public virtual HAZ HAZ { get; set; }
        public virtual HDD HDD { get; set; }
        public virtual MEMORIA MEMORIA { get; set; }
        public virtual SSD SSD { get; set; }
        public virtual TAP TAP { get; set; }

        public override string ToString()
        {
            return "Alaplap: " + ALAPLAP.TIPUSSZAM + "\nCPU: " + CPU.TIPUSSZAM + "\nGPU: " + GPU.TIPUSSZAM + "\nH�z: " + HAZ.TIPUSSZAM +
                "\nHDD: " + HDD.TIPUSSZAM + "\nSSD: " + SSD.TIPUSSZAM + "\nMem�ria: " + MEMORIA.TIPUSSZAM + "\nT�p: " + TAP.TIPUSSZAM;
        }
    }
}
