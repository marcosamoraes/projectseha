using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ProjectSeha.Entity;
using System.Data;

namespace ProjectSeha.Models
{
    public class CursoModel : ModelBase
    {
        public List<Curso> Read()
        {
            List<Curso> lista = new List<Curso>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM ViewCursos";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Curso e = new Curso();

                e.CursoId = (int) reader["CursoId"];
                e.Titulo = (string) reader["Titulo"];
                e.Turno = (string) reader["Turno"];

                lista.Add(e);
            }
            return lista;
        }

        public void Create(Curso e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"EXEC ArmazenaCurso @titulo, @turno";

            cmd.Parameters.AddWithValue("@titulo", e.Titulo);
            cmd.Parameters.AddWithValue("@turno", e.Turno);

            cmd.ExecuteNonQuery();
        }

        public void Update(Curso e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"EXEC AlteraCurso @id, @titulo, @turno";

            cmd.Parameters.AddWithValue("@id", e.CursoId);
            cmd.Parameters.AddWithValue("@titulo", e.Titulo);
            cmd.Parameters.AddWithValue("@turno", e.Turno);

            cmd.ExecuteNonQuery();
        }


        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"ApagaCurso @id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}