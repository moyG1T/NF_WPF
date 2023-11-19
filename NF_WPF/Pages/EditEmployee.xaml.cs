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

namespace NF_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Page
    {
        private Employee employee;
        public EditEmployee(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            DataContext = this.employee;

            DepartmentComboBox.ItemsSource = App.db.Department.ToList();
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


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder err = new StringBuilder();

            if (EmployeeBox.Text == "" || EmployeeBox.Text == null)
                err.AppendLine("ФИО сотрудника пустое");
            if (DepartmentComboBox.SelectedItem == null)
                err.AppendLine("Не выбрана кафедра");
            if (TitleBox.SelectedItem == null)
                err.AppendLine("Не выбрана должность");
            if (TitleRankBox.SelectedItem == null)
                err.AppendLine("Не выбрано звание");
            if (ChefBox.SelectedItem == null && SetChefChecker.IsChecked == false)
                err.AppendLine("Неправильно назначено начальство");
            if (SalaryBox.Text == "")
                err.AppendLine("Не указана зарплата");

            if (err.Length > 0)
            {
                MessageBox.Show(err.ToString());
            }


            else
            {
                if (employee.Id_emp == 0)
                {
                    if (ExpBox.Text == "")
                    {
                        if (SetChefChecker.IsChecked == false)
                        {
                            App.db.Employee.Add(new Employee()
                            {
                                Id_dep = (DepartmentComboBox.SelectedItem as Department).Id_dep,
                                Surname = EmployeeBox.Text,
                                Id_tit = (TitleBox.SelectedItem as Title).Id_tit,
                                Salary = int.Parse(SalaryBox.Text),
                                IsRemoved = false,

                                Chef = (ChefBox.SelectedItem as Employee).Id_emp
                            });
                        }
                        else
                        {
                            App.db.Employee.Add(new Employee()
                            {
                                Id_dep = (DepartmentComboBox.SelectedItem as Department).Id_dep,
                                Surname = EmployeeBox.Text,
                                Id_tit = (TitleBox.SelectedItem as Title).Id_tit,
                                Salary = int.Parse(SalaryBox.Text),
                                IsRemoved = false,
                            });
                        }

                    }
                    else
                    {
                        if (SetChefChecker.IsChecked == false)
                        {
                            App.db.Employee.Add(new Employee()
                            {
                                Id_dep = (DepartmentComboBox.SelectedItem as Department).Id_dep,
                                Surname = EmployeeBox.Text,
                                Id_tit = (TitleBox.SelectedItem as Title).Id_tit,
                                Salary = int.Parse(SalaryBox.Text),
                                IsRemoved = false,

                                Chef = (ChefBox.SelectedItem as Employee).Id_emp,
                                Exp = int.Parse(ExpBox.Text)
                            });
                        }
                        else
                        {
                            App.db.Employee.Add(new Employee()
                            {
                                Id_dep = (DepartmentComboBox.SelectedItem as Department).Id_dep,
                                Surname = EmployeeBox.Text,
                                Id_tit = (TitleBox.SelectedItem as Title).Id_tit,
                                Salary = int.Parse(SalaryBox.Text),
                                IsRemoved = false,

                                Exp = int.Parse(ExpBox.Text)
                            });
                        }
                    }

                    App.db.SaveChanges();
                    AppNav.Navigate(new PageComps("Сотрудники", new EmployeeList()));
                    MessageBox.Show("Добавлено");
                }
                else
                {
                    App.db.SaveChanges();
                    AppNav.Navigate(new PageComps("Сотрудники", new EmployeeList()));
                    MessageBox.Show("Сохранено");
                }
            }
        }

        private void SetChefChecker_Checked(object sender, RoutedEventArgs e)
        {
            ChefBox.SelectedItem = (bool)SetChefChecker.IsChecked ? null : ChefBox.SelectedItem;
        }

        private void ChefBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChefBox.SelectedItem != null)
            {
                SetChefChecker.IsChecked = false;
            }
        }

        private void SalaryBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
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
