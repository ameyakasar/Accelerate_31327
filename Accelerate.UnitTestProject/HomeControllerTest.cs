using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Accelerate_31327.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using Accelerate_31327.Models;

namespace Accelerate.UnitTestProject
{
    [TestClass]
    public class HomeControllerTest
    {
      [TestMethod]
        public void IndexActionReturnsIndexView()
        {
            HomeController controller = new HomeController();
            var result = controller.Index() as ViewResult;
            // Assert
            var model = ((ViewResult)result).Model as List<EmployeeDetailModel>;
            // Test whether type is List<EmployeeDetailModel>
            Assert.IsNotNull(model, "model is not of type List<EmployeeDetailModel>");
            Assert.IsTrue(model.Count > 0);
        }

      [TestMethod]
      public void AboutActionReturnsAboutView()
      {
          string expected = "About";
          HomeController controller = new HomeController();
          var result = controller.About() as ViewResult;

          Assert.AreEqual(expected, result.ViewName);
      }
    }
}
