using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp81
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBoxes();
            txtYearOfBirth.MaxLength = 4;
            txtYearOfLicense.MaxLength = 4;
        }

        private void InitializeComboBoxes()
        {
            cmbLocation.Items.Add("Kuznetsk (Penza Oblast)");
            cmbLocation.Items.Add("Penza (Penza Oblast)");
            cmbLocation.Items.Add("Other cities (Penza Oblast)");
            cmbLocation.Items.Add("Syzran (Samara Oblast)");
            cmbLocation.Items.Add("Samara (Samara Oblast)");
            cmbLocation.Items.Add("Tolyatti (Samara Oblast)");
            cmbLocation.Items.Add("Other cities (Samara Oblast)");
            cmbLocation.Items.Add("Ulyanovsk (Ulyanovsk Oblast)");
            cmbLocation.Items.Add("Dimitrovgrad (Ulyanovsk Oblast)");
            cmbLocation.Items.Add("Other cities (Ulyanovsk Oblast)"); cmbVehicleType.Items.Add("Motorcycle/Scooter (Category A)");
            cmbVehicleType.Items.Add("Passenger Car");
            cmbVehicleType.Items.Add("Cargo Truck");
        }

        private void CalculateInsurance(object sender, RoutedEventArgs e)
        {
            // Проверяем, что все поля заполнены
            if (string.IsNullOrEmpty(txtYearOfBirth.Text) ||
                string.IsNullOrEmpty(txtYearOfLicense.Text) ||
                cmbLocation.SelectedItem == null ||
                cmbVehicleType.SelectedItem == null ||
                string.IsNullOrEmpty(txtVehicleEnginePower.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int yearOfBirth = int.Parse(txtYearOfBirth.Text);
            int yearOfLicense = int.Parse(txtYearOfLicense.Text);
            string location = (string)cmbLocation.SelectedItem;
            string vehicleType = (string)cmbVehicleType.SelectedItem;
            int vehicleEnginePower = int.Parse(txtVehicleEnginePower.Text);

            double territoryCoefficient = GetTerritoryCoefficient(location);
            double ageAndExperienceCoefficient = GetAgeAndExperienceCoefficient(yearOfBirth, yearOfLicense);
            double bonusCoefficient = GetBonusCoefficient(yearOfLicense);
            double powerCoefficient = GetPowerCoefficient(vehicleEnginePower);

            double basePremium = GetBasePremium(vehicleType);
            double totalInsuranceCost = basePremium * territoryCoefficient * ageAndExperienceCoefficient * bonusCoefficient * powerCoefficient;

            lblTotalInsuranceCost.Text = $"Итоговая стоимость: {totalInsuranceCost:C}";
        }

        private double GetTerritoryCoefficient(string location)
        {
            switch (location)
            {
                case "Kuznetsk (Penza Oblast)": return 1.0;
                case "Penza (Penza Oblast)": return 1.4;
                case "Other cities (Penza Oblast)": return 0.7;
                case "Syzran (Samara Oblast)": return 1.1;
                case "Samara (Samara Oblast)": return 1.6;
                case "Tolyatti (Samara Oblast)": return 1.5;
                case "Other cities (Samara Oblast)": return 0.9;
                case "Ulyanovsk (Ulyanovsk Oblast)": return 1.4;
                case "Dimitrovgrad (Ulyanovsk Oblast)": return 1.1;
                case "Other cities (Ulyanovsk Oblast)": return 0.8;
                default: return 1.0;
            }
        }

        private double GetAgeAndExperienceCoefficient(int yearOfBirth, int yearOfLicense)
        {
            int age = DateTime.Now.Year - yearOfBirth;
            int experience = DateTime.Now.Year - yearOfLicense;

            if (age <= 22 && experience <= 3) return 1.8;
            else if (age <= 22 && experience > 3) return 1.6;
            else if (age > 22 && experience <= 3) return 1.7;
            else return 1.0;
        }

        private double GetBonusCoefficient(int yearOfLicense)
        {
            int experience = DateTime.Now.Year - yearOfLicense;
            return experience == 0 ? 3.92 : 3.92 - (0.173 * experience);
        }

        private double GetPowerCoefficient(int enginePower)
        {
            if (enginePower <= 50) return 0.6;
            else if (enginePower <= 70) return 1.0;
            else if (enginePower <= 100) return 1.1;
            else if (enginePower <= 120) return 1.2;
            else if (enginePower <= 150) return 1.4;
            else return 1.6;
        }

        private double GetBasePremium(string vehicleType)
        {
            switch (vehicleType)
            {
                case "Motorcycle/Scooter (Category A)":
                    return 1215;
                case "Passenger Car":
                    return 1980;
                case "Cargo Truck":
                    return 2025;
                default:
                    return 1980;
            }
        }


    }
}