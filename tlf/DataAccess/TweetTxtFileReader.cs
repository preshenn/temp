using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using tlf.DataObjects;

namespace tlf.DataAccess
{
    /// <summary>
    /// Class for reading the tweet file.
    /// There are no numeric identifiers for a tweet in the tweet text file. The order
    /// is taken as the same order within the file. Hence the file must be read sequentially.
    /// The problem definition states the order is important.
    /// Inherits from the fileReader class. All file reader classes should implement the 
    /// standard members/functions.
    /// </summary>
    public class TweetTxtFileReader : FileReader
    {
        private string _fileName;
        private const char userTweetSeperator = '>';

        /// <summary>
        /// Gets or sets the file name being read
        /// </summary>
        public override string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        /// <summary>
        /// Reads the tweet file sequentially, line by line.
        /// These lines must be read in the order which they appear in the file.
        /// </summary>
        /// <param name="fileName">The file name with the path if not in the same directory as the exe.</param>
        /// <param name="tweets">Outputs the list of tweets per user.</param>
        /// <returns>True if success, false otherwise.</returns>
        public bool Read(string fileName, out Dictionary<string, List<TweetObj>> tweets)
        {
            bool success = true;
            tweets = null;

            try
            {
                tweets = new Dictionary<string, List<TweetObj>>();
                
                using (StreamReader streamReader = File.OpenText(fileName))
                {
                    string line = String.Empty;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        ProcessLine(line, tweets);
                    }
                }
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine("Error reading from {0}. Message = {1}", FileName, e.Message);
                success = false;
            }            

            return success;
        }

        /// <summary>
        /// Function for processing a line from the tweet text file.
        /// </summary>
        /// <param name="currentLine">A line from the tweet text file. Assumption is one line per tweet per user.</param>
        /// <param name="tweets">The list of tweets per user. Not const and passed by ref by default. It is modified in the function.</param>
        private void ProcessLine(string currentLine, Dictionary<string, List<TweetObj>> tweets)
        {
            string[] split = currentLine.Split(userTweetSeperator);

            if (split.Count() <= 1)
                return;

            string userStr = split[0].Trim();
            string tweetStr = split[1].Trim();

            //check for validity.
            if (string.IsNullOrEmpty(userStr) || string.IsNullOrEmpty(tweetStr))
                return;

            //We can expect a new tweet so create this object.
            TweetObj myTweet = new TweetObj(tweetStr);

            //check for existence.
            List<TweetObj> userTweets = null;
            if (!tweets.TryGetValue(userStr, out userTweets))
            {
                userTweets = new List<TweetObj>();
                tweets.Add(userStr, userTweets);
            }

            //finally add to the collection.
            userTweets.Add(myTweet);
        }

    }
}
