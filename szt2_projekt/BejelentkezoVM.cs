﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Szt2_projekt
{
    class BejelentkezoVM
    {
        private FELHASZNALO aktualisFelhasznalo;

        private AdatbazisEntities db;

        public BejelentkezoVM()
        {
            aktualisFelhasznalo = new FELHASZNALO();
        }

        public bool Bejelentkezes(string felhasznalonev, string jelszo)
        {
            FELHASZNALO f = this.TartalmazasVizsgalat(felhasznalonev, jelszo);
            if (f != null)
            {
                aktualisFelhasznalo = f;
                Megosztott.Logolas("Logged in: " + aktualisFelhasznalo.NEV);

                if (f.BEOSZTAS == "ADMIN")
                {
                    AdminWindow aw = new AdminWindow();
                    aw.Show();
                }
                else if (f.BEOSZTAS == "UGYINTEZO")
                {
                    UgyintezoWindow uw = new UgyintezoWindow();
                    uw.Show();
                }
                else
                {
                    UserWindow uw = new UserWindow(aktualisFelhasznalo.FELHASZNALO_ID);
                    uw.Show();
                }
            }

            return f != null;
        }

        private bool TartalmazasVizsgalat(string felhasznalonev)
        {
            db = new AdatbazisEntities();
            int tartalmaz = db.FELHASZNALO.Count(x => x.NEV.Equals(felhasznalonev.ToUpper()));
            return tartalmaz > 0;
        }

        private FELHASZNALO TartalmazasVizsgalat(string felhasznalonev, string jelszo)
        {
            db = new AdatbazisEntities();
            var z = db.FELHASZNALO.Where(f => f.NEV == felhasznalonev.ToUpper());

            if (z.Any()) // z.ToList().Count > 0
            {
                var zz = z.Where(f => f.JELSZO == jelszo.ToUpper());

                if (zz.Any()) // zz.ToList().Count > 0
                    return z.First();
            }

            return null;

        }

    }
}
