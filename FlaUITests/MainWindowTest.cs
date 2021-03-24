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
        [TestMethod]
        public void ExitButtonClick()
        {
            // string relPath = Path.GetRelativePath("C:\\Source\\GitHub", "C:\\Source\\GitHub\\HWIDIdentifier\\HWIDIdentifier\\bin\\Debug\\HWIDIdentifier.exe");
            // Console.WriteLine(relPath);

            var application = Application.Launch(@"C:\Source\GitHub\HWIDIdentifier\HWIDIdentifier\bin\Debug\HWIDIdentifier.exe", "/quickStart");
            var automation = new UIA3Automation();
            var mainWindow = application.GetMainWindow(automation);
            ConditionFactory conditionFactory = new ConditionFactory(new UIA3PropertyLibrary());

            mainWindow.FindFirstDescendant(conditionFactory.ByName("Exit")).AsButton().Click();

            Assert.IsNotNull(mainWindow);
        }
    }
}
