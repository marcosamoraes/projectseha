using ProjectSeha.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectSeha.Models
{
    public class PessoaModel : ModelBase
    {
        public Pessoa Login(string email, string senha)
        {
            Pessoa e = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Usuario WHERE Email = @email and Senha = @senha";

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                e = new Pessoa();
                e.PessoaId = (int)reader["IdUsuario"];
                e.Nome = (string)reader["Nome"];
                e.Email = (string)reader["Email"];
            }
            return e; //se n entrar no if vai retornar usuario null
        }
    }
}