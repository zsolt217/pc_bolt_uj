﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace Szt2_projekt
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        AdatbazisEntities DB = new AdatbazisEntities();
        FelhasznaloVM VM;
        FelhasznaloBSL BS;
        decimal id;

        public UserWindow(decimal felhasznaloid)
        {
            InitializeComponent();
            VM = new FelhasznaloVM(felhasznaloid);
            BS = new FelhasznaloBSL(felhasznaloid, VM);
            id = felhasznaloid;
            DataContext = VM;

        }


        private void kedvencMegrendelesButton_Click(object sender, RoutedEventArgs e)
        {
            BS.RendelesMenteseKedvencbol(VM.SelectedKedvenc);
        }

        private void MegrendelésButton_Click(object sender, RoutedEventArgs e)
        {
            BS.RendelesMentes();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            labelek = new List<Label>();
            labelek.Add(lbl1);
            labelek.Add(lbl2);
            labelek.Add(lbl3);
            labelek.Add(lbl4);
            labelek.Add(lbl5);
            labelek.Add(lbl6);
            labelek.Add(lbl7);
            alaplaplabelnevek = new string[] { "Ár (Ft):", "Név:", "Processzor foglalat:", "Memóriaslotok száma:", "Memóriatípus:", "Chipset:", "Méretszabvány:" };
            tarololabelnevek = new string[] { "Ár (Ft):", "Név:", "Kapacitás (GB):" };
            proclabelnevek = new string[] { "Ár (Ft):", "Név:", "Foglalat:", "Fogyasztás (W):", "Órajel (Ghz):", "Magok száma:" };
            gpulabelnevek = new string[] { "Ár (Ft):", "Név:", "Fogyasztás (W):", "Memória (GB):" };
            ramlabelnevek = new string[] { "Ár (Ft):", "Név:", "Típus:", "Órajel (Mhz):", "Kapacitás: (GB)" };
            taplabelnevek = new string[] { "Ár: (Ft)", "Név:", "Teljesítmény: (W)" };
            hazlabelnevek = new string[] { "Ár (Ft):", "Név:", "Méretszabvány:" };

        }
        string[] alaplaplabelnevek;
        string[] tarololabelnevek;
        string[] proclabelnevek;
        string[] gpulabelnevek;
        string[] ramlabelnevek;
        string[] taplabelnevek;
        string[] hazlabelnevek;
        List<Label> labelek;


        private void Kijelentkezo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MainWindow uj = new MainWindow();
            uj.Show();
        }

        private void FelhAdatModosit_Click(object sender, RoutedEventArgs e)
        {
            RegisztracioWindow uj = new RegisztracioWindow(id);
            uj.ShowDialog();
        }

        private void kuldesButton_Click(object sender, RoutedEventArgs e)
        {
            if (VM.UzenetKuldes())
            {
                MessageBox.Show("Sikeres küldés");
            }
            else MessageBox.Show("Sikertelen küldés");
        }

        private void MentésButton_Click(object sender, RoutedEventArgs e)
        {
            BS.KedvencekMentes();
            VM.KedvencMentes();
        }


        #region unchecked rbutton események,nem kell megnyitni

        void LabelContentTorles()
        {
            foreach (Label akt in labelek)
            {
                akt.Content = string.Empty;
            }
        }
        private void rbAlaplap_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelContentTorles();
        }

        private void rbProcesszor_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelContentTorles();
        }

        private void rbVideokartya_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelContentTorles();
        }

        private void rbMemoria_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelContentTorles();
        }

        private void rbWinchester_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelContentTorles();
        }

        private void rbSSD_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelContentTorles();
        }

        private void rbTap_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelContentTorles();
        }

        private void rbHaz_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelContentTorles();
        }
        #endregion

        void LabelNullaz()
        {
            lbl11.Content = string.Empty;
            lbl12.Content = string.Empty;
            lbl13.Content = string.Empty;
            lbl14.Content = string.Empty;
            lbl15.Content = string.Empty;
            lbl16.Content = string.Empty;
            lbl17.Content = string.Empty;
        } 
        AdatbazisEntities ab;
        #region Adatmegjelenítés
        private void rbAlaplap_Checked(object sender, RoutedEventArgs e)
        {
            LabelNullaz();
            for (int i = 0; i < alaplaplabelnevek.Length; i++)
            {
                labelek[i].Content = alaplaplabelnevek[i];
            }
            if (cBoxAlaplap.SelectedIndex == cBoxAlaplap.Items.Count - 1)
            {
                LabelNullaz();
            }
            else
            {
                ALAPLAP aktalaplap = new ALAPLAP();
                aktalaplap = (ALAPLAP)cBoxAlaplap.SelectedValue;

                lbl11.Content = aktalaplap.AR;
                lbl12.Content = aktalaplap.TIPUSSZAM;
                lbl13.Content = aktalaplap.CPUFOGLALAT;
                lbl14.Content = aktalaplap.MEMORIASLOTOK;
                lbl15.Content = aktalaplap.MEMORIATIPUS;
                lbl16.Content = aktalaplap.CHIPSET;
                lbl17.Content = aktalaplap.MERETSZABVANY;
            }

        }

        private void rbWinchester_Checked(object sender, RoutedEventArgs e)
        {
            LabelNullaz();
            for (int i = 0; i < tarololabelnevek.Length; i++)
            {
                labelek[i].Content = tarololabelnevek[i];
            }
            if (cBoxHDD.SelectedIndex == cBoxHDD.Items.Count - 1)
            {
                LabelNullaz();
            }
            else
            {
                HDD aktHDD = new HDD();

                aktHDD = (HDD)cBoxHDD.SelectedValue;

                lbl11.Content = aktHDD.AR;
                lbl12.Content = aktHDD.TIPUSSZAM;
                lbl13.Content = aktHDD.KAPACITAS;
            }
        }


        private void rbProcesszor_Checked(object sender, RoutedEventArgs e)
        {
            LabelNullaz();
            for (int i = 0; i < proclabelnevek.Length; i++)
            {
                labelek[i].Content = proclabelnevek[i];
            }
            if (cBoxCPU.SelectedIndex == cBoxCPU.Items.Count - 1)
            {
                LabelNullaz();
            }
            else
            {
                CPU akt = new CPU();

                akt = (CPU)cBoxCPU.SelectedValue;

                lbl11.Content = akt.AR;
                lbl12.Content = akt.TIPUSSZAM;
                lbl13.Content = akt.CPUFOGLALAT;
                lbl14.Content = akt.FOGYASZTAS;
                lbl15.Content = akt.SEBESSEG;
                lbl16.Content = akt.MAGOK;
            }
        }

        private void rbVideokartya_Checked(object sender, RoutedEventArgs e)
        {
            LabelNullaz();
            for (int i = 0; i < gpulabelnevek.Length; i++)
            {
                labelek[i].Content = gpulabelnevek[i];
            }
            if (cBoxGPU.SelectedIndex == cBoxGPU.Items.Count - 1)
            {
                LabelNullaz();
            }
            else
            {
                GPU akt = new GPU();

                akt = (GPU)cBoxGPU.SelectedValue;

                lbl11.Content = akt.AR;
                lbl12.Content = akt.TIPUSSZAM;
                lbl13.Content = akt.FOGYASZTAS;
                lbl14.Content = akt.MEMORIA;
            }
        }


        private void rbMemoria_Checked(object sender, RoutedEventArgs e)
        {
            LabelNullaz();
            for (int i = 0; i < ramlabelnevek.Length; i++)
            {
                labelek[i].Content = ramlabelnevek[i];
            }
            if (cBoxRAM.SelectedIndex==cBoxRAM.Items.Count-1)
            {
                LabelNullaz();
            }
            else
            {
                MEMORIA akt = new MEMORIA();

                akt = (MEMORIA)cBoxRAM.SelectedValue;

                lbl11.Content = akt.AR;
                lbl12.Content = akt.TIPUSSZAM;
                lbl13.Content = akt.MEMORIATIPUS;
                lbl14.Content = akt.SEBESSEG;
                lbl15.Content = akt.KAPACITAS;
            }
        }

        private void rbSSD_Checked(object sender, RoutedEventArgs e)
        {
            LabelNullaz();
            for (int i = 0; i < tarololabelnevek.Length; i++)
            {
                labelek[i].Content = tarololabelnevek[i];
            }
            if (cBoxSSD.SelectedIndex==cBoxSSD.Items.Count-1)
            {
                LabelNullaz();
            }
            else
            {
                SSD akt = new SSD();

                akt = (SSD)cBoxSSD.SelectedValue;

                lbl11.Content = akt.AR;
                lbl12.Content = akt.TIPUSSZAM;
                lbl13.Content = akt.KAPACITAS;
            }
        }

        private void rbTap_Checked(object sender, RoutedEventArgs e)
        {
            LabelNullaz();
            for (int i = 0; i < taplabelnevek.Length; i++)
            {
                labelek[i].Content = taplabelnevek[i];
            }
            if (cBoxTapok.SelectedIndex==cBoxTapok.Items.Count-1)
            {
                LabelNullaz();
            }
            else
            {
                TAP akt = new TAP();

                akt = (TAP)cBoxTapok.SelectedValue;

                lbl11.Content = akt.AR;
                lbl12.Content = akt.TIPUSSZAM;
                lbl13.Content = akt.TELJESITMENY;
            }
            
        }

        private void rbHaz_Checked(object sender, RoutedEventArgs e)
        {
            LabelNullaz();
            for (int i = 0; i < hazlabelnevek.Length; i++)
            {
                labelek[i].Content = hazlabelnevek[i];
            }
            if (cBoxHazak.SelectedIndex==cBoxHazak.Items.Count-1)
            {
                LabelNullaz();
            }
            else
            {
                HAZ akt = new HAZ();

                akt = (HAZ)cBoxHazak.SelectedValue;

                lbl11.Content = akt.AR;
                lbl12.Content = akt.TIPUSSZAM;
                lbl13.Content = akt.MERETSZABVANY;
            }
            
        }

        private void cBoxAlaplap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cBoxAlaplap.SelectedIndex != -1 && (rbAlaplap.IsChecked == true) && cBoxAlaplap.SelectedIndex != cBoxAlaplap.Items.Count - 1)
            {
                ALAPLAP aktalaplap = new ALAPLAP();
                aktalaplap = (ALAPLAP)cBoxAlaplap.SelectedValue;

                lbl11.Content = aktalaplap.AR;
                lbl12.Content = aktalaplap.TIPUSSZAM;
                lbl13.Content = aktalaplap.CPUFOGLALAT;
                lbl14.Content = aktalaplap.MEMORIASLOTOK;
                lbl15.Content = aktalaplap.MEMORIATIPUS;
                lbl16.Content = aktalaplap.CHIPSET;
                lbl17.Content = aktalaplap.MERETSZABVANY;

            }
            else if (rbAlaplap.IsChecked == true)
            {
                LabelNullaz();
            }
        }

        private void cBoxCPU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cBoxCPU.SelectedIndex != -1 && (rbProcesszor.IsChecked == true) && cBoxCPU.SelectedIndex != cBoxCPU.Items.Count - 1)
            {
                CPU akt = new CPU();

                akt = (CPU)cBoxCPU.SelectedValue;

                lbl11.Content = akt.AR;
                lbl12.Content = akt.TIPUSSZAM;
                lbl13.Content = akt.CPUFOGLALAT;
                lbl14.Content = akt.FOGYASZTAS;
                lbl15.Content = akt.SEBESSEG;
                lbl16.Content = akt.MAGOK;

            }
            else if (rbProcesszor.IsChecked == true)
            {
                LabelNullaz();
            }
        }

        private void cBoxHDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cBoxHDD.SelectedIndex != -1 && (rbWinchester.IsChecked == true) && cBoxHDD.SelectedIndex != cBoxHDD.Items.Count - 1)
            {
                //ab = new AdatbazisEntities();
                HDD aktHDD = new HDD();
                //string keres = cBoxHDD.SelectedItem.ToString();
                //var q = from akt in ab.HDD
                //        where akt.TIPUSSZAM == keres
                //        select akt;

                //aktHDD = q.FirstOrDefault();

                aktHDD = (HDD)cBoxHDD.SelectedValue;

                lbl11.Content = aktHDD.AR;
                lbl12.Content = aktHDD.TIPUSSZAM;
                lbl13.Content = aktHDD.KAPACITAS;

            }
            else if (rbWinchester.IsChecked == true)
            {
                LabelNullaz();
            }
        }

        private void cBoxGPU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cBoxGPU.SelectedIndex != -1 && (rbVideokartya.IsChecked == true) && cBoxGPU.SelectedIndex != cBoxGPU.Items.Count - 1)
            {
                GPU akt = new GPU();

                akt = (GPU)cBoxGPU.SelectedValue;

                lbl11.Content = akt.AR;
                lbl12.Content = akt.TIPUSSZAM;
                lbl13.Content = akt.FOGYASZTAS;
                lbl14.Content = akt.MEMORIA;

            }
            else if (rbVideokartya.IsChecked == true)
            {
                LabelNullaz();
            }
        }

        private void cBoxRAM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cBoxRAM.SelectedIndex != -1 && (rbMemoria.IsChecked == true) && cBoxRAM.SelectedIndex != cBoxRAM.Items.Count - 1)
            {
                MEMORIA akt = new MEMORIA();

                akt = (MEMORIA)cBoxRAM.SelectedValue;

                lbl11.Content = akt.AR;
                lbl12.Content = akt.TIPUSSZAM;
                lbl13.Content = akt.MEMORIATIPUS;
                lbl14.Content = akt.SEBESSEG;
                lbl15.Content = akt.KAPACITAS;
               
            }
            else if (rbMemoria.IsChecked == true)
            {
                LabelNullaz();
            }
        }

        private void cBoxSSD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cBoxSSD.SelectedIndex != -1 && (rbSSD.IsChecked == true) && cBoxSSD.SelectedIndex != cBoxSSD.Items.Count - 1)
            {               
                SSD akt = new SSD();
               
                akt = (SSD)cBoxSSD.SelectedValue;

                lbl11.Content = akt.AR;
                lbl12.Content = akt.TIPUSSZAM;
                lbl13.Content = akt.KAPACITAS;

            }
            else if (rbSSD.IsChecked == true)
            {
                LabelNullaz();
            }
        }

        private void cBoxTapok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cBoxTapok.SelectedIndex != -1 && (rbTap.IsChecked == true) && cBoxTapok.SelectedIndex != cBoxTapok.Items.Count - 1)
            {
                TAP akt = new TAP();

                akt = (TAP)cBoxTapok.SelectedValue;

                lbl11.Content = akt.AR;
                lbl12.Content = akt.TIPUSSZAM;
                lbl13.Content = akt.TELJESITMENY;

            }
            else if (rbTap.IsChecked==true)
            {
                LabelNullaz();
            }
        }

        private void cBoxHazak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cBoxHazak.SelectedIndex != -1 && (rbHaz.IsChecked == true) && cBoxHazak.SelectedIndex != cBoxHazak.Items.Count - 1)
            {
                HAZ akt = new HAZ();

                akt = (HAZ)cBoxHazak.SelectedValue;

                lbl11.Content = akt.AR;
                lbl12.Content = akt.TIPUSSZAM;
                lbl13.Content = akt.MERETSZABVANY;

            }
            else if (rbHaz.IsChecked == true)
            {
                LabelNullaz();
            }
        }
#endregion 
        private void kedvencModositasButton_Click(object sender, RoutedEventArgs e)
        { 
            
            BS.TermekekBetoltese();
            KonfiguracioTabItem.IsSelected = true;
            BS.KedvencModositas((KEDVENCEK)listBox.SelectedItem);
        }

        private void KedvencTorles_Click(object sender, RoutedEventArgs e)
        {
            BS.KedvencTorles();
        }
    }
}
