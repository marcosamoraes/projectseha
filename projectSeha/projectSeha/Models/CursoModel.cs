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

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Curso e = new Curso();

                    e.CursoId = (int)reader["CursoId"];
                    e.Titulo = (string)reader["Titulo"];
                    e.Turno = (string)reader["Turno"];

                    lista.Add(e);
                }
                return lista;
            }
        }

        public Curso Read(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM ViewCursos WHERE CursoId = @id";

            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Curso curso = new Curso();

                curso.CursoId = (int)reader["CursoId"];
                curso.Titulo = (string)reader["Titulo"];
                curso.Turno = (string)reader["Turno"];

                return curso;
            }
            return null;
        }

        public void Create(Curso e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"ArmazenaCurso";

            cmd.Parameters.AddWithValue("@titulo", e.Titulo);
            cmd.Parameters.AddWithValue("@turno", e.Turno);

            cmd.ExecuteNonQuery();
        }

        public void Update(Curso e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"AlteraCurso";

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
            cmd.CommandText = @"ApagaCurso";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public int GetUltimoCursoArmazenado()
        {
            int idCurso = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT TOP 1 (CursoId) AS ultimoId FROM tblCurso ORDER BY CursoId DESC";

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    idCurso = (int)reader["ultimoId"];
                }
                return idCurso;
            }
        }

        public bool VerificaCurso(string Titulo, string Turno)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT UPPER(Titulo)Titulo, UPPER(Turno) Turno FROM tblCurso WHERE Titulo = @Titulo and Turno = @Turno";

            cmd.Parameters.AddWithValue("@Titulo", Titulo);
            cmd.Parameters.AddWithValue("@Turno", Turno);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                return (reader.Read()) ? true : false; //operador ternário em c#
            }
        }

        public bool VerificaCurso(string Titulo, string Turno, int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT UPPER(Titulo)Titulo, UPPER(Turno) Turno FROM tblCurso WHERE Titulo = @Titulo and Turno = @Turno And CursoId <> @id";

            cmd.Parameters.AddWithValue("@Titulo", Titulo);
            cmd.Parameters.AddWithValue("@Turno", Turno);
            cmd.Parameters.AddWithValue("@id", id);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                return (reader.Read()) ? true : false; //operador ternário em c#
            }
        }
    }
}