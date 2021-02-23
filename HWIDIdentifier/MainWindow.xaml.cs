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

namespace HWIDIdentifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Identify_HWID_Click(object sender, RoutedEventArgs e)
        {
            Label_HWID.Content = ReadHelper.HWID.GetValue();
        }

        private void Button_Identify_PCGuid_Click(object sender, RoutedEventArgs e)
        {
            Label_PCGuid.Content = ReadHelper.PCGuid.GetValue();
        }

        private void Button_Identify_PCName_Click(object sender, RoutedEventArgs e)
        {
            Label_PCName.Content = ReadHelper.PCName.GetValue();
        }

        private void Button_Identify_ProductID_Click(object sender, RoutedEventArgs e)
        {
            label_ProductID.Content = ReadHelper.ProductId.GetValue();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
