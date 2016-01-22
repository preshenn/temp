using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tlf.DataObjects;

namespace tlf.DataAccess
{
    /// <summary>
    /// Assumption:
    /// The user is uniquely identified by their user name. User name is case sensitive!
    /// Hence two users may not have the same user name in the given problem.
    /// There are no numeric identifiers for a user in the user text file.
    /// Inherits from the fileReader class. All file reader classes should implement the 
    /// standard members/functions.
    /// </summary>
    public class UserTxtFileReader : FileReader
    {
        private string _fileName;
        private const string _userFollowerSeperator = "follows";
        private const string _followersSeperator = ",";

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
        /// Reads all lines of the file and then processes the lines concurrently.
        /// Based on the current problem definition, this can be done because there is no
        /// particular order that users may be processed.
        /// </summary>
        /// <param name="fileName">The file name with the path if not in the same directory as the exe.</param>
        /// <param name="users">Outputs the users read from the file along with followers assigned to the respective user.</param>
        /// <returns>True if success, false otherwise.</returns>
        public bool Read(string fileName, out Dictionary<string, UserComposite> users)
        {
            bool success = true;
            users = null;

            try
            {
                //Concurrent dictionary is required. thread safe. More than one thread may use this at the same time.
                ConcurrentDictionary<string, UserComposite> cd = new ConcurrentDictionary<string, UserComposite>();                

                string[] lines = File.ReadAllLines(fileName);

                //Begin processing lines concurrently.
                Parallel.ForEach(lines, (currentLine) =>
                {
                    ProcessLine(currentLine, cd);
                });

                users = cd.ToDictionary(x => x.Key, x => x.Value);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine("Error reading from {0}. Message = {1}", FileName, e.Message);
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Function for processing a line from the users text file.
        /// </summary>
        /// <param name="currentLine">A line from the user text file.</param>
        /// <param name="Concur">Thread safe collection. Not const and passed by ref by default. It is modified in the function.</param>
        private void ProcessLine(string currentLine, ConcurrentDictionary<string, UserComposite> Concur)
        {
            string[] seperator = new string[] { _userFollowerSeperator, _followersSeperator };
            string[] split = currentLine.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            if (split.Count() == 0)
                return;

            string userStr = split[0].Trim();

            if (string.IsNullOrEmpty(userStr))
                return;

            UserComposite user = null;

            //Check if the concurrent collection already contains this user.
            if (Concur.ContainsKey(userStr))
            {
                user = Concur[userStr];                                
            }
            else
            {
                user = new UserComposite();
                user.UserName = userStr;
            }

            //Now allocate the followers respectively. Also check for existence.
            //We want only one object instance per user. The followers allocated to the users
            //will also be the same user object. There will not be a separate user and follower
            //object for the same username.
            for (int i = 1; i < split.Count(); i++)
            {
                string folStr = split[i].Trim();
                UserComposite follower = null;
                if (Concur.ContainsKey(folStr))
                {
                    follower = Concur[folStr];
                }
                else
                {
                    follower = new UserComposite();
                    follower.UserName = folStr;
                    Concur.TryAdd(folStr, follower);
                }

                user.AddFollower(follower);
            }

            Concur.TryAdd(userStr, user);
        }
    }
}
