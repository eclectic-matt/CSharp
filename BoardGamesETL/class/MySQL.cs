using MySql.Data;
using MySql.Data.MySqlClient;

//SOURCE: https://stackoverflow.com/questions/21618015/how-to-connect-to-mysql-database
//REQUIRES dotnet add package MySql.Data --version 9.2.0
namespace Data
{
    public class DBConnection
    {
        private DBConnection()
        {
        }

        public string? Server { get; set; }
        public string? DatabaseName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public MySqlConnection? Connection { get; set;}

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
           return _instance;
        }
    
        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(DatabaseName))
                    return false;
                string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, UserName, Password);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            }
    
            return true;
        }
    
        public void Close()
        {
            Connection.Close();
        }        
    }
}
