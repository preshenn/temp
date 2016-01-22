namespace tlf.DataAccess
{
    /// <summary>
    /// Parent/Base class for all file readers.
    /// This should contain the standard members that one expects all child file reader classes
    /// to implement. One may add an xmlfileReader and inherit from this class. Suppose in future
    /// there is a requirement for accessing the data from an xml file or a binary file. This makes
    /// the program easily extendible by provinding a common interface.
    /// I also wanted to include a DataAccess base class for this FileReader class. Then the class that reads from
    /// sql databases i.e. DatabaseReader maybe inherit from the base class DataAccess. Well that is the idea anyway.
    /// </summary>
    public abstract class FileReader
    {
        public abstract string FileName { get; set; }        

    }
}
