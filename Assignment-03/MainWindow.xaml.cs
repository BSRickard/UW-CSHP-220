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
using System.Data.Entity;

// Course:  UW CSHP 220 C (2018 Winter)
// Project: Assignment 03
// Author:  RICKARD, Brian
// Date:    2018-03-20

namespace Assignment_03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Models.User user = new Models.User();

        public MainWindow()
        {
            InitializeComponent();
            //WindowState = WindowState.Maximized;
            uxName.Focus();
            SetSubmitButtonStatus(null, null);
        }

        public override void EndInit()
        {
            base.EndInit();
            //uxName.DataContext = user;
            //uxNameError.DataContext = user;
            uxContainer.DataContext = user;

            var sample = new SampleEntities();
            sample.Users.Load();
            uxList.ItemsSource = sample.Users.Local;
        }

        private void SetSubmitButtonStatus(object sender, TextChangedEventArgs e)
        {
            uxSubmit.IsEnabled = uxName.Text != "" && uxPassword.Text != "";
        }

        private void uxSubmit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Format("Submitting password: {0}", uxPassword.Text));
            //MessageBox.Show("Submitting password:" + uxPassword.Password);

            var window = new SecondWindow();
            Application.Current.MainWindow = window;
            Close();
            window.Show();
        }
    }
}
