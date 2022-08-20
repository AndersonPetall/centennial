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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VehicleManagementSystem.Models;
using VehicleManagementSystem.Utilities;

namespace VehicleManagementSystem
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

        #region Car
        public void ResetCar(object sender, EventArgs e)
        {
            txtLicensePlate.Text = null;
            comboMakeType.SelectedItem = null;
            comboColor.SelectedItem = null;
        }
        public void CreateCar(object sender, EventArgs e)
        {
            if (comboMakeType.SelectedItem == null ||
                comboColor.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtLicensePlate.Text))
            {
                MessageBox.Show("Please input all needed values in the correct format.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Car oneCar = new Car(); 
                oneCar.LicensePlate = txtLicensePlate.Text;
                if (((ComboBoxItem)comboMakeType.SelectedItem).Content.ToString() == "Lexus")
                {
                    oneCar.MakeType = Models.MakeType.Lexus;
                }
                else if (((ComboBoxItem)comboMakeType.SelectedItem).Content.ToString() == "BWM")
                {
                    oneCar.MakeType = Models.MakeType.BWM;
                }
                else if (((ComboBoxItem)comboMakeType.SelectedItem).Content.ToString() == "Toyota")
                {
                    oneCar.MakeType = Models.MakeType.Toyota;
                }
                else
                {
                    oneCar.MakeType = Models.MakeType.Other;
                }
                if (((ComboBoxItem)comboColor.SelectedItem).Content.ToString() == "White")
                {
                    oneCar.Color = Models.Color.White;
                }
                else if (((ComboBoxItem)comboColor.SelectedItem).Content.ToString() == "Black")
                {
                    oneCar.Color = Models.Color.Black;
                }
                else if (((ComboBoxItem)comboColor.SelectedItem).Content.ToString() == "Grey")
                {
                    oneCar.Color = Models.Color.Grey;
                }
                else
                {
                    oneCar.Color = Models.Color.Other;
                }
                Owner oneOwner = new Owner();
                oneCar.Owner = oneOwner;
                try
                {
                    ManagementSystem.CreateCar(oneCar);
                    this.ResetCar(sender, e);
                    dataCar.ItemsSource = null;
                    dataCar.ItemsSource = ManagementSystem.GetAllCar();
                    MessageBox.Show("BMI successfully calculated!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void DeleteCar(object sender, EventArgs e)
        {
            List<Car> cars = (dataCar.ItemsSource as ICollection<Car>).ToList();
            try
            {
                ManagementSystem.DeleteCar(cars[0]);
                this.ResetCar(sender, e);
                dataCar.ItemsSource = null;
                dataCar.ItemsSource = ManagementSystem.GetAllCar();
                MessageBox.Show("Record successfully delete!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void ChangeCar(object sender, EventArgs e)
        {
            List<Car> cars = (dataCar.ItemsSource as ICollection<Car>).ToList();
            try
            {
                ManagementSystem.DeleteCar(cars[0]);
                this.CreateCar(sender, e);
                this.ResetCar(sender, e);
                dataCar.ItemsSource = null;
                dataCar.ItemsSource = ManagementSystem.GetAllCar();
                MessageBox.Show("Record successfully change!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void GetAllCar(object sender, EventArgs e)
        {
            dataCar.ItemsSource = ManagementSystem.GetAllCar();
        }
        public void AddOwner(object sender, EventArgs e)
        {
            List<Car> cars = (dataCar.ItemsSource as ICollection<Car>).ToList();
            Car newCar = cars[0];
            ManagementSystem.DeleteCar(cars[0]);

            try
            {
                List<Owner> owners = (dataOwner.ItemsSource as ICollection<Owner>).ToList();
                newCar.Owner = owners[0];
                ManagementSystem.CreateCar(newCar);
                this.ResetCar(sender, e);
                dataCar.ItemsSource = null;
                dataCar.ItemsSource = ManagementSystem.GetAllCar();
                MessageBox.Show("Owner successfully add!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void RemoveOwner(object sender, EventArgs e)
        {
            List<Car> cars = (dataCar.ItemsSource as ICollection<Car>).ToList();
            if(cars.Count == 0)
            {
                MessageBox.Show("Car doesn't exist!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Car newCar = new Car();
                newCar.LicensePlate = cars[0].LicensePlate;
                newCar.Color = cars[0].Color;
                newCar.MakeType = cars[0].MakeType;
                try
                {
                    ManagementSystem.DeleteCar(cars[0]);
                    ManagementSystem.CreateCar(newCar);
                    this.ResetCar(sender, e);
                    dataCar.ItemsSource = null;
                    dataCar.ItemsSource = ManagementSystem.GetAllCar();
                    MessageBox.Show("Owner successfully delete!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void SearchCar(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLicensePlate.Text))
            {
                MessageBox.Show("Please input all needed values in the correct format.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<Car> cars = ManagementSystem.GetAllCar();
                cars = ManagementSystem.SearchCar(txtLicensePlate.Text, cars);
                dataCar.ItemsSource = cars;
            }
        }
        #endregion
        #region Owner
        public void ResetOwner(object sender, EventArgs e)
        {
            txtName.Text = null;
            dateSelectedDOB.SelectedDate = null;
        }
        public void CreateOwner(object sender, EventArgs e)
        {
            if (dateSelectedDOB.SelectedDate == null ||
                string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please input all needed values in the correct format.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Owner onwowner = new Owner();
                onwowner.Name = txtName.Text;
                onwowner.Birthday = dateSelectedDOB.SelectedDate.Value;
                try
                {
                    ManagementSystem.CreateOwner(onwowner);
                    this.ResetOwner(sender, e);
                    dataOwner.ItemsSource = null;
                    dataOwner.ItemsSource = ManagementSystem.GetAllOwner();
                    MessageBox.Show("Owner successfully Create!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void DeleteOwner(object sender, EventArgs e)
        {
            List<Owner> owners = (dataOwner.ItemsSource as ICollection<Owner>).ToList();
            try
            {
                ManagementSystem.DeleteOwner(owners[0]);
                this.ResetCar(sender, e);
                dataCar.ItemsSource = null;
                dataCar.ItemsSource = ManagementSystem.GetAllCar();
                MessageBox.Show("Record successfully delete!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public void ChangeOwner(object sender, EventArgs e)
        {
            List<Owner> owners = (dataOwner.ItemsSource as ICollection<Owner>).ToList();
            try
            {
                ManagementSystem.DeleteOwner(owners[0]);
                this.CreateOwner(sender, e);
                this.ResetCar(sender, e);
                dataCar.ItemsSource = null;
                dataCar.ItemsSource = ManagementSystem.GetAllCar();
                MessageBox.Show("Record successfully change!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void SearchOwner(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please input all needed values in the correct format.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<Owner> owners = ManagementSystem.GetAllOwner();
                owners = ManagementSystem.SearchOwner(txtName.Text, owners);
                dataOwner.ItemsSource = owners;
            }
        }
        public void GetAllOwner(object sender, EventArgs e)
        {
            dataOwner.ItemsSource = ManagementSystem.GetAllOwner();
        }
        #endregion
        #region Manager
        public void ResetManager(object sender, EventArgs e)
        {
            txtName.Text = null;
            dateSelectedDOB.SelectedDate = null;
        }
        public void CreateManager(object sender, EventArgs e)
        {
            if (dateSelectedDOB.SelectedDate == null ||
                string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please input all needed values in the correct format.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Manager onemanager = new Manager();
                onemanager.Name = txtName.Text;
                onemanager.Birthday = dateSelectedDOB.SelectedDate.Value;
                try
                {
                    ManagementSystem.CreateManager(onemanager);
                    this.ResetOwner(sender, e);
                    dataManager.ItemsSource = null;
                    dataManager.ItemsSource = ManagementSystem.GetAllManager();
                    MessageBox.Show("Manager successfully Create!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void DeleteManager(object sender, EventArgs e)
        {
            List<Manager> managers = (dataManager.ItemsSource as ICollection<Manager>).ToList();
            try
            {
                ManagementSystem.DeleteManager(managers[0]);
                this.ResetManager(sender, e);
                dataManager.ItemsSource = null;
                dataManager.ItemsSource = ManagementSystem.GetAllManager();
                MessageBox.Show("Record successfully delete!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public void ChangeManager(object sender, EventArgs e)
        {
            List<Manager> managers = (dataManager.ItemsSource as ICollection<Manager>).ToList();
            try
            {
                ManagementSystem.DeleteManager(managers[0]);
                this.CreateManager(sender, e);
                this.ResetManager(sender, e);
                dataManager.ItemsSource = null;
                dataManager.ItemsSource = ManagementSystem.GetAllManager();
                MessageBox.Show("Record successfully change!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void GetAllManager(object sender, EventArgs e)
        {
            dataManager.ItemsSource = ManagementSystem.GetAllManager();
        }
        public void AddCar(object sender, EventArgs e)
        {
            List<Manager> managers = (dataManager.ItemsSource as ICollection<Manager>).ToList();
            try
            {
                List<Car> cars = (dataCar.ItemsSource as ICollection<Car>).ToList();
                if (!managers[0].Cars.Contains(cars[0]))
                {
                    Manager newManager = managers[0];
                    ManagementSystem.DeleteManager(managers[0]);
                    newManager.Cars.Add(cars[0]);
                    ManagementSystem.CreateManager(newManager);
                    this.ResetCar(sender, e);
                    dataCar.ItemsSource = null;
                    dataCar.ItemsSource = ManagementSystem.GetAllCar();
                    MessageBox.Show("Car successfully add!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Car already exist!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void RemoveCar(object sender, EventArgs e)
        {
            List<Car> cars = (dataCar.ItemsSource as ICollection<Car>).ToList();
            List<Manager> managers = (dataManager.ItemsSource as ICollection<Manager>).ToList();
            if(cars.Count == 0 || managers.Count == 0)
            {
                MessageBox.Show("Car or Manager don't exist!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                List<Car> newCars = new List<Car>();
                foreach (var i in managers[0].Cars)
                {
                    if (i.LicensePlate != cars[0].LicensePlate) newCars.Add(i);
                }
                if (newCars.Count == managers[0].Cars.Count)
                {
                    MessageBox.Show("No such car in list!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Manager newManager = managers[0];
                    newManager.Cars = newCars;
                    ManagementSystem.DeleteManager(managers[0]);
                    ManagementSystem.CreateManager(newManager);
                    this.ResetCar(sender, e);
                    dataCar.ItemsSource = null;
                    dataCar.ItemsSource = ManagementSystem.GetAllCar();
                    MessageBox.Show("Car successfully remove!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                //if (!managers[0].Cars.Contains(cars[0]))
                //{
                //    MessageBox.Show("No such car in list!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
                //else
                //{
                //    Manager newManager = managers[0];
                //    ManagementSystem.DeleteManager(managers[0]);
                //    List<Car> newCars = new List<Car>();
                //    foreach (var i in cars)
                //    {
                //        if (i.LicensePlate != cars[0].LicensePlate) newCars.Add(i);
                //    }
                    
                //}
            }
        }
        public void SearchManager(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please input all needed values in the correct format.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<Manager> managers = ManagementSystem.GetAllManager();
                managers = ManagementSystem.SearchManager(txtName.Text, managers);
                dataManager.ItemsSource = managers;
            }
        }
        #endregion

        public void initial()
        {
            btnCreateCar.Click += CreateCar;
            btnSearchCar.Click += SearchCar;
            btnChangeCar.Click += ChangeCar;
            btnDeleteCar.Click += DeleteCar;
            btnAddOwner.Click += AddOwner;
            btnRemoveOwner.Click += RemoveOwner;
            btnShowAllCar.Click += GetAllCar;
            ////////////////////////////
            btnCreateOwner.Click += CreateOwner;
            btnChangeOwner.Click += ChangeOwner;
            btnSearchOwner.Click += SearchOwner;
            btnDeleteOwner.Click += DeleteOwner;
            btnShowAllOwner.Click += GetAllOwner;
            //////////////////////////
            btnCreateManager.Click += CreateManager;
            btnChangeManager.Click += ChangeManager;
            btnSearchManager.Click += SearchManager;
            btnDeleteManager.Click += DeleteManager;
            btnAddToList.Click += AddCar;
            btnDeleteFromList.Click += RemoveCar;
            btnShowAllManager.Click += GetAllManager;
        }
    }
}
