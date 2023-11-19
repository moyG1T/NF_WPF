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
using NF_WPF.Data;
using NF_WPF.NavHost;

namespace NF_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для StudentList.xaml
    /// </summary>
    public partial class StudentList : Page
    {
        bool isInverted = false;
        bool isRemovedShowed = false;
        public StudentList()
        {
            InitializeComponent();
            SortByComboBox.SelectedIndex = 0;
            BottomBar.Visibility = App.isAdmin || App.isLecturer ? Visibility.Visible : Visibility.Collapsed;

            ShowRemovedAppointsButton.Visibility = App.isAdmin ? Visibility.Visible : Visibility.Collapsed;
            AddElementButton.Visibility = App.isAdmin ? Visibility.Visible : Visibility.Collapsed;
            RemoveElementButton.Visibility = App.isAdmin ? Visibility.Visible : Visibility.Collapsed;

            StudentListView.ItemsSource = App.isStudent ?
                App.db.Student.Where(x => x.Speciality.Student.FirstOrDefault().Id_stud == App.userId && x.IsRemoved == false).ToList()
                :
                App.db.Student.Where(x => x.IsRemoved == false).ToList();
        }
        private void RefreshFilters()
        {
            IEnumerable<Student> list;
            if (isRemovedShowed)
            {
                list = App.isStudent ?
                App.db.Student.Where(x => x.Speciality.Student.FirstOrDefault().Id_stud == App.userId && x.IsRemoved == true).ToList()
                :
                App.db.Student.Where(x => x.IsRemoved == true).ToList();
            }
            else
            {
                list = App.isStudent ?
                App.db.Student.Where(x => x.Speciality.Student.FirstOrDefault().Id_stud == App.userId && x.IsRemoved == false).ToList()
                :
                App.db.Student.Where(x => x.IsRemoved == false).ToList();
            }

            if (isInverted == false && SortByComboBox.SelectedIndex != 0)
                switch (SortByComboBox.SelectedIndex)
                {
                    case 1:
                        list = list.OrderBy(x => x.Surname).ToList();
                        break;
                    case 2:
                        list = list.OrderBy(x => x.Speciality.SName).ToList();
                        break;
                }
            else if (isInverted == true && SortByComboBox.SelectedIndex != 0)
                switch (SortByComboBox.SelectedIndex)
                {
                    case 1:
                        list = list.OrderByDescending(x => x.Surname).ToList();
                        break;
                    case 2:
                        list = list.OrderByDescending(x => x.Speciality.SName).ToList();
                        break;
                }

            if (SearchbarText.Text != "")
                list = list.Where(x =>
                x.Surname.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Speciality.SName.ToLower().Contains(SearchbarText.Text.ToLower())
                ).ToList();

            StudentListView.ItemsSource = null;
            StudentListView.ItemsSource = list;
            InvertSortByButton.Visibility = SortByComboBox.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
            ClearSearchbarButton.Visibility = SearchbarText.Text == "" ? Visibility.Collapsed : Visibility.Visible;
            ClearFiltersButton.Visibility = SortByComboBox.SelectedIndex == 0 && SearchbarText.Text == "" ? Visibility.Collapsed : Visibility.Visible;
        }

        private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilters();
        }

        private void InvertSortByButton_Click(object sender, RoutedEventArgs e)
        {
            isInverted = !isInverted;
            RefreshFilters();
        }

        private void SearchbarText_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshFilters();
        }

        private void ClearSearchbarButton_Click(object sender, RoutedEventArgs e)
        {
            SearchbarText.Text = "";
            RefreshFilters();
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            isInverted = false;
            SearchbarText.Text = "";
            SortByComboBox.SelectedIndex = 0;
            RefreshFilters();
        }

        private void RemoveElementButton_Click(object sender, RoutedEventArgs e)
        {
            (StudentListView.SelectedItem as Student).IsRemoved = true;
            App.db.SaveChanges();
            RefreshFilters();
        }

        private void ShowRemovedAppointsButton_Click(object sender, RoutedEventArgs e)
        {
            isRemovedShowed = !isRemovedShowed;

            if (isRemovedShowed)
            {
                StudentListView.ItemsSource = App.isStudent ?
                App.db.Student.Where(x => x.Speciality.Student.FirstOrDefault().Id_stud == App.userId && x.IsRemoved == true).ToList()
                :
                App.db.Student.Where(x => x.IsRemoved == true).ToList();
            }
            else
            {
                StudentListView.ItemsSource = App.isStudent ?
                App.db.Student.Where(x => x.Speciality.Student.FirstOrDefault().Id_stud == App.userId && x.IsRemoved == false).ToList()
                :
                App.db.Student.Where(x => x.IsRemoved == false).ToList();
            }

            if (isRemovedShowed == true)
            {
                ShowRemovedAppointsButton.Content = "Актуальный список";
                App.mainWindow.TitleText.Text = "Удаленные элементы";
            }
            else
            {
                ShowRemovedAppointsButton.Content = "Удаленные элементы";
                App.mainWindow.TitleText.Text = "Студенты";
            }

            AddElementButton.Visibility = isRemovedShowed ? Visibility.Collapsed : Visibility.Visible;
            EditElementButton.Visibility = isRemovedShowed ? Visibility.Collapsed : Visibility.Visible;
            RemoveElementButton.Visibility = isRemovedShowed ? Visibility.Collapsed : Visibility.Visible;
            RestoreElementButton.Visibility = isRemovedShowed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void RestoreElementButton_Click(object sender, RoutedEventArgs e)
        {
            (StudentListView.SelectedItem as Student).IsRemoved = false;
            App.db.SaveChanges();
            RefreshFilters();
        }

        private void AddElementButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.Navigate(new PageComps("Добавление", new EditStudent(new Student())));
        }

        private void EditElementButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.Navigate(new PageComps("Редактирование", new EditStudent(StudentListView.SelectedItem as Student)));
        }
    }
}
