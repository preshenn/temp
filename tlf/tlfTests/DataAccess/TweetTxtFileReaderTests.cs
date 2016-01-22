using Microsoft.VisualStudio.TestTools.UnitTesting;
using tlf.DataObjects;
using System;
using System.Collections.Generic;

namespace tlf.DataAccess.Tests
{
    [TestClass()]
    public class TweetTxtFileReaderTests
    {
        [TestMethod()]
        public void ReadTest0()
        {
            TweetTxtFileReader reader = new TweetTxtFileReader();
            Dictionary<string, List<TweetObj>> tweets = null;
            bool test = reader.Read("12334", out tweets);
            Assert.IsFalse(test);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadTest1()
        {
            TweetTxtFileReader reader = new TweetTxtFileReader();
            Dictionary<string, List<TweetObj>> tweets = null;
            bool test = reader.Read(null, out tweets);            
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadTest2()
        {
            TweetTxtFileReader reader = new TweetTxtFileReader();
            Dictionary<string, List<TweetObj>> tweets = null;
            bool test = reader.Read(string.Empty, out tweets);            
        }
    }
}