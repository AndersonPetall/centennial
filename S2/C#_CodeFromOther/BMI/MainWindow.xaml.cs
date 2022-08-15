using BMICalc.Model;
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

namespace BMICalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            
            InitializeComponent();
            this.Initialize();
            btnCalc.Click += Calculate;
        }

        public void Calculate(object sent, EventArgs e)
        {
            try
            {
                var result = new BMI();
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                bool isChecked = iUnits.IsChecked == true;
                UnitType unit = isChecked?  UnitType.Imperial :  UnitType.Metric;
                double feet = Convert.ToDouble(hFeet.Text);
                double inches = Convert.ToDouble(hInches.Text);
                double centi = Convert.ToDouble(hMeters.Text);
                double lbs = Convert.ToDouble(wPounds.Text);
                double kg = Convert.ToDouble(wKg.Text);

            
                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                { throw new Exception("Please input your First and Last Names"); }

                double calc = (result.CalculateBMI(firstName, lastName, unit, feet, inches, centi, lbs, kg));
                
                this.Initialize();
                MessageBox.Show($"{firstName} {lastName}'s BMI is {calc:F2}");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error); }


            
            
        }

        public void Initialize()
        {
            BMI history = null;
            history = new BMI();
            History.ItemsSource = history.GetAll();
        }

        
    }
}
