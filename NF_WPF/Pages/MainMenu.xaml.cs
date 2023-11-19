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

namespace NF_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void NavigateToExamsButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.Navigate(new PageComps("Экзамены", new ExamList()));
        }

        private void NavigateToStudentsButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.Navigate(new PageComps("Студенты", new StudentList()));
        }

        private void NavigateToEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.Navigate(new PageComps("Сотрудники", new EmployeeList()));
        }

        private void NavigateToDisciplinesButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.Navigate(new PageComps("Дисциплины", new DisciplineList()));
        }
    }
}
