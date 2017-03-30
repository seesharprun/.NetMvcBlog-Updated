using System;
using System.Data.SqlClient;

namespace Blog40.Repository
{
    public class BaseRepository
    {
        protected SqlConnection Connection { get; private set; }

        public BaseRepository(string connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException();
            }
            Connection = new SqlConnection(connectionString);
        }
    }
}