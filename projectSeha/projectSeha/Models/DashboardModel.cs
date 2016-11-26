using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ProjectSeha.Entity;
using System.Data;

namespace ProjectSeha.Models
{
    public class DashboardModel : ModelBase
    {
        public int CountProf() //retorna total de professores cadastrados
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT COUNT(*) Professores from tblProfessor";
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (int)reader["Professores"];
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        public int CountDiscp() //retorna total de disciplinas cadastrados
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT COUNT(*) Disciplinas from tblDisciplina";
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (int)reader["Disciplinas"];
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        public int CountCurso() //retorna total de courses cadastrados
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT COUNT(*) Cursos from tblCurso";
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (int)reader["Cursos"];
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        public List<Dashboard.CountProf_Discp> CountProf_Discp() //retorna qtd de diciplinas por professor
        {
            List<Dashboard.CountProf_Discp> lista = new List<Dashboard.CountProf_Discp>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM CountProf_Discp";

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dashboard.CountProf_Discp dc = new Dashboard.CountProf_Discp();
                        dc.Nome = (string)reader["NomeGuerra"];
                        dc.QtdDisciplinas = (int)reader["QtdDisciplinas"];

                        lista.Add(dc);
                    }
                    return lista;
                }
            }
            catch
            {
                return lista = null;
            }
        }

        public List<Dashboard.CountProf_Curso> CountProf_Curso() //retorna qtd de cursos por professor
        {
            List<Dashboard.CountProf_Curso> lista = new List<Dashboard.CountProf_Curso>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM CountProf_Curso";

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dashboard.CountProf_Curso dc = new Dashboard.CountProf_Curso();
                        dc.Nome = (string)reader["NomeGuerra"];
                        dc.QtdCursos = (int)reader["QtdCursos"];

                        lista.Add(dc);
                    }
                    return lista;
                }
            }
            catch
            {
                return lista = null;
            }
        }

        public List<Dashboard.CountCurso_Discp> CountCurso_Discp() //retorna qtd de cursos por professor
        {
            List<Dashboard.CountCurso_Discp> lista = new List<Dashboard.CountCurso_Discp>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM CountCurso_Discp";

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dashboard.CountCurso_Discp dc = new Dashboard.CountCurso_Discp();
                        dc.Titulo = (string)reader["Titulo"];
                        dc.Turno = (string)reader["Turno"];
                        dc.QtdDisciplinas = (int)reader["QtdDisciplinas"];

                        lista.Add(dc);
                    }
                    return lista;
                }
            }
            catch
            {
                return lista = null;
            }
        }

        //Lista os professores que ja preencheram a disponibilidade
        public List<Dashboard.CountProf_Available> CountProf_Available()
        {
            List<Dashboard.CountProf_Available> lista = new List<Dashboard.CountProf_Available>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM CountProf_Available";

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dashboard.CountProf_Available dc = new Dashboard.CountProf_Available();
                        dc.CodProfessor = (int)reader["CodProfessor"];
                        dc.NomeGuerra = (string)reader["NomeGuerra"];

                        lista.Add(dc);
                    }
                    return lista;
                }
            }
            catch
            {
                return lista = null;
            }
        }

        //Lista os slots de cada professor
        public List<Dashboard.CountProf_Slot> CountProf_Slot()
        {
            List<Dashboard.CountProf_Slot> lista = new List<Dashboard.CountProf_Slot>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM CountProf_Slot";

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dashboard.CountProf_Slot dc = new Dashboard.CountProf_Slot();
                        dc.CodProfessor = (int)reader["CodProfessor"];
                        dc.HoraInicio = (string)reader["HoraInicio"];
                        dc.status_slot = (bool)reader["status_slot"];

                        lista.Add(dc);
                    }
                    return lista;
                }
            }
            catch
            {
                return lista = null;
            }
        }
    }
}