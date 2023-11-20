using NF_WPF.NavHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            //if (LoginBox.Text == "1337AdminServera228")
            if (LoginBox.Text == "")
            {
                App.isAdmin = true;
                AppNav.Navigate(new PageComps("Главная", new MainMenu()));
                MessageBox.Show("Вы вошли, как администратор");
            }
            else if (App.db.Employee.Where(x => x.Id_emp.ToString() == LoginBox.Text).FirstOrDefault() != null)
            {
                App.isLecturer = true;
                App.userId = int.Parse(LoginBox.Text);
                AppNav.Navigate(new PageComps("Главная", new MainMenu())); 
                MessageBox.Show("Вы вошли, как сотрудник");
            }
            else if (App.db.Student.Where(x => x.Id_stud.ToString() == LoginBox.Text).FirstOrDefault() != null)
            {
                App.isStudent = true;
                App.userId = int.Parse(LoginBox.Text);
                AppNav.Navigate(new PageComps("Главная", new MainMenu())); 
                MessageBox.Show("Вы вошли, как студент");
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
