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

namespace NF_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Page
    {
        public EditEmployee(Employee employee)
        {
            InitializeComponent();
            DataContext = employee;

            DepartmentComboBox.ItemsSource = App.db.Discipline.ToList();
            DepartmentComboBox.DisplayMemberPath = "DName";

            TitleBox.ItemsSource = App.db.Title.ToList();
            TitleBox.DisplayMemberPath = "TName";

            TitleRankBox.ItemsSource = App.db.TitleRank.ToList();
            TitleRankBox.DisplayMemberPath = "TRRank";

            ChefBox.ItemsSource = App.db.Employee.ToList();
            ChefBox.DisplayMemberPath = "Surname";

            if (employee.Chef != null)
            {
                ChefBox.Text = App.db.Employee.Where(x => x.Id_emp == employee.Chef).FirstOrDefault().Surname.ToString();
            }
        }

        private void ExpBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
