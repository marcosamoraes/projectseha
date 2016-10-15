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
               
                e.PessoaId = (int)reader["PessoaId"];
                e.Nome = (string)reader["Nome"];
                e.Email = (string)reader["Email"];
                e.Senha = (string)reader["Senha"];
                e.Permissao_admin = (bool)reader["Permissao_admin"];
                e.NomeGuerra = (string)reader["NomeGuerra"];
                e.HorasAula = (int)reader["HorasAula"];
                e.ProfessorExiste = (bool)reader["ProfessorExiste"];
                e.ProfessorAtivo = (bool)reader["ProfessorAtivo"];
                e.Observacoes = (string)reader["Observacoes"];

                lista.Add(e);
            }
            return lista;
        }

        public void Create(Professor e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"EXEC ArmazenaProfessor @nome, @email, @senha, @nomeGuerra, @professorExiste";

            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@email", e.Email);
            cmd.Parameters.AddWithValue("@senha", e.Senha);
            cmd.Parameters.AddWithValue("@nomeGuerra", e.NomeGuerra);
            cmd.Parameters.AddWithValue("@professorExiste", e.ProfessorExiste);

            cmd.ExecuteNonQuery();
        }

        public void Update(Professor e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"EXEC AlteraProfessor @id, @nome, @email, @nomeGuerra, @professorExiste, @professorAtivo";

            cmd.Parameters.AddWithValue("@id", e.PessoaId);
            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@email", e.Email);
            cmd.Parameters.AddWithValue("@nomeGuerra", e.NomeGuerra);
            cmd.Parameters.AddWithValue("@professorExiste", e.ProfessorExiste);
            cmd.Parameters.AddWithValue("@professorAtivo", e.ProfessorAtivo);

            cmd.ExecuteNonQuery();
        }


        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"ApagaProfessor @id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

    }
}