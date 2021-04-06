using System;
using System.Windows;
using System.Windows.Controls;
using System.Management;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FlaUITests")]
namespace HWIDIdentifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MainWindow()
        {
            log.Info("Application started at: " + DateTime.Now);
            InitializeComponent();
        }
        private void Product_Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Windows Product Key: " + ReadHelper.GetWindowsProductKey(), "Product Keys", MessageBoxButton.OK);
        }
        private void AppExit_Click(object sender, RoutedEventArgs e)
        {
            ExitApp();
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
            ExitApp();
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
        private void ExitApp()
        {
            log.Info("Application exited at: " + DateTime.Now);

            Environment.Exit(0);
        }
    }
}
