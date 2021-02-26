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
        private void AppExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
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
            DiskDriveHelper();
        }

        private void Button_Spoof_HWID_Click(object sender, RoutedEventArgs e)
        {
            Label_Spoof_HWID.Content = WriteHelper.HWID.SpoofHWID();
        }
        private void Button_Spoof_PCGuid_Click(object sender, RoutedEventArgs e)
        {
            Label_Spoof_GUID.Content = WriteHelper.PCGuid.SpoofPCGuid();
        }
        private void Button_Spoof_PCName_Click(object sender, RoutedEventArgs e)
        {
            Label_Spoof_PCName.Content = WriteHelper.PCName.SpoofPCName();
        }
        private void Button_Spoof_ProductID_Click(object sender, RoutedEventArgs e)
        {
            Label_Spoof_ProductID.Content = WriteHelper.ProductId.SpoofProductID();
        }
        private void DiskDriveHelper()
        {
            treeView_HDD.Items.Clear();

            // Manually added System.Management to References and using System.Management (maybe a bug in .Net)
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject managementObject in managementObjectSearcher.Get())
            {
                TreeViewItem hddItemIdParent = new TreeViewItem
                {
                    Header = managementObject["DeviceID"].ToString()
                };

                TreeViewItem childItemModel = new TreeViewItem
                {
                    Header = "Model: " + managementObject["Model"].ToString()
                };
                hddItemIdParent.Items.Add(childItemModel);

                TreeViewItem childItemInterfaceType = new TreeViewItem
                {
                    Header = "Interface: " + managementObject["InterfaceType"].ToString()
                };
                hddItemIdParent.Items.Add(childItemInterfaceType);

                TreeViewItem childItemSerialNumber = new TreeViewItem
                {
                    Header = "Serial#: " + managementObject["SerialNumber"].ToString()
                };
                hddItemIdParent.Items.Add(childItemSerialNumber);

                treeView_HDD.Items.Add(hddItemIdParent);
            }
        }
    }
}
