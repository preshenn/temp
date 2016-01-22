namespace tlf.DataObjects
{
    /// <summary>
    /// Model class for users.
    /// Just contains the data member username. In future one may add UserID, 
    /// Email address, Surname etc.
    /// </summary>
    public class User
    {
        private string _userName;

        //Commented out for now because a constructor isnt required at the moment.
        //public User(string userName)
        //{
        //    _userName = userName;            
        //}

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }        
    }
}
