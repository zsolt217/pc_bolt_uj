﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Szt2_projekt
{
    class FelhasznaloBSL
    {
        AdatbazisEntities DB;
        FelhasznaloVM VM;
        KompatibilitasVizsgalo vizsgalo;
        decimal felhasznaloid;
        RendelesVezerlo rendelesvezerlo;
        KedvencVezerlo kedvencvezerlo;
        public FelhasznaloBSL(decimal felhasznaloid, FelhasznaloVM Felhasznalovm)
        {
            VM = Felhasznalovm;
            DB = new AdatbazisEntities();
            this.felhasznaloid = felhasznaloid;
            FelhasznaloAdatbetoltesVMbe(felhasznaloid);
            vizsgalo = new KompatibilitasVizsgalo(VM); //feliratkozik az ablak termékváltozás eseményére
            TermekekBetoltese();
            RendelesBetoltesVMbe();
            rendelesvezerlo = new RendelesVezerlo(Rang.Felhasznalo);
            kedvencvezerlo = new KedvencVezerlo();
        }
        void FelhasznaloAdatbetoltesVMbe(decimal felhasznaloid)//felhasználóvm elemeinek feltöltése
        {
            try
            {
                var aktFelhasznalo = DB.SZEMELYES_ADATOK.Where(x => (int)x.FELHASZNALO_ID == (int)felhasznaloid).Select(x => new { felhnev = x.FELHASZNALO.NEV, cim = x.CIM, email = x.EMAILCIM, keresztnev = x.KERESZTNEV, vezeteknev = x.VEZETEKNEV, telefonszam = x.TELEFONSZAM }).ToList();
                if (aktFelhasznalo.Count() == 1)
                {
                    VM.Cim = aktFelhasznalo.First().cim;
                    VM.Email = aktFelhasznalo.First().email;
                    VM.Keresztnev = aktFelhasznalo.First().keresztnev;
                    VM.Vezeteknev = aktFelhasznalo.First().vezeteknev;
                    VM.Telefonszam = aktFelhasznalo.First().telefonszam;
                    VM.Felhasznalonev = aktFelhasznalo.First().felhnev;
                }
            }
            catch (Exception e)//hiba esetén logolás
            {
                Megosztott.Logolas(e.InnerException.Message);
            }
        }

        public void TermekekBetoltese()
        {
            vizsgalo.Ujrabetoltes = true;
            List<ALAPLAP> alaplapok = DB.ALAPLAP.ToList();
            alaplapok.Add(new ALAPLAP { TIPUSSZAM = "*nincs elem kivalasztva" });
            VM.Alaplapok = alaplapok;
            VM.SelectedAlaplap = VM.Alaplapok.Last();
            List<CPU> cpuk = DB.CPU.ToList();
            cpuk.Add(new CPU { TIPUSSZAM = "*nincs elem kivalasztva" });
            VM.Cpuk = cpuk;
            VM.SelectedCpu = VM.Cpuk.Last();
            List<GPU> gpuk = DB.GPU.ToList();
            gpuk.Add(new GPU { TIPUSSZAM = "*nincs elem kivalasztva" });
            VM.Gpuk = gpuk;
            VM.SelectedGpu = VM.Gpuk.Last();
            List<HAZ> hazak = DB.HAZ.ToList();
            hazak.Add(new HAZ { TIPUSSZAM = "*nincs elem kivalasztva" });
            VM.Hazak = hazak;
            VM.SelectedHaz = VM.Hazak.Last();
            List<HDD> hddk = DB.HDD.ToList();
            hddk.Add(new HDD { TIPUSSZAM = "*nincs elem kivalasztva" });
            VM.Hddk = hddk;
            VM.SelectedHdd = VM.Hddk.Last();
            List<MEMORIA> memoriak = DB.MEMORIA.ToList();
            memoriak.Add(new MEMORIA { TIPUSSZAM = "*nincs elem kivalasztva" });
            VM.Memoriak = memoriak;
            VM.SelectedMemoria = VM.Memoriak.Last();
            List<SSD> ssdk = DB.SSD.ToList();
            ssdk.Add(new SSD { TIPUSSZAM = "*nincs elem kivalasztva" });
            VM.Ssdk = ssdk;
            VM.SelectedSsd = VM.Ssdk.Last();
            List<TAP> tapok = DB.TAP.ToList();
            tapok.Add(new TAP { TIPUSSZAM = "*nincs elem kivalasztva" });
            VM.Tapok = tapok;
            VM.SelectedTap = VM.Tapok.Last();
            vizsgalo.Ujrabetoltes = false;
        }
        public void RendelesMentes()
        {
            if (VM.SelectedAlaplap.TIPUSSZAM.Contains("*") || VM.SelectedCpu.TIPUSSZAM.Contains("*") ||
                VM.SelectedHaz.TIPUSSZAM.Contains("*") || VM.SelectedTap.TIPUSSZAM.Contains("*") || VM.SelectedHdd.TIPUSSZAM.Contains("*") ||
                VM.SelectedMemoria.TIPUSSZAM.Contains("*"))
            {
                MessageBox.Show("Nem sikerült a megrendelést rögzíteni, mert a konfiguráció hiányos! Kérjük válassz ki minden alkatrészből egyet!");
            }
            else
            {
                {
                    var p = DB.RENDELESEK.OrderByDescending(x => x.RENDELES_ID).FirstOrDefault();
                    int newId = (null == p ? 0 : (int)p.RENDELES_ID) + 1;
                    RENDELESEK uj = new RENDELESEK
                    {
                        ALAPLAP_ID = VM.SelectedAlaplap.ALAPLAP_ID,
                        CPU_ID = VM.SelectedCpu.CPU_ID,
                        FELHASZNALO_ID = felhasznaloid,
                        GPU_ID = VM.SelectedGpu.GPU_ID,
                        HAZ_ID = VM.SelectedHaz.HAZ_ID,
                        HDD_ID = VM.SelectedHdd.HDD_ID,
                        MEMORIA_ID = VM.SelectedMemoria.MEMORIA_ID,
                        SSD_ID = (VM.SelectedSsd.TIPUSSZAM.Contains("*") ? (decimal?)null : VM.SelectedSsd.SSD_ID),
                        RENDELES_ID = newId,
                        TAP_ID = VM.SelectedTap.TAP_ID
                    };
                    rendelesvezerlo.RendelesMentes(uj);
                }
            }
        }

        public void RendelesMenteseKedvencbol(KEDVENCEK selectedKedvenc)
        {
            var p = DB.RENDELESEK.OrderByDescending(x => x.RENDELES_ID).FirstOrDefault();
            int newId = (null == p ? 0 : (int)p.RENDELES_ID) + 1;
            RENDELESEK uj = new RENDELESEK
            {
                ALAPLAP_ID = selectedKedvenc.ALAPLAP_ID,
                CPU_ID = selectedKedvenc.CPU_ID,
                FELHASZNALO_ID = felhasznaloid,
                GPU_ID = selectedKedvenc.GPU_ID,
                HAZ_ID = selectedKedvenc.HAZ_ID,
                HDD_ID = selectedKedvenc.HDD_ID,
                MEMORIA_ID = selectedKedvenc.MEMORIA_ID,
                SSD_ID = selectedKedvenc.SSD_ID ?? (decimal?)null,
                RENDELES_ID = newId,
                TAP_ID = selectedKedvenc.TAP_ID
            };
            try
            {
                DB.RENDELESEK.Add(uj);
                DB.SaveChanges();
                MessageBox.Show("Rendelés leadva!");
            }
            catch (Exception hiba)
            {
                Megosztott.Logolas(hiba.InnerException.Message);
                MessageBox.Show("Adatbázishiba, nem sikerült rögzíteni.");
            }
        }

        public void KedvencekMentes()
        {
            if (VM.SelectedAlaplap.TIPUSSZAM.Contains("*") || VM.SelectedCpu.TIPUSSZAM.Contains("*") ||
                           VM.SelectedHaz.TIPUSSZAM.Contains("*") || VM.SelectedTap.TIPUSSZAM.Contains("*") || VM.SelectedHdd.TIPUSSZAM.Contains("*") ||
                           VM.SelectedMemoria.TIPUSSZAM.Contains("*"))
            {
                MessageBox.Show("Nem sikerült a megrendelést rögzíteni, mert a konfiguráció hiányos! Kérjük válassz ki minden minden alkatrészből egyet!");
            }
            else
            {
                var p = DB.KEDVENCEK.OrderByDescending(x => x.KEDVENCEK_ID).FirstOrDefault();
                int newId = (null == p ? 0 : (int)p.KEDVENCEK_ID) + 1;
                KEDVENCEK uj = new KEDVENCEK
                {
                    ALAPLAP_ID = VM.SelectedAlaplap.ALAPLAP_ID,
                    CPU_ID = VM.SelectedCpu.CPU_ID,
                    FELHASZNALO_ID = felhasznaloid,
                    GPU_ID = VM.SelectedGpu.GPU_ID,
                    HAZ_ID = VM.SelectedHaz.HAZ_ID,
                    HDD_ID = VM.SelectedHdd.HDD_ID,
                    MEMORIA_ID = VM.SelectedMemoria.MEMORIA_ID,
                    SSD_ID = (VM.SelectedSsd.TIPUSSZAM.Contains("*") ? (decimal?)null : VM.SelectedSsd.SSD_ID),
                    KEDVENCEK_ID = newId,
                    TAP_ID = VM.SelectedTap.TAP_ID
                };
                kedvencvezerlo.MentesKedvencekbe(felhasznaloid, uj);
            }
        }

        public void KedvencModositas(KEDVENCEK selectKedvenc)
        {
            VM.SelectedAlaplap = VM.Alaplapok.Where(x => x.ALAPLAP_ID == selectKedvenc.ALAPLAP.ALAPLAP_ID).Single();

            VM.SelectedCpu = VM.Cpuk.Where(x => x.CPU_ID == selectKedvenc.CPU.CPU_ID).Single();

            VM.SelectedGpu = VM.Gpuk.Where(x => x.GPU_ID == selectKedvenc.GPU.GPU_ID).Single();

            VM.SelectedMemoria = VM.Memoriak.Where(x => x.MEMORIA_ID == selectKedvenc.MEMORIA.MEMORIA_ID).Single();

            VM.SelectedHdd = VM.Hddk.Where(x => x.HDD_ID == selectKedvenc.HDD.HDD_ID).Single();

            if(selectKedvenc.SSD == null)  VM.SelectedSsd = VM.Ssdk.Where(x => x.TIPUSSZAM.Contains("*")).Single(); 
            else VM.SelectedSsd = VM.Ssdk.Where(x => x.SSD_ID == selectKedvenc.SSD.SSD_ID).Single();

            VM.SelectedTap = VM.Tapok.Where(x => x.TAP_ID == selectKedvenc.TAP.TAP_ID).Single();

            VM.SelectedHaz = VM.Hazak.Where(x => x.HAZ_ID == selectKedvenc.HAZ.HAZ_ID).Single();
        }

        public void KedvencTorles()
        {
            if (VM.SelectedKedvenc != null && kedvencvezerlo.KedvencTorles(VM.SelectedKedvenc))
            {
                VM.KedvencBetolt();
                MessageBox.Show("Sikeres törlés.");

            }
            else
            {
                MessageBox.Show("Sikertelen törlés.");
            }
        }

        private void RendelesBetoltesVMbe()
        {
            VM.Rendelesek = DB.RENDELESEK.Where(x => x.FELHASZNALO_ID == felhasznaloid).ToList();
        }
    }
}
