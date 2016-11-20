using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectSeha.Entity;
using System.Data.SqlClient;
using System.Data;

namespace ProjectSeha.Models
{
    public class AssignmentModel : ModelBase
    {
        public List<Atribuicao> Read(int id)
        {
            List<Atribuicao> lista = new List<Atribuicao>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM ViewAtribuicoes Where CodProfessor = @id";

            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Atribuicao e = new Atribuicao();
                    e.CodProfessor = (int)reader["CodProfessor"];
                    e.CodDisciplina = (int)reader["CodDisciplina"];
                    e.CodCurso = (int)reader["CodCurso"];

                    lista.Add(e);
                }
                return lista;
            }
               
        }

        public List<Atribuicao> ReadDisabled(int id)
        {
            List<Atribuicao> lista = new List<Atribuicao>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM ViewAtribuicoes Where CodProfessor <> @id";

            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Atribuicao e = new Atribuicao();
                    e.CodProfessor = (int)reader["CodProfessor"];
                    e.CodDisciplina = (int)reader["CodDisciplina"];
                    e.CodCurso = (int)reader["CodCurso"];

                    lista.Add(e);
                }
                return lista;
            }
                
        }

        public void Create(Atribuicao e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"ArmazenaAtribuicao";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codProfessor", e.CodProfessor);
            cmd.Parameters.AddWithValue("@codDisciplina", e.CodDisciplina);
            cmd.Parameters.AddWithValue("@codCurso", e.CodCurso);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int ProfessorId, int CursoId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"ApagaAtribuicao";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProfessorId", ProfessorId);
            cmd.Parameters.AddWithValue("@CursoId", CursoId);
            cmd.ExecuteNonQuery();
        }

        public List<string> ReadTurno (int ProfessorId)
        {
            List<string> lista = new List<string>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"ViewTurnos";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProfessorId", ProfessorId);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!lista.Contains((string)reader["Turno"]))
                    {
                        lista.Add((string)reader["Turno"]);
                    }
                }
                return lista;
            }
        }
    }
}