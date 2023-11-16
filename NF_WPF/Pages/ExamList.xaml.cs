﻿using NF_WPF.Data;
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
    /// Логика взаимодействия для ExamList.xaml
    /// </summary>
    public partial class ExamList : Page
    {
        bool isInverted = false;
        bool AreRemovedShowed = false;
        public ExamList()
        {
            InitializeComponent();
            SortByComboBox.SelectedIndex = 0;
            BottomBar.Visibility = App.isAdmin || App.isLecturer ? Visibility.Visible : Visibility.Collapsed;
            ExamListView.ItemsSource = ExamListView.ItemsSource = App.db.Exam.Where(x => x.IsRemoved == false).ToList();
        }

        private void RefreshFilters()
        {
            IEnumerable<Exam> list = AreRemovedShowed ?
                App.db.Exam.Where(x => x.IsRemoved == true).ToList()
            :
                App.db.Exam.Where(x => x.IsRemoved == false).ToList();


            if (isInverted == false && SortByComboBox.SelectedIndex != 0)
                switch (SortByComboBox.SelectedIndex)
                {
                    case 1:
                        list = list.OrderBy(x => x.Discipline.DName).ToList();
                        break;
                    case 2:
                        list = list.OrderBy(x => x.Employee.Surname).ToList();
                        break;
                    case 3:
                        list = list.OrderBy(x => x.Student.Surname).ToList();
                        break;
                    case 4:
                        list = list.OrderBy(x => x.EDate).ToList();
                        break;
                    case 5:
                        list = list.OrderBy(x => x.Auditory).ToList();
                        break;
                    case 6:
                        list = list.OrderBy(x => x.Points).ToList();
                        break;
                }
            else if (isInverted == true && SortByComboBox.SelectedIndex != 0)
                switch (SortByComboBox.SelectedIndex)
                {
                    case 1:
                        list = list.OrderByDescending(x => x.Discipline.DName).ToList();
                        break;
                    case 2:
                        list = list.OrderByDescending(x => x.Employee.Surname).ToList();
                        break;
                    case 3:
                        list = list.OrderByDescending(x => x.Student.Surname).ToList();
                        break;
                    case 4:
                        list = list.OrderByDescending(x => x.EDate).ToList();
                        break;
                    case 5:
                        list = list.OrderByDescending(x => x.Auditory).ToList();
                        break;
                    case 6:
                        list = list.OrderByDescending(x => x.Points).ToList();
                        break;
                }

            if (SearchbarText.Text != "")
                list = list.Where(x =>
                x.Discipline.DName.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Employee.Surname.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Student.Surname.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Auditory.ToLower().Contains(SearchbarText.Text.ToLower()) ||
                x.Points.ToString().ToLower().Contains(SearchbarText.Text.ToLower())
                ).ToList();

            ExamListView.ItemsSource = null;
            ExamListView.ItemsSource = list;

            InvertSortByButton.Visibility = SortByComboBox.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
            ClearSearchbarButton.Visibility = SearchbarText.Text == "" ? Visibility.Collapsed : Visibility.Visible;
            ClearFiltersButton.Visibility = SortByComboBox.SelectedIndex == 0 && SearchbarText.Text == "" ? Visibility.Collapsed : Visibility.Visible;
        }

        private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilters();
        }

        private void SearchbarText_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshFilters();
        }

        private void InvertSortByButton_Click(object sender, RoutedEventArgs e)
        {
            isInverted = !isInverted;
            RefreshFilters();
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            isInverted = false;
            SortByComboBox.SelectedIndex = 0;
            SearchbarText.Text = "";
            RefreshFilters();
        }

        private void ClearSearchbarButton_Click(object sender, RoutedEventArgs e)
        {
            SearchbarText.Text = "";
            RefreshFilters();
        }
        private void AddElementButton_Click(object sender, RoutedEventArgs e)
        {
            AppNav.Navigate(new PageComps("Добавление", new EditExam(new Exam())));
        }

        private void EditElementButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExamListView.SelectedItem != null)
                AppNav.Navigate(new PageComps("Редактирование", new EditExam(ExamListView.SelectedItem as Exam)));
            else
                MessageBox.Show("Выберите элемент");
        }


        private void RemoveElementButton_Click(object sender, RoutedEventArgs e)
        {
            (ExamListView.SelectedItem as Exam).IsRemoved = true;
            App.db.SaveChanges();
            RefreshFilters();
        }
        private void RestoreElementButton_Click(object sender, RoutedEventArgs e)
        {
            (ExamListView.SelectedItem as Exam).IsRemoved = false;
            App.db.SaveChanges();
            RefreshFilters();
        }

        private void ShowRemovedAppoints_Click(object sender, RoutedEventArgs e)
        {

            AreRemovedShowed = !AreRemovedShowed;

            ExamListView.ItemsSource = AreRemovedShowed ?
                ExamListView.ItemsSource = App.db.Exam.Where(x => x.IsRemoved == true).ToList()
            :
                ExamListView.ItemsSource = App.db.Exam.Where(x => x.IsRemoved == false).ToList();

            if (AreRemovedShowed == false)
            {
                ShowRemovedText.Text = "Актуальный список";
                App.mainWindow.TitleText.Text = "Экзамены";
            }
            else
            {
                ShowRemovedText.Text = "Удаленные элементы";
                App.mainWindow.TitleText.Text = "Удаленные элементы";
            }

            AddElementButton.Visibility = AreRemovedShowed ? Visibility.Collapsed : Visibility.Visible;
            EditElementButton.Visibility = AreRemovedShowed ? Visibility.Collapsed : Visibility.Visible;
            RemoveElementButton.Visibility = AreRemovedShowed ? Visibility.Collapsed : Visibility.Visible;
            RestoreElementButton.Visibility = AreRemovedShowed ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
