using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Scala.SOOEF.Oef4.Core;

namespace Scala.SOOEF.Oef4.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isNew;
        VakantiehuisService vakantiehuisService;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vakantiehuisService = new VakantiehuisService();
            StartSituatie();
            ControlsLeegmaken();
            VulListbox();
        }
        private void StartSituatie()
        {
            grpVakantiehuizen.IsEnabled = true;
            grpDetails.IsEnabled = false;
            btnBewaren.Visibility = Visibility.Hidden;
            btnAnnuleren.Visibility = Visibility.Hidden;
        }
        private void ControlsLeegmaken()
        {
            txtAdres.Text = "";
            txtGemeente.Text = "";
            txtHuisnaam.Text = "";
            txtMaxPersonen.Text = "";
            txtPrijsPerNacht.Text = "";
        }
        private void BewerkSituatie()
        {
            grpVakantiehuizen.IsEnabled = false;
            grpDetails.IsEnabled = true;
            btnBewaren.Visibility = Visibility.Visible;
            btnAnnuleren.Visibility = Visibility.Visible;
        }
        private void VulListbox()
        {
            lstVakantiehuizen.ItemsSource = null;
            lstVakantiehuizen.ItemsSource = vakantiehuisService.Vakantiehuizen;
        }
        private void LstVakantiehuizen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ControlsLeegmaken();
            if (lstVakantiehuizen.SelectedItem != null)
            {
                Vakantiehuis vakantiehuis = (Vakantiehuis)lstVakantiehuizen.SelectedItem;
                txtHuisnaam.Text = vakantiehuis.Huisnaam;
                txtAdres.Text = vakantiehuis.Adres;
                txtGemeente.Text = vakantiehuis.Gemeente;
                txtPrijsPerNacht.Text = vakantiehuis.PrijsPerNacht.ToString("#,##0.00");
                txtMaxPersonen.Text = vakantiehuis.MaxPersonen.ToString();
            }
        }

        private void BtnNieuw_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            ControlsLeegmaken();
            BewerkSituatie();
            txtHuisnaam.Focus();
        }

        private void BtnWijzig_Click(object sender, RoutedEventArgs e)
        {
            if (lstVakantiehuizen.SelectedItem != null)
            {
                isNew = false;
                BewerkSituatie();
                txtHuisnaam.Focus();
            }
        }

        private void BtnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            if (lstVakantiehuizen.SelectedItem != null)
            {
                if (MessageBox.Show("Ben je zeker ?", "Wissen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Vakantiehuis vakantiehuis = (Vakantiehuis)lstVakantiehuizen.SelectedItem;
                    vakantiehuisService.VerwijderVakantiehuis(vakantiehuis);
                    ControlsLeegmaken();
                    VulListbox();
                }
            }
        }

        private void BtnBewaren_Click(object sender, RoutedEventArgs e)
        {
            string huisnaam = txtHuisnaam.Text.Trim();
            string adres = txtAdres.Text.Trim();
            string gemeente = txtGemeente.Text.Trim();
            decimal prijsPerNacht;
            byte maxPersonen;
            decimal.TryParse(txtPrijsPerNacht.Text, out prijsPerNacht);
            byte.TryParse(txtMaxPersonen.Text, out maxPersonen);
            Vakantiehuis vakantiehuis;
            if (isNew)
            {
                try
                {
                    vakantiehuis = new Vakantiehuis(huisnaam, adres, gemeente, prijsPerNacht, maxPersonen);
                }
                catch (Exception fout)
                {
                    MessageBox.Show(fout.Message, "Error",MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                vakantiehuisService.VoegVakantiehuisToe(vakantiehuis);
            }
            else
            {
                vakantiehuis = (Vakantiehuis)lstVakantiehuizen.SelectedItem;
                try
                {
                    vakantiehuis.Huisnaam = huisnaam;
                    vakantiehuis.Adres = adres;
                    vakantiehuis.Gemeente = gemeente;
                    vakantiehuis.PrijsPerNacht = prijsPerNacht;
                    vakantiehuis.MaxPersonen = maxPersonen;
                }
                catch (Exception fout)
                {
                    MessageBox.Show(fout.Message);
                    return;
                }
            }
            VulListbox();
            StartSituatie();
            lstVakantiehuizen.SelectedItem = vakantiehuis;

        }

        private void BtnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            StartSituatie();
            ControlsLeegmaken();
            if (lstVakantiehuizen.SelectedItem != null)
            {
                LstVakantiehuizen_SelectionChanged(null, null);
            }
        }
    }
}
