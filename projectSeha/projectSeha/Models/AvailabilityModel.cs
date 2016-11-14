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
            SqlDataReader reader = cmd.ExecuteReader();

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

        public void Create(Disponibilidade e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"ArmazenaDisponibilidade";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codProfessor", e.CodProfessor);
            cmd.Parameters.AddWithValue("@codSlot", e.CodSlot);
            cmd.Parameters.AddWithValue("@status_slot", e.Status_slot);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int ProfessorId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"ApagaDisponibilidade";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProfessorId", ProfessorId);
            cmd.ExecuteNonQuery();
        }
    }
}