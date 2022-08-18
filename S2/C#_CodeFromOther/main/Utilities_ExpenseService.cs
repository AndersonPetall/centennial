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
using BMIcalculator.Models;
using BMIcalculator.Utilities;
namespace BMIcalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            initial();
        }
        private void GenerateBMIColumns(object sender, EventArgs e)
        {
            foreach (var column in ((DataGrid)sender).Columns)
            {
                column.MinWidth = 79;
                if (column.Header.ToString() == "Index")
                {
                    column.Visibility = Visibility.Hidden;
                }
            }
        }
        public void Reset(object sender, EventArgs e)
        {
            textName.Text = null;
            date.SelectedDate = null;
            comboType.SelectedItem = null;
            textHeight.Text = null;
            textWeight.Text = null;
        }
        public void Calculate(object sender, EventArgs e)
        {
            double height = 0, weight = 0;
            if (date.SelectedDate == null ||
                comboType.SelectedItem == null ||
                string.IsNullOrWhiteSpace(textName.Text) ||
                string.IsNullOrWhiteSpace(textHeight.Text) ||
                string.IsNullOrWhiteSpace(textWeight.Text) ||
                !Double.TryParse(textHeight.Text, out height) ||
                !Double.TryParse(textWeight.Text, out weight))
            {
                MessageBox.Show("Please input all needed values in the correct format.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                BMI onebmi = new BMI();
                onebmi.Name = textName.Text;
                onebmi.Height = Convert.ToDouble(textHeight.Text);
                onebmi.Weight = Convert.ToDouble(textWeight.Text);
                onebmi.Date = date.SelectedDate;
                onebmi.Index = Guid.NewGuid().ToString();
                if (((ComboBoxItem)comboType.SelectedItem).Content.ToString() == "imperial")
                {
                    onebmi.Type = Models.Type.imperial;
                    onebmi.result = onebmi.Weight / onebmi.Height / onebmi.Height * 703;
                }
                else
                {
                    onebmi.Type = Models.Type.metric;
                    onebmi.result = onebmi.Weight / onebmi.Height / onebmi.Height;
                }
                try
                {
                    BMIService.Create(onebmi);
                    this.Reset(sender, e);
                    dataBMI.ItemsSource = null;
                    dataBMI.ItemsSource = BMIService.GetAll();
                    MessageBox.Show("BMI successfully calculated!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Search(object sender, EventArgs e)
        {
            List<BMI> bmis = BMIService.GetAll();
            double height = 0, weight = 0;
            if (date.SelectedDate != null) bmis = BMIService.Search((DateTime)date.SelectedDate, bmis);
            if (textName.Text != null) bmis = BMIService.Search(textName.Text, bmis);
            if(!string.IsNullOrWhiteSpace(textWeight.Text) && Double.TryParse(textWeight.Text, out weight)) bmis = BMIService.Search(Convert.ToDouble(textWeight.Text), bmis, "aa");
            if(!string.IsNullOrWhiteSpace(textHeight.Text) && Double.TryParse(textHeight.Text, out height)) bmis = BMIService.Search(Convert.ToDouble(textHeight.Text), bmis, 1);
            if (comboType.SelectedItem != null && ((ComboBoxItem)comboType.SelectedItem).Content.ToString() == "imperial") bmis = BMIService.Search(BMIcalculator.Models.Type.imperial, bmis);
            if (comboType.SelectedItem != null && ((ComboBoxItem)comboType.SelectedItem).Content.ToString() == "metric") bmis = BMIService.Search(BMIcalculator.Models.Type.metric, bmis);
            this.Reset(sender, e);
            dataBMI.ItemsSource = null;
            dataBMI.ItemsSource = bmis;
        }
        public void Change(object sender, EventArgs e)
        {
            List<BMI> bmis = (dataBMI.ItemsSource as ICollection<BMI>).ToList();
            try
            {
                BMIService.Delete(bmis[0].Index);
                this.Calculate(sender, e);
                this.Reset(sender, e);
                dataBMI.ItemsSource = null;
                dataBMI.ItemsSource = BMIService.GetAll();
                MessageBox.Show("Record successfully change!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Delete(object sender, EventArgs e)
        {
            List<BMI> bmis = (dataBMI.ItemsSource as ICollection<BMI>).ToList();
            try
            {
                BMIService.Delete(bmis[0].Index);
                this.Reset(sender, e);
                dataBMI.ItemsSource = null;
                dataBMI.ItemsSource = BMIService.GetAll();
                MessageBox.Show("Record successfully delete!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        public void showAll(object sender, EventArgs e)
        {
            this.Reset(sender, e);
            dataBMI.ItemsSource = null;
            dataBMI.ItemsSource = BMIService.GetAll();
        }
        public void initial()
        {
            btnCalculate.Click += Calculate;
            btnReset.Click += Reset;
            btnSearch.Click += Search;
            btnDelete.Click += Delete;
            btnHistory.Click += showAll;
            btnChange.Click += Change;
            dataBMI.ItemsSource = BMIService.GetAll();
        }
    }
}
