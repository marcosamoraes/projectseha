using ProjectSeha.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectSeha.Models
{
    public class ProfessorModel : ModelBase
    {
        public List<Professor> Read()
        {
            List<Professor> lista = new List<Professor>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM ViewProfessores";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Professor e = new Professor();
               
                e.CodPessoa = (int)reader["IdPessoa"];
                e.Nome = (string)reader["Nome"];
                e.Email = (string)reader["Email"];
                e.Senha = (string)reader["Senha"];
                e.Permissao_admin = (bool)reader["Permissao_admin"];
                e.NomeGuerra = (string)reader["NomeGuerra"];
                e.HorasAula = (int)reader["HorasAula"];
                e.ExisteProfessor = (bool)reader["ExisteProfessor"];
                e.InativaProfessor = (bool)reader["InativaProfessor"];
                e.Observacoes = (string)reader["Observacoes"];

                lista.Add(e);
            }
            return lista;
        }

        public void Create(Usuario e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Usuario VALUES (@nome, @email, @senha)";

            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@email", e.Email);
            cmd.Parameters.AddWithValue("@senha", e.Senha);

            cmd.ExecuteNonQuery();
        }

        public void Update(Usuario e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE Usuario SET Nome = @nome, Email = @email WHERE IdUsuario = @idUsuario";

            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@email", e.Email);
            cmd.Parameters.AddWithValue("@idUsuario", e.IdUsuario);

            cmd.ExecuteNonQuery();
        }


        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM Usuario WHERE IdUsuario = @id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

    }
}