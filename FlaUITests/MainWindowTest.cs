using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUI.Core;
using FlaUI.UIA3;
using System.Diagnostics.CodeAnalysis;
using FlaUI.Core.Conditions;
using FlaUI.Core.AutomationElements;
using System.Linq;
using System.Threading;

namespace FlaUITests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class MainWindowTest
    {
        Application application;
        UIA3Automation automation;
        Window mainWindow;
        ConditionFactory conditionFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            // string relPath = Path.GetRelativePath("C:\\Source\\GitHub", "C:\\Source\\GitHub\\HWIDIdentifier\\HWIDIdentifier\\bin\\Debug\\HWIDIdentifier.exe");
            application = Application.Launch(@"C:\Source\GitHub\HWIDIdentifier\HWIDIdentifier\bin\Debug\HWIDIdentifier.exe", "/quickStart");
            automation = new UIA3Automation();
            mainWindow = application.GetMainWindow(automation);
            conditionFactory = new ConditionFactory(new UIA3PropertyLibrary());
        }
        [TestMethod]
        public void HWIDButtonClick()
        {            
            mainWindow.FindFirstDescendant(conditionFactory.ByName("HWID")).AsButton().Click();

            Assert.IsNotNull(mainWindow.FindFirstDescendant(conditionFactory.ByAutomationId("Label_HWID")).AsLabel().Name);
        }
        [TestMethod]
        public void PCGuidButtonClick()
        {
            mainWindow.FindFirstDescendant(conditionFactory.ByName("PCGuid")).AsButton().Click();

            Assert.IsNotNull(mainWindow.FindFirstDescendant(conditionFactory.ByAutomationId("Label_PCGuid")).AsLabel().Name);
        }
        [TestMethod]
        public void PCNameButtonClick()
        {
            mainWindow.FindFirstDescendant(conditionFactory.ByName("PCName")).AsButton().Click();

            Assert.IsNotNull(mainWindow.FindFirstDescendant(conditionFactory.ByAutomationId("Label_PCName")).AsLabel().Name);
        }
        [TestMethod]
        public void ProductIdButtonClick()
        {
            mainWindow.FindFirstDescendant(conditionFactory.ByName("ProductID")).AsButton().Click();

            Assert.IsNotNull(mainWindow.FindFirstDescendant(conditionFactory.ByAutomationId("label_ProductID")).AsLabel().Name);
        }
        [TestMethod]
        public void HardDiskDriveButtonClick()
        {
            mainWindow.FindFirstDescendant(conditionFactory.ByName("HDD(S)")).AsButton().Click();

            Assert.IsNotNull(mainWindow.FindFirstDescendant(conditionFactory.ByAutomationId("treeView_HDD")).AsTree().Name);
        }
        [TestMethod]
        public void SpoofHWIDButtonClick()
        {
            mainWindow.FindFirstDescendant(conditionFactory.ByName("Spoof HWID")).AsButton().Click();

            Assert.IsNotNull(mainWindow.FindFirstDescendant(conditionFactory.ByAutomationId("Label_Spoof_HWID")).AsLabel().Name);
        }
        [TestMethod]
        public void SpoofGUIDButtonClick()
        {
            mainWindow.FindFirstDescendant(conditionFactory.ByName("Spoof GUID")).AsButton().Click();

            Assert.IsNotNull(mainWindow.FindFirstDescendant(conditionFactory.ByAutomationId("Label_Spoof_GUID")).AsLabel().Name);
        }
        [TestMethod]
        public void SpoofNameButtonClick()
        {
            mainWindow.FindFirstDescendant(conditionFactory.ByName("Spoof Name")).AsButton().Click();

            Assert.IsNotNull(mainWindow.FindFirstDescendant(conditionFactory.ByAutomationId("Label_Spoof_PCName")).AsLabel().Name);
        }
        [TestMethod]
        public void SpoofPIDButtonClick()
        {
            mainWindow.FindFirstDescendant(conditionFactory.ByName("Spoof PID")).AsButton().Click();

            Assert.IsNotNull(mainWindow.FindFirstDescendant(conditionFactory.ByAutomationId("Label_Spoof_ProductID")).AsLabel().Name);
        }
        [TestMethod]
        public void KeysClick()
        {
            MenuItem menuItem = mainWindow.FindFirstDescendant(conditionFactory.ByName("Tools")).AsMenuItem();
            menuItem.Items["Keys"].Invoke();

            Window messageBox = mainWindow.ModalWindows.FirstOrDefault().AsWindow();
            var yesButton = messageBox.FindFirstChild(conditionFactory.ByName("OK")).AsButton();
            Thread.Sleep(1000);
            yesButton.Click(); // Invoke seems to not work

            Assert.IsNotNull(mainWindow);
        }
        [TestMethod]
        public void ExitButtonClick()
        {
            mainWindow.FindFirstDescendant(conditionFactory.ByName("Exit")).AsButton().Click();

            Assert.IsNotNull(mainWindow);
        }
        [TestMethod]
        public void AppExitClick()
        {
            MenuItem menuItem = mainWindow.FindFirstDescendant(conditionFactory.ByName("File")).AsMenuItem();
            menuItem.Items["Exit"].Invoke();

            Assert.IsNotNull(mainWindow);
        }
        [TestCleanup]
        public void TestCleanup()
        {
            application.Dispose();
            automation.Dispose();
            if (mainWindow.IsAvailable)
            {
                mainWindow.Close();
            }
            conditionFactory = null;
        }
    }
}