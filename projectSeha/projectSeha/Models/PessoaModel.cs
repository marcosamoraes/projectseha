using ProjectSeha.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace ProjectSeha.Models
{
    public class PessoaModel : ModelBase
    {
        public Pessoa Login(string email, string senha)
        {
            Pessoa e = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"ValidaLogin";

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                e = new Pessoa();
                e.PessoaId = (int)reader["PessoaId"];
                e.Nome = (string)reader["Nome"];
            }
            return e; //se n entrar no if vai retornar usuario null
        }

        //TODO:Verificar se é possível carregar os dados da "Pessoa e" que está logada no sistema no momento
        public void UpdateSenha(Pessoa e, string senhaAntiga, string senhaNova, string senhaNova2)
        {
            if (e.Senha == senhaAntiga && senhaNova == senhaNova2)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = @"AlteraSenha";

                cmd.Parameters.AddWithValue("@id", e.PessoaId);
                cmd.Parameters.AddWithValue("@senhaNova", senhaNova);

                cmd.ExecuteNonQuery();
            }
        }

    }
}