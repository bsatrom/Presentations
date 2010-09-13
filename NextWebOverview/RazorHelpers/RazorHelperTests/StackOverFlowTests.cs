using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RazorHelperTests
{
    [TestClass]
    public class StackOverFlowTests
    {
        [TestMethod]
        public void CanObtainUserIdFromName()
        {
            Assert.AreEqual("380135", RazorHelpers.StackOverflow.Flair.GetUserId("satrom"));
        }
    }
}
