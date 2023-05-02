using GradeBook.ViewModel;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace GradeBook
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        StudentViewModel studentViewModel;
        public Dashboard()
        {
            InitializeComponent();
            studentViewModel = new StudentViewModel();
            DataContext = studentViewModel;
            this.Height = SystemParameters.PrimaryScreenHeight * 0.80;
            this.Width = SystemParameters.PrimaryScreenWidth * 0.60;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void StateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void StudnetsBtn_Click(object sender, RoutedEventArgs e)
        {
            PageTransitioner.SelectedIndex = 0;

            var Converter = new BrushConverter();
            StudnetsBtn.Background = (Brush)Converter.ConvertFromString("#F7F6F4");
            StudnetsBtn.Foreground = (Brush)Converter.ConvertFromString("#4c956c");

            ReportBtn.ClearValue(Button.BackgroundProperty);
            ReportBtn.ClearValue(Button.ForegroundProperty);
            studentViewModel.ClearStudentForm();
        }

        private async void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            
            PageTransitioner.SelectedIndex = 1;
            var Converter = new BrushConverter();
            ReportBtn.Background = (Brush)Converter.ConvertFromString("#F7F6F4");
            ReportBtn.Foreground = (Brush)Converter.ConvertFromString("#4c956c");

            StudnetsBtn.ClearValue(Button.BackgroundProperty);
            StudnetsBtn.ClearValue(Button.ForegroundProperty);
            Stopwatch stopwatch = Stopwatch.StartNew();

            await Task.Run(() =>
            {
                studentViewModel.SortStudents();
                stopwatch.Stop();
            });
        }

       
    }
}
