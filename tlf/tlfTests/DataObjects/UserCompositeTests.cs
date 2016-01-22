using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace tlf.DataObjects.Tests
{
    [TestClass()]
    public class UserCompositeTests
    {
        [TestMethod()]
        public void AddFollowerTest()
        {
            UserComposite uc = new UserComposite();
            bool test = uc.AddFollower(null);

            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void AddTweetTest()
        {
            UserComposite uc = new UserComposite();
            bool test = uc.AddTweet(null);

            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void AddTweetListTest()
        {
            UserComposite uc = new UserComposite();
            bool test = uc.AddTweets(null);

            Assert.IsFalse(test);
        }

        [TestMethod()]
        public void GetFeedTest()
        {
            UserComposite uc = new UserComposite();
            List<TweetObj> listStr = new List<TweetObj>();
            List<string> strl = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                string tweet = string.Format("Hello World {0}", i);
                strl.Add(tweet);
                TweetObj twto = new TweetObj(tweet);
                listStr.Add(twto);
            }

            uc.AddTweets(listStr);

            string[] tweets = uc.GetFeed();
            for (int i = 0; i < 100; i++)
            {                
                Assert.AreEqual(tweets[i].Remove(0,3), strl[i]);
            }
        }
    }
}