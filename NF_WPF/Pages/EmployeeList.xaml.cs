using NF_WPF.Data;
using NF_WPF.NavHost;
using NF_WPF.Pages.UserControls;
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
using System.Windows.Threading;

namespace NF_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Page
    {
        bool isInverted = false;
        bool isRemovedShowed = false;
        private DispatcherTimer timer;

        public EmployeeList()
        {
            InitializeComponent();

            SortByComboBox.SelectedIndex = 0;
            BottomBar.Visibility = App.isAdmin || App.isLecturer ? Visibility.Visible : Visibility.Collapsed;

            ShowRemovedAppointsButton.Visibility = App.isAdmin ? Visibility.Visible : Visibility.Collapsed;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(15);
            Loaded += MainWindow_Loaded;
            timer.Tick += Timer_Tick;
            Unloaded += MainWindow_UnLoaded;

            RefreshFilters();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
        private void MainWindow_UnLoaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            RefreshFilters();
        }

        private void RefreshFilters()
        {
            IEnumerable<Employee> list = isRemovedShowed ?
                App.db.Employee.Where(x => x.IsRemoved == true)
                :
                App.db.Employee.Where(x => x.IsRemoved == false);

            if (SortByComboBox.SelectedIndex != 0)
            {
                if (isInverted)
                    switch (SortByComboBox.SelectedIndex)
                    {
                        case 1:
                            list = list.OrderBy(x => x.Department.DName);
                            break;
                        case 2:
                            list = list.OrderBy(x => x.Title.TName);
                            break;
                        case 3:
                            list = list.OrderBy(x => x.Salary);
                            break;
                        case 4:
                            list = list.Where(x => x.Exp != null).OrderBy(x => x.Exp);
                            break;
                        case 5:
                            list = list.Where(x => x.Chef == null).OrderBy(x => x.Chef);
                            break;
                    }
                else
                    switch (SortByComboBox.SelectedIndex)
                    {
                        case 1:
                            list = list.OrderByDescending(x => x.Department.DName);
                            break;
                        case 2:
                            list = list.OrderByDescending(x => x.Title.TName);
                            break;
                        case 3:
                            list = list.OrderByDescending(x => x.Salary);
                            break;
                        case 4:
                            list = list.Where(x => x.Exp != null).OrderByDescending(x => x.Exp);
                            break;
                        case 5:
                            list = list.Where(x => x.Chef == null).OrderByDescending(x => x.Chef);
                            break;
                    }
            }

            if (SearchbarText.Text != "" || SearchbarText.Text != null)
            {
                list = list.Where(x =>
                x.Surname.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Department.DName.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Exp.ToString().ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Salary.ToString().ToLower().Contains(SearchbarText.Text.ToLower())
                );
            }

            EmployeeWrapPanel.Children.Clear();
            foreach (var item in list)
            {
                EmployeeWrapPanel.Children.Add(new EmployeeUserControl(item));
            }

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

        private void AddElementButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.Navigate(new PageComps("Редактирование", new EditEmployee(new Employee())));
            RefreshFilters();
        }

        private void ShowRemovedAppoints_Click(object sender, RoutedEventArgs e)
        {
            isRemovedShowed = !isRemovedShowed;

            RefreshFilters();

            if (isRemovedShowed == true)
            {
                ShowRemovedText.Text = "Актуальный список";
                App.mainWindow.TitleText.Text = "Удаленные элементы";
            }
            else
            {
                ShowRemovedText.Text = "Удаленные элементы";
                App.mainWindow.TitleText.Text = "Экзамены";
            }

            ElementPanel.Visibility = isRemovedShowed ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
