namespace tlf.DataObjects
{
    /// <summary>
    /// Model class for storing a tweet.
    /// Index keeps track of tweet order so the caller doesn't have to manually assign indexes.
    /// If the tweet file had index's or unique order id's for each tweet then this wouldn't
    /// be necessary. This class is suitable for the given problem definition.
    /// </summary>
    public class TweetObj
    {
        private static int _index = 0;

        string _tweet;
        private int _tweetIndex;

        public TweetObj(string tweet)
        {
            _tweetIndex = _index;
            _index++;
            _tweet = tweet;
        }        
        
        public string Tweet
        {
            get { return _tweet; }
            set { _tweet = value; }
        }

        public int TweetIndex
        {
            get { return _tweetIndex; }
        }
    }
}
