using System;
using SampleWinFormsEVA;   //right click on References (in unit test project) --> Add Reference... --> Solution --> check assignments's solution
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SampleUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private Model model = new Model();

        //necessary assembly reference
        [TestMethod]
        public void ColorTest()    // Right click on method name --> Run tests
        {
            model.ColorGenerator();

            Assert.AreEqual(3, model.RGB.Length);
            foreach(var color in model.RGB)
            {
                Assert.IsTrue(color > -1, "Color code is less than expected.");
                Assert.IsTrue(color < 256, "Color code is greater than expected.");
            }
        }
    }
}
