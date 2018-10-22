using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GEPED.Web.Controllers;

namespace GEPED.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index(null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        
    }
}
