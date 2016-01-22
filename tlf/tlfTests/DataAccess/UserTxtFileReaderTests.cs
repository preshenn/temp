using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using tlf.DataObjects;

namespace tlf.DataAccess.Tests
{
    [TestClass()]
    public class UserTxtFileReaderTests
    {
        [TestMethod()]
        public void ReadUserTest0()
        {
            UserTxtFileReader reader = new UserTxtFileReader();
            Dictionary<string, UserComposite> users = null;
            bool test = reader.Read("12334", out users);
            Assert.IsFalse(test);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadUserTest1()
        {
            UserTxtFileReader reader = new UserTxtFileReader();
            Dictionary<string, UserComposite> users = null;
            bool test = reader.Read(null, out users);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadUserTest2()
        {
            UserTxtFileReader reader = new UserTxtFileReader();
            Dictionary<string, UserComposite> users = null;
            bool test = reader.Read(string.Empty, out users);            
        }
    }
}