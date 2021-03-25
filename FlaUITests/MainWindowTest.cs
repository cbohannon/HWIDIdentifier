using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.UIA2;
using System.Diagnostics.CodeAnalysis;
using FlaUI.Core.Conditions;
using FlaUI.Core.AutomationElements;
using HWIDIdentifier;

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
            // mainWindow.FindFirstDescendant(conditionFactory.ByName("Exit")).AsButton().Click();
            mainWindow.FindFirstDescendant(conditionFactory.ByName("HWID")).AsButton().Click();

            Assert.IsNotNull(mainWindow.FindFirstDescendant(conditionFactory.ByAutomationId("Label_HWID")).AsLabel().Name);
        }
        [TestCleanup]
        public void TestCleanup()
        {
            application.Dispose();
            automation.Dispose();
            mainWindow.Close();
            conditionFactory = null;
        }
    }
}