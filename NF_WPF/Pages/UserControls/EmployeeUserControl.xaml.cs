using NF_WPF.Data;
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
        public EmployeeUserControl(Employee employee)
        {
            InitializeComponent();
            DataContext = employee;

            SalaryText.Text = $"{employee.Salary}₽";

            if (employee.Exp != null)
            {
                HeadDepartmentText.Text = "Зав. кафедрой";
            }
            else
            {
                ExpPanel.Visibility = Visibility.Collapsed;
            }

            if (employee.Chef == null)
            {
                ChefPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                ChefText.Text = App.db.Employee.Where(x => x.Id_emp == employee.Chef).FirstOrDefault().Surname.ToString();
                //ChefText.Text = employee.Chef.ToString();
            }
        }
    }
}
