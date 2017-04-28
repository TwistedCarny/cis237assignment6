using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cis237Assignment6.Controllers;
using System.Web.Mvc;

namespace cis237Assignment6.Tests.Controllers
{
    [TestClass]
    public class BeveragesControllerTest
    {
        [TestMethod]
        public void Create()
        {
            BeveragesController controller = new BeveragesController();

            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ItemExists()
        {
            BeveragesController controller = new BeveragesController();

            ViewResult result = controller.ItemExists() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
