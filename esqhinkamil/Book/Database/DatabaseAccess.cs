using esqhinkamil.Book.Database.Models;

namespace esqhinkamil.Book.Database
{
    public class DatabaseAccess
    {
        public static List<EnBook> EnBooks { get; set; } = new List<EnBook>();
    }
    public class TablePkAutoincrement
    {
        private static int bookCounter;

        public static int BookCounter
        {
            get { return ++bookCounter; }
        }
    }
}
