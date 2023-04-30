using GradeBook.ViewModel;
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
        }

        private void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            PageTransitioner.SelectedIndex = 1;

            var Converter = new BrushConverter();
            ReportBtn.Background = (Brush)Converter.ConvertFromString("#F7F6F4");
            ReportBtn.Foreground = (Brush)Converter.ConvertFromString("#4c956c");

            StudnetsBtn.ClearValue(Button.BackgroundProperty);
            StudnetsBtn.ClearValue(Button.ForegroundProperty);
        }

        private void RandomStudnetsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveStudent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
