using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tlf.DataObjects;
using tlf.DataAccess;

namespace tlf
{
    class Program
    {
        /// <summary>
        /// Main function of the console application.
        /// Two arguments required. One for each file.
        /// File names may include path if not in the same director as exe.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TwitterLikeFeed(args);
        }

        /// <summary>
        /// The function may be the controller. It manages info/data from
        /// the dataAccess to the output/view.
        /// </summary>
        /// <param name="args"></param>
        public static void TwitterLikeFeed(string[] args)
        {
            string userFileName = string.Empty;
            string twtFileName = string.Empty;

            if (string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]))
            {
                Console.WriteLine("One or more of the command line arguments are invalid.");
                return;
            }

            //The assessment/problem definition states that the given files will be named "user.txt" and "tweet.txt".
            //However it doesn't say in which order.
            if (args[0].ToLower().Contains("user"))
            {
                userFileName = args[0];
                twtFileName = args[1];
            }
            else
            {
                userFileName = args[1];
                twtFileName = args[0];
            }

            UserTxtFileReader utfr = new UserTxtFileReader();
            TweetTxtFileReader ttfr = new TweetTxtFileReader();

            try
            {
                //Process the user and tweet files concurrently since they
                //are independent of each other and can be read independently without
                //loss of data.
                Dictionary<string, UserComposite> users = null;
                Task task1 = Task.Factory.StartNew(() =>
                {
                    if (!utfr.Read(userFileName, out users))
                    {
                        Console.WriteLine("Error reading the user file.");
                        return;
                    }
                });

                Dictionary<string, List<TweetObj>> tweetsPerUser = null;
                Task task2 = Task.Factory.StartNew(() =>
                {
                    if (!ttfr.Read(twtFileName, out tweetsPerUser))
                    {
                        Console.WriteLine("Error reading the tweet file.");
                        return;
                    }
                });

                //Wait for both of the files to complete before continuing regular
                //program execution. The rest of the program requires that both files
                //be processed.
                task1.Wait();
                task2.Wait();

                //Assign tweets to users
                foreach (KeyValuePair<string, UserComposite> kvp in users)
                {
                    string userName = kvp.Key;
                    UserComposite userComp = kvp.Value;
                    List<TweetObj> userTweets = null;

                    tweetsPerUser.TryGetValue(userName, out userTweets);

                    userComp.AddTweets(userTweets);
                }

                Output(users);
            }
            catch (Exception ex) //for all other general exceptions
            {
                Console.WriteLine("General exception. Message = {0}", ex.Message);
            }
        }

        /// <summary>
        /// This may be a view object/class that is responsible for display.
        /// For now it is a simple function that satisfies the given problem
        /// definition.
        /// </summary>
        /// <param name="users"></param>
        private static void Output(Dictionary<string, UserComposite> users)
        {
            if (users == null)
                return;

            //output:            
            foreach (KeyValuePair<string, UserComposite> kvp in users)
            {
                string userName = kvp.Key;
                UserComposite userComp = kvp.Value;

                string[] feeds = userComp.GetFeed();

                Console.WriteLine(userName);
                foreach (string feed in feeds)
                    Console.WriteLine(string.Format("\t{0}", feed));
            }
            
            //Wait for the user to hit enter or any key.
            Console.ReadKey();
        }
    }
}
