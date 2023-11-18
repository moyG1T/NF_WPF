using NF_WPF.Data;
using NF_WPF.NavHost;
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

namespace NF_WPF.Pages.UserControls
{
    /// <summary>
    /// Логика взаимодействия для EmployeeUserControl.xaml
    /// </summary>
    public partial class EmployeeUserControl : UserControl
    {
        private Employee employee;
        public EmployeeUserControl(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            DataContext = this.employee;

            SalaryText.Text = $"{employee.Salary}₽";
            SurnameText.Text = employee.Surname;

            if (employee.Exp != null)
            {
                SurnameText.Text = $"Зав. кафедрой {employee.Surname}";
            }
            else
            {
                ExpPanel.Visibility = Visibility.Collapsed;
            }

            if (employee.Chef == null)
            {
                SurnameText.Text += " (начальник)";
                ChefPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                ChefText.Text = App.db.Employee.Where(x => x.Id_emp == employee.Chef).FirstOrDefault().Surname.ToString();
            }
        }

        private void EditElement_Click(object sender, RoutedEventArgs e)
        {
            AppNav.Navigate(new PageComps("Редактирование", new EditEmployee(employee)));
        }

        private void RemoveElement_Click(object sender, RoutedEventArgs e)
        {
            employee.IsRemoved = true;
            App.db.SaveChanges();
            MessageBox.Show("Элемент удален. Ждите обновления окна.");
        }
    }
}
