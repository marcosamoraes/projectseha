using ProjectSeha.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

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

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
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
            }
            catch
            {
                return lista = null;
            }
            
        }

        public Professor Read(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM ViewProfessores WHERE PessoaId = @id";

            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Professor p = new Professor();

                    p.PessoaId = (int)reader["PessoaId"];
                    p.Nome = (string)reader["Nome"];
                    p.Email = (string)reader["Email"];
                    p.Senha = (string)reader["Senha"];
                    p.Permissao_admin = (bool)reader["Permissao_admin"];
                    p.NomeGuerra = (string)reader["NomeGuerra"];
                    p.HorasAula = (int)reader["HorasAula"];
                    p.ProfessorExiste = (bool)reader["ProfessorExiste"];
                    p.ProfessorAtivo = (bool)reader["ProfessorAtivo"];
                    p.Observacoes = (string)reader["Observacoes"];

                    return p;
                }
                return null;
            }
        }

        public bool Create(Professor e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"ArmazenaProfessor";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@email", e.Email);
            cmd.Parameters.AddWithValue("@nomeGuerra", e.NomeGuerra);
            cmd.Parameters.AddWithValue("@professorExiste", e.ProfessorExiste);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Professor e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"AlteraProfessor";

            cmd.Parameters.AddWithValue("@id", e.PessoaId);
            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@email", e.Email);
            cmd.Parameters.AddWithValue("@nomeGuerra", e.NomeGuerra);
            cmd.Parameters.AddWithValue("@professorExiste", e.ProfessorExiste);
            cmd.Parameters.AddWithValue("@professorAtivo", e.ProfessorAtivo);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"ApagaProfessor";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void UpdateHorasAula(int ProfessorId, int QtdAulas)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE tblProfessor SET HorasAula = @HorasAula WHERE CodPessoa = @id";

            cmd.Parameters.AddWithValue("@id", ProfessorId);
            cmd.Parameters.AddWithValue("@HorasAula", QtdAulas);

            cmd.ExecuteNonQuery();
        }

        public void UpdateObservation(int ProfessorId, string observacoes)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE tblProfessor SET Observacoes = @observacoes WHERE CodPessoa = @id";

            cmd.Parameters.AddWithValue("@id", ProfessorId);
            cmd.Parameters.AddWithValue("@observacoes", observacoes);

            cmd.ExecuteNonQuery();
        }
    }
}