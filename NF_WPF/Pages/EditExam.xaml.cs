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
    /// Логика взаимодействия для EditExam.xaml
    /// </summary>
    public partial class EditExam : Page
    {
        private Exam exam;
        public EditExam(Exam exam)
        {
            InitializeComponent();

            this.exam = exam;
            DataContext = this.exam;

            DisciplineComboBox.ItemsSource = App.db.Discipline.ToList();
            DisciplineComboBox.DisplayMemberPath = "DName";

            LecturerComboBox.ItemsSource = App.db.Employee.ToList();
            LecturerComboBox.DisplayMemberPath = "Surname";

            StudentComboBox.ItemsSource = App.db.Student.ToList();
            StudentComboBox.DisplayMemberPath = "Surname";

            AuditoryComboBox.ItemsSource = App.db.ExamAuditory.ToList();
            AuditoryComboBox.DisplayMemberPath = "AName";
        }

        private void PointsText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            PointsText.MaxLength = 1;
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isFine = int.TryParse(PointsText.Text, out int number);
            StringBuilder err = new StringBuilder();
            if (!(number >= 2 && number <= 5))
                err.AppendLine("Неверная оценка");
            if (DisciplineComboBox.SelectedItem == null)
                err.AppendLine("Не выбрана дисциплина");
            if (LecturerComboBox.SelectedItem == null)
                err.AppendLine("Не выбран преподаватель");
            if (StudentComboBox.SelectedItem == null)
                err.AppendLine("Не выбран студент");
            if (AuditoryComboBox.SelectedItem == null)
                err.AppendLine("Не выбрана аудитория");
            if (ExamDatePicker.Text == "")
                err.AppendLine("Не выбрана дата");

            if (err.Length != 0)
                MessageBox.Show(err.ToString());
            else
            {
                AppNav.Navigate(new PageComps("Экзамены", new ExamList()));
                if (exam.Id_exam != 0)
                {
                    App.db.SaveChanges();
                    MessageBox.Show("Сохранено");
                }
                else
                {
                    Discipline discipline = DisciplineComboBox.SelectedItem as Discipline;
                    Employee lecturer = LecturerComboBox.SelectedItem as Employee;
                    Student student = StudentComboBox.SelectedItem as Student;
                    ExamAuditory examAuditory = AuditoryComboBox.SelectedItem as ExamAuditory;

                    App.db.Exam.Add(new Exam()
                    {
                        Id_disc = discipline.Id_disc,
                        Id_emp = lecturer.Id_emp,
                        Id_stud = student.Id_stud,
                        Auditory = examAuditory.AName,
                        Points = int.Parse(PointsText.Text),
                        EDate = ExamDatePicker.SelectedDate
                    });
                    App.db.SaveChanges();
                    MessageBox.Show("Добавлено");
                }
            }
        }
    }
}
