using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ProjectSeha.Entity;
using System.Data;
using System.Collections;

namespace ProjectSeha.Models
{
    public class DisciplinaModel : ModelBase 
    {

        public List<Disciplina> ordenarDisciplina(List<Disciplina> lista)
        {
            List<Disciplina> listaOrdenada = new List<Disciplina>();
            do
            {
                for (var i = 1; i <= 6; i++)
                {
                    listaOrdenada.Add(lista.Find(d => d.Semestre == i));
                    lista.Remove(lista.Find(d => d.Semestre == i));
                }
            } while (lista.Count > 0);
            listaOrdenada.RemoveAll(x => x == null);
            return listaOrdenada;
        }

        public List<Disciplina> Read(int CursoId)
        {
            List<Disciplina> lista = new List<Disciplina>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM ViewDisciplinas WHERE CodCurso = @CursoId";

            cmd.Parameters.AddWithValue("@CursoId", CursoId);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Disciplina e = new Disciplina();

                    e.DisciplinaId = (int)reader["DisciplinaId"];
                    e.CodCurso = (int)reader["CodCurso"];
                    e.Nome = (string)reader["Nome"];
                    e.QtdAulas = (int)reader["QtdAulas"];
                    e.Semestre = (int)reader["Semestre"];
                    e.Sigla = (string)reader["Sigla"];

                    lista.Add(e);
                }

                lista = ordenarDisciplina(lista);
                return lista;
            }
        }

        public void Create(Disciplina e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"ArmazenaDisciplina";

            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@qtdAulas", e.QtdAulas);
            cmd.Parameters.AddWithValue("@semestre", e.Semestre);
            cmd.Parameters.AddWithValue("@sigla", e.Sigla);

            cmd.ExecuteNonQuery();
        }

        public void Update(Disciplina e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"AlteraDisciplina";

            cmd.Parameters.AddWithValue("@id", e.DisciplinaId);
            cmd.Parameters.AddWithValue("@codCurso", e.CodCurso);
            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@qtdAulas", e.QtdAulas);
            cmd.Parameters.AddWithValue("@semestre", e.Semestre);
            cmd.Parameters.AddWithValue("@sigla", e.Sigla);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"ApagaDisciplina";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}