using System.Collections.Generic;
using System.Linq;

namespace tlf.DataObjects
{
    /// <summary>
    /// Class for holding extended user information such has followers or tweets etc.
    /// Inherits from the user class. The user class represents a data model of a user.
    /// The user class maybe used for other methods that do not require extended user
    /// information, but only needs the standard user info.
    /// </summary>
    public class UserComposite : User
    {        
        private Dictionary<string, UserComposite> _following;

        private List<TweetObj> _tweets;

        /// <summary>
        /// Commented out the base constructor initialisation because it isnt
        /// required for now.
        /// </summary>
        public UserComposite()// : base(userName)
        {
            _following = new Dictionary<string, UserComposite>();
            _tweets = new List<TweetObj>();
        }

        /// <summary>
        /// Adds a follower to the collection. Checks if the object is null.
        /// </summary>
        /// <param name="follower">A valid follower object.</param>
        /// <returns>Returns false if the object is null. True if success.</returns>
        public bool AddFollower(UserComposite follower)
        {
            if (follower == null)
                return false;

            if(!_following.ContainsKey(follower.UserName))
                _following.Add(follower.UserName, follower);

            return true;
        }

        /// <summary>
        /// Appends a tweet to the tweet collection.
        /// </summary>
        /// <param name="tweet">A valid tweet object.</param>
        /// <returns>Returns false if the object is null. True if success.</returns>
        public bool AddTweet(TweetObj tweet)
        {
            if (tweet == null)
                return false;

            _tweets.Add(tweet);

            return true;
        }

        /// <summary>
        /// Appends a list of tweets to the tweet collection.
        /// </summary>
        /// <param name="tweet">A valid tweet list object.</param>
        /// <returns>Returns false if the object is null. True if success.</returns>
        public bool AddTweets(List<TweetObj> tweets)
        {
            if (tweets == null)
                return false;
           
            _tweets.AddRange(tweets);

            return true;
        }

        /// <summary>
        /// Gets the list of tweets.
        /// </summary>
        /// <returns></returns>
        public List<TweetObj> GetTweets
        {
            get
            {
                return _tweets;
            }
        }

        /// <summary>
        /// Gets the feeds for this user. Includes feeds of the user and their followers in order.
        /// Order is based on tweet index.
        /// </summary>
        /// <returns>A string array of feeds.</returns>
        public string[] GetFeed()
        {
            SortedList<int, string> feeds = new SortedList<int, string>();

            //Get feed's for the current user.
            foreach(TweetObj tweet in _tweets)
            {
                feeds.Add(tweet.TweetIndex, string.Format("@{0}: {1}", this.UserName, tweet.Tweet));
            }

            //Get feed's for the users followers.
            foreach(KeyValuePair<string,UserComposite> kvp in _following)
            {
                UserComposite uc = kvp.Value;
                List<TweetObj> followersTweets = uc.GetTweets;
                foreach (TweetObj tweet in followersTweets)
                {                    
                    feeds.Add(tweet.TweetIndex, string.Format("@{0}: {1}", uc.UserName, tweet.Tweet));
                }
            }

            return feeds.Select(x => x.Value).ToArray();
        }        
    }
}
