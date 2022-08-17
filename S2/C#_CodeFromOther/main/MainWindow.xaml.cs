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
using ExpenseTracker.Models;
using ExpenseTracker.Utilities;
using static ExpenseTracker.Models.Expense;
using static ExpenseTracker.Models.Record;

namespace ExpenseTracker
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
        public void ResetExpense(object sender, EventArgs e)
        {
            txtAmountExpense.Text = null;
            dateExpense.SelectedDate = null;
            txtNameExpense.Text=null;
            comboResponsorExpense.SelectedItem = null;
            comboCategoryExpense.SelectedItem = null;
            txtDescriptionExpense.Text = null;
        }
        public void ResetSearch(object sender, EventArgs e)
        {
            txtAmountSearch.Text = null;
            dateSearch.SelectedDate = null;
            txtNameSearch.Text = null;
            comboResponsorSearch.SelectedItem = null;
            comboCategorySearch.SelectedItem = null;
            txtExpenseID.Text = null;
        }
        public void CreateExpense(object sender, EventArgs e)
        {
            if(dateExpense.SelectedDate == null || 
                comboCategoryExpense.SelectedItem == null||
                comboResponsorExpense.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtAmountExpense.Text) ||
                string.IsNullOrWhiteSpace(txtNameExpense.Text) ||
                string.IsNullOrWhiteSpace(txtDescriptionExpense.Text))
            {
                MessageBox.Show("Please input all needed values before creating the new account.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Record oneRecord = new Record();
                oneRecord.Name = txtNameExpense.Text;
                if (((ComboBoxItem)comboResponsorExpense.SelectedItem).Content.ToString() == "family") oneRecord.Responsor = Responsors.family;
                else oneRecord.Responsor = Responsors.individual;
                Expense oneExpense = new Expense(amount: Convert.ToInt32(txtAmountExpense.Text), date: (DateTime)dateExpense.SelectedDate, record: oneRecord, description: txtDescriptionExpense.Text);
                if (((ComboBoxItem)comboCategoryExpense.SelectedItem).Content.ToString() == "grocery") oneExpense.Category = Categories.grocery;
                else if (((ComboBoxItem)comboCategoryExpense.SelectedItem).Content.ToString() == "accommodation") oneExpense.Category = Categories.accommodation;
                else if (((ComboBoxItem)comboCategoryExpense.SelectedItem).Content.ToString() == "diet") oneExpense.Category = Categories.diet;
                else if (((ComboBoxItem)comboCategoryExpense.SelectedItem).Content.ToString() == "transportation") oneExpense.Category = Categories.transportation;
                else if (((ComboBoxItem)comboCategoryExpense.SelectedItem).Content.ToString() == "necessities") oneExpense.Category = Categories.necessities;
                else oneExpense.Category = Categories.other;
                
                try
                {
                    ExpenseService.Create(oneExpense);
                    this.ResetExpense(sender, e);
                    dataExpense.ItemsSource = null;
                    dataExpense.ItemsSource = ExpenseService.GetAll();
                    MessageBox.Show("Expense successfully created!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error123!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void CreateSearch()
        {
            //if (dateSearch.SelectedDate == null ||
            //    comboCategorySearch.SelectedItem == null ||
            //    string.IsNullOrWhiteSpace(txtAmountSearch.Text) ||
            //    string.IsNullOrWhiteSpace(txtNameSearch.Text) ||
            //    string.IsNullOrWhiteSpace(txtResponsorSearch.Text) ||
            //    string.IsNullOrWhiteSpace(txtDescriptionExpense.Text))
            //{
            //    MessageBox.Show("Please input all needed values before creating the new account.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //else
            //{

            //}
        }
        public void initial()
        {
            btnCreateExpense.Click += CreateExpense;
            btnResetExpense.Click += ResetExpense;
            //btnExpenseSearch
            //    btnResetSearch
            dataExpense.ItemsSource = ExpenseService.GetAll();
        }

    }
}
