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
    /// Логика взаимодействия для MyProfile.xaml
    /// </summary>
    public partial class MyProfile : Page
    {
        public MyProfile()
        {
            InitializeComponent();
            UserIdBox.Text = App.userId.ToString();
            
            if (App.isStudent)
            {
                NameBox.Text = App.db.Student.Where(x => x.Id_stud == App.userId).FirstOrDefault().Surname.ToString();
                TitleText.Text = "Моя специальность";
                TitleBox.Text = App.db.Student.Where(x => x.Id_stud == App.userId).FirstOrDefault().Speciality.SName.ToString();
            }
            else if (App.isLecturer)
            {
                NameBox.Text = App.db.Employee.Where(x => x.Id_emp == App.userId).FirstOrDefault().Surname.ToString();
                TitleText.Text = "Моя должность";
                TitleBox.Text = $"{App.db.Employee.Where(x => x.Id_emp == App.userId).FirstOrDefault().Title.TName}" +
                    $" {App.db.Employee.Where(x => x.Id_emp == App.userId).FirstOrDefault().Title.TitleRank.TRRank}";
            }
            else
            {
                UserIdBox.Text = "1337AdminServera228";
                DescriptionBox.Text = "Я админ сервера";
                TitlePanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
