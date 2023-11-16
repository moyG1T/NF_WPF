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

            DisciplineComboBox.ItemsSource = App.db.Discipline.Distinct().ToList();
            DisciplineComboBox.DisplayMemberPath = "DName";

            LecturerComboBox.ItemsSource = App.db.Employee.Distinct().ToList();
            LecturerComboBox.DisplayMemberPath = "Surname";

            StudentComboBox.ItemsSource = App.db.Student.Distinct().ToList();
            StudentComboBox.DisplayMemberPath = "Surname";

            AuditoryComboBox.ItemsSource = App.db.ExamAuditory.Distinct().ToList();
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
            else if ()
                err.AppendLine("Оценка");

            if (err.Length != 0)
                MessageBox.Show(err.ToString());
            else
                MessageBox.Show("Сохранено");
        }
    }
}
