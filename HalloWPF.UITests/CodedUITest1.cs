using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace HalloWPF.UITests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        public CodedUITest1()
        {
        }

        [TestMethod]
        [TestCategory("UI Test")]
        public void CodedUITestMethod1()
        {
            // Arrange
            var path = @"C:\Kurse\Testing\2017_08_22_SoftwareTests_Burghausen\HalloWPF\bin\Debug\HalloWPF.exe";
            var app = ApplicationUnderTest.Launch(path);

            var mainWindow = new WpfWindow(app);
            mainWindow.SearchProperties.Add(WpfWindow.PropertyNames.Name, "halloWPFWindow");

            var inputTextBox = new WpfEdit(mainWindow);
            inputTextBox.SearchProperties.Add(WpfEdit.PropertyNames.AutomationId, "inputTextBox");

            var outputTextBlock = new WpfText(mainWindow);
            outputTextBlock.SearchProperties.Add(WpfText.PropertyNames.AutomationId, "outputTextBlock");

            var inputButton = new WpfButton(mainWindow);
            inputButton.SearchProperties.Add(WpfButton.PropertyNames.AutomationId, "inputButton");

            var outputCheckBox = new WpfCheckBox(mainWindow);
            outputCheckBox.SearchProperties.Add(WpfCheckBox.PropertyNames.AutomationId, "outputCheckBox");

            // Act
            Keyboard.SendKeysDelay = 500;
            Keyboard.SendKeys(inputTextBox, "Hallo WPF from coded UI Test.");
            Mouse.Click(inputButton);

            // Assert
            Assert.AreEqual("Hallo WPF from coded UI Test.", outputTextBlock.DisplayText);
            Assert.IsTrue(outputCheckBox.Checked);
        }

        [TestMethod]
        [TestCategory("UI Test")]
        public void CodedUITestMethod2()
        {

            this.UIMap.RecordedMethod1();
            this.UIMap.AssertMethod1();

        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
