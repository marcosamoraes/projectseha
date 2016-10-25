using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ProjectSeha.Models
{
    public abstract class ModelBase : IDisposable
    {
        protected SqlConnection connection = null;

        public ModelBase()
        {
            string connectionString = @"Data Source=localhost;
                                        Initial Catalog=BDSeha; 
                                        Integrated Security=true";

            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}