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
using System.Management;

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

        private void Button_Identify_HDD_Click(object sender, RoutedEventArgs e)
        {
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject managementObject in managementObjectSearcher.Get())
            {
                TreeViewItem hddItemIdParent = new TreeViewItem();
                hddItemIdParent.Header = managementObject["DeviceID"].ToString();

                TreeViewItem childItemModel = new TreeViewItem();
                childItemModel.Header = "Model: " + managementObject["Model"].ToString();
                hddItemIdParent.Items.Add(childItemModel);

                TreeViewItem childItemInterfaceType = new TreeViewItem();
                childItemInterfaceType.Header = "Interface: " + managementObject["InterfaceType"].ToString();
                hddItemIdParent.Items.Add(childItemInterfaceType);

                TreeViewItem childItemSerialNumber = new TreeViewItem();
                childItemSerialNumber.Header = "Serial#: " + managementObject["SerialNumber"].ToString();
                hddItemIdParent.Items.Add(childItemSerialNumber);

                treeView_HDD.Items.Add(hddItemIdParent);
            }
        }
    }
}
