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
    /// Логика взаимодействия для EditStudent.xaml
    /// </summary>
    public partial class EditStudent : Page
    {
        private Student student;
        public EditStudent(Student student)
        {
            InitializeComponent();
            this.student = student;
            DataContext = this.student;

            SpecialityComboBox.ItemsSource = App.db.Speciality.ToList();
            SpecialityComboBox.DisplayMemberPath = "SName";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder err = new StringBuilder();
            if (SpecialityComboBox.SelectedItem == null)
                err.AppendLine("Не выбрана специальность");
            if (StudentBox.Text == "")
                err.AppendLine("Не выбран студент");

            if (err.Length != 0)
                MessageBox.Show(err.ToString());
            else
            {
                if (student.Id_stud != 0)
                {
                    App.db.SaveChanges();
                    AppNav.Navigate(new PageComps("Студенты", new StudentList()));
                    MessageBox.Show("Сохранено");
                }
                else
                {
                    App.db.Student.Add(new Student()
                    {
                        Surname = StudentBox.Text,
                        Id_spec = (SpecialityComboBox.SelectedItem as Speciality).Id_spec,
                        IsRemoved = false
                    });
                    App.db.SaveChanges();
                    AppNav.Navigate(new PageComps("Студенты", new StudentList()));
                    MessageBox.Show("Добавлено");
                }
            }
        }
    }
}
