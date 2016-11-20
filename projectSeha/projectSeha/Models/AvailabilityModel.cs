using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectSeha.Entity;
using System.Data.SqlClient;
using System.Data;

namespace ProjectSeha.Models
{
    public class AvailabilityModel : ModelBase
    {
        public List<Disponibilidade> Read(int id)
        {
            List<Disponibilidade> lista = new List<Disponibilidade>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM ViewDisponibilidades Where CodProfessor = @id";

            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Disponibilidade e = new Disponibilidade();
                        e.CodProfessor = (int)reader["CodProfessor"];
                        e.CodSlot = (int)reader["CodSlot"];
                        e.Status_slot = (bool)reader["status_slot"];

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

        public bool Create(Disponibilidade e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"ArmazenaDisponibilidade";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codProfessor", e.CodProfessor);
            cmd.Parameters.AddWithValue("@codSlot", e.CodSlot);
            cmd.Parameters.AddWithValue("@status_slot", e.Status_slot);

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

        public bool Delete(int ProfessorId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"ApagaDisponibilidade";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProfessorId", ProfessorId);
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
    }
}