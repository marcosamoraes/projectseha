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
            string connectionString = @"Data Source=.\sqlexpress;
                                        Initial Catalog=BDSeha; 
                                        Integrated Security=true"; //.\sqlexpress

            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}