using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tlf.DataObjects.Tests
{
    [TestClass()]
    public class TweetObjTests
    {
        [TestMethod()]
        public void TweetObjTestIndex()
        {
            for (int i = 0; i < 100; i++)
            {
                string tweet = string.Format("Hello World {0}", i);
                TweetObj tweetobj = new TweetObj(tweet);
                Assert.AreEqual(i, tweetobj.TweetIndex);
            }
        }

        [TestMethod()]
        public void TweetObjTestTweet()
        {
            string tweet = "hello world";
            TweetObj tweetobj = new TweetObj(tweet);
            Assert.AreEqual(tweet, tweetobj.Tweet);
        }
        
    }
}