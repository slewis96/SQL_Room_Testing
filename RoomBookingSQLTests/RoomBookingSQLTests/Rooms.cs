using System;
using Xunit;
using Npgsql;
using System.IO;

namespace RoomBookingSQLTests
{
    public class Rooms
    {
        public NpgsqlConnection conn = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres;" + "Password=;Database=guitars;");

        [Fact]
        public void StringReturnName()
        {
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT name FROM guitars", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.String"), entry);
                Console.WriteLine();
            }
            conn.Close();
        }
        [Fact]
        public void IntReturnID()
        {
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id FROM guitars", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.Int32"), entry);
            }
            conn.Close();
        }
    }
}
