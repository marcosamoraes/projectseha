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
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Atribuicao e = new Atribuicao();
                e.CodProfessor = (int)reader["CodProfessor"];
                e.CodDisciplina = (int)reader["CodDisciplina"];
                
                lista.Add(e);
            }
            return lista;
        }

        public void Create(Atribuicao e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"ArmazenaAtribuicao";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nome", e.CodProfessor);
            cmd.Parameters.AddWithValue("@email", e.CodDisciplina);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int ProfessorId, int DisciplinaId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"ApagaProfessor";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProfessorId", ProfessorId);
            cmd.Parameters.AddWithValue("@DisciplinaId", DisciplinaId);
            cmd.ExecuteNonQuery();
        }
    }
}