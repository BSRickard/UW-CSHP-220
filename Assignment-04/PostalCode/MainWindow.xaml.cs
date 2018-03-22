using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

// Course:  UW CSHP 220 C (2018 Winter)
// Project: Assignment 04
// Author:  RICKARD, Brian
// Date:    2018-03-22

namespace PostalCodes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string region;
        static PostalCode[] postal = 
        {
            new PostalCode(
                "the United States", 
                "^[0-9]{5}(-[0-9]{4})?$"), 
            new PostalCode (
                "Canada", 
                "^[ABCEGHJKLMNPRSTVXY][0-9]{1}[ABCEGHJ-NPRSTV-Z][ ]?[0-9]{1}[ABCEGHJ-NPRSTV-Z][0-9]{1}$")
        };

        public MainWindow()
        {
            InitializeComponent();
            uxSubmit.IsEnabled = false;
            uxPostalCode.Focus();
        }

        private void uxSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Assignment doesn't require any submit functionality
            MessageBox.Show(string.Format("Value entered matches the format used in {0}.", region));
        }

        private void uxPostalCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Restrict input to either #####{-####} or A#A#A# format
            int selection = uxPostalCode.SelectionStart;
            uxPostalCode.Text = uxPostalCode.Text.ToUpper();
            uxPostalCode.SelectionStart = selection;

            region = "";
            foreach (PostalCode code in postal)
            {
                if (code.Regex.IsMatch(uxPostalCode.Text))
                {
                    region = code.Region;
                    uxSubmit.IsEnabled = true;
                    break;
                }
            }

            uxSubmit.IsEnabled = ("" != region);
        }
    }
}
